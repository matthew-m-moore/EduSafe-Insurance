IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesAcademicHistory' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesAcademicHistory
END 
GO

CREATE PROCEDURE SP_InsertInsureesAcademicHistory
	@AccountNumber bigint 
	, @AcademicTermStartDate datetime 
	, @AcademicTermEndDate datetime 
	, @CourseName varchar(25) 
	, @CourseInMajor bit 
	, @CollegeMajorOrMinor varchar(50)
	, @CourseUnits int 
	, @CourseGrade varchar(5) 

AS

DECLARE @CollegeMajorOrMinorId int
SET @CollegeMajorOrMinorId = (SELECT Id FROM dbo.CollegeMajor WHERE CollegeMajor = @CollegeMajorOrMinor)

INSERT INTO dbo.InsureesAcademicHistory
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, AcademicTermStartDate
	, AcademicTermEndDate
	, CourseName
	, CourseInMajor
	, CollegeMajorOrMinorId
	, CourseUnits
	, CourseGrade
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber
	, @AcademicTermStartDate
	, @AcademicTermEndDate
	, @CourseName
	, @CourseInMajor
	, @CollegeMajorOrMinorId 
	, @CourseUnits
	, @CourseGrade
)

SELECT Id = MAX(Id) FROM dbo.InsureesAcademicHistory