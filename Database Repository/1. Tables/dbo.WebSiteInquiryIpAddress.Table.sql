IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryIpAddress')
	BEGIN

		CREATE TABLE WebSiteInquiryIpAddress
		(
			Id int IDENTITY(1,1)
			, IpAddress varchar(250) not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			CONSTRAINT PK_IpAddress PRIMARY KEY (Id)
		)
	
	END