SELECT 
	'IF EXISTS (SELECT * FROM sys.objects WHERE Type = ''F'' and Name = ''' 
	+ NAME 
	+ '''' 
	+ ' ) BEGIN ALTER TABLE ' 
	+ OBJECT_NAME(Parent_object_id) 
	+ ' DROP CONSTRAINT ' 
	+ NAME 
	+ ' END'
FROM sys.objects WHERE Type = 'F'


SELECT 
	'IF EXISTS (SELECT * FROM sys.objects WHERE Type = ''P'' and Name = ''' 
	+ NAME 
	+ '''' 
	+ ' ) BEGIN DROP PROCEDURE ' 
	+ NAME
	+ ' END ' ,*
FROM sys.objects WHERE Type = 'P'

