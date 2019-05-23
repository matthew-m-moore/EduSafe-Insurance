
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromIpAddressToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromIpAddressToAnswersToQuestions
		FOREIGN KEY (IPAddressId) REFERENCES WebSiteInquiryIpAddress(ID)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeNameToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromCollegeNameToAnswersToQuestions
		FOREIGN KEY (CollegeNameId) REFERENCES WebSiteInquiryCollegeName(ID)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeTypeToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromCollegeTypeToAnswersToQuestions
		FOREIGN KEY (CollegeTypeId) REFERENCES WebSiteInquiryCollegeType(ID)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromMajorToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromMajorToAnswersToQuestions
		FOREIGN KEY (MajorId) REFERENCES WebSiteInquiryMajor(ID)
END