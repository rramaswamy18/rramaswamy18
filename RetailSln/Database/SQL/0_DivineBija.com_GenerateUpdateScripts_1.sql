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
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '2354 Pez Vela PL' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine1A'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressLine2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'CA' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateAbbrev'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'California' WHERE ClientId = @ClientId AND KVPKey = 'AddressStateName'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '95670' WHERE ClientId = @ClientId AND KVPKey = 'AddressZipCode'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'AddressZipPlus4'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija Inc.' WHERE ClientId = @ClientId AND KVPKey = 'Business' AND KVPSubKey = 'NameOnInvoice'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija (Vedic Way)' WHERE ClientId = @ClientId AND KVPKey = 'BusinessName1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Divine Bija' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord1'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = '' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord2'
UPDATE ArchLib.ApplicationDefault SET ClientId = @ClientId, KVPValue = 'Vedic Way' WHERE ClientId = @ClientId AND KVPKey = 'BusinessNameWord3'
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

UPDATE RetailSlnSch.Item SET ItemRate = [Final Rate USD], ItemRateMSRP = [Final Rate USD] FROM dbo.DivineBija_Products WHERE Item.ProductItemId = DivineBija_Products.Id
UPDATE RetailSlnSch.Item SET ItemRate = [Final Rate USD], ItemRateMSRP = [Final Rate USD] FROM dbo.DivineBija_Books WHERE Item.ProductItemId = DivineBija_Books.Id

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
    DELETE [ArchLib].[DemogInfoAddress] WHERE DemogInfoAddressId > 0
    SET IDENTITY_INSERT [ArchLib].[DemogInfoAddress] ON
    INSERT [ArchLib].[DemogInfoAddress]
         (
          DemogInfoAddressId, ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, DemogInfoCountryId, DemogInfoSubDivisionId
         ,DemogInfoCountyId, DemogInfoCityId, DemogInfoZipId, DemogInfoZipPlusId, AddressTypeId, BuildingTypeId, HouseNumber
         ,CountryAbbrev, CountryDesc, StateAbbrev, CountyName, CityName, ZipCode, ZipPlus4
         )
    SELECT DemogInfoAddressUpload.DemogInfoAddressId, DemogInfoAddressUpload.ClientId, DemogInfoAddressUpload.AddressLine1
          ,DemogInfoAddressUpload.AddressLine2, DemogInfoAddressUpload.AddressLine3, DemogInfoAddressUpload.AddressLine4
          ,DemogInfoData.DemogInfoCountryId, DemogInfoData.DemogInfoSubDivisionId, DemogInfoData.DemogInfoCountyId
          ,DemogInfoData.DemogInfoCityId, DemogInfoData.DemogInfoZipId, DemogInfoData.DemogInfoZipPlusId, 0, 0, ''
          ,DemogInfoData.CountryAbbrev, DemogInfoData.CountryDesc, DemogInfoData.StateAbbrev, DemogInfoData.CountyName
          ,DemogInfoData.CityName, DemogInfoData.ZipCode, DemogInfoData.ZipPlus4
      FROM [ArchLib].[DemogInfoAddressUpload]
INNER JOIN [ArchLib].[DemogInfoData]
        ON DemogInfoAddressUpload.CountryAbbrev = DemogInfoData.CountryAbbrev
       AND DemogInfoAddressUpload.CityName = DemogInfoData.CityName
       AND DemogInfoAddressUpload.StateAbbrev = DemogInfoData.StateAbbrev
       AND DemogInfoAddressUpload.ZipCode = DemogInfoData.ZipCode
     WHERE DemogInfoAddressUpload.InstanceClientId = 98
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
         WHERE Id = 0
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
--         WHERE CorpAcctId > 0
----End Update CorpAcct Upload DemogInfoAddressId
--Begin Corp Acct Location DemogInfoAddress
 --       DELETE ArchLib.DemogInfoAddress WHERE DemogInfoAddressId > 0
 --       SET IDENTITY_INSERT ArchLib.DemogInfoAddress ON
 --       INSERT ArchLib.DemogInfoAddress
 --             (
 --              DemogInfoAddressId, ClientId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressTypeId, BuildingTypeId, CityName, Comments
 --             ,CountryAbbrev, CountryDesc, CountyName, DemogInfoCityId, DemogInfoCountryId, DemogInfoCountyId, DemogInfoSubDivisionId, DemogInfoZipId
 --             ,DemogInfoZipPlusId, FromDate, HouseNumber, StateAbbrev, ToDate, ZipCode, ZipPlus4
 --             )
 --       SELECT 
 --              DemogInfoAddressId, @ClientId AS ClientId, TRIM(ISNULL(AddressLine1, '')) AS AddressLine1, TRIM(AddressLine2) AS AddressLine2
 --             ,TRIM(AddressLine3) AS AddressLine3, TRIM(AddressLine4) AS AddressLine4, 0 AS AddressTypeId
 --             ,0 AS BuildingTypeId, CityName, NULL AS Comments, 'IND' AS CountryAbbrev, 'INDIA' AS CountryDesc, '' AS CountyName
 --             ,DemogInfoCityId, DemogInfoCountryId, DemogInfoCountyId, DemogInfoSubDivisionId
 --             ,DemogInfoZipId, DemogInfoZipId AS DemogInfoZipPlusId, '1900-01-01' AS FromDate, '' AS HouseNumber, StateAbbrev, '' AS ToDate
 --             ,PINCode AS ZipCode, '' AS ZipPlus4
 --         FROM DivineBija_CorpAcctUpload
    --INNER JOIN RetailSlnSch.CorpAcct
    --        ON DivineBija_CorpAcctUpload.Id = CorpAcct.CorpAcctId
 --        WHERE DivineBija_CorpAcctUpload.DemogInfoAddressId <> 0
 --     ORDER BY DivineBija_CorpAcctUpload.DemogInfoAddressId
 --       SET IDENTITY_INSERT ArchLib.DemogInfoAddress OFF
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
        --DELETE RetailSlnSch.ItemDiscount WHERE ItemDiscountId > 1
        --DBCC CHECKIDENT ('RetailSlnSch.ItemBundle', RESEED, 1);
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
DELETE Lookup.CodeData WHERE CodeTypeId = 212 AND CodeDataNameId IN(200, 300)
DELETE RetailSlnSch.PaymentModeFilter WHERE CreditSale = 200 AND PaymentModeId IN(200, 300)

UPDATE Lookup.CodeData SET CodeDataDesc0 = 'Invoice' WHERE CodeTypeId = 215 AND CodeDataNameId = 900
