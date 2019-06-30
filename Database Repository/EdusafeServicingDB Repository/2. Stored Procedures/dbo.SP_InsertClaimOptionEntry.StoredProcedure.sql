IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimOptionEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimOptionEntry
END 
GO

CREATE PROCEDURE SP_InsertClaimOptionEntry
	@ClaimNumber bigint
	, @ClaimOptionTypeId int
	, @ClaimOptionPercentage float

AS

INSERT INTO dbo.ClaimOptionEntry
(			
	CreatedOn
	, CreatedBy
	, ClaimNumber
	, ClaimOptionTypeId
	, ClaimOptionPercentage
)
VALUES
(		
	GETDATE()
	, USER
	, @ClaimNumber
	, @ClaimOptionTypeId
	, @ClaimOptionPercentage
)

SELECT Id = MAX(Id) FROM dbo.ClaimDocumentEntry