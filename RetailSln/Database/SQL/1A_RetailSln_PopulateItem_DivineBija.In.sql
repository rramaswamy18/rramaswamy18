USE [RetailSlnNew]
GO
--1A_RetailSlnNew_PopulateItem_DivineBija.in.sql
--Mar 3 2025
DECLARE @ClientId BIGINT = 3
--
        TRUNCATE TABLE RetailSlnSch.CategoryItemMasterHierNew
		INSERT RetailSlnSch.CategoryItemMasterHierNew(ClientId, AspNetRoleName, CategoryId, ParentCategoryId, SeqNum)
        SELECT @ClientId AS ClientId, DivineBija_CategoryHiers.[Role Name] AS AspNetRoleName, Category.CategoryId, ParentCategory.CategoryId AS ParentCategoryId, DivineBija_CategoryHiers.[Seq Num] AS SeqNum
          FROM DivineBija_CategoryHiers
    INNER JOIN RetailSlnSch.Category ON DivineBija_CategoryHiers.[Category Name Desc] = Category.CategoryNameDesc
    INNER JOIN RetailSlnSch.Category AS ParentCategory ON DivineBija_CategoryHiers.[Parent CategoryName Desc] = ParentCategory.CategoryNameDesc
      ORDER BY DivineBija_CategoryHiers.[Role Name], ParentCategory.CategoryId, DivineBija_CategoryHiers.[Seq Num]
        DROP TABLE IF EXISTS #TEMP1A
        CREATE TABLE #TEMP1A(ParentCategoryId BIGINT, ItemMasterSeqNum FLOAT, ItemMasterId BIGINT, ItemSeqNum FLOAT, ItemId BIGINT, Id BIGINT NOT NULL IDENTITY(1, 1))
        INSERT #TEMP1A(ParentCategoryId, ItemMasterSeqNum, ItemMasterId, ItemSeqNum, ItemId)
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 1] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 1], '') != ''
        UNION
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 2] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 2], '') != ''
        UNION
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 3] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 3], '') != ''
        UNION
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 4] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 4], '') != ''
        UNION
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 5] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 5], '') != ''
        UNION
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 6] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 6], '') != ''
        UNION
        SELECT Category.CategoryId AS ParentCategoryId, DivineBija_Products.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Products.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Products INNER JOIN RetailSlnSch.Category ON [Category 7] = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON Description0 = ItemMaster.ItemMasterDesc0 AND Description1 = ItemMaster.ItemMasterDesc1 AND Description2 = ItemMaster.ItemMasterDesc2 AND Description3 = ItemMaster.ItemMasterDesc3
        INNER JOIN RetailSlnSch.Item ON DivineBija_Products.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL([Category 7], '') != ''
        UNION
        SELECT DISTINCT Category.CategoryId AS ParentCategoryId, DivineBija_Books.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Books.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Books INNER JOIN RetailSlnSch.Category ON Category0 = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON ProductDesc0 = ItemMaster.ItemMasterDesc0 AND ProductDesc1 = ItemMaster.ItemMasterDesc1
        INNER JOIN RetailSlnSch.Item ON DivineBija_Books.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL(Category0, '') != ''
        UNION
        SELECT DISTINCT Category.CategoryId AS ParentCategoryId, DivineBija_Books.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Books.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Books INNER JOIN RetailSlnSch.Category ON Category1 = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON ProductDesc0 = ItemMaster.ItemMasterDesc0 AND ProductDesc1 = ItemMaster.ItemMasterDesc1
        INNER JOIN RetailSlnSch.Item ON DivineBija_Books.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL(Category1, '') != ''
        UNION
        SELECT DISTINCT Category.CategoryId AS ParentCategoryId, DivineBija_Books.SeqNumManual AS ItemMasterSeqNum, ItemMaster.ItemMasterId, DivineBija_Books.[Spec Seq] AS ItemSeqNum, Item.ItemId
        FROM DivineBija_Books INNER JOIN RetailSlnSch.Category ON Category2 = Category.CategoryNameDesc
        INNER JOIN RetailSlnSch.ItemMaster ON ProductDesc0 = ItemMaster.ItemMasterDesc0 AND ProductDesc1 = ItemMaster.ItemMasterDesc1
        INNER JOIN RetailSlnSch.Item ON DivineBija_Books.UniqueDescription = Item.ItemUniqueDesc
        WHERE ISNULL(Category2, '') != ''
        ORDER BY Category.CategoryId, ItemMasterSeqNum, ItemSeqNum
--select * from #TEMP1A
        DROP TABLE IF EXISTS #TEMP2A
        CREATE TABLE #TEMP2A
        (
         ClientId BIGINT, AspNetRoleName NVARCHAR(50), ItemId BIGINT, ItemMasterId BIGINT, ItemMasterSeqNum FLOAT, ItemSeqNum FLOAT, ParentCategoryId BIGINT, Id BIGINT IDENTITY(1, 1),
         CONSTRAINT [Temp2A_PK] PRIMARY KEY CLUSTERED(Id),
         CONSTRAINT [Temp2A_IX0] UNIQUE NONCLUSTERED(AspNetRoleName, ParentCategoryId, ItemMasterSeqNum, ItemSeqNum),
         CONSTRAINT [Temp2A_IX1] UNIQUE NONCLUSTERED(AspNetRoleName, ParentCategoryId, ItemMasterId, ItemId),
        )
        INSERT #TEMP2A(ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, ParentCategoryId)
        SELECT DISTINCT @ClientId AS ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, ParentCategoryId
        FROM #TEMP1A, ArchLib.AspNetRole
        WHERE AspNetRoleName IN('DEFAULTROLE', 'APPLADMN1', 'SYSTADMIN', 'MARKETINGROLE')
        AND ParentCategoryId NOT IN(102, 120)--'Bulk Orders', 'Wholesale Orders'
        UNION
        SELECT DISTINCT @ClientId AS ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, ParentCategoryId
        FROM #TEMP1A, ArchLib.AspNetRole
        WHERE AspNetRoleName IN('BULKORDERSROLE', 'APPLADMN1', 'SYSTADMIN', 'MARKETINGROLE', 'WHOLESALEROLE')
        AND ParentCategoryId IN(102, 120)--'Bulk Orders', 'Wholesale Orders')
        UNION
        SELECT DISTINCT @ClientId AS ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, 100 AS ParentCategoryId
        FROM #TEMP1A, ArchLib.AspNetRole
        WHERE AspNetRoleName IN('DEFAULTROLE', 'APPLADMN1', 'SYSTADMIN', 'MARKETINGROLE')
        AND ParentCategoryId NOT IN(102, 120)--('Bulk Orders', 'Wholesale Orders')
        ORDER BY AspNetRoleName, ParentCategoryId, ItemMasterSeqNum, ItemSeqNum
--CategoryItemMasterHier
        INSERT RetailSlnSch.CategoryItemMasterHierNew(ClientId, AspNetRoleName, ParentCategoryId, ItemMasterId, SeqNum)
        SELECT DISTINCT ClientId, AspNetRoleName, ParentCategoryId, ItemMasterId, ItemMasterSeqNum FROM #TEMP2A
        ORDER BY AspNetRoleName, ParentCategoryId, ItemMasterId, ItemMasterSeqNum
---
SELECT * FROM RetailSlnSch.CategoryItemMasterHierNew WHERE AspNetRoleName = 'MARKETINGROLE' AND ParentCategoryId = 0 AND ItemMasterId IS NULL ORDER BY SeqNum
SELECT * FROM RetailSlnSch.CategoryItemMasterHierNew WHERE AspNetRoleName = 'MARKETINGROLE' AND ParentCategoryId = 112 and CategoryId IS NULL ORDER BY SeqNum
SELECT AspNetRoleName, ParentCategoryId, COUNT(*) FROM RetailSlnSch.CategoryItemMasterHierNew WHERE CategoryId IS NULL GROUP BY AspNetRoleName, ParentCategoryId ORDER BY AspNetRoleName, ParentCategoryId
