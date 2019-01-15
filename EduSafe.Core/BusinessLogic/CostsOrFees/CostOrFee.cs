using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.CostsOrFees
{
    public abstract class CostOrFee
    {
        public double Amount { get; protected set; }
    }
}
