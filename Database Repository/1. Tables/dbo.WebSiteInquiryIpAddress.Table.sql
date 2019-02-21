IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryIpAddress')
	BEGIN

		CREATE TABLE WebSiteInquiryIpAddress
		(
			ID int IDENTITY(1,1)
			, IpAddress varchar(250)
			, CreatedOn datetime
			, CreatedBy varchar(25)
			CONSTRAINT PK_IpAddress PRIMARY KEY (ID)
		)
	
	END