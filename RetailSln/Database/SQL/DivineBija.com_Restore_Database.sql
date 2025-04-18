USE [master]
GO
RESTORE DATABASE [DivineBija.in] FROM DISK = 'C:\Dev\Database\Backup\DivineBija.in.BAK' WITH REPLACE,
    MOVE 'DivineBija.in_Data' TO 'C:\Dev\Database\Files\DivineBija.in_Data.MDF',
    MOVE 'DivineBija.in_Log' TO 'C:\Dev\Database\Files\DivineBija.in_Log.LDF'
GO
