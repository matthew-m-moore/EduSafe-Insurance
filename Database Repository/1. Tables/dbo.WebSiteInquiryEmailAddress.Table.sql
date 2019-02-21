IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryEmailAddress')
	BEGIN

		CREATE TABLE WebSiteInquiryEmailAddress
		(
			ID int IDENTITY(1,1)
			, EmailAddress varchar(250) 
			, IPAddress varchar(250)
			, OptOut bit
			, CreatedOn datetime
			, CreatedBy varchar(25)
		)

	END




