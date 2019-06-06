IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsNextPaymentAndBalanceInformation' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsNextPaymentAndBalanceInformation
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsNextPaymentAndBalanceInformation
	@InstitutionsAccountNumber bigint
	, @NextPaymentAmount numeric
	, @NextPaymentDate datetime
	, @CurrentBalance numeric
	, @NextPaymentStatusType varchar(50)

AS

DECLARE @NextPaymentStatusTypeId int
SET @NextPaymentStatusTypeId = (SELECT Id FROM dbo.PaymentStatusType WHERE PaymentStatusType = @NextPaymentStatusType)

INSERT INTO dbo.InstitutionsNextPaymentAndBalanceInformation
(
	CreatedOn
	, CreatedBy
	, InstitutionsAccountNumber
	, NextPaymentAmount
	, NextPaymentDate
	, CurrentBalance
	, NextPaymentStatusTypeId
)
VALUES
(
	GETDATE()
	, USER
	, @InstitutionsAccountNumber
	, @NextPaymentAmount
	, @NextPaymentDate
	, @CurrentBalance
	, @NextPaymentStatusTypeId
)

SELECT Id = MAX(Id) FROM dbo.InstitutionsNextPaymentAndBalanceInformation