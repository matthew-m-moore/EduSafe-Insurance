IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPremiumCalculationDetails')
	BEGIN

		CREATE TABLE dbo.InsureesPremiumCalculationDetails
		(
			Id int IDENTITY(1,1)
			, AccountNumber bigint not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null

			, PremiumCalulated decimal not null
			, PremiumCalculationDate datetime not null
			, TotalCoverageAmount decimal not null

			, GradSchoolOptionValue decimal null
			, DropOutOptionValue decimal null
			, EarlyHireOptionValue decimal null
			, CollegeClosureOption decimal null
			
			, CollegeStartDate datetime not null
			, ExpectedGraduationDate datetime not null

			, CollegeDetailId int not null
			, CollegeMajorId int not null

			, MajorDeclarationDate datetime null
			, UnitsCompleted int not null
			CONSTRAINT PK_InsureesPremiumCalculationDetails_Id PRIMARY KEY (Id)
		)
	
	END
GO
	
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE NAME = 'InsureesPremiumCalculationDetails_GradSchoolOptionValueBetweenZeroAndOne')
BEGIN 
	ALTER TABLE InsureesPremiumCalculationDetails
	ADD CONSTRAINT InsureesPremiumCalculationDetails_GradSchoolOptionValueBetweenZeroAndOne
	CHECK (GradSchoolOptionValue between 0.0 and 1.0)
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE NAME = 'InsureesPremiumCalculationDetails_DropOutOptionValueBetweenZeroAndOne')
BEGIN 
	ALTER TABLE InsureesPremiumCalculationDetails
	ADD CONSTRAINT InsureesPremiumCalculationDetails_DropOutOptionValueBetweenZeroAndOne
	CHECK (DropOutOptionValue between 0.0 and 1.0)
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE NAME = 'InsureesPremiumCalculationDetails_EarlyHireOptionValueBetweenZeroAndOne')
BEGIN 
	ALTER TABLE InsureesPremiumCalculationDetails
	ADD CONSTRAINT InsureesPremiumCalculationDetails_EarlyHireOptionValueBetweenZeroAndOne
	CHECK (EarlyHireOptionValue between 0.0 and 1.0)
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE NAME = 'InsureesPremiumCalculationDetails_CollegeClosureOptionValueBetweenZeroAndOne')
BEGIN 
	ALTER TABLE InsureesPremiumCalculationDetails
	ADD CONSTRAINT InsureesPremiumCalculationDetails_CollegeClosureOptionValueBetweenZeroAndOne
	CHECK (CollegeClosureOption between 0.0 and 1.0)
END
