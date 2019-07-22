using System;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class CostOrFeeConverter
    {
        public static CostOrFee ConvertToCostOrFee(CostOrFeeRecord costOrFeeRecord)
        {
            var costOrFeeName = costOrFeeRecord.CostOrFeeName;
            var baseAmount = costOrFeeRecord.Amount;
            var endingPeriod = costOrFeeRecord.EndingPeriod;

            var isPeriodicCostOrFee = costOrFeeRecord.FrequencyInMonths.HasValue;
            var isEventBasedCostOrFee = !string.IsNullOrWhiteSpace(costOrFeeRecord.DrivingEvent);

            if (isPeriodicCostOrFee && isEventBasedCostOrFee)
            {
                var exceptionText = string.Format("ERROR: A cost or fee cannot be both event-based and " +
                    "periodic. Please check the cost or fee named: '{0}'.", costOrFeeName);
                throw new Exception(exceptionText);
            }

            if (isPeriodicCostOrFee)
            {
                var paymentConvention = PaymentConventionConverter
                    .ConvertNullableIntegerToPaymentConvention(costOrFeeRecord.FrequencyInMonths);

                var periodicCostOrFee = new PeriodicCostOrFee(paymentConvention, costOrFeeName, baseAmount, endingPeriod);
                return periodicCostOrFee;
            }

            if (isEventBasedCostOrFee)
            {
                var eventStateForCostOrFee = StudentEnrollmentStateConverter
                    .ConvertStringToEnrollmentState(costOrFeeRecord.DrivingEvent);

                var eventBasedCostOrFee = new EventBasedCostOrFee(eventStateForCostOrFee, costOrFeeName, baseAmount, endingPeriod);
                return eventBasedCostOrFee;
            }

            var notSupportedText = string.Format("ERROR: The cost or fee named '{0}' is not supported.", costOrFeeName);
            throw new NotSupportedException(notSupportedText);
        }
    }
}
