using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;

namespace EduSafe.Core.BusinessLogic.Vectors
{
    public abstract class Vector
    {
        public DataCurve<double> Values { get; }

        public Vector (DataCurve<double> values)
        {
            Values = values;
        }

        public abstract List<double> ApplyVector(List<double> suppliedValues);
    }
}
