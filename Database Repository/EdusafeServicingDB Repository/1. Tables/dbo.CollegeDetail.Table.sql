IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CollegeDetail')
	BEGIN

		CREATE TABLE dbo.CollegeDetail 
		(
			Id int IDENTITY(1,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, CollegeName varchar(50) not null
			, CollegeTypeId int not null
			, CollegeAcademicTermTypeId int not null
			CONSTRAINT PK_CollegeDetail_Id PRIMARY KEY (Id)
		)
	
	END