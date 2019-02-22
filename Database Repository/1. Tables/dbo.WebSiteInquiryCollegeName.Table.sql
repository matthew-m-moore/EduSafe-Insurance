IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryCollegeName')
	BEGIN

		CREATE TABLE WebSiteInquiryCollegeName
		(
			Id int IDENTITY(1,1) 
			, CollegeName varchar(250)
			, CreatedOn datetime
			, CreatedBy varchar(25)
			CONSTRAINT PK_CollegeName PRIMARY KEY (Id)
		)
	
	END