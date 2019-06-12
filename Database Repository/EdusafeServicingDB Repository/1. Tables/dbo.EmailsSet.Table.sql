SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmailsSet')
	BEGIN

		CREATE TABLE dbo.EmailsSet
		(
			SetId int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, Email varchar(50) not null
			CONSTRAINT PK_EmailsSet_Id PRIMARY KEY CLUSTERED (SetId)
		)
	
	END