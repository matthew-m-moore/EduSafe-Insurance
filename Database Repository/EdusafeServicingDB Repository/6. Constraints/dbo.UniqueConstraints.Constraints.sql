IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_InsureesAccountData_SSN' and type = 'UQ') 
BEGIN
	ALTER TABLE cust.InsureesAccountData
	ADD CONSTRAINT UC_InsureesAccountData_SSN UNIQUE (SSN)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_ClaimStatusType_ClaimStatusType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.ClaimStatusType
	ADD CONSTRAINT UC_ClaimStatusType_ClaimStatusType UNIQUE (ClaimStatusType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeMajor_CollegeMajor' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeMajor
	ADD CONSTRAINT UC_CollegeMajor_CollegeMajor UNIQUE (CollegeMajor)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeDetail_AllFields' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeDetail
	ADD CONSTRAINT UC_CollegeDetail_AllFields UNIQUE (CollegeName, CollegeTypeId, CollegeAcademicTermTypeId)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeType_CollegeType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeType
	ADD CONSTRAINT UC_CollegeType_CollegeType UNIQUE (CollegeType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeAcademicTermType_CollegeAcademicTermType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeAcademicTermType
	ADD CONSTRAINT UC_CollegeAcademicTermType_CollegeAcademicTermType UNIQUE (CollegeAcademicTermType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_FileVerificationStatusType_FileVerificationStatusType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.FileVerificationStatusType
	ADD CONSTRAINT UC_FileVerificationStatusType_FileVerificationStatusType UNIQUE (FileVerificationStatusType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_OptionType_OptionType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.OptionType
	ADD CONSTRAINT UC_OptionType_OptionType UNIQUE (OptionType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_PaymentStatusType_PaymentStatusType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.PaymentStatusType
	ADD CONSTRAINT UC_PaymentStatusType_PaymentStatusType UNIQUE (PaymentStatusType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_NotificationType_NotificationType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.NotificationType
	ADD CONSTRAINT UC_NotificationType_NotificationType UNIQUE (NotificationType)
END

-- You can only have a particular email listed once per each EmailsSet
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'UC_Emails_EmailsSet' and type = 'UQ')
BEGIN
	ALTER TABLE cust.Emails
	ADD CONSTRAINT UC_Emails_EmailsSet UNIQUE (EmailsSetId, Email)
END

-- You can only designated one email as primary per each EmailsSet
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE NAME = 'UIDX_Emails_IsPrimary')
BEGIN
	CREATE UNIQUE INDEX UIDX_Emails_IsPrimary
	ON cust.Emails(EmailsSetId)
	WHERE IsPrimary = 1
END

