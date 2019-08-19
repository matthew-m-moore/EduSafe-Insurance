	
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

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_DegreeType' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryDegreeType
	ADD CONSTRAINT UC_DegreeType UNIQUE (DegreeType)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_Major' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryMajor
	ADD CONSTRAINT UC_Major UNIQUE (Major)
END