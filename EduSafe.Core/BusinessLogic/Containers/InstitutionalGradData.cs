using System;

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
            GradTargetYear1 = Math.Round(gradTargetYear1, 0);
            GradTargetYear2 = Math.Round(gradTargetYear2, 0);
            GradTargetYear3 = Math.Round(gradTargetYear3, 0);
            UnemploymentTarget = unemploymentTarget;
            CohortDefaultRate = Math.Round(cohortDefaultRate, 0);
        }
    }
}
