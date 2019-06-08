IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertInsureesAccountData' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE cust.SP_InsertInsureesAccountData
END 
GO

CREATE PROCEDURE cust.SP_InsertInsureesAccountData 
			@FolderPath varchar(250) null
			, @FirstName varchar(25) 
			, @MiddleName varchar(25) null
			, @LastName varchar(25) 
			, @Emails varchar(250) 
			, @SSN bigint 
			, @Birthdate datetime 
			, @Address1 varchar(50) 
			, @Address2 varchar(50) null
			, @Address3 varchar(50) null
			, @City varchar(50) 
			, @State varchar(2) 
			, @Zipcode varchar(5) 
			, @IsInsuredViaInstitution bit 

AS

INSERT INTO cust.InsureesAccountData
(
			CreatedOn
			, CreatedBy
			, FolderPath
			, FirstName
			, MiddleName
			, LastName
			, Emails
			, SSN
			, Birthdate
			, Address1
			, Address2
			, Address3
			, City
			, State
			, Zipcode
			, IsInsuredViaInstitution
)
VALUES
(
			GETDATE()
			, USER
			, @FolderPath
			, @FirstName
			, @MiddleName
			, @LastName
			, @Emails
			, @SSN
			, @Birthdate
			, @Address1
			, @Address2
			, @Address3
			, @City
			, @State
			, @Zipcode
			, @IsInsuredViaInstitution
	
)

SELECT AccountNumber = MAX(AccountNumber) FROM cust.InsureesAccountData