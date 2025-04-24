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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Mathangi Divine Mall Pvt Ltd' WHERE ClientId = @ClientId AND KVPKey = 'Business' AND KVPSubKey = 'NameOnInvoice'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija India Pvt Ltd' WHERE ClientId = @ClientId AND KVPKey = 'BusinessName1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'India Pvt Ltd' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord3'
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
SELECT PickupLocationId, @ClientId AS ClientId, LocationNameDesc, LocationDesc, LocationDemogInfoAddressId
FROM PickupLocationUpload WHERE InstanceClientId = @ClientId
ORDER BY PickupLocationId
--SELECT 0 AS PickupLocationId, @ClientId AS ClientId, '' AS LocationNameDesc, '' AS LocationDesc, 0 AS LocationDemogInfoAddressId UNION
--SELECT 1 AS PickupLocationId, @ClientId AS ClientId, 'Divine_Bija_Mylapre' AS LocationNameDesc, 'Divine Bija Mylapre - Pickup' AS LocationDesc, 1 AS LocationDemogInfoAddressId UNION
--SELECT 2 AS PickupLocationId, @ClientId AS ClientId, 'DivineBija_Athipattu' AS LocationNameDesc, 'Divine Bija Athipattu - Pickup' AS LocationDesc, 2 AS LocationDemogInfoAddressId
--ArchLib.DemogInfoAddress
--Begin Corp Acct
        TRUNCATE TABLE RetailSlnSch.CorpAcct
        INSERT RetailSlnSch.CorpAcct(ClientId, CorpAcctName, CorpAcctTypeId, CreditDays, CreditLimit, CreditSale, DefaultDiscountPercent, MinOrderAmount, OrderApprovalRequired, TaxIdentNum, ShippingAndHandlingCharges, StatusId)
        SELECT DISTINCT
               @ClientId AS ClientId, CorpAcctName
              ,CASE CorpAcctType WHEN 'Individual' THEN 100 WHEN 'Priest' THEN 700 ELSE 500 END CorpAcctTypeId, CreditDays, CreditLimit
              ,CASE CreditSale WHEN 0 THEN 200 ELSE 100 END AS CreditSale, DefaultDiscountPercent, MinOrderAmount
              ,CASE OrderApproval WHEN 0 THEN 200 ELSE 100 END AS OrderApproval, Tax_Num
              ,CASE SHCharges WHEN 0 THEN 200 ELSE 100 END AS SHCharges, 100 AS StatusId
          FROM DivineBija_CorpAcctUpload
      ORDER BY CorpAcctName
--End Corp Acct
----Begin Update CorpAcct Upload DemogInfoAddressId
--        UPDATE DivineBija_CorpAcctUpload
--           SET DemogInfoCountryId = NULL, DemogInfoSubDivisionId = NULL, DemogInfoCountyId = NULL, DemogInfoCityId = NULL, DemogInfoZipId = NULL, DemogInfoAddressId = 0
--        UPDATE DivineBija_CorpAcctUpload
--           SET DemogInfoCountryId = DemogInfoCountry.DemogInfoCountryId
--              ,DivineBija_CorpAcctUpload.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
--          FROM ArchLib.DemogInfoCountry, ArchLib.DemogInfoSubDivision, ArchLib.DemogInfoCounty
--         WHERE DivineBija_CorpAcctUpload.CountryAbbrev = DemogInfoCountry.CountryAbbrev
--           AND DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
--           AND DivineBija_CorpAcctUpload.StateAbbrev = DemogInfoSubDivision.StateAbbrev
--           AND DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
--           AND DemogInfoCounty.CountyName = ''
--        UPDATE DivineBija_CorpAcctUpload
--           SET DemogInfoAddressId = CorpAcctId + 2
--		 WHERE CorpAcctId > 0
----End Update CorpAcct Upload DemogInfoAddressId
--Begin Corp Acct Location DemogInfoAddress
        DELETE ArchLib.DemogInfoAddress WHERE DemogInfoAddressId > 4
        SET IDENTITY_INSERT ArchLib.DemogInfoAddress ON
        INSERT ArchLib.DemogInfoAddress
              (
               DemogInfoAddressId, ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressTypeId, BuildingTypeId, CityName, Comments
              ,CountryAbbrev, CountryDesc, CountyName, DemogInfoCityId, DemogInfoCountryId, DemogInfoCountyId, DemogInfoSubDivisionId, DemogInfoZipId
              ,DemogInfoZipPlusId, FromDate, HouseNumber, StateAbbrev, ToDate, ZipCode, ZipPlus4
              )
        SELECT 
               DemogInfoAddressId, @ClientId AS ClientId, TRIM(ISNULL(AddressLine1, '')) AS AddressLine1, TRIM(AddressLine2) AS AddressLine2
              ,TRIM(AddressLine3) AS AddressLine3, TRIM(AddressLine4) AS AddressLine4, 0 AS AddressTypeId
              ,0 AS BuildingTypeId, CityName, NULL AS Comments, 'IND' AS CountryAbbrev, 'INDIA' AS CountryDesc, '' AS CountyName
              ,DemogInfoCityId, DemogInfoCountryId, DemogInfoCountyId, DemogInfoSubDivisionId
              ,DemogInfoZipId, DemogInfoZipId AS DemogInfoZipPlusId, '1900-01-01' AS FromDate, '' AS HouseNumber, StateAbbrev, '' AS ToDate
              ,PINCode AS ZipCode, '' AS ZipPlus4
          FROM DivineBija_CorpAcctUpload
         WHERE DivineBija_CorpAcctUpload.DemogInfoAddressId <> 0
      ORDER BY DivineBija_CorpAcctUpload.DemogInfoAddressId
        SET IDENTITY_INSERT ArchLib.DemogInfoAddress OFF
--Begin Corp Acct Location
        TRUNCATE TABLE RetailSlnSch.CorpAcctLocation
        INSERT RetailSlnSch.CorpAcctLocation(ClientId, CorpAcctId, SeqNum, DemogInfoAddressId, LocationName, AlternateTelephoneCountryId, AlternateTelephoneNumber, PrimaryTelephoneCountryId, PrimaryTelephoneNumber, StatusId)
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, DivineBija_CorpAcctUpload.SeqNum, 0 AS DemogInfoAddressId
              ,DivineBija_CorpAcctUpload.UniqueName AS LocationName, 106 AS AlternateTelephoneCountryId, AlternatePhone
              ,106 AS PrimaryTelephoneCountryId, PrimaryPhone, 100 AS StatusId
          FROM RetailSlnSch.CorpAcct
    INNER JOIN DivineBija_CorpAcctUpload
            ON CorpAcct.CorpAcctName = DivineBija_CorpAcctUpload.CorpAcctName
         WHERE DivineBija_CorpAcctUpload.Id = 0
        INSERT RetailSlnSch.CorpAcctLocation(ClientId, CorpAcctId, SeqNum, DemogInfoAddressId, LocationName, AlternateTelephoneCountryId, AlternateTelephoneNumber, PrimaryTelephoneCountryId, PrimaryTelephoneNumber, StatusId)
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, DivineBija_CorpAcctUpload.SeqNum, DemogInfoAddressId
              ,DivineBija_CorpAcctUpload.UniqueName AS LocationName, 106 AS AlternateTelephoneCountryId, AlternatePhone
              ,106 AS PrimaryTelephoneCountryId, PrimaryPhone, 100 AS StatusId
          FROM RetailSlnSch.CorpAcct
    INNER JOIN DivineBija_CorpAcctUpload
            ON CorpAcct.CorpAcctName = DivineBija_CorpAcctUpload.CorpAcctName
         WHERE DivineBija_CorpAcctUpload.Id > 0
      ORDER BY CorpAcct.CorpAcctId, DivineBija_CorpAcctUpload.SeqNum
--End Corp Acct Location
--Begin Corp Acct Discount
        TRUNCATE TABLE RetailSlnSch.ItemDiscount
        INSERT RetailSlnSch.ItemDiscount(ClientId, CorpAcctId, ItemId, DiscountPercent)
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, Item.ItemId, DivineBija_CorpAcctItems.Discount
          FROM DivineBija_CorpAcctItems
    INNER JOIN RetailSlnSch.CorpAcct
            ON DivineBija_CorpAcctItems.CorpAcctName = CorpAcct.CorpAcctName
    INNER JOIN RetailSlnSch.Item
            ON DivineBija_CorpAcctItems.[Item Unique Desc] = Item.ItemUniqueDesc
UNION
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, Item.ItemId, CorpAcct.DefaultDiscountPercent
          FROM RetailSlnSch.CorpAcct
	CROSS JOIN RetailSlnSch.Item
         WHERE CorpAcctTypeId = 500
      ORDER BY CorpAcctId, ItemId
--End Corp Acct Discount
--
DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(400)
DELETE RetailSlnSch.PaymentModeFilter WHERE CreditSale = 200 AND PaymentModeId IN(400)
