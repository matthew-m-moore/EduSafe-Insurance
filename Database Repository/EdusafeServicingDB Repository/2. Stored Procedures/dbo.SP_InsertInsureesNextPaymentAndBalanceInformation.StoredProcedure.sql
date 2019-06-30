IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesNextPaymentAndBalanceInformation' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesNextPaymentAndBalanceInformation
END 
GO

CREATE PROCEDURE SP_InsertInsureesNextPaymentAndBalanceInformation
	@AccountNumber bigint
	, @NextPaymentAmount float
	, @NextPaymentDate datetime
	, @CurrentBalance float
	, @NextPaymentStatusTypeId int

AS

INSERT INTO dbo.InsureesNextPaymentAndBalanceInformation
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, NextPaymentAmount
	, NextPaymentDate
	, CurrentBalance
	, NextPaymentStatusTypeId
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber
	, @NextPaymentAmount
	, @NextPaymentDate
	, @CurrentBalance
	, @NextPaymentStatusTypeId
)

SELECT Id = MAX(Id) FROM dbo.InsureesNextPaymentAndBalanceInformation