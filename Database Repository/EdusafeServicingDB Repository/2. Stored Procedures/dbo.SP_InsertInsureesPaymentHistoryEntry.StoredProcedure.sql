IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsPaymentHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
	@AccountNumber bigint
	, @PaymentAmount float 
	, @PaymentDate datetime 
	, @PaymentComments varchar(250) null

AS

INSERT INTO dbo.InsureesPaymentHistoryEntry
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, PaymentAmount
	, PaymentDate
	, PaymentComments
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber 
	, @PaymentAmount
	, @PaymentDate
	, @PaymentComments
)

SELECT Id = MAX(Id) FROM dbo.InsureesPaymentHistoryEntry