IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryIpAddress' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertWebSiteInquiryIpAddress
END 
GO

CREATE PROCEDURE SP_InsertWebSiteInquiryIpAddress
	@IpAddress varchar(250)

AS

INSERT INTO WebSiteInquiryIpAddress
(
	IpAddress
	, CreatedOn
	, CreatedBy
)
VALUES
(
	@IpAddress
	, GETDATE()
	, USER
)

