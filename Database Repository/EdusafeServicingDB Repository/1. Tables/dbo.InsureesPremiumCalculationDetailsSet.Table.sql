SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetailsSet')
	BEGIN

		CREATE TABLE dbo.InsureesPremiumCalculationDetailsSet
		(
			SetId int IDENTITY(1000,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, InsureesPremiumCalculationDetailsId int not null
			, InsureesPremiumCalculationOptionDetailsSetId int not null
			, Description varchar(250) null
			CONSTRAINT PK_InsureesPremiumCalculationDetailsSet_Id PRIMARY KEY CLUSTERED (SetId)
		)	
	END
GO
