SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeMajor')
	BEGIN

		CREATE TABLE dbo.CollegeMajor
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, CollegeMajor varchar(50) not null
			CONSTRAINT PK_CollegeMajor_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END