using System;
using System.Collections.Generic;
using System.Data;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public abstract class PremiumCalculation
    {
        public List<PremiumCalculationCashFlow> CalculatedCashFlows { get; protected set; }
        public DataTable ServicingCostsDataTable { get; protected set; }

        public PremiumCalculationModelInput PremiumCalculationModelInput { get; }

        public PremiumCalculation(PremiumCalculationModelInput premiumCalculationModelInput)
        {
            PremiumCalculationModelInput = premiumCalculationModelInput;
        }

        public abstract PremiumCalculation Copy();

        public abstract double CalculatePremium(
            List<EnrollmentStateArray> enrollmentStateTimeSeries, 
            ServicingCostsModel servicingCostsModel);

        protected double CalculateDiscountFactor(int monthlyPeriod)
        {
            var discountRateCurve = PremiumCalculationModelInput.DiscountRateCurve;
            if (discountRateCurve != null)
            {
                var dayCountConvention = discountRateCurve.DayCountConvention;
                var timePeriodInYears = (double) monthlyPeriod / Constants.MonthsInOneYear;
                var discountRate = discountRateCurve[monthlyPeriod];

                var accrualFactor = MathUtility.CalculateInterestAccrualFactor(
                    dayCountConvention,    
                    CompoundingConvention.Monthly,
                    timePeriodInYears,
                    discountRate);

                var discountFactor = 1.0 / (1.0 + accrualFactor);
                return discountFactor;
            }

            throw new Exception("ERROR: No discount rate curve was defined. A discount rate curve must be provided.");
        }
    }
}
