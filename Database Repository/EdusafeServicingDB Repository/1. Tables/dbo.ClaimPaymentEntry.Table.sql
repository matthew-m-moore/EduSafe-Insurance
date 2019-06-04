SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimPaymentEntry')
	BEGIN

		CREATE TABLE dbo.ClaimPaymentEntry
		(
			ClaimPaymentNumber bigint IDENTITY(10000000,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, ClaimNumber bigint not null
			, ClaimPaymentAmount float not null
			, ClaimPaymentDate datetime not null
			, ClaimPaymentStatusTypeId int not null
			, ClaimPaymentComments varchar(250) null
			CONSTRAINT PK_ClaimPaymentEntry PRIMARY KEY CLUSTERED (ClaimPaymentNumber)
		)
	
	END