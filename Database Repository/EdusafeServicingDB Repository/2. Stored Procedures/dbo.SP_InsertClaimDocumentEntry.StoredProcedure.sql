IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertClaimDocumentEntry' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertClaimDocumentEntry
END 
GO

CREATE PROCEDURE SP_InsertClaimDocumentEntry
	@ClaimNumber bigint
	, @FileName varchar(50)
	, @FileType varchar(25)
	, @FileVerificationStatusTypeId int
	, @IsVerified bit
	, @UploadDate datetime
	, @ExpirationDate datetime null

AS

INSERT INTO dbo.ClaimDocumentEntry
(			
	CreatedOn
	, CreatedBy
	, ClaimNumber
	, FileName
	, FileType
	, FileVerificationStatusTypeId
	, IsVerified
	, UploadDate
	, ExpirationDate
)
VALUES
(		
	GETDATE()
	, USER
	, @ClaimNumber
	, @FileName
	, @FileType
	, @FileVerificationStatusTypeId
	, @IsVerified
	, @UploadDate
	, @ExpirationDate
)

SELECT Id = MAX(Id) FROM dbo.ClaimDocumentEntry