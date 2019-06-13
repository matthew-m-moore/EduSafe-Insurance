SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Emails')
	BEGIN

		CREATE TABLE cust.Emails
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, EmailsSetId int not null
			, Email varchar(50) not null
			CONSTRAINT PK_Emails_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END