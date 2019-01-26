namespace EduSafe.Core.BusinessLogic.Containers
{
    public class StudentEnrollmentStateTimeSeriesEntry
    {
        public int Period { get; set; }

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
    }
}
