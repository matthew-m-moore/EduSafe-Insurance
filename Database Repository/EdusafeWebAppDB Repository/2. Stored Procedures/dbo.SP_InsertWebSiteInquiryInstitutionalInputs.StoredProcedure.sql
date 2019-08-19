IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryInstitutionalInputs' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryInstitutionalInputs
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryInstitutionalInputs
	@CollegeName varchar(250)
	, @CollegeType varchar(250)
	, @IpAddress varchar(250)
	, @DegreeType varchar(250)
	, @StudentsPerStartingClass int
	, @GraduationWithinYears1 float
	, @GraduationWithinYears2 float
	, @GraduationWithinYears3 float
	, @StartingCohortDefaultRate float
	, @AverageLoanDebtAtGraduation float

AS

DECLARE @CollegeNameId int  
IF (SELECT Id FROM WebSiteInquiryCollegeName WHERE CollegeName = @CollegeName) is null  
	INSERT INTO WebSiteInquiryCollegeName VALUES (@CollegeName, GETDATE(), USER);  
SET @CollegeNameId = (SELECT Id FROM WebSiteInquiryCollegeName WHERE CollegeName = @CollegeName);  
  
DECLARE @CollegeTypeId int  
IF (SELECT Id FROM WebSiteInquiryCollegeType WHERE CollegeType = @CollegeType) is null  
	INSERT INTO WebSiteInquiryCollegeType VALUES (@CollegeType, GETDATE(), USER);
SET @CollegeTypeId = (SELECT Id FROM WebSiteInquiryCollegeType WHERE CollegeType = @CollegeType);  
  
INSERT INTO WebSiteInquiryIpAddress VALUES (@IpAddress, GETDATE(), USER)
DECLARE @IpAddressId int  
SET @IpAddressId = (SELECT MAX(Id) as Id FROM WebSiteInquiryIpAddress WHERE IpAddress = @IpAddress);  
  
DECLARE @DegreeTypeId int  
IF (SELECT Id FROM WebSiteInquiryDegreeType WHERE DegreeType = @DegreeType) is null  
	INSERT INTO WebSiteInquiryDegreeType VALUES (@DegreeType, GETDATE(), USER);
SET @DegreeTypeId = (SELECT Id FROM WebSiteInquiryDegreeType WHERE DegreeType = @DegreeType);  

INSERT INTO WebSiteInquiryInstitutionalInputs 
(
	IpAddressId 
	, CollegeNameId 
	, CollegeTypeId
	, DegreeTypeId
	, StudentsPerStartingClass
	, GraduationWithinYears1
	, GraduationWithinYears2
	, GraduationWithinYears3
	, StartingCohortDefaultRate
	, AverageLoanDebtAtGraduation
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@IpAddressId
	, @CollegeNameId
	, @CollegeTypeId
	, @DegreeTypeId
	, @StudentsPerStartingClass
	, @GraduationWithinYears1
	, @GraduationWithinYears2
	, @GraduationWithinYears3
	, @StartingCohortDefaultRate
	, @AverageLoanDebtAtGraduation
	, GETDATE()
	, USER
);

SELECT   
	Id     = MAX(Id)  
	, IpAddressId = @IpAddressId  
	, CollegeNameId = @CollegeNameId  
	, CollegeTypeId = @CollegeTypeId  
	, DegreeTypeId  = @DegreeTypeId  
FROM WebSiteInquiryInstitutionalInputs