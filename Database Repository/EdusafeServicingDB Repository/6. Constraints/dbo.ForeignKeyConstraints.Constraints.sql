-- for all the tables with an accountnumber reference to cust schema
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesAcademicHistory_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesAcademicHistory
		ADD CONSTRAINT FK_InsureesAcademicHistory_AccountNumber
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
	ALTER TABLE dbo.InsureesPaymentHistory
		ADD CONSTRAINT FK_InsureesPaymentHistory_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetails_AccountNumber
		FOREIGN KEY (AccountNumber) REFERENCES cust.InsureesAccountData(AccountNumber)
END


-- for InsureesPremiumCalculationDetails
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_CollegeDetailId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeDetailId
		FOREIGN KEY (CollegeDetailId) REFERENCES dbo.CollegeDetail(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_CollegeMajorId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		ADD CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeMajorId
		FOREIGN KEY (CollegeMajorId) REFERENCES dbo.CollegeMajor(Id)
END

-- for CollegeDetail
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