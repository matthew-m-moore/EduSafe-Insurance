IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimStatusEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimStatusEntry
END 
GO

CREATE PROCEDURE SP_InsertClaimStatusEntry
	@ClaimNumber bigint
	, @ClaimStatusType varchar(50)
	, @IsClaimApproved bit

AS

DECLARE @ClaimStatusTypeId int
SET @ClaimStatusTypeId = (SELECT Id FROM dbo.ClaimStatusType WHERE ClaimStatusType = @ClaimStatusType)

INSERT INTO dbo.ClaimStatusEntry
(			
	CreatedOn
	, CreatedBy
	, ClaimNumber
	, ClaimStatusTypeId
	, IsClaimApproved
)
VALUES
(		
	GETDATE()
	, USER
	, @ClaimNumber
	, @ClaimStatusTypeId
	, @IsClaimApproved
)

SELECT Id = MAX(Id) FROM dbo.ClaimStatusEntry