SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsInsureeList')
	BEGIN

		CREATE TABLE dbo.InstitutionsInsureeList
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, InstitutionsAccountNumber bigint not null
			, InsureeAccountNumber bigint not null
			CONSTRAINT PKInstitutionsInsureeList_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END