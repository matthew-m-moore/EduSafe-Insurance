IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertOptionType' and TYPE = 'P') 
BEGIN 
	DROP PROCEDURE SP_InsertOptionType
END 
GO

CREATE PROCEDURE SP_InsertOptionType
	@OptionType varchar(25)
	, @Description varchar(250)

AS

INSERT INTO dbo.OptionType
(			
	CreatedOn
	, CreatedBy
	, OptionType
	, Description
)
VALUES
(		
	GETDATE()
	, USER
	, @OptionType
	, @Description
)

SELECT Id = MAX(Id) FROM dbo.OptionType