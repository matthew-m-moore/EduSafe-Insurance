IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryInstitutionalInputs')
	BEGIN
		CREATE TABLE WebSiteInquiryInstitutionalInputs
		(
			Id int IDENTITY(1,1)
			, IpAddressId int not null
			, CollegeNameId int not null
			, CollegeTypeId int not null
			, DegreeTypeId int not null
			, StudentsPerStartingClass int not null
			, GraduationWithinYears1 float not null
			, GraduationWithinYears2 float not null
			, GraduationWithinYears3 float not null
			, StartingCohortDefaultRate float not null
			, AverageLoanDebtAtGraduation float not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			CONSTRAINT PK_InstitutionalInputs PRIMARY KEY (Id)
		)
	END