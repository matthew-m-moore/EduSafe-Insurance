using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.IO.Excel;


namespace EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment
{
    [TestClass]
    public class EnrollmentModelTests
    {
        private bool _outputExel = false;
        private bool _analyticalOuput = false;
        private double _precision = 1e-8;

        private EnrollmentModel _studentEnrollmentModel;
        private ServicingCostsModel _servicingCostsModel;
        private PremiumCalculation _premiumCalculation;

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithPostgraduationTargets_NumericalPremiumSearch()
        {
            _analyticalOuput = false;
            _studentEnrollmentModel = PopulateEnrollmentModel(includePostGraduationTargets: true);
            _studentEnrollmentModel.ParameterizeModel();

            _servicingCostsModel = PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = _studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumNumerically(premiumCalculationModelInput, enrollmentStateTimeSeries, out DataTable servicingCosts);
            CheckResults(premium);
            
            if (_outputExel)
                OutputResultsToExcel(servicingCosts);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithoutPostgraduationTargets_NumericalPremiumSearch()
        {
            _analyticalOuput = false;
            _studentEnrollmentModel = PopulateEnrollmentModel(includePostGraduationTargets: false);
            _studentEnrollmentModel.ParameterizeModel();

            _servicingCostsModel = PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = _studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumNumerically(premiumCalculationModelInput, enrollmentStateTimeSeries, out DataTable servicingCosts);
            CheckResults(premium);

            if (_outputExel)
                OutputResultsToExcel(servicingCosts);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithPostgraduationTargets_AnalyticalPremiumCalculation()
        {
            _analyticalOuput = true;
            _studentEnrollmentModel = PopulateEnrollmentModel(includePostGraduationTargets: true);
            _studentEnrollmentModel.ParameterizeModel();

            _servicingCostsModel = PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = _studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumAnalytically(premiumCalculationModelInput, enrollmentStateTimeSeries, out DataTable servicingCosts);
            CheckResults(premium);

            if (_outputExel)
                OutputResultsToExcel(servicingCosts);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithoutPostgraduationTargets_AnalyticalPremiumCalculation()
        {
            _analyticalOuput = true;
            _studentEnrollmentModel = PopulateEnrollmentModel(includePostGraduationTargets: false);
            _studentEnrollmentModel.ParameterizeModel();

            _servicingCostsModel = PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = _studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumAnalytically(premiumCalculationModelInput, enrollmentStateTimeSeries, out DataTable servicingCosts);
            CheckResults(premium);

            if (_outputExel)
                OutputResultsToExcel(servicingCosts);
        }

        private void CheckResults(double premium)
        {
            Assert.IsTrue(_studentEnrollmentModel.IsParameterized);

            var totalEnrollmentChange = _studentEnrollmentModel.EnrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.Enrolled]);
            Assert.AreEqual(-1.0, totalEnrollmentChange, _precision);

            var totalDropOuts = _studentEnrollmentModel.EnrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.DroppedOut]);
            Assert.AreEqual(0.40, totalDropOuts, _precision);

            totalDropOuts = _studentEnrollmentModel.EnrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.DroppedOut);
            Assert.AreEqual(0.40, totalDropOuts, _precision);

            var totalGradStudents = _studentEnrollmentModel.EnrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.GraduateSchool]);
            Assert.AreEqual(0.15, totalGradStudents, _precision);

            totalGradStudents = _studentEnrollmentModel.EnrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.GraduateSchool);
            Assert.AreEqual(0.15, totalGradStudents, _precision);

            var totalEarlyHires = _studentEnrollmentModel.EnrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.EarlyHire]);
            Assert.AreEqual(0.05, totalEarlyHires, _precision);

            totalEarlyHires = _studentEnrollmentModel.EnrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.EarlyHire);
            Assert.AreEqual(0.05, totalEarlyHires, _precision);

            var totalEmployed = _studentEnrollmentModel.EnrollmentStateTimeSeries.Sum(e => e[StudentEnrollmentState.GraduatedEmployed]);
            Assert.AreEqual(0.39, totalEmployed + totalEarlyHires, _precision);

            totalEmployed = _studentEnrollmentModel.EnrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.GraduatedEmployed);
            Assert.AreEqual(0.39, totalEmployed + totalEarlyHires, _precision);

            Assert.AreEqual(82.35792486, premium, _precision);
        }

        private void OutputResultsToExcel(DataTable servicingCosts)
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
            excelFileWriter.AddWorksheetForListOfData(listOfTimeSeriesEntries.ToList(), "Enrollment Model");
            excelFileWriter.AddWorksheetForDataTable(servicingCosts, "Servicing Costs");
                
            if (_analyticalOuput)
            {
                var analyticalPremiumCalculationCashFlows = 
                    _premiumCalculation.CalculatedCashFlows.Select(c => (AnalyticalPremiumCalculationCashFlow)c).ToList();

                excelFileWriter.AddWorksheetForListOfData(analyticalPremiumCalculationCashFlows, "Cash Flows");
            }
            else
            {
                excelFileWriter.AddWorksheetForListOfData(_premiumCalculation.CalculatedCashFlows, "Cash Flows");
            }

            excelFileWriter.ExportWorkbook();
        }

        public PremiumCalculationModelInput PreparePremiumCalculationModelInput()
        {
            var discountFactorCurve = new InterestRateCurve(
                InterestRateCurveType.Treasury1Mo,
                new DateTime(2018, 12, 19),
                new DataCurve<double>(0.0235), 1, 
                DayCountConvention.Thirty360);

            var annualIncomeCoverage = 50000;
            var monthsOfIncomeCoverage = 6;
            var dropOutCoverageOption = 0.25;
            var gradSchoolCoverageOption = 0.25;
            var earlyHireCoverageOption = 0.25;

            var premiumCalculationModelInput = 
                new PremiumCalculationModelInput(
                    annualIncomeCoverage, 
                    monthsOfIncomeCoverage, 
                    discountFactorCurve,
                    dropOutCoverageOption,
                    gradSchoolCoverageOption,
                    earlyHireCoverageOption);

            return premiumCalculationModelInput;
        }

        private double CalculatePremiumNumerically(
            PremiumCalculationModelInput premiumCalculationModelInput,
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            out DataTable servicingCosts)
        {
            _premiumCalculation = new NumericalPremiumCalculation(premiumCalculationModelInput);
            var premium = _premiumCalculation.CalculatePremium(enrollmentStateTimeSeries, _servicingCostsModel);

            servicingCosts = _premiumCalculation.ServicingCostsDataTable;
            return premium;
        }

        private double CalculatePremiumAnalytically(
            PremiumCalculationModelInput premiumCalculationModelInput,
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            out DataTable servicingCosts)
        {
            _premiumCalculation = new AnalyticalPremiumCalculation(premiumCalculationModelInput);
            var premium = _premiumCalculation.CalculatePremium(enrollmentStateTimeSeries, _servicingCostsModel);

            servicingCosts = _premiumCalculation.ServicingCostsDataTable;
            return premium;
        }

        public ServicingCostsModel PopulateServicingCostsModel()
        {
            var listOfCostsOrFees = new List<CostOrFee>();

            var backgroundCheckFee = new PeriodicCostOrFee(PaymentConvention.Annual, "Background Check", 100);
            var creditScoreFee = new PeriodicCostOrFee(PaymentConvention.Annual, "Credit Score", 25);
            var transcriptsFee = new PeriodicCostOrFee(PaymentConvention.Annual, "Transcripts", 25);
            var servicingFee = new PeriodicCostOrFee(PaymentConvention.Monthly, "Servicing", 20);

            var dropOutVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.DroppedOut, "Drop Out Verification", 150);
            var gradSchoolVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.GraduateSchool, "Graduate School Verification", 150);
            var earlyHireVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.EarlyHire, "Early Hire Verification", 300);
            var unemploymentVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.GraduatedUnemployed, "Unemployment Verification", 1000);

            listOfCostsOrFees.Add(backgroundCheckFee);
            listOfCostsOrFees.Add(creditScoreFee);
            listOfCostsOrFees.Add(transcriptsFee);
            listOfCostsOrFees.Add(servicingFee);

            listOfCostsOrFees.Add(dropOutVerificationFee);
            listOfCostsOrFees.Add(gradSchoolVerificationFee);
            listOfCostsOrFees.Add(earlyHireVerificationFee);
            listOfCostsOrFees.Add(unemploymentVerificationFee);

            return new ServicingCostsModel(listOfCostsOrFees, 72);
        }

        public EnrollmentModel PopulateEnrollmentModel(bool includePostGraduationTargets)
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
