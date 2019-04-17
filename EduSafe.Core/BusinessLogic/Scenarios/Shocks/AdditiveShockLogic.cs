namespace EduSafe.Core.BusinessLogic.Scenarios.Shocks
{
    public class AdditiveShock : ShockLogic
    {
        public AdditiveShock(double shockValue) : base(shockValue) { }

        public override double ApplyShockValue(double baseValue)
        {
            var shockedValue = baseValue + ShockValue;
            return shockedValue;
        }
    }
}
