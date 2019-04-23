using System;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class ShockLogicConvertor
    {
        private const string _mulitplicative = "multiplicative";
        private const string _additive = "additive";

        public static ShockLogic Convert(string shockLogicType, double shockValue)
        {
            if (string.IsNullOrWhiteSpace(shockLogicType))
                throw new Exception("ERROR: A shock logic type such as 'Mulitplicative' or 'Additive' must be provided.");

            switch (shockLogicType.ToLower())
            {
                case _mulitplicative:
                    return new MultiplicativeShock(shockValue);

                case _additive:
                    return new AdditiveShock(shockValue);

                default:
                    throw new Exception(string.Format("ERROR: Shock logic type of '{0}' is not supported, please check inputs.",
                        shockLogicType));
            }
        }
    }
}
