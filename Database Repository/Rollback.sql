USE [EduSafeDB]

-- Rollback Constraints
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeName' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryCollegeName
	DROP CONSTRAINT UC_CollegeName 
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_CollegeType' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryCollegeType
	DROP CONSTRAINT UC_CollegeType 
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_EmailAddress' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryEmailAddress
	DROP CONSTRAINT UC_EmailAddress 
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'UC_Major' and type = 'UQ') 
BEGIN
	ALTER TABLE WebSiteInquiryMajor
	DROP CONSTRAINT UC_Major
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromIpAddressToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		DROP CONSTRAINT FK_FromIpAddressToAnswersToQuestions
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeNameToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		DROP CONSTRAINT FK_FromCollegeNameToAnswersToQuestions
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeTypeToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		DROP CONSTRAINT FK_FromCollegeTypeToAnswersToQuestions
END

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromMajorToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		DROP CONSTRAINT FK_FromMajorToAnswersToQuestions
END




-- Rollback Stored Procedures
IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryAnswersToQuestions' and TYPE = 'SP')	BEGIN DROP PROCEDURE SP_InsertWebSiteInquiryAnswersToQuestions	END 
IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryCollegeName' and TYPE = 'SP')			BEGIN DROP PROCEDURE SP_InsertWebSiteInquiryCollegeName			END 
IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryCollegeType' and TYPE = 'SP')			BEGIN DROP PROCEDURE SP_InsertWebSiteInquiryCollegeType			END 
IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryEmailAddress' and TYPE = 'SP')		BEGIN DROP PROCEDURE SP_InsertWebSiteInquiryEmailAddress		END 
IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryIPAddress' and TYPE = 'SP')			BEGIN DROP PROCEDURE SP_InsertWebSiteInquiryIPAddress			END 
IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'SP_InsertWebSiteInquiryMajor' and TYPE = 'SP')				BEGIN DROP PROCEDURE SP_InsertWebSiteInquiryMajor				END 

-- Rollback Table Creation
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryAnswersToQuestions')	BEGIN DROP TABLE WebSiteInquiryAnswersToQuestions	END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryCollegeName')			BEGIN DROP TABLE WebSiteInquiryCollegeName			END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryCollegeType')			BEGIN DROP TABLE WebSiteInquiryCollegeType			END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryEmailAddress')			BEGIN DROP TABLE WebSiteInquiryEmailAddress			END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryIPAddress')			BEGIN DROP TABLE WebSiteInquiryIPAddress			END
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WebSiteInquiryMajor')				BEGIN DROP TABLE WebSiteInquiryMajor				END