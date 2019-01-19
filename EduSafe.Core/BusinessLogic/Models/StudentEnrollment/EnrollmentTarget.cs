using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentTarget
    {
        public bool IsTotalTarget => !TargetMonth.HasValue;

        public StudentEnrollmentState TargetEnrollmentState { get; }
        public int? TargetMonth { get; }
        public double TargetValue { get; }

        public EnrollmentTarget(
            StudentEnrollmentState targetEnrollmentState,
            double targetValue,
            int? targetMonth)
        {
            TargetEnrollmentState = targetEnrollmentState;
            TargetValue = targetValue;
            TargetMonth = targetMonth;
        }

        public EnrollmentTarget(
            StudentEnrollmentState targetEnrollmentState,
            double targetValue) : 
                this(targetEnrollmentState, targetValue, null)
        { }
    }
}
