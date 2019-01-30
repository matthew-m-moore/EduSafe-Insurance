using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.Core.Tests.BusinessLogic.Models.Premiums;
using EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.Tests.BusinessLogic.Models
{
    [TestClass]
    public class RollForwardRepricingModelTests
    {
        private bool _outputExcel = true;
        private double _precision = 1e-8;

        [TestMethod, Owner("Matthew Moore")]
        public void RollforwardRepricingModel_ProofOfConcept()
        {
            var analyticalOuput = true;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(12);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(12);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();
            var premium = PremiumCalculationTests.CalculatePremiumAnalytically(
                premiumCalculationModelInput, 
                rollForwardServicingCosts, 
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation, 
                out DataTable servicingCosts);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries, 
                    premiumCalculation, 
                    servicingCosts, 
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }
        }
    }
}
