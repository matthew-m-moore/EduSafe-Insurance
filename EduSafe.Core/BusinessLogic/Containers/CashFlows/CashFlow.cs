namespace EduSafe.Core.BusinessLogic.Containers.CashFlows
{
    public abstract class CashFlow
    {
        public int Period { get; set; }
        public double StudentCount { get; set; }

        public CashFlow() { StudentCount = 1.0; }

        protected CashFlow(CashFlow cashFlow)
        {
            Period = cashFlow.Period;
            StudentCount = cashFlow.StudentCount;
        }

        public abstract CashFlow Copy();
        public abstract void Scale(double scaleFactor);
        public abstract void Aggregate(CashFlow cashFlow);
    }
}
