using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class InterestRateCurveConverter
    {
        public static InterestRateCurve ConvertInputsToInterestRateCurve(
            InterestRateCurveType interestRateCurveType,
            DateTime interestRateCurveDate,
            DataCurve<double> interestRateCurveValues)
        {
            var dayCountConvention = GetDayCountConventionFromInteresRateCurveType(interestRateCurveType);
            var tenorInMonths = GetTenorInMonthsFromInterestRateCurveType(interestRateCurveType);

            var interestRateCurve = new InterestRateCurve(
                interestRateCurveType,
                interestRateCurveDate,
                interestRateCurveValues,
                tenorInMonths,
                dayCountConvention);

            return interestRateCurve;
        }

        private static DayCountConvention GetDayCountConventionFromInteresRateCurveType(InterestRateCurveType interestRateCurveType)
        {
            throw new NotImplementedException();
        }

        private static int GetTenorInMonthsFromInterestRateCurveType(InterestRateCurveType interestRateCurveType)
        {
            throw new NotImplementedException();
        }
    }
}
