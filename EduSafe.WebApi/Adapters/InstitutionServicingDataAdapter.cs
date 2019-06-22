using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduSafe.Core.Repositories.Database;
using EduSafe.IO.Database;

namespace EduSafe.WebApi.Adapters
{
    public class InstitutionServicingDataAdapter
    {
        private readonly ServicingDataTypesRepository _servicingDataTypesRepository;
        private readonly InstitutionServicingDataRepository _institutionServicingDataRepository;
        private readonly IndividualServicingDataAdapter _individualServicingDataAdapter;

        public InstitutionServicingDataAdapter()
        {
            var databaseContext = DatabaseContextRetriever.GetServicingDataContext();

            _servicingDataTypesRepository = new ServicingDataTypesRepository(databaseContext);
            _institutionServicingDataRepository = new InstitutionServicingDataRepository(databaseContext);
            _individualServicingDataAdapter = new IndividualServicingDataAdapter();
        }
    }
}