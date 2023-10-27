USE [master]
GO
RESTORE DATABASE [PrjctMgmt] FROM DISK = 'C:\Dev\Database\Backup\PrjctMgmt.BAK' WITH REPLACE,
    MOVE 'PrjctMgmt_Data' TO 'C:\Dev\Database\Files\PrjctMgmt_Data.MDF',
    MOVE 'PrjctMgmt_Log' TO 'C:\Dev\Database\Files\PrjctMgmt_Log.LDF'
GO
