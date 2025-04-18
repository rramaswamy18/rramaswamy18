USE [DivineBija.in]
GO
DECLARE @ClientId INT = 97, @DevMode BIT = 1

TRUNCATE TABLE ArchLib.ExceptionLog

TRUNCATE TABLE RetaiLSlnSch.PersonExtn1
TRUNCATE TABLE ArchLib.Telephone
DELETE ArchLib.PersonRelation --WHERE PersonId > 0
DELETE ArchLib.Person WHERE PersonId > 0

DBCC CHECKIDENT ('ArchLib.Person', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.PersonRelation', RESEED, 0);

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
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.Document', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.EmailData', RESEED, 0);
DBCC CHECKIDENT ('ArchLib.CreditCardData', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.GiftCert', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderDelivery', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderPayment', RESEED, 0);
DBCC CHECKIDENT ('RetailSlnSch.OrderDetailItemBundle', RESEED, 0);

SELECT IDENT_CURRENT('RetailSlnSch.OrderDetail')
DBCC CHECKIDENT ('RetailSlnSch.OrderDetail', NORESEED)
DBCC CHECKIDENT ('RetailSlnSch.OrderDetail', RESEED, 0);

SELECT IDENT_CURRENT('RetailSlnSch.OrderPayment')
DBCC CHECKIDENT ('RetailSlnSch.OrderPayment', NORESEED);
DBCC CHECKIDENT ('RetailSlnSch.OrderPayment', RESEED, 0);

SELECT IDENT_CURRENT('RetailSlnSch.OrderDelivery')
DBCC CHECKIDENT ('RetailSlnSch.OrderDelivery', NORESEED)
DBCC CHECKIDENT ('RetailSlnSch.OrderDelivery', RESEED, 0);

SELECT IDENT_CURRENT('RetailSlnSch.OrderHeader')
DBCC CHECKIDENT ('RetailSlnSch.OrderHeader', NORESEED);
DBCC CHECKIDENT ('RetailSlnSch.OrderHeader', RESEED, 0);

---PickupLocation
TRUNCATE TABLE RetailSlnSch.PickupLocation
INSERT RetailSlnSch.PickupLocation(PickupLocationId, ClientId, LocationNameDesc, LocationDesc, LocationDemogInfoAddressId)
SELECT 0 AS PickupLocationId, @ClientId AS ClientId, 'CustomLocation' AS LocationNameDesc, 'Custom Location' AS LocationDesc, 0 AS LocationDemogInfoAddressId UNION
SELECT 1 AS PickupLocationId, @ClientId AS ClientId, 'Divine_Bija_Mylapore' AS LocationNameDesc, 'Divine Bija Mylapore' AS LocationDesc, 1 AS LocationDemogInfoAddressId UNION
SELECT 2 AS PickupLocationId, @ClientId AS ClientId, 'DivineBija_Athipattu' AS LocationNameDesc, 'Divine Bija Athipattu' AS LocationDesc, 2 AS LocationDemogInfoAddressId

--Begin For Dev Only
IF @DevMode = 1
BEGIN
--AspNetUser
    INSERT ArchLib.AspNetUser(AspNetUserId, ClientId, Email, TelephoneCountryId, PhoneNumber, UserName, LoginTypeId, UserTypeId, UserStatusId, LoginPassword, PasswordExpiry, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
    SELECT AspNetUserId, @ClientId AS ClientId, EmailAddress, TelephoneCountryId, TelephoneNumber, EmailAddress AS UserName, 300 AS LoginTypeId, 100 AS UserTypeId, 100 AS UserStatusId, LoginPassword, CONVERT(NVARCHAR, DATEADD(DD, 180, GETDATE()), 121) AS PasswordExpiry, 1 AS EmailConfirmed, 1 AS PhoneNumberConfirmed, 1 AS TwoFactorEnabled, 1 AS LockoutEnabled, 0 AS AccessFailedCount
    FROM UsersUpload

--Person
    SET IDENTITY_INSERT ArchLib.Person ON
    INSERT ArchLib.Person(PersonId, ClientId, AspNetUserId, FirstName, HomeDemogInfoAddressId, LastName, StatusId, ElectronicSignatureConsentAccepted, NicknameFirst, NicknameLast, SalutationId, SuffixId)
    SELECT PersonId, @ClientId AS ClientId, AspNetUserId, FirstName, HomeDemogInfoAddressId, LastName, 100 StatusId, 1 AS ElectronicSignatureConsentAccepted, NicknameFirst, NicknameLast, 100 AS SalutationId, 100 AS SuffixId
    FROM UsersUpload
    SET IDENTITY_INSERT ArchLib.Person OFF

--AspNetUserRole
    INSERT ArchLib.AspNetUserRole(AspNetUserRoleId, AspNetUserId, AspNetRoleId)
    SELECT AspNetUserRoleId, AspNetUserId, AspNetRoleId
    FROM UsersUpload

--PersonExtn1
    INSERT RetailSlnSch.PersonExtn1(ClientId, PersonId, CorpAcctId)
    SELECT @ClientId AS ClientId, PersonId, CorpAcctId
    FROM UsersUpload
END
--DemogInfoAddressUpload
    TRUNCATE TABLE [ArchLib].[DemogInfoAddress]
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] ON
    INSERT [ArchLib].[DemogInfoAddress]
          (
           DemogInfoAddressId, ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, DemogInfoCountryId, DemogInfoSubDivisionId
          ,DemogInfoCountyId, DemogInfoCityId, DemogInfoZipId, DemogInfoZipPlusId, AddressTypeId, BuildingTypeId, HouseNumber
          ,CountryAbbrev, CountryDesc, StateAbbrev, CountyName, CityName, ZipCode, ZipPlus4
          )
    SELECT DemogInfoAddressUpload.DemogInfoAddressId, @ClientId AS ClientId, DemogInfoAddressUpload.AddressLine1
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
     WHERE InstanceClientId = @ClientId
  ORDER BY DemogInfoAddressUpload.DemogInfoAddressUploadId
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] OFF
--DemogInfoAddressUploadDev
IF @DevMode = 1
BEGIN
    INSERT [ArchLib].[DemogInfoAddress]
          (
           ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, DemogInfoCountryId, DemogInfoSubDivisionId
          ,DemogInfoCountyId, DemogInfoCityId, DemogInfoZipId, DemogInfoZipPlusId, AddressTypeId, BuildingTypeId, HouseNumber
          ,CountryAbbrev, CountryDesc, StateAbbrev, CountyName, CityName, ZipCode, ZipPlus4
          )
    SELECT @ClientId AS ClientId
          ,DemogInfoAddressUploadForDev.AddressLine1, DemogInfoAddressUploadForDev.AddressLine2, DemogInfoAddressUploadForDev.AddressLine3
          ,DemogInfoAddressUploadForDev.AddressLine4, DemogInfoCountry.DemogInfoCountryId, DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId
          ,DemogInfoCity.DemogInfoCityId, DemogInfoZip.DemogInfoZipId, DemogInfoZipPlus.DemogInfoZipPlusId, 0, 0, ''
          ,DemogInfoCountry.CountryAbbrev,DemogInfoCountry.CountryDesc, DemogInfoSubDivision.StateAbbrev, DemogInfoCounty.CountyName
          ,DemogInfoCity.CityName, DemogInfoZip.ZipCode, DemogInfoZipPlus.ZipPlus4
      FROM [dbo].[DemogInfoAddressUploadForDev]
INNER JOIN [ArchLib].[DemogInfoCountry]
        ON DemogInfoAddressUploadForDev.CountryAbbrev = DemogInfoCountry.CountryAbbrev
INNER JOIN [ArchLib].[DemogInfoSubDivision]
        ON DemogInfoAddressUploadForDev.StateAbbrev = DemogInfoSubDivision.StateAbbrev
       AND DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
INNER JOIN [ArchLib].[DemogInfoCounty]
        ON DemogInfoAddressUploadForDev.CountyName = DemogInfoCounty.CountyName
       AND DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
INNER JOIN [ArchLib].[DemogInfoCity]
        ON DemogInfoAddressUploadForDev.CityName = DemogInfoCity.CityName
       AND DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
INNER JOIN [ArchLib].[DemogInfoZip]
        ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId
       AND DemogInfoAddressUploadForDev.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [ArchLib].[DemogInfoZipPlus]
        ON DemogInfoZip.DemogInfoZipId = DemogInfoZipPlus.DemogInfoZipId
     WHERE InstanceClientId = @ClientId
  ORDER BY DemogInfoAddressUploadForDev.DemogInfoAddressUploadId
END
--
DECLARE @DemogInfoAddressId BIGINT
SELECT @DemogInfoAddressId = MAX(DemogInfoAddressId) FROM ArchLib.DemogInfoAddress
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', NORESEED);
DBCC CHECKIDENT ('ArchLib.DemogInfoAddress', RESEED, @DemogInfoAddressId);
--End For Dev Only
