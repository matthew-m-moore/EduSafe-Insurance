IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesNotificationHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesNotificationHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInsureesNotificationHistoryEntry
	@AccountNumber bigint
	, @NotificationTypeId int
	, @NotificationDate datetime

AS

INSERT INTO dbo.InsureesNotificationHistoryEntry
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, NotificationTypeId
	, NotificationDate
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber
	, @NotificationTypeId
	, @NotificationDate
)

SELECT Id = MAX(Id) FROM dbo.InsureesNotificationHistoryEntry