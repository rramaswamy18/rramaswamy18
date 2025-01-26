USE [DivineBija.in]
GO
--0_DivineBija.in_GenerateUpdateScripts_1.sql
--Apr 21 2024
SET NOCOUNT ON

DECLARE @TestOrProdMode VARCHAR(50) = 'DEVMODE'

DECLARE @SqlStmt VARCHAR(MAX), @SchemaName VARCHAR(250), @TableName VARCHAR(250)
DECLARE @ClientId VARCHAR(5) = '97'

DECLARE UpdateSqlCursor CURSOR FOR
SELECT DISTINCT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'ClientId' AND TABLE_SCHEMA IN('ArchLib', 'RetailSlnSch', 'Lookup')
ORDER BY TABLE_SCHEMA, TABLE_NAME

OPEN UpdateSqlCursor

FETCH NEXT FROM UpdateSqlCursor INTO @SchemaName, @TableName

WHILE @@FETCH_STATUS = 0  
BEGIN
    SET @SqlStmt = 'UPDATE [' + @SchemaName + '].[' + @TableName + '] SET ClientId = ' + @ClientId + ' WHERE ClientId <> 0'
    --SELECT @SqlStmt
    EXEC(@SqlStmt)
    FETCH NEXT FROM UpdateSqlCursor INTO @SchemaName, @TableName
END

CLOSE UpdateSqlCursor
DEALLOCATE UpdateSqlCursor

BEGIN
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Chennai' WHERE ClientId = @ClientId AND KVPKey = 'AddressCityName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'IND' WHERE ClientId = @ClientId AND KVPKey = 'AddressCountryAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India' WHERE ClientId = @ClientId AND KVPKey = 'AddressCountryName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '#178/4, Selvambigai Nagar' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Attipattu' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1A'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Land Mark: Railway Station' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'TN' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Tamil Nadu' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '600120' WHERE ClientId = @ClientId AND KVPKey = 'AddressZipCode'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressZipPlus4'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija India Pvt Ltd (Vedic Way)' WHERE ClientId = @ClientId AND KVPKey = 'BusinessName1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India Pvt Ltd' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Vedic Way' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord3'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '919840570834' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+91 98405 70834' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '91-984-057-0834' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhoneHref'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '919840570834' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+91 98405 70834' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '91-984-057-0834' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhoneHref'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '919551071919' WHERE ClientId = @ClientId AND KVPKey = 'ContactWhatsAppPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+91 95510 71919' WHERE ClientId = @ClientId AND KVPKey = 'ContactWhatsAppPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'IND' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryDesc'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'en-IN' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CultureInfo'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'INR' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyAbbreviation'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'C2' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyDecimalPlaces'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Indian Rupee' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '106' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'DemogInfoCountryId'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '106' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DefaultDemogInfoCountry'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '106' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DemogInfoCountryIds'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Support' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddressDisplayName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'OrderProcess' AND KVPSubKey = 'DefaultOrderQty'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Word9#9Pass9#9Temp' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testaccounts@divinebija.in' WHERE ClientId = 98 AND KVPKey = 'OrderProcess' AND KVPSubKey = 'ToEmailAddress'
END

UPDATE RetailSlnSch.Item SET ItemRate = [Retail Rate INR], ItemRateMSRP = [MSRP INR] FROM dbo.DivineBija_Products WHERE Item.ProductItemId = DivineBija_Products.Id
UPDATE RetailSlnSch.Item SET ItemRate = [Retail Rate INR], ItemRateMSRP = [MSRP INR] FROM dbo.DivineBija_Books WHERE Item.ProductItemId = DivineBija_Books.Id

SET NOCOUNT OFF

IF @TestOrProdMode = 'TESTMODE'
BEGIN
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'http://test.divinebija.in/' WHERE ClientId = @ClientId AND KVPKey = 'BaseUrl'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Word9#9Pass9#9Temp' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'FALSE' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'PickupDirectory'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testaccounts@divinebija.in' WHERE ClientId = 98 AND KVPKey = 'OrderProcess' AND KVPSubKey = 'ToEmailAddress'
END

IF @TestOrProdMode = 'PRODMODE'
BEGIN
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'https://www.divinebija.in/' WHERE ClientId = @ClientId AND KVPKey = 'BaseUrl'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Login9#9Password' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'FALSE' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'PickupDirectory'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'accounts@divinebija.in' WHERE ClientId = 98 AND KVPKey = 'OrderProcess' AND KVPSubKey = 'ToEmailAddress'
END
--
--Insert DemogInfoAddress from Upload
    TRUNCATE TABLE [ArchLib].[DemogInfoAddress]
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
     WHERE DemogInfoAddressUpload.InstanceClientId = @ClientId
  ORDER BY DemogInfoAddressUpload.DemogInfoAddressUploadId
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] OFF
--
TRUNCATE TABLE RetailSlnSch.PickupLocation
INSERT RetailSlnSch.PickupLocation(PickupLocationId, ClientId, LocationNameDesc, LocationDesc, LocationDemogInfoAddressId)
SELECT 0 AS PickupLocationId, @ClientId AS ClientId, 'CustomLocation' AS LocationNameDesc, 'Custom Location' AS LocationDesc, 0 AS LocationDemogInfoAddressId UNION
SELECT 1 AS PickupLocationId, @ClientId AS ClientId, 'Divine_Bija_Mylapre' AS LocationNameDesc, 'Divine Bija Mylapre' AS LocationDesc, 1 AS LocationDemogInfoAddressId UNION
SELECT 2 AS PickupLocationId, @ClientId AS ClientId, 'DivineBija_Athipattu' AS LocationNameDesc, 'Divine Bija Athipattu' AS LocationDesc, 2 AS LocationDemogInfoAddressId
--ArchLib.DemogInfoAddress
--Begin Corp Acct
        TRUNCATE TABLE RetailSlnSch.CorpAcct
        INSERT RetailSlnSch.CorpAcct(ClientId, CorpAcctKey, CorpAcctName, CorpAcctTypeId, CreditDays, CreditLimit, CreditSale, TaxIdentNum, ShippingAndHandlingCharges)
        SELECT DISTINCT @ClientId AS ClientId, LoginKey AS CorpAcctKey, CorpAcctName, CASE CorpAcctType WHEN 'Individual' THEN 100 ELSE 200 END CorpAcctTypeId, CreditDays, CreditLimit, CreditSale, Tax_Num, SHCharges
          FROM DivineBija_CorpAcctUpload
      ORDER BY CorpAcctName
--End Corp Acct
--Begin Corp Acct Location
        INSERT ArchLib.DemogInfoAddress
              (
               ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressTypeId, BuildingTypeId, CityName, Comments, CountryAbbrev
              ,CountryDesc, CountyName, DemogInfoCityId, DemogInfoCountryId, DemogInfoCountyId, DemogInfoSubDivisionId, DemogInfoZipId
              ,DemogInfoZipPlusId, FromDate, HouseNumber, StateAbbrev, ToDate, ZipCode, ZipPlus4
              )
        SELECT 
               @ClientId AS ClientId, TRIM(ISNULL(AddressLine1, '')) AS AddressLine1, TRIM(AddressLine2) AS AddressLine2
			  ,TRIM(AddressLine3) AS AddressLine3, TRIM(AddressLine4) AS AddressLine4, 0 AS AddressTypeId
              ,0 AS BuildingTypeId, CityName, NULL AS Comments, 'IND' AS CountryAbbrev, 'INDIA' AS CountryDesc, '' AS CountyName
              ,NULL AS DemogInfoCityId, NULL AS DemogInfoCountryId, NULL AS DemogInfoCountyId, NULL AS DemogInfoSubDivisionId
              ,NULL AS DemogInfoZipId, 0 AS DemogInfoZipPlusId, '1900-01-01' AS FromDate, '' AS HouseNumber, StateAbbrev, '' AS ToDate
              ,PINCode AS ZipCode, '' AS ZipPlus4
          FROM DivineBija_CorpAcctUpload
		 WHERE DivineBija_CorpAcctUpload.Id > 0
      ORDER BY CorpAcctName, SeqNum
        TRUNCATE TABLE RetailSlnSch.CorpAcctLocation
        INSERT RetailSlnSch.CorpAcctLocation(ClientId, CorpAcctId, SeqNum, DemogInfoAddressId, LocationName, AlternateTelephoneCountryId, AlternateTelephoneNumber, PrimaryTelephoneCountryId, PrimaryTelephoneNumber)
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, DivineBija_CorpAcctUpload.SeqNum, 0 AS DemogInfoAddressId
              ,DivineBija_CorpAcctUpload.UniqueName AS LocationName, 106 AS AlternateTelephoneCountryId, AlternatePhone
			  ,106 AS PrimaryTelephoneCountryId, PrimaryPhone
          FROM RetailSlnSch.CorpAcct
    INNER JOIN DivineBija_CorpAcctUpload
            ON CorpAcct.CorpAcctName = DivineBija_CorpAcctUpload.CorpAcctName
         WHERE DivineBija_CorpAcctUpload.Id = 0
        INSERT RetailSlnSch.CorpAcctLocation(ClientId, CorpAcctId, SeqNum, DemogInfoAddressId, LocationName, AlternateTelephoneCountryId, AlternateTelephoneNumber, PrimaryTelephoneCountryId, PrimaryTelephoneNumber)
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, DivineBija_CorpAcctUpload.SeqNum, DivineBija_CorpAcctUpload.Id + 2 AS DemogInfoAddressId
              ,DivineBija_CorpAcctUpload.UniqueName AS LocationName, 106 AS AlternateTelephoneCountryId, AlternatePhone
			  ,106 AS PrimaryTelephoneCountryId, PrimaryPhone
          FROM RetailSlnSch.CorpAcct
    INNER JOIN DivineBija_CorpAcctUpload
            ON CorpAcct.CorpAcctName = DivineBija_CorpAcctUpload.CorpAcctName
         WHERE DivineBija_CorpAcctUpload.Id > 0
      ORDER BY CorpAcct.CorpAcctId, DivineBija_CorpAcctUpload.SeqNum
--End Corp Acct Location
--PersonExtn1
--TRUNCATE TABLE RetaiLSlnSch.PersonExtn1
--INSERT RetailSlnSch.PersonExtn1(ClientId, PersonId, CorpAcctId, CorpAcctLocationId)
--SELECT @ClientId AS ClientId, PersonId, UsersUpload.CorpAcctId, CorpAcctLocation.CorpAcctLocationId
--FROM UsersUpload INNER JOIN RetailSlnSch.CorpAcctLocation ON UsersUpload.CorpAcctId = CorpAcctLocation.CorpAcctId
--PersonExtn1
DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(400)
