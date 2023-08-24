USE [master]
GO
RESTORE DATABASE ArchitectureLibrary FROM DISK = 'C:\Dev\Database\Backup\ArchitectureLibrary.BAK' WITH REPLACE,
    MOVE 'ArchitectureLibrary_Data' TO 'C:\Dev\Database\Files\ArchitectureLibrary_Data.MDF',
    MOVE 'ArchitectureLibrary_Log' TO 'C:\Dev\Database\Files\ArchitectureLibrary_Log.LDF'
GO
