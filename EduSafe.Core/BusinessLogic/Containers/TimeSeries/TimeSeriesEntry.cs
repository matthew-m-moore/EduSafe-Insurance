namespace EduSafe.Core.BusinessLogic.Containers.TimeSeries
{
    public abstract class TimeSeriesEntry
    {
        public int Period { get; set; }

        public TimeSeriesEntry() { }

        protected TimeSeriesEntry(TimeSeriesEntry timeSeriesEntry)
        {
            Period = timeSeriesEntry.Period;
        }

        public abstract TimeSeriesEntry Copy();
        public abstract void Scale(double scaleFactor);
        public abstract void Aggregate(TimeSeriesEntry timeSeriesEntry);
    }
}
