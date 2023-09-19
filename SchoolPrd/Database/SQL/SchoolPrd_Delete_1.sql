--USE [SchoolPrd]
--GO

TRUNCATE TABLE ArchLib.Telephone
DELETE ArchLib.PersonRelation --WHERE PersonId > 0
DELETE ArchLib.Person WHERE PersonId > 0
DELETE ArchLib.DemogInfoAddress WHERE DemogInfoAddressId > 0

DBCC CHECKIDENT ('ArchLib.Person', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.PersonRelation', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, 0);

DELETE ArchLib.AspNetUserRole
DELETE ArchLib.AspNetUser WHERE AspNetUserId <> ''

DELETE ArchLib.Document WHERE DocumentId > 0
DBCC CHECKIDENT ('ArchLib.Document', RESEED, 0);

TRUNCATE TABLE ArchLib.ExceptionLog

TRUNCATE TABLE ArchLib.EmailRecipient

DELETE ArchLib.EmailData
DBCC CHECKIDENT ('ArchLib.EmailData', RESEED, 0);

TRUNCATE TABLE SchoolPrdSch.Enrollment
TRUNCATE TABLE SchoolPrdSch.ClassEnrollInitialSignature
TRUNCATE TABLE SchoolPrdSch.ClassEnrollFees
TRUNCATE TABLE SchoolPrdSch.ClassEnrollDate
TRUNCATE TABLE SchoolPrdSch.ClassEnrollInitialSignature
DELETE SchoolPrdSch.ClassEnroll
DBCC CHECKIDENT ('SchoolPrdSch.ClassEnroll', RESEED, 0);

TRUNCATE TABLE ArchLib.ContactUs
--TRUNCATE TABLE Training.StudentJobBoard

--TRUNCATE TABLE Training.Payment
--TRUNCATE TABLE Training.PaymentCheck
--TRUNCATE TABLE Training.PaymentCreditCard
/*
UPDATE ArchLib.Person SET HomeDemogInfoAddressId = 0 WHERE PersonId > 0
DELETE ArchLib.DemogInfoAddress WHERE DemogInfoAddressId > 0
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, 0);
*/
