SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FileVerificationStatusType')
	BEGIN

		CREATE TABLE dbo.FileVerificationStatusType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, FileVerificationStatusType varchar(25)
			, Description varchar(250) null
			CONSTRAINT PK_FileVerificationStatusType PRIMARY KEY CLUSTERED (Id)
		)
	
	END
GO

INSERT INTO FileVerificationStatusType VALUES(GETDATE(), USER, 'Uploaded', 'File has been successfully upload, no further action has occurred')
INSERT INTO FileVerificationStatusType VALUES(GETDATE(), USER, 'Pending', 'File is pending verification, it is in queue to begin verification process')
INSERT INTO FileVerificationStatusType VALUES(GETDATE(), USER, 'Verifying', 'File is in the proces of being verified')
INSERT INTO FileVerificationStatusType VALUES(GETDATE(), USER, 'PartiallyVerified', 'File has been partially verified, but not fully verified')
INSERT INTO FileVerificationStatusType VALUES(GETDATE(), USER, 'FullyVerified', 'File is fully verified')
INSERT INTO FileVerificationStatusType VALUES(GETDATE(), USER, 'Rejected', 'File was rejected during verification')