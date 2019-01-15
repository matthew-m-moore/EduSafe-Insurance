using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Enums;

namespace EduSafe.IO.Excel.Records
{
    public class StudentEnrollmentStateTargetRecord
    {
        public StudentEnrollmentState EnrollmentState { get; set; }
        public double TargetValue { get; set; }
        public int TargetMonth { get; set; }
    }
}
