--0_PopulateDemogInfo
USE [DivineBija.in]
GO
SELECT * FROM RetailSlnSch.CorpAcct WHERE CorpAcctId > 0 ORDER BY CorpAcctName
SELECT * FROM ArchLib.DemogInfoAddressUpload
SELECT * FROM ArchLib.DemogInfoData ORDER BY CountryAbbrev, StateAbbrev, CountyName, CityName, ZipCode
TRUNCATE TABLE CorpAcctEmailUpload
INSERT CorpAcctEmailUpload(CorpAcctName, SeqNum)
SELECT CorpAcctName, 1 SeqNum FROM RetailSlnSch.CorpAcct
--IndiaPinCode1A
        PRINT 'IndiaPinCode1A'
        TRUNCATE TABLE IndiaPinCode1A
        INSERT IndiaPinCode1A(CountryAbbrev, CountryDesc, StateAbbrev, StateName, CountyName, CityName, ZipCode)
        SELECT DISTINCT
               'IND' AS CountryAbbrev, 'India' AS CountryDesc, StateAbbrev, StateName, '' AS CountyName, District AS CityName, Pincode AS Zipcode
          FROM IndiaPinCode1
    INNER JOIN IndiaStateList
            ON IndiaPinCode1.StateName = IndiaStateList.SubDivisionDesc
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
        INSERT [ArchLib].[DemogInfoData](ClientId, DemogInfoCountryId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel, PostalCodeRegEx, DemogInfoSubDivisionId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, DemogInfoCountyId, CountyNameDesc, CountyName, DemogInfoCityId, CityNameDesc, CityName, DemogInfoZipId, ZipCode, DemogInfoZipPlusId, ZipPlus4)
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
