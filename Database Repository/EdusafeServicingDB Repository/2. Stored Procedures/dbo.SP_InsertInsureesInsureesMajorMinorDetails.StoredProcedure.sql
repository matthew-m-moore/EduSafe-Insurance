IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesMajorMinorDetails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesMajorMinorDetails
END 
GO

CREATE PROCEDURE SP_InsertInsureesMajorMinorDetails
	@InsureesMajorMinorDetailsSetId int
	, @AccountNumber bigint 
	, @CollegeMajorId int
	, @IsMinor bit 
AS

INSERT INTO dbo.InsureesMajorMinorDetails
(
	CreatedOn
	, CreatedBy
	, InsureesMajorMinorDetailsSetId
	, AccountNumber
	, CollegeMajorId
	, IsMinor
)
VALUES
(
	GETDATE()
	, USER
	, @InsureesMajorMinorDetailsSetId
	, @AccountNumber
	, @CollegeMajorId
	, @IsMinor
)

SELECT Id = MAX(Id) FROM dbo.InsureesMajorMinorDetails