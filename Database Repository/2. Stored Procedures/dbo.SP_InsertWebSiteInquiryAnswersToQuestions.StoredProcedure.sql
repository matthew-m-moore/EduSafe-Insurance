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
IF (SELECT ID FROM WebSiteInquiryCollegeName WHERE CollegeName = @CollegeName) is null
	EXEC  SP_InsertWebSiteInquiryCollegeName @CollegeName;
SET @CollegeNameId = (SELECT ID FROM WebSiteInquiryCollegeName WHERE CollegeName = @CollegeName);

DECLARE @CollegeTypeId int
IF (SELECT ID FROM WebSiteInquiryCollegeType WHERE CollegeType = @CollegeType) is null
	EXEC  SP_InsertWebSiteInquiryCollegeType @CollegeType;
SET @CollegeTypeId = (SELECT ID FROM WebSiteInquiryCollegeType WHERE CollegeType = @CollegeType);

DECLARE @IpAddressId int
	EXEC  SP_InsertWebSiteInquiryIpAddress @IpAddress;
SET @IpAddressId = (SELECT MAX(ID) as ID FROM WebSiteInquiryIpAddress WHERE IpAddress = @IpAddress);

DECLARE @MajorId int
IF (SELECT ID FROM WebSiteInquiryMajor WHERE Major = @Major) is null
	EXEC  SP_InsertWebSiteInquiryIpAddress @IpAddress;
SET @MajorId = (SELECT ID FROM WebSiteInquiryMajor WHERE Major = @Major);

DECLARE @AnswersId int
IF (
	SELECT ID
	FROM WebSiteInquiryAnswersToQuestions 
	WHERE 
			IPAddressId = @IpAddressId
		and CollegeNameId = @CollegeNameId
		and CollegeTypeId = @CollegeTypeId
		and MajorId	= @MajorId 
	) is null

	INSERT INTO WebSiteInquiryAnswersToQuestions 
	(
		IPAddressId 
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
GO