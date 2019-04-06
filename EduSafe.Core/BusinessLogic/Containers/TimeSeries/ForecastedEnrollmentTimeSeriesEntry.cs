using System.Collections.Generic;
using System.Linq;

namespace EduSafe.Core.BusinessLogic.Containers.TimeSeries
{
    public class ForecastedEnrollmentTimeSeriesEntry : TimeSeriesEntry
    {
        public Dictionary<string, double> EnrollmentCountProjections { get; set; }
        public Dictionary<string, double> PercentageFirstTimeEnrolleeProjections { get; set; }

        public double TotalEnrollmentProjection => EnrollmentCountProjections.Values.Sum();        

        public ForecastedEnrollmentTimeSeriesEntry()
        {
            EnrollmentCountProjections = new Dictionary<string, double>();
            PercentageFirstTimeEnrolleeProjections = new Dictionary<string, double>();
        }

        private ForecastedEnrollmentTimeSeriesEntry(ForecastedEnrollmentTimeSeriesEntry forecastedEnrollmentTimeSeriesEntry) 
            : base(forecastedEnrollmentTimeSeriesEntry)
        {
            EnrollmentCountProjections = forecastedEnrollmentTimeSeriesEntry.EnrollmentCountProjections
                .ToDictionary(kvp => new string(kvp.Key.ToCharArray()), kvp => kvp.Value);

            PercentageFirstTimeEnrolleeProjections = forecastedEnrollmentTimeSeriesEntry.PercentageFirstTimeEnrolleeProjections
                .ToDictionary(kvp => new string(kvp.Key.ToCharArray()), kvp => kvp.Value);
        }

        public override TimeSeriesEntry Copy()
        {
            return new ForecastedEnrollmentTimeSeriesEntry(this);
        }

        public override void Scale(double scaleFactor)
        {
            foreach (var key in EnrollmentCountProjections.Keys)
                EnrollmentCountProjections[key] *= scaleFactor;

            foreach (var key in PercentageFirstTimeEnrolleeProjections.Keys)
                PercentageFirstTimeEnrolleeProjections[key] *= scaleFactor;
        }

        public override void Aggregate(TimeSeriesEntry timeSeriesEntry)
        {
            if (timeSeriesEntry is ForecastedEnrollmentTimeSeriesEntry forecastedEnrollmentTimeSeriesEntry)
            {
                foreach (var key in forecastedEnrollmentTimeSeriesEntry.EnrollmentCountProjections.Keys)
                {
                    if (EnrollmentCountProjections.ContainsKey(key))
                        EnrollmentCountProjections[key] += forecastedEnrollmentTimeSeriesEntry.EnrollmentCountProjections[key];
                    else
                        EnrollmentCountProjections.Add(key, forecastedEnrollmentTimeSeriesEntry.EnrollmentCountProjections[key]);
                }

                foreach (var key in forecastedEnrollmentTimeSeriesEntry.PercentageFirstTimeEnrolleeProjections.Keys)
                {
                    if (PercentageFirstTimeEnrolleeProjections.ContainsKey(key))
                        PercentageFirstTimeEnrolleeProjections[key] += forecastedEnrollmentTimeSeriesEntry.PercentageFirstTimeEnrolleeProjections[key];
                    else
                        PercentageFirstTimeEnrolleeProjections.Add(key, forecastedEnrollmentTimeSeriesEntry.PercentageFirstTimeEnrolleeProjections[key]);
                }
            }
        }
    }
}
