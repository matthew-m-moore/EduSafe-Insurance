using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models

{
    public class ServicingCostsModel
    {
        public List<CostOrFee> CostsOrFees { get; }
        public int NumberOfMonthlyPeriodsToProject { get; }

        private const string _periodHeader = "Period";

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

            dataTableColumnHeaders.Insert(0, _periodHeader);
            dataTableColumnHeaders.ForEach(h => servicingCostsDataTable.Columns.Add(h, typeof(double)));

            for (var monthlyPeriod = 1; monthlyPeriod <= NumberOfMonthlyPeriodsToProject; monthlyPeriod++)
            {
                var dataTableRow = servicingCostsDataTable.NewRow();
                dataTableRow[_periodHeader] = monthlyPeriod;

                foreach (var costOrFee in CostsOrFees)
                {
                    var costOrFeeDescription = costOrFee.Description;
                    var amountOfCostOrFee = costOrFee.CalculateAmount(monthlyPeriod, enrollmentStateTimeSeries);

                    dataTableRow[costOrFeeDescription] = amountOfCostOrFee;
                }

                servicingCostsDataTable.Rows.Add(dataTableRow);
            }

            return servicingCostsDataTable;
        }
    }
}
