IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimPaymentEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimPaymentEntry
END 
GO

CREATE PROCEDURE SP_InsertClaimPaymentEntry
	@ClaimNumber bigint
	, @ClaimPaymentAmount float
	, @ClaimPaymentDate datetime
	, @ClaimPaymentStatusTypeId int
	, @ClaimPaymentComments varchar(250) null

AS

INSERT INTO dbo.ClaimPaymentEntry
(			
	CreatedOn
	, CreatedBy
	, ClaimNumber
	, ClaimPaymentAmount
	, ClaimPaymentDate
	, ClaimPaymentStatusTypeId
	, ClaimPaymentComments
)
VALUES
(		
	GETDATE()
	, USER
	, @ClaimNumber
	, @ClaimPaymentAmount
	, @ClaimPaymentDate
	, @ClaimPaymentStatusTypeId
	, @ClaimPaymentComments
)

SELECT Id = MAX(Id) FROM dbo.ClaimPaymentEntry