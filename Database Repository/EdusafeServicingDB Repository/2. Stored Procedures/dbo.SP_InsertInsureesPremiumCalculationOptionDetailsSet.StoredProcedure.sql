IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesPremiumCalculationOptionDetailsSet' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesPremiumCalculationOptionDetailsSet
END 
GO

CREATE PROCEDURE SP_InsertInsureesPremiumCalculationOptionDetailsSet
	@AccountNumber bigint 
	, @Description varchar(250) null
AS


INSERT INTO dbo.InsureesPremiumCalculationOptionDetailsSet
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, Description
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber
	, @Description
)

SELECT SetId = MAX(SetId) FROM dbo.InsureesPremiumCalculationOptionDetailsSet