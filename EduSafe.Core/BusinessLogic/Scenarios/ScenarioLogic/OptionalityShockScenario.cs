using System;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic
{
    public class OptionalityShockScenario : IScenario
    {
        private readonly StudentEnrollmentState _enrollmentState;

        public string ScenarioName { get; set; }

        public bool AllowPremiumsToAdjust { get; }
        public bool IsNewStudent { get; }
        public int? RollForwardPeriod { get; }

        public ShockLogic ShockLogic { get; }

        public OptionalityShockScenario(
            StudentEnrollmentState enrollmentState,
            ShockLogic shockLogic,
            bool allowPremiumsToAdjust = false,
            bool isNewStudent = true,
            int? rollForwardPeriod = null)
        {
            ShockLogic = shockLogic;

            _enrollmentState = enrollmentState;

            AllowPremiumsToAdjust = allowPremiumsToAdjust;
            IsNewStudent = isNewStudent;
            RollForwardPeriod = rollForwardPeriod;
        }

        public PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine premiumComputationEngine)
        {
            double? baseValue; double shockedValue;
            switch (_enrollmentState)
            {    
                case StudentEnrollmentState.DroppedOut:
                    baseValue = premiumComputationEngine.PremiumCalculation.PremiumCalculationModelInput.DropOutOptionCoveragePercentage;
                    if (baseValue.HasValue)
                    {
                        shockedValue = ShockLogic.ApplyShockValue(baseValue.Value);
                        premiumComputationEngine.PremiumCalculation.PremiumCalculationModelInput.DropOutOptionCoveragePercentage = shockedValue;
                    }
                    break;

                case StudentEnrollmentState.EarlyHire:
                    baseValue = premiumComputationEngine.PremiumCalculation.PremiumCalculationModelInput.EarlyHireOptionCoveragePercentage;
                    if (baseValue.HasValue)
                    {
                        shockedValue = ShockLogic.ApplyShockValue(baseValue.Value);
                        premiumComputationEngine.PremiumCalculation.PremiumCalculationModelInput.EarlyHireOptionCoveragePercentage = shockedValue;
                    }
                    break;

                case StudentEnrollmentState.GraduateSchool:
                    baseValue = premiumComputationEngine.PremiumCalculation.PremiumCalculationModelInput.GradSchoolOptionCoveragePercentage;
                    if (baseValue.HasValue)
                    {
                        shockedValue = ShockLogic.ApplyShockValue(baseValue.Value);
                        premiumComputationEngine.PremiumCalculation.PremiumCalculationModelInput.GradSchoolOptionCoveragePercentage = shockedValue;
                    }
                    break;

                default:
                    throw new Exception(string.Format("ERROR: The event type '{0}' is not supported for shocking optionality.", _enrollmentState));
            }

            return premiumComputationEngine;
        }
    }
}
