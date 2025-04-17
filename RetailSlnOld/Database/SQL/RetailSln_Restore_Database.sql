USE [master]
GO
RESTORE DATABASE RetailSln FROM DISK = 'C:\Dev\Database\Backup\RetailSln.BAK' WITH REPLACE,
    MOVE 'RetailSln_Data' TO 'C:\Dev\Database\Files\RetailSln_Data.MDF',
    MOVE 'RetailSln_Log' TO 'C:\Dev\Database\Files\RetailSln_Log.LDF'
GO
