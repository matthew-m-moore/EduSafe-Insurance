using System;
using System.Collections.Generic;
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

        private const double _targetPrecision = 1e-14;

        public EnrollmentModel(StudentEnrollmentModelInput studentEnrollmentModelInput)
        {
            _studentEnrollmentModelInput = studentEnrollmentModelInput;
            _flatMultiplicativeVector = new MultiplicativeVector(new DataCurve<double>(1.0));
        }

        public void ParameterizeModel()
        {
            ParameterizeDropOutRate();
            ParameterizeGraduateSchoolRate();
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
                    _targetPrecision,
                    floorValue: 0.0,
                    ceilingValue: 1.0);

                EnrollmentStateTimeSeries = dropOutRateParameterizer.EnrollmentStateTimeSeries;
            }
            else
            {
                throw new Exception("ERROR: An overall drop-out target must be provided.");
            }
        }        

        private void ParameterizeGraduateSchoolRate()
        {

        }

        private void ParameterizeEarlyHireRate()
        {

        }   
    }
}
