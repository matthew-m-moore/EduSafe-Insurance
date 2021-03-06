IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInstitutionsAccountData' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE dbo.SP_InsertInstitutionsAccountData
END 
GO

CREATE PROCEDURE dbo.SP_InsertInstitutionsAccountData
	@FolderPath varchar(250) null
	, @InstitutionName varchar(100) 
	, @Address1 varchar(50) 
	, @Address2 varchar(50) null
	, @Address3 varchar(50) null
	, @City varchar(50) 
	, @State varchar(2) 
	, @Zipcode varchar(5) 

AS

EXEC cust.SP_InsertEmailsSet

DECLARE @EmailsSetId int
SET @EmailsSetId = (SELECT Max(SetId) FROM cust.EmailsSet)

INSERT INTO dbo.InstitutionsAccountData
(
	CreatedOn
	, CreatedBy
	, FolderPath
	, InstitutionName
	, EmailsSetId
	, Address1
	, Address2
	, Address3
	, City
	, State
	, Zipcode
)
VALUES
(
	GETDATE()
	, USER
	, @FolderPath
	, @InstitutionName
	, @EmailsSetId
	, @Address1
	, @Address2
	, @Address3
	, @City
	, @State
	, @Zipcode
)

SELECT InstitutionsAccountNumber = MAX(InstitutionsAccountNumber) FROM dbo.InstitutionsAccountData