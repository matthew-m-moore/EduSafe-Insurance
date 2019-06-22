IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertEmails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE cust.SP_InsertEmails
END 
GO

CREATE PROCEDURE cust.SP_InsertEmails
		@EmailsSetId int
		, @Email varchar(50)
		, @IsPrimary bit
		
AS

IF (@IsPrimary = 1)
BEGIN
	UPDATE cust.Emails SET IsPrimary = 0 WHERE EmailsSetId = @EmailsSetId
END

INSERT INTO cust.Emails
(			
	CreatedOn
	, CreatedBy
	, EmailsSetId
	, Email
	, IsPrimary
)
VALUES
(		
	GETDATE()
	, USER
	, @EmailsSetId
	, @Email
	, @IsPrimary
)

SELECT Id = MAX(Id) FROM cust.Emails