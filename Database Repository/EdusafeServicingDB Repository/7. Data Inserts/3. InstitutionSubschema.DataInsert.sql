EXEC SP_InsertInstitutionsAccountData null, 'AnyCollegeUSA', 'AnyTownUSA', null, null, 'AnyCityUSA', 'CA', '99999'
GO

DECLARE @MaxEmailId int
SET @MaxEmailId = (SELECT MAX(SetId) FROM cust.EmailsSet)

EXEC cust.SP_InsertEmails @MaxEmailId, 'someone@anycollegeusa.com', 1
GO

-------------------------- insert into InstitutionsInsureeList
DECLARE @InstitutionsAccountNumber bigint
SET @InstitutionsAccountNumber = (SELECT MAX(InstitutionsAccountNumber) FROM InstitutionsAccountData WHERE InstitutionName = 'AnyCollegeUSA')

DECLARE @InsureeAccountNumberFirst bigint
SET @InsureeAccountNumberFirst = (SELECT MIN(AccountNumber) FROM cust.InsureesAccountData)

DECLARE @InsureeAccountNumberLast bigint
SET @InsureeAccountNumberLast = (SELECT MAX(AccountNumber) FROM cust.InsureesAccountData)

DECLARE @Count int = 1;
DECLARE @Target int = (@InsureeAccountNumberLast - @InsureeAccountNumberFirst) + 2;
WHILE @Count < @Target
	BEGIN
		EXEC SP_InsertInstitutionsInsureeList @InstitutionsAccountNumber, @InsureeAccountNumberFirst
		SET @Count = @Count + 1
		SET @InsureeAccountNumberFirst = @InsureeAccountNumberFirst + 1;
	END;
GO

EXEC SP_InsertInstitutionsNextPaymentAndBalanceInformation 500000,542.241,'7/31/2019',542.241,1

EXEC SP_InsertInstitutionsNotificationHistoryEntry 500000,5,'2019-07-01'

EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1365,'2018-01-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1296.75,'2018-02-28',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1231.92,'2018-03-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1170.33,'2018-04-30',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1111.82,'2018-05-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1056.23,'2018-06-30',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,1003.42,'2018-07-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,953.25,'2018-08-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,905.59,'2018-09-30',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,860.32,'2018-10-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,817.31,'2018-11-30',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,776.45,'2018-12-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,737.63,'2019-01-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,700.75,'2019-02-28',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,665.72,'2019-03-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,632.44,'2019-04-30',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,600.82,'2019-05-31',3,'On time'
EXEC SP_InsertInstitutionsPaymentHistoryEntry 500000,570.78,'2019-06-30',3,'On time'
