DELETE RetailSlnSch.Item
SET IDENTITY_INSERT RetailSlnSch.Item ON
INSERT RetailSlnSch.Item(ItemId, ClientId, ImageName, ItemDesc, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, ItemTypeId, UploadImageFileName)
SELECT ItemId, 3 AS ClientId, 'Item' + CAST(ItemId AS VARCHAR(20)) + '.jpg' AS ImageName, Description AS ItemDesc, Rate AS ItemRate, Description AS ItemShortDesc, 5 AS ItemStarCount, 100 AS ItemStatusId, CASE WHEN ItemId < 233 THEN 100 ELSE 200 END AS ItemTypeId, '' AS UploadImageFileName
FROM dbo.DivineBijaProducts WHERE [Item Type] = 'ITEM'
ORDER BY CAST(ItemId AS INT)
SET IDENTITY_INSERT RetailSlnSch.Item OFF

TRUNCATE TABLE RetailSlnSch.CategoryItemHier
SET IDENTITY_INSERT RetailSlnSch.CategoryItemHier ON
INSERT RetailSlnSch.CategoryItemHier(CategoryItemHierId, ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 1 AS CategoryItemHierId, 3 AS ClientId, 0 AS ParentCategoryId, 1 AS SeqNum, 1 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 2 AS CategoryItemHierId, 3 AS ClientId, 0 AS ParentCategoryId, 2 AS SeqNum, 3 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 3 AS CategoryItemHierId, 3 AS ClientId, 0 AS ParentCategoryId, 3 AS SeqNum, 8 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem UNION

SELECT 4 AS CategoryItemHierId, 3 AS ClientId, 1 AS ParentCategoryId, 1 AS SeqNum, NULL AS CategoryId, 1 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 5 AS CategoryItemHierId, 3 AS ClientId, 1 AS ParentCategoryId, 2 AS SeqNum, NULL AS CategoryId, 2 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 6 AS CategoryItemHierId, 3 AS ClientId, 1 AS ParentCategoryId, 3 AS SeqNum, NULL AS CategoryId, 3 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 7 AS CategoryItemHierId, 3 AS ClientId, 1 AS ParentCategoryId, 4 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION

SELECT 8 AS CategoryItemHierId, 3 AS ClientId, 3 AS ParentCategoryId, 1 AS SeqNum, NULL AS CategoryId, 11 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 9 AS CategoryItemHierId, 3 AS ClientId, 3 AS ParentCategoryId, 2 AS SeqNum, NULL AS CategoryId, 12 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 10 AS CategoryItemHierId, 3 AS ClientId, 3 AS ParentCategoryId, 3 AS SeqNum, NULL AS CategoryId, 13 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
SELECT 11 AS CategoryItemHierId, 3 AS ClientId, 3 AS ParentCategoryId, 4 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION

SELECT 12 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 1 AS SeqNum, 100 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 13 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 2 AS SeqNum, 101 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 14 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 3 AS SeqNum, 102 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 15 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 4 AS SeqNum, 103 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 16 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 5 AS SeqNum, 6 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 17 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 6 AS SeqNum, 5 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 18 AS CategoryItemHierId, 3 AS ClientId, 8 AS ParentCategoryId, 7 AS SeqNum, 7 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Category' AS CategoryOrItem UNION

SELECT 19 AS CategoryItemHierId, 3 AS ClientId, 5 AS ParentCategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 20 AS CategoryItemHierId, 3 AS ClientId, 5 AS ParentCategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem
SET IDENTITY_INSERT RetailSlnSch.CategoryItemHier OFF

--Religious Books
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 100 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 1 AND 50

--Kids Books
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 101 AS ParentCategoryId, ItemId - 25 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 51 AND 100

--Homa
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 102 AS ParentCategoryId, ItemId - 50 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 101 AND 150

--Vastram
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 103 AS ParentCategoryId, ItemId - 75 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 151 AND 200

--Other
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 6 AS ParentCategoryId, ItemId - 100 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 201 AND 212

--Featured Items All
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 2 AS ParentCategoryId, ItemId - 0 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 1 AND 25

--New Arrivals All
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT 3 AS ClientId, 4 AS ParentCategoryId, ItemId - 25 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM RetailSlnSch.Item WHERE ItemId BETWEEN 26 AND 60

TRUNCATE TABLE RetailSlnSch.ItemAttrib
INSERT RetailSlnSch.ItemAttrib(ItemId, ItemAttribMasterId, ItemAttribValue, ItemAttribUnitValue, SeqNum)
SELECT Item.ItemId, ItemAttribMaster.ItemAttribMasterId, '' ItemAttribValue, '' ItemAttribUnitValue, SeqNum
  FROM RetailSlnSch.Item, RetailSlnSch.ItemAttribMaster
ORDER BY ItemId, SeqNum
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBijaProducts.Length, ItemAttribUnitValue = 100
FROM dbo.DivineBijaProducts WHERE Length <> '' AND ItemAttrib.ItemId = DivineBijaProducts.ItemId AND ItemAttribMasterId = 1
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBijaProducts.Width, ItemAttribUnitValue = 100
FROM dbo.DivineBijaProducts WHERE Width <> '' AND ItemAttrib.ItemId = DivineBijaProducts.ItemId AND ItemAttribMasterId = 2
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBijaProducts.Height, ItemAttribUnitValue = 100
FROM dbo.DivineBijaProducts WHERE Height <> '' AND ItemAttrib.ItemId = DivineBijaProducts.ItemId AND ItemAttribMasterId = 3
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBijaProducts.Width, ItemAttribUnitValue = 100
FROM dbo.DivineBijaProducts WHERE Width <> '' AND ItemAttrib.ItemId = DivineBijaProducts.ItemId AND ItemAttribMasterId = 4
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBijaProducts.[HSN Code]
FROM dbo.DivineBijaProducts WHERE [HSN Code] <> '' AND ItemAttrib.ItemId = DivineBijaProducts.ItemId AND ItemAttribMasterId = 5
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBijaProducts.[Prod Code]
FROM dbo.DivineBijaProducts WHERE [Prod Code] <> '' AND ItemAttrib.ItemId = DivineBijaProducts.ItemId AND ItemAttribMasterId = 6

/*
SET IDENTITY_INSERT RetailSlnSch.Item OFF
SELECT * FROM RetailSlnSch.ItemAttribMaster
SELECT * FROM RetailSlnSch.ItemAttrib ORDER BY ItemId, SeqNum
*/
/* May 16 2023
SELECT  * INTO Item_20230516 FROM RetailSlnSch.Item
*/
/*
SELECT * FROM RetailSlnSch.ItemAttrib WHERE ItemAttribValue = '0' AND ItemAttribMasterId = 1

SELECT DISTINCT [Width Unit] FROM dbo.DivineBijaProducts WHERE Width <> ''
SELECT ItemId, Width, [Width Unit] FROM dbo.DivineBijaProducts WHERE Width <> ''
SELECT  * FROM RetailSlnSch.ItemAttrib

SET IDENTITY_INSERT RetailSlnSch.Item ON
INSERT RetailSlnSch.Item(ItemId, ClientId, ImageName, ItemDesc, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, UploadImageFileName)
SELECT Item_20230224.ItemId, 1 AS ClientId, ImageName, Description AS ItemDesc, Rate AS ItemRate, Description AS ItemShortDesc, 5 AS ItemStarCount, 100 AS ItemStatusId, '' AS UploadImageFileName
FROM dbo.DivineBijaProducts
INNER JOIN dbo.Item_20230224 ON DivineBijaProducts.ItemId = Item_20230224.ItemId
ORDER BY 1
SET IDENTITY_INSERT RetailSlnSch.Item OFF
TRUNCATE TABLE RetailSlnSch.CategoryItem
INSERT RetailSlnSch.CategoryItem(ClientId, CategoryId, SeqNum, ItemId)
SELECT 1 ClientId, 1 CategoryId, 1 SeqNum, 9 ItemId UNION
SELECT 1 ClientId, 1 CategoryId, 2 SeqNum, 18 ItemId UNION
SELECT 1 ClientId, 1 CategoryId, 3 SeqNum, 27 ItemId UNION
SELECT 1 ClientId, 1 CategoryId, 4 SeqNum, 36 ItemId UNION
SELECT 1 ClientId, 2 CategoryId, ItemId - 0 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 1 AND 18 UNION
SELECT 1 ClientId, 3 CategoryId, ItemId - 18 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 19 AND 36 UNION
SELECT 1 ClientId, 4 CategoryId, ItemId - 36 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 37 AND 54 UNION
SELECT 1 ClientId, 5 CategoryId, ItemId - 54 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 55 AND 72 UNION
SELECT 1 ClientId, 6 CategoryId, ItemId - 72 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 73 AND 90 UNION
SELECT 1 ClientId, 7 CategoryId, ItemId - 90 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 91 AND 108 UNION
SELECT 1 ClientId, 8 CategoryId, ItemId - 108 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 109 AND 126 UNION
SELECT 1 ClientId, 9 CategoryId, ItemId - 126 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 127 AND 144 UNION
SELECT 1 ClientId, 10 CategoryId, ItemId - 144 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 145 AND 162 UNION
SELECT 1 ClientId, 11 CategoryId, ItemId - 162 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 163 AND 180 UNION
SELECT 1 ClientId, 12 CategoryId, ItemId - 180 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 181 AND 198 UNION
SELECT 1 ClientId, 13 CategoryId, ItemId - 198 SeqNum, ItemId FROM RetailSlnSch.Item WHERE ItemId BETWEEN 199 AND 215 --UNION

TRUNCATE TABLE RetailSlnSch.ItemImage
INSERT RetailSlnSch.ItemImage(ImageDesc, ItemId, SeqNum)
SELECT 'Front View' AS ImageDesc, ItemId, 1 AS SeqNum FROM RetailSlnSch.Item UNION
SELECT 'Top View' AS ImageDesc, ItemId, 2 AS SeqNum FROM RetailSlnSch.Item UNION
SELECT 'Side View' AS ImageDesc, ItemId, 3 AS SeqNum FROM RetailSlnSch.Item
ORDER BY 2, 3

TRUNCATE TABLE RetailSlnSch.ItemImageSrcSet
INSERT RetailSlnSch.ItemImageSrcSet(ItemImageId, SeqNum, ImageName)
SELECT ItemImageId, 1 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 1 UNION
SELECT ItemImageId, 2 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 1 UNION
SELECT ItemImageId, 3 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 1 UNION
SELECT ItemImageId, 1 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 2 UNION
SELECT ItemImageId, 2 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 2 UNION
SELECT ItemImageId, 3 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 2 UNION
SELECT ItemImageId, 1 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 3 UNION
SELECT ItemImageId, 2 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 3 UNION
SELECT ItemImageId, 3 SeqNum, '' ImageName FROM RetailSlnSch.ItemImage WHERE SeqNum = 3 --UNION
ORDER BY 1, 2

UPDATE RetailSlnSch.ItemImageSrcSet SET ImageName = ItemImage_20230227.ImageName
FROM dbo.ItemImage_20230227 WHERE ItemImageSrcSet.ItemImageSrcSetId = ItemImage_20230227.ItemImageId --AND ItemImageSrcSet.

SELECT * FROM ItemImage_20230227
SELECT * FROM RetailSlnSch.ItemImage

SELECT * FROM RetailSlnSch.Category
SELECT * FROM RetailSlnSch.CategoryItem

UPDATE RetailSlnSch.ItemImageSrcSet SET ImageHeight = 450, ImageWidth = 450 WHERE SeqNum = 1
UPDATE RetailSlnSch.ItemImageSrcSet SET ImageHeight = 630, ImageWidth = 630 WHERE SeqNum = 2
UPDATE RetailSlnSch.ItemImageSrcSet SET ImageHeight = 810, ImageWidth = 810 WHERE SeqNum = 3
*/
