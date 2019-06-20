using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Database.Entities.Servicing.Individuals;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.Core.Repositories.Database
{
    public class InstitutionServicingDataRepository : DatabaseRepository
    {
        private readonly DbContext _databaseContext;
        public override DbContext DatabaseContext => _databaseContext ?? DatabaseContextRetriever.GetServicingDataContext();

        public InstitutionServicingDataRepository(DbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public InstitutionsAccountDataEntity GetInstitutionCustomerData(long institutionAccountNumber)
        {
            InstitutionsAccountDataEntity institutionCustomerDataEntity;

            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                institutionCustomerDataEntity = servicingDataContext.InstitutionsAccountDataEntities
                    .SingleOrDefault(e => e.InstitutionsAccountNumber == institutionAccountNumber);
            }

            return institutionCustomerDataEntity;
        }

        public List<InsureesAccountDataEntity> GetAllIndividualCustomerDataForInstitution(long institutionAccountNumber)
        {
            var individualCustomerDataEntities = new List<InsureesAccountDataEntity>();

            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                var institutionCustomerDataEntity = servicingDataContext.InstitutionsAccountDataEntities
                    .SingleOrDefault(e => e.InstitutionsAccountNumber == institutionAccountNumber);

                if (institutionCustomerDataEntity != null)
                {
                    var individualAccountNumbers = servicingDataContext.InstitutionsInsureeListEntities
                        .Where(e => e.InstitutionsAccountNumber == institutionAccountNumber)
                        .Select(i => i.InsureeAccountNumber).ToList();

                    individualCustomerDataEntities = servicingDataContext.InsureesAccountDataEntities
                        .Where(e => individualAccountNumbers.Contains(e.AccountNumber)).ToList();
                }
            }

            return individualCustomerDataEntities;
        }
    }
}
