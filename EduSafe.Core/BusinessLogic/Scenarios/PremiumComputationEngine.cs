using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Scenarios
{
    public class PremiumComputationEngine
    {
        public int? ScenarioId { get; set; }
        public string ScenarioName { get; set; }

        public PremiumCalculation PremiumCalculation { get; }
        public RollForwardRepricingModel RepricingModel { get; }

        public bool IsPremiumComputed { get; private set; }

        private int? _startingPeriod = null;
        private double? _givenPremium = null;

        public PremiumComputationEngine(
            PremiumCalculation premiumCalculation,
            RollForwardRepricingModel repricingModel,
            int? startingPeriod = null,
            double? givenPremium = null)
        {
            PremiumCalculation = premiumCalculation;
            RepricingModel = repricingModel;
            _startingPeriod = startingPeriod ?? _startingPeriod;
            _givenPremium = givenPremium ?? _givenPremium;
            IsPremiumComputed = false;
        }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public PremiumComputationEngine Copy()
        {
            var copyOfPremiumCalculation = PremiumCalculation.Copy();
            var copyOfRepricingModel = RepricingModel.Copy();
            var copyOfStartingPeriod = _startingPeriod.HasValue ? new int?(_startingPeriod.Value) : null;
            var copyOfGivenPremium = _givenPremium.HasValue ? new double?(_givenPremium.Value) : null;

            copyOfPremiumCalculation.NumberOfForecastedPeriodsAhead = PremiumCalculation.NumberOfForecastedPeriodsAhead;

            return new PremiumComputationEngine(
                copyOfPremiumCalculation,
                copyOfRepricingModel,
                copyOfStartingPeriod,
                copyOfGivenPremium);
        }

        /// <summary>
        /// Computes a premium result object with options to roll-forward the starting period of the student, and
        /// treat the student as new or old. If a given premium was provided during creation of the object, it will be
        /// used in place of solving for a premium.
        /// </summary>
        public PremiumComputationResult ComputePremiumResult(int? startingPeriod = null, bool isNewStudent = true)
        {
            var rollForwardPeriod = startingPeriod ?? _startingPeriod.GetValueOrDefault(0);
            var enrollmentStateTimeSeries = RepricingModel.RollForwardEnrollmentStates(rollForwardPeriod);
            var servicingCosts = RepricingModel.RollForwardServicingCosts(rollForwardPeriod, isNewStudent);

            double premium;
            if (_givenPremium.HasValue)
            {
                PremiumCalculation.CalculateResultWithGivenPremium(enrollmentStateTimeSeries, servicingCosts, _givenPremium.Value);
                premium = _givenPremium.Value;
            }
            else
            {
                premium = PremiumCalculation.CalculatePremium(enrollmentStateTimeSeries, servicingCosts);
                IsPremiumComputed = true;
            }

            var premiumComputationResult = new PremiumComputationResult
            {
                ScenarioId = ScenarioId,
                ScenarioName = ScenarioName,

                ServicingCosts = PremiumCalculation.ServicingCostsDataTable,
                PremiumCalculationCashFlows = PremiumCalculation.CalculatedCashFlows,
                EnrollmentStateTimeSeries = CreateTimeSeriesEntries(enrollmentStateTimeSeries),
                TotalCoverageAmount = PremiumCalculation.PremiumCalculationModelInput.IncomeCoverageAmount,
                CalculatedMonthlyPremium = premium,
            };

            return premiumComputationResult;
        }

        /// <summary>
        /// Computes a premium result object using the premium provided, and ignoring any premium given during the creation of
        /// the object. There are options to roll-forward the starting period of the student, and treat the student as new or old.
        /// </summary>
        public PremiumComputationResult ComputeResultWithGivenPremium(double premium, int? startingPeriod = null, bool isNewStudent = true)
        {
            var rollForwardPeriod = startingPeriod ?? _startingPeriod.GetValueOrDefault(0);
            var enrollmentStateTimeSeries = RepricingModel.RollForwardEnrollmentStates(rollForwardPeriod);
            var servicingCosts = RepricingModel.RollForwardServicingCosts(rollForwardPeriod, isNewStudent);

            PremiumCalculation.CalculateResultWithGivenPremium(enrollmentStateTimeSeries, servicingCosts, premium);

            var premiumComputationResult = new PremiumComputationResult
            {
                ScenarioId = ScenarioId,
                ScenarioName = ScenarioName,

                ServicingCosts = PremiumCalculation.ServicingCostsDataTable,
                PremiumCalculationCashFlows = PremiumCalculation.CalculatedCashFlows,
                EnrollmentStateTimeSeries = CreateTimeSeriesEntries(enrollmentStateTimeSeries),
                TotalCoverageAmount = PremiumCalculation.PremiumCalculationModelInput.IncomeCoverageAmount,
                CalculatedMonthlyPremium = premium
            };

            return premiumComputationResult;
        }

        /// <summary>
        /// This is used to roll-forward down any time-dependent projections used in forecasting, such as interest rate curves.
        /// </summary>
        public void SetNumberOfForecastedPeriodsAhead(int monthlyPeriodOfForecasting)
        {
            PremiumCalculation.NumberOfForecastedPeriodsAhead = monthlyPeriodOfForecasting;
        }

        /// <summary>
        /// Converts an enrollment state array list into a list of output-friendly time series entry objects.
        /// </summary>
        public static List<StudentEnrollmentStateTimeSeriesEntry> CreateTimeSeriesEntries(List<EnrollmentStateArray> enrollmentStateTimeSeries)
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
                });

            return listOfTimeSeriesEntries.ToList();
        }
    }
}
