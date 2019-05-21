IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesPaymentHistory')
	BEGIN

		CREATE TABLE dbo.InsureesPaymentHistory
		(
			Id int IDENTITY(1,1)
			, AccountNumber bigint not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, PaymentAmount decimal not null
			, PaymentDate datetime not null
			CONSTRAINT PK_InsureesPaymentHistory_Id PRIMARY KEY (Id)
		)
	
	END