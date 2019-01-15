using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;

namespace EduSafe.Core.BusinessLogic.Vectors
{
    public class MutiliplicativeVector : Vector
    {
        public MutiliplicativeVector(DataCurve<double> values) : base(values)
        { }

        public override List<double> ApplyVector(List<double> suppliedValues)
        {
            throw new NotImplementedException();
        }
    }
}
