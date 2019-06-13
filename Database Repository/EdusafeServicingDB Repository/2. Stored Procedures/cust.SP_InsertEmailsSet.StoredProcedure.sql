IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertEmailsSet' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE cust.SP_InsertEmailsSet
END 
GO

CREATE PROCEDURE cust.SP_InsertEmailsSet

AS

INSERT INTO cust.EmailsSet
(			
	CreatedOn
	, CreatedBy
)
VALUES
(		
	GETDATE()
	, USER
)

SELECT SetId = MAX(SetId) FROM cust.EmailsSet