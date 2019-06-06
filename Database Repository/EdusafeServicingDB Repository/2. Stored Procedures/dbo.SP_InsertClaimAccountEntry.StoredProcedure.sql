IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimAccountEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimAccountEntry
END 
GO

CREATE PROCEDURE SP_InsertClaimAccountEntry 
		@AccountNumber bigint

AS

INSERT INTO dbo.ClaimAccountEntry
(
			CreatedOn
			, CreatedBy
			, AccountNumber
)
VALUES
(
			GETDATE()
			, USER
			, @AccountNumber
	
)

SELECT ClaimNumber = MAX(ClaimNumber) FROM dbo.ClaimAccountEntry