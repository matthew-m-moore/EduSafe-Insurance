IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertEmailsSet' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE cust.SP_InsertEmails
END 
GO

CREATE PROCEDURE cust.SP_InsertEmails
		@EmailsSetId int
		, @Email varchar(50)

AS

INSERT INTO cust.Emails
(			
	CreatedOn
	, CreatedBy
	, EmailsSetId
	, Email
)
VALUES
(		
	GETDATE()
	, USER
	, @EmailsSetId
	, @Email
)

SELECT Id = MAX(Id) FROM cust.Emails