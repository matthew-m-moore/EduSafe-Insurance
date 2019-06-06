IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesPremiumCalculationDetailsSet' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesPremiumCalculationDetailsSet
END 
GO

CREATE PROCEDURE SP_InsertInsureesPremiumCalculationDetailsSet
	@AccountNumber bigint 
	, @InsureesPremiumCalculationDetailsId int 
	, @InsureesPremiumCalculationOptionDetailsSetId int 
	, @Description varchar(250) null
AS


INSERT INTO dbo.InsureesPremiumCalculationDetailsSet
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, InsureesPremiumCalculationDetailsId
	, InsureesPremiumCalculationOptionDetailsSetId
	, Description
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber
	, @InsureesPremiumCalculationDetailsId
	, @InsureesPremiumCalculationOptionDetailsSetId
	, @Description
)

SELECT SetId = MAX(SetId) FROM dbo.InsureesPremiumCalculationDetailsSet