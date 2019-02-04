using System;
using EduSafe.Common.Enums;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class PaymentConventionConverter
    {
        public static PaymentConvention ConvertNullableIntegerToPaymentConvention(int? nullableInteger)
        {
            if (!nullableInteger.HasValue)
                return default(PaymentConvention);

            var paymentConvention = (PaymentConvention) nullableInteger.Value;
            var isDefined = Enum.IsDefined(typeof(PaymentConvention), paymentConvention);

            if (!isDefined)
            {
                var exceptionText = string.Format("A value of {0} months is not a valid payment convention.",
                    nullableInteger.Value);
                throw new InvalidCastException(exceptionText);
            }

            return paymentConvention;
        }
    }
}
