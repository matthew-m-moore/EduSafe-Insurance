IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesPremiumCalculationDetails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesPremiumCalculationDetails
END 
GO

CREATE PROCEDURE SP_InsertInsureesPremiumCalculationDetails
	@PremiumCalulated float 
	, @PremiumCalculationDate datetime
	, @TotalCoverageAmount float 
	, @CoverageMonths int
	, @CollegeStartDate datetime
	, @ExpectedGraduationDate datetime
	, @CollegeDetailId int
	, @InsureesMajorMinorDetailsSetId int
	, @MajorDeclarationDate datetime null
	, @UnitsCompleted int
AS

INSERT INTO dbo.InsureesPremiumCalculationDetails
(
	CreatedOn
	, CreatedBy
	, PremiumCalulated
	, PremiumCalculationDate
	, TotalCoverageAmount
	, CoverageMonths
	, CollegeStartDate
	, ExpectedGraduationDate
	, CollegeDetailId
	, InsureesMajorMinorDetailsSetId
	, MajorDeclarationDate
	, UnitsCompleted
)
VALUES
(
	GETDATE()
	, USER
	, @PremiumCalulated
	, @PremiumCalculationDate
	, @TotalCoverageAmount
	, @CoverageMonths
	, @CollegeStartDate
	, @ExpectedGraduationDate
	, @CollegeDetailId
	, @InsureesMajorMinorDetailsSetId
	, @MajorDeclarationDate
	, @UnitsCompleted
)

SELECT Id = MAX(Id) FROM dbo.InsureesPremiumCalculationDetails