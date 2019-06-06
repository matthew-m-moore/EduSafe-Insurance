SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesMajorMinorDetailsSet')
	BEGIN

		CREATE TABLE dbo.InsureesMajorMinorDetailsSet
		(
			SetId int IDENTITY(1000,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, Description varchar(250) null
			CONSTRAINT PK_InsureesMajorMinorDetailsSet_Id PRIMARY KEY CLUSTERED (SetId)
		)
	
	END