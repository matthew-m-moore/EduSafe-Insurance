namespace EduSafe.Core.BusinessLogic.Containers
{
    public class InstitutionalGradData
    {
        public double LoanInterestRate { get; }
        public double GradTargetYear1 { get; }
        public double GradTargetYear2 { get; }
        public double GradTargetYear3 { get; }
        public double UnemploymentTarget { get; }

        public double IncrementalTargetYear2 => GradTargetYear2 - GradTargetYear1;
        public double IncrementalTargetYear3 => GradTargetYear3 - GradTargetYear2;

        public InstitutionalGradData(
            double loanInterestRate,
            double gradTargetYear1,
            double gradTargetYear2,
            double gradTargetYear3,
            double unemploymentTarget)
        {
            LoanInterestRate = loanInterestRate;
            GradTargetYear1 = gradTargetYear1;
            GradTargetYear2 = gradTargetYear2;
            GradTargetYear3 = gradTargetYear3;
            UnemploymentTarget = unemploymentTarget;
        }
    }
}
