using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.CostsOrFees
{
    public class PeriodicCostOrFee : CostOrFee
    {
        public int FrequencyInMonths { get; }
        
        public PeriodicCostOrFee(int periodicityInMonths, double amount)
        {
            FrequencyInMonths = periodicityInMonths;
            Amount = amount;
        }
    }
}
