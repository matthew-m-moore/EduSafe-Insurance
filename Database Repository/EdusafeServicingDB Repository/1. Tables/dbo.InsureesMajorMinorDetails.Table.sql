SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesMajorMinorDetails')
	BEGIN

		CREATE TABLE dbo.InsureesMajorMinorDetails
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, CollegeMajorId int not null
			, isMinor bit not null
			CONSTRAINT PK_InsureesMajorMinorDetails_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END