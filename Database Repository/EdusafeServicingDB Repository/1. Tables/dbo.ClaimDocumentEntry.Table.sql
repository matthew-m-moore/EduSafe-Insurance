SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimDocumentEntry')
	BEGIN

		CREATE TABLE dbo.ClaimDocumentEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, ClaimNumber bigint not null  
			, FileName varchar(50) not null 
			, FileType varchar(25) not null 
			, FileVerificationStatusTypeId int not null
			, IsVerified bit not null
			, UploadDate datetime not null
			, ExpirationDate datetime null
			CONSTRAINT PK_ClaimDocumentEntry PRIMARY KEY CLUSTERED (Id)
		)
	
	END