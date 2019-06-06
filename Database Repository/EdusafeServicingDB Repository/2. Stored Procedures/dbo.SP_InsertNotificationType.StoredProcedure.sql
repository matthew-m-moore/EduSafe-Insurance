IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertNotificationType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertNotificationType
END 
GO

CREATE PROCEDURE SP_InsertNotificationType
	@NotificationType varchar(25)
	, @Description varchar(250)

AS

INSERT INTO dbo.NotificationType
(			
	CreatedOn
	, CreatedBy
	, NotificationType
	, Description
)
VALUES
(		
	GETDATE()
	, USER
	, @NotificationType
	, @Description
)

SELECT Id = MAX(Id) FROM dbo.NotificationType