IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryDegreeType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryDegreeType
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryDegreeType 
	@DegreeType varchar(250)

AS

INSERT INTO WebSiteInquiryDegreeType
(
	DegreeType
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@DegreeType
	, GETDATE()
	, USER
)

SELECT Id = MAX(Id) FROM WebSiteInquiryDegreeType