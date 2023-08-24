SELECT 'ALTER TABLE ' + '[' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(parent_object_id) + ']' + ' DROP CONSTRAINT ' + name AS DropConstraintSQL,
       'ALTER TABLE ' + '[' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(parent_object_id) + ']' + ' ADD CONSTRAINT [' + OBJECT_NAME(parent_object_id) + '_DF_' + COL_NAME(parent_object_id, parent_column_id) + '] DEFAULT ' + LEFT(RIGHT(definition, LEN(definition) - 1), LEN(definition) - 2) + ' FOR ' + COL_NAME(parent_object_id, parent_column_id) AS CreateConstraintSQL,
       SCHEMA_NAME(schema_id), OBJECT_NAME(parent_object_id), COL_NAME(parent_object_id, parent_column_id), definition, LEFT(RIGHT(definition, LEN(definition) - 1), LEN(definition) - 2), name
  FROM sys.default_constraints ORDER BY '[' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(parent_object_id) + ']'

SELECT 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] DROP CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_AddUserId]' AS DropConstraintSQL
      ,'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] ADD CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_AddUserId' + '] DEFAULT '''' FOR AddUserId' AS AddConstraintSQL
  FROM sys.objects WHERE type = 'U'
UNION
SELECT 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] DROP CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_AddUserName]' AS DropConstraintSQL
      ,'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] ADD CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_AddUserName' + '] DEFAULT SUSER_NAME() FOR AddUserName' AS AddConstraintSQL
  FROM sys.objects WHERE type = 'U'
UNION
SELECT 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] DROP CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_AddDateTime]' AS DropConstraintSQL
      ,'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] ADD CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_AddDateTime' + '] DEFAULT SYSDATETIME() FOR AddDateTime' AS AddConstraintSQL
  FROM sys.objects WHERE type = 'U'
UNION
SELECT 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] DROP CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_UpdUserId]' AS DropConstraintSQL
      ,'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] ADD CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_UpdUserId' + '] DEFAULT '''' FOR UpdUserId' AS UpdConstraintSQL
  FROM sys.objects WHERE type = 'U'
UNION
SELECT 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] DROP CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_UpdUserName]' AS DropConstraintSQL
      ,'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] ADD CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_UpdUserName' + '] DEFAULT SUSER_NAME() FOR UpdUserName' AS UpdConstraintSQL
  FROM sys.objects WHERE type = 'U'
UNION
SELECT 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] DROP CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_UpdDateTime]' AS DropConstraintSQL
      ,'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(object_id) + '] ADD CONSTRAINT [' + OBJECT_NAME(object_id) + '_DF_UpdDateTime' + '] DEFAULT SYSDATETIME() FOR UpdDateTime' AS UpdConstraintSQL
  FROM sys.objects WHERE type = 'U'
ORDER BY 1
