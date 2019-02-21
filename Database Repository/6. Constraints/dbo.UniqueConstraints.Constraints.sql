	
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeName' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryCollegeName
	ADD CONSTRAINT UC_CollegeName UNIQUE (CollegeName)
END

	
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeType' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryCollegeType
	ADD CONSTRAINT UC_CollegeType UNIQUE (CollegeType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_EmailAddress' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryEmailAddress
	ADD CONSTRAINT UC_EmailAddress UNIQUE (EmailAddress)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_Major' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryMajor
	ADD CONSTRAINT UC_Major UNIQUE (Major)
END