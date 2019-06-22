IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsPaymentHistoryEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsPaymentHistoryEntry
	@InstitutionsAccountNumber bigint
	, @PaymentAmount float 
	, @PaymentDate datetime 
	, @PaymentStatusType varchar(50)
	, @PaymentComments varchar(250) null

AS

DECLARE @PaymentStatusTypeId int
SET @PaymentStatusTypeId = (SELECT Id FROM dbo.PaymentStatusType WHERE PaymentStatusType = @PaymentStatusType)

INSERT INTO dbo.InstitutionsPaymentHistoryEntry
(
	CreatedOn
	, CreatedBy
	, InstitutionsAccountNumber
	, PaymentAmount
	, PaymentDate
	, PaymentStatusTypeId
	, PaymentComments
)
VALUES
(
	GETDATE()
	, USER
	, @InstitutionsAccountNumber 
	, @PaymentAmount
	, @PaymentDate
	, @PaymentStatusTypeId
	, @PaymentComments
)

SELECT Id = MAX(Id) FROM dbo.InstitutionsPaymentHistoryEntry