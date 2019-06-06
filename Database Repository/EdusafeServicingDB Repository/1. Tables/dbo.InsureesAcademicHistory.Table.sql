SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesAcademicHistory')
	BEGIN

		CREATE TABLE dbo.InsureesAcademicHistory
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, AcademicTermStartDate datetime not null
			, AcademicTermEndDate datetime not null
			, CourseName varchar(25) not null
			, CourseInMajor bit not null
			, CollegeMajorOrMinorId int not null
			, CourseUnits int not null
			, CourseGrade varchar(5) not null
			CONSTRAINT PK_InsureesAcademicHistory_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END