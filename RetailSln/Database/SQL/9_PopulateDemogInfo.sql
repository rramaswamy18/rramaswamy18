--0_PopulateDemogInfo
USE [RetailSln]
GO

     UPDATE SalesTaxRate_USAByZip
        SET DemogInfoZipId = NULL
           ,DemogInfoCityId = NULL
           ,DemogInfoCountyId = NULL
           ,DemogInfoSubDivisionId = NULL
           ,DemogInfoCountryId = NULL
           ,CityName = NULL
           ,CountyName = NULL
----
      DROP TABLE IF EXISTS #TEMP1
    SELECT DISTINCT
           DemogInfoZip.DemogInfoZipId, DemogInfoZip.ZipCode
          ,DemogInfoCity.DemogInfoCityId, DemogInfoCity.CityName
          ,DemogInfoCounty.DemogInfoCountyId, DemogInfoCounty.CountyName
          ,DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoSubDivision.StateAbbrev
          ,DemogInfoCountry.DemogInfoCountryId, DemogInfoCountry.CountryAbbrev
      INTO #TEMP1
      FROM SalesTaxRate_USAByZip
INNER JOIN ArchLib.DemogInfoZip
        ON SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
INNER JOIN ArchLib.DemogInfoCity
        ON DemogInfoZip.DemogInfoCityId = DemogInfoCity.DemogInfoCityId
INNER JOIN ArchLib.DemogInfoCounty
        ON DemogInfoCity.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId
INNER JOIN ArchLib.DemogInfoSubDivision
        ON DemogInfoCounty.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
       AND SalesTaxRate_USAByZip.StateName = DemogInfoSubDivision.StateAbbrev
INNER JOIN ArchLib.DemogInfoCountry
        ON DemogInfoSubDivision.DemogInfoCountryId = DemogInfoCountry.DemogInfoCountryId
----
    UPDATE SalesTaxRate_USAByZip
       SET DemogInfoZipId = #TEMP1.DemogInfoZipId
          ,DemogInfoCityId = #TEMP1.DemogInfoCityId
          ,DemogInfoCountyId = #TEMP1.DemogInfoCountyId
          ,DemogInfoSubDivisionId = #TEMP1.DemogInfoSubDivisionId
          ,DemogInfoCountryId = #TEMP1.DemogInfoCountryId
      FROM #TEMP1
INNER JOIN SalesTaxRate_USAByZip
        ON SalesTaxRate_USAByZip.ZipCode = #TEMP1.ZipCode
       AND SalesTaxRate_USAByZip.StateName = #TEMP1.StateAbbrev
----
DECLARE @ClientId INT = 3
TRUNCATE TABLE ArchLib.SalesTaxList
INSERT ArchLib.SalesTaxList
      (
       ClientId, BegEffDate, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId, DestDemogInfoZipId,
       EndEffDate, LineItemLevelName, SalesTaxCaptionId, SalesTaxRate, ShowOnInvoice,
       SrceDemogInfoCountryId, SrceDemogInfoSubDivisionId, SrceDemogInfoCountyId, SrceDemogInfoCityId, SrceDemogInfoZipId, TaxRegionName
      )
SELECT 
       @ClientId AS ClientId, BegEffDate, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId,
       NULL DestDemogInfoZipId, EndEffDate, LineItemLevelName, SalesTaxCaptionId, SalesTaxRate, ShowOnInvoice, SrceDemogInfoCountryId,
	   SrceDemogInfoSubDivisionId, SrceDemogInfoCountyId, SrceDemogInfoCityId, SrceDemogInfoZipId, TaxRegionName
  FROM SalesTaxList_20240504
 WHERE SrceDemogInfoCountryId = 106
   AND DestDemogInfoCountryId = 106
----
INSERT ArchLib.SalesTaxList
      (
       ClientId, BegEffDate, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId, DestDemogInfoZipId,
       EndEffDate, LineItemLevelName, SalesTaxCaptionId, SalesTaxRate, ShowOnInvoice, SrceDemogInfoCountryId, SrceDemogInfoSubDivisionId,
	   SrceDemogInfoCountyId, SrceDemogInfoCityId, SrceDemogInfoZipId, TaxRegionName
      )
SELECT 
       @ClientId AS ClientId, '1900-01-01' AS BegEffDate, DemogInfoCountryId AS DestDemogInfoCountryId, DemogInfoSubDivisionId AS DestDemogInfoSubDivisionId
      ,NULL AS DestDemogInfoCountyId, NULL AS DestDemogInfoCityId, DemogInfoZipId AS DestDemogInfoZipId
      ,'9999-12-31' AS EndEffDate, 'SUMMARY' AS LineItemLevelName, 500 AS SalesTaxCaptionId, CAST(EstimatedCombinedRate AS FLOAT) * 100 AS SalesTaxRate
      ,1 AS ShowOnInvoice, 236 AS SrceDemogInfoCountryId, NULL AS SrceDemogInfoSubDivisionId, NULL AS SrceDemogInfoCountyId
      ,NULL AS SrceDemogInfoCityId, NULL AS SrceDemogInfoZipId, NULL AS TaxRegionName
 FROM SalesTaxRate_USAByZip
WHERE DemogInfoZipId IS NOT NULL
  AND DemogInfoCountryId = 236
ORDER BY SrceDemogInfoCountryId, DestDemogInfoCountryId, DestDemogInfoSubDivisionId
----
--TRUNCATE TABLE [DivineBija.com].ArchLib.SalesTaxList
--INSERT [DivineBija.com].ArchLib.SalesTaxList
--      (
--       ClientId, BegEffDate, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId, DestDemogInfoZipId,
--       DestDemogInfoZipIdFrom, DestDemogInfoZipIdTo, EndEffDate, LineItemLevelName, SalesTaxCaptionId, SalesTaxRate, ShowOnInvoice,
--       SrceDemogInfoCountryId, SrceDemogInfoSubDivisionId, SrceDemogInfoCountyId, SrceDemogInfoCityId, SrceDemogInfoZipId, TaxRegionName
--      )
--SELECT 
--       98 AS ClientId, BegEffDate, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId,
--       DestDemogInfoZipId, NULL DestDemogInfoZipIdFrom, NULL DestDemogInfoZipIdTo, EndEffDate, LineItemLevelName, SalesTaxCaptionId,
--       SalesTaxRate, ShowOnInvoice, SrceDemogInfoCountryId, SrceDemogInfoSubDivisionId, SrceDemogInfoCountyId, SrceDemogInfoCityId,
--       SrceDemogInfoZipId, TaxRegionName
--  FROM ArchLib.SalesTaxList
--TRUNCATE TABLE CorpAcctUpload
--TRUNCATE TABLE RetailSlnSch.CorpAcct
--INSERT RetailSlnSch.CorpAcct(ClientId, CorpAcctName, CorpAcctTypeId, CreditDays, MinOrderAmount, CreditLimit, TaxIdentNum)
--SELECT 97 AS ClientId, '@Individual (Regular or Retail)' AS CorpAcctName, 100 AS CorpAcctTypeId, 0 AS CreditDays, 1000000 CreditLimit, 1000 MinOrderAmount, NULL TaxIdentNum
--UNION
--SELECT 97 AS ClientId, MIN(Name) AS CorpAcctName, 200 AS CorpAcctTypeId, 30 AS CreditDays, 1000000 CreditLimit, 1000 MinOrderAmount, Tax_No AS TaxIdentNum FROM CorpAcctUpload WHERE Tax_No IS NOT NULL GROUP BY Tax_No
--UNION
--SELECT 97 AS ClientId, Name AS CorpAcctName, 200 AS CorpAcctTypeId, 30 AS CreditDays, 1000000 CreditLimit, 1000 MinOrderAmount, Tax_No AS TaxIdentNum FROM CorpAcctUpload WHERE Tax_No IS NULL
--ORDER BY CorpAcctName
--SELECT * FROM RetailSlnSch.CorpAcct ORDER BY CorpAcctId

SELECT * FROM GSTStateCodeUpload INNER JOIN ArchLib.DemogInfoSubDivision ON StateName = SubDivisionDesc
SELECT * FROM ArchLib.DemogInfoAddressUpload
SELECT * FROM ArchLib.DemogInfoData ORDER BY CountryAbbrev, StateAbbrev, CountyName, CityName, ZipCode
--TRUNCATE TABLE CorpAcctEmailUpload
--INSERT CorpAcctEmailUpload(CorpAcctName, SeqNum)
--SELECT CorpAcctName, 1 SeqNum FROM RetailSlnSch.CorpAcct
--IndiaPinCode1A
        PRINT 'IndiaPinCode1A'
        TRUNCATE TABLE IndiaPinCode1A
        INSERT IndiaPinCode1A(CountryAbbrev, CountryDesc, StateAbbrev, StateName, CountyName, CityName, ZipCode)
        SELECT DISTINCT
               'IND' AS CountryAbbrev, 'India' AS CountryDesc, StateAbbrev, StateName, '' AS CountyName, District AS CityName, Pincode AS Zipcode
          FROM IndiaPinCode9
    INNER JOIN IndiaStateList
            ON IndiaPinCode9.StateName = IndiaStateList.SubDivisionDesc
         WHERE Delivery= 'Delivery'
      ORDER BY StateName, CityName, Zipcode
--[ArchLib].[DemogInfoDeNormalized]
        PRINT '[ArchLib].[DemogInfoDeNormalized]'
        TRUNCATE TABLE [ArchLib].[DemogInfoDeNormalized]
        INSERT [ArchLib].[DemogInfoDeNormalized]
              (
               [ClientId], [CountryAbbrev], [CountryDesc], [Alpha2Code], [Alpha3Code], [NumericCode], [SubDivisionCodeHyperLink]
              ,[StateAbbrev], [SubDivisionCode], [SubDivisionDesc], [SubDivisionCategoryNameDesc], [ParentSubDivisionCode]
              ,[CountyName], [CityName], [ZipCode], [TelephoneCode], [PostalCodeLabel], [PostalCodeRegEx]
              )
        SELECT [ClientId], [CountryAbbrev], [CountryDesc], [Alpha2Code], [Alpha3Code], [NumericCode], [SubDivisionCodeHyperLink], [StateAbbrev]
              ,[SubDivisionCode], [SubDivisionDesc], [SubDivisionCategoryNameDesc], [ParentSubDivisionCode], [CountyName], [CityName], [ZipCode]
              ,[TelephoneCode], [PostalCodeLabel], [PostalCodeRegEx]
         FROM [ArchLib].[DemogInfoDeNormalizedWithNoDups]
        WHERE CountryAbbrev <> 'IND'
        UNION
        SELECT [ClientId], [CountryAbbrev], [CountryDesc], [Alpha2Code], [Alpha3Code], [NumericCode], [SubDivisionCodeHyperLink], [StateAbbrev]
              ,[SubDivisionCode], [SubDivisionDesc], [SubDivisionCategoryNameDesc], [ParentSubDivisionCode], [CountyName], [CityName], [ZipCode]
              ,[TelephoneCode], [PostalCodeLabel], [PostalCodeRegEx]
         FROM [ArchLib].[DemogInfoDeNormalizedWithNoDups]
        WHERE CountryAbbrev = 'IND' AND StateAbbrev = ''
        UNION
        SELECT DISTINCT
               0 AS ClientId, CountryAbbrev, CountryDesc, 'IN' Alpha2Code, 'IND' Alpha3Code, '356' NumericCode, 'ISO 3166-2:IN' SubDivisionCodeHyperLink
              ,StateAbbrev, 'IN-' + StateAbbrev [SubDivisionCode], StateName [SubDivisionDesc], '' [SubDivisionCategoryNameDesc]
              ,'' [ParentSubDivisionCode], CountyName, CityName, ZipCode, '91' TelephoneCode, 'Postal Code' PostalCodeLabel
              ,'\d{6}' PostalCodeRegEx
          FROM IndiaPinCode1A
        UNION
        SELECT DISTINCT
               0 AS ClientId, CountryAbbrev, CountryDesc, 'IN' Alpha2Code, 'IND' Alpha3Code, '356' NumericCode, 'ISO 3166-2:IN' SubDivisionCodeHyperLink
              ,StateAbbrev, 'IN-' + StateAbbrev [SubDivisionCode], StateName [SubDivisionDesc], '' [SubDivisionCategoryNameDesc]
              ,'' [ParentSubDivisionCode], '' CountyName, '' CityName, '' ZipCode, '91' TelephoneCode, 'Postal Code' PostalCodeLabel
              ,'\d{6}' PostalCodeRegEx
          FROM IndiaPinCode1A
        UNION
        SELECT DISTINCT
               0 AS ClientId, CountryAbbrev, CountryDesc, 'IN' Alpha2Code, 'IND' Alpha3Code, '356' NumericCode, 'ISO 3166-2:IN' SubDivisionCodeHyperLink
              ,StateAbbrev, 'IN-' + StateAbbrev [SubDivisionCode], StateName [SubDivisionDesc], '' [SubDivisionCategoryNameDesc]
              ,'' [ParentSubDivisionCode], CountyName, '' CityName, '' ZipCode, '91' TelephoneCode, 'Postal Code' PostalCodeLabel
              ,'\d{6}' PostalCodeRegEx
          FROM IndiaPinCode1A
        UNION
        SELECT DISTINCT
               0 AS ClientId, CountryAbbrev, CountryDesc, 'IN' Alpha2Code, 'IND' Alpha3Code, '356' NumericCode, 'ISO 3166-2:IN' SubDivisionCodeHyperLink
              ,StateAbbrev, 'IN-' + StateAbbrev [SubDivisionCode], StateName [SubDivisionDesc], '' [SubDivisionCategoryNameDesc]
              ,'' [ParentSubDivisionCode], CountyName, CityName, '' ZipCode, '91' TelephoneCode, 'Postal Code' PostalCodeLabel
              ,'\d{6}' PostalCodeRegEx
          FROM IndiaPinCode1A
      ORDER BY [CountryAbbrev], [StateAbbrev], [CountyName], [CityName], [ZipCode]
--[ArchLib].[DemogInfoCountry]
        PRINT '[ArchLib].[DemogInfoCountry]'
        TRUNCATE TABLE ArchLib.DemogInfoCountry
        INSERT [ArchLib].[DemogInfoCountry]
              (ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode
              ,PostalCodeLabel, PostalCodeRegEx
              )
        SELECT DISTINCT
                ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode
               ,PostalCodeLabel, PostalCodeRegEx
          FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized
      ORDER BY CountryAbbrev
--[ArchLib].[DemogInfoSubDivision]
        PRINT '[ArchLib].[DemogInfoSubDivision]'
        TRUNCATE TABLE [ArchLib].[DemogInfoSubDivision]
        INSERT [ArchLib].[DemogInfoSubDivision]
              (
               ClientId, DemogInfoCountryId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode
              )
        SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCountryId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode
          FROM [ArchLib].[DemogInfoDeNormalized]
    INNER JOIN [ArchLib].[DemogInfoCountry]
        ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
        ORDER BY DemogInfoCountryId, StateAbbrev
--[ArchLib].[DemogInfoCounty]
        PRINT 'ArchLib.DemogInfoCounty'
        TRUNCATE TABLE [ArchLib].[DemogInfoCounty]
        INSERT [ArchLib].[DemogInfoCounty](ClientId, DemogInfoSubDivisionId, CountyNameDesc, CountyName)
        SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoDeNormalized.CountyName, DemogInfoDeNormalized.CountyName
          FROM [ArchLib].[DemogInfoDeNormalized]
    INNER JOIN [ArchLib].[DemogInfoCountry]
            ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
    INNER JOIN [ArchLib].[DemogInfoSubDivision]
            ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
           AND DemogInfoDeNormalized.StateAbbrev = DemogInfoSubDivision.StateAbbrev
      ORDER BY DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoDeNormalized.CountyName
--ArchLib.DemogInfoCity
PRINT 'ArchLib.DemogInfoCity'
        TRUNCATE TABLE [ArchLib].[DemogInfoCity]
        INSERT [ArchLib].[DemogInfoCity](ClientId, DemogInfoCountyId, CityNameDesc, CityName)
        SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCounty.DemogInfoCountyId, DemogInfoDeNormalized.CityName, DemogInfoDeNormalized.CityName
          FROM [ArchLib].[DemogInfoDeNormalized]
    INNER JOIN [ArchLib].[DemogInfoCountry]
            ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
    INNER JOIN [ArchLib].[DemogInfoSubDivision]
            ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
           AND DemogInfoDeNormalized.StateAbbrev = DemogInfoSubDivision.StateAbbrev
    INNER JOIN [ArchLib].[DemogInfoCounty]
            ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
           AND DemogInfoDeNormalized.CountyName = DemogInfoCounty.CountyName
      ORDER BY DemogInfoCounty.DemogInfoCountyId, DemogInfoDeNormalized.CityName
--ArchLib.DemogInfoZip
PRINT 'ArchLib.DemogInfoZip'
        TRUNCATE TABLE [ArchLib].[DemogInfoZip]
        INSERT [ArchLib].[DemogInfoZip](ClientId, DemogInfoCityId, ZipCode)
        SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCityId, ZipCode
          FROM [ArchLib].[DemogInfoDeNormalized]
    INNER JOIN [ArchLib].[DemogInfoCountry]
            ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
    INNER JOIN [ArchLib].[DemogInfoSubDivision]
            ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
           AND DemogInfoDeNormalized.StateAbbrev = DemogInfoSubDivision.StateAbbrev
    INNER JOIN [ArchLib].[DemogInfoCounty]
            ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
           AND DemogInfoDeNormalized.CountyName = DemogInfoCounty.CountyName
    INNER JOIN [ArchLib].[DemogInfoCity]
            ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
           AND DemogInfoDeNormalized.CityName = DemogInfoCity.CityName
      ORDER BY DemogInfoCity.DemogInfoCityId, DemogInfoDeNormalized.ZipCode
--[ArchLib].[DemogInfoZipPlus]
        PRINT '[ArchLib].[DemogInfoZipPlus]'
        TRUNCATE TABLE [ArchLib].[DemogInfoZipPlus]
        INSERT [ArchLib].[DemogInfoZipPlus](ClientId, DemogInfoZipId, ZipPlus4)
        SELECT DISTINCT ClientId, DemogInfoZipId, '' AS ZipPlus4
          FROM [ArchLib].[DemogInfoZip]
      ORDER BY DemogInfoZipId
--ArchLib.DemogInfoData
        TRUNCATE TABLE [ArchLib].[DemogInfoData]
        INSERT [ArchLib].[DemogInfoData]
              (
               ClientId, DemogInfoCountryId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink
              ,TelephoneCode, PostalCodeLabel, PostalCodeRegEx, DemogInfoSubDivisionId, StateAbbrev, SubDivisionCode, SubDivisionDesc
              ,SubDivisionCategoryNameDesc, ParentSubDivisionCode, DemogInfoCountyId, CountyNameDesc, CountyName, DemogInfoCityId, CityNameDesc
              ,CityName, DemogInfoZipId, ZipCode, DemogInfoZipPlusId, ZipPlus4
              )
        SELECT --DISTINCT
               DemogInfoCountry.ClientId, DemogInfoCountry.DemogInfoCountryId, DemogInfoCountry.CountryAbbrev, DemogInfoCountry.CountryDesc
              ,DemogInfoCountry.Alpha2Code, DemogInfoCountry.Alpha3Code, DemogInfoCountry.NumericCode, DemogInfoCountry.SubDivisionCodeHyperLink
              ,DemogInfoCountry.TelephoneCode, DemogInfoCountry.PostalCodeLabel, DemogInfoCountry.PostalCodeRegEx
              ,DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoSubDivision.StateAbbrev, DemogInfoSubDivision.SubDivisionCode
              ,DemogInfoSubDivision.SubDivisionDesc, DemogInfoSubDivision.SubDivisionCategoryNameDesc, DemogInfoSubDivision.ParentSubDivisionCode
              ,DemogInfoCounty.DemogInfoCountyId, DemogInfoCounty.CountyNameDesc, DemogInfoCounty.CountyName, DemogInfoCity.DemogInfoCityId
              ,DemogInfoCity.CityNameDesc, DemogInfoCity.CityName, DemogInfoZip.DemogInfoZipId, DemogInfoZip.ZipCode
              ,DemogInfoZipPlus.DemogInfoZipPlusId, DemogInfoZipPlus.ZipPlus4
          FROM [ArchLib].[DemogInfoCountry]
    INNER JOIN [ArchLib].[DemogInfoSubDivision] ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
    INNER JOIN [ArchLib].[DemogInfoCounty] ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
    INNER JOIN [ArchLib].[DemogInfoCity] ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
    INNER JOIN [ArchLib].[DemogInfoZip] ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId
    INNER JOIN [ArchLib].[DemogInfoZipPlus] ON DemogInfoZip.DemogInfoZipId = DemogInfoZipPlus.DemogInfoZipId
      ORDER BY DemogInfoCountry.DemogInfoCountryId, DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId
              ,DemogInfoCity.DemogInfoCityId, DemogInfoZip.DemogInfoZipId
--End
