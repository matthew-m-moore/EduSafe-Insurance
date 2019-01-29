using System.Collections.Generic;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public class AnalyticalPremiumCalculation : NumericalPremiumCalculation
    {
        private double _analyticalFormulaNumerator;
        private double _analyticalFormulaDenominator;

        public AnalyticalPremiumCalculation(PremiumCalculationModelInput premiumCalculationModelInput)
            : base(premiumCalculationModelInput)
        { }

        public override double CalculatePremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel)
        {
            _analyticalFormulaNumerator = 0.0;
            _analyticalFormulaDenominator = 0.0;

            ServicingCostsDataTable = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            SetEnrollmentStateTimeSeries(enrollmentStateTimeSeries);
            CalculateCashFlows();

            var premium = (_analyticalFormulaDenominator > 0.0)
                ? _analyticalFormulaNumerator / _analyticalFormulaDenominator
                : double.NaN;

            SetCalculatedCashFlows();
            return premium;
        }

        protected override PremiumCalculationCashFlow CreateInitialCashFlow()
        {
            var initialPeriodCashFlow = base.CreateInitialCashFlow();
            var initialPeriodAnalyticalCashFlow = new AnalyticalPremiumCalculationCashFlow(initialPeriodCashFlow);

            return initialPeriodAnalyticalCashFlow;
        }

        protected override PremiumCalculationCashFlow CalculateCashFlow
            (EnrollmentStateArray enrollmentStateArray, double premiumAmountGuess, int monthlyPeriod)
        {
            var currentPeriodCashFlow  = base.CalculateCashFlow(enrollmentStateArray, premiumAmountGuess, monthlyPeriod);
            var analyticalPremiumCashFlow = new AnalyticalPremiumCalculationCashFlow(currentPeriodCashFlow);

            _analyticalFormulaNumerator += analyticalPremiumCashFlow.DiscountedTotalNumerator;
            _analyticalFormulaDenominator += analyticalPremiumCashFlow.DiscountedTotalDenominator;

            return analyticalPremiumCashFlow;
        }
    }
}
