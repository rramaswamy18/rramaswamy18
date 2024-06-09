USE [RetailSln]
GO
--1_RetailSln_PopulateItem.sql
--Dec 20 2024, Apr 2 2024, Apr 21 2024
DECLARE @ClientId BIGINT = 3

TRUNCATE TABLE RetailSlnSch.ItemMaster

--Begin Item Master
INSERT RetailSlnSch.ItemMaster(ClientId, ItemMasterDesc)
SELECT DISTINCT @ClientId AS ClientId, RTRIM(LTRIM(Description1)) AS ItemMasterDesc
FROM dbo.DivineBija_Products --WHERE Active = 1
UNION
SELECT DISTINCT @ClientId AS ClientId, RTRIM(LTRIM(ProductDesc)) AS ItemSItemMasterDeschortDesc
FROM dbo.DivineBija_Books --WHERE Active = 1
ORDER BY ItemMasterDesc
--End Item Master

--Begin Item
TRUNCATE TABLE RetailSlnSch.CategoryItemHier
DELETE RetailSlnSch.Item
DBCC CHECKIDENT ('RetailSlnSch.Item', RESEED, 0);

SET IDENTITY_INSERT RetailSlnSch.Item ON

INSERT RetailSlnSch.Item(ItemId, ClientId, ItemDesc, ItemMasterId, ItemRate, ItemRateMSRP, ItemShortDesc0, ItemShortDesc1, ItemShortDesc2, ItemShortDesc3, ItemShortDesc, ItemStarCount, ItemStatusId, ItemTypeId, ProductItemId, UploadImageFileName)
--Type -> Items
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(Description)) AS ItemDesc, ItemMaster.ItemMasterId, [Retail Rate INR] AS ItemRate
      ,[MSRP INR] AS ItemRateMSRP, RTRIM(LTRIM(Description0)) AS ItemShortDesc0, RTRIM(LTRIM(Description1)) AS ItemShortDesc1
	  ,RTRIM(LTRIM(Description2)) AS ItemShortDesc2, RTRIM(LTRIM(Description3)) AS ItemShortDesc3, RTRIM(LTRIM(Description)) AS ItemShortDesc
	  ,5 AS ItemStarCount, CASE WHEN Active = 1 THEN 100 ELSE 200 END AS ItemStatusId, 100 AS ItemTypeId, ItemId AS ProductItemId, ImageFileName + '.jpg' AS UploadImageFileName
FROM dbo.DivineBija_Products
INNER JOIN RetailSlnSch.ItemMaster ON ItemMaster.ItemMasterDesc = RTRIM(LTRIM(DivineBija_Products.Description1))
WHERE [Item Type] = 'ITEMS' --AND Active = 1
UNION
--Type --> Item Bundle
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(Description)) AS ItemDesc, ItemMaster.ItemMasterId, [Retail Rate INR] AS ItemRate
      ,[MSRP INR] AS ItemRateMSRP, RTRIM(LTRIM(Description0)) AS ItemShortDesc0, RTRIM(LTRIM(Description1)) AS ItemShortDesc1
	  ,RTRIM(LTRIM(Description2)) AS ItemShortDesc2, RTRIM(LTRIM(Description3)) AS ItemShortDesc3, RTRIM(LTRIM(Description)) AS ItemShortDesc
	  ,5 AS ItemStarCount, CASE WHEN Active = 1 THEN 100 ELSE 200 END AS ItemStatusId, 300 AS ItemTypeId, ItemId AS ProductItemId, ImageFileName + '.jpg' AS UploadImageFileName
FROM dbo.DivineBija_Products
INNER JOIN RetailSlnSch.ItemMaster ON ItemMaster.ItemMasterDesc = RTRIM(LTRIM(DivineBija_Products.Description1))
WHERE [Item Type] = 'BUNDLE' --AND Active = 1
UNION
----Type --> Books
SELECT Id, 3 AS ClientId, RTRIM(LTRIM(ProductDesc)) AS ItemDesc, ItemMaster.ItemMasterId, [Retail Rate INR] AS ItemRate
      ,[MSRP INR] AS ItemRateMSRP, RTRIM(LTRIM(ProductDesc0)) AS ItemShortDesc0, RTRIM(LTRIM(ProductDesc1)) AS ItemShortDesc1
	  ,NULL AS ItemShortDesc2, NULL AS ItemShortDesc3, RTRIM(LTRIM(ProductDesc)) AS ItemShortDesc, 5 AS ItemStarCount, CASE WHEN Active = 1 THEN 100 ELSE 200 END AS ItemStatusId
	  ,200 AS ItemTypeId, ItemId AS ProductItemId, Image1 AS UploadImageFileName
FROM dbo.DivineBija_Books
INNER JOIN RetailSlnSch.ItemMaster ON ItemMaster.ItemMasterDesc = RTRIM(LTRIM(DivineBija_Books.ProductDesc))
WHERE Active = 1
ORDER BY Id

SET IDENTITY_INSERT RetailSlnSch.Item OFF
--End Item

--Begin Corp Acct Discount
TRUNCATE TABLE RetailSlnSch.ItemDiscount
INSERT RetailSlnSch.ItemDiscount(ClientId, CorpAcctId, ItemId, DiscountPercent)
SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, Item.ItemId, 35 AS DiscountPercent
FROM RetailSlnSch.Item
CROSS JOIN RetailSlnSch.CorpAcct WHERE CreditSale = 1
ORDER BY CorpAcctId, ItemId
--End Corp Acct Discount

--Begin Item Attributes
TRUNCATE TABLE RetailSlnSch.ItemAttrib
INSERT RetailSlnSch.ItemAttrib(ClientId, ItemAttribMasterId, ItemAttribUnitValue, ItemAttribValue, ItemId, SeqNum, ShowValue)
SELECT ItemAttribMaster.ClientId, ItemAttribMaster.ItemAttribMasterId, '' ItemAttribUnitValue, '' ItemAttribValue, Item.ItemId, SeqNum, 0 AS ShowValue
  FROM RetailSlnSch.Item, RetailSlnSch.ItemAttribMaster
ORDER BY ItemId, SeqNum

/*Update Products Begin-------------------------------------------------------------------------------------------------------------*/

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Product Length], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Length] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 1

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Product Width], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Width] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 2

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Product Height], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Height] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 3

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Product Weight], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 4 AND [Product Weight Unit] = 'G'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Product Weight], ItemAttribUnitValue = 100, ShowValue = CASE [Show Weight] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Product Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 4 AND [Product Weight Unit] = 'G'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Product Weight], ItemAttribUnitValue = 200, ShowValue = CASE [Show Weight] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Product Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 4 AND [Product Weight Unit] = 'KG'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[HSN Code]    
FROM dbo.DivineBija_Products WHERE [HSN Code] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 5

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Prod Code]    
FROM dbo.DivineBija_Products WHERE [Prod Code] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 6

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Fluid Vol], ItemAttribUnitValue = 100, ShowValue = CASE [Show Volume] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Fluid Vol] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 7 AND [Fluid Vol Unit] = 'L'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Color, ShowValue = CASE [Show Color] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE Color <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 8

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Package, ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE Package <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 9 AND [Package] = 'Box'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Package, ItemAttribUnitValue = 200    
FROM dbo.DivineBija_Products WHERE Package <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 9 AND [Package] = 'Container'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Package, ItemAttribUnitValue = 300    
FROM dbo.DivineBija_Products WHERE Package <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 9 AND [Package] = 'Packet'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Size, ShowValue = CASE [Show Size] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE Size <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 10

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Material, ShowValue = CASE [Show Material] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE Material <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 11

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Calc Product Weight], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 15 --AND [Product Weight Unit] = 'G'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = REPLACE(DivineBija_Products.[Central GST], '%', '')
FROM dbo.DivineBija_Products WHERE [Central GST] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 16

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = REPLACE(DivineBija_Products.[State GST], '%', '')
FROM dbo.DivineBija_Products WHERE [State GST] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 17

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = CAST(REPLACE(DivineBija_Products.[Central GST], '%', '') AS FLOAT) + CAST(REPLACE(DivineBija_Products.[State GST], '%', '') AS FLOAT)
FROM dbo.DivineBija_Products WHERE [Central GST] <> '' AND DivineBija_Products.[State GST] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 18

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Weight Attribute], ShowValue = CASE [Show Weight Attribute] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Weight Attribute] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 19

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Packet], ItemAttribUnitValue = 100, ShowValue = CASE [Show Packet] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Packet] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 20 AND [Packet Unit] = 'Packet(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Packet], ItemAttribUnitValue = 200, ShowValue = CASE [Show Packet] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Packet] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 20 AND [Packet Unit] = 'Bag(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Package Length], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Package Length] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 21

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Package Width], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Package Width] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 22

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Package Height], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Package Height] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 23

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Volumetric Weight], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Volumetric Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 24

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Volumetric Weight], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Volumetric Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 25

/*Update Products End---------------------------------------------------------------------------------------------------------------*/

/*Update Book Begin-----------------------------------------------------------------------------------------------------------------*/

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Product Length], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Length] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 1

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Product Width], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Width] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 2

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Product Height], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Height] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 3

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Product Weight], ItemAttribUnitValue = 200    
FROM dbo.DivineBija_Books WHERE [Product Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 4 AND [Product Weight Unit] = 'KG'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[HSN Code]    
FROM dbo.DivineBija_Books WHERE [HSN Code] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 5

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Prod Code]    
FROM dbo.DivineBija_Books WHERE [Prod Code] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 6

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Fluid Vol], ItemAttribUnitValue = 200, ShowValue = CASE [Show Volume] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Fluid Vol] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 7 AND [Fluid Vol Unit] = 'ML'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Count], ItemAttribUnitValue = 100, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 12 AND [Count Unit] = 'Cone(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Count], ItemAttribUnitValue = 200, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 12 AND [Count Unit] = 'Cup(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Count], ItemAttribUnitValue = 300, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 12 AND [Count Unit] = 'Piece(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Count], ItemAttribUnitValue = 400, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 12 AND [Count Unit] = 'Set(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Count], ItemAttribUnitValue = 500, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 12 AND [Count Unit] = 'Stem(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Count], ItemAttribUnitValue = 600, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemAttrib.ItemId = DivineBija_Products.Id AND ItemAttribMasterId = 12 AND [Count Unit] = 'Stick(s)'

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.Publisher    
FROM dbo.DivineBija_Books WHERE Publisher <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 13

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.PageCount    
FROM dbo.DivineBija_Books WHERE PageCount <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 14

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Calc Product Weight], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 15

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = REPLACE(DivineBija_Books.[Central GST], '%', '')
FROM dbo.DivineBija_Books WHERE [Central GST] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 16

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = REPLACE(DivineBija_Books.[State GST], '%', '')
FROM dbo.DivineBija_Books WHERE [State GST] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 17

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = CAST(REPLACE(DivineBija_Books.[Central GST], '%', '') AS FLOAT) + CAST(REPLACE(DivineBija_Books.[State GST], '%', '') AS FLOAT)
FROM dbo.DivineBija_Books WHERE [Central GST] <> '' AND DivineBija_Books.[State GST] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 18

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Package Length], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Package Length] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 21

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Package Width], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Package Width] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 22

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Package Height], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Package Height] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 23

UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Books.[Volumetric Weight], ItemAttribUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Volumetric Weight] <> '' AND ItemAttrib.ItemId = DivineBija_Books.Id AND ItemAttribMasterId = 24

/*Update Books End------------------------------------------------------------------------------------------------------------------*/
--End Item Attributes

--Begin ItemSpec
TRUNCATE TABLE RetailSlnSch.ItemSpec
INSERT RetailSlnSch.ItemSpec(ClientId, ItemId, ItemSpecLabelText, ItemSpecText, SeqNum)
SELECT Item.ClientId, Item.ItemId, 'Specification(s)' AS ItemSpecLabelText, 'Specifications' AS ItemSpecText, 1 AS SeqNum
  FROM RetailSlnSch.Item
ORDER BY ItemId, SeqNum
--SELECT Item.ClientId, Item.ItemId, 'Attribute(s)' AS ItemSpecLabelText, '' AS ItemSpecText, 1 AS SeqNum
--  FROM RetailSlnSch.Item
--UNION
--SELECT Item.ClientId, Item.ItemId, '' AS ItemSpecLabelText, '' AS ItemSpecText, 3 AS SeqNum
--  FROM RetailSlnSch.Item
--SELECT Item.ClientId, Item.ItemId, '' AS ItemSpecLabelText, '' AS ItemSpecText, 4 AS SeqNum
--  FROM RetailSlnSch.Item
--End ItemSpec

--Begin CategoryHierItem
TRUNCATE TABLE RetailSlnSch.CategoryItemHier

--Start
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 1 AS SeqNum, 8 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem

--Categories
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 4 AS SeqNum, 102 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 5 AS SeqNum, 118 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 6 AS SeqNum, 113 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 7 AS SeqNum, 114 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 8 AS SeqNum, 115 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 9 AS SeqNum, 116 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 10 AS SeqNum, 117 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 11 AS SeqNum, 100 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 12 AS SeqNum, 7 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 13 AS SeqNum, 119 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 14 AS SeqNum, 120 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 15 AS SeqNum, 121 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 16 AS SeqNum, 122 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 17 AS SeqNum, 9 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 18 AS SeqNum, 6 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem

--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 5 AS CategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, 'Recursive' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 5 AS CategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, 'Recursive' AS ProcessType, 'Category' AS CategoryOrItem

--Featured Items All
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 2 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 2 AS ParentCategoryId, ItemId - 0 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId IN(9, 18, 27, 36)--BETWEEN 1 AND 4
ORDER BY SeqNum

----New Arrivals All
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
----SELECT @ClientId AS ClientId, 4 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 4 AS ParentCategoryId, ItemId - 9 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId BETWEEN 10 AND 18
--ORDER BY SeqNum

--All Items
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 9 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 9 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products
WHERE Active = 1
UNION
SELECT @ClientId AS ClientId, 9 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books
WHERE Active = 1
ORDER BY SeqNum

--Homa Items
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 102 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 102 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Homa Items' AND Active = 1
ORDER BY SeqNum

--Pooja Items
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 118 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 118 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Pooja Items' AND Active = 1
ORDER BY SeqNum

--Abhishekam
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 113 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 113 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Abhishekam' AND Active = 1
ORDER BY SeqNum

--Deepam
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 114 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 114 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Deepam' AND Active = 1
ORDER BY SeqNum

--Rituals
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 115 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 115 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Rituals' AND Active = 1
ORDER BY SeqNum

--Sumangali
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 116 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 116 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Sumangali' AND Active = 1
ORDER BY SeqNum

--Thamboolam
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 117 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 117 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Thamboolam' AND Active = 1
ORDER BY SeqNum

--Vehicle Ornaments
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 119 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 119 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Vehicle Ornaments' AND Active = 1
ORDER BY SeqNum

--Pooja Metals
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 120 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 120 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Pooja Metals' AND Active = 1
ORDER BY SeqNum

--Mala Items
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 121 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 121 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Mala Items' AND Active = 1
ORDER BY SeqNum

--Bulk Orders
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 122 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 122 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Bulk Orders' AND Active = 1
ORDER BY SeqNum

--Item Bundle
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 7 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 7 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Item Bundle' AND Active = 1
ORDER BY SeqNum

--Religious Books
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 100 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 100 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books WHERE Category = 'Religious Books' AND Active = 1
ORDER BY SeqNum

--Kids Books
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 101 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 101 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books WHERE Category = 'Kids Books' AND Active = 1
ORDER BY SeqNum

--Other Items
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 6 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 6 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM DivineBija_Products WHERE [Item Type] = 'Return Gift'
--ORDER BY SeqNum

--Other Items
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 6 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 6 AS ParentCategoryId, ItemId - 100 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 19 AND 27
ORDER BY SeqNum

----Featured Items Summary
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 1 AS SeqNum, NULL AS CategoryId, 1 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 2 AS SeqNum, NULL AS CategoryId, 2 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 3 AS SeqNum, NULL AS CategoryId, 3 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 4 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem

----New Arrivals Summary
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 1 AS SeqNum, NULL AS CategoryId, 11 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 2 AS SeqNum, NULL AS CategoryId, 12 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 3 AS SeqNum, NULL AS CategoryId, 13 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 4 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem

----Featured Items  & New Arrivals
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 5 AS ParentCategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 5 AS ParentCategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem

--End CategoryHierItem
