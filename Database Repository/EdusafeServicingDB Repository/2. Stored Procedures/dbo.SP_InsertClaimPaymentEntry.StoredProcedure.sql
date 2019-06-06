IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimPaymentEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimPaymentEntry
END 
GO

CREATE PROCEDURE SP_InsertClaimPaymentEntry
	@ClaimNumber bigint
	, @ClaimPaymentAmount float
	, @ClaimPaymentDate datetime
	, @ClaimPaymentStatusType varchar(50)
	, @ClaimPaymentComments varchar(250) null

AS

DECLARE @ClaimPaymentStatusTypeId int
SET @ClaimPaymentStatusTypeId = (SELECT Id FROM dbo.PaymentStatusType WHERE PaymentStatusType = @ClaimPaymentStatusType)

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