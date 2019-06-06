IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesEnrollmentVerificationDetails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesEnrollmentVerificationDetails
END 
GO

CREATE PROCEDURE SP_InsertInsureesEnrollmentVerificationDetails
	@AccountNumber bigint 
	, @IsVerified bit 
	, @VerificationDate datetime null
	, @Comments varchar(250) null

AS

INSERT INTO dbo.InsureesEnrollmentVerificationDetails
(
	CreatedOn
	, CreatedBy
	, AccountNumber
	, IsVerified
	, VerificationDate
	, Comments
)
VALUES
(
	GETDATE()
	, USER
	, @AccountNumber
	, @IsVerified
	, @VerificationDate
	, @Comments
)

SELECT Id = MAX(Id) FROM dbo.InsureesEnrollmentVerificationDetails