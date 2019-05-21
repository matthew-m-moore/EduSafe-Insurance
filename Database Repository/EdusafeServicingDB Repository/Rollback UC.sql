IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_InsureesAccountData_SSN' and type = 'UQ') 
BEGIN
	ALTER TABLE cust.InsureesAccountData
	DROP CONSTRAINT UC_InsureesAccountData_SSN
END
	
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_InsureesAccountData_EmailDROPress' and type = 'UQ') 
BEGIN
	ALTER TABLE cust.InsureesAccountData
	DROP CONSTRAINT UC_InsureesAccountData_EmailDROPress
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeMajor_CollegeMajor' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeMajor
	DROP CONSTRAINT UC_CollegeMajor_CollegeMajor
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeName_CollegeName' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeName
	DROP CONSTRAINT UC_CollegeName_CollegeName
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeNameToCollegeTypeMap_CollegeNameIdCollegeTypeId' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeNameToCollegeTypeMap
	DROP CONSTRAINT UC_CollegeNameToCollegeTypeMap_CollegeNameIdCollegeTypeId
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeType_CollegeType' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.CollegeType
	DROP CONSTRAINT UC_CollegeType_CollegeType
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_InsureesCollegeAndMajorData_AllFields' and type = 'UQ') 
BEGIN
	ALTER TABLE dbo.InsureesCollegeAndMajorData
	DROP CONSTRAINT UC_InsureesCollegeAndMajorData_AllFields
END