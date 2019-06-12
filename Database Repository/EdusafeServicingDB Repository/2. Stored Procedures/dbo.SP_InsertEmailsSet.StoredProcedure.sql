IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertEmailsSet' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertEmailsSet
END 
GO

CREATE PROCEDURE SP_InsertEmailsSet
		@Email varchar(50)

AS

INSERT INTO dbo.EmailsSet
(			
	CreatedOn
	, CreatedBy
	, Email
)
VALUES
(		
	GETDATE()
	, USER
	, @Email
)

SELECT SetId = MAX(SetId) FROM dbo.EmailsSet