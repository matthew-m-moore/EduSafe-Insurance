using System.Collections.Generic;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.CompoundKeys;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class EnrollmentModelConverter
    {
        private Dictionary<VectorAssignmentEntry, Vector> _vectorAssignmentsDictionary;

        public EnrollmentModelConverter(Dictionary<VectorAssignmentEntry, Vector> vectorAssignmentsDictionary)
        {
            _vectorAssignmentsDictionary = vectorAssignmentsDictionary;
        }

        public EnrollmentModel ConvertEnrollmentModelScenarioToEnrollmentModelInput
            (EnrollmentModelScenarioRecord enrollmentModelScenarioRecord)
        {         
            var numberOfMonthlyPeriodsToProject = enrollmentModelScenarioRecord.TotalMonths;
            var earlyHireStartingPeriod = enrollmentModelScenarioRecord.EarlyHireStartPeriod;

            var enrollmentTargetsArray = CreateEnrollmentTargetsArray(enrollmentModelScenarioRecord);
            var enrollmentTransitionsArray = CreateEnrollmentTransitionsArray(
                enrollmentModelScenarioRecord,
                numberOfMonthlyPeriodsToProject, 
                earlyHireStartingPeriod);

            var studentEnrollmentModelInput = new StudentEnrollmentModelInput(
                enrollmentTargetsArray,
                enrollmentTransitionsArray,
                numberOfMonthlyPeriodsToProject,
                earlyHireStartingPeriod);

            studentEnrollmentModelInput.AddPostGraduationTargetState(StudentEnrollmentState.GraduateSchool);
            studentEnrollmentModelInput.AddPostGraduationTargetState(StudentEnrollmentState.GraduatedEmployed);

            return new EnrollmentModel(studentEnrollmentModelInput);
        }

        private EnrollmentTargetsArray CreateEnrollmentTargetsArray(EnrollmentModelScenarioRecord enrollmentModelScenarioRecord)
        {
            var enrollmentTargetsArray = new EnrollmentTargetsArray();

            var fourYearGradTarget = enrollmentModelScenarioRecord.FourYearGradTarget / Constants.PercentagePoints;
            var fiveYearGradTarget = enrollmentModelScenarioRecord.FiveYearGradTarget / Constants.PercentagePoints;
            var sixYearGradTarget = enrollmentModelScenarioRecord.SixYearGradTarget / Constants.PercentagePoints;

            var fourYearsInMonths = 4 * Constants.MonthsInOneYear;
            var fiveYearsInMonths = 5 * Constants.MonthsInOneYear;
            var sixYearsInMonths = 6 * Constants.MonthsInOneYear;

            var graduationState = StudentEnrollmentState.Graduated;
            enrollmentTargetsArray[fourYearsInMonths, graduationState] = new EnrollmentTarget(graduationState, fourYearGradTarget, fourYearsInMonths);
            enrollmentTargetsArray[fiveYearsInMonths, graduationState] = new EnrollmentTarget(graduationState, fiveYearGradTarget, fiveYearsInMonths);
            enrollmentTargetsArray[sixYearsInMonths, graduationState] = new EnrollmentTarget(graduationState, sixYearGradTarget, sixYearsInMonths);

            var dropOutState = StudentEnrollmentState.DroppedOut;
            var dropOutValue = 1.0 - enrollmentTargetsArray.TotalTarget(StudentEnrollmentState.Graduated);
            enrollmentTargetsArray[dropOutState] = new EnrollmentTarget(dropOutState, dropOutValue);

            var totalHiredTarget = enrollmentModelScenarioRecord.HireTarget + enrollmentModelScenarioRecord.HireEarlyTarget;
            var employeedState = StudentEnrollmentState.GraduatedEmployed;
            enrollmentTargetsArray[employeedState] = new EnrollmentTarget(employeedState, totalHiredTarget / Constants.PercentagePoints);

            var gradSchoolState = StudentEnrollmentState.GraduateSchool;
            enrollmentTargetsArray[gradSchoolState] =
                new EnrollmentTarget(gradSchoolState, enrollmentModelScenarioRecord.GradSchoolTarget / Constants.PercentagePoints);
                       
            var earlyHireState = StudentEnrollmentState.EarlyHire;
            enrollmentTargetsArray[earlyHireState] =
                new EnrollmentTarget(earlyHireState, enrollmentModelScenarioRecord.HireEarlyTarget / Constants.PercentagePoints);

            return enrollmentTargetsArray;
        }

        private EnrollmentTransitionsArray CreateEnrollmentTransitionsArray(
            EnrollmentModelScenarioRecord enrollmentModelScenarioRecord, 
            int numberOfMonthlyPeriodsToProject, 
            int earlyHireStartPeriod)
        {
            // If there aren't any curves provided, just return an empty object
            var enrollmentTransitionsArray = new EnrollmentTransitionsArray();
            if (!_vectorAssignmentsDictionary.Any()) return enrollmentTransitionsArray;

            var holdingPlaceRate = 0.01;
            var vectorSetName = enrollmentModelScenarioRecord.VectorSetName;

            foreach (var vectorAssignment in _vectorAssignmentsDictionary)
            {
                // If this is not the right vector set, then skip it
                if (vectorAssignment.Key.VectorSetName != vectorSetName) continue;

                var startPeriod = (vectorAssignment.Key.EndState == StudentEnrollmentState.EarlyHire)
                    ? earlyHireStartPeriod : 1;

                for (var period = startPeriod; period <= numberOfMonthlyPeriodsToProject; period++)
                {
                    var transitionRate = new EnrollmentTransition(
                        vectorAssignment.Key.StartState,
                        vectorAssignment.Key.EndState,
                        vectorAssignment.Value, 
                        period);

                    // This holding place rate will get adjusted during optimization
                    transitionRate.SetBaseTransitionRate(holdingPlaceRate);
                    enrollmentTransitionsArray[transitionRate] = transitionRate;
                }
            }

            return enrollmentTransitionsArray;
        }
    }
}
