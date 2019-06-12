IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertCollegeType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertCollegeType
END 
GO

CREATE PROCEDURE SP_InsertCollegeType
	@CollegeType varchar(25)
	, @Description varchar(250)

AS

INSERT INTO dbo.CollegeType
(			
	CreatedOn
	, CreatedBy
	, CollegeType
	, Description
)
VALUES
(		
	GETDATE()
	, USER
	, @CollegeType
	, @Description
)

SELECT Id = MAX(Id) FROM dbo.CollegeType