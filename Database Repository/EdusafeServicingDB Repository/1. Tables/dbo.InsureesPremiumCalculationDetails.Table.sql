SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetails')
	BEGIN

		CREATE TABLE dbo.InsureesPremiumCalculationDetails
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null

			, PremiumCalulated decimal not null
			, PremiumCalculationDate datetime not null
			, TotalCoverageAmount decimal not null
			
			, CollegeStartDate datetime not null
			, ExpectedGraduationDate datetime not null

			, CollegeDetailId int not null
			, CollegeMajorId int not null

			, MajorDeclarationDate datetime null
			, UnitsCompleted int not null

			CONSTRAINT PK_InsureesPremiumCalculationDetails_Id PRIMARY KEY CLUSTERED (Id)
		)	
	END
GO
