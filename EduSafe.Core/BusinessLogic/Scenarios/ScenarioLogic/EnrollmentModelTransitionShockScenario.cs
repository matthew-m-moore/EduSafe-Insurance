using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic
{
    public class EnrollmentModelTransitionShockScenario : IScenario
    {
        private readonly int? _monthlyPeriod;
        private readonly StudentEnrollmentState _startingEnrollmentState;
        private readonly StudentEnrollmentState _endingEnrollmentState;

        public string ScenarioName { get; set; }

        public bool AllowPremiumsToAdjust { get; }
        public bool IsNewStudent { get; }
        public int? RollForwardPeriod { get; }

        public ShockLogic ShockLogic { get; }

        public EnrollmentModelTransitionShockScenario(
            StudentEnrollmentState startingEnrollmentState,
            StudentEnrollmentState endingEnrollmentState,
            ShockLogic shockLogic,
            int? monthlyPeriod = null,
            bool allowPremiumsToAdjust = false,
            bool isNewStudent = true,
            int? rollForwardPeriod = null)
        {
            ShockLogic = shockLogic;

            _startingEnrollmentState = startingEnrollmentState;
            _endingEnrollmentState = endingEnrollmentState;
            _monthlyPeriod = monthlyPeriod;

            AllowPremiumsToAdjust = allowPremiumsToAdjust;
            IsNewStudent = isNewStudent;
            RollForwardPeriod = rollForwardPeriod;
        }

        public PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine premiumComputationEngine)
        {
            if (_monthlyPeriod.HasValue)
                return ShockTransitionRate(premiumComputationEngine);
    
            return ShockTransitionRates(premiumComputationEngine);
        }

        private PremiumComputationEngine ShockTransitionRate(PremiumComputationEngine premiumComputationEngine)
        {
            var enrollmentTransition = premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .TransitionRatesArray[_startingEnrollmentState, _endingEnrollmentState, _monthlyPeriod.Value];

            if (enrollmentTransition == null) return premiumComputationEngine;

            var baseValue = enrollmentTransition.BaseTransitionRate;
            var shockedValue = ShockLogic.ApplyShockValue(baseValue);

            premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .TransitionRatesArray[_startingEnrollmentState, _endingEnrollmentState, _monthlyPeriod.Value]
                .SetBaseTransitionRate(shockedValue);

            return premiumComputationEngine;
        }

        private PremiumComputationEngine ShockTransitionRates(PremiumComputationEngine premiumComputationEngine)
        {
            var enrollmentTransitions = premiumComputationEngine
                            .RepricingModel
                            .EnrollmentModel
                            .StudentEnrollmentModelInput
                            .TransitionRatesArray[_startingEnrollmentState, _endingEnrollmentState];

            if (enrollmentTransitions == null) return premiumComputationEngine;

            var shockedTransitions = enrollmentTransitions.Select(enrollmentTransition =>
            {
                var baseValue = enrollmentTransition.BaseTransitionRate;
                var shockedValue = ShockLogic.ApplyShockValue(baseValue);
                enrollmentTransition.SetBaseTransitionRate(shockedValue);
                return enrollmentTransition;
            }).ToList();

            premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .TransitionRatesArray[_startingEnrollmentState, _endingEnrollmentState] = shockedTransitions;

            return premiumComputationEngine;
        }
    }
}
