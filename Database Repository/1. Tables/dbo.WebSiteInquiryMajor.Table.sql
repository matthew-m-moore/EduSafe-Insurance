IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryMajor')
	BEGIN

		CREATE TABLE WebSiteInquiryMajor
		(
			ID int IDENTITY(1,1)
			, Major varchar(250)
			, CreatedOn datetime
			, CreatedBy varchar(25)
			CONSTRAINT PK_Major PRIMARY KEY (ID)
		)
	
	END