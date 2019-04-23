namespace EduSafe.Core.BusinessLogic.Scenarios.Shocks
{
    public abstract class ShockLogic
    {
        protected const double _Precision = 1e-12;

        public double ShockValue { get; }

        public ShockLogic(double shockValue)
        {
            ShockValue = shockValue;
        }

        public abstract double ApplyShockValue(double baseValue);
    }
}
