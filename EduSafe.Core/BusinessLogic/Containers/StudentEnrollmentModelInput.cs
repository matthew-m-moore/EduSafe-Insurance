using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class StudentEnrollmentModelInput
    {
        public StudentEnrollmentState TargetEnrollmentState { get; }
        public List<(int TargetMonth, double TargetValue)> EnrollmentTargetValues { get; }

        public Dictionary<StudentEnrollmentState, Vector> VectorsDictionary { get; }
        public Dictionary<StudentEnrollmentState, StudentEnrollmentCurvesDictionary> EnrollmentArray { get; }
    }
}
