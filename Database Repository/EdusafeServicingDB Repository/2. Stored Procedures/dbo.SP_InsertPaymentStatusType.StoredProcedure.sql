IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertPaymentStatusType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertPaymentStatusType
END 
GO

CREATE PROCEDURE SP_InsertPaymentStatusType
	@PaymentStatusType varchar(25)
	, @Description varchar(250)

AS

INSERT INTO dbo.PaymentStatusType
(			
	CreatedOn
	, CreatedBy
	, PaymentStatusType
	, Description
)
VALUES
(		
	GETDATE()
	, USER
	, @PaymentStatusType
	, @Description
)

SELECT Id = MAX(Id) FROM dbo.PaymentStatusType