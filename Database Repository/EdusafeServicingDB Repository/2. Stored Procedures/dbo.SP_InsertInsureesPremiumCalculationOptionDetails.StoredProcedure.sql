IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesPremiumCalculationOptionDetails2' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesPremiumCalculationOptionDetails
END 
GO

CREATE PROCEDURE SP_InsertInsureesPremiumCalculationOptionDetails
	@InsureesPremiumCalculationOptionDetailsSetId int 
	, @OptionType varchar(25)  
	, @OptionPercentage float 
AS

DECLARE @OptionTypeId int
SET @OptionTypeId = (SELECT Id FROM dbo.OptionType WHERE OptionType = @OptionType)

INSERT INTO dbo.InsureesPremiumCalculationOptionDetails
(
	CreatedOn
	, CreatedBy
	, InsureesPremiumCalculationOptionDetailsSetId
	, OptionTypeId
	, OptionPercentage
)
VALUES
(
	GETDATE()
	, USER
	, @InsureesPremiumCalculationOptionDetailsSetId
	, @OptionTypeId
	, @OptionPercentage
)

SELECT Id = MAX(Id) FROM dbo.InsureesPremiumCalculationOptionDetails