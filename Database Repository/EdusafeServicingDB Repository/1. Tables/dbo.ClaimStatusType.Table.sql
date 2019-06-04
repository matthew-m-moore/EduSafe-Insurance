SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusType')
	BEGIN

		CREATE TABLE dbo.ClaimStatusType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, ClaimStatusType varchar(50) not null
			CONSTRAINT PK_ClaimStatusType PRIMARY KEY CLUSTERED (Id)
		)
	
	END