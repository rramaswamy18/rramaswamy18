USE [RetailSln]
GO
--1_RetailSln_PopulateItem_DivineBija.In.sql
--Dec 20 2024, Apr 2 2024, Apr 21 2024
DECLARE @ClientId BIGINT = 3

TRUNCATE TABLE RetailSlnSch.ItemMaster

--Begin Item Master
INSERT RetailSlnSch.ItemMaster(ClientId, ItemMasterUniqueDesc)
SELECT DISTINCT @ClientId AS ClientId, RTRIM(LTRIM(UniqueDescription)) AS ItemMasterUniqueDesc
FROM dbo.DivineBija_Products --WHERE [India Active] = 1
UNION
SELECT DISTINCT @ClientId AS ClientId, RTRIM(LTRIM(UniqueDescription)) AS ItemMasterUniqueDesc
FROM dbo.DivineBija_Books --WHERE [India Active] = 1
ORDER BY ItemMasterUniqueDesc
--End Item Master

--Begin Item
TRUNCATE TABLE RetailSlnSch.CategoryItemHier

DELETE RetailSlnSch.Item
DBCC CHECKIDENT ('RetailSlnSch.Item', RESEED, 0);

DELETE RetailSlnSch.Category

INSERT RetailSlnSch.Category
      (
	   CategoryId, ClientId, AllowSubCategory, CategoryDesc, CategoryLongDesc, CategoryNameDesc, CategoryStatusId, CategoryTypeId
	  ,DefaultCategory, ImageExtension, MaxPerPage, ViewName
	  )
SELECT Id AS CategoryId, @ClientId AS ClientId, [Allow Sub] AS AllowSubCategory,[Category Desc] AS CategoryDesc, '' AS CategoryLongDesc
      ,[Category Name Desc] AS CategoryNameDesc, CASE Active WHEN 1 THEN 100 ELSE 900 END AS CategoryStatusId
	  ,CASE [Category Type] WHEN 'Regular Category' THEN 100 WHEN 'Item Bundle' THEN 400 ELSE 200 END AS CategoryTypeId
	  ,[Def Cat] AS DefaultCategory, 'png' AS ImageExtension, MaxPerPage, '_OrderCategoryItem' AS ViewName
  FROM dbo.DivineBija_Categories
--
UPDATE RetailSlnSch.Category SET ViewName = '_FestivalList' WHERE CategoryNameDesc = 'Festival List'

SET IDENTITY_INSERT RetailSlnSch.Item ON

INSERT RetailSlnSch.Item
      (ItemId, ClientId, ItemDesc, ItemForSaleId, ItemMasterId, ItemRate, ItemRateMSRP, ItemShortDesc0, ItemShortDesc1, ItemShortDesc2
	  ,ItemShortDesc3, ItemShortDesc, ItemStarCount, ItemStatusId, ItemTypeId, ItemUniqueDesc, ProductItemId, UploadImageFileName)
--Type -> Items
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(Description)) AS ItemDesc, CASE [India For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId
      ,ItemMaster.ItemMasterId, [Retail Rate INR] AS ItemRate, [MSRP INR] AS ItemRateMSRP, RTRIM(LTRIM(Description0)) AS ItemShortDesc0
	  ,RTRIM(LTRIM(Description1)) AS ItemShortDesc1, RTRIM(LTRIM(Description2)) AS ItemShortDesc2, RTRIM(LTRIM(Description3)) AS ItemShortDesc3
	  ,RTRIM(LTRIM(Description)) AS ItemShortDesc, 5 AS ItemStarCount, CASE WHEN [India Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId
	  ,100 AS ItemTypeId, RTRIM(LTRIM(UniqueDescription)) AS ItemUniqueDesc, ItemId AS ProductItemId, ImageFileName AS UploadImageFileName
  FROM dbo.DivineBija_Products
INNER JOIN RetailSlnSch.ItemMaster ON ItemMaster.ItemMasterUniqueDesc = RTRIM(LTRIM(DivineBija_Products.UniqueDescription))
WHERE [Item Type] = 'ITEMS' AND [India Active] = 1
UNION
--Type --> Item Bundle
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(Description)) AS ItemDesc, CASE [India For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId
      ,ItemMaster.ItemMasterId, [Retail Rate INR] AS ItemRate, [MSRP INR] AS ItemRateMSRP, RTRIM(LTRIM(Description0)) AS ItemShortDesc0
	  ,RTRIM(LTRIM(Description1)) AS ItemShortDesc1, RTRIM(LTRIM(Description2)) AS ItemShortDesc2, RTRIM(LTRIM(Description3)) AS ItemShortDesc3
	  ,RTRIM(LTRIM(Description)) AS ItemShortDesc, 5 AS ItemStarCount, CASE WHEN [India Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId
	  ,300 AS ItemTypeId, RTRIM(LTRIM(UniqueDescription)) AS ItemUniqueDesc, ItemId AS ProductItemId, ImageFileName AS UploadImageFileName
  FROM dbo.DivineBija_Products
INNER JOIN RetailSlnSch.ItemMaster ON ItemMaster.ItemMasterUniqueDesc = RTRIM(LTRIM(DivineBija_Products.UniqueDescription))
WHERE [Item Type] = 'BUNDLE' AND [India Active] = 1
UNION
----Type --> Books
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(ProductDesc)) AS ItemDesc, CASE [India For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId
      ,ItemMaster.ItemMasterId, [Retail Rate INR] AS ItemRate, [MSRP INR] AS ItemRateMSRP, RTRIM(LTRIM(ProductDesc0)) AS ItemShortDesc0
	  ,RTRIM(LTRIM(ProductDesc1)) AS ItemShortDesc1, NULL AS ItemShortDesc2, NULL AS ItemShortDesc3, RTRIM(LTRIM(ProductDesc)) AS ItemShortDesc
	  ,5 AS ItemStarCount, CASE WHEN [India Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId, 200 AS ItemTypeId
	  ,RTRIM(LTRIM(ItemMaster.ItemMasterUniqueDesc)) AS ItemMasterUniqueDesc, ItemId AS ProductItemId, Image1 AS UploadImageFileName
  FROM dbo.DivineBija_Books
INNER JOIN RetailSlnSch.ItemMaster ON ItemMaster.ItemMasterUniqueDesc = RTRIM(LTRIM(DivineBija_Books.UniqueDescription))
WHERE [India Active] = 1
ORDER BY Id

SET IDENTITY_INSERT RetailSlnSch.Item OFF
--End Item

--Begin CategoryHierItem
TRUNCATE TABLE RetailSlnSch.CategoryItemHier

DROP TABLE IF EXISTS #TEMP1
CREATE TABLE #TEMP1(ClientId BIGINT NOT NULL, ParentCategoryId BIGINT NOT NULL, SeqNum FLOAT NOT NULL, CategoryId BIGINT NULL, ItemId BIGINT NULL, ProcessType NVARCHAR(50) NOT NULL, CategoryOrItem NVARCHAR(50) NOT NULL)

--Start
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 1 AS SeqNum, 8 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem

--Categories
INSERT #TEMP1 --RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, ParentId AS ParentCategoryId, [Seq Num] AS SeqNum, Id AS CategoryId, NULL AS ItemId, '' AS ProcessType
      ,'Category' AS CategoryOrItem
  FROM dbo.DivineBija_Categories
WHERE ParentId IS NOT NULL
--ORDER BY ParentId, [Seq Num]

--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 5 AS CategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, 'Recursive' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 5 AS CategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, 'Recursive' AS ProcessType, 'Category' AS CategoryOrItem

----Test
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
----SELECT @ClientId AS ClientId, 2 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 121 AS ParentCategoryId, ItemId - 0 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId IN(45, 54, 63, 72, 81)--BETWEEN 1 AND 4
--ORDER BY SeqNum

----Featured Items
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
----SELECT @ClientId AS ClientId, 2 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 99 AS ParentCategoryId, ItemId - 0 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId IN(9, 18, 27, 36)--BETWEEN 1 AND 4
--ORDER BY SeqNum

--All Items
INSERT #TEMP1 --RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 9 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT DISTINCT @ClientId AS ClientId, 100 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products
WHERE [India Active] = 1
UNION
SELECT DISTINCT @ClientId AS ClientId, 100 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books
WHERE [India Active] = 1
--ORDER BY SeqNum

--Item Bundle
INSERT #TEMP1 --RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 9 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 119 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products
WHERE [India Active] = 1 AND [Item Type] = 'BUNDLE'
ORDER BY UniqueDescription

--Items in Remaining Categories - Products
INSERT #TEMP1 --RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, Category.CategoryId AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON Category.CategoryNameDesc IN([Category 1], [Category 2], [Category 3], [Category 4], [Category 5], [Category 6])
WHERE [India Active] = 1 AND Category.CategoryId <> 100
ORDER BY ParentCategoryId, SeqNum

--Religious 
--Items in Remaining Categories - Books
INSERT #TEMP1 --RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 100 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 3 AS ClientId, Category.CategoryId AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books INNER JOIN RetailSlnSch.Category ON Category.CategoryNameDesc IN([Category0], [Category1], [Category2])
WHERE [India Active] = 1
ORDER BY ParentCategoryId, SeqNum

----Kids Books
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
----SELECT @ClientId AS ClientId, 101 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 108 AS ParentCategoryId, SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM DivineBija_Books WHERE Category = 'Kids Books' AND [India Active] = 1
--ORDER BY SeqNum

INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT DISTINCT ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem
FROM #TEMP1
ORDER BY ClientId, ParentCategoryId, SeqNum
--End CategoryHierItem

--Begin Corp Acct Discount
TRUNCATE TABLE RetailSlnSch.ItemDiscount
INSERT RetailSlnSch.ItemDiscount(ClientId, CorpAcctId, ItemId, DiscountPercent)
SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, Item.ItemId, 35 AS DiscountPercent
FROM RetailSlnSch.Item
CROSS JOIN RetailSlnSch.CorpAcct WHERE CreditSale = 1
ORDER BY CorpAcctId, ItemId
--End Corp Acct Discount

--Begin Item Attributes
TRUNCATE TABLE RetailSlnSch.ItemSpec
INSERT RetailSlnSch.ItemSpec(ClientId, ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, ItemId, SeqNum, ShowValue)
SELECT ItemSpecMaster.ClientId, ItemSpecMaster.ItemSpecMasterId, '' ItemAttribUnitValue, '' ItemAttribValue, Item.ItemId, SeqNum, 0 AS ShowValue
  FROM RetailSlnSch.Item, RetailSlnSch.ItemSpecMaster
ORDER BY ItemId, SeqNum

/*Update Products Begin-------------------------------------------------------------------------------------------------------------*/
BEGIN

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[HSN Code]
FROM dbo.DivineBija_Products WHERE [HSN Code] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 5

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Prod Code]
FROM dbo.DivineBija_Products WHERE [Prod Code] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 6

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = REPLACE(DivineBija_Products.[Central GST], '%', '')
FROM dbo.DivineBija_Products WHERE [Central GST] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 16

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = REPLACE(DivineBija_Products.[State GST], '%', '')
FROM dbo.DivineBija_Products WHERE [State GST] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 17

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = CAST(REPLACE(DivineBija_Products.[Central GST], '%', '') AS FLOAT) + CAST(REPLACE(DivineBija_Products.[State GST], '%', '') AS FLOAT)
FROM dbo.DivineBija_Products WHERE [Central GST] <> '' AND DivineBija_Products.[State GST] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 18

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Product Weight], ItemSpecUnitValue = 100, ShowValue = CASE [Show Weight] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Product Weight] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 4 AND [Product Weight Unit] = 'G'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Product Weight], ItemSpecUnitValue = 200, ShowValue = CASE [Show Weight] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Product Weight] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 4 AND [Product Weight Unit] = 'KG'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Fluid Vol], ItemSpecUnitValue = 100, ShowValue = CASE [Show Volume] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Fluid Vol] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 7 AND [Fluid Vol Unit] = 'L'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Product Length], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Length] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 1

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Product Width], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Width] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 2

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Product Height], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Product Height] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 3

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.Size, ShowValue = CASE [Show Size] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE Size <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 10

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.Color, ShowValue = CASE [Show Color] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE Color <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 8

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Count], ItemSpecUnitValue = 100, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Cone(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Count], ItemSpecUnitValue = 200, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Cup(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Count], ItemSpecUnitValue = 300, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Piece(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Count], ItemSpecUnitValue = 400, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Set(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Count], ItemSpecUnitValue = 500, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Stem(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Count], ItemSpecUnitValue = 600, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Stick(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.Material, ShowValue = CASE [Show Material] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE Material <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 11

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Calc Product Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Calc Product Weight] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 15

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Weight Attribute], ShowValue = CASE [Show Weight Attribute] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Weight Attribute] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 19

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Packet], ItemSpecUnitValue = 100, ShowValue = CASE [Show Packet] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Packet] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 20 AND [Packet Unit] = 'Packet(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Packet], ItemSpecUnitValue = 200, ShowValue = CASE [Show Packet] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Packet] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 20 AND [Packet Unit] = 'Bag(s)'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.Package, ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE Package <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 9 AND [Package] = 'Box'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.Package, ItemSpecUnitValue = 200    
FROM dbo.DivineBija_Products WHERE Package <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 9 AND [Package] = 'Container'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.Package, ItemSpecUnitValue = 300    
FROM dbo.DivineBija_Products WHERE Package <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 9 AND [Package] = 'Packet'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Package Length], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Package Length] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 21

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Package Width], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Package Width] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 22

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Package Height], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Package Height] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 23

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Volumetric Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Volumetric Weight] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 24

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Volumetric Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Volumetric Weight] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 25

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Calc Product Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Products WHERE [Calc Product Weight] <> '' AND [Calc Product Weight] > [Volumetric Weight] AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 25

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Specs], ShowValue = CASE [Show Specs] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Products WHERE [Specs] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 26

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Products.[Keypoints]
FROM dbo.DivineBija_Products WHERE [Keypoints] <> '' AND ItemSpec.ItemId = DivineBija_Products.Id AND ItemSpecMasterId = 27

END
/*Update Products End-------------------------------------------------------------------------------------------------------------*/

/*Update Book Begin-----------------------------------------------------------------------------------------------------------------*/
BEGIN

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[HSN Code]
FROM dbo.DivineBija_Books WHERE [HSN Code] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 5

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Prod Code]
FROM dbo.DivineBija_Books WHERE [Prod Code] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 6

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = REPLACE(DivineBija_Books.[Central GST], '%', '')
FROM dbo.DivineBija_Books WHERE [Central GST] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 16

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = REPLACE(DivineBija_Books.[State GST], '%', '')
FROM dbo.DivineBija_Books WHERE [State GST] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 17

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = CAST(REPLACE(DivineBija_Books.[Central GST], '%', '') AS FLOAT) + CAST(REPLACE(DivineBija_Books.[State GST], '%', '') AS FLOAT)
FROM dbo.DivineBija_Books WHERE [Central GST] <> '' AND DivineBija_Books.[State GST] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 18

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Product Weight], ItemSpecUnitValue = 100--, ShowValue = CASE [Show Weight] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Books WHERE [Product Weight] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 4 AND [Product Weight Unit] = 'G'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Product Weight], ItemSpecUnitValue = 200--, ShowValue = CASE [Show Weight] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Books WHERE [Product Weight] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 4 AND [Product Weight Unit] = 'KG'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Fluid Vol], ItemSpecUnitValue = 100, ShowValue = CASE [Show Volume] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Fluid Vol] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 7 AND [Fluid Vol Unit] = 'L'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Product Length], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Length] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 1

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Product Width], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Width] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 2

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Product Height], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Product Height] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 3

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Size, ShowValue = CASE [Show Size] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE Size <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 10

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Color, ShowValue = CASE [Show Color] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE Color <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 8

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Count], ItemSpecUnitValue = 100, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Cone(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Count], ItemSpecUnitValue = 200, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Cup(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Count], ItemSpecUnitValue = 300, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Piece(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Count], ItemSpecUnitValue = 400, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Set(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Count], ItemSpecUnitValue = 500, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Stem(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Count], ItemSpecUnitValue = 600, ShowValue = CASE [Show Count] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Count] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 12 AND [Count Unit] = 'Stick(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Material, ShowValue = CASE [Show Material] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE Material <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 11

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Calc Product Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Calc Product Weight] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 15

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Weight Attribute], ShowValue = CASE [Show Weight Attribute] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Weight Attribute] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 19

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Packet], ItemSpecUnitValue = 100, ShowValue = CASE [Show Packet] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Packet] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 20 AND [Packet Unit] = 'Packet(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Packet], ItemSpecUnitValue = 200, ShowValue = CASE [Show Packet] WHEN 'Yes' THEN 1 ELSE 0 END
--FROM dbo.DivineBija_Books WHERE [Packet] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 20 AND [Packet Unit] = 'Bag(s)'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Package, ItemSpecUnitValue = 100
--FROM dbo.DivineBija_Books WHERE Package <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 9 AND [Package] = 'Box'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Package, ItemSpecUnitValue = 200    
--FROM dbo.DivineBija_Books WHERE Package <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 9 AND [Package] = 'Container'

--UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Package, ItemSpecUnitValue = 300    
--FROM dbo.DivineBija_Books WHERE Package <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 9 AND [Package] = 'Packet'

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Package Length], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Package Length] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 21

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Package Width], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Package Width] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 22

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Package Height], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Package Height] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 23

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Volumetric Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Volumetric Weight] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 24

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Volumetric Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Volumetric Weight] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 25

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Calc Product Weight], ItemSpecUnitValue = 100
FROM dbo.DivineBija_Books WHERE [Calc Product Weight] <> '' AND [Calc Product Weight] > [Volumetric Weight] AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 25

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.Publisher, ShowValue = 0
FROM dbo.DivineBija_Books WHERE Publisher <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 13

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.PageCount, ShowValue = 0
FROM dbo.DivineBija_Books WHERE PageCount <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 14

UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = DivineBija_Books.[Language], ShowValue = CASE [Show Language] WHEN 'Yes' THEN 1 ELSE 0 END
FROM dbo.DivineBija_Books WHERE DivineBija_Books.[Language] <> '' AND ItemSpec.ItemId = DivineBija_Books.Id AND ItemSpecMasterId = 28

END
/*Update Books End------------------------------------------------------------------------------------------------------------------*/
--End Item Attributes

--Begin Item Bundle
UPDATE dbo.DivineBija_ItemBundle SET BundleItemId = NULL, ItemId = NULL
UPDATE dbo.DivineBija_ItemBundle SET BundleItemId = Item.ItemId FROM RetailSlnSch.Item WHERE BundleUniqueDescription = Item.ItemUniqueDesc AND BundleItemId IS NULL
UPDATE dbo.DivineBija_ItemBundle SET ItemId = Item.ItemId FROM RetailSlnSch.Item WHERE ItemUniqueDescription = Item.ItemUniqueDesc AND DivineBija_ItemBundle.ItemId IS NULL

SELECT DISTINCT BundleUniqueDescription FROM dbo.DivineBija_ItemBundle WHERE BundleItemId IS NULL
SELECT DISTINCT ItemUniqueDescription FROM dbo.DivineBija_ItemBundle WHERE ItemId IS NULL

TRUNCATE TABLE RetailSlnSch.ItemBundleItem

INSERT RetailSlnSch.ItemBundleItem(ClientId, BundleItemId, SeqNum, ItemId, Quantity)
SELECT @ClientId AS ClientId, ItemBundle.ItemId AS BundleItemId, CAST(DivineBija_ItemBundle.[Seq Num] AS FLOAT) AS SeqNum, Item.ItemId, DivineBija_ItemBundle.Quantity
  FROM dbo.DivineBija_ItemBundle
INNER JOIN RetailSlnSch.Item AS ItemBundle ON DivineBija_ItemBundle.BundleUniqueDescription = ItemBundle.ItemUniqueDesc
INNER JOIN RetailSlnSch.Item ON DivineBija_ItemBundle.ItemUniqueDescription = Item.ItemUniqueDesc
ORDER BY ItemBundle.ItemId, CAST(DivineBija_ItemBundle.[Seq Num] AS FLOAT)
--End Item Bundle

--Begin SearchList & SearchResult
BEGIN
TRUNCATE TABLE RetailSlnSch.SearchMetaData
TRUNCATE TABLE RetailSlnSch.SearchKeyword

DECLARE @EntityId BIGINT, @SearchCharIndex BIGINT, @SearchKeywords NVARCHAR(MAX), @SearchKeywordText NVARCHAR(512)
DECLARE @SearchKeywordId BIGINT, @EntityTypeNameDesc NVARCHAR(50)

DECLARE SearchKeywordMetaDataCursor CURSOR FOR
SELECT Id AS EntityId, 'CATEGORY' AS EntityTypeNameDesc, [Search Keywords] FROM dbo.DivineBija_Categories WHERE Active = 1 AND [Search Keywords] <> ''
UNION
SELECT Id AS EntityId, 'ITEM' AS EntityTypeNameDesc, [Search Keywords] FROM dbo.DivineBija_Products WHERE [Search Keywords] <> ''
UNION
SELECT Id AS EntityId, 'ITEM' AS EntityTypeNameDesc, [Search Keywords] FROM dbo.DivineBija_Books WHERE [Search Keywords] <> ''
ORDER BY EntityTypeNameDesc, Id

OPEN SearchKeywordMetaDataCursor

FETCH SearchKeywordMetaDataCursor INTO @EntityId, @EntityTypeNameDesc, @SearchKeywords

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @SearchCharIndex = CHARINDEX(' ', @SearchKeywords)
	WHILE @SearchCharIndex > 0
	BEGIN
	    SET @SearchKeywordText = SUBSTRING(@SearchKeywords, 1, @SearchCharIndex - 1)
		SET @SearchKeywords = SUBSTRING(@SearchKeywords, @SearchCharIndex + 1, LEN(@SearchKeywords))
		SET @SearchKeywordId = NULL
		SELECT @SearchKeywordId = SearchKeyword.SearchKeywordId FROM RetailSlnSch.SearchKeyword WHERE SearchKeyword.SearchKeywordText = @SearchKeywordText
		IF @SearchKeywordId IS NULL
		BEGIN
		    INSERT RetailSlnSch.SearchKeyword(ClientId, SearchKeywordText)
			SELECT @ClientId AS ClientId, LOWER(@SearchKeywordText)
			SET @SearchKeywordId = @@IDENTITY
		END
		INSERT RetailSlnSch.SearchMetaData(ClientId, SearchKeywordId, EntityTypeNameDesc, EntityId, SeqNum)
		SELECT @ClientId AS ClientId, @SearchKeywordId AS SearchKeywordId, @EntityTypeNameDesc AS EntityTypeNameDesc, @EntityId AS EntityId, 1 AS SeqNum
		SET @SearchCharIndex = CHARINDEX(' ', @SearchKeywords)
	END
    FETCH SearchKeywordMetaDataCursor INTO @EntityId, @EntityTypeNameDesc, @SearchKeywords
END

CLOSE SearchKeywordMetaDataCursor
DEALLOCATE SearchKeywordMetaDataCursor
END
--End SearchList & SearchResult

--Begin ItemInfo
TRUNCATE TABLE RetailSlnSch.ItemInfo
INSERT RetailSlnSch.ItemInfo(ClientId, ItemId, ItemInfoLabelText, ItemInfoText, SeqNum)
SELECT Item.ClientId, Item.ItemId, 'Specification(s)' AS ItemSpecLabelText, '<h2>Specifications<h2><p1>Sample Specifications</p1>' AS ItemSpecText, 1 AS SeqNum
  FROM RetailSlnSch.Item
ORDER BY ItemId, SeqNum
--SELECT Item.ClientId, Item.ItemId, 'Attribute(s)' AS ItemSpecLabelText, '' AS ItemSpecText, 1 AS SeqNum
--  FROM RetailSlnSch.Item
--UNION
--SELECT Item.ClientId, Item.ItemId, '' AS ItemSpecLabelText, '' AS ItemSpecText, 3 AS SeqNum
--  FROM RetailSlnSch.Item
--SELECT Item.ClientId, Item.ItemId, '' AS ItemSpecLabelText, '' AS ItemSpecText, 4 AS SeqNum
--  FROM RetailSlnSch.Item
--End ItemInfo
