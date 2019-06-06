IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesInsureesMajorMinorDetailsSet' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesInsureesMajorMinorDetailsSet
END 
GO

CREATE PROCEDURE SP_InsertInsureesInsureesMajorMinorDetailsSet
	@AccountNumber bigint
	, @Description varchar(250)
AS

INSERT INTO dbo.InsureesMajorMinorDetailsSet
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

SELECT SetId = MAX(SetId) FROM dbo.InsureesMajorMinorDetailsSet