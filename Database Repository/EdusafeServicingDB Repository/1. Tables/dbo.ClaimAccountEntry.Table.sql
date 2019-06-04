SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimAccountEntry')
	BEGIN

		CREATE TABLE dbo.ClaimAccountEntry
		(
			ClaimNumber bigint IDENTITY(10000000,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, AccountNumber bigint not null
			CONSTRAINT PK_ClaimAccountEntry PRIMARY KEY CLUSTERED (ClaimNumber)
		)
	
	END