using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models

{
    public class ServicingCostsModel
    {
        public List<CostOrFee> CostsOrFees { get; }
        public int NumberOfMonthlyPeriodsToProject { get; }

        public ServicingCostsModel(List<CostOrFee> costsOrFees, int numberOfMonthlyPeriodsToProject)
        {
            CostsOrFees = costsOrFees;
            NumberOfMonthlyPeriodsToProject = numberOfMonthlyPeriodsToProject;
        }

        /// <summary>
        /// Returns a data table of the servicing costs projected out to the specified number of monthly periods.
        /// </summary>
        public DataTable CalculateServicingCosts(List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            var servicingCostsDataTable = new DataTable();
            var dataTableColumnHeaders = CostsOrFees.Select(c => c.Description).ToList();

            dataTableColumnHeaders.Insert(0, Constants.PeriodIdentifier);
            dataTableColumnHeaders.Add(Constants.TotalIdentifier);
            dataTableColumnHeaders.ForEach(h => servicingCostsDataTable.Columns.Add(h, typeof(double)));

            for (var monthlyPeriod = 1; monthlyPeriod <= NumberOfMonthlyPeriodsToProject; monthlyPeriod++)
            {
                var dataTableRow = servicingCostsDataTable.NewRow();
                dataTableRow[Constants.PeriodIdentifier] = monthlyPeriod;

                var totalMontlyCostsOrFees = 0.0;
                foreach (var costOrFee in CostsOrFees)
                {
                    var costOrFeeDescription = costOrFee.Description;
                    var amountOfCostOrFee = costOrFee.CalculateAmount(monthlyPeriod, enrollmentStateTimeSeries);

                    dataTableRow[costOrFeeDescription] = amountOfCostOrFee;
                    totalMontlyCostsOrFees += amountOfCostOrFee;
                }

                dataTableRow[Constants.TotalIdentifier] = totalMontlyCostsOrFees;
                servicingCostsDataTable.Rows.Add(dataTableRow);
            }

            return servicingCostsDataTable;
        }
    }
}
