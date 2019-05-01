using System;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic
{
    public class EnrollmentModelTargetShockScenario : IScenario
    {
        private readonly int _monthlyPeriod;
        private readonly StudentEnrollmentState _enrollmentState;

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
            _monthlyPeriod = monthlyPeriod;

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

            if (_enrollmentState == StudentEnrollmentState.Graduated)
                AdjustComplementaryTotalEnrollmentTarget(StudentEnrollmentState.DroppedOut, premiumComputationEngine, baseValue, shockedValue);

            if (_enrollmentState == StudentEnrollmentState.DroppedOut)
                AdjustComplementaryTotalEnrollmentTarget(StudentEnrollmentState.Graduated, premiumComputationEngine, baseValue, shockedValue);

            return premiumComputationEngine;
        }

        private void AdjustComplementaryTotalEnrollmentTarget(
            StudentEnrollmentState complementaryEnrollmentState,
            PremiumComputationEngine premiumComputationEngine, 
            double baseValue, 
            double shockedValue)
        {
            var incrementalChange = baseValue - shockedValue;

            var unadjustedTarget = premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .EnrollmentTargetsArray[complementaryEnrollmentState];

            if (unadjustedTarget == null) return;

            var adjustedTargetValue = unadjustedTarget.TargetValue + incrementalChange;

            premiumComputationEngine
                .RepricingModel
                .EnrollmentModel
                .StudentEnrollmentModelInput
                .EnrollmentTargetsArray[complementaryEnrollmentState].SetTargetValue(adjustedTargetValue);
        }
    }
}
