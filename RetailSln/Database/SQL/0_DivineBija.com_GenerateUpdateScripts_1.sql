USE [DivineBija.com]
GO
--0_DivineBija.com_GenerateUpdateScripts_1.sql
--Apr 21 2024
SET NOCOUNT ON

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

SET NOCOUNT OFF

UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Global' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '(Vedic Way)' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord3'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USA' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CountryAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'en-US' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CultureInfo'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USA' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USD' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyAbbreviation'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'DemogInfoCountryId'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DefaultDemogInfoCountry'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '236' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DemogInfoCountryIds'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testinfo@divinebija.com' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Support' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddressDisplayName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Gold River' WHERE ClientId = @ClientId AND KVPKey = 'AddressCityName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USA' WHERE ClientId = @ClientId AND KVPKey = 'AddressCountryAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USA' WHERE ClientId = @ClientId AND KVPKey = 'AddressCountryName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '2355 Gold Meadow Way' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1A'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'CA' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'California' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AdminRepresentativeName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Inc.' WHERE ClientId = @ClientId AND KVPKey = 'BusinessName1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '19168476669' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+1 (916) 847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '1-916-847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactPhoneHref'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '19168476669' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+1 (916) 847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhoneFormatted'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '1-916-847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactTextPhoneHref'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '19168476669' WHERE ClientId = @ClientId AND KVPKey = 'ContactWhatsAppPhone'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '+1 (916) 847-6669' WHERE ClientId = @ClientId AND KVPKey = 'ContactWhatsAppPhoneFormatted'

DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(200, 300)
