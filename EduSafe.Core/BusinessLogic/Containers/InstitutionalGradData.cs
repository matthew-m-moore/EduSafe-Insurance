namespace EduSafe.Core.BusinessLogic.Containers
{
    public class InstitutionalGradData
    {
        public double LoanInterestRate { get; }
        public double AverageLoanDebt { get; set; }
        public double GradTargetYear1 { get; }
        public double GradTargetYear2 { get; }
        public double GradTargetYear3 { get; }
        public double UnemploymentTarget { get; }
        public double CohortDefaultRate { get; }

        public InstitutionalGradData(
            double loanInterestRate,
            double averageLoanDebt,
            double gradTargetYear1,
            double gradTargetYear2,
            double gradTargetYear3,
            double unemploymentTarget,
            double cohortDefaultRate)
        {
            LoanInterestRate = loanInterestRate;
            AverageLoanDebt = averageLoanDebt;
            GradTargetYear1 = gradTargetYear1;
            GradTargetYear2 = gradTargetYear2;
            GradTargetYear3 = gradTargetYear3;
            UnemploymentTarget = unemploymentTarget;
            CohortDefaultRate = cohortDefaultRate;
        }
    }
}
