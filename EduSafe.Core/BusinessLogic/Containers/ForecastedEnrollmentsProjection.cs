using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Curves;

namespace EduSafe.Core.BusinessLogic.Containers.TimeSeries
{
    public class ForecastedEnrollmentsProjection
    {
        public Dictionary<string, DataCurve<double>> EnrollmentCountProjections { get; set; }
        public Dictionary<string, DataCurve<double>> PercentageFirstYearEnrolleeProjections { get; set; }

        public ForecastedEnrollmentsProjection()
        {
            EnrollmentCountProjections = new Dictionary<string, DataCurve<double>>();
            PercentageFirstYearEnrolleeProjections = new Dictionary<string, DataCurve<double>>();
        }

        private ForecastedEnrollmentsProjection(ForecastedEnrollmentsProjection forecastedEnrollmentsProjection) 
        {
            EnrollmentCountProjections = forecastedEnrollmentsProjection.EnrollmentCountProjections
                .ToDictionary(kvp => new string(kvp.Key.ToCharArray()), kvp => new DataCurve<double>(kvp.Value.ToList()));

            PercentageFirstYearEnrolleeProjections = forecastedEnrollmentsProjection.PercentageFirstYearEnrolleeProjections
                .ToDictionary(kvp => new string(kvp.Key.ToCharArray()), kvp => new DataCurve<double>(kvp.Value.ToList()));
        }

        public ForecastedEnrollmentsProjection Copy()
        {
            return new ForecastedEnrollmentsProjection(this);
        }
    }
}
