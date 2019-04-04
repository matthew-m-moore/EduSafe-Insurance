using System.Collections.Generic;
using EduSafe.Common.Curves;

namespace EduSafe.Core.BusinessLogic.Vectors
{
    public abstract class Vector
    {
        protected DataCurve<double> _VectorValues { get; }

        public Vector (DataCurve<double> values)
        {
            _VectorValues = values;
        }

        public abstract Vector Copy();

        public double this[int index]
        {
            get { return _VectorValues[index]; }
        }

        public abstract double ApplyVector(int index, double suppliedValue);
        public abstract List<double> ApplyVector(List<double> suppliedValues);

        public abstract double UnapplyVector(int index, double suppliedValue);
        public abstract List<double> UnapplyVector(List<double> suppliedValues);
    }
}
