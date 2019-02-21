
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.WebSiteInquiryAnswersToQuestions.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.WebSiteInquiryCollegeName.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.WebSiteInquiryCollegeType.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.WebSiteInquiryEmailAddress.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.WebSiteInquiryIpAddress.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.WebSiteInquiryMajor.Table.sql" >> CreateTable.txt

sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "2. Stored Procedures\dbo.SP_InsertWebSiteInquiryAnswersToQuestions.StoredProcedure.sql" >> CreateProc.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "2. Stored Procedures\dbo.SP_InsertWebSiteInquiryCollegeName.StoredProcedure.sql" >> CreateProc.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "2. Stored Procedures\dbo.SP_InsertWebSiteInquiryCollegeType.StoredProcedure.sql" >> CreateProc.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "2. Stored Procedures\dbo.SP_InsertWebSiteInquiryEmailAddress.StoredProcedure.sql" >> CreateProc.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "2. Stored Procedures\dbo.SP_InsertWebSiteInquiryIpAddress.StoredProcedure.sql" >> CreateProc.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "2. Stored Procedures\dbo.SP_InsertWebSiteInquiryMajor.StoredProcedure.sql" >> CreateProc.txt

sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "6. Constraints\dbo.ForeignKeyConstraints.Constraints.sql" >> CreateConstraints.txt
sqlcmd -S edusafe.database.windows.net -d EduSafeDB -U EduSafeAdmin -P Master123 -i "6. Constraints\dbo.UniqueConstraints.Constraints.sql" >> CreateConstraints.txt


pause