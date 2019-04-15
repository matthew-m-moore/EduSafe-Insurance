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
        public string ScenarioName => throw new NotImplementedException();

        public bool IsNewStudent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? RollForwardPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AllowPremiumsToAdjust { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PremiumComputationEngine ApplyScenarioLogic(PremiumComputationEngine basePremiumComputationEngine)
        {
            throw new NotImplementedException();
        }
    }
}
