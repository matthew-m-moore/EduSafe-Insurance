IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertFileVerificationStatusType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertFileVerificationStatusType
END 
GO

CREATE PROCEDURE SP_InsertFileVerificationStatusType
	@FileVerificationStatusType varchar(25)
	, @Description varchar(250)
	
AS

INSERT INTO dbo.FileVerificationStatusType
(			
	CreatedOn
	, CreatedBy
	, FileVerificationStatusType
	, Description

)
VALUES
(		
	GETDATE()
	, USER
	, @FileVerificationStatusType
	, @Description

)

SELECT Id = MAX(Id) FROM dbo.FileVerificationStatusType