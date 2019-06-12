IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsPaymentHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
	@InstitutionsAccountNumber bigint
	, @PaymentAmount float 
	, @PaymentDate datetime 
	, @PaymentComments varchar(250) null

AS

INSERT INTO dbo.InstitutionsPaymentHistoryEntry
(
	CreatedOn
	, CreatedBy
	, InstitutionsAccountNumber
	, PaymentAmount
	, PaymentDate
	, PaymentComments
)
VALUES
(
	GETDATE()
	, USER
	, @InstitutionsAccountNumber 
	, @PaymentAmount
	, @PaymentDate
	, @PaymentComments
)

SELECT Id = MAX(Id) FROM dbo.InstitutionsPaymentHistoryEntry