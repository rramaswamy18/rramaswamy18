using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static List<CategoryModel> CategoryList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.Category ORDER BY CategoryId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryModel> categoryModels = new List<CategoryModel>();
                while (sqlDataReader.Read())
                {
                    categoryModels.Add
                    (
                        new CategoryModel
                        {
                            CategoryId = long.Parse(sqlDataReader["CategoryId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            AssignSubCategory = bool.Parse(sqlDataReader["AssignSubCategory"].ToString()),
                            AssignItem = bool.Parse(sqlDataReader["AssignItem"].ToString()),
                            CategoryDesc = sqlDataReader["CategoryDesc"].ToString(),
                            CategoryName = sqlDataReader["CategoryName"].ToString(),
                            CategoryNameDesc = sqlDataReader["CategoryNameDesc"].ToString(),
                            CategoryStatusId = (CategoryStatusEnum)int.Parse(sqlDataReader["CategoryStatusId"].ToString()),
                            CategoryTypeId = (CategoryTypeEnum)int.Parse(sqlDataReader["CategoryTypeId"].ToString()),
                            DefaultCategory = bool.Parse(sqlDataReader["DefaultCategory"].ToString()),
                            ImageExtension = sqlDataReader["ImageExtension"].ToString(),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            MaxPerPage = short.Parse(sqlDataReader["MaxPerPage"].ToString()),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                            ViewName = sqlDataReader["ViewName"].ToString(),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<CategoryItemMasterHierModel> CategoryItemMasterHierList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemMasterHier ORDER BY AspNetRoleName, ParentCategoryId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryItemMasterHierModel> categoryItemMasterHierModels = new List<CategoryItemMasterHierModel>();
                while (sqlDataReader.Read())
                {
                    categoryItemMasterHierModels.Add
                    (
                        new CategoryItemMasterHierModel
                        {
                            CategoryItemMasterHierId = long.Parse(sqlDataReader["CategoryItemMasterHierId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            AspNetRoleName = sqlDataReader["AspNetRoleName"].ToString(),
                            CategoryId = sqlDataReader["CategoryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
                            ItemMasterId = sqlDataReader["ItemMasterId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryItemMasterHierModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<CorpAcctModel> CorpAcctList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.CorpAcct ORDER BY CorpAcctId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CorpAcctModel> corpAcctModels = new List<CorpAcctModel>();
                while (sqlDataReader.Read())
                {
                    corpAcctModels.Add
                    (
                        new CorpAcctModel
                        {
                            CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CorpAcctKey = sqlDataReader["CorpAcctKey"].ToString(),
                            CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                            CorpAcctTypeId = (CorpAcctTypeEnum)int.Parse(sqlDataReader["CorpAcctTypeId"].ToString()),
                            CreditDays = short.Parse(sqlDataReader["CreditDays"].ToString()),
                            CreditLimit = float.Parse(sqlDataReader["CreditLimit"].ToString()),
                            CreditSale = (YesNoEnum)long.Parse(sqlDataReader["CreditSale"].ToString()),
                            MinOrderAmount = float.Parse(sqlDataReader["MinOrderAmount"].ToString()),
                            OrderApprovalRequired = (YesNoEnum)long.Parse(sqlDataReader["OrderApprovalRequired"].ToString()),
                            ShippingAndHandlingCharges = (YesNoEnum)long.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                            TaxIdentNum = sqlDataReader["TaxIdentNum"].ToString(),
                            StatusId = (YesNoEnum)long.Parse(sqlDataReader["StatusId"].ToString()),
                            CorpAcctLocationModels = new List<CorpAcctLocationModel>(),
                            DiscountDtlModels = new List<DiscountDtlModel>(),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return corpAcctModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<CorpAcctLocationModel> CorpAcctLocationList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.CorpAcctLocation INNER JOIN ArchLib.DemogInfoAddress ON CorpAcctLocation.DemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId ORDER BY CorpAcctId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CorpAcctLocationModel> corpAcctLocationModels = new List<CorpAcctLocationModel>();
                while (sqlDataReader.Read())
                {
                    corpAcctLocationModels.Add
                    (
                        new CorpAcctLocationModel
                        {
                            CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            AlternateTelephoneCountryId = sqlDataReader["AlternateTelephoneCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["AlternateTelephoneCountryId"].ToString()),
                            AlternateTelephoneNumber = sqlDataReader["AlternateTelephoneNumber"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["AlternateTelephoneNumber"].ToString()),
                            CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                            DemogInfoAddressId = long.Parse(sqlDataReader["DemogInfoAddressId"].ToString()),
                            LocationName = sqlDataReader["LocationName"].ToString(),
                            PrimaryTelephoneCountryId = sqlDataReader["PrimaryTelephoneCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["PrimaryTelephoneCountryId"].ToString()),
                            PrimaryTelephoneNumber = sqlDataReader["PrimaryTelephoneNumber"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["PrimaryTelephoneNumber"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            DemogInfoAddressModel = new DemogInfoAddressModel
                            {
                                AddressLine1 = sqlDataReader["AddressLine1"].ToString(),
                                AddressLine2 = sqlDataReader["AddressLine2"].ToString(),
                                AddressLine3 = sqlDataReader["AddressLine3"].ToString(),
                                AddressLine4 = sqlDataReader["AddressLine4"].ToString(),
                                AddressTypeId = (AddressTypeEnum)int.Parse(sqlDataReader["AddressTypeId"].ToString()),
                                BuildingTypeId = (BuildingTypeEnum)int.Parse(sqlDataReader["BuildingTypeId"].ToString()),
                                CityName = sqlDataReader["CityName"].ToString(),
                                CountryAbbrev = sqlDataReader["CountryAbbrev"].ToString(),
                                CountryDesc = sqlDataReader["CountryDesc"].ToString(),
                                CountyName = sqlDataReader["CountyName"].ToString(),
                                DemogInfoCityId = sqlDataReader["DemogInfoCityId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCityId"].ToString()),
                                DemogInfoCountyId = sqlDataReader["DemogInfoCountyId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCountyId"].ToString()),
                                DemogInfoCountryId = sqlDataReader["DemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                                DemogInfoSubDivisionId = sqlDataReader["DemogInfoZipId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipId"].ToString()),
                                DemogInfoZipId = sqlDataReader["DemogInfoZipId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipId"].ToString()),
                                DemogInfoZipPlusId = sqlDataReader["DemogInfoZipPlusId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipPlusId"].ToString()),
                                HouseNumber = sqlDataReader["HouseNumber"].ToString(),
                                ZipCode = sqlDataReader["ZipCode"].ToString(),
                                ZipPlus4 = sqlDataReader["ZipPlus4"].ToString(),
                                Comments = sqlDataReader["Comments"].ToString(),
                                FromDate = sqlDataReader["FromDate"].ToString(),
                                ToDate = sqlDataReader["ToDate"].ToString(),
                            },
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return corpAcctLocationModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static List<DeliveryMethodFilterModel> DeliveryMethodFilterList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)

        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.DeliveryMethodFilter ORDER BY ShippingAndHandlingCharges, DeliveryMethodId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<DeliveryMethodFilterModel> deliveryMethodFilterModels = new List<DeliveryMethodFilterModel>();
                while (sqlDataReader.Read())
                {
                    deliveryMethodFilterModels.Add
                    (
                        new DeliveryMethodFilterModel
                        {
                            DeliveryMethodFilterId = long.Parse(sqlDataReader["DeliveryMethodFilterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ShippingAndHandlingCharges = (YesNoEnum)long.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                            DeliveryMethodId = long.Parse(sqlDataReader["DeliveryMethodId"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return deliveryMethodFilterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static List<ItemBundleModel> ItemBundleList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.ItemBundle ORDER BY ItemBundleId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<ItemBundleModel> itemBundleModels = new List<ItemBundleModel>();
                while (sqlDataReader.Read())
                {
                    itemBundleModels.Add
                    (
                        new ItemBundleModel
                        {
                            ItemBundleId = long.Parse(sqlDataReader["ItemBundleId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ParentItemId = long.Parse(sqlDataReader["ParentItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return itemBundleModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemDiscountModel> ItemDiscountList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemDiscountModel> itemDiscountModels = new List<ItemDiscountModel>();
            try
            {
                string sqlStmt;
                sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount ORDER BY CorpAcctId, ItemId";
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemDiscountModels.Add
                    (
                        new ItemDiscountModel
                        {
                            ItemDiscountId = long.Parse(sqlDataReader["ItemDiscountId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                        }
                    );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return itemDiscountModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemModel> ItemList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemModel> itemModels = new List<ItemModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.Item ORDER BY ItemId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemModels.Add
                    (
                        new ItemModel
                        {
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            ItemName = sqlDataReader["ItemName"].ToString(),
                            ItemForSaleId = (YesNoEnum)int.Parse(sqlDataReader["ItemForSaleId"].ToString()),
                            ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                            ItemRateMSRP = float.Parse(sqlDataReader["ItemRateMSRP"].ToString()),
                            ItemShortDesc0 = sqlDataReader["ItemShortDesc0"].ToString(),
                            ItemShortDesc1 = sqlDataReader["ItemShortDesc1"].ToString(),
                            ItemShortDesc2 = sqlDataReader["ItemShortDesc2"].ToString(),
                            ItemShortDesc3 = sqlDataReader["ItemShortDesc3"].ToString(),
                            ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                            ItemStarCount = 4,
                            ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                            ProductItemId = long.Parse(sqlDataReader["ProductItemId"].ToString()),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static int ItemMasterCount(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM RetailSlnSch.ItemMaster", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int totalRowCount = 0;
                if (sqlDataReader.Read())
                {
                    totalRowCount = int.Parse(sqlDataReader[0].ToString());
                }
                sqlDataReader.Close();
                return totalRowCount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemMasterModel> ItemMasterList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "      ORDER BY ItemMasterId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemMasterModels.Add
                    (
                        new ItemMasterModel
                        {
                            ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ImageExtension = sqlDataReader["ImageExtension"].ToString(),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemMasterDesc0 = sqlDataReader["ItemMasterDesc0"].ToString(),
                            ItemMasterDesc1 = sqlDataReader["ItemMasterDesc1"].ToString(),
                            ItemMasterDesc2 = sqlDataReader["ItemMasterDesc2"].ToString(),
                            ItemMasterDesc3 = sqlDataReader["ItemMasterDesc3"].ToString(),
                            ItemMasterDesc = sqlDataReader["ItemMasterDesc"].ToString(),
                            ItemMasterName = sqlDataReader["ItemMasterName"].ToString(),
                            ItemMasterStatusId = (YesNoEnum)int.Parse(sqlDataReader["ItemMasterStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                            ProductItemId = sqlDataReader["ProductItemId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ProductItemId"].ToString()),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemMasterItemSpecModel> ItemMasterItemSpecList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterItemSpecModel> itemMasterItemSpecModels = new List<ItemMasterItemSpecModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemMasterItemSpec ORDER BY ItemMasterId, SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemMasterItemSpecModels.Add
                    (
                        new ItemMasterItemSpecModel
                        {
                            ItemMasterItemSpecId = long.Parse(sqlDataReader["ItemMasterItemSpecId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
                            ItemSpecUnitValue = sqlDataReader["ItemSpecUnitValue"].ToString(),
                            ItemSpecValue = sqlDataReader["ItemSpecValue"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SeqNumItemMaster = sqlDataReader["SeqNumItemMaster"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNumItemMaster"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemMasterItemSpecModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemMasterModel> ItemMasterList(int offSetCount, int rowCount, SqlConnection sqlConnection, SqlConnection sqlConnection2, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
            try
            {
                //long itemMasterId;
                string sqlStmt = "";
                SqlDataReader sqlDataReader;
                #region
                sqlStmt += "        DROP TABLE IF EXISTS #TEMP1" + Environment.NewLine;
                sqlStmt += "        SELECT ItemMaster.ItemMasterId" + Environment.NewLine;
                sqlStmt += "          INTO #TEMP1" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "         WHERE ItemMaster.ItemMasterId > 0" + Environment.NewLine;
                sqlStmt += "      ORDER BY ItemMasterDesc" + Environment.NewLine;
                sqlStmt += $"               OFFSET {offSetCount} ROWS FETCH NEXT {rowCount} ROWS ONLY" + Environment.NewLine;
                sqlStmt += ";" + Environment.NewLine;
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.Item" + Environment.NewLine;
                sqlStmt += "    INNER JOIN #TEMP1" + Environment.NewLine;
                sqlStmt += "            ON Item.ItemMasterId = #TEMP1.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.ItemItemSpec" + Environment.NewLine;
                sqlStmt += "            ON Item.ItemId = ItemItemSpec.ItemId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.ItemSpecMaster" + Environment.NewLine;
                sqlStmt += "            ON ItemItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId" + Environment.NewLine;
                sqlStmt += "     LEFT JOIN Lookup.CodeData" + Environment.NewLine;
                sqlStmt += "            ON ItemSpecMaster.CodeTypeId = CodeData.CodeTypeId" + Environment.NewLine;
                sqlStmt += "           AND ItemItemSpec.ItemSpecUnitValue = CodeData.CodeDataNameId" + Environment.NewLine;
                sqlStmt += "      ORDER BY ItemItemSpec.ItemId" + Environment.NewLine;
                sqlStmt += "              ,ItemItemSpec.SeqNum" + Environment.NewLine;
                sqlStmt += ";" + Environment.NewLine;
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "    INNER JOIN #TEMP1" + Environment.NewLine;
                sqlStmt += "            ON ItemMaster.ItemMasterId = #TEMP1.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.Item" + Environment.NewLine;
                sqlStmt += "            ON ItemMaster.ItemMasterId = Item.ItemMasterId" + Environment.NewLine;
                sqlStmt += "      ORDER BY ItemMaster.ItemMasterDesc" + Environment.NewLine;
                sqlStmt += "              ,Item.ItemSeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                #region
                List<ItemItemSpecModel> itemItemSpecModels = new List<ItemItemSpecModel>();
                #endregion
                while (sqlDataReader.Read())
                {
                    itemItemSpecModels.Add
                    (
                        new ItemItemSpecModel
                        {

                        }
                    );
                }
                #region
                sqlDataReader.NextResult();
                ItemMasterModel itemMasterModel;
                List<ItemSpecModel> itemSpecModels = new List<ItemSpecModel>();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    itemMasterModels.Add
                    (
                        itemMasterModel = new ItemMasterModel
                        {
                            ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ImageExtension = sqlDataReader["ImageExtension"].ToString(),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemMasterDesc0 = sqlDataReader["ItemMasterDesc0"].ToString(),
                            ItemMasterDesc1 = sqlDataReader["ItemMasterDesc1"].ToString(),
                            ItemMasterDesc2 = sqlDataReader["ItemMasterDesc2"].ToString(),
                            ItemMasterDesc3 = sqlDataReader["ItemMasterDesc3"].ToString(),
                            ItemMasterDesc = sqlDataReader["ItemMasterDesc"].ToString(),
                            ItemMasterName = sqlDataReader["ItemMasterName"].ToString(),
                            ItemMasterStatusId = (YesNoEnum)int.Parse(sqlDataReader["ItemMasterStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                            ProductItemId = sqlDataReader["ProductItemId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ProductItemId"].ToString()),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                            ItemModels = new List<ItemModel>(),
                        }
                    );
                    while (sqlDataReaderRead && itemMasterModel.ItemMasterId == long.Parse(sqlDataReader["ItemMasterId"].ToString()))
                    {
                        itemMasterModel.ItemModels.Add
                        (
                            new ItemModel
                            {
                                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                ItemName = sqlDataReader["ItemName"].ToString(),
                                ItemForSaleId = (YesNoEnum)int.Parse(sqlDataReader["ItemForSaleId"].ToString()),
                                ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                                ImageName = sqlDataReader["ImageName"].ToString(),
                                ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                                ItemRateMSRP = float.Parse(sqlDataReader["ItemRateMSRP"].ToString()),
                                ItemShortDesc0 = sqlDataReader["ItemShortDesc0"].ToString(),
                                ItemShortDesc1 = sqlDataReader["ItemShortDesc1"].ToString(),
                                ItemShortDesc2 = sqlDataReader["ItemShortDesc2"].ToString(),
                                ItemShortDesc3 = sqlDataReader["ItemShortDesc3"].ToString(),
                                ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                                ItemStarCount = 4,
                                ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                                ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                                ProductItemId = long.Parse(sqlDataReader["ProductItemId"].ToString()),
                                UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                            }
                        );
                    }
                }
                sqlDataReader.Close();
                #endregion
                #region
                //sqlStmt += "        SELECT *" + Environment.NewLine;
                //sqlStmt += "          FROM RetailSlnSch.ItemMasterItemSpec" + Environment.NewLine;
                //sqlStmt += "    INNER JOIN #TEMP1" + Environment.NewLine;
                //sqlStmt += "            ON ItemMasterItemSpec.ItemMasterId = #TEMP1.ItemMasterId" + Environment.NewLine;
                //sqlStmt += "    INNER JOIN RetailSlnSch.ItemSpecMaster" + Environment.NewLine;
                //sqlStmt += "            ON ItemMasterItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId" + Environment.NewLine;
                //sqlStmt += "     LEFT JOIN Lookup.CodeData" + Environment.NewLine;
                //sqlStmt += "            ON ItemSpecMaster.CodeTypeId = CodeData.CodeTypeId" + Environment.NewLine;
                //sqlStmt += "           AND ItemSpec.ItemSpecUnitValue = CodeData.CodeDataNameId" + Environment.NewLine;
                //sqlStmt += "      ORDER BY ItemMasterItemSpec.ItemMasterId" + Environment.NewLine;
                //sqlStmt += "              ,ItemMasterItemSpec.SeqNum" + Environment.NewLine;
                //sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                #endregion
                #region
                //sqlDataReaderRead = sqlDataReader.Read();
                //while (sqlDataReaderRead)
                //{
                //    itemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString());
                //    while (sqlDataReaderRead && itemMasterId == long.Parse(sqlDataReader["ItemMasterId"].ToString()))
                //    {
                //        sqlDataReaderRead = sqlDataReader.Read();
                //    }
                //}
                #endregion
                return itemMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemMasterModel> ItemMasterListBackup(int offSetCount, int rowCount, SqlConnection sqlConnection, SqlConnection sqlConnection2, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT ItemMaster.ItemMasterId" + Environment.NewLine;
                sqlStmt += "          INTO #TEMP1" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "         WHERE ItemMaster.ItemMasterId > 0" + Environment.NewLine;
                sqlStmt += "      ORDER BY ItemMasterDesc" + Environment.NewLine;
                sqlStmt += $"               OFFSET {offSetCount} ROWS FETCH NEXT {rowCount} ROWS ONLY" + Environment.NewLine;
                sqlStmt += ";" + Environment.NewLine;
                //sqlStmt += "        SELECT" + Environment.NewLine;
                //sqlStmt += "               Item.ItemMasterId" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpec.ClientId" + Environment.NewLine;
                //sqlStmt += "              ,Item.ItemId" + Environment.NewLine;
                //sqlStmt += "              ,Item.ItemSeqNum" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpec.ItemSpecId" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpec.ItemSpecValue" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpec.SeqNumItem" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpecMaster.ItemSpecMasterId" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpecMaster.SpecDesc" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpecMaster.CodeTypeId" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpec.ItemSpecUnitValue" + Environment.NewLine;
                //sqlStmt += "              ,CodeData.CodeDataNameDesc" + Environment.NewLine;
                //sqlStmt += "              ,CodeData.CodeDataDesc0" + Environment.NewLine;
                //sqlStmt += "          FROM RetailSlnSch.ItemSpec" + Environment.NewLine;
                //sqlStmt += "    INNER JOIN RetailSlnSch.ItemSpecMaster" + Environment.NewLine;
                //sqlStmt += "            ON ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId" + Environment.NewLine;
                //sqlStmt += "    INNER JOIN RetailSlnSch.Item" + Environment.NewLine;
                //sqlStmt += "            ON ItemSpec.ItemId = Item.ItemId" + Environment.NewLine;
                //sqlStmt += "    INNER JOIN #TEMP1" + Environment.NewLine;
                //sqlStmt += "            ON Item.ItemMasterId = #TEMP1.ItemMasterId" + Environment.NewLine;
                //sqlStmt += "     LEFT JOIN Lookup.CodeData" + Environment.NewLine;
                //sqlStmt += "            ON ItemSpecMaster.CodeTypeId = CodeData.CodeTypeId" + Environment.NewLine;
                //sqlStmt += "           AND ItemSpec.ItemSpecUnitValue = CodeData.CodeDataNameId" + Environment.NewLine;
                //sqlStmt += "         WHERE ItemSpec.SeqNumItem IS NOT NULL" + Environment.NewLine;
                //sqlStmt += "      ORDER BY Item.ItemMasterId" + Environment.NewLine;
                //sqlStmt += "              ,Item.ItemId" + Environment.NewLine;
                //sqlStmt += "              ,ItemSpec.SeqNumItem" + Environment.NewLine;
                sqlStmt += ";" + Environment.NewLine;
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "    INNER JOIN #TEMP1" + Environment.NewLine;
                sqlStmt += "            ON ItemMaster.ItemMasterId = #TEMP1.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.Item" + Environment.NewLine;
                sqlStmt += "            ON ItemMaster.ItemMasterId = Item.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.ItemItemSpec" + Environment.NewLine;
                sqlStmt += "            ON Item.ItemId = ItemItemSpec.ItemId" + Environment.NewLine;
                sqlStmt += "      ORDER BY ItemMaster.ItemMasterDesc" + Environment.NewLine;
                sqlStmt += "              ,Item.ItemSeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                #endregion
                #region
                ItemMasterModel itemMasterModel;
                List<ItemSpecModel> itemSpecModels = new List<ItemSpecModel>();
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemSpecModels.Add
                    (
                        new ItemSpecModel
                        {
                            ItemSpecId = long.Parse(sqlDataReader["ItemSpecId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ItemModel = new ItemModel
                            {
                                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                                ItemMasterModel = new ItemMasterModel
                                {
                                    ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                                },
                            },
                            SeqNumItem = float.Parse(sqlDataReader["SeqNumItem"].ToString()),
                        }
                    );
                }
                #endregion
                #region
                sqlDataReader.NextResult();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    itemMasterModels.Add
                    (
                        itemMasterModel =new ItemMasterModel
                        {
                            ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ImageExtension = sqlDataReader["ImageExtension"].ToString(),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemMasterDesc0 = sqlDataReader["ItemMasterDesc0"].ToString(),
                            ItemMasterDesc1 = sqlDataReader["ItemMasterDesc1"].ToString(),
                            ItemMasterDesc2 = sqlDataReader["ItemMasterDesc2"].ToString(),
                            ItemMasterDesc3 = sqlDataReader["ItemMasterDesc3"].ToString(),
                            ItemMasterDesc = sqlDataReader["ItemMasterDesc"].ToString(),
                            ItemMasterName = sqlDataReader["ItemMasterName"].ToString(),
                            ItemMasterStatusId = (YesNoEnum)int.Parse(sqlDataReader["ItemMasterStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                            ProductItemId = sqlDataReader["ProductItemId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ProductItemId"].ToString()),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                            ItemModels = new List<ItemModel>(),
                        }
                    );
                    while (sqlDataReaderRead && itemMasterModel.ItemMasterId == long.Parse(sqlDataReader["ItemMasterId"].ToString()))
                    {
                        itemMasterModel.ItemModels.Add
                        (
                            /*itemModel = */new ItemModel
                            {
                                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                ItemName = sqlDataReader["ItemName"].ToString(),
                                ItemForSaleId = (YesNoEnum)int.Parse(sqlDataReader["ItemForSaleId"].ToString()),
                                ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                                ImageName = sqlDataReader["ImageName"].ToString(),
                                ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                                ItemRateMSRP = float.Parse(sqlDataReader["ItemRateMSRP"].ToString()),
                                ItemShortDesc0 = sqlDataReader["ItemShortDesc0"].ToString(),
                                ItemShortDesc1 = sqlDataReader["ItemShortDesc1"].ToString(),
                                ItemShortDesc2 = sqlDataReader["ItemShortDesc2"].ToString(),
                                ItemShortDesc3 = sqlDataReader["ItemShortDesc3"].ToString(),
                                ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                                ItemStarCount = 4,
                                ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                                ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                                ProductItemId = long.Parse(sqlDataReader["ProductItemId"].ToString()),
                                UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                #endregion
                sqlDataReader.Close();
                return itemMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemItemSpecModel> ItemItemSpecList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemItemSpecModel> itemItemSpecModels = new List<ItemItemSpecModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemItemSpec ORDER BY ItemId, SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemItemSpecModels.Add
                    (
                        new ItemItemSpecModel
                        {
                            ItemItemSpecId = long.Parse(sqlDataReader["ItemItemSpecId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
                            ItemSpecUnitValue = sqlDataReader["ItemSpecUnitValue"].ToString(),
                            ItemSpecValue = sqlDataReader["ItemSpecValue"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SeqNumItem = sqlDataReader["SeqNumItem"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNumItem"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemItemSpecModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemSpecMasterModel> ItemSpecMasterList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemSpecMasterModel> itemSpecMasterModels = new List<ItemSpecMasterModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemSpecMaster LEFT JOIN Lookup.CodeType ON ItemSpecMaster.CodeTypeId = CodeType.CodeTypeId ORDER BY SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemSpecMasterModels.Add
                    (
                        new ItemSpecMasterModel
                        {
                            ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            BookFlag = bool.Parse(sqlDataReader["BookFlag"].ToString()),
                            CodeTypeId = sqlDataReader["CodeTypeId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["CodeTypeId"].ToString()),
                            SpecDesc = sqlDataReader["SpecDesc"].ToString(),
                            SpecName = sqlDataReader["SpecName"].ToString(),
                            SpecUnitType = sqlDataReader["SpecUnitType"].ToString(),
                            SpecValueType = sqlDataReader["SpecValueType"].ToString(),
                            IsMandatory = bool.Parse(sqlDataReader["IsMandatory"].ToString()),
                            ItemMasterFlag = bool.Parse(sqlDataReader["ItemMasterFlag"].ToString()),
                            ProductFlag = bool.Parse(sqlDataReader["ProductFlag"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            CodeTypeModel = new CodeTypeModel
                            {
                                CodeTypeId = sqlDataReader["CodeTypeId"].ToString() == "" ? 0 : long.Parse(sqlDataReader["CodeTypeId"].ToString()),
                                CodeTypeDesc = sqlDataReader["CodeTypeDesc"].ToString(),
                                CodeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString(),
                            },
                        }
                    );

                }
                sqlDataReader.Close();
                return itemSpecMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemSpecModel> ItemSpecList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemSpecModel> itemSpecModels = new List<ItemSpecModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemSpec ORDER BY ItemId, SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemSpecModels.Add
                    (
                        new ItemSpecModel
                        {
                            ItemSpecId = long.Parse(sqlDataReader["ItemSpecId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
                            ItemSpecUnitValue = sqlDataReader["ItemSpecUnitValue"].ToString(),
                            ItemSpecValue = sqlDataReader["ItemSpecValue"].ToString(),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SeqNumItem = sqlDataReader["SeqNumItem"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNumItem"].ToString()),
                            SeqNumItemMaster = sqlDataReader["SeqNumItemMaster"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNumItemMaster"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemSpecModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<OrderListDataModel> OrderList(long? corpAcctId, long? personId, long? createdForPersonId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               OrderHeader.OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,CreatedForAspNetUser.Email AS CreatedForEmailAddress" + Environment.NewLine;
                sqlStmt += "              ,CreatedForPerson.PersonId AS CreatedForPersonId" + Environment.NewLine;
                sqlStmt += "              ,CreatedForPerson.FirstName AS CreatedForFirstName" + Environment.NewLine;
                sqlStmt += "              ,CreatedForPerson.LastName AS CreatedForLastName" + Environment.NewLine;
                sqlStmt += "              ,AspNetUser.Email AS EmailAddress" + Environment.NewLine;
                sqlStmt += "              ,Person.PersonId" + Environment.NewLine;
                sqlStmt += "              ,Person.FirstName" + Environment.NewLine;
                //sqlStmt += "              ,OrderHeader.InvoiceTypeId" + Environment.NewLine;
                sqlStmt += "              ,Person.LastName" + Environment.NewLine;
                sqlStmt += "              ,OrderHeader.OrderDateTime" + Environment.NewLine;
                sqlStmt += "              ,OrderHeader.OrderStatusId" + Environment.NewLine;
                sqlStmt += "              ,Person.PersonId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.BalanceDue" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.InvoiceTypeId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.ShippingAndHandlingCharges" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.TotalAmountPaid" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.TotalDiscountAmount" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.TotalInvoiceAmount" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.TotalOrderAmount" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummary.TotalTaxAmount" + Environment.NewLine;
                //sqlStmt += "              ," + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.OrderHeader" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.OrderHeaderSummary" + Environment.NewLine;
                sqlStmt += "            ON OrderHeader.OrderHeaderId = OrderHeaderSummary.OrderHeaderId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person" + Environment.NewLine;
                sqlStmt += "            ON OrderHeader.PersonId = Person.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "            ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person AS CreatedForPerson" + Environment.NewLine;
                sqlStmt += "            ON OrderHeader.CreatedForPersonId = CreatedForPerson.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser AS CreatedForAspNetUser" + Environment.NewLine;
                sqlStmt += "            ON CreatedForPerson.AspNetUserId = CreatedForAspNetUser.AspNetUserId" + Environment.NewLine;
                if (corpAcctId != null && corpAcctId > 0)
                {
                    sqlStmt += "    INNER JOIN RetailSlnSch.PersonExtn1" + Environment.NewLine;
                    sqlStmt += "            ON Person.PersonId = PersonExtn1.PersonId" + Environment.NewLine;
                    sqlStmt += $"           AND PersonExtn1.CorpAcctId = {corpAcctId}" + Environment.NewLine;
                }
                sqlStmt += "         WHERE OrderHeader.OrderHeaderId > 0" + Environment.NewLine;
                sqlStmt += "      ORDER BY OrderHeader.OrderHeaderId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                #endregion
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<OrderListDataModel> orderListDataModels = new List<OrderListDataModel>();
                while (sqlDataReader.Read())
                {
                    orderListDataModels.Add
                    (
                        new OrderListDataModel
                        {
                            OrderHeaderId = long.Parse(sqlDataReader["OrderHeaderId"].ToString()),
                            CreatedForEmailAddress = sqlDataReader["CreatedForEmailAddress"].ToString(),
                            CreatedForFirstName = sqlDataReader["CreatedForFirstName"].ToString(),
                            CreatedForLastName = sqlDataReader["CreatedForLastName"].ToString(),
                            CreatedForPersonId = long.Parse(sqlDataReader["CreatedForPersonId"].ToString()),
                            EmailAddress = sqlDataReader["EmailAddress"].ToString(),
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                            OrderDateTime = DateTime.Parse(sqlDataReader["OrderDateTime"].ToString()).ToString("MMM-dd-yyyy h:mm tt"),
                            OrderStatusId = (OrderStatusEnum)long.Parse(sqlDataReader["OrderStatusId"].ToString()),
                            PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),

                            BalanceDue = float.Parse(sqlDataReader["BalanceDue"].ToString()),
                            InvoiceTypeId = (InvoiceTypeEnum)long.Parse(sqlDataReader["InvoiceTypeId"].ToString()),
                            ShippingAndHandlingCharges = float.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                            TotalAmountPaid = float.Parse(sqlDataReader["TotalAmountPaid"].ToString()),
                            TotalDiscountAmount = float.Parse(sqlDataReader["TotalDiscountAmount"].ToString()),
                            TotalInvoiceAmount = float.Parse(sqlDataReader["TotalInvoiceAmount"].ToString()),
                            TotalOrderAmount = float.Parse(sqlDataReader["TotalOrderAmount"].ToString()),
                            TotalTaxAmount = float.Parse(sqlDataReader["TotalTaxAmount"].ToString()),
                        }
                    );
                }
                sqlDataReader.Close();
                return orderListDataModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<PaymentModeFilterModel> PaymentModeFilterList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)

        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.PaymentModeFilter ORDER BY CreditSale, PaymentModeId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<PaymentModeFilterModel> paymentModeFilterModels = new List<PaymentModeFilterModel>();
                while (sqlDataReader.Read())
                {
                    paymentModeFilterModels.Add
                    (
                        new PaymentModeFilterModel
                        {
                            PaymentModeFilterId = long.Parse(sqlDataReader["PaymentModeFilterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CreditSale = (YesNoEnum)long.Parse(sqlDataReader["CreditSale"].ToString()),
                            PaymentModeId = long.Parse(sqlDataReader["PaymentModeId"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return paymentModeFilterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static List<PickupLocationModel> PickupLocationList(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                #region
                sqlStmt += "        SELECT PickupLocation.*" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.AddressLine1" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.AddressLine2" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCountry.CountryAbbrev" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoSubDivisionId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoSubDivision.StateAbbrev" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoCountyId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCounty.CountyName" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoCityId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCity.CityName" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoZipId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoZip.ZipCode" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.DemogInfoZipPlusId" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.PickupLocation" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoAddress ON PickupLocation.LocationDemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoCountry ON DemogInfoAddress.DemogInfoCountryId = DemogInfoCountry.DemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoSubDivision ON DemogInfoAddress.DemogInfoSubDivisionId = DemogInfoSubDivision.DemogInfoSubDivisionId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoCounty ON DemogInfoAddress.DemogInfoCountyId = DemogInfoCounty.DemogInfoCountyId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoCity ON DemogInfoAddress.DemogInfoCityId = DemogInfoCity.DemogInfoCityId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoZip ON DemogInfoAddress.DemogInfoZipId = DemogInfoZip.DemogInfoZipId" + Environment.NewLine;
                sqlStmt += "      ORDER BY PickupLocation.PickupLocationId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<PickupLocationModel> pickupLocationModels = new List<PickupLocationModel>();
                #endregion
                while (sqlDataReader.Read())
                {
                    pickupLocationModels.Add
                    (
                        new PickupLocationModel
                        {
                            PickupLocationId = long.Parse(sqlDataReader["PickupLocationId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            LocationDemogInfoAddressId = long.Parse(sqlDataReader["LocationDemogInfoAddressId"].ToString()),
                            LocationDesc = sqlDataReader["LocationDesc"].ToString(),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            DemogInfoAddressModel = new DemogInfoAddressModel
                            {
                                DemogInfoAddressId = long.Parse(sqlDataReader["DemogInfoAddressId"].ToString()),
                                AddressLine1 = sqlDataReader["AddressLine1"].ToString(),
                                AddressLine2 = sqlDataReader["AddressLine2"].ToString(),
                                DemogInfoCountryId = long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                                CountryAbbrev = sqlDataReader["CountryAbbrev"].ToString(),
                                DemogInfoSubDivisionId = long.Parse(sqlDataReader["DemogInfoSubDivisionId"].ToString()),
                                StateAbbrev = sqlDataReader["StateAbbrev"].ToString(),
                                DemogInfoCountyId = long.Parse(sqlDataReader["DemogInfoCountyId"].ToString()),
                                CountyName = sqlDataReader["CountyName"].ToString(),
                                DemogInfoCityId = long.Parse(sqlDataReader["DemogInfoCityId"].ToString()),
                                CityName = sqlDataReader["CityName"].ToString(),
                                DemogInfoZipId = long.Parse(sqlDataReader["DemogInfoZipId"].ToString()),
                                ZipCode = sqlDataReader["ZipCode"].ToString(),
                            },
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return pickupLocationModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
