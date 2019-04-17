namespace EduSafe.Core.BusinessLogic.Scenarios.Shocks
{
    public class MultiplicativeShock : ShockLogic
    {
        public MultiplicativeShock(double shockValue) : base(shockValue) { }

        public override double ApplyShockValue(double baseValue)
        {
            var shockedValue = baseValue * ShockValue;
            return shockedValue;
        }
    }
}
