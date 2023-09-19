--USE [SchoolPrd]
--GO
SELECT * FROM  Lookup.CodeType ORDER BY CodeTypeNameDesc
SELECT * FROM  Lookup.CodeData ORDER BY CodeTypeId, CodeDataNameDesc
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
SELECT * FROM SchoolPrdSch.ClassList
SELECT * FROM SchoolPrdSch.ClassFees
SELECT * FROM SchoolPrdSch.ClassSession
SELECT * FROM SchoolPrdSch.ClassDetail
SELECT * FROM SchoolPrdSch.ClassSchedule
SELECT * FROM SchoolPrdSch.ClassEnroll
SELECT * FROM SchoolPrdSch.ClassEnrollDate
SELECT * FROM SchoolPrdSch.ClassEnrollFees
