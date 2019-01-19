using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class StudentEnrollmentModelInput
    {
        public EnrollmentTargetsArray EnrollmentTargetsArray { get; }
        public EnrollmentTransitionsArray TransitionRatesArray { get; }

        public int NumberOfMonthlyPeriodsToProject { get; }
        public int EarlyHireStartingMonth { get; }

        public StudentEnrollmentModelInput(
            EnrollmentTargetsArray enrollmentTargetsArray,
            EnrollmentTransitionsArray transitionRatesArray,
            int numberOfMonthlyPeriodsToProject,
            int earlyHireStartingMonth)
        {
            EnrollmentTargetsArray = enrollmentTargetsArray;
            TransitionRatesArray = transitionRatesArray;

            NumberOfMonthlyPeriodsToProject = numberOfMonthlyPeriodsToProject;
            EarlyHireStartingMonth = earlyHireStartingMonth;
        }
    }
}
