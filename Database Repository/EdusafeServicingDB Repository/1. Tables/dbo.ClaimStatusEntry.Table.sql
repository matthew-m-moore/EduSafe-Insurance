SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClaimStatusEntry')
	BEGIN

		CREATE TABLE dbo.ClaimStatusEntry
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			, ClaimNumber bigint not null
			, ClaimStatusTypeId int not null
			, IsClaimApproved bit not null
			CONSTRAINT PK_ClaimStatusEntry PRIMARY KEY CLUSTERED (Id)
		)
	
	END