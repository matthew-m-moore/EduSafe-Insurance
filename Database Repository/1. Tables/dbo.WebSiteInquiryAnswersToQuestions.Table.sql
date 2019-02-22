IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryAnswersToQuestions')
	BEGIN
		CREATE TABLE WebSiteInquiryAnswersToQuestions
		(
			Id int IDENTITY(1,1)
			, IpAddressId int
			, CollegeNameId int
			, CollegeTypeId int
			, MajorId int
			, CollegeStartDate datetime
			, GraduationDate datetime
			, AnnualCoverage float
			, CreatedOn datetime
			, CreatedBy varchar(25)
		)
	END