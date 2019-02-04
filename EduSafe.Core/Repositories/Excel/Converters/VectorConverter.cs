using System;
using EduSafe.Common.Curves;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class VectorConverter
    {
        private const string _additive = "Additive";
        private const string _multiplicative = "Multiplicative";

        public static Vector ConvertStringAndValuesToVector(string vectorType, DataCurve<double> vectorValues)
        {
            if (vectorType == null) throw new Exception();

            switch (vectorType)
            {
                case _additive:
                    return new AdditiveVector(vectorValues);

                case _multiplicative:
                    return new MultiplicativeVector(vectorValues);

                default:
                    var exceptionText = string.Format("ERROR: The vector type '{0}' is not supported",
                        vectorType);
                    throw new NotSupportedException(exceptionText);
            }
        }
    }
}
