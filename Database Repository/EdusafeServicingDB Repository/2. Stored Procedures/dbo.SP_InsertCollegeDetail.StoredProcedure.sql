IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertCollegeDetail' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertCollegeDetail
END 
GO

CREATE PROCEDURE SP_InsertCollegeDetail
	@CollegeName varchar(250)
	, @CollegeType varchar(25) 
	, @CollegeAcademicTermType varchar(25)
AS

DECLARE @CollegeTypeId int
SET @CollegeTypeId = 
	(SELECT Id FROM CollegeType WHERE CollegeType = @CollegeType)

DECLARE @CollegeAcademicTermTypeId int 
SET @CollegeAcademicTermTypeId = 
	(SELECT Id FROM CollegeAcademicTermType WHERE CollegeAcademicTermType = @CollegeAcademicTermType)

INSERT INTO dbo.CollegeDetail
(
	CreatedOn
	, CreatedBy
	, CollegeName
	, CollegeTypeId
	, CollegeAcademicTermTypeId
)
VALUES
(
	GETDATE()
	, USER
	, @CollegeName
	, @CollegeTypeId
	, @CollegeAcademicTermTypeId
)

SELECT Id = MAX(Id) FROM CollegeDetail