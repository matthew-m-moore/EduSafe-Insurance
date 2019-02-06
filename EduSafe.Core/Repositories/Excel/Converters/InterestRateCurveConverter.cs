using System;
using System.Collections.Generic;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class InterestRateCurveConverter
    {
        private Dictionary<InterestRateCurveType, InterestRateTypeDetailRecord> _interestRateCurveTypeDetails;
        private Dictionary<string, DayCountConvention> _dayCountConventionDescriptions;

        public InterestRateCurveConverter(
            Dictionary<InterestRateCurveType, InterestRateTypeDetailRecord> interestRateCurveTypeDetails,
            Dictionary<string, DayCountConvention> dayCountConventionDescriptions)
        {
            _interestRateCurveTypeDetails = interestRateCurveTypeDetails;
            _dayCountConventionDescriptions = dayCountConventionDescriptions;
        }

        public InterestRateCurve ConvertInputsToInterestRateCurve(
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

        private DayCountConvention GetDayCountConventionFromInteresRateCurveType(InterestRateCurveType interestRateCurveType)
        {
            if (_interestRateCurveTypeDetails.ContainsKey(interestRateCurveType))
            {
                var dayCountDescription = _interestRateCurveTypeDetails[interestRateCurveType].DayCountConvention;

                if (_dayCountConventionDescriptions.ContainsKey(dayCountDescription))
                    return _dayCountConventionDescriptions[dayCountDescription];

                throw new KeyNotFoundException(string.Format("ERROR: '{0}' was not found in day count convention details data.",
                    dayCountDescription));
            }

            throw new KeyNotFoundException(string.Format("ERROR: '{0}' was not found in interest rate curve type details data.",
                interestRateCurveType));
        }

        private int? GetTenorInMonthsFromInterestRateCurveType(InterestRateCurveType interestRateCurveType)
        {
            if (_interestRateCurveTypeDetails.ContainsKey(interestRateCurveType))
                return _interestRateCurveTypeDetails[interestRateCurveType].TenorInMonths;

            throw new KeyNotFoundException(string.Format("ERROR: '{0}' was not found in interest rate curve type details data.",
                interestRateCurveType));
        }
    }
}
