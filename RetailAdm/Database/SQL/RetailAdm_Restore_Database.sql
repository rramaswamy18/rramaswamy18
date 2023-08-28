USE [master]
GO
RESTORE DATABASE RetailAdm FROM DISK = 'C:\Dev\Database\Backup\RetailAdm.BAK' WITH REPLACE,
    MOVE 'RetailAdm_Data' TO 'C:\Dev\Database\Files\RetailAdm_Data.MDF',
    MOVE 'RetailAdm_Log' TO 'C:\Dev\Database\Files\RetailAdm_Log.LDF'
GO
