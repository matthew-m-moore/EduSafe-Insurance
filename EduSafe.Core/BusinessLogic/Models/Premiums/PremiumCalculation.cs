using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public abstract class PremiumCalculation
    {
        private ServicingCostsModel _servicingCostsModel { get; }
        private EnrollmentModel _studentEnrollmentModel { get; }
    }
}
