EXEC SP_InsertClaimAccountEntry 10000000004
EXEC SP_InsertClaimAccountEntry 10000000005
EXEC SP_InsertClaimAccountEntry 10000000009
EXEC SP_InsertClaimAccountEntry 10000000010
EXEC SP_InsertClaimAccountEntry 10000000013
EXEC SP_InsertClaimAccountEntry 10000000014
EXEC SP_InsertClaimAccountEntry 10000000015
EXEC SP_InsertClaimAccountEntry 10000000019
EXEC SP_InsertClaimAccountEntry 10000000020
EXEC SP_InsertClaimAccountEntry 10000000004

EXEC SP_InsertClaimDocumentEntry 10000000,'Resume1','pdf',1,0,'2019-05-14',null
EXEC SP_InsertClaimDocumentEntry 10000001,'Grad School Doc','pdf',5,1,'2018-11-28',null
EXEC SP_InsertClaimDocumentEntry 10000002,'Termination Doc','pdf',2,0,'2019-01-28',null
EXEC SP_InsertClaimDocumentEntry 10000003,'Early Hire Doc','pdf',3,0,'2019-03-22',null
EXEC SP_InsertClaimDocumentEntry 10000004,'College Closure Doc','pdf',5,1,'2019-02-18',null
EXEC SP_InsertClaimDocumentEntry 10000005,'Resume1','pdf',5,1,'2019-04-28',null
EXEC SP_InsertClaimDocumentEntry 10000006,'Grad School Doc','pdf',6,0,'2019-01-28',null
EXEC SP_InsertClaimDocumentEntry 10000007,'Termination Doc','pdf',2,0,'2019-03-22',null
EXEC SP_InsertClaimDocumentEntry 10000008,'Early Hire Doc','pdf',2,0,'2019-02-18',null
EXEC SP_InsertClaimDocumentEntry 10000009,'Grad School Doc','pdf',5,1,'2019-06-30',null

EXEC SP_InsertClaimOptionEntry 10000000,1,1
EXEC SP_InsertClaimOptionEntry 10000001,2,0.5
EXEC SP_InsertClaimOptionEntry 10000002,3,0.5
EXEC SP_InsertClaimOptionEntry 10000003,4,0.5
EXEC SP_InsertClaimOptionEntry 10000004,5,0.5
EXEC SP_InsertClaimOptionEntry 10000005,1,1
EXEC SP_InsertClaimOptionEntry 10000006,2,0.5
EXEC SP_InsertClaimOptionEntry 10000007,3,0.5
EXEC SP_InsertClaimOptionEntry 10000008,4,0.5
EXEC SP_InsertClaimOptionEntry 10000009,4,0.5

EXEC SP_InsertClaimStatusEntry 10000000,1,0
EXEC SP_InsertClaimStatusEntry 10000001,2,1
EXEC SP_InsertClaimStatusEntry 10000002,3,0
EXEC SP_InsertClaimStatusEntry 10000003,4,0
EXEC SP_InsertClaimStatusEntry 10000004,5,1
EXEC SP_InsertClaimStatusEntry 10000005,1,1
EXEC SP_InsertClaimStatusEntry 10000006,2,0
EXEC SP_InsertClaimStatusEntry 10000007,3,0
EXEC SP_InsertClaimStatusEntry 10000008,4,0

EXEC SP_InsertClaimPaymentEntry 10000001,5000,'2018-12-28',3,NULL
EXEC SP_InsertClaimPaymentEntry 10000004,5000,'2019-03-18',3,NULL
EXEC SP_InsertClaimPaymentEntry 10000005,10000,'2019-05-28',3,NULL
EXEC SP_InsertClaimPaymentEntry 10000009,5000,'2019-05-28',3,NULL
