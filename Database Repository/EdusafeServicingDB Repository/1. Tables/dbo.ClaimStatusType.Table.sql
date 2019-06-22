SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusType')
	BEGIN

		CREATE TABLE dbo.ClaimStatusType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, ClaimStatusType varchar(25) not null
			, Description varchar(250) null
			CONSTRAINT PK_ClaimStatusType PRIMARY KEY CLUSTERED (Id)
		)
	
	END
	
INSERT INTO ClaimStatusType VALUES(GETDATE(), USER, 'Received', 'Claim was recieved, no further action has occured')
INSERT INTO ClaimStatusType VALUES(GETDATE(), USER, 'Pending', 'Claim is pending validation, it is in queue to begin validating')
INSERT INTO ClaimStatusType VALUES(GETDATE(), USER, 'Validating', 'Claim is in the process of being validated')
INSERT INTO ClaimStatusType VALUES(GETDATE(), USER, 'Accepted', 'Claim has passed validation and is accepted')
INSERT INTO ClaimStatusType VALUES(GETDATE(), USER, 'Denied', 'Claim has not passed validation and is denied')