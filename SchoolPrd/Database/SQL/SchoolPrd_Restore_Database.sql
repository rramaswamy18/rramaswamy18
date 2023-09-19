USE [master]
GO
RESTORE DATABASE SchoolPrd FROM DISK = 'C:\Dev\Database\Backup\SchoolPrd.BAK' WITH REPLACE,
    MOVE 'SchoolPrd_Data' TO 'C:\Dev\Database\Files\SchoolPrd_Data.MDF',
    MOVE 'SchoolPrd_Log' TO 'C:\Dev\Database\Files\SchoolPrd_Log.LDF'
GO
