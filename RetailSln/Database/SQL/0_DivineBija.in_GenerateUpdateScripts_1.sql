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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryAbbrev'
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
DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(400)
