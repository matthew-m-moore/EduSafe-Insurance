using System;

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

        public override bool Equals(object obj)
        {
            if (obj is MultiplicativeShock multiplicativeShock)
            {
                var shockValueDifference = Math.Abs(multiplicativeShock.ShockValue - ShockValue);
                return shockValueDifference < _Precision;
            }

            return false;
         }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
