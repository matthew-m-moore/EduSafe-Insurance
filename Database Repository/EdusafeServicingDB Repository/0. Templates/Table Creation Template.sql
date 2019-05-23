IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '')
	BEGIN

		CREATE TABLE 
		(
			Id int IDENTITY(1,1)
			, 
			, CreatedOn datetime null
			, CreatedBy varchar(25) null
			CONSTRAINT PK_ PRIMARY KEY (Id)
		)
	
	END