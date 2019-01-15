using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.CostsOrFees;

namespace EduSafe.Core.BusinessLogic.Models

{
    public class ServicingCostsModel
    {
        public List<CostOrFee> CostsOrFees { get; }

        public ServicingCostsModel(List<CostOrFee> costsOrFees)
        {
            CostsOrFees = costsOrFees;
        }

        public List<double> CalculateServicingCosts()
        {
            throw new NotImplementedException();
        }
    }
}
