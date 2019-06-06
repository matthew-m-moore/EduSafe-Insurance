IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsInsureeList' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInstitutionsInsureeList
END 
GO

CREATE PROCEDURE SP_InsertInstitutionsInsureeList
	@InstitutionsAccountNumber bigint
	, @InsureeAccountNumber bigint

AS

INSERT INTO dbo.InstitutionsInsureeList
(
	CreatedOn
	, CreatedBy
	, InstitutionsAccountNumber
	, InsureeAccountNumber
)
VALUES
(
	GETDATE()
	, USER
	, @InstitutionsAccountNumber
	, @InsureeAccountNumber
)

SELECT Id = MAX(Id) FROM dbo.InstitutionsInsureeList