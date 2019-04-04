using System;
using System.Collections.Generic;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.CostsOrFees
{
    public class PeriodicCostOrFee : CostOrFee
    {
        public int FrequencyInMonths { get; }
        public StudentEnrollmentState BaseStateForAdjustment { get; private set; }

        public PeriodicCostOrFee(
            PaymentConvention paymentConvention,
            string description,
            double baseAmount)
            : base(description, baseAmount)
        {
            SetBaseStateForAdjustment(StudentEnrollmentState.Enrolled);
            FrequencyInMonths = (int)paymentConvention;

            if (FrequencyInMonths <= 0)
            {
                var exceptionTest = string.Format("ERROR: The following is not a supported payment convention " +
                    "for periodic costs or fees, '{0}'.", paymentConvention.ToString());
                throw new Exception(exceptionTest);
            }
        }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public override CostOrFee Copy()
        {
            var paymentConvention = (PaymentConvention)FrequencyInMonths;
            var periodicCostOrFee = new PeriodicCostOrFee(
                paymentConvention,
                Description,
                BaseAmount);

            periodicCostOrFee.SetStartingPeriod(StartingPeriodOfCostOrFee);
            return periodicCostOrFee;
        }

        public void SetBaseStateForAdjustment(StudentEnrollmentState enrollmentState)
        {
            BaseStateForAdjustment = enrollmentState;
        }

        public override double CalculateAmount(int monthlyPeriod, List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            var adjustedMonthlyPeriod = monthlyPeriod - StartingPeriodOfCostOrFee;
            var isCostOrFeeMonth = MathUtility.CheckDivisibilityOfIntegers(adjustedMonthlyPeriod, FrequencyInMonths);

            if (!isCostOrFeeMonth) return 0.0;

            // Note: The adjustment should be based on the starting proportion of the enrollee factor
            var priorPeriodStateArray = enrollmentStateTimeSeries[monthlyPeriod - 1];
            var startingEnrollmentAmount = priorPeriodStateArray.GetTotalState(BaseStateForAdjustment);
            var amount = startingEnrollmentAmount * BaseAmount;

            return amount;
        }
    }
}
