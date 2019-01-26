using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class StudentEnrollmentModelInput
    {
        public EnrollmentTargetsArray EnrollmentTargetsArray { get; }
        public EnrollmentTransitionsArray TransitionRatesArray { get; }

        public HashSet<StudentEnrollmentState> PostGraduationTargetStates { get; private set; }

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

            PostGraduationTargetStates = new HashSet<StudentEnrollmentState>();

            NumberOfMonthlyPeriodsToProject = numberOfMonthlyPeriodsToProject;
            EarlyHireStartingMonth = earlyHireStartingMonth;
        }

        public void AddPostGraduationTargetState(StudentEnrollmentState postGraduationEnrollmentState)
        {           
            if (Constants.PostGraduationEnrollmentStates.Contains(postGraduationEnrollmentState))
            {
                PostGraduationTargetStates.Add(postGraduationEnrollmentState);

                if (PostGraduationTargetStates.Count < Constants.PostGraduationEnrollmentStates.Count)
                    return;

                // Basically, this means the sytem is over-determined, so it returns an error
                var tooManyTargetsText = string.Format("ERROR: You cannot target the '{0}' post-graduation state. "
                    + "The following states are already being targeted: ", postGraduationEnrollmentState);

                PostGraduationTargetStates.ToList().ForEach(s => tooManyTargetsText += (" " + s.ToString() + " "));
                throw new Exception(tooManyTargetsText);
            }

            var invalidAddText = string.Format("ERROR: This is not a valid post-graduation enrollment state, '{0}'",
                postGraduationEnrollmentState.ToString());
            throw new Exception(invalidAddText);
        }
    }
}
