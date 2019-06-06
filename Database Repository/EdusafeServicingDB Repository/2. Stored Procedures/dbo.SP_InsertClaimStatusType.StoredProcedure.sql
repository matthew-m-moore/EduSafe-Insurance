IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimStatusType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimStatusType
END 
GO

CREATE PROCEDURE SP_InsertClaimStatusType
	@ClaimStatusType varchar(25)
	, @Description varchar(250)

AS

INSERT INTO dbo.ClaimStatusType
(			
	CreatedOn
	, CreatedBy
	, ClaimStatusType
	, Description
)
VALUES
(		
	GETDATE()
	, USER
	, @ClaimStatusType
	, @Description
)

SELECT Id = MAX(Id) FROM dbo.ClaimStatusType