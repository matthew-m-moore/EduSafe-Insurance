IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertCollegeAcademicTermType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertCollegeAcademicTermType
END 
GO

CREATE PROCEDURE SP_InsertCollegeAcademicTermType
	@CollegeAcademicTermType varchar(25)
	, @Description varchar(250)

AS

INSERT INTO dbo.CollegeAcademicTermType
(			
	CreatedOn
	, CreatedBy
	, CollegeAcademicTermType
	, Description
)
VALUES
(		
	GETDATE()
	, USER
	, @CollegeAcademicTermType
	, @Description
)

SELECT Id = MAX(Id) FROM dbo.CollegeAcademicTermType