IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesPaymentHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesPaymentHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInsureesPaymentHistoryEntry
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