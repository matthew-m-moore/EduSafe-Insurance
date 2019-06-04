SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimOptionEntry')
	BEGIN

		CREATE TABLE dbo.ClaimOptionEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, ClaimNumber bigint not null
			, ClaimOptionTypeId int not null 
			, ClaimOptionPercentage decimal not null 
			CONSTRAINT PK_ClaimOptionEntry PRIMARY KEY CLUSTERED (Id)
		)
	
	END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE NAME = 'ClaimOptionEntry_ClaimOptionPercentageBetweenZeroAndOne')
BEGIN 
	ALTER TABLE ClaimOptionEntry
	ADD CONSTRAINT ClaimOptionEntry_ClaimOptionPercentageBetweenZeroAndOne
	CHECK (ClaimOptionPercentage between 0.0 and 1.0)
END