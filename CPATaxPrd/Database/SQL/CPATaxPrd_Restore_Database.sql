USE [master]
GO
RESTORE DATABASE [CPATaxPrd] FROM DISK = 'C:\Dev\Database\Backup\CPATaxPrd.BAK' WITH REPLACE,
    MOVE 'CPATaxPrd_Data' TO 'C:\Dev\Database\Files\CPATaxPrd_Data.MDF',
    MOVE 'CPATaxPrd_Log' TO 'C:\Dev\Database\Files\CPATaxPrd_Log.LDF'
GO
