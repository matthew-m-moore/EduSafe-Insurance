SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'NotificationType')
	BEGIN

		CREATE TABLE dbo.NotificationType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, NotificationType varchar(50) not null
			, Description varchar(250) null
			CONSTRAINT PK_NotificationType_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END

INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailWelcome', 'Welcome email to customer to let them know they are being evaluated for coverage')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailAccepted', 'Acceptance email to customer to tell them they are accepted for coverage')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailPleaseReapply', 'Denial of coverage email to customer inviting them to re-apply should circumstances leading to their denial change')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailPolicyDetails', 'Email including a copy of policy details for customer review')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailBilling', 'Email of next payment invoice for customer to remind them of billing cycle')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailDelinquency', 'Delinquency warning email detailing past due balance of customer')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailPolicyPendingCancellation', 'Final warning email detailing past due balance and date when policy or policies will be cancelled unless payment is received')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailPolicyCancelled', 'Confirmation email that policy or polices have been cancelled')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailClaimInstructions', 'Detailed instructions email to customer on how to submit various claims')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailClaimAccepted', 'Acceptance email to customer of their submitted claim or claims')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'EmailClaimDenied', 'Denial of claim email to customer with details of denial reason')

INSERT INTO NotificationType VALUES(GETDATE(), USER, 'LetterPolicyDetails', 'Paper mail copy of policy details and policy contract')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'LetterPolicyPendingCancellation', 'Paper mail copy of final warning detailing past due balance and date when policy or policies will be cancelled unless payment is received')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'LetterPolicyCancelled', 'Paper mail copy of confirmation that policy or polices have been cancelled')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'LetterClaimInstructions', 'Detailed printed paper instructions to customer on how to submit various claims')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'LetterClaimAccepted', 'Paper copy of acceptance to customer of their submitted claim or claims')
INSERT INTO NotificationType VALUES(GETDATE(), USER, 'LetterClaimDenied', 'Paper copy of denial of claim email to customer with details of denial reason')