using System.Collections.Generic;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.CostsOrFees
{
    public class EventBasedCostOrFee : CostOrFee
    {
        public StudentEnrollmentState EventStateForCostOrFee { get; }

        public EventBasedCostOrFee(
            StudentEnrollmentState eventStateForCostOrFee,
            string description,
            double baseAmount)
            : base (description, baseAmount)
        {
            EventStateForCostOrFee = eventStateForCostOrFee;
        }

        public override double CalculateAmount(int monthlyPeriod, List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            var currentPeriodStateArray = enrollmentStateTimeSeries[monthlyPeriod];
            var eventStateDeltaAmount = currentPeriodStateArray[EventStateForCostOrFee];

            if (eventStateDeltaAmount <= 0.0) return 0.0;

            var amount = eventStateDeltaAmount * BaseAmount;
            return amount;
        }
    }
}
