IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesGraduationVerificationDetails' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertInsureesGraduationVerificationDetails
END 
GO

CREATE PROCEDURE SP_InsertInsureesGraduationVerificationDetails
	@AccountNumber bigint 
	, @IsVerified bit 
	, @VerificationDate datetime null
	, @Comments varchar(250) null

AS

INSERT INTO dbo.InsureesGraduationVerificationDetails
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

SELECT Id = MAX(Id) FROM dbo.InsureesGraduationVerificationDetails