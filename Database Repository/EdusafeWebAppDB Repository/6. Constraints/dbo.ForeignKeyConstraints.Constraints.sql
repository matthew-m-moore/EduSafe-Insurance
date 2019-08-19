
IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromIpAddressToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromIpAddressToAnswersToQuestions
		FOREIGN KEY (IpAddressId) REFERENCES WebSiteInquiryIpAddress(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeNameToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromCollegeNameToAnswersToQuestions
		FOREIGN KEY (CollegeNameId) REFERENCES WebSiteInquiryCollegeName(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeTypeToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromCollegeTypeToAnswersToQuestions
		FOREIGN KEY (CollegeTypeId) REFERENCES WebSiteInquiryCollegeType(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromMajorToAnswersToQuestions' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryAnswersToQuestions
		ADD CONSTRAINT FK_FromMajorToAnswersToQuestions
		FOREIGN KEY (MajorId) REFERENCES WebSiteInquiryMajor(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromIpAddressToInstitutionalInputs' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryInstitutionalInputs
		ADD CONSTRAINT FK_FromIpAddressToInstitutionalInputs
		FOREIGN KEY (IpAddressId) REFERENCES WebSiteInquiryIpAddress(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeNameToInstitutionalInputs' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryInstitutionalInputs
		ADD CONSTRAINT FK_FromCollegeNameToInstitutionalInputs
		FOREIGN KEY (CollegeNameId) REFERENCES WebSiteInquiryCollegeName(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromCollegeTypeToInstitutionalInputs' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryInstitutionalInputs
		ADD CONSTRAINT FK_FromCollegeTypeToInstitutionalInputs
		FOREIGN KEY (CollegeTypeId) REFERENCES WebSiteInquiryCollegeType(Id)
END

IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = 'FK_FromDegreeTypeToInstitutionalInputs' and type = 'FK') 
BEGIN
	ALTER TABLE WebSiteInquiryInstitutionalInputs
		ADD CONSTRAINT FK_FromDegreeTypeToInstitutionalInputs
		FOREIGN KEY (DegreeTypeId) REFERENCES WebSiteInquiryDegreeType(Id)
END