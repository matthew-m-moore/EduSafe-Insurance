using System;
using System.Collections.Generic;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios
{
    public class PremiumComputationScenariosEngine
    {
        public PremiumComputationEngine BasePremiumComputationEngine { get; }
        public Dictionary<string, PremiumComputationResult> ScenarioResultsDictionary { get; private set; }

        private List<IScenario> _scenariosList;

        public PremiumComputationScenariosEngine(PremiumComputationEngine basePremiumComputationEngine)
        {
            BasePremiumComputationEngine = basePremiumComputationEngine;

            ScenarioResultsDictionary = new Dictionary<string, PremiumComputationResult>();
            _scenariosList = new List<IScenario>();
        }

        public void AddScenarios(List<IScenario> scenarios)
        {
            _scenariosList.AddRange(scenarios);
        }

        public void AddScenario(IScenario scenario)
        {
            _scenariosList.Add(scenario);
        }

        public void RunScenarios()
        {
            foreach (var scenario in _scenariosList)
            {
                var scenarioName = scenario.ScenarioName;
                if (ScenarioResultsDictionary.ContainsKey(scenarioName))
                    throw new Exception(string.Format("ERROR: Cannot have mulitple scenarios with the same name, '{0}'", scenarioName));

                var rollForwardPeriod = scenario.RollForwardPeriod;
                var isNewStudent = scenario.IsNewStudent;

                var copyOfPremiumCompuationEngine = BasePremiumComputationEngine.Copy();
                var scenarioPremiumComputationEngine = scenario.ApplyScenarioLogic(copyOfPremiumCompuationEngine);
                var scenarioResults = scenarioPremiumComputationEngine.ComputePremiumResult(rollForwardPeriod, isNewStudent);

                ScenarioResultsDictionary.Add(scenarioName, scenarioResults);
            }
        }
    }
}
