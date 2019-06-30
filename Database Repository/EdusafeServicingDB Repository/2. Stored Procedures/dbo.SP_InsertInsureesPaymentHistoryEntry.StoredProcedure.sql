IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsPaymentHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
	@AccountNumber bigint
	, @PaymentAmount float 
	, @PaymentDate datetime 
	, @PaymentStatusTypeId int
	, @PaymentComments varchar(250) null

AS

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