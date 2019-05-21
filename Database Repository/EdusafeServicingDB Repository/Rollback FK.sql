-- for all the tables with an accountnumber reference to cust schema
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_CollegeNameToCollegeTypeMap_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesCollegeAndMajorData
		DROP CONSTRAINT FK_CollegeNameToCollegeTypeMap_AccountNumber
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesGradeHistory_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesGradeHistory
		DROP CONSTRAINT FK_InsureesGradeHistory_CollegeNameId
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPaymentHistory_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPaymentHistory
		DROP CONSTRAINT FK_InsureesPaymentHistory_AccountNumber
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_AccountNumber' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		DROP CONSTRAINT FK_InsureesPremiumCalculationDetails_AccountNumber
END

--- for CollegeNameToCollegeTypeMap
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_CollegeNameToCollegeTypeMap_CollegeNameId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.CollegeNameToCollegeTypeMap
		DROP CONSTRAINT FK_CollegeNameToCollegeTypeMap_CollegeNameId
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_CollegeNameToCollegeTypeMap_CollegeTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.CollegeNameToCollegeTypeMap
		DROP CONSTRAINT FK_CollegeNameToCollegeTypeMap_CollegeTypeId
END

-- for InsureesCollegeAndMajorData
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesCollegeAndMajorData_CollegeNameId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesCollegeAndMajorData
		DROP CONSTRAINT FK_InsureesCollegeAndMajorData_CollegeNameId
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesCollegeAndMajorData_CollegeMajorId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesCollegeAndMajorData
		DROP CONSTRAINT FK_InsureesCollegeAndMajorData_CollegeMajorId
END

-- for InsureesPremiumCalculationDetails
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_CollegeNameId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		DROP CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeNameId
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_CollegeTypeId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		DROP CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeTypeId
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_InsureesPremiumCalculationDetails_CollegeMajorId' and type = 'F') 
BEGIN
	ALTER TABLE dbo.InsureesPremiumCalculationDetails
		DROP CONSTRAINT FK_InsureesPremiumCalculationDetails_CollegeMajorId
END

