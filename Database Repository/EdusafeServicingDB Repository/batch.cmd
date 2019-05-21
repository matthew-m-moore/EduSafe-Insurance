sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\cust.InsureesAccountData.Table.sql" >> CreateTable.txt

sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.CollegeAcademicTermType.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.CollegeDetail.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.CollegeMajor.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.CollegeType.Table.sql" >> CreateTable.txt

sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.InsureesAcademicHistory.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.InsureesNextPaymentAndBalanceInformation.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.InsureesPaymentHistory.Table.sql" >> CreateTable.txt
sqlcmd -S edusafe.database.windows.net -d EdusafeServicingDB -U EduSafeAdmin -P Master123 -i "1. Tables\dbo.InsureesPremiumCalculationDetails.Table.sql" >> CreateTable.txt

pause
