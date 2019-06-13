SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsAccountData')
	BEGIN

		CREATE TABLE dbo.InstitutionsAccountData
		(
			InstitutionsAccountNumber bigint IDENTITY(500000,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, FolderPath varchar(250) null
			, InstitutionName varchar(100) not null
			, EmailsSetId int not null
			, Address1 varchar(50) not null
			, Address2 varchar(50) null
			, Address3 varchar(50) null
			, City varchar(50) not null
			, State varchar(2) not null
			, Zipcode varchar(5) not null
			CONSTRAINT PK_InstitutionsAccountData_InstitutionsAccountNumber  PRIMARY KEY CLUSTERED (InstitutionsAccountNumber )
		)
	
	END