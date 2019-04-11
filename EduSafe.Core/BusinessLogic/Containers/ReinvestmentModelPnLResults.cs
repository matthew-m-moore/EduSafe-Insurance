using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class ReinvestmentModelPnLObject
    {
        public int ScenarioId { get; set; }
        public double FinalCashFlow { get; set; }
        public double ProfitFrom3M { get; set; }
        public double ProfitFrom6M { get; set; }
        public double ProfitFrom1Y { get; set; }
        public double TotalPnL { get; set; }
    }
}
