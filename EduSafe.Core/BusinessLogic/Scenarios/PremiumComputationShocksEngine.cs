using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios
{
    public class PremiumComputationShocksEngine
    {
        public PremiumComputationEngine BasePremiumComputationEngine { get; }
        public Dictionary<string, PremiumComputationResult> ScenarioResultsDictionary { get; private set; }

        private Dictionary<string, List<IScenario>> _shockScenariosDictionary;

        public PremiumComputationShocksEngine(PremiumComputationEngine basePremiumComputationEngine,
            Dictionary<string, List<IScenario>> shockScenariosDictionary = null)
        {
            BasePremiumComputationEngine = basePremiumComputationEngine;

            ScenarioResultsDictionary = new Dictionary<string, PremiumComputationResult>();
            _shockScenariosDictionary = shockScenariosDictionary ?? new Dictionary<string, List<IScenario>>();
        }

        public void AddShockScenarios(List<IScenario> scenarios, string scenarioName)
        {
            if (!_shockScenariosDictionary.ContainsKey(scenarioName))
                _shockScenariosDictionary.Add(scenarioName, new List<IScenario>());

            _shockScenariosDictionary[scenarioName].AddRange(scenarios);
        }

        public void AddShockScenario(IScenario scenario, string scenarioName = null)
        {
            scenarioName = scenarioName ?? scenario.ScenarioName;
            if (scenarioName == null) return;

            if (!_shockScenariosDictionary.ContainsKey(scenarioName))
                _shockScenariosDictionary.Add(scenarioName, new List<IScenario>());

            _shockScenariosDictionary[scenarioName].Add(scenario);
        }

        public void RunShockScenarios()
        {
            var basePremiumResults = BasePremiumComputationEngine.ComputePremiumResult();
            var basePremium = basePremiumResults.CalculatedMonthlyPremium;
            var baseScenarioName = BasePremiumComputationEngine.ScenarioName;

            ScenarioResultsDictionary.Add(baseScenarioName, basePremiumResults);

            foreach (var shockScenarioEntry in _shockScenariosDictionary)
            {
                if (!shockScenarioEntry.Value.Any()) continue;

                var shockScenarioName = shockScenarioEntry.Key;
                var fullyQualifiedScenarioName = baseScenarioName + " - " + shockScenarioName;
                var copyOfPremiumCompuationEngine = BasePremiumComputationEngine.Copy();
                
                PremiumComputationEngine shockScenarioComputationEngine;
                PremiumComputationResult shockScenarioResults;

                if (shockScenarioEntry.Value.Count > 1)
                {
                    shockScenarioComputationEngine = copyOfPremiumCompuationEngine;
                    foreach (var shockScenario in shockScenarioEntry.Value)
                        shockScenarioComputationEngine = shockScenario.ApplyScenarioLogic(shockScenarioComputationEngine);

                    var allowPremiumsToAdjust = shockScenarioEntry.Value.All(s => s.AllowPremiumsToAdjust);
                    var isNewStudent = shockScenarioEntry.Value.All(s => s.IsNewStudent);

                    int? rollForwardPeriod = null;
                    if (shockScenarioEntry.Value.Select(s => s.RollForwardPeriod).Distinct().Count() == 1)
                        rollForwardPeriod = shockScenarioEntry.Value.First().RollForwardPeriod;

                    shockScenarioResults = allowPremiumsToAdjust
                        ? shockScenarioComputationEngine.ComputePremiumResult(rollForwardPeriod, isNewStudent)
                        : shockScenarioComputationEngine.ComputeResultWithGivenPremium(basePremium, rollForwardPeriod, isNewStudent);
                }
                else
                {
                    var shockScenario = shockScenarioEntry.Value.Single();
                    var rollForwardPeriod = shockScenario.RollForwardPeriod;
                    var isNewStudent = shockScenario.IsNewStudent;                    

                    shockScenarioComputationEngine = shockScenario.ApplyScenarioLogic(copyOfPremiumCompuationEngine);
                    shockScenarioResults = shockScenario.AllowPremiumsToAdjust
                        ? shockScenarioComputationEngine.ComputePremiumResult(rollForwardPeriod, isNewStudent)
                        : shockScenarioComputationEngine.ComputeResultWithGivenPremium(basePremium, rollForwardPeriod, isNewStudent);
                }

                shockScenarioResults.ScenarioName = fullyQualifiedScenarioName;
                ScenarioResultsDictionary.Add(fullyQualifiedScenarioName, shockScenarioResults);
            }
        }
    }
}
