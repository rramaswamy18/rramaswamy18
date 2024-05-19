USE [DivineBija.in]
GO
--0_GenerateUpdateScripts_1.sql
--Apr 21 2024
SET NOCOUNT ON

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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Private Limited' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord3'
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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'USD' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyAbbreviation'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'C2' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyDecimalPlaces'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Indian Rupee' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'CurrencyName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '106' WHERE ClientId = @ClientId AND KVPKey = 'Currency' AND KVPSubKey = 'DemogInfoCountryId'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '106' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DefaultDemogInfoCountry'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '106' WHERE ClientId = @ClientId AND KVPKey = 'DeliveryInfo' AND KVPSubKey = 'DemogInfoCountryIds'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testinfo@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddress'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Support' WHERE ClientId = @ClientId AND KVPKey = 'FromEmailAddressDisplayName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'OrderProcess' AND KVPSubKey = 'DefaultOrderQty'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'testinfo@divinebija.in' WHERE ClientId = @ClientId AND KVPKey = 'PrimaryEmailAddress' AND KVPSubKey = ''

DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(400)

SET NOCOUNT OFF
