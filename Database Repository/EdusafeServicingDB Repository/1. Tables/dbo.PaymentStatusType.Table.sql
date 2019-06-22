SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PaymentStatusType')
	BEGIN

		CREATE TABLE dbo.PaymentStatusType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, PaymentStatusType varchar(25) not null
			, Description varchar(250) not null
			CONSTRAINT PK_PaymentStatusType PRIMARY KEY CLUSTERED (Id)
		)
	
	END

INSERT INTO PaymentStatusType VALUES(GETDATE(), USER, 'Billed', 'Payment has been billed and customer notified accordingly')	
INSERT INTO PaymentStatusType VALUES(GETDATE(), USER, 'Pending', 'Payment is pending, information received and awaiting processing')
INSERT INTO PaymentStatusType VALUES(GETDATE(), USER, 'Processed', 'Payment has been processed and funds have been successfully transferred')
INSERT INTO PaymentStatusType VALUES(GETDATE(), USER, 'Declined', 'Payment was declined and not processed, funds were not transferred')
INSERT INTO PaymentStatusType VALUES(GETDATE(), USER, 'Reversed', 'Payment was reversed, funds have been returned to original source')