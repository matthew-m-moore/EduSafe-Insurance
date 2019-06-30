IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsNotificationHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsNotificationHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsNotificationHistoryEntry
	@InstitutionsAccountNumber bigint
	, @NotificationTypeId int
	, @NotificationDate datetime

AS

INSERT INTO dbo.InstitutionsNotificationHistoryEntry
(
	CreatedOn
	, CreatedBy
	, InstitutionsAccountNumber
	, NotificationTypeId
	, NotificationDate
)
VALUES
(
	GETDATE()
	, USER
	, @InstitutionsAccountNumber 
	, @NotificationTypeId
	, @NotificationDate
)

SELECT Id = MAX(Id) FROM dbo.InstitutionsNotificationHistoryEntry