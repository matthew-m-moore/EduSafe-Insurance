SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OptionType')
	BEGIN

		CREATE TABLE dbo.OptionType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, OptionType varchar(50) not null
			CONSTRAINT PK_OptionType PRIMARY KEY CLUSTERED (Id)
		)
	
	END

INSERT INTO OptionType VALUES(GETDATE(), USER, 'GradSchoolOption')
INSERT INTO OptionType VALUES(GETDATE(), USER, 'TerminationOption')
INSERT INTO OptionType VALUES(GETDATE(), USER, 'EarlyHireOption')
INSERT INTO OptionType VALUES(GETDATE(), USER, 'CollegeClosureOption')