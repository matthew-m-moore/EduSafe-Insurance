using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduSafe.Core.Repositories.Database;
using EduSafe.IO.Database;

namespace EduSafe.WebApi.Adapters
{
    public class ServicingDataAdapter
    {
        private ServicingDataTypesRepository _servicingDataTypesRepository;
        private IndividualServicingDataRepository _individualServicingDataRepository;
        private InstitutionServicingDataRepository _institutionServicingDataRepository;

        public ServicingDataAdapter()
        {
            var databaseContext = DatabaseContextRetriever.GetServicingDataContext();

            _servicingDataTypesRepository = new ServicingDataTypesRepository(databaseContext);
            _individualServicingDataRepository = new IndividualServicingDataRepository(databaseContext);
            _institutionServicingDataRepository = new InstitutionServicingDataRepository(databaseContext);
        }
    }
}