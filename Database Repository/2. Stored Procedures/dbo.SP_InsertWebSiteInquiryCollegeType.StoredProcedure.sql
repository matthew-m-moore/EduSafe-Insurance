IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryCollegeType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryCollegeType
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryCollegeType 
	@CollegeType varchar(250)

AS

INSERT INTO WebSiteInquiryCollegeType
(
	CollegeType
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@CollegeType
	, GETDATE()
	, USER
)

