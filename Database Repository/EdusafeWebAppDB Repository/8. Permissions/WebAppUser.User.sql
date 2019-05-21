IF EXISTS (SELECT * FROM sys.sysusers WHERE NAME = 'WebAppUser') BEGIN DROP USER WebAppUser END

CREATE USER [WebAppUser]
	FOR LOGIN [WebAppUser]

GO


-- Add an existing user to the role
EXEC sp_addrolemember 'WebAppRole','WebAppUser'
GO
