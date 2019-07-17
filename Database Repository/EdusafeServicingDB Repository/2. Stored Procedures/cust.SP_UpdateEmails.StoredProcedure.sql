IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_UpdateEmails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE cust.SP_UpdateEmails
END 
GO

--Note that Entity Framework 6 forces all columns to be included as inputs, even if not all are used
CREATE PROCEDURE cust.SP_UpdateEmails
		@Id int
		, @EmailsSetId int
		, @Email varchar(50)
		, @IsPrimary bit
		
AS

IF (@IsPrimary = 1)
BEGIN
	UPDATE cust.Emails SET IsPrimary = 0 WHERE EmailsSetId = @EmailsSetId
END

UPDATE cust.Emails
SET
	CreatedOn = GETDATE()
	, CreatedBy = USER
	, IsPrimary = @IsPrimary
WHERE Id = @Id
	