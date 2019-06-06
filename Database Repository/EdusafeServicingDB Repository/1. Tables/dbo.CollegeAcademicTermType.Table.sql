SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeAcademicTermType')
	BEGIN

		CREATE TABLE dbo.CollegeAcademicTermType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, CollegeAcademicTermType varchar(25) not null
			, Description varchar(250) null
			CONSTRAINT PK_CollegeAcademicTermType_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END

INSERT INTO CollegeAcademicTermType VALUES(GETDATE(), 'Sharon Paesachov', 'Semester', null)
INSERT INTO CollegeAcademicTermType VALUES(GETDATE(), 'Sharon Paesachov', 'Quarter', null)
INSERT INTO CollegeAcademicTermType VALUES(GETDATE(), 'Sharon Paesachov', 'Trimester', null)