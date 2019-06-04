SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesGraduationVerificationDetails')
	BEGIN

		CREATE TABLE dbo.InsureesGraduationVerificationDetails
		(
			Id int IDENTITY(1,1) 
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, AccountNumber bigint not null
			, IsVerified bit not null
			, VerificationDate datetime null
			CONSTRAINT PK_InsureesGraduationVerificationDetails_Id PRIMARY KEY CLUSTERED (Id)
		)
	
	END