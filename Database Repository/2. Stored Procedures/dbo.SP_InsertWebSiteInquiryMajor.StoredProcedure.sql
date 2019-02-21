IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryMajor' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryMajor
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryMajor
	@Major varchar(250)

AS

INSERT INTO WebSiteInquiryMajor
(
	Major
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@Major
	, GETDATE()
	, USER
)

