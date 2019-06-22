SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPaymentHistoryEntry')
	BEGIN

		CREATE TABLE dbo.InsureesPaymentHistoryEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, PaymentAmount float not null
			, PaymentDate datetime not null
			, PaymentStatusTypeId int not null
			, PaymentComments varchar(250) null
			CONSTRAINT PK_InsureesPaymentHistoryEntry_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END