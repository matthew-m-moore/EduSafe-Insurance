SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'NotificationType')
	BEGIN

		CREATE TABLE dbo.NotificationType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, NotificationType varchar(25) not null
			, Description varchar(250) null
			CONSTRAINT PK_NotificationType_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END