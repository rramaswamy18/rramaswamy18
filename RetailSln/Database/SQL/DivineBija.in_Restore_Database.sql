USE [master]
GO
ALTER DATABASE [DivineBija.in] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
RESTORE DATABASE [DivineBija.in] FROM  DISK = N'C:\Dev\Database\Backup\RetailSln.BAK' WITH  FILE = 1,  MOVE N'RetailSln_Data' TO N'C:\Dev\Database\Files\DivineBija.in_Data.MDF',  MOVE N'RetailSln_Log' TO N'C:\Dev\Database\Files\DivineBija.in_Log.LDF',  NOUNLOAD,  REPLACE,  STATS = 10
ALTER DATABASE [DivineBija.in] SET MULTI_USER
GO
ALTER DATABASE [DivineBija.in] 
MODIFY FILE (NAME = 'RetailSln_Data', NEWNAME = 'DivineBija.in_Data');
GO
ALTER DATABASE [DivineBija.in] 
MODIFY FILE (NAME = 'RetailSln_Log', NEWNAME = 'DivineBija.in_Log');
GO
