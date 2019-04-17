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
        public double TargetValue { get; private set; }

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

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public EnrollmentTarget Copy()
        {
            var copyOfTargetMonth = TargetMonth.HasValue ? new int?(TargetMonth.Value) : null;

            return new EnrollmentTarget(
                TargetEnrollmentState,
                TargetValue,
                copyOfTargetMonth);
        }

        public void SetTargetValue(double targetValue)
        {
            TargetValue = targetValue;
        }
    }
}
