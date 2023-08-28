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
DBCC CHECKIDENT ('RetailSlnSch.GiftCert', RESEED, 0);

DELETE RetailSlnSch.OrderDelivery WHERE OrderDeliveryId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderDelivery', RESEED, 0);

DELETE RetailSlnSch.OrderPayment WHERE OrderPaymentId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderPayment', RESEED, 0);

DELETE RetailSlnSch.OrderDetail WHERE OrderDetailId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderDetail', RESEED, 0);

DELETE RetailSlnSch.OrderHeader WHERE OrderHeaderId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderHeader', RESEED, 0);
