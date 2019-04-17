using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic
{
    public class EnrollmentModelTargetShockScenario : IScenario
    {
        private int _monthlyPeriod;
        private StudentEnrollmentState _enrollmentState;

        public string ScenarioName { get; set; }

        public bool AllowPremiumsToAdjust { get; }
        public bool IsNewStudent { get; }
        public int? RollForwardPeriod { get; }

        public ShockLogic ShockLogic { get; }

        // Note that the -1 here refers to a total overall target for projected enrollment of the student
        public EnrollmentModelTargetShockScenario(
            StudentEnrollmentState enrollmentState,
            ShockLogic shockLogic,
            int monthlyPeriod = -1,
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
            var enrollmentTarget = premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .EnrollmentTargetsArray[_monthlyPeriod, _enrollmentState];

            if (enrollmentTarget == null) return premiumComputationEngine;

            var baseValue = enrollmentTarget.TargetValue;
            var shockedValue = ShockLogic.ApplyShockValue(baseValue);

            premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .EnrollmentTargetsArray[_monthlyPeriod, _enrollmentState].SetTargetValue(shockedValue);

            return premiumComputationEngine;
        }
    }
}
