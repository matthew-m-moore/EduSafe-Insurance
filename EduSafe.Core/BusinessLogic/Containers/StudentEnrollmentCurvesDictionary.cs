using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class StudentEnrollmentCurvesDictionary
    {
        private Dictionary<StudentEnrollmentState, DataCurve<double>> _enrollmentCurvesDictionary { get; }
    }
}
