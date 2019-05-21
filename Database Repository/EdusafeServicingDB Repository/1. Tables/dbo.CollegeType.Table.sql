IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeType')
	BEGIN

		CREATE TABLE dbo.CollegeType
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, CollegeType varchar(25) not null
			CONSTRAINT PK_CollegeType_Id PRIMARY KEY (Id)
		)
	
	END

INSERT INTO CollegeType VALUES(GETDATE(), 'Sharon Paesachov', 'Public')
INSERT INTO CollegeType VALUES(GETDATE(), 'Sharon Paesachov', 'Private')
INSERT INTO CollegeType VALUES(GETDATE(), 'Sharon Paesachov', 'ForProfit')