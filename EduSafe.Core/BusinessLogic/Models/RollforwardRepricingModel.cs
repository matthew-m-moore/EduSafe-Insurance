using System.Collections.Generic;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models
{
    public class RollForwardRepricingModel
    {
        public EnrollmentModel EnrollmentModel { get; }
        public ServicingCostsModel ServicingCostsModel { get; }

        private StudentEnrollmentState _baseEnrollmentState;

        public RollForwardRepricingModel(EnrollmentModel enrollmentModel, ServicingCostsModel servicingCostsModel, 
            StudentEnrollmentState baseEnrollmentState = StudentEnrollmentState.Enrolled)
        {
            EnrollmentModel = enrollmentModel;
            ServicingCostsModel = servicingCostsModel;
            _baseEnrollmentState = baseEnrollmentState;
        }

        /// <summary>
        /// Rolls forward the student enrollment model the given number of integer periods provided.
        /// Only affects the ouput of the enrollment model, does not affect the initial object.
        /// </summary>
        public List<EnrollmentStateArray> RollForwardEnrollmentStates(int numberOfPeriodsToRollForward)
        {
            if (!EnrollmentModel.IsParameterized) EnrollmentModel.ParameterizeModel();

            var enrollmentStateTimeSeries = EnrollmentModel.EnrollmentStateTimeSeries;
            if (numberOfPeriodsToRollForward == 0) return enrollmentStateTimeSeries;

            var baseEnrollmentStateArray = enrollmentStateTimeSeries[numberOfPeriodsToRollForward];
            var rollForwardEnrollmentStateTimeSeries = new List<EnrollmentStateArray>();

            foreach (var enrollmentStateArray in enrollmentStateTimeSeries)
            {
                var monthlyPeriod = enrollmentStateArray.MonthlyPeriod;
                if (monthlyPeriod < numberOfPeriodsToRollForward) continue;
                
                var rollForwardEnrollmentState = enrollmentStateArray.Copy();
                rollForwardEnrollmentState.RenormalizeTotalStateArray(baseEnrollmentStateArray, _baseEnrollmentState);
                rollForwardEnrollmentState.MonthlyPeriod -= numberOfPeriodsToRollForward;
                rollForwardEnrollmentStateTimeSeries.Add(rollForwardEnrollmentState);

                if (monthlyPeriod == numberOfPeriodsToRollForward)
                {
                    rollForwardEnrollmentState.ZeroOutIncrementalStateArray();
                }
                else
                {
                    var adjustedPriorPeriodIndex = monthlyPeriod - numberOfPeriodsToRollForward - 1;
                    var priorEnrollmentStateArray = rollForwardEnrollmentStateTimeSeries[adjustedPriorPeriodIndex];
                    rollForwardEnrollmentState.ResetIncrementalStateArray(priorEnrollmentStateArray);
                }
            }

            return rollForwardEnrollmentStateTimeSeries;
        }

        /// <summary>
        /// Rolls forward servicing costs model the desired integer number of periods without affecting the original model provided.
        /// Also accounts for the new starting payment period of rolled forward periodic costs or fees.
        /// </summary>
        public ServicingCostsModel RollForwardServicingCosts(int numberOfPeriodsToRollForward, bool isStudentNew = false)
        {
            if (numberOfPeriodsToRollForward == 0) return ServicingCostsModel;

            var listOfCostsOrFees = new List<CostOrFee>();
            foreach (var costOrFee in ServicingCostsModel.CostsOrFees)
            {
                var rollForwardCostOrFee = costOrFee.Copy();

                if (rollForwardCostOrFee is PeriodicCostOrFee castedRollForwardCostOrFee)
                {
                    var frequencyInMonths = castedRollForwardCostOrFee.FrequencyInMonths;
                    var isStartingPeriodUnchanged = MathUtility
                        .CheckDivisibilityOfIntegers(numberOfPeriodsToRollForward, frequencyInMonths);

                    if (!isStartingPeriodUnchanged && !isStudentNew)
                    {
                        var remainderPeriods = numberOfPeriodsToRollForward % frequencyInMonths;
                        var rollForwardStartingPeriod = frequencyInMonths - remainderPeriods + 1;
                        rollForwardCostOrFee.SetStartingPeriod(rollForwardStartingPeriod);
                    }
                }

                listOfCostsOrFees.Add(rollForwardCostOrFee);
            }

            var remainingPeriodsToProject = ServicingCostsModel.NumberOfMonthlyPeriodsToProject - numberOfPeriodsToRollForward;

            var rollForwardServicingCostsModel = new ServicingCostsModel(listOfCostsOrFees, remainingPeriodsToProject);
            return rollForwardServicingCostsModel;
        }
    }
}
