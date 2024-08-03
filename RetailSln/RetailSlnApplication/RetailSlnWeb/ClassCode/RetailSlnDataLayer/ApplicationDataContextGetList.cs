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
        public static List<FestivalListModel> GetFestivalLists(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.FestivalList ORDER BY FestivalListId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<FestivalListModel> featuredItemsModels = new List<FestivalListModel>();
                while (sqlDataReader.Read())
                {
                    featuredItemsModels.Add
                    (
                        new FestivalListModel
                        {
                            FestivalListId = long.Parse(sqlDataReader["FestivalListId"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishDate = sqlDataReader["FinishDate"].ToString(),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            EventDate = sqlDataReader["EventDate"].ToString(),
                            EventDesc = sqlDataReader["EventDesc"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return featuredItemsModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<FestivalListImageModel> GetFestivalListImages(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.FestivalListImage ORDER BY FestivalListId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<FestivalListImageModel> featuredItemsModels = new List<FestivalListImageModel>();
                while (sqlDataReader.Read())
                {
                    featuredItemsModels.Add
                    (
                        new FestivalListImageModel
                        {
                            FestivalListImageId = long.Parse(sqlDataReader["FestivalListImageId"].ToString()),
                            FestivalListId = long.Parse(sqlDataReader["FestivalListId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ImageExtension = sqlDataReader["ImageExtension"].ToString(),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ImageNotes = sqlDataReader["ImageNotes"].ToString(),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return featuredItemsModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<CategoryModel> GetCategorys(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        public static List<CorpAcctModel> GetCorpAccts(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                            CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                            CorpAcctTypeId = (CorpAcctTypeEnum)int.Parse(sqlDataReader["CorpAcctTypeId"].ToString()),
                            CreditDays = short.Parse(sqlDataReader["CreditDays"].ToString()),
                            CreditLimit = float.Parse(sqlDataReader["CreditLimit"].ToString()),
                            CreditSale = bool.Parse(sqlDataReader["CreditSale"].ToString()),
                            MinOrderAmount = float.Parse(sqlDataReader["MinOrderAmount"].ToString()),
                            ShippingAndHandlingCharges = bool.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                            TaxIdentNum = sqlDataReader["TaxIdentNum"].ToString(),
                            DemogInfoAddressModels = new List<DemogInfoAddressModel>(),
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
        public static List<DiscountDtlModel> GetDiscountDtls(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.DiscountDtl ORDER BY DiscountDtlId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<DiscountDtlModel> discountDtlModels = new List<DiscountDtlModel>();
                while (sqlDataReader.Read())
                {
                    discountDtlModels.Add
                    (
                        new DiscountDtlModel
                        {
                            DiscountDtlId = long.Parse(sqlDataReader["DiscountDtlId"].ToString()),
                            CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                            CorpAcctDiscountPercent = float.Parse(sqlDataReader["CorpAcctDiscountPercent"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return discountDtlModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static List<GiftCertModel> GetGiftCerts(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                //sqlStmt += "SELECT GiftCert.*, AspNetUser.UserName AS SenderEmailAddress FROM RetailSlnSch.GiftCert INNER JOIN ArchLib.Person ON GiftCert.PersonId = Person.PersonId INNER JOIN ArchLib.AspNetUser ON Person.AspNetUserId = AspNetUser.AspNetUserId WHERE GiftCertId > 0 ORDER BY GiftCertId" + Environment.NewLine;
                sqlStmt += "        SELECT GiftCert.*" + Environment.NewLine;
                sqlStmt += "              ,AspNetUser.UserName AS SenderEmailAddress" + Environment.NewLine;
                sqlStmt += "              ,AspNetUser1.UserName AS RecipientEmailAddress" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.GiftCert" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person" + Environment.NewLine;
                sqlStmt += "            ON GiftCert.PersonId = Person.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "            ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person AS Person1" + Environment.NewLine;
                sqlStmt += "            ON GiftCert.RecipientPersonId = Person1.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser AS AspNetUser1" + Environment.NewLine;
                sqlStmt += "            ON Person1.AspNetUserId = AspNetUser1.AspNetUserId" + Environment.NewLine;
                sqlStmt += "" + Environment.NewLine;
                sqlStmt += "" + Environment.NewLine;
                sqlStmt += "         WHERE GiftCertId > 0" + Environment.NewLine;
                sqlStmt += "      ORDER BY GiftCertId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<GiftCertModel> giftCertModels = new List<GiftCertModel>();
                while (sqlDataReader.Read())
                {
                    giftCertModels.Add
                    (
                        new GiftCertModel
                        {
                            GiftCertId = long.Parse(sqlDataReader["GiftCertId"].ToString()),
                            SenderFullName = sqlDataReader["SenderFullName"].ToString(),
                            SenderEmailAddress = sqlDataReader["SenderEmailAddress"].ToString(),
                            RecipientFullName = sqlDataReader["RecipientFullName"].ToString(),
                            RecipientEmailAddress = sqlDataReader["RecipientEmailAddress"].ToString(),
                            GiftCertNumber = long.Parse(sqlDataReader["GiftCertNumber"].ToString()),
                            GiftCertAmount = float.Parse(sqlDataReader["GiftCertAmount"].ToString()),
                            GiftCertBalanceAmount = float.Parse(sqlDataReader["GiftCertBalanceAmount"].ToString()),
                            GiftCertUsedAmount = float.Parse(sqlDataReader["GiftCertUsedAmount"].ToString()),
                            //CategoryName = sqlDataReader["CategoryName"].ToString(),
                            //CategoryStatusId = (CategoryStatusEnum)int.Parse(sqlDataReader["CategoryStatusId"].ToString()),
                            //CategoryTypeId = (CategoryTypeEnum)int.Parse(sqlDataReader["CategoryTypeId"].ToString()),
                            //ImageName = sqlDataReader["ImageName"].ToString(),
                            //UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return giftCertModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemBundleDiscountModel> GetItemBundleDiscounts(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.ItemBundleDiscount ORDER BY ItemBundleDiscountId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<ItemBundleDiscountModel> itemBundleDiscountModels = new List<ItemBundleDiscountModel>();
                while (sqlDataReader.Read())
                {
                    itemBundleDiscountModels.Add
                    (
                        new ItemBundleDiscountModel
                        {
                            ItemBundleDiscountId = long.Parse(sqlDataReader["ItemBundleDiscountId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ItemBundleId = long.Parse(sqlDataReader["ItemBundleId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return itemBundleDiscountModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<CategoryItemMasterHierModel> GetCategoryItemMasterHiers(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemMasterHier ORDER BY ParentCategoryId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryItemMasterHierModel> categoryItemHierModels = new List<CategoryItemMasterHierModel>();
                while (sqlDataReader.Read())
                {
                    categoryItemHierModels.Add
                    (
                        new CategoryItemMasterHierModel
                        {
                            CategoryItemMasterHierId = long.Parse(sqlDataReader["CategoryItemMasterHierId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CategoryId = string.IsNullOrWhiteSpace(sqlDataReader["CategoryId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
                            CategoryOrItem = sqlDataReader["CategoryOrItem"].ToString(),
                            ItemMasterId = string.IsNullOrWhiteSpace(sqlDataReader["ItemMasterId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            ProcessType = sqlDataReader["ProcessType"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryItemHierModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<CategoryItemMasterHierModel> GetCategoryItemMasterHiers(long parentCategoryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += $"SELECT * FROM RetailSlnSch.CategoryItemMasterHier INNER JOIN RetailSlnSch.Category ON CategoryItemMasterHier.CategoryId = Category.CategoryId WHERE CategoryItemMasterHier.ParentCategoryId = ${parentCategoryId} ORDER BY SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryItemMasterHierModel> categoryModels = new List<CategoryItemMasterHierModel>();
                while (sqlDataReader.Read())
                {
                    categoryModels.Add
                    (
                        new CategoryItemMasterHierModel
                        {
                            CategoryItemMasterHierId = long.Parse(sqlDataReader["CategoryItemMasterHierId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CategoryId = string.IsNullOrWhiteSpace(sqlDataReader["CategoryId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
                            CategoryOrItem = sqlDataReader["CategoryOrItem"].ToString(),
                            ItemMasterId = string.IsNullOrWhiteSpace(sqlDataReader["ItemMasterId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["ItemId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            ProcessType = sqlDataReader["ProcessType"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            CategoryModel = new CategoryModel
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
                            },
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
        public static List<ItemBundleItemModel> GetItemBundleItems(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.ItemBundleItem ORDER BY ItemBundleItemId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<ItemBundleItemModel> itemBundleItemModels = new List<ItemBundleItemModel>();
                while (sqlDataReader.Read())
                {
                    itemBundleItemModels.Add
                    (
                        new ItemBundleItemModel
                        {
                            ItemBundleItemId = long.Parse(sqlDataReader["ItemBundleItemId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            BundleItemId = long.Parse(sqlDataReader["BundleItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            Quantity = short.Parse(sqlDataReader["Quantity"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return itemBundleItemModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemModel> GetItems(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                            ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            ItemName = sqlDataReader["ItemName"].ToString(),
                            ItemDesc = sqlDataReader["ItemDesc"].ToString(),
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
        public static List<ItemMasterModel> GetItemMasters(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemMaster ORDER BY ItemMasterId", sqlConnection);
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
        public static List<ItemMasterItemSpecModel> GetItemMasterItemSpecs(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterItemSpecModel> itemMasterItemSpecModels = new List<ItemMasterItemSpecModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemMasterItemSpec ORDER BY ItemMasterId, SeqNumItemMaster", sqlConnection);
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
                            ItemSpecId = long.Parse(sqlDataReader["ItemSpecId"].ToString()),
                            SeqNumItemMaster = float.Parse(sqlDataReader["SeqNumItemMaster"].ToString()),
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
        public static List<ItemSpecMasterModel> GetItemSpecMasters(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemSpecMasterModel> itemSpecMasterModels = new List<ItemSpecMasterModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemSpecMaster ORDER BY SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemSpecMasterModels.Add
                    (
                        new ItemSpecMasterModel
                        {
                            ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CodeTypeId = sqlDataReader["CodeTypeId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["CodeTypeId"].ToString()),
                            SpecDesc = sqlDataReader["SpecDesc"].ToString(),
                            SpecName = sqlDataReader["SpecName"].ToString(),
                            SpecUnitType = sqlDataReader["SpecUnitType"].ToString(),
                            SpecValueType = sqlDataReader["SpecValueType"].ToString(),
                            IsMandatory = bool.Parse(sqlDataReader["IsMandatory"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
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
        public static List<ItemSpecModel> GetItemSpecs(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        //public static List<ItemItemSpecModel> GetItemItemSpecs(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    List<ItemItemSpecModel> itemItemSpecModels = new List<ItemItemSpecModel>();
        //    try
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemItemSpec ORDER BY ItemId, SeqNum", sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        while (sqlDataReader.Read())
        //        {
        //            itemItemSpecModels.Add
        //            (
        //                new ItemItemSpecModel
        //                {
        //                    ItemItemSpecId = long.Parse(sqlDataReader["ItemItemSpecId"].ToString()),
        //                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                    ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
        //                    ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                }
        //            );

        //        }
        //        sqlDataReader.Close();
        //        return itemItemSpecModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static List<ItemMasterItemSpecModel> GetItemMasterItemSpecs(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    List<ItemMasterItemSpecModel> itemMasterItemSpecModels = new List<ItemMasterItemSpecModel>();
        //    try
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemItemSpec ORDER BY ItemId, SeqNum", sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        while (sqlDataReader.Read())
        //        {
        //            itemMasterItemSpecModels.Add
        //            (
        //                new ItemMasterItemSpecModel
        //                {
        //                    ItemMasterItemSpecId = long.Parse(sqlDataReader["ItemMasterItemSpecId"].ToString()),
        //                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                    ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
        //                    ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                }
        //            );

        //        }
        //        sqlDataReader.Close();
        //        return itemMasterItemSpecModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static List<ItemModel> GetItemsAssigned(long categoryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    List<ItemModel> itemModels = new List<ItemModel>();
        //    try
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.CategoryItem INNER JOIN RetailSlnSch.Item ON CategoryItem.ItemId = Item.ItemId WHERE CategoryItem.CategoryId = " + categoryId + " ORDER BY CategoryItem.SeqNum", sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        while (sqlDataReader.Read())
        //        {
        //            itemModels.Add
        //            (
        //                new ItemModel
        //                {
        //                    ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
        //                    ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
        //                    ItemName = sqlDataReader["ItemName"].ToString(),
        //                    ItemDesc = sqlDataReader["ItemDesc"].ToString(),
        //                    ItemForSaleId = (YesNoEnum)int.Parse(sqlDataReader["ItemForSaleId"].ToString()),
        //                    ImageName = sqlDataReader["ImageName"].ToString(),
        //                    ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
        //                    ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
        //                    ItemStarCount = int.Parse(sqlDataReader["ItemStarCount"].ToString()),
        //                    ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
        //                    ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
        //                    UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
        //                }
        //            );

        //        }
        //        sqlDataReader.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    return itemModels;
        //}
        //public static List<ItemSpecModel> GetItemSpecs(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        List<ItemSpecModel> itemSpecModels = new List<ItemSpecModel>();
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemSpec INNER JOIN RetailSlnSch.ItemSpecMaster ON ItemSpec.ItemSpecMasterId = ItemSpecMaster.ItemSpecMasterId WHERE ItemSpec.ItemId = " + itemId + " ORDER BY ItemSpec.SeqNum", sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        while (sqlDataReader.Read())
        //        {
        //            itemSpecModels.Add
        //            (
        //                new ItemSpecModel
        //                {
        //                    ItemSpecId = long.Parse(sqlDataReader["ItemSpecId"].ToString()),
        //                    ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
        //                    ItemSpecUnitValue = sqlDataReader["ItemSpecUnitValue"].ToString(),
        //                    ItemSpecValue = sqlDataReader["ItemSpecValue"].ToString(),
        //                    ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                    ItemSpecMasterModel = new ItemSpecMasterModel
        //                    {
        //                        ItemSpecMasterId = long.Parse(sqlDataReader["ItemSpecMasterId"].ToString()),
        //                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                        CodeTypeId = sqlDataReader["CodeTypeId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["CodeTypeId"].ToString()),
        //                        SpecDesc = sqlDataReader["SpecDesc"].ToString(),
        //                        SpecName = sqlDataReader["SpecName"].ToString(),
        //                        SpecUnitType = sqlDataReader["SpecUnitType"].ToString(),
        //                        SpecValueType = sqlDataReader["SpecValueType"].ToString(),
        //                        IsMandatory = bool.Parse(sqlDataReader["IsMandatory"].ToString()),
        //                        SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                    },
        //                }
        //            );
        //        }
        //        sqlDataReader.Close();
        //        return itemSpecModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        public static List<ItemBundleItemModel> GetItemBundleItems(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<ItemBundleItemModel> itemBundleItemModels = new List<ItemBundleItemModel>();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemBundleItem WHERE ItemBundleItem.ItemId = " + itemId + " ORDER BY ItemBundleItem.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemBundleItemModels.Add
                    (
                        new ItemBundleItemModel
                        {
                            BundleItemId = long.Parse(sqlDataReader["BundleItemId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                    );
                }
                sqlDataReader.Close();
                return itemBundleItemModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemImageModel> GetItemImages(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<ItemImageModel> itemImageModels = new List<ItemImageModel>();
                ItemImageModel itemImageModel;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemImage INNER JOIN RetailSlnSch.ItemImageSrcSet ON ItemImage.ItemImageId = ItemImageSrcSet.ItemImageId ORDER BY ItemImage.ItemId, ItemImage.SeqNum, ItemImageSrcSet.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    itemImageModels.Add
                    (
                        itemImageModel = new ItemImageModel
                        {
                            ItemImageId = long.Parse(sqlDataReader["ItemImageId"].ToString()),
                            ImageDesc = sqlDataReader["ImageDesc"].ToString(),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ItemImageSrcSetModels = new List<ItemImageSrcSetModel>(),
                        }
                    );
                    while (sqlDataReaderRead && itemImageModel.SeqNum == float.Parse(sqlDataReader["SeqNum"].ToString()))
                    {
                        itemImageModel.ItemImageSrcSetModels.Add
                        (
                            new ItemImageSrcSetModel
                            {
                                ItemImageSrcSetId = long.Parse(sqlDataReader["ItemImageSrcSetId"].ToString()),
                                ImageHeight = int.Parse(sqlDataReader["ImageHeight"].ToString()),
                                ImageHeightUnit = sqlDataReader["ImageHeightUnit"].ToString(),
                                ImageWidth = int.Parse(sqlDataReader["ImageWidth"].ToString()),
                                ImageWidthUnit = sqlDataReader["ImageWidthUnit"].ToString(),
                                ImageName = sqlDataReader["ImageName"].ToString(),
                                ItemImageId = long.Parse(sqlDataReader["ItemImageId"].ToString()),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                return itemImageModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemImageModel> GetItemImages(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<ItemImageModel> itemImageModels = new List<ItemImageModel>();
                ItemImageModel itemImageModel;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemImage INNER JOIN RetailSlnSch.ItemImageSrcSet ON ItemImage.ItemImageId = ItemImageSrcSet.ItemImageId WHERE ItemImage.ItemId = " + itemId + " ORDER BY ItemImage.SeqNum, ItemImageSrcSet.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    itemImageModels.Add
                    (
                        itemImageModel = new ItemImageModel
                        {
                            ItemImageId = long.Parse(sqlDataReader["ItemImageId"].ToString()),
                            ImageDesc = sqlDataReader["ImageDesc"].ToString(),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ItemImageSrcSetModels = new List<ItemImageSrcSetModel>(),
                        }
                    );
                    while (sqlDataReaderRead && itemImageModel.SeqNum == float.Parse(sqlDataReader["SeqNum"].ToString()))
                    {
                        itemImageModel.ItemImageSrcSetModels.Add
                        (
                            new ItemImageSrcSetModel
                            {
                                ItemImageSrcSetId = long.Parse(sqlDataReader["ItemImageSrcSetId"].ToString()),
                                ImageHeight = int.Parse(sqlDataReader["ImageHeight"].ToString()),
                                ImageHeightUnit = sqlDataReader["ImageHeightUnit"].ToString(),
                                ImageWidth = int.Parse(sqlDataReader["ImageWidth"].ToString()),
                                ImageWidthUnit = sqlDataReader["ImageWidthUnit"].ToString(),
                                ImageName = sqlDataReader["ImageName"].ToString(),
                                ItemImageId = long.Parse(sqlDataReader["ItemImageId"].ToString()),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                return itemImageModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemInfoModel> GetItemInfos(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemInfoModel> itemInfoModels = new List<ItemInfoModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemInfo ORDER BY ItemInfo.ItemId, ItemInfo.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemInfoModels.Add
                    (
                        new ItemInfoModel
                        {
                            ItemInfoId = long.Parse(sqlDataReader["ItemInfoId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ItemInfoLabelText = sqlDataReader["ItemInfoLabelText"].ToString(),
                            ItemInfoText = sqlDataReader["ItemInfoText"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemInfoModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemInfoModel> GetItemInfos(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemInfoModel> itemInfoModels = new List<ItemInfoModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemInfo WHERE ItemInfo.ItemId = " + itemId + " ORDER BY ItemInfo.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemInfoModels.Add
                    (
                        new ItemInfoModel
                        {
                            ItemInfoId = long.Parse(sqlDataReader["ItemInfoId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ItemInfoLabelText = sqlDataReader["ItemInfoLabelText"].ToString(),
                            ItemInfoText = sqlDataReader["ItemInfoText"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemInfoModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemModel> GetItemsUnassigned(long categoryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemModel> itemModels = new List<ItemModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.Item WHERE NOT EXISTS (SELECT 1 FROM RetailSlnSch.CategoryItem WHERE CategoryId = " + categoryId + " AND Item.ItemId = CategoryItem.ItemId) ORDER BY Item.ItemId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemModels.Add
                    (
                        new ItemModel
                        {
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            ItemName = sqlDataReader["ItemName"].ToString(),
                            ItemDesc = sqlDataReader["ItemDesc"].ToString(),
                            ItemForSaleId = (YesNoEnum)int.Parse(sqlDataReader["ItemForSaleId"].ToString()),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                            ItemStarCount = int.Parse(sqlDataReader["ItemStarCount"].ToString()),
                            ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                            ProductItemId = long.Parse(sqlDataReader["ProductItemId"].ToString()),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        }
                    );

                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return itemModels;
        }
        public static List<OrderListModel> GetOrderLists(long? personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                if (personId == null)
                {
                    sqlStmt = "SELECT * FROM RetailSlnSch.OrderHeader INNER JOIN RetailSlnSch.OrderDetail ON OrderHeader.OrderHeaderId = OrderDetail.OrderHeaderId AND OrderDetail.OrderDetailTypeId IN(400, 700, 1000, 1100) ORDER BY OrderHeader.OrderHeaderId, OrderDetail.OrderDetailTypeId";
                }
                else
                {
                    sqlStmt = "SELECT * FROM RetailSlnSch.OrderHeader INNER JOIN RetailSlnSch.OrderDetail ON OrderHeader.OrderHeaderId = OrderDetail.OrderHeaderId AND OrderDetail.OrderDetailTypeId IN(400, 700, 1000, 1100) WHERE OrderHeader.PersonId = " + personId + " ORDER BY OrderHeader.OrderHeaderId, OrderDetail.OrderDetailTypeId";
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                OrderListModel orderListModel;
                List<OrderListModel> orderListModels = new List<OrderListModel>();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    orderListModels.Add
                    (
                        orderListModel = new OrderListModel
                        {
                            OrderHeaderId = long.Parse(sqlDataReader["OrderHeaderId"].ToString()),
                            OrderNumber = sqlDataReader["OrderNumber"].ToString(),
                            OrderDate = DateTime.Parse(sqlDataReader["OrderDate"].ToString()).ToString("MMM-dd-yyyy"),
                            TotalInvoiceAmount = 0,
                            ShippingHandlingCharges = 0,
                            TotalAmountPaid = 0,
                            BalanceDue = 0,
                        }
                    );
                    while (sqlDataReaderRead && orderListModel.OrderHeaderId == long.Parse(sqlDataReader["OrderHeaderId"].ToString()))
                    {
                        switch (sqlDataReader["OrderDetailTypeId"].ToString())
                        {
                            case "400":
                                orderListModel.ShippingHandlingCharges += float.Parse(sqlDataReader["OrderAmount"].ToString());
                                break;
                            case "700":
                                orderListModel.TotalInvoiceAmount += float.Parse(sqlDataReader["OrderAmount"].ToString());
                                break;
                            case "1000":
                                orderListModel.TotalAmountPaid += float.Parse(sqlDataReader["OrderAmount"].ToString());
                                break;
                            case "1100":
                                orderListModel.BalanceDue += float.Parse(sqlDataReader["OrderAmount"].ToString());
                                break;
                            default:
                                break;
                        }
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                return orderListModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<SearchKeywordModel> GetSearchKeywords(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT SearchKeyword.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "              ,SearchKeyword.ClientId" + Environment.NewLine;
                sqlStmt += "              ,SearchKeyword.SearchKeywordText" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.SearchMetaDataId" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.EntityTypeNameDesc" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.EntityId" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.SeqNum" + Environment.NewLine;
                sqlStmt += "              ,Category.CategoryNameDesc AS EntityDesc" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchKeyword" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "            ON SearchKeyword.SearchKeywordId = SearchMetaData.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.Category" + Environment.NewLine;
                sqlStmt += "            ON SearchMetaData.EntityId = Category.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE SearchMetaData.EntityTypeNameDesc = 'CATEGORY'" + Environment.NewLine;
                sqlStmt += "         UNION" + Environment.NewLine;
                sqlStmt += "        SELECT SearchKeyword.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "              ,SearchKeyword.ClientId" + Environment.NewLine;
                sqlStmt += "              ,SearchKeyword.SearchKeywordText" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.SearchMetaDataId" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.EntityTypeNameDesc" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.EntityId" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.SeqNum" + Environment.NewLine;
                sqlStmt += "              ,Item.ItemUniqueDesc AS EntityDesc" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchKeyword" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "            ON SearchKeyword.SearchKeywordId = SearchMetaData.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.Item" + Environment.NewLine;
                sqlStmt += "            ON SearchMetaData.EntityId = Item.ItemId" + Environment.NewLine;
                sqlStmt += "         WHERE SearchMetaData.EntityTypeNameDesc = 'ITEM'" + Environment.NewLine;
                sqlStmt += "      ORDER BY" + Environment.NewLine;
                sqlStmt += "               SearchKeyword.SearchKeywordText" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.EntityTypeNameDesc" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.SeqNum" + Environment.NewLine;
                #endregion
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<SearchKeywordModel> searchKeywordModels = new List<SearchKeywordModel>();
                SearchKeywordModel searchKeywordModel;
                SearchMetaDataModel searchMetaDataModel;
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    searchKeywordModels.Add
                    (
                        searchKeywordModel = new SearchKeywordModel
                        {
                            SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            SearchKeywordText = sqlDataReader["SearchKeywordText"].ToString(),
                            SearchMetaDataModels = new List<SearchMetaDataModel>(),
                        }
                    );
                    while (sqlDataReaderRead && searchKeywordModel.SearchKeywordId == long.Parse(sqlDataReader["SearchKeywordId"].ToString()))
                    {
                        searchKeywordModel.SearchMetaDataModels.Add
                        (
                            searchMetaDataModel = new SearchMetaDataModel
                            {
                                SearchMetaDataId = long.Parse(sqlDataReader["SearchMetaDataId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
                                EntityTypeNameDesc = sqlDataReader["EntityTypeNameDesc"].ToString(),
                                EntityId = long.Parse(sqlDataReader["EntityId"].ToString()),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            }
                        );
                        if (sqlDataReader["EntityTypeNameDesc"].ToString() == "CATEGORY")
                        {
                            searchMetaDataModel.CategoryModel = new CategoryModel
                            {
                                CategoryId = long.Parse(sqlDataReader["EntityId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                CategoryDesc = sqlDataReader["EntityDesc"].ToString(),
                            };
                        }
                        else
                        {
                            searchMetaDataModel.ItemModel = new ItemModel
                            {
                                ItemId = long.Parse(sqlDataReader["EntityId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                ItemDesc = sqlDataReader["EntityDesc"].ToString(),
                            };
                        }
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                return searchKeywordModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<SearchMetaDataModel> GetSearchMetaDatas(string searchKeywordText, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT DISTINCT SearchMetaData.EntityTypeNameDesc, SearchMetaData.EntityId, SearchMetaData.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchKeyword" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "            ON SearchKeyword.SearchKeywordId = SearchMetaData.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "           AND SearchKeyword.SearchKeywordText LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "      ORDER BY" + Environment.NewLine;
                sqlStmt += "               SearchMetaData.EntityTypeNameDesc" + Environment.NewLine;
                sqlStmt += "              ,SearchMetaData.SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<SearchMetaDataModel> searchMetaDataModels = new List<SearchMetaDataModel>();
                SearchMetaDataModel searchMetaDataModel;
                while (sqlDataReader.Read())
                {
                    searchMetaDataModels.Add
                    (
                        searchMetaDataModel = new SearchMetaDataModel
                        {
                            //SearchMetaDataModelId = long.Parse(sqlDataReader["SearchMetaDataId"].ToString()),
                            //SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
                            EntityTypeNameDesc = sqlDataReader["EntityTypeNameDesc"].ToString(),
                            EntityId = long.Parse(sqlDataReader["EntityId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SearchKeywordModel = new SearchKeywordModel
                            {
                                //SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
                                //SearchKeywordText = sqlDataReader["SearchKeywordText"].ToString(),
                            },
                        }
                    );
                }
                return searchMetaDataModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
