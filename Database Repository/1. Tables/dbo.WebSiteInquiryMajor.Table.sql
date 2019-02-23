IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryMajor')
	BEGIN

		CREATE TABLE WebSiteInquiryMajor
		(
			Id int IDENTITY(1,1)
			, Major varchar(250) not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			CONSTRAINT PK_Major PRIMARY KEY (Id)
		)
	
	END