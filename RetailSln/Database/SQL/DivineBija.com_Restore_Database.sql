USE [master]
GO
ALTER DATABASE [DivineBija.com] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
RESTORE DATABASE [DivineBija.com] FROM  DISK = N'C:\Dev\Database\Backup\RetailSlnCom.BAK' WITH  FILE = 1,  MOVE N'RetailSlnCom_Data' TO N'C:\Dev\Database\Files\DivineBija.com_Data.MDF',  MOVE N'RetailSlnCom_Log' TO N'C:\Dev\Database\Files\DivineBija.com_Log.LDF',  NOUNLOAD,  REPLACE,  STATS = 10
ALTER DATABASE [DivineBija.com] SET MULTI_USER
GO
ALTER DATABASE [DivineBija.com] 
MODIFY FILE (NAME = 'RetailSlnCom_Data', NEWNAME = 'DivineBija.com_Data');
GO
ALTER DATABASE [DivineBija.com] 
MODIFY FILE (NAME = 'RetailSlnCom_Log', NEWNAME = 'DivineBija.com_Log');
GO
