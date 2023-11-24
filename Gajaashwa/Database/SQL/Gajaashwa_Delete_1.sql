--USE [Gajaashwa]
--GO

TRUNCATE TABLE ArchLib.ExceptionLog

TRUNCATE TABLE ArchLib.Telephone
DELETE ArchLib.PersonRelation --WHERE PersonId > 0
DELETE ArchLib.Person WHERE PersonId > 0
DELETE ArchLib.DemogInfoAddress WHERE DemogInfoAddressId > 0

DBCC CHECKIDENT ('ArchLib.Person', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.PersonRelation', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, 0);

DELETE ArchLib.AspNetUserRole
DELETE ArchLib.AspNetUser WHERE AspNetUserId <> ''

TRUNCATE TABLE ArchLib.EmailRecipient

DELETE ArchLib.Document WHERE DocumentId > 0
DBCC CHECKIDENT ('ArchLib.Document', RESEED, 0);

DELETE ArchLib.EmailData
DBCC CHECKIDENT ('ArchLib.EmailData', RESEED, 0);

TRUNCATE TABLE ArchLib.ContactUs
/*
TRUNCATE TABLE GajaashSch.FlavorData
TRUNCATE TABLE GajaashSch.FlavorSetup
TRUNCATE TABLE GajaashSch.Product
TRUNCATE TABLE GajaashSch.ProductHier
*/
/*
UPDATE ArchLib.Person SET HomeDemogInfoAddressId = 0 WHERE PersonId > 0
DELETE ArchLib.DemogInfoAddress WHERE DemogInfoAddressId > 0
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, 0);
*/
