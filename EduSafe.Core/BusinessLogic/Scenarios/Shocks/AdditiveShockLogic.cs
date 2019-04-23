using System;

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

        public override bool Equals(object obj)
        {
            if (obj is AdditiveShock additiveShock)
            {
                var shockValueDifference = Math.Abs(additiveShock.ShockValue - ShockValue);
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
