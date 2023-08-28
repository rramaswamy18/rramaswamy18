--USE [RetailAdm]
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

DELETE ArchLib.CreditCardData WHERE CreditCardDataId > 0
DBCC CHECKIDENT ('ArchLib.CreditCardData', RESEED, 0);

DELETE RetailSlnSch.GiftCert WHERE GiftCertId > 0
DBCC CHECKIDENT ('RetailApp.GiftCert', RESEED, 0);

DELETE RetailApp.OrderDelivery WHERE OrderDeliveryId > 0
DBCC CHECKIDENT ('RetailApp.OrderDelivery', RESEED, 0);

DELETE RetailApp.OrderPayment WHERE OrderPaymentId > 0
DBCC CHECKIDENT ('RetailApp.OrderPayment', RESEED, 0);

DELETE RetailApp.OrderDetail WHERE OrderDetailId > 0
DBCC CHECKIDENT ('RetailApp.OrderDetail', RESEED, 0);

DELETE RetailApp.OrderHeader WHERE OrderHeaderId > 0
DBCC CHECKIDENT ('RetailApp.OrderHeader', RESEED, 0);
