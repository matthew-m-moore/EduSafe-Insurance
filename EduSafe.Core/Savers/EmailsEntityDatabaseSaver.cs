using System;
using System.Data.Entity;
using System.Linq;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.Core.Savers
{
    public class EmailsEntityDatabaseSaver : DatabaseSaver
    {
        public override DbContext DatabaseContext => DatabaseContextRetriever.GetServicingDataContext();

        public int SaveNewEmailAddress(int emailSetId, string emailAddress, bool isPrimary)
        {
            try
            {
                var emailEntityToAdd = new EmailsEntity
                {
                    EmailsSetId = emailSetId,
                    Email = emailAddress,
                    IsPrimary = isPrimary,
                };

                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    if (isPrimary)
                        UpdateEmailAddressToPrimary(servicingDataContext, emailSetId, emailAddress);

                    servicingDataContext.EmailsEntities.Add(emailEntityToAdd);
                    servicingDataContext.SaveChanges();
                }

                return emailEntityToAdd.Id;
            }
            catch
            {
                return 0;
            }
        }

        public bool DeleteEmailAddress(int emailSetId, string emailAddress)
        {
            try
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var emailEntityToRemove = servicingDataContext.EmailsEntities
                        .SingleOrDefault(e => e.EmailsSetId == emailSetId && e.Email == emailAddress);

                    if (emailEntityToRemove == null) return false;

                    servicingDataContext.EmailsEntities.Remove(emailEntityToRemove);
                    servicingDataContext.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEmailAddressToPrimary(int emailSetId, string emailAddress)
        {
            try
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    DatabaseContextRetriever.LogDatabaseActvityForDebug(servicingDataContext);
                    UpdateEmailAddressToPrimary(servicingDataContext, emailSetId, emailAddress);
                    servicingDataContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void UpdateEmailAddressToPrimary(ServicingDataContext servicingDataContext, int emailSetId, string emailAddress)
        {
            var emailEntities = servicingDataContext.EmailsEntities
                .Where(e => e.EmailsSetId == emailSetId).ToList();

            foreach (var emailEntity in emailEntities)
            {
                if (emailEntity.Email == emailAddress)
                    emailEntity.IsPrimary = true;
                else
                    emailEntity.IsPrimary = false;
            }
        }
    }
}
