IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_DeleteEmails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE cust.SP_DeleteEmails
END 
GO

CREATE PROCEDURE cust.SP_DeleteEmails
		@Id int
		
AS

DELETE FROM cust.Emails
WHERE Id = @Id
	