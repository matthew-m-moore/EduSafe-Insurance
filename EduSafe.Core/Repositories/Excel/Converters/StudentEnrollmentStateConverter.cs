using System;
using EduSafe.Common.Enums;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class StudentEnrollmentStateConverter
    {
        private const string _enrolled = "Enrolled";
        private const string _graduated = "Graduated";
        private const string _dropOut = "DropOut";
        private const string _earlyHire = "EarlyHire";
        private const string _gradSchool = "GradSchool";
        private const string _employed = "Employed";
        private const string _unemployed = "Unemployed";

        public static StudentEnrollmentState ConvertStringToEnrollmentState(string enrollmentStateText)
        {
            if (enrollmentStateText == null) return default(StudentEnrollmentState);

            switch (enrollmentStateText)
            {
                case _enrolled:
                    return StudentEnrollmentState.Enrolled;

                case _graduated:
                    return StudentEnrollmentState.Graduated;

                case _dropOut:
                    return StudentEnrollmentState.DroppedOut;

                case _earlyHire:
                    return StudentEnrollmentState.EarlyHire;

                case _gradSchool:
                    return StudentEnrollmentState.GraduateSchool;

                case _employed:
                    return StudentEnrollmentState.GraduatedEmployed;

                case _unemployed:
                    return StudentEnrollmentState.GraduatedUnemployed;

                default:
                    var exceptionText = string.Format("ERROR: The enrollment state '{0}' is not supported", 
                        enrollmentStateText);
                    throw new NotSupportedException(exceptionText);
            }
        }
    }
}
