-- for all the tables with an InstitutionsAccountNumber reference to cust schema
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsInsureeList_InstitutionsAccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsInsureeList
		ADD CONSTRAINT FK_InstitutionsInsureeList_InstitutionsAccountNumber
		FOREIGN KEY (InstitutionsAccountNumber) REFERENCES dbo.InstitutionsAccountData(InstitutionsAccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsNextPaymentAndBalanceInformation_InstitutionsAccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsNextPaymentAndBalanceInformation
		ADD CONSTRAINT FK_InstitutionsNextPaymentAndBalanceInformation_InstitutionsAccountNumber
		FOREIGN KEY (InstitutionsAccountNumber) REFERENCES dbo.InstitutionsAccountData(InstitutionsAccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsNotificationHistoryEntry_InstitutionsAccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsNotificationHistoryEntry
		ADD CONSTRAINT FK_InstitutionsNotificationHistoryEntry_InstitutionsAccountNumber
		FOREIGN KEY (InstitutionsAccountNumber) REFERENCES dbo.InstitutionsAccountData(InstitutionsAccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsPaymentHistoryEntry_InstitutionsAccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsPaymentHistoryEntry
		ADD CONSTRAINT FK_InstitutionsPaymentHistoryEntry_InstitutionsAccountNumber
		FOREIGN KEY (InstitutionsAccountNumber) REFERENCES dbo.InstitutionsAccountData(InstitutionsAccountNumber)
END

-- for all the tables with an accountnumber reference to cust schema
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationOptionDetailsSet_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationOptionDetailsSet
		ADD CONSTRAINT FK_InsureesPremiumCalculationOptionDetailsSet_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationOptionDetailsSet_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesMajorMinorDetailsSet
		ADD CONSTRAINT FK_InsureesPremiumCalculationOptionDetailsSet_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END


IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsInsureeList_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsInsureeList
		ADD CONSTRAINT FK_InstitutionsInsureeList_AccountNumber
		FOREIGN KEY (InsureeAccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesMajorMinorDetails_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesMajorMinorDetails
		ADD CONSTRAINT FK_InsureesMajorMinorDetails_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesAcademicHistory_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesAcademicHistory
		ADD CONSTRAINT FK_InsureesAcademicHistory_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesNotificationHistoryEntry_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesNotificationHistoryEntry
		ADD CONSTRAINT FK_InsureesNotificationHistoryEntry_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesNextPaymentAndBalanceInformation_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesNextPaymentAndBalanceInformation
		ADD CONSTRAINT FK_InsureesNextPaymentAndBalanceInformation_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPaymentHistory_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPaymentHistoryEntry
		ADD CONSTRAINT FK_InsureesPaymentHistory_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetailsSet_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetailsSet
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetailsSet_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimAccountEntry_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimAccountEntry
		ADD CONSTRAINT FK_ClaimAccountEntry_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesEnrollmentVerificationDetails_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesEnrollmentVerificationDetails
		ADD CONSTRAINT FK_InsureesEnrollmentVerificationDetails_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesGraduationVerificationDetails_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesGraduationVerificationDetails
		ADD CONSTRAINT FK_InsureesGraduationVerificationDetails_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

--CollegeDetail
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_CollegeDetail_CollegeTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.CollegeDetail
		ADD CONSTRAINT FK_CollegeDetail_CollegeTypeId
		FOREIGN KEY (CollegeTypeId) REFERENCES dbo.CollegeType(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_CollegeDetail_CollegeAcademicTermTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.CollegeDetail
		ADD CONSTRAINT FK_CollegeDetail_CollegeAcademicTermTypeId
		FOREIGN KEY (CollegeAcademicTermTypeId) REFERENCES dbo.CollegeAcademicTermType(Id)
END

--References to ClaimAccountEntry table for claim number
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimDocumentEntry_ClaimNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimDocumentEntry
		ADD CONSTRAINT FK_ClaimDocumentEntry_ClaimNumber
		FOREIGN KEY (ClaimNumber) REFERENCES dbo.ClaimAccountEntry(ClaimNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimOptionEntry_ClaimNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimOptionEntry
		ADD CONSTRAINT FK_ClaimOptionEntry_ClaimNumber
		FOREIGN KEY (ClaimNumber) REFERENCES dbo.ClaimAccountEntry(ClaimNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimPaymentEntry_ClaimNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimPaymentEntry
		ADD CONSTRAINT FK_ClaimPaymentEntry_ClaimNumber
		FOREIGN KEY (ClaimNumber) REFERENCES dbo.ClaimAccountEntry(ClaimNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimStatusEntry_ClaimNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimStatusEntry
		ADD CONSTRAINT FK_ClaimStatusEntry_ClaimNumber
		FOREIGN KEY (ClaimNumber) REFERENCES dbo.ClaimAccountEntry(ClaimNumber)
END

--ClaimDocumentEntry
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimDocumentEntry_FileVerificationStatusTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimDocumentEntry
		ADD CONSTRAINT FK_ClaimDocumentEntry_FileVerificationStatusTypeId
		FOREIGN KEY (FileVerificationStatusTypeId) REFERENCES dbo.FileVerificationStatusType(Id)
END

--ClaimOptionEntry
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimOptionEntry_ClaimOptionTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimOptionEntry
		ADD CONSTRAINT FK_ClaimOptionEntry_ClaimOptionTypeId
		FOREIGN KEY (ClaimOptionTypeId) REFERENCES dbo.OptionType(Id)
END

--ClaimPaymentEntry
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimPaymentEntry_ClaimPaymentStatusTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimPaymentEntry
		ADD CONSTRAINT FK_ClaimPaymentEntry_ClaimPaymentStatusTypeId
		FOREIGN KEY (ClaimPaymentStatusTypeId) REFERENCES dbo.PaymentStatusType(Id)
END

--ClaimStatusEntry
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_ClaimStatusEntry_ClaimStatusTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.ClaimStatusEntry
		ADD CONSTRAINT FK_ClaimStatusEntry_ClaimStatusTypeId
		FOREIGN KEY (ClaimStatusTypeId) REFERENCES dbo.ClaimStatusType(Id)
END

--InsureesPaymentHistory
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesNextPaymentAndBalanceInformation_PaymentStatusTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesNextPaymentAndBalanceInformation
		ADD CONSTRAINT FK_InsureesNextPaymentAndBalanceInformation_PaymentStatusTypeId
		FOREIGN KEY (NextPaymentStatusTypeId) REFERENCES dbo.PaymentStatusType(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsNextPaymentAndBalanceInformation_PaymentStatusTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsNextPaymentAndBalanceInformation
		ADD CONSTRAINT FK_InstitutionsNextPaymentAndBalanceInformation_PaymentStatusTypeId
		FOREIGN KEY (NextPaymentStatusTypeId) REFERENCES dbo.PaymentStatusType(Id)
END


--InsureesPremiumCalculationOptionDetails
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationOptionDetails_OptionTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationOptionDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationOptionDetails_OptionTypeId
		FOREIGN KEY (OptionTypeId) REFERENCES dbo.OptionType(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationOptionDetails_InsureesPremiumCalculationOptionDetailsSetId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationOptionDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationOptionDetails_InsureesPremiumCalculationOptionDetailsSetId
		FOREIGN KEY (InsureesPremiumCalculationOptionDetailsSetId) REFERENCES dbo.InsureesPremiumCalculationOptionDetailsSet(SetId)
END

--InsureesPremiumCalculationDetails
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_CollegeDetailId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeDetailId
		FOREIGN KEY (CollegeDetailId) REFERENCES dbo.CollegeDetail(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_InsureesMajorMinorDetailsSetId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetails_InsureesMajorMinorDetailsSetId
		FOREIGN KEY (InsureesMajorMinorDetailsSetId) REFERENCES dbo.InsureesMajorMinorDetailsSet(SetId)
END

--InsureesPremiumCalculationDetailsSet
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationOptionDetailsSetId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetailsSet
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationOptionDetailsSetId
		FOREIGN KEY (InsureesPremiumCalculationOptionDetailsSetId) REFERENCES dbo.InsureesPremiumCalculationOptionDetailsSet(SetId)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationDetailsId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetailsSet
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetailsSet_InsureesPremiumCalculationDetailsId
		FOREIGN KEY (InsureesPremiumCalculationDetailsId) REFERENCES dbo.InsureesPremiumCalculationDetails(Id)
END

--NotificationHistoryEntry
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesNotificationHistoryEntry_NotificationTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesNotificationHistoryEntry
		ADD CONSTRAINT FK_InsureesNotificationHistoryEntry_NotificationTypeId
		FOREIGN KEY (NotificationTypeId) REFERENCES dbo.NotificationType(Id)
END


IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsNotificationHistoryEntry_NotificationTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InstitutionsNotificationHistoryEntry
		ADD CONSTRAINT FK_InstitutionsNotificationHistoryEntry_NotificationTypeId
		FOREIGN KEY (NotificationTypeId) REFERENCES dbo.NotificationType(Id)
END

--InsureesAcademicHistory
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesAcademicHistory_CollegeMajorOrMinorId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesAcademicHistory
		ADD CONSTRAINT FK_InsureesAcademicHistory_CollegeMajorOrMinorId
		FOREIGN KEY (CollegeMajorOrMinorId) REFERENCES dbo.CollegeMajor(Id)
END

--InsureesMajorMinorDetails
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesMajorMinorDetails_CollegeMajorId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesMajorMinorDetails
		ADD CONSTRAINT FK_InsureesMajorMinorDetails_CollegeMajorId
		FOREIGN KEY (CollegeMajorId) REFERENCES dbo.CollegeMajor(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesMajorMinorDetails_InsureesMajorMinorDetailsSetId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesMajorMinorDetails
		ADD CONSTRAINT FK_InsureesMajorMinorDetails_InsureesMajorMinorDetailsSetId
		FOREIGN KEY (InsureesMajorMinorDetailsSetId) REFERENCES dbo.InsureesMajorMinorDetailsSet(SetId)
END


--InsureesAccountData
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'FK_InsureesAccountData_EmailsSetId' and Type = 'F') 
BEGIN 
	ALTER TABLE cust.InsureesAccountData 
		ADD CONSTRAINT FK_InsureesAccountData_EmailsSetId
		FOREIGN KEY (EmailsSetId) REFERENCES cust.EmailsSet(SetId)
END

--InstitutionsAccountData
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'FK_InstitutionsAccountData_EmailsSetId' and Type = 'F') 
BEGIN 
	ALTER TABLE dbo.InstitutionsAccountData 
		ADD CONSTRAINT FK_InstitutionsAccountData_EmailsSetId
		FOREIGN KEY (EmailsSetId) REFERENCES cust.EmailsSet(SetId)
END

--Emails
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'FK_Emails_EmailsSetId' and Type = 'F') 
BEGIN 
	ALTER TABLE cust.Emails 
		ADD CONSTRAINT FK_Emails_EmailsSetId
		FOREIGN KEY (EmailsSetId) REFERENCES cust.EmailsSet(SetId)
END