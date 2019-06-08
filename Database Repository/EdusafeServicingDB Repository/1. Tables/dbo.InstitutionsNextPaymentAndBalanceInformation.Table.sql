SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InstitutionsNextPaymentAndBalanceInformation')
	BEGIN

		CREATE TABLE dbo.InstitutionsNextPaymentAndBalanceInformation
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, InstitutionsAccountNumber bigint not null
			, NextPaymentAmount float not null
			, NextPaymentDate datetime not null
			, CurrentBalance float not null
			, NextPaymentStatusTypeId int not null
			CONSTRAINT PK_InstitutionsNextPaymentAndBalanceInformation_Id PRIMARY KEY CLUSTERED (Id)
		)

	END