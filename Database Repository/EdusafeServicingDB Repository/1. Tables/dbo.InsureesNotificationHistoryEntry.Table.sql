SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesNotificationHistoryEntry')
	BEGIN

		CREATE TABLE dbo.InsureesNotificationHistoryEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, AccountNumber bigint not null
			, NotificationTypeId int null
			, NotificationDate datetime not null
			CONSTRAINT PK_InsureesNotificationHistoryEntry_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END