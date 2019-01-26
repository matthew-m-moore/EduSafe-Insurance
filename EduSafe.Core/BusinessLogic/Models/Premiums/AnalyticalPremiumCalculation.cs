using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public class AnalyticalPremiumCalculation : PremiumCalculation
    {
        public AnalyticalPremiumCalculation(PremiumCalculationModelInput premiumCalculationModelInput)
            : base(premiumCalculationModelInput)
        { }

        public override double CalculatePremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel)
        {
            throw new NotImplementedException();
        }
    }
}
