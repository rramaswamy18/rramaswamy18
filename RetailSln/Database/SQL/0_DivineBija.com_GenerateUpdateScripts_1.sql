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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'en-US' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CultureInfo'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USD' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyAbbreviation'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'C2' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyDecimalPlaces'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'US Dollar' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'DemogInfoCountryId'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DefaultDemogInfoCountry'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DemogInfoCountryIds'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Support' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddressDisplayName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'OrderProcess' AND KVPSubKey = 'DefaultOrderQty'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
END

UPDATE RetailSlnSch.Item SET ItemRate = CAST(REPLACE([Final Rate USD], '$', '') AS FLOAT), ItemRateMSRP = 0/*[Final Rate USD]*/ FROM dbo.DivineBija_Products WHERE Item.ItemId = DivineBija_Products.Id
UPDATE RetailSlnSch.Item SET ItemRate = CAST(REPLACE([Retail Rate USD], '$', '') AS FLOAT), ItemRateMSRP = 0/*[Selling Rate USD]*/ FROM dbo.DivineBija_Books WHERE Item.ItemId = DivineBija_Books.Id

SET NOCOUNT OFF

IF @TestOrProdMode = 'TESTMODE'
BEGIN
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'http://test.divinebija.com/' WHERE ClientId = @ClientId AND KVPKey = 'BaseUrl'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Word9#9Pass9#9Temp' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'FALSE' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'PickupDirectory'
END

IF @TestOrProdMode = 'PRODMODE'
BEGIN
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'https://www.divinebija.com/' WHERE ClientId = @ClientId AND KVPKey = 'BaseUrl'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'info@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkUsername'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'Login9#9Password' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'NetworkPassword'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'info@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'BusinessEmail'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'info@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'info@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''
    UPDATE ArchLib.ApplicationDefault SET KVPValue = 'FALSE' WHERE ClientId = @ClientId AND KVPKey = 'SMTP' AND KVPSubKey = 'PickupDirectory'
END

DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(200, 300)
