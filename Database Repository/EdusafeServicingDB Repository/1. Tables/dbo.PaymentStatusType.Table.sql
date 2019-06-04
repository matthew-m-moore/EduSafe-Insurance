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
			, PaymentStatusType varchar(50) not null
			CONSTRAINT PK_PaymentStatusType PRIMARY KEY CLUSTERED (Id)
		)
	
	END