using System;
using EduSafe.Common.Enums;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class InterestRateCurveTypeConverter
    {
        public static InterestRateCurveType ConvertStringToInterestRateCurveType(string interestRateCurveTypeText)
        {
            if (Enum.TryParse(interestRateCurveTypeText, out InterestRateCurveType interestRateCurveType))
                return interestRateCurveType;

            var exceptionText = string.Format("The interest rate curve type '{0}' is not yet supported.",
                interestRateCurveTypeText);
            throw new NotSupportedException(exceptionText);
        }
    }
}
