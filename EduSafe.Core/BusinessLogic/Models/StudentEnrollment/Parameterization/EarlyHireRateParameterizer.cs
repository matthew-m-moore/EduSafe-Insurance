using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment.Parameterization
{
    public class EarlyHireRateParameterizer : PostGraduationParameterizer
    {
        public EarlyHireRateParameterizer(
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            StudentEnrollmentModelInput studentEnrollmentModelInput,
            MultiplicativeVector flatMultiplicativeVector)
            : base(
                  enrollmentStateTimeSeries, 
                  studentEnrollmentModelInput, 
                  flatMultiplicativeVector, 
                  StudentEnrollmentState.EarlyHire)
        {
            _StartingEnrollmentState = StudentEnrollmentState.Enrolled;
            _StartingMonthlyPeriod = studentEnrollmentModelInput.EarlyHireStartingMonth;
        }

        protected override void PopulateStateArray(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray currentPeriodStateArray,
            EnrollmentStateArray priorPeriodStateArray,
            int monthlyPeriod)
        {
            CalculateEndingStateAmount(transitionRatesArray, currentPeriodStateArray, priorPeriodStateArray, monthlyPeriod);
            ReadjustEmploymentAmount(currentPeriodStateArray, priorPeriodStateArray, monthlyPeriod);
        }

        protected override double GetEndingStateAmount(EnrollmentStateArray currentPeriodStateArray)
        {
            return currentPeriodStateArray.GetTotalState(_StartingEnrollmentState);
        }

        private void ReadjustEmploymentAmount(
            EnrollmentStateArray currentPeriodStateArray,
            EnrollmentStateArray priorPeriodStateArray,
            int monthlyPeriod)
        {
            if (currentPeriodStateArray[StudentEnrollmentState.GraduatedEmployed] > 0.0)
            {
                var lastStateArrayWithGraduates = EnrollmentStateTimeSeries
                    .LastOrDefault(t => t[StudentEnrollmentState.Graduated] > 0.0 && t.MonthlyPeriod < monthlyPeriod);

                var lastPeriodWithGraduates = (lastStateArrayWithGraduates != null)
                    ? lastStateArrayWithGraduates.MonthlyPeriod
                    : 0;

                var totalEarlyHireAmountAdjustment = EnrollmentStateTimeSeries
                    .Where(t => t.MonthlyPeriod > lastPeriodWithGraduates && t.MonthlyPeriod <= monthlyPeriod)
                    .Sum(e => e[StudentEnrollmentState.EarlyHire]);

                var currentPeriodEmployedGraduates = currentPeriodStateArray[StudentEnrollmentState.GraduatedEmployed];
                                
                if (totalEarlyHireAmountAdjustment > currentPeriodEmployedGraduates)
                {
                    // This protects against violating the princple that you cannot have more early hires 
                    // than available employed graduates
                    EnrollmentStateTimeSeries.Last().SetTotalState(StudentEnrollmentState.EarlyHire, double.NaN);
                }
                else
                {
                    var adjustedEmployedGraduates = currentPeriodEmployedGraduates - totalEarlyHireAmountAdjustment;
                    currentPeriodStateArray[StudentEnrollmentState.GraduatedEmployed] = adjustedEmployedGraduates;
                }
            }

            var priorPeriodTotalAmount = priorPeriodStateArray.GetTotalState(StudentEnrollmentState.GraduatedEmployed);
            currentPeriodStateArray.SetTotalState(StudentEnrollmentState.GraduatedEmployed, priorPeriodTotalAmount);
        }
    }
}
