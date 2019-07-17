using System;
using System.Data.Entity;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Common.ExtensionMethods;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.Core.Savers
{
    public class ClaimDocumentDatabaseSaver : DatabaseSaver
    {
        public override DbContext DatabaseContext => DatabaseContextRetriever.GetServicingDataContext();

        public void SaveClaimDocumentEntry(long claimNumber, string fileType, string fileName)
        {
            var currentDateTime = DateTime.Now;
            var expirationDateTime = currentDateTime.AddMonths(Constants.MonthsToDocumentExpiration);
            var typeId = (int) FileVerificationStatusType.Uploaded;

            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                var claimDocumentEntryEntity = new ClaimDocumentEntryEntity
                {
                    ClaimNumber = claimNumber,
                    FileName = fileName,
                    FileType = fileType,
                    FileVerificationStatusTypeId = typeId,
                    UploadDate = currentDateTime,
                    ExpirationDate = expirationDateTime,
                    IsVerified = false,
                };

                servicingDataContext.ClaimDocumentEntryEntities.Add(claimDocumentEntryEntity);
                servicingDataContext.SaveChanges();
            }
        }
    }
}
