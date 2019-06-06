SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationOptionDetails')
	BEGIN

		CREATE TABLE dbo.InsureesPremiumCalculationOptionDetails
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, InsureesPremiumCalculationOptionDetailsSetId int not null
			, OptionTypeId int not null 
			, OptionPercentage decimal not null 
			CONSTRAINT PK_InsureesPremiumCalculationOptionDetails_Id PRIMARY KEY CLUSTERED (Id)
		)	
	END
GO

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE NAME = 'InsureesPremiumCalculationOptionDetails_OptionPercentageBetweenZeroAndOne')
BEGIN 
	ALTER TABLE InsureesPremiumCalculationOptionDetails
	ADD CONSTRAINT InsureesPremiumCalculationOptionDetails_OptionPercentageBetweenZeroAndOne
	CHECK (OptionPercentage between 0.0 and 1.0)
END