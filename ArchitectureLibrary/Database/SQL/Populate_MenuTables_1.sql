--Populate_MenuTables_1.sql
		SELECT * FROM ArchLib.MenuList
		SELECT * FROM ArchLib.MenuLayout ORDER BY ParentMenuListId, SeqNum
		SELECT * FROM ArchLib.MenuKVP ORDER BY MenuListId, SeqNum
/*
        TRUNCATE TABLE MenuListUpload
        TRUNCATE TABLE MenuKVPUpload
        TRUNCATE TABLE MenuLayoutUpload

        SELECT 0 AS ClientId, ParentMenuList.MenuListNameDesc AS ParentMenuListNameDesc, SeqNum, MenuList.MenuListNameDesc, MenuList.MenuListDesc
          FROM ArchLib.MenuLayout
    INNER JOIN ArchLib.MenuList AS ParentMenuList
            ON MenuLayout.ParentMenuListId = ParentMenuList.MenuListId
    INNER JOIN ArchLib.MenuList
            ON MenuLayout.MenuListId = MenuList.MenuListId
      ORDER BY ParentMenuList.MenuListNameDesc
              ,SeqNum
*/
        TRUNCATE TABLE ArchLib.ApplicationDefault
        TRUNCATE TABLE ArchLib.MenuKVP
        DELETE ArchLib.MenuLayout
        DELETE ArchLib.MenuList

		INSERT ArchLib.ApplicationDefault(ApplicationDefaultId, ClientId, KVPKey, KVPSubKey, SeqNum, KVPValue)
		SELECT ApplicationDefaultId, ClientId, KVPKey, KVPSubKey, SeqNum, KVPValue FROM SchoolPrdA1Truck_Save.ArchLib.ApplicationDefault
		ORDER BY 1

        INSERT ArchLib.MenuList(MenuListId, ClientId, MenuListDesc, MenuListNameDesc)
		SELECT MenuListId, ClientId, MenuListDesc, MenuListNameDesc
		FROM SchoolPrdA1Truck_Save.ArchLib.MenuList

        INSERT ArchLib.MenuKVP(MenuKVPId, ClientId, MenuListId, SeqNum, MenuKVPKeyData, MenuKVPValueData)
		SELECT MenuKVPId, ClientId, MenuListId, SeqNum, MenuKVPKeyData, MenuKVPValueData
		FROM SchoolPrdA1Truck_Save.ArchLib.MenuKVP

	    INSERT ArchLib.MenuLayout(MenuLayoutId, ClientId, MenuListDesc, MenuListId, ParentMenuListId, SeqNum)
        SELECT MenuLayoutId, ClientId, MenuListDesc, MenuListId, ParentMenuListId, SeqNum
		  FROM SchoolPrdA1Truck_Save.ArchLib.MenuLayout ORDER BY ParentMenuListId, SeqNum

        TRUNCATE TABLE ArchLib.MenuKVP
        DELETE ArchLib.MenuLayout
        DELETE ArchLib.MenuList

        INSERT ArchLib.MenuList(MenuListId, ClientId, MenuListDesc, MenuListNameDesc)
        SELECT MenuListUploadId AS MenuListId, 92 AS ClientId, MenuListDesc, MenuListNameDesc
          FROM dbo.MenuListUpload
      ORDER BY MenuListUploadId

        INSERT ArchLib.MenuKVP(MenuKVPId, ClientId, MenuListId, SeqNum, MenuKVPKeyData, MenuKVPValueData)
        SELECT MenuKVPUpload.Id AS MenuKVPId, 92 AS ClientId, MenuListUpload.MenuListUploadId AS MenuListId, MenuKVPUpload.SeqNum, MenuKVPUpload.MenuKVPKeyData, MenuKVPUpload.MenuKVPValueData
          FROM dbo.MenuKVPUpload
	INNER JOIN dbo.MenuListUpload
			ON MenuKVPUpload.MenuListNameDesc = MenuListUpload.MenuListNameDesc
      ORDER BY MenuListId, SeqNum

	    INSERT ArchLib.MenuLayout(MenuLayoutId, ClientId, MenuListDesc, MenuListId, ParentMenuListId, SeqNum)
        SELECT Id AS MenuLayoutId, 92 AS ClientId, MenuLayoutUpload.MenuListDesc, MenuList.MenuListId, ParentMenuList.MenuListId AS ParentMenuListId, SeqNum
          FROM dbo.MenuLayoutUpload
	INNER JOIN ArchLib.MenuList AS ParentMenuList
			ON MenuLayoutUpload.ParentMenuListNameDesc = ParentMenuList.MenuListNameDesc
	INNER JOIN ArchLib.MenuList
			ON MenuLayoutUpload.MenuListNameDesc = MenuList.MenuListNameDesc
      ORDER BY ParentMenuList.MenuListId, SeqNum
--
