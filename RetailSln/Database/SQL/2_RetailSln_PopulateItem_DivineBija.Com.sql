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
--
--;
--        WITH UpdateData  As
--        (
--            SELECT BundleUniqueDescription
--                  ,CAST([Seq Num] AS FLOAT) AS SeqNum
--                  ,ROW_NUMBER() OVER (PARTITION BY BundleUniqueDescription ORDER BY BundleUniqueDescription, CAST([Seq Num] AS FLOAT)) AS RowNumber
--              FROM dbo.DivineBija_ItemBundleItem
--        )
--        UPDATE dbo.DivineBija_ItemBundle
--           SET [Seq Num] = RowNumber
--          FROM dbo.DivineBija_ItemBundle
--    INNER JOIN UpdateData
--            ON DivineBija_ItemBundle.BundleUniqueDescription = UpdateData.BundleUniqueDescription
--           AND DivineBija_ItemBundle.[Seq Num] = UpdateData.RowNumber
--;
--
        TRUNCATE TABLE RetailSlnSch.ItemBundleItem
        DELETE RetailSlnSch.ItemBundle
        DBCC CHECKIDENT ('RetailSlnSch.ItemBundle', RESEED, 0);
        DELETE RetailSlnSch.Item
        DBCC CHECKIDENT ('RetailSlnSch.Item', RESEED, 0);
        DELETE RetailSlnSch.ItemMaster
        DBCC CHECKIDENT ('RetailSlnSch.ItemMaster', RESEED, 0);
--
--Begin Item Master
        INSERT RetailSlnSch.ItemMaster
              (
               ClientId, ImageExtension, ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3, ItemTypeId, ProductItemId
              )
        SELECT @ClientId AS ClientId, 'png' AS ImageExtension, Description0 AS ItemMasterDesc0, Description1 AS ItemMasterDesc1
              ,Description2 AS ItemMasterDesc2, Description3 AS ItemMasterDesc3
              ,CASE [Item Type] WHEN 'ITEMS' THEN 100 WHEN 'BUNDLE' THEN 300 END AS ItemTypeId
              ,MIN(ItemId) AS ProductItemId
          FROM dbo.DivineBija_Products
         WHERE [USA Active] = 1
      GROUP BY Description0, Description1, Description2, Description3
              ,CASE [Item Type] WHEN 'ITEMS' THEN 100 WHEN 'BUNDLE' THEN 300 END
      ORDER BY ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3
--
        INSERT RetailSlnSch.ItemMaster
              (
               ClientId, ImageExtension, ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3, ItemTypeId, ProductItemId
              )
        SELECT @ClientId AS ClientId, 'png' AS ImageExtension, ProductDesc0 AS ItemMasterDesc0, ProductDesc1 AS ItemMasterDesc1
              ,'' AS ItemMasterDesc2, '' AS ItemMasterDesc3, 200 AS ItemTypeId, MIN(ItemId) AS ProductItemId
          FROM dbo.DivineBija_Books
         WHERE [USA Active] = 1
      GROUP BY ProductDesc0, ProductDesc1
      ORDER BY ItemMasterDesc0, ItemMasterDesc1, ItemMasterDesc2, ItemMasterDesc3
--
        UPDATE RetailSlnSch.ItemMaster
           SET UploadImageFileName = RTRIM(LTRIM(ImageFileName))
          FROM dbo.DivineBija_Products, RetailSlnSch.ItemSpecMaster
         WHERE DivineBija_Products.ItemId = ItemMaster.ProductItemId
           AND UploadImageFileName IS NULL
--
        UPDATE RetailSlnSch.ItemMaster
           SET UploadImageFileName = RTRIM(LTRIM(Image1))
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
              ,ItemShortDesc3, ItemStarCount, ItemStatusId, ItemTypeId, ItemUniqueDesc, ProductItemId, UploadImageFileName
              )
--Item & Item Bundle
        SELECT @ClientId AS ClientId, CASE [USA For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId, ItemMaster.ItemMasterId
              ,-1 AS ItemRate, -1 AS ItemRateMSRP
              ,CASE ISNUMERIC([Spec Seq]) WHEN 1 THEN CAST([Spec Seq] AS INT) ELSE 0 END AS ItemSeqNum, Description0 AS ItemShortDesc0
              ,Description1 AS ItemShortDesc1, Description2 AS ItemShortDesc2, Description3 AS ItemShortDesc3, 5 AS ItemStarCount
              ,CASE WHEN [USA Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId
              ,CASE [Item Type] WHEN 'ITEMS' THEN 100 WHEN 'BUNDLE' THEN 300 END AS ItemTypeId, UniqueDescription AS ItemUniqueDesc
              ,ItemId AS ProductItemId, ImageFileName AS UploadImageFileName
          FROM dbo.DivineBija_Products
    INNER JOIN RetailSlnSch.ItemMaster
            ON DivineBija_Products.Description0 = ItemMaster.ItemMasterDesc0
           AND DivineBija_Products.Description1 = ItemMaster.ItemMasterDesc1
           AND DivineBija_Products.Description2 = ItemMaster.ItemMasterDesc2
           AND DivineBija_Products.Description3 = ItemMaster.ItemMasterDesc3
         WHERE [USA Active] = 1
      ORDER BY ItemShortDesc0, ItemShortDesc1, ItemShortDesc2, ItemShortDesc3, ItemSeqNum
--
        INSERT RetailSlnSch.Item
              (ClientId, ItemForSaleId, ItemMasterId, ItemRate, ItemRateMSRP, ItemSeqNum, ItemShortDesc0, ItemShortDesc1, ItemShortDesc2
              ,ItemShortDesc3, ItemStarCount, ItemStatusId, ItemTypeId, ItemUniqueDesc, ProductItemId, UploadImageFileName
              )
--Books
        SELECT @ClientId AS ClientId, CASE [USA For Sale] WHEN 1 THEN 100 ELSE 200 END AS ItemForSaleId
              ,ItemMaster.ItemMasterId, -1 AS ItemRate, -1 AS ItemRateMSRP, 0 AS ItemSeqNum
              ,ProductDesc0 AS ItemShortDesc0, ProductDesc1 AS ItemShortDesc1, '' AS ItemShortDesc2, '' AS ItemShortDesc3
              ,5 AS ItemStarCount, CASE WHEN [USA Active] = 1 THEN 100 ELSE 200 END AS ItemStatusId, 200 AS ItemTypeId
              ,UniqueDescription AS ItemUniqueDesc, ItemId AS ProductItemId, Image1 AS UploadImageFileName
          FROM dbo.DivineBija_Books
    INNER JOIN RetailSlnSch.ItemMaster
            ON DivineBija_Books.ProductDesc0 = ItemMaster.ItemMasterDesc0
           AND DivineBija_Books.ProductDesc1 = ItemMaster.ItemMasterDesc1
         WHERE [USA Active] = 1
      ORDER BY ItemShortDesc0, ItemShortDesc1, ItemShortDesc2, ItemShortDesc3, ItemSeqNum
--
--End Item

--Begin Item Bundle
--;
--        WITH UpdateData  As
--        (
--            SELECT BundleUniqueDescription
--                  ,[Seq Num]
--                  ,ROW_NUMBER() OVER (PARTITION BY BundleUniqueDescription ORDER BY BundleUniqueDescription) AS RowNumber
--              FROM dbo.DivineBija_ItemBundle
--        )
--        UPDATE dbo.DivineBija_ItemBundle
--           SET [Seq Num] = RowNumber
--          FROM dbo.DivineBija_ItemBundle
--    INNER JOIN UpdateData
--            ON DivineBija_ItemBundle.BundleUniqueDescription = UpdateData.BundleUniqueDescription
--           AND DivineBija_ItemBundle.[Seq Num] = UpdateData.[Seq Num]
--;
--UPDATE dbo.DivineBija_ItemBundle SET BundleItemId = NULL, ItemId = NULL
--UPDATE dbo.DivineBija_ItemBundle SET BundleItemId = Item.ItemId FROM RetailSlnSch.Item WHERE BundleUniqueDescription = Item.ItemUniqueDesc AND BundleItemId IS NULL
--UPDATE dbo.DivineBija_ItemBundle SET ItemId = Item.ItemId FROM RetailSlnSch.Item WHERE ItemUniqueDescription = Item.ItemUniqueDesc AND DivineBija_ItemBundle.ItemId IS NULL

--SELECT DISTINCT BundleUniqueDescription FROM dbo.DivineBija_ItemBundle WHERE BundleItemId IS NULL
--SELECT DISTINCT ItemUniqueDescription FROM dbo.DivineBija_ItemBundle WHERE ItemId IS NULL
--
--
        INSERT RetailSlnSch.ItemBundle(ClientId, ItemId, DiscountPercent)
        SELECT DISTINCT
               @ClientId AS ClientId
              ,Item.ItemId
              ,DiscountPercent
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
--End Item Bundle

--Begin CategoryItemMasterHiers
--Sequence
;
--        WITH UpdateData  As
--        (
--            SELECT [Category Name Desc]
--                  ,[Seq Num]
--                  ,ROW_NUMBER() OVER (PARTITION BY [Category Name Desc] ORDER BY [Category Name Desc], CAST([Seq Num] AS FLOAT)) AS RowNumber
--              FROM dbo.DivineBija_CategoryItemHiers
--        )
--        UPDATE dbo.DivineBija_CategoryItemHiers
--           SET [Seq Num] = RowNumber
--          FROM dbo.DivineBija_CategoryItemHiers
--    INNER JOIN UpdateData
--            ON DivineBija_CategoryItemHiers.[Category Name Desc] = UpdateData.[Category Name Desc]
--           AND DivineBija_CategoryItemHiers.[Seq Num] = UpdateData.[Seq Num]
--;
--SELECT @ClientId AS ClientId, 9 AS CategoryId, 0 AS SeqNum, NULL AS CategoryId, NULL AS Id, 'ParentCategoryName' AS ProcessType, 'Category' AS CategoryOrItem UNION
        TRUNCATE TABLE RetailSlnSch.CategoryItemMasterHier
--Categories
        INSERT RetailSlnSch.CategoryItemMasterHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemMasterId, ProcessType, CategoryOrItem)
        SELECT @ClientId AS ClientId, ParentId AS ParentCategoryId, [Seq Num] AS SeqNum, Id AS CategoryId, NULL AS ItemMasterId, '' AS ProcessType
              ,'Category' AS CategoryOrItem
          FROM dbo.DivineBija_Categories
         WHERE ParentId IS NOT NULL
      ORDER BY CategoryOrItem, ParentCategoryId, SeqNum
--
        INSERT RetailSlnSch.CategoryItemMasterHier(ClientId, ParentCategoryId, SeqNum, CategoryId, ItemMasterId, ProcessType, CategoryOrItem)
--Items
        SELECT @ClientId AS ClientId, Category.CategoryId AS ParentCategoryId, [Seq Num] AS SeqNum, NULL AS CategoryId, ItemMaster.ItemMasterId
              ,'' AS ProcessType, 'Item' AS CategoryOrItem
          FROM dbo.DivineBija_CategoryItemHiers
    INNER JOIN RetailSlnSch.Category
            ON DivineBija_CategoryItemHiers.[Category Name Desc] = Category.CategoryNameDesc
    INNER JOIN RetailSlnSch.ItemMaster
            ON DivineBija_CategoryItemHiers.[Item Master Desc] = ItemMaster.ItemMasterDesc
      ORDER BY ParentCategoryId
              ,CAST([Seq Num] AS FLOAT)
--
--End CategoryItemMasterHier

--Begin Corp Acct Discount
        TRUNCATE TABLE RetailSlnSch.ItemDiscount
        INSERT RetailSlnSch.ItemDiscount(ClientId, CorpAcctId, ItemId, DiscountPercent)
        SELECT @ClientId AS ClientId, CorpAcct.CorpAcctId, Item.ItemId, 35 AS DiscountPercent
          FROM RetailSlnSch.Item
    CROSS JOIN RetailSlnSch.CorpAcct WHERE CreditSale = 1
      ORDER BY CorpAcctId, ItemId
--End Corp Acct Discount

--Begin Item Spec
SET NOCOUNT ON
        TRUNCATE TABLE RetailSlnSch.ItemSpec
        INSERT RetailSlnSch.ItemSpec
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
        SET @SqlStmtTemp = 'UPDATE RetailSlnSch.ItemSpec SET ItemSpecValue = '
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT CAST(@ItemSpecMasterId AS VARCHAR)
            IF @ProductFlag = 1
            BEGIN
                SET @SqlStmt = @SqlStmtTemp + 'DivineBija_Products.' + @ValueColumnName + ' FROM RetailSlnSch.Item, dbo.DivineBija_Products'
                SET @SqlStmt = @SqlStmt + ' WHERE Item.ProductItemId = DivineBija_Products.Id AND ItemSpec.ItemId = Item.ItemId AND '
                SET @SqlStmt = @SqlStmt + ''''' <> ISNULL(' +  + @ValueColumnName + ', '''') AND ItemSpecMasterId = ' +  @ItemSpecMasterId
                INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                SELECT 1, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 1'
                EXEC(@SqlStmt)
                IF @CodeTypeId IS NOT NULL
                BEGIN
                    SET @SqlStmt = ''
                    SET @SqlStmt = @SqlStmt + 'UPDATE RetailSlnSch.ItemSpec SET ItemSpecUnitValue = CodeData.CodeDataNameId'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Products'
                    SET @SqlStmt = @SqlStmt + '         ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpec'
                    SET @SqlStmt = @SqlStmt + '         ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + '         ,Lookup.CodeData'
                    SET @SqlStmt = @SqlStmt + ' WHERE '
                    SET @SqlStmt = @SqlStmt + '       Item.ProductItemId = DivineBija_Products.Id'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpec.ItemId = Item.ItemId'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
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
                    SET @SqlStmt = @SqlStmt + 'UPDATE RetailSlnSch.ItemSpec SET SeqNumItem = ItemSpecMaster.SeqNum'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Products'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + ' WHERE ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
                    SET @SqlStmt = @SqlStmt + ' AND ISNULL(DivineBija_Products.' + @ShowValueColumnName + ', '''') = ''YES'''
                    SET @SqlStmt = @SqlStmt + ' AND DivineBija_Products.Id = Item.ProductItemId'
                    SET @SqlStmt = @SqlStmt + ' AND ItemSpec.ItemId = Item.ItemId'
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
                SET @SqlStmt = @SqlStmt + ' WHERE Item.ProductItemId = DivineBija_Books.Id AND ItemSpec.ItemId = Item.ItemId AND '
                SET @SqlStmt = @SqlStmt + ''''' <> ISNULL(' +  + @ValueColumnName + ', '''') AND ItemSpecMasterId = ' +  @ItemSpecMasterId
                INSERT #TEMP1(Num, ItemSpecMasterId, ValueColumnName, ShowValueColumnName, UnitColumnName, SqlStmt)
                SELECT 4, @ItemSpecMasterId, @ValueColumnName, @ShowValueColumnName, @UnitColumnName, @SqlStmt
--PRINT CAST(@ItemSpecMasterId AS VARCHAR) + ' 4'
                EXEC(@SqlStmt)
                IF @CodeTypeId IS NOT NULL
                BEGIN
                    SET @SqlStmt = ''
                    SET @SqlStmt = @SqlStmt + 'UPDATE RetailSlnSch.ItemSpec SET ItemSpecUnitValue = CodeData.CodeDataNameId'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Books'
                    SET @SqlStmt = @SqlStmt + '         ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpec'
                    SET @SqlStmt = @SqlStmt + '         ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + '         ,Lookup.CodeData'
                    SET @SqlStmt = @SqlStmt + ' WHERE '
                    SET @SqlStmt = @SqlStmt + '       Item.ProductItemId = DivineBija_Books.Id'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpec.ItemId = Item.ItemId'
                    SET @SqlStmt = @SqlStmt + '   AND ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
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
                    SET @SqlStmt = @SqlStmt + 'UPDATE RetailSlnSch.ItemSpec SET SeqNumItem = ItemSpecMaster.SeqNum'
                    SET @SqlStmt = @SqlStmt + '  FROM dbo.DivineBija_Books'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.Item'
                    SET @SqlStmt = @SqlStmt + '      ,RetailSlnSch.ItemSpecMaster'
                    SET @SqlStmt = @SqlStmt + ' WHERE ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId'
                    SET @SqlStmt = @SqlStmt + ' AND ISNULL(DivineBija_Books.' + @ShowValueColumnName + ', '''') = ''YES'''
                    SET @SqlStmt = @SqlStmt + ' AND DivineBija_Books.Id = Item.ProductItemId'
                    SET @SqlStmt = @SqlStmt + ' AND ItemSpec.ItemId = Item.ItemId'
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
SELECT * FROM #TEMP1
--End Item Spec Update
--End Item Spec

--Begin Item Spec Update SeqNumItemMaster
        UPDATE RetailSlnSch.ItemSpec
           SET SeqNumItemMaster = A.SeqNum
          FROM
              (
        SELECT DISTINCT
               Item.ItemId, ItemSpec.ItemSpecId, ItemSpec.SeqNum
          FROM RetailSlnSch.ItemSpec
    INNER JOIN RetailSlnSch.Item
            ON Item.ItemId = ItemSpec.ItemId
    INNER JOIN dbo.DivineBija_Products
            ON DivineBija_Products.ItemId = Item.ProductItemId
    INNER JOIN RetailSlnSch.ItemSpecMaster
            ON ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
           AND ItemSpecMaster.SpecName
               IN (
                   DivineBija_Products.[Spec Name 1], DivineBija_Products.[Spec Name 2], DivineBija_Products.[Spec Name 2]
                  ,DivineBija_Products.[Spec Name 3], DivineBija_Products.[Spec Name 2], DivineBija_Products.[Spec Name 4]
                  ,DivineBija_Products.[Spec Name 5]
                  )
              ) A
        WHERE ItemSpec.ItemSpecId = A.ItemSpecId
          AND SeqNumItemMaster IS NULL
--Books
        UPDATE RetailSlnSch.ItemSpec
           SET SeqNumItemMaster = A.SeqNum
          FROM
              (
        SELECT DISTINCT
               Item.ItemId, ItemSpec.ItemSpecId, ItemSpec.SeqNum
          FROM RetailSlnSch.ItemSpec
    INNER JOIN RetailSlnSch.Item
            ON Item.ItemId = ItemSpec.ItemId
    INNER JOIN dbo.DivineBija_Books
            ON DivineBija_Books.ItemId = Item.ProductItemId
    INNER JOIN RetailSlnSch.ItemSpecMaster
            ON ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId
           AND ItemSpecMaster.SpecName
               IN (
                   DivineBija_Books.[Spec Name 1], DivineBija_Books.[Spec Name 2], DivineBija_Books.[Spec Name 2]
                  ,DivineBija_Books.[Spec Name 3], DivineBija_Books.[Spec Name 2], DivineBija_Books.[Spec Name 4]
                  ,DivineBija_Books.[Spec Name 5]
                  )
              ) A
        WHERE ItemSpec.ItemSpecId = A.ItemSpecId
          AND SeqNumItemMaster IS NULL
--End Item Spec Update SeqNumItemMaster
--Begin Item Master Item Spec
        TRUNCATE TABLE RetailSlnSch.ItemMasterItemSpec
--
        INSERT RetailSlnSch.ItemMasterItemSpec(ClientId, ItemMasterId, ItemSpecId, SeqNumItemMaster)
        SELECT @ClientId AS ClientId, Item.ItemMasterId, MIN(ItemSpec.ItemSpecId) AS ItemSpecId, ItemSpec.SeqNumItemMaster
          FROM RetailSlnSch.Item INNER JOIN RetailSlnSch.ItemSpec ON Item.Itemid = ItemSpec.ItemId
         WHERE ItemSpec.SeqNumItemMaster IS NOT NULL --AND Item.ItemMasterId <= 207
      GROUP BY Item.ItemMasterId, ItemSpec.SeqNumItemMaster
      ORDER BY Item.ItemMasterId, ItemSpec.SeqNumItemMaster
--End Item Master Item Spec

--Begin SearchList & SearchResult
BEGIN
TRUNCATE TABLE RetailSlnSch.SearchMetaData
TRUNCATE TABLE RetailSlnSch.SearchKeyword
--
DECLARE @EntityId BIGINT, @SearchCharIndex BIGINT, @SearchKeywords NVARCHAR(MAX), @SearchKeywordText NVARCHAR(512)
DECLARE @SearchKeywordId BIGINT, @EntityTypeNameDesc NVARCHAR(50)
--
DECLARE SearchKeywordMetaDataCursor CURSOR FOR
SELECT Id AS EntityId, 'CATEGORY' AS EntityTypeNameDesc, [Search Keywords] FROM dbo.DivineBija_Categories WHERE Active = 1 AND [Search Keywords] <> ''
UNION
SELECT ItemMaster.ItemMasterId AS EntityId, 'ITEMMASTER' AS EntityTypeNameDesc, [Search Keywords]
FROM dbo.DivineBija_Products INNER JOIN RetailSlnSch.ItemMaster ON DivineBija_Products.Id = ItemMaster.ProductItemId
WHERE [Search Keywords] <> ''
UNION
SELECT ItemMaster.ItemMasterId AS EntityId, 'ITEMMASTER' AS EntityTypeNameDesc, [Search Keywords]
FROM dbo.DivineBija_Books INNER JOIN RetailSlnSch.ItemMaster ON DivineBija_Books.Id = ItemMaster.ProductItemId
WHERE [Search Keywords] <> ''
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
