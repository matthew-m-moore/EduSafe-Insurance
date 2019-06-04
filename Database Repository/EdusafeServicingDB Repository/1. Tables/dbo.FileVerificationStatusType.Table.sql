SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FileVerificationStatusType')
	BEGIN

		CREATE TABLE dbo.FileVerificationStatusType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, FileVerificationStatusType varchar(25)
			CONSTRAINT PK_FileVerificationStatusType PRIMARY KEY CLUSTERED (Id)
		)
	
	END