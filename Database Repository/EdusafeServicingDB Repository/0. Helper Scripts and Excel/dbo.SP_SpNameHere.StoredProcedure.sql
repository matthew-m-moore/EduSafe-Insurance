IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesAccountData' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesAccountData
END 
GO

CREATE PROCEDURE SP_InsertInsureesAccountData 
	@CollegeName varchar(250)

AS

INSERT INTO InsureesAccountData
(
	
)
VALUES
(
	
)

SELECT Id = MAX(Id) FROM WebSiteInquiryCollegeName