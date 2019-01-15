using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.CostsOrFees
{
    public class EventBasedCostOrFee : CostOrFee
    {
        public StudentEnrollmentState EventStateForCostOrFee { get; }

        public EventBasedCostOrFee(StudentEnrollmentState eventStateForCostOrFee, double amount)
        {
            EventStateForCostOrFee = eventStateForCostOrFee;
            Amount = amount;
        }
    }
}
