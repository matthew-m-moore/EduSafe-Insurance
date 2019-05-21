IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertMajor' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertMajor
END 
GO

CREATE PROCEDURE SP_InsertMajor
	@Major varchar(250)

AS

INSERT INTO Major
(
	Major
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@Major
	, GETDATE()
	, USER
)

SELECT Id = MAX(Id) FROM Major