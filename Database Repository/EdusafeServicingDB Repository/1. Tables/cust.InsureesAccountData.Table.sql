IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'InsureesAccountData')
	BEGIN

		CREATE TABLE cust.InsureesAccountData
		(
			AccountNumber bigint IDENTITY(10000000000,1)
			, CreatedOn datetime not null
			, CreatedBy varchar(25) not null
			, FirstName varchar(25) not null
			, MiddleName varchar(25) null
			, LastName varchar(25) not null
			, EmailAddress varchar(50) not null
			, SSN bigint not null
			, Birthdate datetime not null
			-- address information
			, Address1 varchar(50) not null
			, Address2 varchar(50) null
			, Address3 varchar(50) null
			, City varchar(50) not null
			, State varchar(2) not null
			, Zipcode varchar(5) not null
			, isInsuredViaInstitution bit not null
			CONSTRAINT PK_InsureesAccountData_AcccountNumber PRIMARY KEY (AccountNumber)
		)
	
	END