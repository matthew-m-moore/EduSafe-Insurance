IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryEmailAddress')
	BEGIN

		CREATE TABLE WebSiteInquiryEmailAddress
		(
			Id int IDENTITY(1,1)
			, EmailAddress varchar(250) not null
			, IpAddress varchar(250) not null
			, ContactName varchar(250) null
			, OptOut bit null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
		)

	END




