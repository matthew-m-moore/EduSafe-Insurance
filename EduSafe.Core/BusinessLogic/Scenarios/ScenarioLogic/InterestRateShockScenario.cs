using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic
{
    public class InterestRateShockScenario : IScenario
    {
        public string ScenarioName { get; set; }

        public bool AllowPremiumsToAdjust { get; }
        public bool IsNewStudent { get; }
        public int? RollForwardPeriod { get; }

        public ShockLogic ShockLogic { get; }

        public InterestRateShockScenario(
            ShockLogic shockLogic,
            bool allowPremiumsToAdjust = false,
            bool isNewStudent = true,
            int? rollForwardPeriod = null)
        {
            ShockLogic = shockLogic;

            AllowPremiumsToAdjust = allowPremiumsToAdjust;
            IsNewStudent = isNewStudent;
            RollForwardPeriod = rollForwardPeriod;
        }

        public PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine premiumComputationEngine)
        {
            //var interestRateCurve = premiumComputationEngine
            //    .PremiumCalculation
            //    .PremiumCalculationModelInput
            //    .DiscountRateCurve;

            //if (interestRateCurve == null) return premiumComputationEngine;

            //var baseValue = interestRateCurve.;
            //var shockedValue = interestRateCurve.RateCurve.Select(r => ShockLogic.ApplyShockValue(r)).ToList();

            //premiumComputationEngine
            //    .RepricingModel
            //    .EnrollmentModel
            //    .StudentEnrollmentModelInput
            //    .EnrollmentTargetsArray[_monthlyPeriod, _enrollmentState].SetTargetValue(shockedValue);

            return premiumComputationEngine;
        }
    }
}
