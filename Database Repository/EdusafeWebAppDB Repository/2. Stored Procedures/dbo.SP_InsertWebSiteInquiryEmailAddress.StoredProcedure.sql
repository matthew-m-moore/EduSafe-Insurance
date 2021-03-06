IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryEmailAddress' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryEmailAddress
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryEmailAddress
	@EmaiLAddress varchar(250)
	, @IpAddress varchar(250)
	, @ContactName varchar(250)
	, @OptOut bit

AS

INSERT INTO WebSiteInquiryEmailAddress
(
	EmailAddress
	, IpAddress
	, ContactName
	, OptOut
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@EmailAddress
	, @IpAddress
	, @ContactName
	, @OptOut
	, GETDATE()
	, USER
)

SELECT Id = MAX(Id) FROM WebSiteInquiryEmailAddress