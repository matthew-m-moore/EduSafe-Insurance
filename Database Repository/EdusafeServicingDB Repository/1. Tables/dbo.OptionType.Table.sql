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
			, OptionType varchar(25) not null
			, Description varchar(250) null
			CONSTRAINT PK_OptionType PRIMARY KEY CLUSTERED (Id)
		)
	
	END

INSERT INTO OptionType VALUES(GETDATE(), USER, 'GradSchoolOption', 'Optionality to allow repayment in the event of student transitioning to graduate school')
INSERT INTO OptionType VALUES(GETDATE(), USER, 'TerminationOption', 'Optionality to allow repayment in the event of student terminates education. Example is dropout, but not limited to')
INSERT INTO OptionType VALUES(GETDATE(), USER, 'EarlyHireOption', 'Optionality to allow repayment in the event of student getting hired before graduation')
INSERT INTO OptionType VALUES(GETDATE(), USER, 'CollegeClosureOption', 'Optionality to allow repayment in the event of institution closure')