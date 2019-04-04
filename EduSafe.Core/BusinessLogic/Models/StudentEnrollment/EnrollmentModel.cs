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

        public StudentEnrollmentModelInput StudentEnrollmentModelInput { get; }
        public List<EnrollmentStateArray> EnrollmentStateTimeSeries { get; private set; }
          
        private readonly MultiplicativeVector _flatMultiplicativeVector;     

        public EnrollmentModel(StudentEnrollmentModelInput studentEnrollmentModelInput)
        {
            StudentEnrollmentModelInput = studentEnrollmentModelInput;
            _flatMultiplicativeVector = new MultiplicativeVector(new DataCurve<double>(1.0));
        }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public EnrollmentModel Copy()
        {
            var copyOfStudentEnrollmentModelInput = StudentEnrollmentModelInput.Copy();
            return new EnrollmentModel(copyOfStudentEnrollmentModelInput);
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
            var dropOutTarget = StudentEnrollmentModelInput.EnrollmentTargetsArray[StudentEnrollmentState.DroppedOut];
            if (dropOutTarget != null)
            {
                var targetValue = dropOutTarget.TargetValue;
                var dropOutRateParameterizer = new DropOutRateParameterizer(
                    StudentEnrollmentModelInput,            
                    _flatMultiplicativeVector);

                var resultRate = NumericalSearchUtility.NewtonRaphsonWithBisection(
                    dropOutRateParameterizer.Parameterize,
                    targetValue,
                    floorValue: 0.0,
                    ceilingValue: 1.0);

                dropOutRateParameterizer.Parameterize(resultRate);
                EnrollmentStateTimeSeries = dropOutRateParameterizer.EnrollmentStateTimeSeries;
            }
            else
            {
                throw new Exception("ERROR: An overall drop-out target must be provided, either directly or indirectly.");
            }
        }

        private void ParameterizePostGraduationRates()
        {
            StudentEnrollmentModelInput
                .PostGraduationTargetStates.ToList()
                .ForEach(ParameterizePostGraduationRate);
        }

        private void ParameterizePostGraduationRate(StudentEnrollmentState postGraduationState)
        {
            var postGraduationRateParameterizer = new PostGraduationParameterizer(
                    EnrollmentStateTimeSeries,
                    StudentEnrollmentModelInput,
                    _flatMultiplicativeVector,
                    postGraduationState);

            var postGraduationTarget = StudentEnrollmentModelInput.EnrollmentTargetsArray[postGraduationState];
            if (postGraduationTarget != null)
            {
                var targetValue = postGraduationTarget.TargetValue;
                
                var resultRate = NumericalSearchUtility.NewtonRaphsonWithBisection(
                    postGraduationRateParameterizer.Parameterize,
                    targetValue,
                    floorValue: 0.0,
                    ceilingValue: 1.0);

                postGraduationRateParameterizer.Parameterize(resultRate);
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
                StudentEnrollmentModelInput,
                _flatMultiplicativeVector);

            var earlyHireTarget = StudentEnrollmentModelInput.EnrollmentTargetsArray[StudentEnrollmentState.EarlyHire];
            if (earlyHireTarget != null)
            {
                NumericalSearchUtility.BisectionWithNotANumber(
                    earlyHireRateParameterizer.Parameterize, 
                    out double ceilingValue,
                    floorValue: 0.0);

                if (!double.IsNaN(ceilingValue))
                {
                    var targetValue = earlyHireTarget.TargetValue;

                    var resultRate = NumericalSearchUtility.NewtonRaphsonWithBisection(
                        earlyHireRateParameterizer.Parameterize,
                        targetValue,
                        floorValue: 0.0,
                        ceilingValue: ceilingValue);

                    earlyHireRateParameterizer.Parameterize(resultRate);
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
