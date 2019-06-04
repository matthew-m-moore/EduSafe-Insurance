SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesEnrollmentVerificationDetails')
	BEGIN

		CREATE TABLE dbo.InsureesEnrollmentVerificationDetails
		(
			Id int IDENTITY(1,1) 
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, IsVerified bit not null
			, VerificationDate datetime null
			, Comments varchar(250) null
			CONSTRAINT PK_InsureesEnrollmentVerificationDetails_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END