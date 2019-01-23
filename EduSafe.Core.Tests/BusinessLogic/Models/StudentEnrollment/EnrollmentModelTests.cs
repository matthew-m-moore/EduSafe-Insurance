using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.IO.Excel;

namespace EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment
{
    [TestClass]
    public class EnrollmentModelTests
    {
        private EnrollmentModel _studentEnrollmentModel;

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_ProofOfConceptTest_WithPostgraduationTargets()
        {
            PopulateEnrollmentModel(true);
            _studentEnrollmentModel.ParameterizeModel();
            Assert.IsTrue(_studentEnrollmentModel.IsParameterized);

            OutputEnrollmentArrayTimeSeriesToExcel();
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_ProofOfConceptTest_WithoutPostgraduationTargets()
        {
            PopulateEnrollmentModel(false);
            _studentEnrollmentModel.ParameterizeModel();
            Assert.IsTrue(_studentEnrollmentModel.IsParameterized);

            OutputEnrollmentArrayTimeSeriesToExcel();
        }

        private void OutputEnrollmentArrayTimeSeriesToExcel()
        {
            var listOfTimeSeriesEntries = _studentEnrollmentModel.EnrollmentStateTimeSeries
                .Select((enrollmentStateArray, i) =>
                    {
                        return new StudentEnrollmentStateTimeSeriesEntry
                        {
                            Period = i,

                            Enrolled = enrollmentStateArray.GetTotalState(StudentEnrollmentState.Enrolled),
                            DroppedOut = enrollmentStateArray.GetTotalState(StudentEnrollmentState.DroppedOut),
                            Graduated = enrollmentStateArray.GetTotalState(StudentEnrollmentState.Graduated),
                            Employed = enrollmentStateArray.GetTotalState(StudentEnrollmentState.GraduatedEmployed),
                            EalyHire = enrollmentStateArray.GetTotalState(StudentEnrollmentState.EarlyHire),
                            Unemployed = enrollmentStateArray.GetTotalState(StudentEnrollmentState.GraduatedUnemployed),
                            GradSchool = enrollmentStateArray.GetTotalState(StudentEnrollmentState.GraduateSchool),

                            DeltaEnrolled = enrollmentStateArray[StudentEnrollmentState.Enrolled],
                            DeltaDroppedOut = enrollmentStateArray[StudentEnrollmentState.DroppedOut],
                            DeltaGraduated = enrollmentStateArray[StudentEnrollmentState.Graduated],
                            DeltaEmployed = enrollmentStateArray[StudentEnrollmentState.GraduatedEmployed],
                            DeltaEalyHire = enrollmentStateArray[StudentEnrollmentState.EarlyHire],
                            DeltaUnemployed = enrollmentStateArray[StudentEnrollmentState.GraduatedUnemployed],
                            DeltaGradSchool = enrollmentStateArray[StudentEnrollmentState.GraduateSchool],
                        };
                    }
                );

            var excelFileWriter = new ExcelFileWriter(openFileOnSave: true);
            excelFileWriter.AddWorksheetForListOfData(listOfTimeSeriesEntries.ToList());
            excelFileWriter.ExportWorkbook();
        }

        private void PopulateEnrollmentModel(bool includePostGraduationTargets)
        {
            var enrollmentTargetsArray = new EnrollmentTargetsArray();

            var graduationState = StudentEnrollmentState.Graduated;
            enrollmentTargetsArray[48, graduationState] = new EnrollmentTarget(graduationState, 0.40, 48);
            enrollmentTargetsArray[60, graduationState] = new EnrollmentTarget(graduationState, 0.15, 60);
            enrollmentTargetsArray[72, graduationState] = new EnrollmentTarget(graduationState, 0.05, 72);

            var dropOutState = StudentEnrollmentState.DroppedOut;
            var dropOutValue = 1.0 - enrollmentTargetsArray.TotalTarget(StudentEnrollmentState.Graduated);
            enrollmentTargetsArray[dropOutState] = new EnrollmentTarget(dropOutState, dropOutValue);

            var enrollmentTransitionsArray = new EnrollmentTransitionsArray();

            if (includePostGraduationTargets)
                AddPostGraduationTargets(enrollmentTargetsArray);
            else
                AddPostGraduationTransitionRates(enrollmentTransitionsArray);

            var numberOfMonthlyPeriodsToProject = 72;
            var earlyHireStartingPeriod = 36;

            var studentEnrollmentModelInput = new StudentEnrollmentModelInput(
                enrollmentTargetsArray,
                enrollmentTransitionsArray,
                numberOfMonthlyPeriodsToProject,
                earlyHireStartingPeriod);

            studentEnrollmentModelInput.AddPostGraduationTargetState(StudentEnrollmentState.GraduateSchool);
            studentEnrollmentModelInput.AddPostGraduationTargetState(StudentEnrollmentState.GraduatedEmployed);

            _studentEnrollmentModel = new EnrollmentModel(studentEnrollmentModelInput);
        }

        private void AddPostGraduationTargets(EnrollmentTargetsArray enrollmentTargetsArray)
        {
            var gradSchoolState = StudentEnrollmentState.GraduateSchool;
            enrollmentTargetsArray[gradSchoolState] = new EnrollmentTarget(gradSchoolState, 0.15);

            var employeedState = StudentEnrollmentState.GraduatedEmployed;
            enrollmentTargetsArray[employeedState] = new EnrollmentTarget(employeedState, 0.39);

            var earlyHireState = StudentEnrollmentState.EarlyHire;
            enrollmentTargetsArray[earlyHireState] = new EnrollmentTarget(earlyHireState, 0.05);
        }

        private void AddPostGraduationTransitionRates(EnrollmentTransitionsArray enrollmentTransitionsArray)
        {
            var flatMultiplicativeVector = new MultiplicativeVector(new DataCurve<double>(1.0));

            var hiringTransitionRate1 = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                StudentEnrollmentState.GraduatedEmployed,
                flatMultiplicativeVector, 48);

            var hiringTransitionRate2 = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                StudentEnrollmentState.GraduatedEmployed,
                flatMultiplicativeVector, 60);

            var hiringTransitionRate3 = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                StudentEnrollmentState.GraduatedEmployed,
                flatMultiplicativeVector, 72);

            var hiringRate = 0.65;
            hiringTransitionRate1.SetBaseTransitionRate(hiringRate);
            hiringTransitionRate2.SetBaseTransitionRate(hiringRate);
            hiringTransitionRate3.SetBaseTransitionRate(hiringRate);

            enrollmentTransitionsArray[hiringTransitionRate1] = hiringTransitionRate1;
            enrollmentTransitionsArray[hiringTransitionRate2] = hiringTransitionRate2;
            enrollmentTransitionsArray[hiringTransitionRate3] = hiringTransitionRate3;

            var gradSchoolTransitionRate1 = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                StudentEnrollmentState.GraduateSchool,
                flatMultiplicativeVector, 48);

            var gradSchoolTransitionRate2 = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                StudentEnrollmentState.GraduateSchool,
                flatMultiplicativeVector, 60);

            var gradSchoolTransitionRate3 = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                StudentEnrollmentState.GraduateSchool,
                flatMultiplicativeVector, 72);

            var graduateSchoolRate = 0.25;
            gradSchoolTransitionRate1.SetBaseTransitionRate(graduateSchoolRate);
            gradSchoolTransitionRate2.SetBaseTransitionRate(graduateSchoolRate);
            gradSchoolTransitionRate3.SetBaseTransitionRate(graduateSchoolRate);

            enrollmentTransitionsArray[gradSchoolTransitionRate1] = gradSchoolTransitionRate1;
            enrollmentTransitionsArray[gradSchoolTransitionRate2] = gradSchoolTransitionRate2;
            enrollmentTransitionsArray[gradSchoolTransitionRate3] = gradSchoolTransitionRate3;

            var earlyHireRate = 0.00440975890188199;
            for (var period = 36; period <= 72; period++)
            {
                var earlyHireTransitionRate = new EnrollmentTransition(
                    StudentEnrollmentState.Enrolled,
                    StudentEnrollmentState.EarlyHire,
                    flatMultiplicativeVector, period);

                earlyHireTransitionRate.SetBaseTransitionRate(earlyHireRate);
                enrollmentTransitionsArray[earlyHireTransitionRate] = earlyHireTransitionRate;
            }
        }
    }
}
