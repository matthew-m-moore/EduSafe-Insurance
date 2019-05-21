IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_InsureesAccountData_SSN' and type = 'UQ') 
BEGIN
	ALTER TABLE cust.InsureesAccountData
	ADD CONSTRAINT UC_InsureesAccountData_SSN UNIQUE (SSN)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_InsureesAccountData_EmailAddress' and type = 'UQ') 
BEGIN
	ALTER TABLE cust.InsureesAccountData
	ADD CONSTRAINT UC_InsureesAccountData_EmailAddress UNIQUE (EmailAddress)
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