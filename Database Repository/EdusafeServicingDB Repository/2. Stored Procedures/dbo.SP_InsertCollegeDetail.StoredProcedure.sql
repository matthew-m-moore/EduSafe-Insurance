IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertCollegeDetail' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertCollegeDetail
END 
GO

CREATE PROCEDURE SP_InsertCollegeDetail
	@CollegeName varchar(250)
	, @CollegeTypeId int
	, @CollegeAcademicTermTypeId int
AS

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