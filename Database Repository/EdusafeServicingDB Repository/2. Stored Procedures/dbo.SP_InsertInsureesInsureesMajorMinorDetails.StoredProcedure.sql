IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesInsureesMajorMinorDetails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesInsureesMajorMinorDetails
END 
GO

CREATE PROCEDURE SP_InsertInsureesInsureesMajorMinorDetails
	@InsureesMajorMinorDetailsSetId int
	, @AccountNumber bigint 
	, @CollegeMajor varchar(50)
	, @isMinor bit 
AS

DECLARE @CollegeMajorId int
SET @CollegeMajorId = (SELECT Id FROM CollegeMajor WHERE CollegeMajor = @CollegeMajor)

INSERT INTO dbo.InsureesMajorMinorDetails
(
	CreatedOn
	, CreatedBy
	, InsureesMajorMinorDetailsSetId
	, AccountNumber
	, CollegeMajorId
	, isMinor
)
VALUES
(
	GETDATE()
	, USER
	, @InsureesMajorMinorDetailsSetId
	, @AccountNumber
	, @CollegeMajorId
	, @isMinor
)

SELECT Id = MAX(Id) FROM dbo.InsureesMajorMinorDetails