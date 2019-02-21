IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryCollegeName' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryCollegeName
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryCollegeName 
	@CollegeName varchar(250)

AS

INSERT INTO WebSiteInquiryCollegeName
(
	CollegeName
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@CollegeName
	, GETDATE()
	, USER
)

