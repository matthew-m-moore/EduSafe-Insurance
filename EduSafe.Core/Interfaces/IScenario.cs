using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;

namespace EduSafe.Core.Interfaces
{
    public interface IScenario
    {
        string ScenarioName { get; set; }

        bool AllowPremiumsToAdjust { get; }
        bool IsNewStudent { get; }
        int? RollForwardPeriod { get; }

        ShockLogic ShockLogic { get; }

        PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine premiumComputationEngine);
    }
}
