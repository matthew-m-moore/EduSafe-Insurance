IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsPaymentHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
	@AccountNumber bigint
	, @PaymentAmount float 
	, @PaymentDate datetime 
	, @PaymentStatusType varchar(50)
	, @PaymentComments varchar(250) null

AS

DECLARE @PaymentStatusTypeId int
SET @PaymentStatusTypeId = (SELECT Id FROM dbo.PaymentStatusType WHERE PaymentStatusType = @PaymentStatusType)

INSERT INTO dbo.InsureesPaymentHistoryEntry
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, PaymentAmount
	, PaymentDate
	, PaymentStatusTypeId
	, PaymentComments
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber 
	, @PaymentAmount
	, @PaymentDate
	, @PaymentStatusTypeId
	, @PaymentComments
)

SELECT Id = MAX(Id) FROM dbo.InsureesPaymentHistoryEntry