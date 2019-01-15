using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Core.BusinessLogic.Models
{
    public abstract class PremiumCalculationModel
    {
        private ServicingCostsModel _servicingCostsModel { get; }
        private StudentEnrollmentModel _studentEnrollmentModel { get; }
    }
}
