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
            double baseAmount,
            int? endingPeriod = null)
            : base (description, baseAmount, endingPeriod)
        {
            EventStateForCostOrFee = eventStateForCostOrFee;
        }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public override CostOrFee Copy()
        {
            var eventBasedCostOrFee = new EventBasedCostOrFee(
                EventStateForCostOrFee,
                Description,
                BaseAmount);

            eventBasedCostOrFee.SetStartingPeriod(StartingPeriodOfCostOrFee);
            eventBasedCostOrFee.SetEndingPeriod(EndingPeriodOfCostOrFee);

            return eventBasedCostOrFee;
        }

        public override double CalculateAmount(int monthlyPeriod, List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            if (monthlyPeriod < StartingPeriodOfCostOrFee) return 0.0;
            if (EndingPeriodOfCostOrFee.HasValue && monthlyPeriod > EndingPeriodOfCostOrFee.Value) return 0.0;

            var currentPeriodStateArray = enrollmentStateTimeSeries[monthlyPeriod];
            var eventStateDeltaAmount = currentPeriodStateArray[EventStateForCostOrFee];

            if (eventStateDeltaAmount <= 0.0) return 0.0;

            var amount = eventStateDeltaAmount * BaseAmount;
            return amount;
        }
    }
}
