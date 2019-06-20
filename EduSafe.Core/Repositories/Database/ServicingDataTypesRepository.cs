using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Common.ExtensionMethods;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;

namespace EduSafe.Core.Repositories.Database
{
    public class ServicingDataTypesRepository : DatabaseRepository
    {
        private readonly DbContext _databaseContext;
        public override DbContext DatabaseContext => _databaseContext ?? DatabaseContextRetriever.GetServicingDataContext();

        private Dictionary<int, string> _collegeMajorDictionary;

        private Dictionary<int, ClaimStatusType> _claimStatusTypeDictionary;
        private Dictionary<int, CollegeAcademicTermType> _collegeAcademicTermTypeDictionary;   
        private Dictionary<int, CollegeType> _collegeTypeDictionary;
        private Dictionary<int, FileVerificationStatusType> _fileVerificationStatusTypeDictionary;
        private Dictionary<int, NotificationType> _notificationTypeDictionary;
        private Dictionary<int, OptionType> _optionTypeDictionary;
        private Dictionary<int, PaymentStatusType> _paymentStatusTypeDictionary;

        public Dictionary<int, string> CollegeMajorDictionary => GetCollegeMajorDictionary();

        public Dictionary<int, ClaimStatusType> ClaimStatusTypeDictionary => GetClaimStatusTypeDictionary();
        public Dictionary<int, CollegeAcademicTermType> CollegeAcademicTermTypeDictionary => GetCollegeAcademicTermTypeDictionary();
        public Dictionary<int, CollegeType> CollegeTypeDictionary => GetCollegeTypeDictionary();
        public Dictionary<int, FileVerificationStatusType> FileVerificationStatusTypeDictionary => GetFileVerificationStatusTypeDictionary();
        public Dictionary<int, NotificationType> NotificationTypeDictionary => GetNotificationTypeDictionary();
        public Dictionary<int, OptionType> OptionTypeDictionary => GetOptionTypeDictionary();
        public Dictionary<int, PaymentStatusType> PaymentStatusTypeDictionary => GetPaymentStatusTypeDictionary();

        public ServicingDataTypesRepository(DbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        private Dictionary<int, string> GetCollegeMajorDictionary()
        {
            if (_collegeMajorDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var collegeMajors = servicingDataContext.CollegeMajorEntities.ToList();
                    _collegeMajorDictionary = collegeMajors.ToDictionary(
                        e => e.Id,
                        e => e.CollegeMajor);
                }
            }

            return _collegeMajorDictionary;
        }

        private Dictionary<int, ClaimStatusType> GetClaimStatusTypeDictionary()
        {
            if (_claimStatusTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var claimStatusTypes = servicingDataContext.ClaimStatusTypeEntities.ToList();
                    _claimStatusTypeDictionary = claimStatusTypes.ToDictionary(
                        e => e.Id,
                        e => e.ClaimStatusType.ConvertToEnum<ClaimStatusType>());
                }
            }

            return _claimStatusTypeDictionary;
        }


        private Dictionary<int, CollegeAcademicTermType> GetCollegeAcademicTermTypeDictionary()
        {
            if (_collegeAcademicTermTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var collegeAcademicTermTypes = servicingDataContext.CollegeAcademicTermTypeEntities.ToList();
                    _collegeAcademicTermTypeDictionary = collegeAcademicTermTypes.ToDictionary(
                        e => e.Id,
                        e => e.CollegeAcademicTermType.ConvertToEnum<CollegeAcademicTermType>());
                }
            }

            return _collegeAcademicTermTypeDictionary;
        }

        private Dictionary<int, CollegeType> GetCollegeTypeDictionary()
        {
            if (_collegeTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var collegeTypes = servicingDataContext.CollegeTypeEntities.ToList();
                    _collegeTypeDictionary = collegeTypes.ToDictionary(
                        e => e.Id,
                        e => e.CollegeType.ConvertToEnum<CollegeType>());
                }
            }

            return _collegeTypeDictionary;
        }

        private Dictionary<int, FileVerificationStatusType> GetFileVerificationStatusTypeDictionary()
        {
            if (_fileVerificationStatusTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var fileVerificationStatusTypes = servicingDataContext.FileVerificationStatusTypeEntities.ToList();
                    _fileVerificationStatusTypeDictionary = fileVerificationStatusTypes.ToDictionary(
                        e => e.Id,
                        e => e.FileVerificationStatusType.ConvertToEnum<FileVerificationStatusType>());
                }
            }

            return _fileVerificationStatusTypeDictionary;
        }

        private Dictionary<int, NotificationType> GetNotificationTypeDictionary()
        {
            if (_notificationTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var notificationTypes = servicingDataContext.NotificationTypeEntities.ToList();
                    _notificationTypeDictionary = notificationTypes.ToDictionary(
                        e => e.Id,
                        e => e.NotificationType.ConvertToEnum<NotificationType>());
                }
            }

            return _notificationTypeDictionary;
        }

        private Dictionary<int, OptionType> GetOptionTypeDictionary()
        {
            if (_optionTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var optionTypes = servicingDataContext.OptionTypeEntities.ToList();
                    _optionTypeDictionary = optionTypes.ToDictionary(
                        e => e.Id,
                        e => e.OptionType.ConvertToEnum<OptionType>());
                }
            }

            return _optionTypeDictionary;
        }

        private Dictionary<int, PaymentStatusType> GetPaymentStatusTypeDictionary()
        {
            if (_paymentStatusTypeDictionary == null)
            {
                using (var servicingDataContext = DatabaseContext as ServicingDataContext)
                {
                    var paymentStatusTypes = servicingDataContext.PaymentStatusTypeEntities.ToList();
                    _paymentStatusTypeDictionary = paymentStatusTypes.ToDictionary(
                        e => e.Id,
                        e => e.PaymentStatusType.ConvertToEnum<PaymentStatusType>());
                }
            }

            return _paymentStatusTypeDictionary;
        }
    }
}
