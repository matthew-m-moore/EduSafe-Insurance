using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic
{
    public class ServicingCostsModelShockScenario : IScenario
    {
        public string ScenarioName { get; set; }

        public bool AllowPremiumsToAdjust { get; }
        public bool IsNewStudent { get; }
        public int? RollForwardPeriod { get; }

        public ShockLogic ShockLogic { get; }
        public HashSet<string> CostOrFeeNamesToShock { get; private set; }

        public ServicingCostsModelShockScenario(
            ShockLogic shockLogic,
            bool allowPremiumsToAdjust = false,
            bool isNewStudent = true,
            int? rollForwardPeriod = null)
        {
            ShockLogic = shockLogic;

            AllowPremiumsToAdjust = allowPremiumsToAdjust;
            IsNewStudent = isNewStudent;
            RollForwardPeriod = rollForwardPeriod;

            CostOrFeeNamesToShock = new HashSet<string>();
        }

        public void AddCostOrFeeName(string costOrFeeName)
        {
            CostOrFeeNamesToShock.Add(costOrFeeName);
        }

        public PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine premiumComputationEngine)
        {
            var servicingCostsModel = premiumComputationEngine
                .RepricingModel
                .ServicingCostsModel;

            if (servicingCostsModel == null) return premiumComputationEngine;

            var shockedCostsOrFees = CostOrFeeNamesToShock.Any()
                ? ShockSpecificCostsOrFees(servicingCostsModel)
                : ShockAllCostsOrFees(servicingCostsModel);

            // It may not be necessary to actually set this, since the underlying referenced object
            // is in fact being modified above, but you never can be too careful
            premiumComputationEngine
                .RepricingModel
                .ServicingCostsModel.SetCostsOrFees(shockedCostsOrFees);

            return premiumComputationEngine;
        }

        private List<CostOrFee> ShockSpecificCostsOrFees(ServicingCostsModel servicingCostsModel)
        {
            var shockedCostsOrFees = servicingCostsModel.CostsOrFees.Select(costOrFee =>
            {
                if (CostOrFeeNamesToShock.Contains(costOrFee.Description))
                {
                    var shockedAmount = ShockLogic.ApplyShockValue(costOrFee.BaseAmount);
                    costOrFee.SetBaseAmount(shockedAmount);
                }
                return costOrFee;
            }).ToList();
           
            return shockedCostsOrFees;
        }

        private List<CostOrFee> ShockAllCostsOrFees(ServicingCostsModel servicingCostsModel)
        {
            var shockedCostsOrFees = servicingCostsModel.CostsOrFees.Select(costOrFee =>
            {
                var shockedAmount = ShockLogic.ApplyShockValue(costOrFee.BaseAmount);
                costOrFee.SetBaseAmount(shockedAmount);
                return costOrFee;
            }).ToList();

            return shockedCostsOrFees;
        }
    }
}
