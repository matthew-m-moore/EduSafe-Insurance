namespace EduSafe.Core.BusinessLogic.Scenarios.Shocks
{
    public abstract class ShockLogic
    {
        public double ShockValue { get; }

        public ShockLogic(double shockValue)
        {
            ShockValue = shockValue;
        }

        public abstract double ApplyShockValue(double baseValue);
    }
}
