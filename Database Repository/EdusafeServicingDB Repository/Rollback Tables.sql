USE EdusafeServicingDB 

IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPaymentHistory_AccountNumber') BEGIN ALTER TABLE InsureesPaymentHistoryEntry DROP CONSTRAINT FK_InsureesPaymentHistory_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationDetailsSet_AccountNumber') BEGIN ALTER TABLE InsureesPremiumCalculationDetailsSet DROP CONSTRAINT FK_InsureesPremiumCalculationDetailsSet_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimAccountEntry_AccountNumber') BEGIN ALTER TABLE ClaimAccountEntry DROP CONSTRAINT FK_ClaimAccountEntry_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesEnrollmentVerificationDetails_AccountNumber') BEGIN ALTER TABLE InsureesEnrollmentVerificationDetails DROP CONSTRAINT FK_InsureesEnrollmentVerificationDetails_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesGraduationVerificationDetails_AccountNumber') BEGIN ALTER TABLE InsureesGraduationVerificationDetails DROP CONSTRAINT FK_InsureesGraduationVerificationDetails_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_CollegeDetail_CollegeTypeId') BEGIN ALTER TABLE CollegeDetail DROP CONSTRAINT FK_CollegeDetail_CollegeTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_CollegeDetail_CollegeAcademicTermTypeId') BEGIN ALTER TABLE CollegeDetail DROP CONSTRAINT FK_CollegeDetail_CollegeAcademicTermTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimDocumentEntry_ClaimNumber') BEGIN ALTER TABLE ClaimDocumentEntry DROP CONSTRAINT FK_ClaimDocumentEntry_ClaimNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimOptionEntry_ClaimNumber') BEGIN ALTER TABLE ClaimOptionEntry DROP CONSTRAINT FK_ClaimOptionEntry_ClaimNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimPaymentEntry_ClaimNumber') BEGIN ALTER TABLE ClaimPaymentEntry DROP CONSTRAINT FK_ClaimPaymentEntry_ClaimNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimStatusEntry_ClaimNumber') BEGIN ALTER TABLE ClaimStatusEntry DROP CONSTRAINT FK_ClaimStatusEntry_ClaimNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimDocumentEntry_FileVerificationStatusTypeId') BEGIN ALTER TABLE ClaimDocumentEntry DROP CONSTRAINT FK_ClaimDocumentEntry_FileVerificationStatusTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimOptionEntry_ClaimOptionTypeId') BEGIN ALTER TABLE ClaimOptionEntry DROP CONSTRAINT FK_ClaimOptionEntry_ClaimOptionTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimPaymentEntry_ClaimPaymentStatusTypeId') BEGIN ALTER TABLE ClaimPaymentEntry DROP CONSTRAINT FK_ClaimPaymentEntry_ClaimPaymentStatusTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_ClaimStatusEntry_ClaimStatusTypeId') BEGIN ALTER TABLE ClaimStatusEntry DROP CONSTRAINT FK_ClaimStatusEntry_ClaimStatusTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesNextPaymentAndBalanceInformation_PaymentStatusTypeId') BEGIN ALTER TABLE InsureesNextPaymentAndBalanceInformation DROP CONSTRAINT FK_InsureesNextPaymentAndBalanceInformation_PaymentStatusTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsNextPaymentAndBalanceInformation_PaymentStatusTypeId') BEGIN ALTER TABLE InstitutionsNextPaymentAndBalanceInformation DROP CONSTRAINT FK_InstitutionsNextPaymentAndBalanceInformation_PaymentStatusTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationOptionDetails_OptionTypeId') BEGIN ALTER TABLE InsureesPremiumCalculationOptionDetails DROP CONSTRAINT FK_InsureesPremiumCalculationOptionDetails_OptionTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationOptionDetails_InsureesPremiumCalculationOptionDetailsSetId') BEGIN ALTER TABLE InsureesPremiumCalculationOptionDetails DROP CONSTRAINT FK_InsureesPremiumCalculationOptionDetails_InsureesPremiumCalculationOptionDetailsSetId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationDetails_CollegeDetailId') BEGIN ALTER TABLE InsureesPremiumCalculationDetails DROP CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeDetailId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationDetails_InsureesMajorMinorDetailsSetId') BEGIN ALTER TABLE InsureesPremiumCalculationDetails DROP CONSTRAINT FK_InsureesPremiumCalculationDetails_InsureesMajorMinorDetailsSetId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationOptionDetailsSetId') BEGIN ALTER TABLE InsureesPremiumCalculationDetailsSet DROP CONSTRAINT FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationOptionDetailsSetId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationDetailsId') BEGIN ALTER TABLE InsureesPremiumCalculationDetailsSet DROP CONSTRAINT FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationDetailsId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesNotificationHistoryEntry_NotificationTypeId') BEGIN ALTER TABLE InsureesNotificationHistoryEntry DROP CONSTRAINT FK_InsureesNotificationHistoryEntry_NotificationTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsNotificationHistoryEntry_NotificationTypeId') BEGIN ALTER TABLE InstitutionsNotificationHistoryEntry DROP CONSTRAINT FK_InstitutionsNotificationHistoryEntry_NotificationTypeId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesAcademicHistory_CollegeMajorOrMinorId') BEGIN ALTER TABLE InsureesAcademicHistory DROP CONSTRAINT FK_InsureesAcademicHistory_CollegeMajorOrMinorId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesMajorMinorDetails_CollegeMajorId') BEGIN ALTER TABLE InsureesMajorMinorDetails DROP CONSTRAINT FK_InsureesMajorMinorDetails_CollegeMajorId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesMajorMinorDetails_InsureesMajorMinorDetailsSetId') BEGIN ALTER TABLE InsureesMajorMinorDetails DROP CONSTRAINT FK_InsureesMajorMinorDetails_InsureesMajorMinorDetailsSetId END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsInsureeList_InstitutionsAccountNumber') BEGIN ALTER TABLE InstitutionsInsureeList DROP CONSTRAINT FK_InstitutionsInsureeList_InstitutionsAccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsNextPaymentAndBalanceInformation_InstitutionsAccountNumber') BEGIN ALTER TABLE InstitutionsNextPaymentAndBalanceInformation DROP CONSTRAINT FK_InstitutionsNextPaymentAndBalanceInformation_InstitutionsAccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsNotificationHistoryEntry_InstitutionsAccountNumber') BEGIN ALTER TABLE InstitutionsNotificationHistoryEntry DROP CONSTRAINT FK_InstitutionsNotificationHistoryEntry_InstitutionsAccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsPaymentHistoryEntry_InstitutionsAccountNumber') BEGIN ALTER TABLE InstitutionsPaymentHistoryEntry DROP CONSTRAINT FK_InstitutionsPaymentHistoryEntry_InstitutionsAccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesPremiumCalculationOptionDetailsSet_AccountNumber') BEGIN ALTER TABLE InsureesPremiumCalculationOptionDetailsSet DROP CONSTRAINT FK_InsureesPremiumCalculationOptionDetailsSet_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsInsureeList_AccountNumber') BEGIN ALTER TABLE InstitutionsInsureeList DROP CONSTRAINT FK_InstitutionsInsureeList_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesMajorMinorDetails_AccountNumber') BEGIN ALTER TABLE InsureesMajorMinorDetails DROP CONSTRAINT FK_InsureesMajorMinorDetails_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesAcademicHistory_AccountNumber') BEGIN ALTER TABLE InsureesAcademicHistory DROP CONSTRAINT FK_InsureesAcademicHistory_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InsureesNotificationHistoryEntry_AccountNumber') BEGIN ALTER TABLE InsureesNotificationHistoryEntry DROP CONSTRAINT FK_InsureesNotificationHistoryEntry_AccountNumber END
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'F' and Name = 'FK_InstitutionsAccountData_EmailsSetId') BEGIN ALTER TABLE cust.InsureesAccountData DROP CONSTRAINT FK_InstitutionsAccountData_EmailsSetId END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimDocumentEntry') BEGIN DROP TABLE dbo.ClaimDocumentEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimOptionEntry') BEGIN DROP TABLE dbo.ClaimOptionEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimPaymentEntry') BEGIN DROP TABLE dbo.ClaimPaymentEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusEntry') BEGIN DROP TABLE dbo.ClaimStatusEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusType') BEGIN DROP TABLE dbo.ClaimStatusType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FileVerificationStatusType') BEGIN DROP TABLE dbo.FileVerificationStatusType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsInsureeList') BEGIN DROP TABLE dbo.InstitutionsInsureeList END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsNextPaymentAndBalanceInformation') BEGIN DROP TABLE dbo.InstitutionsNextPaymentAndBalanceInformation END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsNotificationHistoryEntry') BEGIN DROP TABLE dbo.InstitutionsNotificationHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsPaymentHistoryEntry') BEGIN DROP TABLE dbo.InstitutionsPaymentHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesAcademicHistory') BEGIN DROP TABLE dbo.InsureesAcademicHistory END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesEnrollmentVerificationDetails') BEGIN DROP TABLE dbo.InsureesEnrollmentVerificationDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesGraduationVerificationDetails') BEGIN DROP TABLE dbo.InsureesGraduationVerificationDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesMajorMinorDetails') BEGIN DROP TABLE dbo.InsureesMajorMinorDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesNextPaymentAndBalanceInformation') BEGIN DROP TABLE dbo.InsureesNextPaymentAndBalanceInformation END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesNotificationHistoryEntry') BEGIN DROP TABLE dbo.InsureesNotificationHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPaymentHistoryEntry') BEGIN DROP TABLE dbo.InsureesPaymentHistoryEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetailsSet') BEGIN DROP TABLE dbo.InsureesPremiumCalculationDetailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationOptionDetails') BEGIN DROP TABLE dbo.InsureesPremiumCalculationOptionDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationOptionDetailsSet') BEGIN DROP TABLE dbo.InsureesPremiumCalculationOptionDetailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'NotificationType') BEGIN DROP TABLE dbo.NotificationType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OptionType') BEGIN DROP TABLE dbo.OptionType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PaymentStatusType') BEGIN DROP TABLE dbo.PaymentStatusType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimAccountEntry') BEGIN DROP TABLE dbo.ClaimAccountEntry END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeAcademicTermType') BEGIN DROP TABLE dbo.CollegeAcademicTermType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeType') BEGIN DROP TABLE dbo.CollegeType END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesMajorMinorDetailsSet') BEGIN DROP TABLE dbo.InsureesMajorMinorDetailsSet END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesAccountData') BEGIN DROP TABLE cust.InsureesAccountData END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeDetail') BEGIN DROP TABLE dbo.CollegeDetail END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeMajor') BEGIN DROP TABLE dbo.CollegeMajor END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsAccountData') BEGIN DROP TABLE dbo.InstitutionsAccountData END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetails') BEGIN DROP TABLE dbo.InsureesPremiumCalculationDetails END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmailsSet') BEGIN DROP TABLE dbo.EmailsSet END



-- DROP SPs
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
IF EXISTS (SELECT * FROM sys.objects WHERE Type = 'P' and Name = 'SP_InsertEmailsSet' ) BEGIN DROP PROCEDURE dbo.SP_InsertEmailsSet END
