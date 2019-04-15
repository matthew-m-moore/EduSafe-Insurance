using EduSafe.Core.BusinessLogic.Scenarios;

namespace EduSafe.Core.Interfaces
{
    public interface IScenario
    {
        string ScenarioName { get; }

        bool AllowPremiumsToAdjust { get; set; }
        bool IsNewStudent { get; set; }
        int? RollForwardPeriod { get; set; }

        PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine basePremiumComputationEngine);
    }
}
