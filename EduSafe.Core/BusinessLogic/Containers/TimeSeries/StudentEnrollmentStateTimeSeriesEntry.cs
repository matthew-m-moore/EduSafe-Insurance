namespace EduSafe.Core.BusinessLogic.Containers.TimeSeries
{
    public class StudentEnrollmentStateTimeSeriesEntry : TimeSeriesEntry
    {
        public double Enrolled { get; set; }
        public double DroppedOut { get; set; }
        public double Graduated { get; set; }
        public double Employed { get; set; }
        public double EalyHire { get; set; }
        public double Unemployed { get; set; }
        public double GradSchool { get; set; }

        public double DeltaEnrolled { get; set; }
        public double DeltaDroppedOut { get; set; }
        public double DeltaGraduated { get; set; }
        public double DeltaEmployed { get; set; }
        public double DeltaEalyHire { get; set; }
        public double DeltaUnemployed { get; set; }
        public double DeltaGradSchool { get; set; }

        public StudentEnrollmentStateTimeSeriesEntry() { }

        private StudentEnrollmentStateTimeSeriesEntry(StudentEnrollmentStateTimeSeriesEntry studentEnrollmentStateTimeSeriesEntry)
            : base(studentEnrollmentStateTimeSeriesEntry)
        {
            Enrolled = studentEnrollmentStateTimeSeriesEntry.Enrolled;
            DroppedOut = studentEnrollmentStateTimeSeriesEntry.DroppedOut;
            GradSchool = studentEnrollmentStateTimeSeriesEntry.Graduated;
            Employed = studentEnrollmentStateTimeSeriesEntry.Employed;
            EalyHire = studentEnrollmentStateTimeSeriesEntry.EalyHire;
            Unemployed = studentEnrollmentStateTimeSeriesEntry.Unemployed;
            Graduated = studentEnrollmentStateTimeSeriesEntry.Graduated;

            DeltaEnrolled = studentEnrollmentStateTimeSeriesEntry.DeltaEnrolled;
            DeltaDroppedOut = studentEnrollmentStateTimeSeriesEntry.DeltaDroppedOut;
            DeltaGraduated = studentEnrollmentStateTimeSeriesEntry.DeltaGraduated;
            DeltaEmployed = studentEnrollmentStateTimeSeriesEntry.DeltaEmployed;
            DeltaEalyHire = studentEnrollmentStateTimeSeriesEntry.DeltaEalyHire;
            DeltaUnemployed = studentEnrollmentStateTimeSeriesEntry.DeltaUnemployed;
            DeltaGradSchool = studentEnrollmentStateTimeSeriesEntry.DeltaGradSchool;
        }

        public override TimeSeriesEntry Copy()
        {
            return new StudentEnrollmentStateTimeSeriesEntry(this);
        }

        public override void Scale(double scaleFactor)
        {
            Enrolled *= scaleFactor;
            DroppedOut *= scaleFactor;
            GradSchool *= scaleFactor;
            Employed *= scaleFactor;
            EalyHire *= scaleFactor;
            Unemployed *= scaleFactor;
            Graduated *= scaleFactor;

            DeltaEnrolled *= scaleFactor;
            DeltaDroppedOut *= scaleFactor;
            DeltaGraduated *= scaleFactor;
            DeltaEmployed *= scaleFactor;
            DeltaEalyHire *= scaleFactor;
            DeltaUnemployed *= scaleFactor;
            DeltaGradSchool *= scaleFactor;
        }

        public override void Aggregate(TimeSeriesEntry timeSeriesEntry)
        {
            if (timeSeriesEntry is StudentEnrollmentStateTimeSeriesEntry studentEnrollmentStateTimeSeriesEntry)
            {
                Enrolled += studentEnrollmentStateTimeSeriesEntry.Enrolled;
                DroppedOut += studentEnrollmentStateTimeSeriesEntry.DroppedOut;
                GradSchool += studentEnrollmentStateTimeSeriesEntry.Graduated;
                Employed += studentEnrollmentStateTimeSeriesEntry.Employed;
                EalyHire += studentEnrollmentStateTimeSeriesEntry.EalyHire;
                Unemployed += studentEnrollmentStateTimeSeriesEntry.Unemployed;
                Graduated += studentEnrollmentStateTimeSeriesEntry.Graduated;

                DeltaEnrolled += studentEnrollmentStateTimeSeriesEntry.DeltaEnrolled;
                DeltaDroppedOut += studentEnrollmentStateTimeSeriesEntry.DeltaDroppedOut;
                DeltaGraduated += studentEnrollmentStateTimeSeriesEntry.DeltaGraduated;
                DeltaEmployed += studentEnrollmentStateTimeSeriesEntry.DeltaEmployed;
                DeltaEalyHire += studentEnrollmentStateTimeSeriesEntry.DeltaEalyHire;
                DeltaUnemployed += studentEnrollmentStateTimeSeriesEntry.DeltaUnemployed;
                DeltaGradSchool += studentEnrollmentStateTimeSeriesEntry.DeltaGradSchool;
            }
        }
    }
}
