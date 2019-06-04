SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsPaymentHistoryEntry')
	BEGIN

		CREATE TABLE dbo.InstitutionsPaymentHistoryEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, InstitutionsAccountNumber bigint not null
			, PaymentAmount decimal not null
			, PaymentDate datetime not null
			, PaymentComments varchar(250) null
			CONSTRAINT PK_InstitutionsPaymentHistoryEntry_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END