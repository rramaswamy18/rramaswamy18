--ArchLib_CodeType_CodeData_Split_1.sql
		SELECT 'DROP TABLE ' + DatabaseSchemaName + '.' + CodeTypeNameDesc
		  FROM Lookup.CodeType INNER JOIN Lookup.CodeTypeEnumeration ON CodeType.CodeTypeId = CodeTypeEnumeration.CodeTypeId
	  ORDER BY DatabaseSchemaName, CodeTypeNameDesc
----
		SELECT 'CREATE TABLE ' + DatabaseSchemaName + '.' + CodeTypeNameDesc + '(' + CodeTypeNameDesc + 'Id BIGINT NOT NULL IDENTITY(0, 100), ' + CodeTypeNameDesc + 'NameDesc VARCHAR(100) NOT NULL, '
		       + CodeTypeNameDesc + 'Desc0 NVARCHAR(1024) NOT NULL, ' + CodeTypeNameDesc + 'Desc1 NVARCHAR(1024) NOT NULL, ' + CodeTypeNameDesc + 'Desc2 NVARCHAR(1024) NOT NULL'
			   + ', AddUserId NVARCHAR(128) NOT NULL CONSTRAINT ' + CodeTypeNameDesc + '_Default_AddUserId DEFAULT '''''
			   + ', AddUserName NVARCHAR(128) NOT NULL CONSTRAINT ' + CodeTypeNameDesc + '_Default_AddUserName DEFAULT SUSER_NAME()'
			   + ', AddDateTime DATETIME2 NOT NULL CONSTRAINT ' + CodeTypeNameDesc + '_Default_AddDateTime DEFAULT SYSDATETIME()'
			   + ', UpdUserId NVARCHAR(128) NOT NULL CONSTRAINT ' + CodeTypeNameDesc + '_Default_UpAddUserId DEFAULT '''''
			   + ', UpdUserName NVARCHAR(128) NOT NULL CONSTRAINT ' + CodeTypeNameDesc + '_Default_UpdUserName DEFAULT SUSER_NAME()'
			   + ', UpdDateTime DATETIME2 NOT NULL CONSTRAINT ' + CodeTypeNameDesc + '_Default_UpdDateTime DEFAULT SYSDATETIME()'
			   + ', CONSTRAINT ' + CodeTypeNameDesc + '_PK PRIMARY KEY CLUSTERED(' + CodeTypeNameDesc + 'Id ASC)'
			   + ', CONSTRAINT ' + CodeTypeNameDesc + '_IX0 UNIQUE NONCLUSTERED(' + CodeTypeNameDesc + 'NameDesc ASC)'
			   + ')'
		  FROM Lookup.CodeType INNER JOIN Lookup.CodeTypeEnumeration ON CodeType.CodeTypeId = CodeTypeEnumeration.CodeTypeId
	  ORDER BY DatabaseSchemaName, CodeTypeNameDesc
----
		SELECT
		        'SET IDENTITY_INSERT ' + DatabaseSchemaName + '.' + CodeTypeNameDesc + ' ON'
			  + CHAR(10)
		      + 'INSERT ' + DatabaseSchemaName + '.' + CodeTypeNameDesc + '('
		      + CodeTypeNameDesc + 'Id, '
		      + CodeTypeNameDesc + 'NameDesc, ' + CodeTypeNameDesc + 'Desc0, ' + CodeTypeNameDesc + 'Desc1, ' + CodeTypeNameDesc + 'Desc2'
			  +')'
			  +'SELECT CodeDataNameId, CodeDataNameDesc, CodeDataDesc0, CodeDataDesc1, CodeDataDesc2 FROM Lookup.CodeData WHERE CodeTypeId IN(SELECT CodeTypeId FROM Lookup.CodeType WHERE CodeTypeNameDesc = ''' + CodeTypeNameDesc + ''')'
			  + CHAR(10)
		      + 'SET IDENTITY_INSERT ' + DatabaseSchemaName + '.' + CodeTypeNameDesc + ' OFF'
		  FROM Lookup.CodeType INNER JOIN Lookup.CodeTypeEnumeration ON CodeType.CodeTypeId = CodeTypeEnumeration.CodeTypeId
	  ORDER BY DatabaseSchemaName, CodeTypeNameDesc
