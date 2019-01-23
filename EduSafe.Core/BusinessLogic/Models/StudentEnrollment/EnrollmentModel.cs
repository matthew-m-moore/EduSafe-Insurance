using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment.Parameterization;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentModel
    {
        public bool IsParameterized = false;

        public List<EnrollmentStateArray> EnrollmentStateTimeSeries { get; private set; }

        private StudentEnrollmentModelInput _studentEnrollmentModelInput;       
        private MultiplicativeVector _flatMultiplicativeVector;     

        public EnrollmentModel(StudentEnrollmentModelInput studentEnrollmentModelInput)
        {
            _studentEnrollmentModelInput = studentEnrollmentModelInput;
            _flatMultiplicativeVector = new MultiplicativeVector(new DataCurve<double>(1.0));
        }

        public void ParameterizeModel()
        {
            ParameterizeDropOutRate();
            ParameterizePostGraduationRates();
            ParameterizeEarlyHireRate();

            IsParameterized = true;          
        }

        private void ParameterizeDropOutRate()
        {
            var dropOutTarget = _studentEnrollmentModelInput.EnrollmentTargetsArray[StudentEnrollmentState.DroppedOut];
            if (dropOutTarget != null)
            {
                var targetValue = dropOutTarget.TargetValue;
                var dropOutRateParameterizer = new DropOutRateParameterizer(
                    _studentEnrollmentModelInput,            
                    _flatMultiplicativeVector);

                NumericalSearchUtility.NewtonRaphsonWithBisection(
                    dropOutRateParameterizer.Parameterize,
                    targetValue,
                    floorValue: 0.0,
                    ceilingValue: 1.0);

                EnrollmentStateTimeSeries = dropOutRateParameterizer.EnrollmentStateTimeSeries;
            }
            else
            {
                throw new Exception("ERROR: An overall drop-out target must be provided, either directly or indirectly.");
            }
        }

        private void ParameterizePostGraduationRates()
        {
            _studentEnrollmentModelInput
                .PostGraduationTargetStates.ToList()
                .ForEach(ParameterizePostGraduationRate);
        }

        private void ParameterizePostGraduationRate(StudentEnrollmentState postGraduationState)
        {
            var postGraduationRateParameterizer = new PostGraduationParameterizer(
                    EnrollmentStateTimeSeries, // Need to do a deep copy of this first
                    _studentEnrollmentModelInput,
                    _flatMultiplicativeVector,
                    postGraduationState);

            var postGraduationTarget = _studentEnrollmentModelInput.EnrollmentTargetsArray[postGraduationState];
            if (postGraduationTarget != null)
            {
                var targetValue = postGraduationTarget.TargetValue;
                
                NumericalSearchUtility.NewtonRaphsonWithBisection(
                    postGraduationRateParameterizer.Parameterize,
                    targetValue,
                    floorValue: 0.0,
                    ceilingValue: 1.0);
            }
            else
            {
                // Note: Parameterization without a target will just default to whatever input rates are given
                postGraduationRateParameterizer.Parameterize();
            }

            EnrollmentStateTimeSeries = postGraduationRateParameterizer.OutputEnrollmentStateTimeSeries;
        }

        private void ParameterizeEarlyHireRate()
        {
            var earlyHireRateParameterizer = new EarlyHireRateParameterizer(
                EnrollmentStateTimeSeries,
                _studentEnrollmentModelInput,
                _flatMultiplicativeVector);

            var earlyHireTarget = _studentEnrollmentModelInput.EnrollmentTargetsArray[StudentEnrollmentState.EarlyHire];
            if (earlyHireTarget != null)
            {
                NumericalSearchUtility.BisectionWithNotANumber(
                    earlyHireRateParameterizer.Parameterize, 
                    out double ceilingValue,
                    floorValue: 0.0);

                if (!double.IsNaN(ceilingValue))
                {
                    var targetValue = earlyHireTarget.TargetValue;

                    NumericalSearchUtility.NewtonRaphsonWithBisection(
                        earlyHireRateParameterizer.Parameterize,
                        targetValue,
                        floorValue: 0.0,
                        ceilingValue: ceilingValue);
                }
                else
                {
                    throw new Exception("ERROR: Cannot find a valid range to serach for early hire rates");
                }
            }
            else
            {
                // Note: Parameterization without a target will just default to whatever input rates are given,
                // even if this returns a double.NaN due to invalid inputs.
                earlyHireRateParameterizer.Parameterize();
            }

            EnrollmentStateTimeSeries = earlyHireRateParameterizer.OutputEnrollmentStateTimeSeries;
        }   
    }
}
