namespace EduSafe.Core.BusinessLogic.Investments
{
    public class ReinvestmentModelProfitLossResult
    {
        public int ScenarioId { get; set; }
        public double FinalCashFlow { get; set; }
        public double ProfitLossFrom3M { get; set; }
        public double ProfitLossFrom6M { get; set; }
        public double ProfitLossFrom1Y { get; set; }
        public double TotalProfitLoss { get; set; }
    }
}
