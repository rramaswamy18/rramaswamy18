--Export from Database to file
DECLARE @ServerName VARCHAR(500) = @@SERVERNAME
SELECT 'bcp [' + TABLE_CATALOG + '].[' + TABLE_SCHEMA + '].[' + TABLE_NAME + '] out Data\' + TABLE_SCHEMA + '_' + TABLE_NAME + '.dat -T -S ' + @ServerName + ' -n > Output\' + TABLE_SCHEMA + '_' + TABLE_NAME + '.txt'
FROM INFORMATION_SCHEMA.TABLES
--WHERE TABLE_SCHEMA NOT IN('Training')
ORDER BY TABLE_SCHEMA, TABLE_NAME
