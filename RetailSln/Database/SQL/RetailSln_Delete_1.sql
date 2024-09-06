USE [RetailSln]
GO
TRUNCATE TABLE ArchLib.ExceptionLog

TRUNCATE TABLE RetaiLSlnSch.PersonExtn1
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

DELETE RetailSlnSch.OrderDetailItemBundle --WHERE OrderDetailId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderDetailItemBundle', RESEED, 0);

DELETE RetailSlnSch.OrderDetail WHERE OrderDetailId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderDetail', RESEED, 0);

DELETE RetailSlnSch.OrderHeader WHERE OrderHeaderId > 0
DBCC CHECKIDENT ('RetailSlnSch.OrderHeader', RESEED, 0);

--Insert DemogInfoAddress from Upload
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] ON
	INSERT [ArchLib].[DemogInfoAddress]
	     (
		  DemogInfoAddressId, ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, DemogInfoCountryId, DemogInfoSubDivisionId
		 ,DemogInfoCountyId, DemogInfoCityId, DemogInfoZipId, DemogInfoZipPlusId, AddressTypeId, BuildingTypeId, HouseNumber
		 ,CountryAbbrev, CountryDesc, StateAbbrev, CountyName, CityName, ZipCode, ZipPlus4
		 )
    SELECT DemogInfoAddressUpload.DemogInfoAddressUploadId, DemogInfoAddressUpload.ClientId, DemogInfoAddressUpload.AddressLine1
	      ,DemogInfoAddressUpload.AddressLine2, DemogInfoAddressUpload.AddressLine3, DemogInfoAddressUpload.AddressLine4
		  ,DemogInfoCountry.DemogInfoCountryId, DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId
		  ,DemogInfoCity.DemogInfoCityId, DemogInfoZip.DemogInfoZipId, DemogInfoZipPlus.DemogInfoZipPlusId, 0, 0, ''
		  ,DemogInfoCountry.CountryAbbrev,DemogInfoCountry.CountryDesc, DemogInfoSubDivision.StateAbbrev, DemogInfoCounty.CountyName
		  ,DemogInfoCity.CityName, DemogInfoZip.ZipCode, DemogInfoZipPlus.ZipPlus4
      FROM [ArchLib].[DemogInfoAddressUpload]
INNER JOIN [ArchLib].[DemogInfoCountry]
        ON DemogInfoAddressUpload.CountryAbbrev = DemogInfoCountry.CountryAbbrev
INNER JOIN [ArchLib].[DemogInfoSubDivision]
        ON DemogInfoAddressUpload.StateAbbrev = DemogInfoSubDivision.StateAbbrev
	   AND DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
INNER JOIN [ArchLib].[DemogInfoCounty]
        ON DemogInfoAddressUpload.CountyName = DemogInfoCounty.CountyName
	   AND DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
INNER JOIN [ArchLib].[DemogInfoCity]
        ON DemogInfoAddressUpload.CityName = DemogInfoCity.CityName
	   AND DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
INNER JOIN [ArchLib].[DemogInfoZip]
        ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId
       AND DemogInfoAddressUpload.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [ArchLib].[DemogInfoZipPlus]
        ON DemogInfoZip.DemogInfoZipId = DemogInfoZipPlus.DemogInfoZipId
  ORDER BY DemogInfoAddressUpload.DemogInfoAddressUploadId
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] OFF
--
SELECT IDENT_CURRENT('RetailSlnSch.Person')
SELECT IDENT_CURRENT('RetailSlnSch.PersonRelation')
SELECT IDENT_CURRENT('RetailSlnSch.DemogInfoAddress')
SELECT IDENT_CURRENT('RetailSlnSch.Document')
SELECT IDENT_CURRENT('RetailSlnSch.EmailData')
SELECT IDENT_CURRENT('RetailSlnSch.CreditCardData')
SELECT IDENT_CURRENT('RetailSlnSch.GiftCert')
SELECT IDENT_CURRENT('RetailSlnSch.OrderDelivery')
SELECT IDENT_CURRENT('RetailSlnSch.OrderPayment')
SELECT IDENT_CURRENT('RetailSlnSch.OrderDetailItemBundle')
SELECT IDENT_CURRENT('RetailSlnSch.OrderDetail')
SELECT IDENT_CURRENT('RetailSlnSch.OrderHeader')

DBCC CHECKIDENT ('ArchLib.Person', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.PersonRelation', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, 2);
DBCC CHECKIDENT ('ArchLib.Document', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.EmailData', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.CreditCardData', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.GiftCert', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderDelivery', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderPayment', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderDetailItemBundle', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderDetail', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderHeader', RESEED, 0);
