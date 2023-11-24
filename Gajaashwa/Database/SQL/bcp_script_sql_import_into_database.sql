--Import from file to database
DECLARE @ServerName VARCHAR(500) = @@SERVERNAME
SELECT 'bcp [' + TABLE_CATALOG + '].[' + TABLE_SCHEMA + '].[' + TABLE_NAME + '] in Data\' + TABLE_SCHEMA + '_' + TABLE_NAME + '.dat -T -S ' + @ServerName + ' -n > Input\' + TABLE_SCHEMA + '_' + TABLE_NAME + '.txt'
FROM INFORMATION_SCHEMA.TABLES
--WHERE TABLE_SCHEMA = 'ArchLib'
ORDER BY TABLE_SCHEMA, TABLE_NAME
