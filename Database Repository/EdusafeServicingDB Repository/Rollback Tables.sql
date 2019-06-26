IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimAccountEntry') BEGIN DROP TABLE dbo.ClaimAccountEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimDocumentEntry') BEGIN DROP TABLE dbo.ClaimDocumentEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimOptionEntry') BEGIN DROP TABLE dbo.ClaimOptionEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimPaymentEntry') BEGIN DROP TABLE dbo.ClaimPaymentEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusEntry') BEGIN DROP TABLE dbo.ClaimStatusEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusType') BEGIN DROP TABLE dbo.ClaimStatusType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeAcademicTermType') BEGIN DROP TABLE dbo.CollegeAcademicTermType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeDetail') BEGIN DROP TABLE dbo.CollegeDetail END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeMajor') BEGIN DROP TABLE dbo.CollegeMajor END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeType') BEGIN DROP TABLE dbo.CollegeType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FileVerificationStatusType') BEGIN DROP TABLE dbo.FileVerificationStatusType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsAccountData') BEGIN DROP TABLE dbo.InstitutionsAccountData END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsInsureeList') BEGIN DROP TABLE dbo.InstitutionsInsureeList END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsNextPaymentAndBalanceInformation') BEGIN DROP TABLE dbo.InstitutionsNextPaymentAndBalanceInformation END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsNotificationHistoryEntry') BEGIN DROP TABLE dbo.InstitutionsNotificationHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsPaymentHistoryEntry') BEGIN DROP TABLE dbo.InstitutionsPaymentHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesAcademicHistory') BEGIN DROP TABLE dbo.InsureesAcademicHistory END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesEnrollmentVerificationDetails') BEGIN DROP TABLE dbo.InsureesEnrollmentVerificationDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesGraduationVerificationDetails') BEGIN DROP TABLE dbo.InsureesGraduationVerificationDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesMajorMinorDetails') BEGIN DROP TABLE dbo.InsureesMajorMinorDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesMajorMinorDetailsSet') BEGIN DROP TABLE dbo.InsureesMajorMinorDetailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesNextPaymentAndBalanceInformation') BEGIN DROP TABLE dbo.InsureesNextPaymentAndBalanceInformation END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesNotificationHistoryEntry') BEGIN DROP TABLE dbo.InsureesNotificationHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPaymentHistoryEntry') BEGIN DROP TABLE dbo.InsureesPaymentHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetails') BEGIN DROP TABLE dbo.InsureesPremiumCalculationDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetailsSet') BEGIN DROP TABLE dbo.InsureesPremiumCalculationDetailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationOptionDetails') BEGIN DROP TABLE dbo.InsureesPremiumCalculationOptionDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationOptionDetailsSet') BEGIN DROP TABLE dbo.InsureesPremiumCalculationOptionDetailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'NotificationType') BEGIN DROP TABLE dbo.NotificationType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OptionType') BEGIN DROP TABLE dbo.OptionType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PaymentStatusType') BEGIN DROP TABLE dbo.PaymentStatusType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Emails') BEGIN DROP TABLE cust.Emails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmailsSet') BEGIN DROP TABLE cust.EmailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesAccountData') BEGIN DROP TABLE cust.InsureesAccountData END


IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_DeleteEmails' ) BEGIN DROP PROCEDURE cust.SP_DeleteEmails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_UpdateEmails' ) BEGIN DROP PROCEDURE cust.SP_UpdateEmails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertEmails' ) BEGIN DROP PROCEDURE cust.SP_InsertEmails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertEmailsSet' ) BEGIN DROP PROCEDURE cust.SP_InsertEmailsSet END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesAccountData' ) BEGIN DROP PROCEDURE cust.SP_InsertInsureesAccountData END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertClaimAccountEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertClaimAccountEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertClaimDocumentEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertClaimDocumentEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertClaimOptionEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertClaimOptionEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertClaimPaymentEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertClaimPaymentEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertClaimStatusEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertClaimStatusEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertClaimStatusType' ) BEGIN DROP PROCEDURE dbo.SP_InsertClaimStatusType END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertCollegeAcademicTermType' ) BEGIN DROP PROCEDURE dbo.SP_InsertCollegeAcademicTermType END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertCollegeDetail' ) BEGIN DROP PROCEDURE dbo.SP_InsertCollegeDetail END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertCollegeMajor' ) BEGIN DROP PROCEDURE dbo.SP_InsertCollegeMajor END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertCollegeType' ) BEGIN DROP PROCEDURE dbo.SP_InsertCollegeType END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertFileVerificationStatusType' ) BEGIN DROP PROCEDURE dbo.SP_InsertFileVerificationStatusType END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInstitutionsAccountData' ) BEGIN DROP PROCEDURE dbo.SP_InsertInstitutionsAccountData END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInstitutionsInsureeList' ) BEGIN DROP PROCEDURE dbo.SP_InsertInstitutionsInsureeList END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInstitutionsNextPaymentAndBalanceInformation' ) BEGIN DROP PROCEDURE dbo.SP_InsertInstitutionsNextPaymentAndBalanceInformation END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInstitutionsNotificationHistoryEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertInstitutionsNotificationHistoryEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInstitutionsPaymentHistoryEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertInstitutionsPaymentHistoryEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesAcademicHistory' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesAcademicHistory END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesEnrollmentVerificationDetails' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesEnrollmentVerificationDetails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesGraduationVerificationDetails' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesGraduationVerificationDetails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesMajorMinorDetails' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesMajorMinorDetails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesMajorMinorDetailsSet' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesMajorMinorDetailsSet END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesNextPaymentAndBalanceInformation' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesNextPaymentAndBalanceInformation END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesNotificationHistoryEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesNotificationHistoryEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesPaymentHistoryEntry' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesPaymentHistoryEntry END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesPremiumCalculationDetails' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesPremiumCalculationDetails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesPremiumCalculationDetailsSet' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesPremiumCalculationDetailsSet END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesPremiumCalculationOptionDetails' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesPremiumCalculationOptionDetails END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertInsureesPremiumCalculationOptionDetailsSet' ) BEGIN DROP PROCEDURE dbo.SP_InsertInsureesPremiumCalculationOptionDetailsSet END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertNotificationType' ) BEGIN DROP PROCEDURE dbo.SP_InsertNotificationType END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertOptionType' ) BEGIN DROP PROCEDURE dbo.SP_InsertOptionType END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertPaymentStatusType' ) BEGIN DROP PROCEDURE dbo.SP_InsertPaymentStatusType END
