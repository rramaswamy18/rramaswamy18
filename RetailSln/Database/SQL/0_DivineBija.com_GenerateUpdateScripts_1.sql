USE [DivineBija.com]
GO
--0_DivineBija.com_GenerateUpdateScripts_1.sql
--Apr 21 2024
SET NOCOUNT ON

DECLARE @TestOrProdMode VARCHAR(50) = 'DEVMODE'

DECLARE @SqlStmt VARCHAR(MAX), @SchemaName VARCHAR(250), @TableName VARCHAR(250)
DECLARE @ClientId VARCHAR(5) = '98'

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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Gold River' WHERE ClientId = @ClientId AND KVPKey = 'AddressCityName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USA' WHERE ClientId = @ClientId AND KVPKey = 'AddressCountryAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'United States' WHERE ClientId = @ClientId AND KVPKey = 'AddressCountryName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '2355 Gold Meadow Way' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1A'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'CA' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'California' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '95670' WHERE ClientId = @ClientId AND KVPKey = 'AddressZipCode'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressZipPlus4'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Global (Vedic Way)' WHERE ClientId = @ClientId AND KVPKey = 'BusinessName1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Global' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '(Vedic Way)' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord3'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '19168476669' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+1 (916) 847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '1-916-847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhoneHref'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '19168476669' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+1 (916) 847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '1-916-847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhoneHref'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '19168476669' WHERE ClientId = @ClientId AND KVPKey = 'ContactWhatsAppPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+1 (916) 847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactWhatsAppPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USA' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'United States of America' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'en-US' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CultureInfo'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USD' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyAbbreviation'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'United States of America' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'United States of America' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryDesc'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'C2' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyDecimalPlaces'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'US Dollar' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'DemogInfoCountryId'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DefaultDemogInfoCountry'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DemogInfoCountryIds'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Support' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddressDisplayName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'OrderProcess' AND KVPSubKey = 'DefaultOrderQty'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
END

UPDATE RetailSlnSch.Item SET ItemRate = CAST(REPLACE([Final Rate USD], '$', '') AS FLOAT), ItemRateMSRP = CAST(REPLACE([Final Rate USD], '$', '') AS FLOAT) FROM dbo.DivineBija_Products WHERE Item.ProductItemId = DivineBija_Products.Id
UPDATE RetailSlnSch.Item SET ItemRate = CAST(REPLACE([Final Rate USD], '$', '') AS FLOAT), ItemRateMSRP = CAST(REPLACE([Final Rate USD], '$', '') AS FLOAT) FROM dbo.DivineBija_Books WHERE Item.ProductItemId = DivineBija_Books.Id
UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Word9#9Pass9#9Temp' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testaccounts@divinebija.com' WHERE ClientId = 98 AND KVPKey = 'OrderProcess' AND KVPSubKey = 'ToEmailAddress'

SET NOCOUNT OFF

IF @TestOrProdMode = 'TESTMODE'
BEGIN
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'http://test.divinebija.com/' WHERE ClientId = @ClientId AND KVPKey = 'BaseUrl'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Word9#9Pass9#9Temp' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testsales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'FALSE' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'PickupDirectory'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testaccounts@divinebija.com' WHERE ClientId = 98 AND KVPKey = 'OrderProcess' AND KVPSubKey = 'ToEmailAddress'
END

IF @TestOrProdMode = 'PRODMODE'
BEGIN
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'https://www.divinebija.com/' WHERE ClientId = @ClientId AND KVPKey = 'BaseUrl'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Login9#9Password' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'sales@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'FALSE' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'PickupDirectory'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'accounts@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'OrderProcess' AND KVPSubKey = 'ToEmailAddress'
END
--
--Insert DemogInfoAddress from Upload
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] ON
	INSERT [ArchLib].[DemogInfoAddress]
	     (
		  DemogInfoAddressId, ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, DemogInfoCountryId, DemogInfoSubDivisionId
		 ,DemogInfoCountyId, DemogInfoCityId, DemogInfoZipId, DemogInfoZipPlusId, AddressTypeId, BuildingTypeId, HouseNumber
		 ,CountryAbbrev, CountryDesc, StateAbbrev, CountyName, CityName, ZipCode, ZipPlus4
		 )
    SELECT DemogInfoAddressUpload.DemogInfoAddressId, DemogInfoAddressUpload.ClientId, DemogInfoAddressUpload.AddressLine1
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
SELECT 1 AS PickupLocationId, @ClientId AS ClientId, 'DivineBija_Athipattu' AS LocationNameDesc, 'Divine Bija Athipattu' AS LocationDesc, 1 AS LocationDemogInfoAddressId UNION
SELECT 2 AS PickupLocationId, @ClientId AS ClientId, 'DivineBija_Mylapore' AS LocationNameDesc, 'Divine Bija Mylapore' AS LocationDesc, 2 AS LocationDemogInfoAddressId
--
--
DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(200, 400)
DELETE Lookup.CodeData WHERE CodeTypeId = 205 AND CodeDataNameId IN(100)
