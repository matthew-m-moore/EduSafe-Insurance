using System.Collections.Generic;
using System.Data;
using EduSafe.Core.BusinessLogic.Models;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumComputationResult
    {
        public int? ScenarioId { get; set; }
        public string ScenarioName { get; set; }

        public PremiumCalculationModelInput PremiumCalculationModelInput { get; set; }
        public StudentEnrollmentModelInput EnrollmentModelInput { get; set; }
        public ServicingCostsModel ServicingCostsModel { get; set; }

        public DataTable ServicingCosts { get; set; }
        public List<PremiumCalculationCashFlow> PremiumCalculationCashFlows { get; set; }
        public List<StudentEnrollmentStateTimeSeriesEntry> EnrollmentStateTimeSeries { get; set; }       
        public double CalculatedMonthlyPremium { get; set; }
    }
}
