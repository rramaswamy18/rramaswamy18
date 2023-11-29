--1_PopulateItem_DivineBija.com.sql
use [DivineBija.com]
/*
SELECT Category, COUNT(*) FROM dbo.DivineBija_Products GROUP BY Category
UNION
SELECT Category, COUNT(*) FROM dbo.DivineBija_Books GROUP BY Category

SELECT DISTINCT [Category] FROM DivineBija_Products
    SELECT DivineBija_Products.ItemId, DivineBija_Products.Description, DivineBija_Products_3.Description
      FROM DivineBija_Products
INNER JOIN DivineBija_Products_3
        ON DivineBija_Products.ItemId = DivineBija_Products_3.ItemId
       AND DivineBija_Products.Description = DivineBija_Products_3.Description
       AND DivineBija_Products.[Item Type] = DivineBija_Products_3.[Item Type]
     WHERE DivineBija_Products.[Item Type] <> 'Books'
  ORDER BY DivineBija_Products.ItemId
TRUNCATE TABLE RetailSlnSch.ItemCost
INSERT RetailSlnSch.ItemCost(ItemId, ItemCostTypeId, ItemUnitRate, ItemUnitRateDiscount, MinQty)
SELECT ItemId, ItemCostTypeId, ItemUnitRate, ItemUnitRateDiscount, MinQty FROM [DivineBija.in_1].RetailSlnSch.ItemCost
TRUNCATE TABLE RetailSlnSch.ItemCost
INSERT RetailSlnSch.ItemCost(ItemId, ItemCostTypeId, ItemRate, MinQty)
SELECT ItemId, 100 AS ItemCostTypeId, zzz_ItemRate AS ItemRate, 1 AS MinQty FROM RetailSlnSch.Item
UNION
SELECT ItemId, 200 AS ItemCostTypeId, zzz_ItemRate * 0.9 AS ItemRate, 9 AS MinQty FROM RetailSlnSch.Item
ORDER BY 1, 2
SELECT * INTO CategoryItemHier_20230927 FROM [DivineBija.in_1]..CategoryItemHier_20230927
SELECT * INTO DivineBija_Books FROM [DivineBija.in_1]..DivineBija_Books
SELECT * INTO DivineBija_Books_0 FROM [DivineBija.in_1]..DivineBija_Books_0
SELECT * INTO DivineBija_Books_1 FROM [DivineBija.in_1]..DivineBija_Books_1
SELECT * INTO DivineBija_Books_2 FROM [DivineBija.in_1]..DivineBija_Books_2
SELECT * INTO DivineBija_ItemCost FROM [DivineBija.in_1]..DivineBija_ItemCost
SELECT * INTO DivineBija_Products FROM [DivineBija.in_1]..DivineBija_Products
SELECT * INTO DivineBija_Products_1 FROM [DivineBija.in_1]..DivineBija_Products_1
SELECT * INTO DivineBija_Products_2 FROM [DivineBija.in_1]..DivineBija_Products_2
SELECT * INTO Item_20230927 FROM [DivineBija.in_1]..Item_20230927

--UPDATE RetailSlnSch.Item SET ImageAvailable = 1 WHERE ItemId IN(1,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,45,46,47,48,49,50,51,52,53,54,55,56,57,58,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,122,123,124,125,126,127,128,129,130,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,161,162,163,165,166,167,168,169,171,172,178,181,182,184,185,186,187,188,189,190,191,192,193,194,195,196,197,200,201,203,205,206,207,212,213,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,334,335,336,337,338,339,341,343,345)
SELECT * FROM RetailSlnSch.Item WHERE ImageAvailable IS NULL AND ItemTypeId IN(100, 200) ORDER BY ItemId
SELECT ItemTypeId, MIN(ItemId), MAX(ItemId) FROM RetailSlnSch.Item GROUP BY ItemTypeId
CREATE TABLE [dbo].[DivineBija_Products](
    [ItemId] [bigint] NOT NULL,
    [Item Type] [nvarchar](50) NULL,
    [Description] [nvarchar](350) NULL,
    [Rate] [nvarchar](50) NULL,
    [MSRP] [nvarchar](50) NULL,
    [State GST] [nvarchar](50) NULL,
    [Central GST] [nvarchar](50) NULL,
    [HSN Code] [nvarchar](50) NULL,
    [Prod Code] [nvarchar](50) NULL,
    [Length] [nvarchar](50) NULL,
    [Length Unit] [nvarchar](50) NULL,
    [Width] [nvarchar](50) NULL,
    [Width Unit] [nvarchar](50) NULL,
    [Height] [nvarchar](50) NULL,
    [Height Unit] [nvarchar](50) NULL,
    [Weight] [nvarchar](50) NULL,
    [Weight Unit] [nvarchar](50) NULL,
    [Fluid Vol] [nvarchar](50) NULL,
    [Fluid Vol Unit] [nvarchar](50) NULL,
    [Color] [nvarchar](50) NULL,
    [Package] [nvarchar](50) NULL,
    [Size] [nvarchar](50) NULL,
    [Material] [nvarchar](50) NULL,
    [Count] [nvarchar](50) NULL,
    [Count Unit] [nvarchar](50) NULL,
    [Stock] [nvarchar](50) NULL,
    [Item 180] [nvarchar](50) NULL,
    [Top 450] [nvarchar](50) NULL,
    [Top 630] [nvarchar](50) NULL,
    [Top 810] [nvarchar](50) NULL,
    [Front 450] [nvarchar](50) NULL,
    [Front 630] [nvarchar](50) NULL,
    [Front 810] [nvarchar](50) NULL,
    [Side 450] [nvarchar](50) NULL,
    [Side 630] [nvarchar](50) NULL,
    [Side 810] [nvarchar](50) NULL,
    [AddDateTime] [datetime] NOT NULL,
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_DivineBija_Products] PRIMARY KEY NONCLUSTERED([Id] ASC)
)
ALTER TABLE [dbo].[DivineBija_Products] ADD  CONSTRAINT [DF_DivineBija_Products_AddDateTime]  DEFAULT (getdate()) FOR [AddDateTime]
*/
/*
TRUNCATE TABLE DivineBija_Products
SELECT * FROM DivineBija_Books ORDER BY Id
SELECT * FROM DivineBija_Products ORDER BY Id
SELECT Item.ItemId, DivineBija_Products.* FROM DivineBija_Products INNER JOIN RetailSlnSch.Item ON Id = ProductItemId WHERE ItemTypeId = 100 ORDER BY Item.ItemId
SELECT DivineBija_Books.* FROM DivineBija_Books INNER JOIN RetailSlnSch.Item ON Id = ProductItemId WHERE ItemTypeId = 200 ORDER BY Id
*/
TRUNCATE TABLE RetailSlnSch.CategoryItemHier
DELETE RetailSlnSch.Item
DBCC CHECKIDENT ('RetailSlnSch.Item', RESEED, 0);

--Begin Items
DECLARE @ClientId BIGINT = 98
--Type -> Item
SET IDENTITY_INSERT RetailSlnSch.Item ON
INSERT RetailSlnSch.Item(ItemId, ClientId, ItemDesc, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, ItemTypeId, ProductItemId, UploadImageFileName)
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(Description)) AS ItemDesc, [Rate USD] AS ItemRate, RTRIM(LTRIM(Description)) AS ItemShortDesc, 5 AS ItemStarCount, 100 AS ItemStatusId, 100 AS ItemTypeId, ItemId AS ProductItemId, [Item 180] + '.jpg' AS UploadImageFileName
FROM dbo.DivineBija_Products WHERE [Item Type] = 'ITEMS'
--GROUP BY RTRIM(LTRIM(Description)), [Rate USD]
--ORDER BY RTRIM(LTRIM(Description)), [Rate USD]
ORDER BY Id

--Type --> Item Bundle
INSERT RetailSlnSch.Item(ItemId, ClientId, ItemDesc, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, ItemTypeId, ProductItemId, UploadImageFileName)
SELECT Id, @ClientId AS ClientId, RTRIM(LTRIM(Description)) AS ItemDesc, [Rate USD] AS ItemRate, RTRIM(LTRIM(Description)) AS ItemShortDesc, 5 AS ItemStarCount, 100 AS ItemStatusId, 300 AS ItemTypeId, ItemId AS ProductItemId, [Item 180] + '.jpg' AS UploadImageFileName
FROM dbo.DivineBija_Products WHERE [Item Type] = 'BUNDLE'
--GROUP BY RTRIM(LTRIM(Description)), [Rate USD]
--ORDER BY RTRIM(LTRIM(Description)), [Rate USD]
ORDER BY Id

--Type --> Books
INSERT RetailSlnSch.Item(ItemId, ClientId, ItemDesc, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, ItemTypeId, ProductItemId, UploadImageFileName)
SELECT Id + 220, @ClientId AS ClientId, RTRIM(LTRIM(ProductDesc)) AS ItemDesc, RateUSD AS ItemRate, RTRIM(LTRIM(ProductDesc)) AS ItemShortDesc, 5 AS ItemStarCount, 100 AS ItemStatusId, 200 AS ItemTypeId, ItemId AS ProductItemId, Image1 AS UploadImageFileName
FROM dbo.DivineBija_Books --WHERE [Item Type] = 'ITEM'
--GROUP BY RTRIM(LTRIM(ProductDesc)), MRP
--ORDER BY RTRIM(LTRIM(ProductDesc)), MRP
ORDER BY Id

SET IDENTITY_INSERT RetailSlnSch.Item OFF
--End Items

--Begin CategoryHierItem
TRUNCATE TABLE RetailSlnSch.CategoryItemHier

--Start
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 0 AS ParentCategoryId, 1 AS SeqNum, 8 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem

--Categories
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 5 AS CategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, 'Recursive' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 5 AS CategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, 'Recursive' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 1 AS SeqNum, 102 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 2 AS SeqNum, 118 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 3 AS SeqNum, 113 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 4 AS SeqNum, 114 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 5 AS SeqNum, 115 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 6 AS SeqNum, 116 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 7 AS SeqNum, 117 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 8 AS SeqNum, 7 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 9 AS SeqNum, 100 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 10 AS SeqNum, 101 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 8 AS CategoryId, 11 AS SeqNum, 5 AS CategoryId, NULL AS ItemId, '' AS ProcessType, 'Category' AS CategoryOrItem

--Homa
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 102 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 102 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Homa Items'
ORDER BY Id

--Pooja Items
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 118 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 118 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Pooja Items'
ORDER BY Id

--Abhishekam
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 113 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 113 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Abhishegam'
ORDER BY Id

--Deepam
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 114 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 114 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Deepam'
ORDER BY Id

--Rituals
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 115 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 115 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Rituals'
ORDER BY Id

--Sumangali
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 116 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 116 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Sumangali'
ORDER BY Id

--Thamboolam
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 117 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 117 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE Category = 'Thamboolam'
ORDER BY Id

--Item Bundle
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 7 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT 98 AS ClientId, 7 AS ParentCategoryId, Id - 0 AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Products WHERE [Item Type] = 'Bundle'
ORDER BY Id

--Religious Books
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 100 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 100 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, Id + 220 AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books WHERE Category = 'Religious Books'
ORDER BY Id

--Kids Books
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 101 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 101 AS ParentCategoryId, ItemId AS SeqNum, NULL AS CategoryId, Id AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
FROM DivineBija_Books WHERE Category = 'Kids Books'
ORDER BY Id

--Other
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 6 AS ParentCategoryId, ItemId - 100 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId BETWEEN 208 AND 215

--Item Bundle
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 7 AS ParentCategoryId, ItemId - 338 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId BETWEEN 341 AND 346

--Featured Items All
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 2 AS ParentCategoryId, ItemId - 0 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId BETWEEN 1 AND 9

--New Arrivals All
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 4 AS ParentCategoryId, ItemId - 20 AS SeqNum, NULL AS CategoryId, ItemId AS ItemId, '' AS ProcessType, 'Item' AS CategoryOrItem
--FROM RetailSlnSch.Item WHERE ItemId BETWEEN 21 AND 29

--Featured Items Summary
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 1 AS SeqNum, NULL AS CategoryId, 1 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 2 AS SeqNum, NULL AS CategoryId, 2 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 3 AS SeqNum, NULL AS CategoryId, 3 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 1 AS ParentCategoryId, 4 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem

--New Arrivals Summary
--INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 1 AS SeqNum, NULL AS CategoryId, 11 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 2 AS SeqNum, NULL AS CategoryId, 12 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 3 AS SeqNum, NULL AS CategoryId, 13 AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem UNION
--SELECT @ClientId AS ClientId, 3 AS ParentCategoryId, 4 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, '' ProcessType, 'Item' AS CategoryOrItem

--Featured Items  & New Arrivals
INSERT RetailSlnSch.CategoryItemHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemId, ProcessType, CategoryOrItem)
SELECT @ClientId AS ClientId, 5 AS ParentCategoryId, 1 AS SeqNum, 2 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem UNION
SELECT @ClientId AS ClientId, 5 AS ParentCategoryId, 2 AS SeqNum, 4 AS CategoryId, NULL AS ItemId, 'Recursive' ProcessType, 'Category' AS CategoryOrItem

--End CategoryHierItem

TRUNCATE TABLE RetailSlnSch.ItemAttrib
INSERT RetailSlnSch.ItemAttrib(ItemId, ItemAttribMasterId, ItemAttribValue, ItemAttribUnitValue, SeqNum)
SELECT Item.ItemId, ItemAttribMaster.ItemAttribMasterId, '' ItemAttribValue, '' ItemAttribUnitValue, SeqNum
  FROM RetailSlnSch.Item, RetailSlnSch.ItemAttribMaster
ORDER BY ItemId, SeqNum
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Length, ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE Length <> '' AND ItemAttrib.ItemId = DivineBija_Products.ItemId AND ItemAttribMasterId = 1
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Width, ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE Width <> '' AND ItemAttrib.ItemId = DivineBija_Products.ItemId AND ItemAttribMasterId = 2
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Height, ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE Height <> '' AND ItemAttrib.ItemId = DivineBija_Products.ItemId AND ItemAttribMasterId = 3
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.Width, ItemAttribUnitValue = 100
FROM dbo.DivineBija_Products WHERE Width <> '' AND ItemAttrib.ItemId = DivineBija_Products.ItemId AND ItemAttribMasterId = 4
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[HSN Code]
FROM dbo.DivineBija_Products WHERE [HSN Code] <> '' AND ItemAttrib.ItemId = DivineBija_Products.ItemId AND ItemAttribMasterId = 5
UPDATE RetailSlnSch.ItemAttrib SET ItemAttribValue = DivineBija_Products.[Prod Code]
FROM dbo.DivineBija_Products WHERE [Prod Code] <> '' AND ItemAttrib.ItemId = DivineBija_Products.ItemId AND ItemAttribMasterId = 6

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

SELECT DISTINCT [Width Unit] FROM dbo.DivineBija_Products WHERE Width <> ''
SELECT ItemId, Width, [Width Unit] FROM dbo.DivineBija_Products WHERE Width <> ''
SELECT  * FROM RetailSlnSch.ItemAttrib

SET IDENTITY_INSERT RetailSlnSch.Item ON
INSERT RetailSlnSch.Item(ItemId, ClientId, ImageName, ItemDesc, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, UploadImageFileName)
SELECT Item_20230224.ItemId, 1 AS ClientId, ImageName, Description AS ItemDesc, Rate AS ItemRate, Description AS ItemShortDesc, 5 AS ItemStarCount, 100 AS ItemStatusId, '' AS UploadImageFileName
FROM dbo.DivineBija_Products
INNER JOIN dbo.Item_20230224 ON DivineBija_Products.ItemId = Item_20230224.ItemId
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
