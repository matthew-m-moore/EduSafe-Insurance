using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
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

        public PremiumComputationEngine(
            PremiumCalculation premiumCalculation,
            RollForwardRepricingModel repricingModel,
            int? startingPeriod = null)
        {
            PremiumCalculation = premiumCalculation;
            RepricingModel = repricingModel;
            _startingPeriod = startingPeriod ?? _startingPeriod;
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

            return new PremiumComputationEngine(
                copyOfPremiumCalculation,
                copyOfRepricingModel,
                copyOfStartingPeriod);
        }

        public PremiumComputationResult ComputePremiumResult(int? startingPeriod = null, bool isNewStudent = true)
        {
            var rollForwardPeriod = startingPeriod ?? _startingPeriod.GetValueOrDefault(0);
            var enrollmentStateTimeSeries = RepricingModel.RollForwardEnrollmentStates(rollForwardPeriod);
            var servicingCosts = RepricingModel.RollForwardServicingCosts(rollForwardPeriod, isNewStudent);

            var premium = PremiumCalculation.CalculatePremium(enrollmentStateTimeSeries, servicingCosts);
            IsPremiumComputed = true;

            var premiumComputationResult = new PremiumComputationResult
            {
                ScenarioId = ScenarioId,
                ScenarioName = ScenarioName,

                PremiumCalculationModelInput = PremiumCalculation.PremiumCalculationModelInput,
                EnrollmentModelInput = RepricingModel.EnrollmentModel.StudentEnrollmentModelInput,
                ServicingCostsModel = RepricingModel.ServicingCostsModel,

                ServicingCosts = PremiumCalculation.ServicingCostsDataTable,
                PremiumCalculationCashFlows = PremiumCalculation.CalculatedCashFlows,
                EnrollmentStateTimeSeries = CreateTimeSeriesEntries(enrollmentStateTimeSeries),
                CalculatedMonthlyPremium = premium
            };

            return premiumComputationResult;
        }

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
