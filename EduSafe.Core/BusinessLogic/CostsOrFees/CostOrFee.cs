using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.CostsOrFees
{
    public abstract class CostOrFee
    {
        public string Description { get; }
        public double BaseAmount { get; private set; }
        public int StartingPeriodOfCostOrFee { get; private set; }
        
        public CostOrFee(string description, double baseAmount)
        {
            Description = description;
            SetBaseAmount(baseAmount);
            SetStartingPeriod();
        }

        public void SetBaseAmount(double baseAmount)
        {
            BaseAmount = baseAmount;
        }

        public void SetStartingPeriod(int startingPeriodOfCostOrFee = 1)
        {
            StartingPeriodOfCostOrFee = startingPeriodOfCostOrFee;
        }

        public abstract double CalculateAmount(int monthlyPeriod, List<EnrollmentStateArray> enrollmentStateTimeSeries);
    }
}
