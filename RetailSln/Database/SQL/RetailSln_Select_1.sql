--USE [RetailSln]
--GO

SELECT * FROM ArchLib.ExceptionLog ORDER BY 1 DESC

SELECT * FROM ArchLib.AspNetRole
SELECT * FROM ArchLib.AspNetUser
SELECT * FROM ArchLib.AspNetUserRole
SELECT * FROM ArchLib.DemogInfoAddress
SELECT * FROM ArchLib.Document
SELECT * FROM ArchLib.Person
SELECT * FROM ArchLib.PersonRelation
--SELECT * FROM ArchLib.Telephone
SELECT * FROM ArchLib.EmailData
SELECT * FROM ArchLib.EmailRecipient

SELECT * FROM RetailSlnSch.OrderHeader ORDER BY 1 DESC
SELECT * FROM RetailSlnSch.OrderDetail ORDER BY 1 DESC
SELECT * FROM RetailSlnSch.OrderDetailItemBundle ORDER BY 1 DESC
SELECT * FROM RetailSlnSch.OrderPayment ORDER BY 1 DESC
