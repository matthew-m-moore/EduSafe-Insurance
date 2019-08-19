IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryDegreeType')
	BEGIN

		CREATE TABLE WebSiteInquiryDegreeType
		(
			Id int IDENTITY(1,1)
			, DegreeType varchar(250) not null
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			CONSTRAINT PK_DegreeType PRIMARY KEY (Id)
		)
	
	END