--EXEC SP_InsertCollegeDetail	'AnyCollegeUSA', 'ForProfit', 'Semester'

--EXEC SP_InsertCollegeMajor 'GetRichQuick'

--EXEC SP_InsertInstitutionsAccountData null, 'AnyCollegeUSA', 'AnyTownUSA', null, null, 'AnyCityUSA', 'CA', '99999'
--EXEC cust.SP_InsertEmails 24, 'someone@anycollegeusa.com'

---------------------------- insert into InstitutionsInsureeList
--DECLARE @InstitutionsAccountNumber bigint
--SET @InstitutionsAccountNumber = (SELECT MAX(InstitutionsAccountNumber) FROM InstitutionsAccountData WHERE InstitutionName = 'AnyCollegeUSA')

--DECLARE @InsureeAccountNumberFirst bigint
--SET @InsureeAccountNumberFirst = (SELECT MIN(AccountNumber) FROM cust.InsureesAccountData)

--DECLARE @InsureeAccountNumberLast bigint
--SET @InsureeAccountNumberLast = (SELECT MAX(AccountNumber) FROM cust.InsureesAccountData)

--DECLARE @Count int = 1;
--DECLARE @Target int = (@InsureeAccountNumberLast - @InsureeAccountNumberFirst) + 2;
--WHILE @Count < @Target
--	BEGIN
--		EXEC SP_InsertInstitutionsInsureeList @InstitutionsAccountNumber, @InsureeAccountNumberFirst
--		SET @Count = @Count + 1
--		SET @InsureeAccountNumberFirst = @InsureeAccountNumberFirst + 1;
--	END;
