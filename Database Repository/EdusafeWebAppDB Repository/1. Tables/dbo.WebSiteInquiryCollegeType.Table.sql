IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryCollegeType')
	BEGIN

		CREATE TABLE WebSiteInquiryCollegeType
		(
			Id int IDENTITY(1,1)
			, CollegeType varchar(250) not null
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			CONSTRAINT PK_CollegeType PRIMARY KEY (Id)
		)
	
	END