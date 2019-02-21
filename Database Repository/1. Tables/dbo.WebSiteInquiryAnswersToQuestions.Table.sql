IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryAnswersToQuestions')
	BEGIN
		CREATE TABLE WebSiteInquiryAnswersToQuestions
		(
			Id int IDENTITY(1,1)
			, IpAddressId int not null
			, CollegeNameId int not null
			, CollegeTypeId int not null
			, MajorId int not null
			, CollegeStartDate datetime not null
			, GraduationDate datetime not null
			, AnnualCoverage float not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
		)
	END