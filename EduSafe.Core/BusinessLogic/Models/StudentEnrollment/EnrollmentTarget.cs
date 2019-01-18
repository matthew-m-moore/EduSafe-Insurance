using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class StudentEnrollmentTarget
    {
        public bool IsTotalTarget => !TargetMonth.HasValue;

        public StudentEnrollmentState TargetEnrollmentState { get; }
        public int? TargetMonth { get; }
        public double TargetValue { get; }
    }
}
