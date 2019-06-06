IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsNotificationHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsNotificationHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsNotificationHistoryEntry
	@InstitutionsAccountNumber bigint
	, @NotificationType varchar(50) 
	, @NotificationDate datetime

AS

DECLARE @NotificationTypeId int
SET @NotificationTypeId = (SELECT Id FROM dbo.NotificationType WHERE NotificationType = @NotificationType)

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
	, @NotificationType
	, @NotificationDate
)

SELECT Id = MAX(Id) FROM dbo.InstitutionsNotificationHistoryEntry