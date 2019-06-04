SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetailsSet')
	BEGIN

		CREATE TABLE dbo.InsureesPremiumCalculationDetailsSet
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, InsureesPremiumCalculationDetailsId int not null
			, InsureesPremiumCalculationOptionDetailsId int not null
			CONSTRAINT PK_InsureesPremiumCalculationDetailsSet_Id PRIMARY KEY CLUSTERED (Id)
		)	
	END
GO
