IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesNotificationHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesNotificationHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInsureesNotificationHistoryEntry
	@AccountNumber bigint
	, @NotificationType varchar(50) 
	, @NotificationDate datetime

AS

DECLARE @NotificationTypeId int
SET @NotificationTypeId = (SELECT Id FROM dbo.NotificationType WHERE NotificationType = @NotificationType)

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
	, @NotificationType
	, @NotificationDate
)

SELECT Id = MAX(Id) FROM dbo.InsureesNotificationHistoryEntry