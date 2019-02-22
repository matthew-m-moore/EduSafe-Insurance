IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryAnswersToQuestions' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryAnswersToQuestions
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryAnswersToQuestions
	@CollegeName varchar(250)
	, @CollegeType varchar(250)
	, @IpAddress varchar(250)
	, @Major varchar(250)
	, @CollegeStartDate datetime
	, @GraduationDate datetime
	, @AnnualCoverage float

AS

DECLARE @CollegeNameId int
IF (SELECT Id FROM WebSiteInquiryCollegeName WHERE CollegeName = @CollegeName) is null
	INSERT INTO WebSiteInquiryCollegeName VALUES (@CollegeName, GETDATE(), USER); 
SET @CollegeNameId = (SELECT Id FROM WebSiteInquiryCollegeName WHERE CollegeName = @CollegeName);

DECLARE @CollegeTypeId int
IF (SELECT Id FROM WebSiteInquiryCollegeType WHERE CollegeType = @CollegeType) is null
	INSERT INTO WebSiteInquiryCollegeType VALUES (@CollegeType, GETDATE(), USER);
SET @CollegeTypeId = (SELECT Id FROM WebSiteInquiryCollegeType WHERE CollegeType = @CollegeType);

DECLARE @IpAddressId int
	INSERT INTO WebSiteInquiryIpAddress VALUES (@IpAddress, GETDATE(), USER);
SET @IpAddressId = (SELECT MAX(Id) as Id FROM WebSiteInquiryIpAddress WHERE IpAddress = @IpAddress);

DECLARE @MajorId int
IF (SELECT Id FROM WebSiteInquiryMajor WHERE Major = @Major) is null
	INSERT INTO WebSiteInquiryMajor VALUES (@Major, GETDATE(), USER);
SET @MajorId = (SELECT Id FROM WebSiteInquiryMajor WHERE Major = @Major);

DECLARE @AnswersId int
IF (
	SELECT Id
	FROM WebSiteInquiryAnswersToQuestions 
	WHERE 
			IpAddressId = @IpAddressId
		and CollegeNameId = @CollegeNameId
		and CollegeTypeId = @CollegeTypeId
		and MajorId	= @MajorId 
	) is null

	INSERT INTO WebSiteInquiryAnswersToQuestions 
	(
		IpAddressId 
		, CollegeNameId 
		, CollegeTypeId
		, MajorId
		, CollegeStartDate
		, GraduationDate
		, AnnualCoverage
		, CreatedOn
		, CreatedBy
	)

	VALUES
	(
		@IpAddressId
		, @CollegeNameId
		, @CollegeTypeId
		, @MajorId
		, @CollegeStartDate
		, @GraduationDate
		, @AnnualCoverage
		, GETDATE()
		, USER	
	);

	SELECT   
	 Id     = MAX(Id)  
	 , IpAddressId = @IpAddressId  
	 , CollegeNameId = @CollegeNameId  
	 , CollegeTypeId = @CollegeTypeId  
	 , MajorId  = @MajorId  
	FROM WebSiteInquiryAnswersToQuestions
GO