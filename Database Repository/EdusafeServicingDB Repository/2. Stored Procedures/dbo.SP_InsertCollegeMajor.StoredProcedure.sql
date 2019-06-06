IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertCollegeMajor' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertCollegeMajor
END 
GO

CREATE PROCEDURE SP_InsertCollegeMajor
	@CollegeMajor varchar(50)

AS

INSERT INTO CollegeMajor
(
	CreatedOn
	, CreatedBy
	, CollegeMajor
)
VALUES
(
	GETDATE()
	, USER
	, @CollegeMajor
)

SELECT Id = MAX(Id) FROM dbo.CollegeMajor