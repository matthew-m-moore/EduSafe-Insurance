SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsNotificationHistoryEntry')
	BEGIN

		CREATE TABLE dbo.InstitutionsNotificationHistoryEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, InstitutionsAccountNumber bigint not null
			, NotificationTypeId int not null
			, NotificationDate datetime not null
			CONSTRAINT PK_InstitutionsNotificationHistoryEntry_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END