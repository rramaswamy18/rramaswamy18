USE [RetailSln]
GO
--1_RetailSln_PopulateItem_DivineBija.in.sql
--Dec 20 2024, Apr 2 2024, Apr 21 2024
DECLARE @ClientId BIGINT = 3
--
        UPDATE dbo.DivineBija_Products
           SET 
               [Central GST] = REPLACE([Central GST], '%', '')
              ,[State GST] = REPLACE([State GST], '%', '')
              ,[Total Cost USD] = REPLACE([Total Cost USD], '$', '')
              ,[Retail Rate USD] = REPLACE([Retail Rate USD], '$', '')
              ,[Final Rate USD] = REPLACE([Final Rate USD], '$', '')
--
        UPDATE dbo.DivineBija_Products
           SET 
               [Interstate GST] = CAST([Central GST] AS FLOAT) + CAST([State GST] AS FLOAT)
              ,Description0 = RTRIM(LTRIM(Description0)), Description1 = RTRIM(LTRIM(Description1))
              ,Description2 = RTRIM(LTRIM(Description2)), Description3 = RTRIM(LTRIM(Description3))
              ,[Description] = RTRIM(LTRIM(Description0)) + ' ' + RTRIM(LTRIM(Description1)) + ' ' + RTRIM(LTRIM(Description2)) + ' ' + RTRIM(LTRIM(Description3))
              ,UniqueDescription = RTRIM(LTRIM(UniqueDescription))
--
        UPDATE dbo.DivineBija_Books
           SET 
               [Central GST] = REPLACE([Central GST], '%', '')
              ,[State GST] = REPLACE([State GST], '%', '')
--
        UPDATE dbo.DivineBija_Books
           SET 
               [Interstate GST] = CAST([Central GST] AS FLOAT) + CAST([State GST] AS FLOAT)
              ,ProductDesc0 = RTRIM(LTRIM(ProductDesc0)), ProductDesc1 = RTRIM(LTRIM(ProductDesc1))
              ,ProductDesc = RTRIM(LTRIM(ProductDesc0)) + ' ' + RTRIM(LTRIM(ProductDesc1))
              ,UniqueDescription = RTRIM(LTRIM(UniqueDescription))
--Cleanup
        DELETE RetailSlnSch.ItemBundle WHERE ItemBundleId > 0
        DBCC CHECKIDENT ('RetailSlnSch.ItemBundle', RESEED, 0);
        DELETE RetailSlnSch.Item WHERE ItemId > 0
        DBCC CHECKIDENT ('RetailSlnSch.Item', RESEED, 0);
        DELETE RetailSlnSch.ItemMaster WHERE ItemMasterId > 0
        DBCC CHECKIDENT ('RetailSlnSch.ItemMaster', RESEED, 0);
--
--Begin Item Master
        INSERT RetailSlnSch.ItemMaster
              (
               ClientId, ImageExtension, ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3, ItemMasterStatusId, ItemTypeId, ProductItemId
              )
        SELECT @ClientId AS ClientId, 'png' AS ImageExtension, Description0 AS ItemMasterDesc0, Description1 AS ItemMasterDesc1
              ,Description2 AS ItemMasterDesc2, Description3 AS ItemMasterDesc3, 100 AS ItemMasterStatusId
              ,CASE [Item Type] WHEN 'ITEMS' THEN 100 WHEN 'BUNDLE' THEN 300 END AS ItemTypeId
              ,MIN(ItemId) AS ProductItemId
          FROM dbo.DivineBija_Products
         WHERE [India Active] = 1
      GROUP BY Description0, Description1, Description2, Description3
              ,CASE [Item Type] WHEN 'ITEMS' THEN 100 WHEN 'BUNDLE' THEN 300 END
      ORDER BY ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3
--
        INSERT RetailSlnSch.ItemMaster
              (
               ClientId, ImageExtension, ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3, ItemMasterStatusId, ItemTypeId, ProductItemId
              )
        SELECT @ClientId AS ClientId, 'png' AS ImageExtension, ProductDesc0 AS ItemMasterDesc0, ProductDesc1 AS ItemMasterDesc1
              ,'' AS ItemMasterDesc2, '' AS ItemMasterDesc3, 100 AS ItemMasterStatusId, 200 AS ItemTypeId, MIN(ItemId) AS ProductItemId
          FROM dbo.DivineBija_Books
         WHERE [India Active] = 1
      GROUP BY ProductDesc0, ProductDesc1
      ORDER BY ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3
--
        UPDATE RetailSlnSch.ItemMaster
           SET UploadImageFileName = RTRIM(LTRIM(ImageFileName))
              ,ImageExtension = RIGHT(DivineBija_Products.ImageFileName, CHARINDEX('.', REVERSE(DivineBija_Products.ImageFileName)) - 1)
          FROM dbo.DivineBija_Products, RetailSlnSch.ItemSpecMaster
         WHERE DivineBija_Products.ItemId = ItemMaster.ProductItemId
           AND UploadImageFileName IS NULL
--
        UPDATE RetailSlnSch.ItemMaster
           SET UploadImageFileName = RTRIM(LTRIM(Image1))
              ,ImageExtension = RIGHT(DivineBija_Books.Image1, CHARINDEX('.', REVERSE(DivineBija_Books.Image1)) - 1)
          FROM dbo.DivineBija_Books, RetailSlnSch.ItemSpecMaster
         WHERE DivineBija_Books.ItemId = ItemMaster.ProductItemId
           AND UploadImageFileName IS NULL
--End Item Master
--Begin Category
        TRUNCATE TABLE RetailSlnSch.CategoryItemMasterHier
--
        DELETE RetailSlnSch.Category
--
        INSERT RetailSlnSch.Category
              (
               CategoryId, ClientId, AssignSubCategory, AssignItem, CategoryDesc, CategoryLongDesc, CategoryNameDesc, CategoryStatusId
              ,CategoryTypeId, DefaultCategory, ImageExtension, MaxPerPage, ViewName
              )
        SELECT Id AS CategoryId, @ClientId AS ClientId, [Allow Sub] AS AssignSubCategory, [Allow Item] AS AssignItem
              ,[Category Desc] AS CategoryDesc, '' AS CategoryLongDesc, [Category Name Desc] AS CategoryNameDesc
              ,CASE Active WHEN 1 THEN 100 ELSE 900 END AS CategoryStatusId
              ,CASE [Category Type] WHEN 'Regular Category' THEN 100 WHEN 'Item Bundle' THEN 400 ELSE 200 END AS CategoryTypeId
              ,[Def Cat] AS DefaultCategory, 'png' AS ImageExtension, MaxPerPage, '_OrderCategoryItem' AS ViewName
          FROM dbo.DivineBija_Categories
      ORDER BY CategoryId
--
        UPDATE RetailSlnSch.Category SET ViewName = '_FestivalList' WHERE CategoryNameDesc = 'Festival List'
--End Category
--Begin Item
--
        INSERT RetailSlnSch.Item
              (ClientId, ItemForSaleId, ItemMasterId, ItemRate, ItemRateMSRP, ItemSeqNum, ItemShortDesc0, ItemShortDesc1, ItemShortDesc2
              ,ItemShortDesc3, ItemStarCount, ItemStatusId, ItemTypeId, ItemUniqueDesc, ProductItemId, QuantityOnHand, UploadImageFileName
              )
--Item & Item Bundle
        SELECT @ClientId AS ClientId, CASE [India For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId, ItemMaster.ItemMasterId
              ,-1 AS ItemRate, -1 AS ItemRateMSRP
              ,CASE ISNUMERIC([Spec Seq]) WHEN 1 THEN CAST([Spec Seq] AS INT) ELSE 0 END AS ItemSeqNum, Description0 AS ItemShortDesc0
              ,Description1 AS ItemShortDesc1, Description2 AS ItemShortDesc2, Description3 AS ItemShortDesc3, 5 AS ItemStarCount
              ,CASE WHEN [India Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId
              ,CASE [Item Type] WHEN 'ITEMS' THEN 100 WHEN 'BUNDLE' THEN 300 END AS ItemTypeId, UniqueDescription AS ItemUniqueDesc
              ,ItemId AS ProductItemId, 0 AS QuantityOnHand, ImageFileName AS UploadImageFileName
          FROM dbo.DivineBija_Products
    INNER JOIN RetailSlnSch.ItemMaster
            ON DivineBija_Products.Description0 = ItemMaster.ItemMasterDesc0
           AND DivineBija_Products.Description1 = ItemMaster.ItemMasterDesc1
           AND DivineBija_Products.Description2 = ItemMaster.ItemMasterDesc2
           AND DivineBija_Products.Description3 = ItemMaster.ItemMasterDesc3
         WHERE [India Active] = 1
      ORDER BY ItemShortDesc0, ItemShortDesc1, ItemShortDesc2, ItemShortDesc3, ItemSeqNum
--
        INSERT RetailSlnSch.Item
              (ClientId, ItemForSaleId, ItemMasterId, ItemRate, ItemRateMSRP, ItemSeqNum, ItemShortDesc0, ItemShortDesc1, ItemShortDesc2
              ,ItemShortDesc3, ItemStarCount, ItemStatusId, ItemTypeId, ItemUniqueDesc, ProductItemId, QuantityOnHand, UploadImageFileName
              )
--Books
        SELECT @ClientId AS ClientId, CASE [India For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId
              ,ItemMaster.ItemMasterId, -1 AS ItemRate, -1 AS ItemRateMSRP
			  ,CASE ISNUMERIC([Spec Seq]) WHEN 1 THEN CAST([Spec Seq] AS INT) ELSE 0 END AS ItemSeqNum
              ,ProductDesc0 AS ItemShortDesc0, ProductDesc1 AS ItemShortDesc1, '' AS ItemShortDesc2, '' AS ItemShortDesc3
              ,5 AS ItemStarCount, CASE WHEN [India Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId, 200 AS ItemTypeId
              ,UniqueDescription AS ItemUniqueDesc, ItemId AS ProductItemId, 0 AS QuantityOnHand, Image1 AS UploadImageFileName
          FROM dbo.DivineBija_Books
    INNER JOIN RetailSlnSch.ItemMaster
            ON DivineBija_Books.ProductDesc0 = ItemMaster.ItemMasterDesc0
           AND DivineBija_Books.ProductDesc1 = ItemMaster.ItemMasterDesc1
         WHERE [India Active] = 1
      ORDER BY ItemShortDesc0, ItemShortDesc1, ItemShortDesc2, ItemShortDesc3, ItemSeqNum
--
--End Item
--Begin itemBundle
        INSERT RetailSlnSch.ItemBundle(ClientId, ItemId, ParentItemId, SeqNum)
        SELECT @ClientId AS ClientId
              ,Item.ItemId
              ,ParentItem.ItemId AS ParentItemId
              ,CAST(DivineBija_ItemBundleItem.[Seq Num] AS FLOAT) AS SeqNum
          FROM dbo.DivineBija_ItemBundle
    INNER JOIN dbo.DivineBija_ItemBundleItem
            ON DivineBija_ItemBundle.BundleUniqueDescription = DivineBija_ItemBundleItem.BundleUniqueDescription 
    INNER JOIN RetailSlnSch.Item AS ParentItem
            ON DivineBija_ItemBundle.BundleUniqueDescription = ParentItem.ItemUniqueDesc
    INNER JOIN RetailSlnSch.Item
            ON DivineBija_ItemBundleItem.ItemUniqueDescription = Item.ItemUniqueDesc
      ORDER BY ParentItem.ItemId
              ,CAST(DivineBija_ItemBundleItem.[Seq Num] AS FLOAT)
/*
        SELECT DISTINCT
               @ClientId AS ClientId
              ,Item.ItemId
              --,DiscountPercent
          FROM dbo.DivineBija_ItemBundle
    INNER JOIN RetailSlnSch.Item
            ON DivineBija_ItemBundle.BundleUniqueDescription = Item.ItemUniqueDesc
      ORDER BY Item.ItemId
--
        INSERT RetailSlnSch.ItemBundleItem(ClientId, ItemBundleId, SeqNum, ItemId, Quantity)
        SELECT @ClientId AS ClientId
              ,ItemBundle.ItemBundleId
              ,CAST(DivineBija_ItemBundleItem.[Seq Num] AS FLOAT) AS SeqNum
              ,Item.ItemId
              ,DivineBija_ItemBundleItem.Quantity
          FROM dbo.DivineBija_ItemBundleItem
    INNER JOIN RetailSlnSch.Item AS BundleItem
            ON DivineBija_ItemBundleItem.BundleUniqueDescription = BundleItem.ItemUniqueDesc
    INNER JOIN RetailSlnSch.ItemBundle
            ON BundleItem.ItemId = ItemBundle.ItemId
    INNER JOIN RetailSlnSch.Item
            ON DivineBija_ItemBundleItem.ItemUniqueDescription = Item.ItemUniqueDesc
      ORDER BY ItemBundle.ItemBundleId
              ,CAST(DivineBija_ItemBundleItem.[Seq Num] AS FLOAT)
*/
--End Item Bundle
--Begin Item Spec
SET NOCOUNT ON
        TRUNCATE TABLE dbo.ItemSpecWork
        INSERT dbo.ItemSpecWork
              (ClientId, ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, ItemId, SeqNum, SeqNumItem, SeqNumItemMaster)
        SELECT ItemSpecMaster.ClientId, ItemSpecMaster.ItemSpecMasterId, '' ItemAttribUnitValue, '' ItemAttribValue, Item.ItemId
              ,SeqNum, NULL AS SeqNumItem, NULL AS SeqNumItemMaster
          FROM RetailSlnSch.Item, RetailSlnSch.ItemSpecMaster
      ORDER BY ItemId, SeqNum
--Begin Item Spec Update SeqNumItem
        DROP TABLE IF EXISTS #TEMP1
        CREATE TABLE #TEMP1
              (
                Id BIGINT NOT NULL IDENTITY(1, 1), Num INT, ItemSpecMasterId BIGINT, ValueColumnName NVARCHAR(512)
               ,ShowValueColumnName NVARCHAR(512), UnitColumnName NVARCHAR(512), SqlStmt VARCHAR(MAX)
              )
        DECLARE @SqlStmt VARCHAR(MAX), @SqlStmtTemp VARCHAR(MAX), @SqlStmtTemp2 VARCHAR(MAX)
        DECLARE @ItemSpecMasterId VARCHAR(10), @BookFlag BIT, @ProductFlag BIT, @ValueColumnName NVARCHAR(256), @UnitColumnName NVARCHAR(256)
        DECLARE @ShowValueColumnName NVARCHAR(256)
        DECLARE @CodeTypeId BIGINT
        DECLARE ItemSpecMasterCursor CURSOR FOR
        SELECT ItemSpecMaster.ItemSpecMasterId, ItemSpecMaster.BookFlag, ItemSpecMaster.ProductFlag, ItemSpecMaster.ValueColumnName
              ,ItemSpecMaster.UnitColumnName, ItemSpecMaster.ShowValueColumnName, ItemSpecMaster.CodeTypeId
          FROM RetailSlnSch.ItemSpecMaster WHERE ValueColumnName IS NOT NULL
--
        OPEN ItemSpecMasterCursor
--
        FETCH ItemSpecMasterCursor INTO @ItemSpecMasterId, @BookFlag, @ProductFlag, @ValueColumnName, @UnitColumnName
             ,@ShowValueColumnName, @CodeTypeId
--
        SET @SqlStmtTemp = 'UPDATE dbo.ItemSpecWork SET ItemSpecValue = '
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT CAST(@ItemSpecMasterId AS VARCHAR)
            IF @ProductFlag = 1
            BEGIN
                SET @SqlStmt = @SqlStmtTemp + 'DivineBija_Products.' + @ValueColumnName + ' FROM RetailSlnSch.Item, dbo.DivineBija_Products'
                SET @SqlStmt = @SqlStmt + ' WHERE Item.ProductItemId = DivineBija_Products.Id AND ItemSpecWork.ItemId = Item.ItemId AND '
                SET @SqlStmt = @SqlStmt + ''''' <> ISNULL(' +  + @ValueColumnName + ', '''') AND ItemSpecMasterId = ' +  @ItemSpecMasterId
                INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                SELECT 1, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 1'
                EXEC(@SqlStmt)
                IF @CodeTypeId IS NOT NULL
                BEGIN
                    SET @SqlStmt = ''
                    SET @SqlStmt = @SqlStmt + 'UPDATE dbo.ItemSpecWork SET ItemSpecUnitValue = CodeData.CodeDataNameId'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Products'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,dbo.ItemSpecWork'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + '      ,Lookup.CodeData'
                    SET @SqlStmt = @SqlStmt + ' WHERE '
                    SET @SqlStmt = @SqlStmt + '       Item.ProductItemId = DivineBija_Products.Id'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpecWork.ItemId = Item.ItemId'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpecMaster.CodeTypeId = CodeData.CodeTypeId'
                    SET @SqlStmt = @SqlStmt + '   AND CodeData.CodeDataDesc4 = DivineBija_Products.' + @UnitColumnName
                    SET @SqlStmt = @SqlStmt + '   AND CodeData.CodeTypeId = ' + CAST(@CodeTypeId AS VARCHAR(5))
                    INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                    SELECT 2, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 3 ' + @SqlStmt
                    EXEC(@SqlStmt)
                END
                IF ISNULL(@ShowValueColumnName, '') <> ''
                BEGIN
                    SET @SqlStmt = ''
                    SET @SqlStmt = @SqlStmt + 'UPDATE dbo.ItemSpecWork SET SeqNumItem = ItemSpecMaster.SeqNum'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Products'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + ' WHERE ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
                    SET @SqlStmt = @SqlStmt + ' AND ISNULL(DivineBija_Products.' + @ShowValueColumnName + ', '''') = ''YES'''
                    SET @SqlStmt = @SqlStmt + ' AND DivineBija_Products.Id = Item.ProductItemId'
                    SET @SqlStmt = @SqlStmt + ' AND ItemSpecWork.ItemId = Item.ItemId'
                    SET @SqlStmt = @SqlStmt + ' AND ItemSpecMaster.ItemSpecMasterId = ''' + @ItemSpecMasterId + ''''
                    INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                    SELECT 3, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 2 ' + @SqlStmt
                    EXEC(@SqlStmt)
                END
            END
            IF @BookFlag = 1
            BEGIN
                SET @SqlStmt = @SqlStmtTemp + 'DivineBija_Books.' + @ValueColumnName + ' FROM RetailSlnSch.Item, dbo.DivineBija_Books'
                SET @SqlStmt = @SqlStmt + ' WHERE Item.ProductItemId = DivineBija_Books.Id AND ItemSpecWork.ItemId = Item.ItemId AND '
                SET @SqlStmt = @SqlStmt + ''''' <> ISNULL(' +  + @ValueColumnName + ', '''') AND ItemSpecMasterId = ' +  @ItemSpecMasterId
                INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                SELECT 4, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 4'
                EXEC(@SqlStmt)
                IF @CodeTypeId IS NOT NULL
                BEGIN
                    SET @SqlStmt = ''
                    SET @SqlStmt = @SqlStmt + 'UPDATE dbo.ItemSpecWork SET ItemSpecUnitValue = CodeData.CodeDataNameId'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Books'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,dbo.ItemSpecWork'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + '      ,Lookup.CodeData'
                    SET @SqlStmt = @SqlStmt + ' WHERE '
                    SET @SqlStmt = @SqlStmt + '       Item.ProductItemId = DivineBija_Books.Id'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpecWork.ItemId = Item.ItemId'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpecMaster.CodeTypeId = CodeData.CodeTypeId'
                    SET @SqlStmt = @SqlStmt + '   AND CodeData.CodeDataDesc4 = DivineBija_Books.' + @UnitColumnName
                    SET @SqlStmt = @SqlStmt + '   AND CodeData.CodeTypeId = ' + CAST(@CodeTypeId AS VARCHAR(5))
                    INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                    SELECT 5, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 3 ' + @SqlStmt
                    EXEC(@SqlStmt)
                END
                IF ISNULL(@ShowValueColumnName, '') <> ''
                BEGIN
                    SET @SqlStmt = ''
                    SET @SqlStmt = @SqlStmt + 'UPDATE dbo.ItemSpecWork SET SeqNumItem = ItemSpecMaster.SeqNum'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Books'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + ' WHERE ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
                    SET @SqlStmt = @SqlStmt + ' AND ISNULL(DivineBija_Books.' + @ShowValueColumnName + ', '''') = ''YES'''
                    SET @SqlStmt = @SqlStmt + ' AND DivineBija_Books.Id = Item.ProductItemId'
                    SET @SqlStmt = @SqlStmt + ' AND ItemSpecWork.ItemId = Item.ItemId'
                    SET @SqlStmt = @SqlStmt + ' AND ItemSpecMaster.ItemSpecMasterId = ' + @ItemSpecMasterId
                    --SET @SqlStmt = @SqlStmt + ' AND ItemSpecMaster.ItemSpecMasterId = ' + @ItemSpecMasterId
                    INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                    SELECT 6, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 5'
                    EXEC(@SqlStmt)
                END
            END
            FETCH ItemSpecMasterCursor INTO @ItemSpecMasterId, @BookFlag, @ProductFlag, @ValueColumnName, @UnitColumnName
                 ,@ShowValueColumnName, @CodeTypeId
        END

        CLOSE ItemSpecMasterCursor
        DEALLOCATE ItemSpecMasterCursor
SET NOCOUNT OFF
--SELECT * FROM #TEMP1
--End Item Spec Update
--Item Spec Update for single items
--End Item Spec
--Begin Item Spec Update SeqNumItemMaster
        UPDATE dbo.ItemSpecWork
           SET SeqNumItemMaster = A.SeqNum
          FROM
              (
        SELECT DISTINCT
               Item.ItemId, ItemSpecWork.ItemSpecId, ItemSpecWork.SeqNum
          FROM dbo.ItemSpecWork
    INNER JOIN RetailSlnSch.Item
            ON Item.ItemId = ItemSpecWork.ItemId
    INNER JOIN dbo.DivineBija_Products
            ON DivineBija_Products.ItemId = Item.ProductItemId
    INNER JOIN RetailSlnSch.ItemSpecMaster
            ON ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
           AND ItemSpecMaster.SpecName
               IN (
                   DivineBija_Products.[Spec Name 1], DivineBija_Products.[Spec Name 2], DivineBija_Products.[Spec Name 3]
                  ,DivineBija_Products.[Spec Name 4], DivineBija_Products.[Spec Name 5]
                  )
              ) A
        WHERE ItemSpecWork.ItemSpecId = A.ItemSpecId
          AND SeqNumItemMaster IS NULL
--Books
        UPDATE dbo.ItemSpecWork
           SET SeqNumItemMaster = A.SeqNum
          FROM
              (
        SELECT DISTINCT
               Item.ItemId, ItemSpecWork.ItemSpecId, ItemSpecWork.SeqNum
          FROM dbo.ItemSpecWork
    INNER JOIN RetailSlnSch.Item
            ON Item.ItemId = ItemSpecWork.ItemId
    INNER JOIN dbo.DivineBija_Books
            ON DivineBija_Books.ItemId = Item.ProductItemId
    INNER JOIN RetailSlnSch.ItemSpecMaster
            ON ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
           AND ItemSpecMaster.SpecName
               IN (
                   DivineBija_Books.[Spec Name 1], DivineBija_Books.[Spec Name 2], DivineBija_Books.[Spec Name 3]
                  ,DivineBija_Books.[Spec Name 4], DivineBija_Books.[Spec Name 5]
                  )
              ) A
        WHERE ItemSpecWork.ItemSpecId = A.ItemSpecId
          AND SeqNumItemMaster IS NULL
--End Item Spec Update SeqNumItemMaster
--Begin Update ItemSpec
        UPDATE dbo.ItemSpecWork SET SeqNumItem = SeqNumItemMaster
         WHERE SeqNumItem IS NULL AND SeqNumItemMaster IS NOT NULL
           AND ItemId IN (SELECT ItemId FROM RetailSlnSch.Item WHERE ItemMasterId
               IN (SELECT ItemMasterId FROM RetailSlnSch.Item GROUP BY ItemMasterId HAVING COUNT(*) = 1))
--End Update ItemSpec
--Begin ItemMasterItemSpec
            TRUNCATE TABLE RetailSlnSch.ItemMasterItemSpec
--
            INSERT RetailSlnSch.ItemMasterItemSpec(ClientId, ItemMasterId, ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, SeqNum, SeqNumItemMaster)
            SELECT DISTINCT
                   ItemSpecWork.ClientId, Item.ItemMasterId, ItemSpecWork.ItemSpecMasterId, ItemSpecWork.ItemSpecUnitValue, ItemSpecWork.ItemSpecValue, ItemSpecWork.SeqNum, ItemSpecWork.SeqNumItemMaster
              FROM dbo.ItemSpecWork
        INNER JOIN RetailSlnSch.Item
                ON ItemSpecWork.ItemId = Item.ItemId
        INNER JOIN RetailSlnSch.ItemSpecMaster
                ON ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
               AND ItemSpecMaster.ItemMasterFlag = 1
             WHERE Item.ItemMasterId > 0
          ORDER BY Item.ItemMasterId
                  ,ItemSpecWork.ItemSpecMasterId
--End ItemMasterItemSpec
--Begin ItemItemSpec
          TRUNCATE TABLE RetailSlnSch.ItemItemSpec
--
            INSERT RetailSlnSch.ItemItemSpec(ClientId, ItemId, ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, SeqNum, SeqNumItem)
            SELECT
                   ItemSpecWork.ClientId, ItemSpecWork.ItemId, ItemSpecWork.ItemSpecMasterId, ItemSpecWork.ItemSpecUnitValue, ItemSpecWork.ItemSpecValue, ItemSpecWork.SeqNum, ItemSpecWork.SeqNumItem
              FROM dbo.ItemSpecWork
        INNER JOIN RetailSlnSch.ItemSpecMaster
                ON ItemSpecWork.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
               AND ItemSpecMaster.ProductFlag = 1
             WHERE ItemSpecWork.ItemId > 0
          ORDER BY ItemSpecWork.ItemId
                  ,ItemSpecWork.ItemSpecMasterId
--End ItemItemSpec
--
--Begin SearchList & SearchResult
--DECLARE @ClientId INT = 3
TRUNCATE TABLE RetailSlnSch.SearchMetaData
TRUNCATE TABLE RetailSlnSch.SearchKeywordSynonym
TRUNCATE TABLE RetailSlnSch.SearchKeyword

--SearchKeyword
INSERT RetailSlnSch.SearchKeyword(ClientId, SearchKeywordText)
SELECT DISTINCT @ClientId AS ClientId, LOWER(MainSearchKeyword) AS SearchKeywordText FROM DivineBija_SearchKeyword WHERE ISNULL(MainSearchKeyword, '') <> '' ORDER BY 1
--SearchMetaData
DROP TABLE IF EXISTS #TEMPA
SELECT DISTINCT SearchKeyword.ClientId, SearchKeyword.SearchKeywordId, 'ITEMMASTER' AS EntityTypeNameDesc, Item.ItemMasterId AS EntityId
  INTO #TEMPA
FROM DivineBija_SearchKeyword
INNER JOIN RetailSlnSch.SearchKeyword ON DivineBija_SearchKeyword.MainSearchKeyword = SearchKeyword.SearchKeywordText
INNER JOIN RetailSlnSch.Item ON DivineBija_SearchKeyword.UniqueDescription = Item.ItemUniqueDesc
WHERE DivineBija_SearchKeyword.KeywordType = 'ITEM' AND ISNULL(MainSearchKeyword, '') <> ''
UNION
SELECT DISTINCT SearchKeyword.ClientId, SearchKeyword.SearchKeywordId, 'CATEGORY' AS EntityTypeNameDesc, Category.CategoryId AS EntityId
FROM DivineBija_SearchKeyword
INNER JOIN RetailSlnSch.SearchKeyword ON DivineBija_SearchKeyword.MainSearchKeyword = SearchKeyword.SearchKeywordText
INNER JOIN RetailSlnSch.Category ON DivineBija_SearchKeyword.UniqueDescription = Category.CategoryNameDesc
WHERE DivineBija_SearchKeyword.KeywordType = 'CATEGORY' AND ISNULL(MainSearchKeyword, '') <> ''
INSERT RetailSlnSch.SearchMetaData(ClientId, SearchKeywordId, EntityTypeNameDesc, EntityId, SeqNum)
SELECT *, ROW_NUMBER() OVER(PARTITION BY EntityTypeNameDesc, SearchKeywordId ORDER BY EntityTypeNameDesc, SearchKeywordId)
FROM #TEMPA
--SearchKeywordSynonym
INSERT RetailSlnSch.SearchKeywordSynonym(SearchKeywordId, SearchKeywordSynonymText)
SELECT DISTINCT SearchKeyword.SearchKeywordId, DivineBija_SearchKeywordSynonym.MainSearchKeywordSynonym
FROM DivineBija_SearchKeywordSynonym
INNER JOIN RetailSlnSch.SearchKeyword ON DivineBija_SearchKeywordSynonym.MainSearchKeyword = SearchKeyword.SearchKeywordText
ORDER BY 1, 2
--End SearchList & SearchResult
--
--Begin ItemInfo
        TRUNCATE TABLE RetailSlnSch.ItemInfo
        INSERT RetailSlnSch.ItemInfo(ClientId, ItemId, ItemInfoLabelText, ItemInfoText, SeqNum)
        SELECT Item.ClientId, Item.ItemId, 'Specification(s)' AS ItemSpecLabelText, '<h2>Specifications<h2><p1>Sample Specifications</p1>' AS ItemSpecText, 1 AS SeqNum
          FROM RetailSlnSch.Item
        ORDER BY ItemId, SeqNum
--Begin CategoryItemMasterHier
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
         CONSTRAINT [Temp2A2_PK] PRIMARY KEY CLUSTERED(Id),
         CONSTRAINT [Temp2A2_IX0] UNIQUE NONCLUSTERED(AspNetRoleName, ParentCategoryId, ItemMasterSeqNum, ItemSeqNum),
         CONSTRAINT [Temp2A2_IX1] UNIQUE NONCLUSTERED(AspNetRoleName, ParentCategoryId, ItemMasterId, ItemId),
        )
        INSERT #TEMP2A(ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, ParentCategoryId)
        SELECT DISTINCT @ClientId AS ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, ParentCategoryId
        FROM #TEMP1A, ArchLib.AspNetRole
        WHERE AspNetRoleName IN('DEFAULTROLE', 'GUESTROLE', 'APPLADMN1', 'SYSTADMIN', 'MARKETINGROLE')
        AND ParentCategoryId NOT IN(102, 120)--'Bulk Orders', 'Wholesale Orders'
        UNION
        SELECT DISTINCT @ClientId AS ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, ParentCategoryId
        FROM #TEMP1A, ArchLib.AspNetRole
        WHERE AspNetRoleName IN('BULKORDERSROLE', 'APPLADMN1', 'SYSTADMIN', 'MARKETINGROLE', 'WHOLESALEROLE')
        AND ParentCategoryId IN(102, 120)--'Bulk Orders', 'Wholesale Orders')
        UNION
        SELECT DISTINCT @ClientId AS ClientId, AspNetRoleName, ItemId, ItemMasterId, ItemMasterSeqNum, ItemSeqNum, 100 AS ParentCategoryId
        FROM #TEMP1A, ArchLib.AspNetRole
        WHERE AspNetRoleName IN('DEFAULTROLE', 'GUESROLE', 'APPLADMN1', 'SYSTADMIN', 'MARKETINGROLE')
        AND ParentCategoryId NOT IN(102, 120)--('Bulk Orders', 'Wholesale Orders')
        ORDER BY AspNetRoleName, ParentCategoryId, ItemMasterSeqNum, ItemSeqNum
--Category Hier
        INSERT RetailSlnSch.CategoryItemMasterHier(ClientId, AspNetRoleName, CategoryId, ParentCategoryId, SeqNum)
        SELECT @ClientId AS ClientId, DivineBija_CategoryHiers.[Role Name] AS AspNetRoleName, Category.CategoryId, ParentCategory.CategoryId AS ParentCategoryId, DivineBija_CategoryHiers.[Seq Num] AS SeqNum
          FROM DivineBija_CategoryHiers
    INNER JOIN RetailSlnSch.Category ON DivineBija_CategoryHiers.[Category Name Desc] = Category.CategoryNameDesc
    INNER JOIN RetailSlnSch.Category AS ParentCategory ON DivineBija_CategoryHiers.[Parent CategoryName Desc] = ParentCategory.CategoryNameDesc
         WHERE DivineBija_CategoryHiers.[Role Name] IN('APPLADMN1', 'BULKORDERSROLE', 'DEFAULTROLE', 'WHOLESALEROLE')
      ORDER BY DivineBija_CategoryHiers.[Role Name], ParentCategory.CategoryId, DivineBija_CategoryHiers.[Seq Num]
        INSERT RetailSlnSch.CategoryItemMasterHier(ClientId, AspNetRoleName, ParentCategoryId, ItemMasterId, SeqNum)
        SELECT DISTINCT ClientId, AspNetRoleName, ParentCategoryId, ItemMasterId, ItemMasterSeqNum FROM #TEMP2A
         WHERE AspNetRoleName IN('APPLADMN1', 'BULKORDERSROLE', 'DEFAULTROLE', 'WHOLESALEROLE')
      ORDER BY AspNetRoleName, ParentCategoryId, ItemMasterId, ItemMasterSeqNum
--End CategoryItemMasterHier
--Begin Language in Books
INSERT RetailSlnSch.ItemItemSpec(ClientId, ItemId, ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, SeqNum, SeqNumItem)
SELECT 3 ClientId, ItemId, 28 AS ItemSpecMasterId, '' AS ItemSpecUnitValue, [Language] ItemSpecValue, 28 AS SeqNum, 28 AS SeqNumItem FROM DivineBija_Books ORDER BY ItemId
--End Language in Books
--Begin Update ItemSpecValueWithUnit in ItemItemSpec
UPDATE RetailSlnSch.ItemItemSpec SET ItemSpecValueWithUnit = NULL
UPDATE RetailSlnSch.ItemItemSpec SET ItemSpecValueWithUnit = ItemItemSpec.ItemSpecValue + ' ' + CodeData.CodeDataDesc0
FROM RetailSlnSch.ItemItemSpec
INNER JOIN RetailSlnSch.ItemSpecMaster
ON ItemItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
INNER JOIN Lookup.CodeData
ON ItemSpecMaster.CodeTypeId = CodeData.CodeTypeId AND ItemItemSpec.ItemSpecUnitValue = CodeData.CodeDataNameId
UPDATE RetailSlnSch.ItemItemSpec SET ItemSpecValueWithUnit = ItemSpecValue WHERE ItemSpecValueWithUnit IS NULL
--End Update ItemSpecValueWithUnit in ItemItemSpec
SELECT 'copy "C:\Common\Images\DivineBijaIn\DivineBija_20230927\UploadedImages\' + UploadImageFileName + '" DivineBija.InUploadedImages\' AS DosCopyCommand, UploadImageFileName, ImageName, ItemMasterId, ItemMasterDesc FROM RetailSlnSch.ItemMaster WHERE ItemMasterId > 0 ORDER BY ItemMasterId
SELECT 'copy "DivineBija.InUploadedImages\' + UploadImageFileName + '" ItemMaster\ItemMaster' + CAST(ItemMasterId AS VARCHAR) + '.' + ImageExtension AS DosCopyCommand, UploadImageFileName, ImageName, ItemMasterId, ItemMasterDesc FROM RetailSlnSch.ItemMaster WHERE ItemMasterId > 0 ORDER BY ItemMasterId
