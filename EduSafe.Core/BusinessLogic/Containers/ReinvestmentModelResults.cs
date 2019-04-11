using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class ReinvestmentModelResults
    {
        public int Period { get; set; }

        public double BeginningCashFlow { get; set; }
        public double EndingCashFlow { get; set; }

        public double PortionInCash { get; set; }
        public double PortionInOneMonth { get; set; }
        public double PortionInThreeMonth { get; set; }
        public double PortionInSixMonth { get; set; }
        public double PortionInOneYear { get; set; }

        public double InterestFromOneMonth { get; set; }
        public double InterestFromThreeMonth { get; set; }
        public double InterestFromSixMonth { get; set; }
        public double InterestFromOneYear { get; set; }

        public double CashFlowReturningFromThreeMonth { get; set; }
        public double CashFlowReturningFromSixMonth { get; set; }
        public double CashFlowReturningFromOneYear { get; set; }
    }
}
