using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.IO.Excel;

namespace EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment
{
    [TestClass]
    public class EnrollmentModelTests
    {
        private bool _outputExcel = false;
        private double _precision = 1e-8;

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithPostgraduationTargets()
        {
            var studentEnrollmentModel = PopulateEnrollmentModel(includePostGraduationTargets: true);
            studentEnrollmentModel.ParameterizeModel();
            Assert.IsTrue(studentEnrollmentModel.IsParameterized);

            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            CheckResults(enrollmentStateTimeSeries);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(enrollmentStateTimeSeries);
                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithoutPostgraduationTargets()
        {
            var studentEnrollmentModel = PopulateEnrollmentModel(includePostGraduationTargets: false);
            studentEnrollmentModel.ParameterizeModel();
            Assert.IsTrue(studentEnrollmentModel.IsParameterized);

            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            CheckResults(enrollmentStateTimeSeries);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(enrollmentStateTimeSeries);
                excelFileWriter.ExportWorkbook();
            }
        }

        private void CheckResults(List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            var totalEnrollmentChange = enrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.Enrolled]);
            Assert.AreEqual(-1.0, totalEnrollmentChange, _precision);

            var totalDropOuts = enrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.DroppedOut]);
            Assert.AreEqual(0.40, totalDropOuts, _precision);

            totalDropOuts = enrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.DroppedOut);
            Assert.AreEqual(0.40, totalDropOuts, _precision);

            var totalGradStudents = enrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.GraduateSchool]);
            Assert.AreEqual(0.15, totalGradStudents, _precision);

            totalGradStudents = enrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.GraduateSchool);
            Assert.AreEqual(0.15, totalGradStudents, _precision);

            var totalEarlyHires = enrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.EarlyHire]);
            Assert.AreEqual(0.05, totalEarlyHires, _precision);

            totalEarlyHires = enrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.EarlyHire);
            Assert.AreEqual(0.05, totalEarlyHires, _precision);

            var totalEmployed = enrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.GraduatedEmployed]);
            Assert.AreEqual(0.39, totalEmployed + totalEarlyHires, _precision);

            totalEmployed = enrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.GraduatedEmployed);
            Assert.AreEqual(0.39, totalEmployed + totalEarlyHires, _precision);
        }

        public static ExcelFileWriter CreateExcelOutput(List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            var listOfTimeSeriesEntries = 
                enrollmentStateTimeSeries.Select((enrollmentStateArray, i) =>
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
            excelFileWriter.AddWorksheetForListOfData(listOfTimeSeriesEntries.ToList(), "Enrollment Model");

            return excelFileWriter;
        }

        public static EnrollmentModel PopulateEnrollmentModel(bool includePostGraduationTargets)
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

            return new EnrollmentModel(studentEnrollmentModelInput);
        }

        private static void AddPostGraduationTargets(EnrollmentTargetsArray enrollmentTargetsArray)
        {
            var gradSchoolState = StudentEnrollmentState.GraduateSchool;
            enrollmentTargetsArray[gradSchoolState] = new EnrollmentTarget(gradSchoolState, 0.15);

            var employeedState = StudentEnrollmentState.GraduatedEmployed;
            enrollmentTargetsArray[employeedState] = new EnrollmentTarget(employeedState, 0.39);

            var earlyHireState = StudentEnrollmentState.EarlyHire;
            enrollmentTargetsArray[earlyHireState] = new EnrollmentTarget(earlyHireState, 0.05);
        }

        private static void AddPostGraduationTransitionRates(EnrollmentTransitionsArray enrollmentTransitionsArray)
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
