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
                            CategoryDesc = sqlDataReader["CategoryDesc"].ToString(),
                            CategoryName = sqlDataReader["CategoryName"].ToString(),
                            CategoryStatusId = (CategoryStatusEnum)int.Parse(sqlDataReader["CategoryStatusId"].ToString()),
                            CategoryTypeId = (CategoryTypeEnum)int.Parse(sqlDataReader["CategoryTypeId"].ToString()),
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
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
                            CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                            CreditDays = short.Parse(sqlDataReader["CreditDays"].ToString()),
                            CreditLimit = float.Parse(sqlDataReader["CreditLimit"].ToString()),
                            MinOrderAmount = float.Parse(sqlDataReader["MinOrderAmount"].ToString()),
                            TaxIdentNum = sqlDataReader["TaxIdentNum"].ToString(),
                            DemogInfoAddressModels = new List<DemogInfoAddressModel>(),
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
        public static List<DeliveryChargeModel> GetDeliveryCharges(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.DeliveryCharge ORDER BY DeliveryChargeId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<DeliveryChargeModel> deliveryChargeModels = new List<DeliveryChargeModel>();
                while (sqlDataReader.Read())
                {
                    deliveryChargeModels.Add
                    (
                        new DeliveryChargeModel
                        {
                            DeliveryChargeId = long.Parse(sqlDataReader["DeliveryChargeId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            DeliveryChargeAmount = float.Parse(sqlDataReader["DeliveryChargeAmount"].ToString()),
                            DeliveryChargeAmountAdditional = float.Parse(sqlDataReader["DeliveryChargeAmountAdditional"].ToString()),
                            DeliveryListId = long.Parse(sqlDataReader["DeliveryListId"].ToString()),
                            DeliveryModeId = (DeliveryModeEnum)long.Parse(sqlDataReader["DeliveryModeId"].ToString()),
                            DeliveryTime = sqlDataReader["DeliveryTime"].ToString(),
                            DeliveryTypeId = (DeliveryTypeEnum)long.Parse(sqlDataReader["DeliveryTypeId"].ToString()),
                            DestDemogInfoCityId = sqlDataReader["DestDemogInfoCityId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoCityId"].ToString()),
                            DestDemogInfoCountryId = sqlDataReader["DestDemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoCountryId"].ToString()),
                            DestDemogInfoCountyId = sqlDataReader["DestDemogInfoCountyId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoCountyId"].ToString()),
                            DestDemogInfoSubDivisionId = sqlDataReader["DestDemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoSubDivisionId"].ToString()),
                            DestDemogInfoZipIdFrom = sqlDataReader["DestDemogInfoZipIdFrom"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoZipIdFrom"].ToString()),
                            DestDemogInfoZipIdTo = sqlDataReader["DestDemogInfoZipIdTo"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoZipIdTo"].ToString()),
                            FuelChargePercent = float.Parse(sqlDataReader["FuelChargePercent"].ToString()),
                            SrceDemogInfoCountryId = sqlDataReader["SrceDemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SrceDemogInfoCountryId"].ToString()),
                            UnitId = long.Parse(sqlDataReader["UnitId"].ToString()),
                            UnitMeasure = sqlDataReader["UnitMeasure"].ToString(),
                            ValueFrom = long.Parse(sqlDataReader["ValueFrom"].ToString()),
                            ValueTo = long.Parse(sqlDataReader["ValueTo"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return deliveryChargeModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<DeliveryListModel> GetDeliveryLists(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.DeliveryList ORDER BY DeliveryListId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<DeliveryListModel> deliveryListModels = new List<DeliveryListModel>();
                while (sqlDataReader.Read())
                {
                    deliveryListModels.Add
                    (
                        new DeliveryListModel
                        {
                            DeliveryListId = long.Parse(sqlDataReader["DeliveryListId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            DeliveryListName = sqlDataReader["DeliveryListName"].ToString(),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return deliveryListModels;
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
        public static List<CategoryItemHierModel> GetCategoryItemHiers(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemHier ORDER BY ParentCategoryId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryItemHierModel> categoryItemHierModels = new List<CategoryItemHierModel>();
                while (sqlDataReader.Read())
                {
                    categoryItemHierModels.Add
                    (
                        new CategoryItemHierModel
                        {
                            CategoryItemHierId = long.Parse(sqlDataReader["CategoryItemHierId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            CategoryId = string.IsNullOrWhiteSpace(sqlDataReader["CategoryId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
                            ItemId = string.IsNullOrWhiteSpace(sqlDataReader["ItemId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["ItemId"].ToString()),
                            ProcessType = sqlDataReader["ProcessType"].ToString(),
                            CategoryOrItem = sqlDataReader["CategoryOrItem"].ToString(),
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
                            BundledItemId = long.Parse(sqlDataReader["BundledItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
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
        public static List<ItemAttribMasterModel> GetItemAttribMasters(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemAttribMasterModel> itemAttribMasterModels = new List<ItemAttribMasterModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemAttribMaster ORDER BY SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemAttribMasterModels.Add
                    (
                        new ItemAttribMasterModel
                        {
                            ItemAttribMasterId = long.Parse(sqlDataReader["ItemAttribMasterId"].ToString()),
                            AttribDesc = sqlDataReader["AttribDesc"].ToString(),
                            AttribName = sqlDataReader["AttribName"].ToString(),
                            AttribUnitType = sqlDataReader["AttribUnitType"].ToString(),
                            AttribUnitCategory = sqlDataReader["AttribUnitCategory"].ToString(),
                            AttribValueType = sqlDataReader["AttribValueType"].ToString(),
                            IsMandatory = bool.Parse(sqlDataReader["IsMandatory"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemAttribMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemAttribModel> GetItemAttribs(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemAttribModel> itemAttribModels = new List<ItemAttribModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemAttrib ORDER BY ItemId, SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemAttribModels.Add
                    (
                        new ItemAttribModel
                        {
                            ItemAttribId = long.Parse(sqlDataReader["ItemAttribId"].ToString()),
                            ItemAttribMasterId = long.Parse(sqlDataReader["ItemAttribMasterId"].ToString()),
                            ItemAttribUnitValue = sqlDataReader["ItemAttribUnitValue"].ToString(),
                            ItemAttribValue = sqlDataReader["ItemAttribValue"].ToString(),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemAttribModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ItemModel> GetItemsAssigned(long categoryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemModel> itemModels = new List<ItemModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.CategoryItem INNER JOIN RetailSlnSch.Item ON CategoryItem.ItemId = Item.ItemId WHERE CategoryItem.CategoryId = " + categoryId + " ORDER BY CategoryItem.SeqNum", sqlConnection);
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
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                            ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                            ItemStarCount = int.Parse(sqlDataReader["ItemStarCount"].ToString()),
                            ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
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
        public static List<ItemAttribModel> GetItemAttribs(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<ItemAttribModel> itemAttribModels = new List<ItemAttribModel>();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemAttrib INNER JOIN RetailSlnSch.ItemAttribMaster ON ItemAttrib.ItemAttribMasterId = ItemAttribMaster.ItemAttribMasterId WHERE ItemAttrib.ItemId = " + itemId + " ORDER BY ItemAttrib.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemAttribModels.Add
                    (
                        new ItemAttribModel
                        {
                            ItemAttribId = long.Parse(sqlDataReader["ItemAttribId"].ToString()),
                            ItemAttribMasterId = long.Parse(sqlDataReader["ItemAttribMasterId"].ToString()),
                            ItemAttribUnitValue = sqlDataReader["ItemAttribUnitValue"].ToString(),
                            ItemAttribValue = sqlDataReader["ItemAttribValue"].ToString(),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            ItemAttribMasterModel = new ItemAttribMasterModel
                            {
                                ItemAttribMasterId = long.Parse(sqlDataReader["ItemAttribMasterId"].ToString()),
                                AttribDesc = sqlDataReader["AttribDesc"].ToString(),
                                AttribName = sqlDataReader["AttribName"].ToString(),
                                AttribUnitCategory = sqlDataReader["AttribUnitCategory"].ToString(),
                                AttribUnitType = sqlDataReader["AttribUnitType"].ToString(),
                                AttribValueType = sqlDataReader["AttribValueType"].ToString(),
                                IsMandatory = bool.Parse(sqlDataReader["IsMandatory"].ToString()),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            },
                        }
                    );
                }
                sqlDataReader.Close();
                return itemAttribModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
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
                            BundledItemId = long.Parse(sqlDataReader["BundledItemId"].ToString()),
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
        public static List<ItemSpecModel> GetItemSpecs(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemSpecModel> itemSpecModels = new List<ItemSpecModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemSpec WHERE ItemSpec.ItemId = " + itemId + " ORDER BY ItemSpec.SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemSpecModels.Add
                    (
                        new ItemSpecModel
                        {
                            ItemSpecId = long.Parse(sqlDataReader["ItemSpecId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ItemSpecLabelText = sqlDataReader["ItemSpecLabelText"].ToString(),
                            ItemSpecText = sqlDataReader["ItemSpecText"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
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
                            ImageName = sqlDataReader["ImageName"].ToString(),
                            ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                            ItemStarCount = int.Parse(sqlDataReader["ItemStarCount"].ToString()),
                            ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                            ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
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
    }
}
