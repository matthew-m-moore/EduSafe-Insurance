using EduSafe.Core.BusinessLogic.Scenarios;

namespace EduSafe.Core.Interfaces
{
    public interface IScenario
    {
        string ScenarioName { get; }

        bool IsNewStudent { get; set; }
        int? RollForwardPeriod { get; set; }

        PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine basePremiumComputationEngine);
    }
}
