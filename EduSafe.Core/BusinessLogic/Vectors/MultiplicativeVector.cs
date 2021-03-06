﻿using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Curves;

namespace EduSafe.Core.BusinessLogic.Vectors
{
    public class MultiplicativeVector : Vector
    {
        public MultiplicativeVector(DataCurve<double> values) : base(values)
        { }

        public override Vector Copy()
        {
            var copyOfValues = new DataCurve<double>(_VectorValues.ToList());
            return new MultiplicativeVector(copyOfValues);
        }

        public override double ApplyVector(int index, double suppliedValue)
        {
            var adjustedValue = suppliedValue * _VectorValues[index];
            return adjustedValue;
        }

        public override List<double> ApplyVector(List<double> suppliedValues)
        {
            var adjustedValues = suppliedValues.Select((v, i) => v * _VectorValues[i]).ToList();
            return adjustedValues;
        }

        public override double UnapplyVector(int index, double adjustedValue)
        {
            var suppliedValue = adjustedValue / _VectorValues[index];
            return suppliedValue;
        }

        public override List<double> UnapplyVector(List<double> adjustedValues)
        {
            var suppliedValues = adjustedValues.Select((v, i) => v / _VectorValues[i]).ToList();
            return suppliedValues;
        }
    }
}
