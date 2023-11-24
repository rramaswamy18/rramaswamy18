USE [master]
GO
RESTORE DATABASE Gajaashwa FROM DISK = 'C:\Dev\Database\Backup\Gajaashwa.BAK' WITH REPLACE,
    MOVE 'Gajaashwa_Data' TO 'C:\Dev\Database\Files\Gajaashwa_Data.MDF',
    MOVE 'Gajaashwa_Log' TO 'C:\Dev\Database\Files\Gajaashwa_Log.LDF'
GO
