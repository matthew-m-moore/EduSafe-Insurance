IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryCollegeName')
	BEGIN

		CREATE TABLE WebSiteInquiryCollegeName
		(
			Id int IDENTITY(1,1) 
			, CollegeName varchar(250) not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			CONSTRAINT PK_CollegeName PRIMARY KEY (Id)
		)
	
	END