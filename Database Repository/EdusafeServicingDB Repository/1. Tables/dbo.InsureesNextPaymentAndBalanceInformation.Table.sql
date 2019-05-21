IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesNextPaymentAndBalanceInformation')
	BEGIN

		CREATE TABLE dbo.InsureesNextPaymentAndBalanceInformation
		(
			Id int IDENTITY(1,1)
			, AccountNumber bigint not null
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, NextPaymentAmount decimal not null
			, NextPaymentDate datetime not null
			, CurrentBalance decimal not null
			CONSTRAINT PK_InsureesNextPaymentAndBalanceInformation_Id PRIMARY KEY (Id)
		)
	
	END