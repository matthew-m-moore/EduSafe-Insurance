IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryCollegeType')
	BEGIN

		CREATE TABLE WebSiteInquiryCollegeType
		(
			ID int IDENTITY(1,1)
			, CollegeType varchar(250)
			, CreatedOn datetime
			, CreatedBy varchar(25)
			CONSTRAINT PK_CollegeType PRIMARY KEY (ID)
		)
	
	END