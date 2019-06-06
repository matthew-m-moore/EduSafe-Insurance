SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeType')
	BEGIN

		CREATE TABLE dbo.CollegeType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, CollegeType varchar(25) not null
			, Description varchar(250) null
			CONSTRAINT PK_CollegeType_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END

INSERT INTO CollegeType VALUES(GETDATE(), 'Sharon Paesachov', 'Public', 'Public Institutions')
INSERT INTO CollegeType VALUES(GETDATE(), 'Sharon Paesachov', 'Private', 'Private Institutions')
INSERT INTO CollegeType VALUES(GETDATE(), 'Sharon Paesachov', 'ForProfit', 'ForProfit Institutions')