--0_PopulateDemogInfo
USE [DivineBija.in]
GO
/*
SELECT * FROM [DivineBija.in].[ArchLib].[DemogInfoDeNormalized] ORDER BY CountryAbbrev DESC
INSERT [DivineBija.in].[ArchLib].[DemogInfoDeNormalized](ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode, TelephoneCode, PostalCodeLabel, PostalCodeRegEx)
SELECT DISTINCT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, StateAbbrev, 'US-' + StateAbbrev AS SubDivisionCode, StateName AS SubDivisionDesc, 'ATE' AS SubDivisionCategoryNameDesc, '' AS ParentSubDivisionCode, CountyName, CityName, ZipCode, 1 AS TelephoneCode, 'Zip Code' AS PostalCodeLabel, '\d{5}' AS PostalCodeRegEx FROM USAZipCode
WHERE NOT EXISTS(SELECT 1 FROM [DivineBija.in].[ArchLib].[DemogInfoDeNormalized] WHERE USAZipCode.ZipCode = DemogInfoDeNormalized.ZipCode)
ORDER BY ZipCode
SELECT * FROM [DivineBija.in].[ArchLib].[DemogInfoDeNormalized] WHERE CountryAbbrev = 'USA' AND StateAbbrev <> '' AND CountyName <> '' AND CityName <> '' AND ZipCode <> ''
AND NOT EXISTS(SELECT 1 FROM USAZipCode WHERE USAZipCode.ZipCode = DemogInfoDeNormalized.ZipCode)
ORDER BY ZipCode
SELECT * FROM USAZipCode WHERE NOT EXISTS (SELECT 1 FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized WHERE USAZipCode.ZipCode = DemogInfoDeNormalized.ZipCode)
SELECT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode, TelephoneCode, PostalCodeLabel, PostalCodeRegEx
FROM [DivineBija.in].[ArchLib].DemogInfoDeNormalized WHERE CountryAbbrev = 'USA'
UNION
SELECT * FROM USAZipCode
SELECT * FROM [DivineBija.in].[ArchLib].DemogInfoDeNormalized WHERE CountryAbbrev = 'USA' ORDER BY CountryAbbrev, StateAbbrev, CountyName, CityName, ZipCode
SELECT * FROM ArchLib.DemogInfoSubDivision WHERE StateAbbrev = 'DC'
SELECT * FROM ArchLib.DemogInfoCounty WHERE DemogInfoSubDivisionId = 549
SELECT * FROM ArchLib.DemogInfoCity WHERE DemogInfoCountyId = 1717
SELECT * FROM [DivineBija.in]..SalesTaxRate_USAByZip WHERE DemogInfoZipId IS NULL /*AND TaxRegionName = 'DISTRICT OF COLUMBIA'*/ ORDER BY StateName, ZipCode
UPDATE [DivineBija.in]..SalesTaxRate_USAByZip
   SET DemogInfoZipId = DemogInfoZip.DemogInfoZipId
      ,DemogInfoCityId = DemogInfoZip.DemogInfoCityId
  FROM ArchLib.DemogInfoZip
 WHERE SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoDeNormalized
INSERT [DivineBija.in].ArchLib.DemogInfoDeNormalized (ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode, TelephoneCode, PostalCodeLabel, PostalCodeRegEx)
SELECT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode, TelephoneCode, PostalCodeLabel, PostalCodeRegEx
FROM  DemogInfo.ArchLib.DemogInfoDeNormalized
ORDER BY CountryAbbrev, StateAbbrev, CountyName, CityName, ZipCode
*/
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoCountry
INSERT [DivineBija.in].ArchLib.DemogInfoCountry(ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel, PostalCodeRegEx)
SELECT DISTINCT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel, PostalCodeRegEx
FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized
ORDER BY CountryAbbrev
--
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoSubDivision
INSERT [DivineBija.in].ArchLib.DemogInfoSubDivision(ClientId, DemogInfoCountryId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode)
SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCountryId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode
FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCountry
ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
ORDER BY DemogInfoCountryId, StateAbbrev
--
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoCounty
INSERT [DivineBija.in].ArchLib.DemogInfoCounty(ClientId, DemogInfoSubDivisionId, CountyNameDesc, CountyName)
SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoSubDivisionId, CountyName, CountyName
FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCountry
ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
AND DemogInfoDeNormalized.StateAbbrev = DemogInfoSubDivision.StateAbbrev
ORDER BY DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoDeNormalized.CountyName
--
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoCity
INSERT [DivineBija.in].ArchLib.DemogInfoCity(ClientId, DemogInfoCountyId, CityNameDesc, CityName)
SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCountyId, CityName, CityName
FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCountry
ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
AND DemogInfoDeNormalized.StateAbbrev = DemogInfoSubDivision.StateAbbrev
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
AND DemogInfoDeNormalized.CountyName = DemogInfoCounty.CountyName
ORDER BY DemogInfoCounty.DemogInfoCountyId, DemogInfoDeNormalized.CityName
--
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoZip
INSERT [DivineBija.in].ArchLib.DemogInfoZip(ClientId, DemogInfoCityId, ZipCode)
SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCityId, ZipCode
FROM [DivineBija.in].ArchLib.DemogInfoDeNormalized
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCountry
ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
AND DemogInfoDeNormalized.StateAbbrev = DemogInfoSubDivision.StateAbbrev
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
AND DemogInfoDeNormalized.CountyName = DemogInfoCounty.CountyName
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity
ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
AND DemogInfoDeNormalized.CityName = DemogInfoCity.CityName
ORDER BY DemogInfoCity.DemogInfoCityId, DemogInfoDeNormalized.ZipCode
--
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoZipPlus
INSERT [DivineBija.in].ArchLib.DemogInfoZipPlus(ClientId, DemogInfoZipId, ZipPlus4)
SELECT DISTINCT ClientId, DemogInfoZipId, '' AS ZipPlus4 FROM [DivineBija.in].ArchLib.DemogInfoZip
--
TRUNCATE TABLE [DivineBija.in].ArchLib.DemogInfoData
INSERT [DivineBija.in].ArchLib.DemogInfoData(ClientId, DemogInfoCountryId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel, PostalCodeRegEx, DemogInfoSubDivisionId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, DemogInfoCountyId, CountyNameDesc, CountyName, DemogInfoCityId, CityNameDesc, CityName, DemogInfoZipId, ZipCode)
SELECT --DISTINCT
       DemogInfoCountry.ClientId, DemogInfoCountry.DemogInfoCountryId, DemogInfoCountry.CountryAbbrev, DemogInfoCountry.CountryDesc
      ,DemogInfoCountry.Alpha2Code, DemogInfoCountry.Alpha3Code, DemogInfoCountry.NumericCode, DemogInfoCountry.SubDivisionCodeHyperLink
	  ,DemogInfoCountry.TelephoneCode, DemogInfoCountry.PostalCodeLabel, DemogInfoCountry.PostalCodeRegEx
	  ,DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoSubDivision.StateAbbrev, DemogInfoSubDivision.SubDivisionCode
	  ,DemogInfoSubDivision.SubDivisionDesc, DemogInfoSubDivision.SubDivisionCategoryNameDesc, DemogInfoSubDivision.ParentSubDivisionCode
	  ,DemogInfoCounty.DemogInfoCountyId, DemogInfoCounty.CountyNameDesc, DemogInfoCounty.CountyName, DemogInfoCity.DemogInfoCityId
	  ,DemogInfoCity.CityNameDesc, DemogInfoCity.CityName, DemogInfoZip.DemogInfoZipId, DemogInfoZip.ZipCode
FROM [DivineBija.in].ArchLib.DemogInfoCountry
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoZip ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId
ORDER BY DemogInfoCountry.DemogInfoCountryId, DemogInfoSubDivision.DemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId
        ,DemogInfoCity.DemogInfoCityId, DemogInfoZip.DemogInfoZipId
--
TRUNCATE TABLE [DivineBija.in].[ArchLib].[SalesTaxList]
    INSERT [DivineBija.in].ArchLib.[SalesTaxList](ClientId, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId, DestDemogInfoZipIdFrom, DestDemogInfoZipIdTo, ShowOnInvoice, SalesTaxCaptionId, SalesTaxRate, SrceDemogInfoCountryId)
    SELECT 97 AS ClientId, 106 AS DestDemogInfoCountryId, 391 AS DestDemogInfoSubDivisionId, NULL AS DestDemogInfoCountyId
	      ,NULL AS DestDemogInfoCityId, NULL AS DestDemogInfoZipIdFrom
		  ,NULL AS DestDemogInfoZipIdTo, 1 AS ShowOnInvoice, 600 AS SalesTaxCaptionId, 9 AS SalesTaxRate, 106 AS SrceDemogInfoCountryId
UNION
    SELECT 97 AS ClientId, 106 AS DestDemogInfoCountryId, 391 AS DestDemogInfoSubDivisionId, NULL AS DestDemogInfoCountyId, NULL AS DestDemogInfoCityId, NULL AS DestDemogInfoZipIdFrom, NULL AS DestDemogInfoZipIdTo, 1 AS ShowOnInvoice, 700 AS SalesTaxCaptionId, 9 AS SalesTaxRate, 106 AS SrceDemogInfoCountryId
UNION
    SELECT 97 AS ClientId, 106 AS DestDemogInfoCountryId, 391 AS DestDemogInfoSubDivisionId, NULL AS DestDemogInfoCountyId, NULL AS DestDemogInfoCityId, NULL AS DestDemogInfoZipIdFrom, NULL AS DestDemogInfoZipIdTo, 1 AS ShowOnInvoice, 800 AS SalesTaxCaptionId, 9 AS SalesTaxRate, 106 AS SrceDemogInfoCountryId
ORDER BY SrceDemogInfoCountryId, DestDemogInfoCountryId, 3, 4, 5, 6, 7, SalesTaxCaptionId
    INSERT [DivineBija.in].ArchLib.[SalesTaxList]
	      (
		   ClientId, DestDemogInfoCountryId, DestDemogInfoSubDivisionId, DestDemogInfoCountyId, DestDemogInfoCityId, DestDemogInfoZipIdFrom
		  ,DestDemogInfoZipIdTo, ShowOnInvoice, SalesTaxCaptionId, SalesTaxRate, SrceDemogInfoCountryId
		  )
    SELECT 97 AS ClientId, DemogInfoSubDivision.DemogInfoCountryId AS DestDemogInfoCountryId
	      ,DemogInfoSubDivision.DemogInfoSubDivisionId AS DestDemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId AS DestDemogInfoCountyId
		  ,DemogInfoCity.DemogInfoCityId AS DestDemogInfoCityId, DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdFrom
		  ,DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdTo, 0 AS ShowOnInvoice, 100 AS SalesTaxCaptionId, StateRate AS SalesTaxRate
		  ,236 AS SrceDemogInfoCountryId
	  FROM SalesTaxRate_USAByZip
INNER JOIN [DivineBija.in].ArchLib.DemogInfoZip
        ON SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity
        ON DemogInfoZip.DemogInfoCityId = DemogInfoCity.DemogInfoCityId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
        ON DemogInfoCity.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
        ON DemogInfoCounty.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
     WHERE DemogInfoSubDivision.DemogInfoCountryId IN(236)
UNION
    SELECT 97 AS ClientId, DemogInfoSubDivision.DemogInfoCountryId AS DestDemogInfoCountryId
	      ,DemogInfoSubDivision.DemogInfoSubDivisionId AS DestDemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId AS DestDemogInfoCountyId
		  ,DemogInfoCity.DemogInfoCityId AS DestDemogInfoCityId, DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdFrom
		  ,DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdTo, 0 AS ShowOnInvoice, 200 AS SalesTaxCaptionId, EstimatedCountyRate * 1.0 AS SalesTaxRate
		  ,236 AS SrceDemogInfoCountryId
	  FROM SalesTaxRate_USAByZip
INNER JOIN [DivineBija.in].ArchLib.DemogInfoZip
        ON SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity
        ON DemogInfoZip.DemogInfoCityId = DemogInfoCity.DemogInfoCityId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
        ON DemogInfoCity.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
        ON DemogInfoCounty.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
     WHERE DemogInfoSubDivision.DemogInfoCountryId IN(236)
UNION
    SELECT 97 AS ClientId, DemogInfoSubDivision.DemogInfoCountryId AS DestDemogInfoCountryId
	      ,DemogInfoSubDivision.DemogInfoSubDivisionId AS DestDemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId AS DestDemogInfoCountyId
		  ,DemogInfoCity.DemogInfoCityId AS DestDemogInfoCityId, DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdFrom
		  ,DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdTo, 0 AS ShowOnInvoice, 300 AS SalesTaxCaptionId, EstimatedCityRate * 1.0 AS SalesTaxRate
		  ,236 AS SrceDemogInfoCountryId
	  FROM SalesTaxRate_USAByZip
INNER JOIN [DivineBija.in].ArchLib.DemogInfoZip
        ON SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity
        ON DemogInfoZip.DemogInfoCityId = DemogInfoCity.DemogInfoCityId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
        ON DemogInfoCity.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
        ON DemogInfoCounty.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
     WHERE DemogInfoSubDivision.DemogInfoCountryId IN(236)
UNION
    SELECT 97 AS ClientId, DemogInfoSubDivision.DemogInfoCountryId AS DestDemogInfoCountryId
	      ,DemogInfoSubDivision.DemogInfoSubDivisionId AS DestDemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId AS DestDemogInfoCountyId
		  ,DemogInfoCity.DemogInfoCityId AS DestDemogInfoCityId, DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdFrom
		  ,DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdTo, 0 AS ShowOnInvoice, 400 AS SalesTaxCaptionId, EstimatedSpecialRate * 1.0 AS SalesTaxRate
		  ,236 AS SrceDemogInfoCountryId
	  FROM SalesTaxRate_USAByZip
INNER JOIN [DivineBija.in].ArchLib.DemogInfoZip
        ON SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity
        ON DemogInfoZip.DemogInfoCityId = DemogInfoCity.DemogInfoCityId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
        ON DemogInfoCity.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
        ON DemogInfoCounty.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
     WHERE DemogInfoSubDivision.DemogInfoCountryId IN(236)
UNION
    SELECT 97 AS ClientId, DemogInfoSubDivision.DemogInfoCountryId AS DestDemogInfoCountryId
	      ,DemogInfoSubDivision.DemogInfoSubDivisionId AS DestDemogInfoSubDivisionId, DemogInfoCounty.DemogInfoCountyId AS DestDemogInfoCountyId
		  ,DemogInfoCity.DemogInfoCityId AS DestDemogInfoCityId, DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdFrom
		  ,DemogInfoZip.DemogInfoZipId AS DestDemogInfoZipIdTo, 1 AS ShowOnInvoice, 500 AS SalesTaxCaptionId, EstimatedCombinedRate * 1.0 AS SalesTaxRate
		  ,236 AS SrceDemogInfoCountryId
	  FROM SalesTaxRate_USAByZip
INNER JOIN [DivineBija.in].ArchLib.DemogInfoZip
        ON SalesTaxRate_USAByZip.ZipCode = DemogInfoZip.ZipCode
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCity
        ON DemogInfoZip.DemogInfoCityId = DemogInfoCity.DemogInfoCityId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoCounty
        ON DemogInfoCity.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId
INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision
        ON DemogInfoCounty.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId
     WHERE DemogInfoSubDivision.DemogInfoCountryId IN(236)
ORDER BY SrceDemogInfoCountryId, DestDemogInfoCountryId, 3, 4, 5, 6, 7, SalesTaxCaptionId
--
--219322
--SELECT MAX(DemogInfoDeNormalizedId) FROM [ArchLib].[DemogInfoDeNormalized]
--SELECT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel
--      ,[DivineBija.in].ArchLib.DemogInfoSubDivisionPostalCodeRegEx, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName
--      ,ZipCode
--  FROM [ArchLib].[DemogInfoDeNormalized]
-- WHERE CountryDesc IN('India')
--ORDER BY CountryAbbrev, StateAbbrev, CountyName, CityName, ZipCode
--SELECT * FROM IndiaCityPIN WHERE Delivery = 'Delivery
--SELECT * INTO ArchLib.DemogInfoDeNormalized_1 FROM ArchLib.DemogInfoDeNormalized
--INSERT ArchLib.DemogInfoDeNormalized
--      (
--        ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel
--       ,PostalCodeRegEx, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode
--      )
--SELECT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel
--      ,PostalCodeRegEx, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName
--      ,PostalCode AS ZipCode
--  FROM ArchLib.DemogInfoDeNormalized, SingaporePostalCode
-- WHERE CountryDesc IN('Singapore')
--ORDER BY PostalCode
--INSERT ArchLib.DemogInfoDeNormalized
--      (
--        ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel
--       ,PostalCodeRegEx, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode
--      )
--SELECT DISTINCT 0 AS ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel
--      ,PostalCodeRegEx, MalaysiaPostalCode.StateCode AS StateAbbrev, DemogInfoSubDivision.SubDivisionCode, DemogInfoSubDivision.SubDivisionDesc
--,   ,DemogInfoSubDivision.SubDivisionCategoryNameDesc, DemogInfoSubDivision.ParentSubDivisionCode, CountyName, CityName
--      ,MalaysiaPostalCode.PostalCode AS ZipCode
--  FROM ArchLib.DemogInfoDeNormalized, MalaysiaPostalCode
--INNER JOIN [DivineBija.in].ArchLib.DemogInfoSubDivision ON MalaysiaPostalCode.StateCode = DemogInfoSubDivision.StateAbbrev
-- WHERE CountryDesc IN('Malaysia')
--ORDER BY StateAbbrev, CityName, PostalCode
--SELECT DISTINCT MalaysiaPostalCode.StateCode, MalaysiaPostalCode.PostOffice, MalaysiaPostalCode.PostalCode FROM MalaysiaPostalCode ORDER BY 1, 2, 3
--SELECT StateAbbrev FROM [DivineBija.in].ArchLib.DemogInfoSubDivision WHERE DemogInfoCountryId = 159 ORDER BY SubDivisionDesc
--
/*
SELECT * FROM ArchLib.DemogInfoCountry WHERE CountryDesc IN('Singapore', 'Malaysia')
SELECT * FROM ArchLib.DemogInfoSubDivision WHERE DemogInfoCountryId IN(159, 196)
SELECT * FROM ArchLib.DemogInfoCounty WHERE DemogInfoSubDivisionId IN(448, 485)
TRUNCATE TABLE [ArchLib].[DemogInfoCountry]
INSERT [ArchLib].[DemogInfoCountry](ClientId, CountryAbbrev, CountryDesc,Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel, PostalCodeRegEx)
SELECT DISTINCT ClientId, CountryAbbrev, CountryDesc,Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink, TelephoneCode, PostalCodeLabel, PostalCodeRegEx
FROM [ArchLib].[DemogInfoDeNormalized] ORDER BY CountryAbbrev

TRUNCATE TABLE [ArchLib].[DemogInfoSubDivision]
INSERT [ArchLib].[DemogInfoSubDivision](ClientId, DemogInfoCountryId, StateAbbrev,SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode)
SELECT DISTINCT DemogInfoDeNormalized.ClientId, DemogInfoCountryId, StateAbbrev, SubDivisionCode, SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode
FROM [ArchLib].[DemogInfoDeNormalized]
INNER JOIN [ArchLib].[DemogInfoCountry] ON DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
ORDER BY DemogInfoCountryId, StateAbbrev
*/
/*
SELECT DISTINCT CountryAbbrev, TelephoneCode, PostalCodeLabel, PostalCodeRegEx FROM ArchLib.DemogInfoDeNormalized ORDER BY 1
UPDATE ArchLib.DemogInfoDeNormalized
SET TelephoneCode = DemogInfoCountry.TelephoneCode, PostalCodeLabel = DemogInfoCountry.PostalCodeLabel, PostalCodeRegEx =DemogInfoCountry.PostalCodeRegEx
FROM [DivineBija.in].[ArchLib].[DemogInfoCountry] WHERE DemogInfoDeNormalized.CountryAbbrev = DemogInfoCountry.CountryAbbrev
SELECT DISTINCT CountryAbbrev, TelephoneCode, PostalCodeLabel, PostalCodeRegEx FROM ArchLib.DemogInfoDeNormalized ORDER BY 1
SELECT * FROM [DivineBija.in].[ArchLib].[DemogInfoCountry] ORDER BY CountryAbbrev
        SELECT DISTINCT ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink
          FROM ArchLib.DemogInfoDeNormalized
      ORDER BY CountryAbbrev
*/
/*
INSERT ArchLib.DemogInfoDeNormalized
      (
       ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink,StateAbbrev, SubDivisionCode
       ,SubDivisionDesc, SubDivisionCategoryNameDesc, ParentSubDivisionCode, CountyName, CityName, ZipCode
      )
SELECT DISTINCT
       DemogInfoDeNormalized.ClientId, DemogInfoDeNormalized.CountryAbbrev, DemogInfoDeNormalized.CountryDesc, DemogInfoDeNormalized.Alpha2Code
      ,DemogInfoDeNormalized.Alpha3Code, DemogInfoDeNormalized.NumericCode, DemogInfoDeNormalized.SubDivisionCodeHyperLink
      ,DemogInfoDeNormalized.StateAbbrev, DemogInfoDeNormalized.SubDivisionCode, DemogInfoDeNormalized.SubDivisionDesc
      ,DemogInfoDeNormalized.SubDivisionCategoryNameDesc, DemogInfoDeNormalized.ParentSubDivisionCode
      ,IndiaCityPIN.District AS CountyName, IndiaCityPIN.[Office Name] AS CityName, IndiaCityPIN.Pincode AS ZipCode
FROM ArchLib.DemogInfoDeNormalized
INNER JOIN IndiaStateList ON DemogInfoDeNormalized.SubDivisionDesc = IndiaStateList.SubDivisionDesc
INNER JOIN IndiaCityPIN ON IndiaStateList.StateName = IndiaCityPIN.StateName
WHERE CountryDesc = 'India' AND Delivery = 'Delivery'
*/
/*
SELECT * FROM RetailSlnSch.Item ORDER BY ItemShortDesc
SELECT * FROM dbo.DivineBija_ItemCost ORDER BY Description
SELECT Item.ItemId, DivineBija_Products.ItemId FROM RetailSlnSch.Item INNER JOIN dbo.DivineBija_Products ON Item.ItemShortDesc = DivineBija_Products.Description --AND Item.ItemId = DivineBija_Products.ItemId
*/
/*
SELECT TOP 10 * FROM ArchLib.DemogInfoData WHERE DemogInfoCountryId = 236 AND ZipCode LIKE '945%'
        TRUNCATE TABLE ArchLib.DemogInfoData
        INSERT ArchLib.DemogInfoData
              (
               DemogInfoCountryId, ClientId, CountryAbbrev, CountryDesc, Alpha2Code, Alpha3Code, NumericCode, SubDivisionCodeHyperLink
              ,TelephoneCode, PostalCodeLabel, PostalCodeRegEx, DemogInfoSubDivisionId, StateAbbrev, SubDivisionCode, SubDivisionDesc
              ,SubDivisionCategoryNameDesc, ParentSubDivisionCode, DemogInfoCountyId, CountyNameDesc, CountyName, DemogInfoCityId, CityNameDesc
              ,CityName, DemogInfoZipId, ZipCode
              )
        SELECT 
               DemogInfoCountry.DemogInfoCountryId
              ,DemogInfoCountry.ClientId
              ,DemogInfoCountry.CountryAbbrev
              ,DemogInfoCountry.CountryDesc
              ,DemogInfoCountry.Alpha2Code
              ,DemogInfoCountry.Alpha3Code
              ,DemogInfoCountry.NumericCode
              ,DemogInfoCountry.SubDivisionCodeHyperLink
              ,DemogInfoCountry.TelephoneCode
              ,DemogInfoCountry.PostalCodeLabel
              ,DemogInfoCountry.PostalCodeRegEx
              ,DemogInfoSubDivision.DemogInfoSubDivisionId
              ,DemogInfoSubDivision.StateAbbrev
              ,DemogInfoSubDivision.SubDivisionCode
              ,DemogInfoSubDivision.SubDivisionDesc
              ,DemogInfoSubDivision.SubDivisionCategoryNameDesc
              ,DemogInfoSubDivision.ParentSubDivisionCode
              ,DemogInfoCounty.DemogInfoCountyId
              ,DemogInfoCounty.CountyNameDesc
              ,DemogInfoCounty.CountyName
              ,DemogInfoCity.DemogInfoCityId
              ,DemogInfoCity.CityNameDesc
              ,DemogInfoCity.CityName
              ,DemogInfoZip.DemogInfoZipId
              ,DemogInfoZip.ZipCode
          FROM ArchLib.DemogInfoCountry
     LEFT JOIN ArchLib.DemogInfoSubDivision
            ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId
     LEFT JOIN ArchLib.DemogInfoCounty
            ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId
     LEFT JOIN ArchLib.DemogInfoCity
            ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId
     LEFT JOIN ArchLib.DemogInfoZip
            ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId
*/
--
/*
BEGIN TRAN
UPDATE ArchLib.DemogInfoCountry SET PostalCodeLabel = 'Postal Code'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' GIR[ ]?0AA|((AB|AL|B|BA|BB|BD|BH|BL|BN|BR|BS|BT|CA|CB|CF|CH|CM|CO|CR|CT|CV|CW|DA|DD|DE|DG|DH|DL|DN|DT|DY|E|EC|EH|EN|EX|FK|FY|G|GL|GY|GU|HA|HD|HG|HP|HR|HS|HU|HX|IG|IM|IP|IV|JE|KA|KT|KW|KY|L|LA|LD|LE|LL|LN|LS|LU|M|ME|MK|ML|N|NE|NG|NN|NP|NR|NW|OL|OX|PA|PE|PH|PL|PO|PR|RG|RH|RM|S|SA|SE|SG|SK|SL|SM|SN|SO|SP|SR|SS|ST|SW|SY|TA|TD|TF|TN|TQ|TR|TS|TW|UB|W|WA|WC|WD|WF|WN|WR|WS|WV|YO|ZE)(\d[\dA-Z]?[ ]?\d[ABD-HJLN-UW-Z]{2}))|BFPO[ ]?\d{1')) WHERE Alpha2Code = 'GB'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' JE\d[\dA-Z]?[ ]?\d[ABD-HJLN-UW-Z]{2}')) WHERE Alpha2Code = 'JE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' GY\d[\dA-Z]?[ ]?\d[ABD-HJLN-UW-Z]{2}')) WHERE Alpha2Code = 'GG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' IM\d[\dA-Z]?[ ]?\d[ABD-HJLN-UW-Z]{2}')) WHERE Alpha2Code = 'IM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}([ \-]\d{4})?')) WHERE Alpha2Code = 'US'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' [ABCEGHJKLMNPRSTVXY]\d[ABCEGHJ-NPRSTV-Z][ ]?\d[ABCEGHJ-NPRSTV-Z]\d')) WHERE Alpha2Code = 'CA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'DE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}-\d{4}')) WHERE Alpha2Code = 'JP'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{2}[ ]?\d{3}')) WHERE Alpha2Code = 'FR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'AU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'IT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'CH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'AT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'ES'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}[ ]?[A-Z]{2}')) WHERE Alpha2Code = 'NL'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'BE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'DK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}[ ]?\d{2}')) WHERE Alpha2Code = 'SE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'NO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}[\-]?\d{3}')) WHERE Alpha2Code = 'BR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}([\-]\d{3})?')) WHERE Alpha2Code = 'PT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'FI'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 22\d{3}')) WHERE Alpha2Code = 'AX'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}[\-]\d{3}')) WHERE Alpha2Code = 'KR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'CN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}(\d{2})?')) WHERE Alpha2Code = 'TW'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'SG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'DZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' AD\d{3}')) WHERE Alpha2Code = 'AD'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' ([A-HJ-NP-Z])?\d{4}([A-Z]{3})?')) WHERE Alpha2Code = 'AR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (37)?\d{4}')) WHERE Alpha2Code = 'AM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'AZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' ((1[0-2]|[2-9])\d{2})?')) WHERE Alpha2Code = 'BH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'BD'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (BB\d{5})?')) WHERE Alpha2Code = 'BB'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'BY'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' [A-Z]{2}[ ]?[A-Z0-9]{2}')) WHERE Alpha2Code = 'BM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'BA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' BBND 1ZZ')) WHERE Alpha2Code = 'IO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' [A-Z]{2}[ ]?\d{4}')) WHERE Alpha2Code = 'BN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'BG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'KH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'CV'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{7}')) WHERE Alpha2Code = 'CL'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4')) WHERE Alpha2Code = 'CR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'HR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'CY'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}[ ]?\d{2}')) WHERE Alpha2Code = 'CZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'DO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' ([A-Z]\d{4}[A-Z]|(?:[A-Z]{2})?\d{6})?')) WHERE Alpha2Code = 'EC'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'EG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'EE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}')) WHERE Alpha2Code = 'FO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'GE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}[ ]?\d{2}')) WHERE Alpha2Code = 'GR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 39\d{2}')) WHERE Alpha2Code = 'GL'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'GT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'HT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (?:\d{5})?')) WHERE Alpha2Code = 'HN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'HU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}')) WHERE Alpha2Code = 'IS'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'IN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'ID'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'IL'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'JO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'KZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'KE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'KW'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'LA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'LV'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (\d{4}([ ]?\d{4})?)?')) WHERE Alpha2Code = 'LB'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (948[5-9])|(949[0-7])')) WHERE Alpha2Code = 'LI'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'LT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'LU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'MK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'MY'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'MV'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' [A-Z]{3}[ ]?\d{2')) WHERE Alpha2Code = 'MT'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (\d{3}[A-Z]{2}\d{3})?')) WHERE Alpha2Code = 'MU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'MX'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'MD'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 980\d{2}')) WHERE Alpha2Code = 'MC'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'MA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'NP'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'NZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' ((\d{4}-)?\d{3}-\d{3}(-\d{1})?)?')) WHERE Alpha2Code = 'NI'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (\d{6})?')) WHERE Alpha2Code = 'NG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (PC )?\d{3}')) WHERE Alpha2Code = 'OM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'PK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'PY'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'PH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{2}-\d{3}')) WHERE Alpha2Code = 'PL'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 00[679]\d{2}([ \-]\d{4})?')) WHERE Alpha2Code = 'PR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'RO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'RU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 4789\d')) WHERE Alpha2Code = 'SM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'SA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'SN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}[ ]?\d{2}')) WHERE Alpha2Code = 'SK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'SI'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'ZA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'LK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'TJ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'TH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'TN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'TR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'TM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'UA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'UY'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'UZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM('120')) WHERE Alpha2Code = 'VA'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'VE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'ZM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM('96799')) WHERE Alpha2Code = 'AS'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM('6799')) WHERE Alpha2Code = 'CC'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'CK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'RS'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 8\d{4}')) WHERE Alpha2Code = 'ME'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'CS'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'YU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM('6798')) WHERE Alpha2Code = 'CX'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'ET'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' FIQQ 1ZZ')) WHERE Alpha2Code = 'FK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM('2899')) WHERE Alpha2Code = 'NF'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (9694[1-4])([ \-]\d{4})?')) WHERE Alpha2Code = 'FM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 9[78]3\d{2}')) WHERE Alpha2Code = 'GF'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}')) WHERE Alpha2Code = 'GN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 9[78][01]\d{2}')) WHERE Alpha2Code = 'GP'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' SIQQ 1ZZ')) WHERE Alpha2Code = 'GS'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 969[123]\d([ \-]\d{4})?')) WHERE Alpha2Code = 'GU'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'GW'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'HM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'IQ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'KG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'LR'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}')) WHERE Alpha2Code = 'LS'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}')) WHERE Alpha2Code = 'MG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 969[67]\d([ \-]\d{4})?')) WHERE Alpha2Code = 'MH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{6}')) WHERE Alpha2Code = 'MN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 9695[012]([ \-]\d{4})?')) WHERE Alpha2Code = 'MP'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 9[78]2\d{2}')) WHERE Alpha2Code = 'MQ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 988\d{2}')) WHERE Alpha2Code = 'NC'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'NE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 008(([0-4]\d)|(5[01]))([ \-]\d{4})?')) WHERE Alpha2Code = 'VI'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 987\d{2}')) WHERE Alpha2Code = 'PF'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{3}')) WHERE Alpha2Code = 'PG'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 9[78]5\d{2}')) WHERE Alpha2Code = 'PM'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' PCRN 1ZZ')) WHERE Alpha2Code = 'PN'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM('96940')) WHERE Alpha2Code = 'PW'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 9[78]4\d{2}')) WHERE Alpha2Code = 'RE'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' (ASCN|STHL) 1ZZ')) WHERE Alpha2Code = 'SH'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{4}')) WHERE Alpha2Code = 'SJ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'SO'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' [HLMS]\d{3}')) WHERE Alpha2Code = 'SZ'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' TKCA 1ZZ')) WHERE Alpha2Code = 'TC'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 986\d{2}')) WHERE Alpha2Code = 'WF'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' \d{5}')) WHERE Alpha2Code = 'XK'
UPDATE ArchLib.DemogInfoCountry SET PostalCodeRegEx = RTRIM(LTRIM(' 976\d{2}')) WHERE Alpha2Code = 'YT'
SELECT * FROM ArchLib.DemogInfoCountry
COMMIT
*/

/*
update IndiaCityPIN set DemogInfoSubDivisionId= 360 where StateName='Andaman and Nico.In.'
update IndiaCityPIN set DemogInfoSubDivisionId= 361 where StateName='Andhra Pradesh'
update IndiaCityPIN set DemogInfoSubDivisionId= 362 where StateName='Arunachal Pradesh'
update IndiaCityPIN set DemogInfoSubDivisionId= 363 where StateName='Assam'
update IndiaCityPIN set DemogInfoSubDivisionId= 364 where StateName='Bihar'
update IndiaCityPIN set DemogInfoSubDivisionId= 365 where StateName='Chandigarh'
update IndiaCityPIN set DemogInfoSubDivisionId= 366 where StateName='Chattisgarh'
update IndiaCityPIN set DemogInfoSubDivisionId= 367 where StateName='Daman and Diu'
update IndiaCityPIN set DemogInfoSubDivisionId= 368 where StateName='Delhi'
update IndiaCityPIN set DemogInfoSubDivisionId= 369 where StateName='Dadra and Nagar Hav.'
update IndiaCityPIN set DemogInfoSubDivisionId= 370 where StateName='Goa'
update IndiaCityPIN set DemogInfoSubDivisionId= 371 where StateName='Gujarat'
update IndiaCityPIN set DemogInfoSubDivisionId= 372 where StateName='Himachal Pradesh'
update IndiaCityPIN set DemogInfoSubDivisionId= 373 where StateName='Haryana'
update IndiaCityPIN set DemogInfoSubDivisionId= 374 where StateName='Jharkhand'
update IndiaCityPIN set DemogInfoSubDivisionId= 375 where StateName='Jammu and Kashmir'
update IndiaCityPIN set DemogInfoSubDivisionId= 376 where StateName='Karnataka'
update IndiaCityPIN set DemogInfoSubDivisionId= 377 where StateName='Kerala'
update IndiaCityPIN set DemogInfoSubDivisionId= 378 where StateName='Lakshadweep'
update IndiaCityPIN set DemogInfoSubDivisionId= 379 where StateName='Maharashtra'
update IndiaCityPIN set DemogInfoSubDivisionId= 380 where StateName='Megalaya'
update IndiaCityPIN set DemogInfoSubDivisionId= 381 where StateName='Manipur'
update IndiaCityPIN set DemogInfoSubDivisionId= 382 where StateName='Madhya Pradesh'
update IndiaCityPIN set DemogInfoSubDivisionId= 383 where StateName='Mizoram'
update IndiaCityPIN set DemogInfoSubDivisionId= 384 where StateName='Nagaland'
update IndiaCityPIN set DemogInfoSubDivisionId= 385 where StateName='Odisha'
update IndiaCityPIN set DemogInfoSubDivisionId= 386 where StateName='Punjab'
update IndiaCityPIN set DemogInfoSubDivisionId= 387 where StateName='Pondicherry'
update IndiaCityPIN set DemogInfoSubDivisionId= 388 where StateName='Rajasthan'
update IndiaCityPIN set DemogInfoSubDivisionId= 389 where StateName='Sikkim'
update IndiaCityPIN set DemogInfoSubDivisionId= 390 where StateName='Telangana'
update IndiaCityPIN set DemogInfoSubDivisionId= 391 where StateName='Tamil Nadu'
update IndiaCityPIN set DemogInfoSubDivisionId= 392 where StateName='Tripura'
update IndiaCityPIN set DemogInfoSubDivisionId= 393 where StateName='Uttar Pradesh'
update IndiaCityPIN set DemogInfoSubDivisionId= 394 where StateName='Uttarakhand'
update IndiaCityPIN set DemogInfoSubDivisionId= 395 where StateName='West Bengal'


insert DemogInfoCounty(ClientId,DemogInfoSubDivisionId,CountyName,CountyNameDesc)
select distinct 0 as ClientId, DemogInfoSubDivisionId,District AS CountyName,District From IndiaCityPIN 
ORDER BY DemogInfoSubDivisionId, District

INSERT DemogInfoCity(ClientId,CityName,CityNameDesc,DemogInfoCountyId)
SELECT DISTINCT 0 AS ClientId, I.[Office Name], I.[Office Name], D.DemogInfoCountyId
FROM IndiaCityPIN I
INNER JOIN DemogInfoCounty D
ON I.DemogInfoSubDivisionID = D.DemogInfoSubDivisionId
AND I.District = D.CountyNameDesc
ORDER BY D.DemogInfoCountyId,I.[Office Name]

INSERT DemogInfoZip(ClientId,ZipCode,DemogInfoCityId)
SELECT DISTINCT 0 AS ClientId, I.[Pincode],C.DemogInfoCityId
FROM IndiaCityPIN I
INNER JOIN DemogInfoCounty D
ON I.DemogInfoSubDivisionID = D.DemogInfoSubDivisionId
AND I.District = D.CountyNameDesc
INNER JOIN DemogInfoCity C
ON D.DemogInfoCountyId=C. DemogInfoCountyId
AND I.[Office Name]= C.CityNameDesc
ORDER BY C.DemogInfoCityId,I.Pincode

Insert DemogInfoZipPlus (ClientId,DemogInfoZipId,ZipPlus4)
SELECT 0 AS ClientId,DemogInfoZipId,'' ZipPlus4
FROM DemogInfoZip WHERE DemogInfoZipId >71621


SELECT Top 10 * from DemogInfoZipPlus ORDER BY 1 DESC

SELECT TOP 10  * from DemogInfoZip ORDER BY 1 DESC
*/
