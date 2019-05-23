-- ==========================================================================================
-- Create Database Role template for Azure SQL Database and Azure SQL Data Warehouse Database
-- ==========================================================================================
-- Create the database role
CREATE ROLE WebAppRole AUTHORIZATION [dbo]
GO

-- Grant access rights to a specific schema in the database
GRANT 
	EXECUTE, 
	INSERT, 
	REFERENCES, 
	SELECT, 
	VIEW DEFINITION 
ON SCHEMA::dbo
	TO WebAppUser
GO


 
