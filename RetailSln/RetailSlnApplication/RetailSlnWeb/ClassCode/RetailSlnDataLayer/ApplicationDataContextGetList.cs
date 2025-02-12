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
        public static List<CategoryHierModel> GetCategoryHiers(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.CategoryHier ORDER BY AspNetRoleName, ParentCategoryId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryHierModel> categoryHierModels = new List<CategoryHierModel>();
                while (sqlDataReader.Read())
                {
                    categoryHierModels.Add
                    (
                        new CategoryHierModel
                        {
                            AspNetRoleName = sqlDataReader["AspNetRoleName"].ToString(),
                            CategoryHierId = long.Parse(sqlDataReader["CategoryHierId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CategoryId = long.Parse(sqlDataReader["CategoryId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryHierModels;
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
                sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemHier ORDER BY CategoryItemMasterHierId, SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<CategoryItemHierModel> categoryItemModels = new List<CategoryItemHierModel>();
                while (sqlDataReader.Read())
                {
                    categoryItemModels.Add
                    (
                        new CategoryItemHierModel
                        {
                            CategoryItemHierId = long.Parse(sqlDataReader["CategoryItemHierId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CategoryItemMasterHierId = long.Parse(sqlDataReader["CategoryItemMasterHierId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        }
                     );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryItemModels;
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
                sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemMasterHier ORDER BY AspNetRoleName, ParentCategoryId, SeqNum" + Environment.NewLine;
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
                            AspNetRoleName = sqlDataReader["AspNetRoleName"].ToString(),
                            ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
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
                            //CategoryId = string.IsNullOrWhiteSpace(sqlDataReader["CategoryId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
                            //CategoryOrItem = sqlDataReader["CategoryOrItem"].ToString(),
                            ItemMasterId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
                            //ProcessType = sqlDataReader["ProcessType"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            //CategoryModel = new CategoryModel
                            //{
                            //    CategoryId = long.Parse(sqlDataReader["CategoryId"].ToString()),
                            //    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            //    AssignSubCategory = bool.Parse(sqlDataReader["AssignSubCategory"].ToString()),
                            //    AssignItem = bool.Parse(sqlDataReader["AssignItem"].ToString()),
                            //    CategoryDesc = sqlDataReader["CategoryDesc"].ToString(),
                            //    CategoryName = sqlDataReader["CategoryName"].ToString(),
                            //    CategoryNameDesc = sqlDataReader["CategoryNameDesc"].ToString(),
                            //    CategoryStatusId = (CategoryStatusEnum)int.Parse(sqlDataReader["CategoryStatusId"].ToString()),
                            //    CategoryTypeId = (CategoryTypeEnum)int.Parse(sqlDataReader["CategoryTypeId"].ToString()),
                            //    DefaultCategory = bool.Parse(sqlDataReader["DefaultCategory"].ToString()),
                            //    ImageExtension = sqlDataReader["ImageExtension"].ToString(),
                            //    ImageName = sqlDataReader["ImageName"].ToString(),
                            //    MaxPerPage = short.Parse(sqlDataReader["MaxPerPage"].ToString()),
                            //    UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                            //    ViewName = sqlDataReader["ViewName"].ToString(),
                            //},
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
        public static CorpAcctModel GetCorpAcct(long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.CorpAcct" + Environment.NewLine;
                sqlStmt += "         WHERE CorpAcct.CorpAcctId = " + corpAcctId + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CorpAcctModel corpAcctModel = null;
                if (sqlDataReader.Read())
                {
                    corpAcctModel = new CorpAcctModel
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
                        DiscountDtlModels = new List<DiscountDtlModel>(),
                        StatusId = (YesNoEnum)long.Parse(sqlDataReader["StatusId"].ToString()),
                        CorpAcctLocationModels = new List<CorpAcctLocationModel>(),
                    };
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return corpAcctModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static CorpAcctModel GetCorpAcctCorpAcctLocation(long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                //sqlStmt += "SELECT * FROM RetailSlnSch.CorpAcct INNER JOIN RetailSlnSch.CorpAcctLocation ON CorpAcct.CorpAcctId = CorpAcctLocation.CorpAcctId WHERE CorpAcctId = " + corpAcctId + Environment.NewLine;
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.CorpAcct" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CorpAcctLocation" + Environment.NewLine;
                sqlStmt += "            ON CorpAcct.CorpAcctId = CorpAcctLocation.CorpAcctId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoAddress" + Environment.NewLine;
                sqlStmt += "            ON CorpAcctLocation.DemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "         WHERE CorpAcct.CorpAcctId = " + corpAcctId + Environment.NewLine;
                sqlStmt += "      ORDER BY CorpAcctLocation.SeqNum" + Environment.NewLine;
                //sqlStmt += "    " + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CorpAcctModel corpAcctModel = null;
                bool sqlDataReaderRead = sqlDataReader.Read();
                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Close();
                    corpAcctModel = GetCorpAcct(corpAcctId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (corpAcctModel == null)
                    {
                        corpAcctModel = new CorpAcctModel
                        {
                            CorpAcctLocationModels = new List<CorpAcctLocationModel>
                            {
                                new CorpAcctLocationModel
                                {
                                    DemogInfoAddressModel = new DemogInfoAddressModel
                                    {

                                    },
                                },
                            },
                        };
                    }
                }
                else
                {
                    while (sqlDataReaderRead)
                    {
                        corpAcctModel = new CorpAcctModel
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
                            DiscountDtlModels = new List<DiscountDtlModel>(),
                            StatusId = (YesNoEnum)long.Parse(sqlDataReader["StatusId"].ToString()),
                            CorpAcctLocationModels = new List<CorpAcctLocationModel>()
                        };
                        while (sqlDataReaderRead && long.Parse(sqlDataReader["CorpAcctId"].ToString()) == corpAcctModel.CorpAcctId)
                        {
                            corpAcctModel.CorpAcctLocationModels.Add
                            (
                                new CorpAcctLocationModel
                                {
                                    CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                    AlternateTelephoneCountryId = null,
                                    AlternateTelephoneNumber = sqlDataReader["AlternateTelephoneNumber"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["AlternateTelephoneNumber"].ToString()),
                                    CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                                    DemogInfoAddressId = long.Parse(sqlDataReader["DemogInfoAddressId"].ToString()),
                                    DemogInfoAddressModel = new DemogInfoAddressModel
                                    {
                                        DemogInfoAddressId = long.Parse(sqlDataReader["DemogInfoAddressId"].ToString()),
                                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                        AddressLine1 = sqlDataReader["AddressLine1"].ToString(),
                                        AddressLine2 = sqlDataReader["AddressLine2"].ToString(),
                                        AddressLine3 = sqlDataReader["AddressLine3"].ToString(),
                                        AddressLine4 = sqlDataReader["AddressLine4"].ToString(),
                                        AddressName = sqlDataReader["AddressName"].ToString(),
                                        AddressTypeId = (AddressTypeEnum)long.Parse(sqlDataReader["AddressTypeId"].ToString()),
                                        BuildingTypeId = (BuildingTypeEnum)long.Parse(sqlDataReader["BuildingTypeId"].ToString()),
                                        CityName = sqlDataReader["CityName"].ToString(),
                                        Comments = sqlDataReader["Comments"].ToString(),
                                        CountryAbbrev = sqlDataReader["CountryAbbrev"].ToString(),
                                        CountryDesc = sqlDataReader["CountryDesc"].ToString(),
                                        CountyName = sqlDataReader["CountyName"].ToString(),
                                        DemogInfoCityId = sqlDataReader["DemogInfoCityId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCityId"].ToString()),
                                        DemogInfoCountryId = sqlDataReader["DemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                                        DemogInfoCountyId = sqlDataReader["DemogInfoCountyId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                                        DemogInfoSubDivisionId = sqlDataReader["DemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoSubDivisionId"].ToString()),
                                        DemogInfoZipId = sqlDataReader["DemogInfoZipId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipId"].ToString()),
                                        DemogInfoZipPlusId = sqlDataReader["DemogInfoZipPlusId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipPlusId"].ToString()),
                                        HouseNumber = sqlDataReader["HouseNumber"].ToString(),
                                        StateAbbrev = sqlDataReader["StateAbbrev"].ToString(),
                                        ZipCode = sqlDataReader["ZipCode"].ToString(),
                                        ZipPlus4 = sqlDataReader["ZipPlus4"].ToString(),
                                    },
                                    LocationName = sqlDataReader["LocationName"].ToString(),
                                    PrimaryTelephoneNumber = sqlDataReader["PrimaryTelephoneNumber"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["PrimaryTelephoneNumber"].ToString()),
                                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                                    StatusId = (YesNoEnum)long.Parse(sqlDataReader["StatusId"].ToString()),
                                }
                            );
                            sqlDataReaderRead = sqlDataReader.Read();
                        }
                    }
                    sqlDataReader.Close();
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return corpAcctModel;
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
        public static List<CorpAcctModel> GetCorpAccts(int offsetRows, int rowCount, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        public static List<CorpAcctLocationModel> GetCorpAcctLocations(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        //public static List<CategoryItemHierModel> GetCategoryItemHiersNew(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string sqlStmt = "";
        //        sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemHierNew ORDER BY CategoryOrItem, AspNetRoleName, ParentCategoryId, SeqNum" + Environment.NewLine;
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        List<CategoryItemHierModel> categoryItemModels = new List<CategoryItemHierModel>();
        //        while (sqlDataReader.Read())
        //        {
        //            categoryItemModels.Add
        //            (
        //                new CategoryItemHierModel
        //                {
        //                    CategoryItemHierId = long.Parse(sqlDataReader["CategoryItemHierId"].ToString()),
        //                    AspNetRoleName = sqlDataReader["AspNetRoleName"].ToString(),
        //                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                    CategoryId = string.IsNullOrWhiteSpace(sqlDataReader["CategoryId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
        //                    CategoryOrItem = sqlDataReader["CategoryOrItem"].ToString(),
        //                    ItemId = string.IsNullOrWhiteSpace(sqlDataReader["ItemId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["ItemId"].ToString()),
        //                    ItemMasterId = string.IsNullOrWhiteSpace(sqlDataReader["ItemMasterId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["ItemMasterId"].ToString()),
        //                    ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
        //                    ProcessType = sqlDataReader["ProcessType"].ToString(),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                }
        //             );
        //        }
        //        sqlDataReader.Close();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return categoryItemModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static List<CategoryItemMasterHierModel> GetCategoryItemMasterHiers(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string sqlStmt = "";
        //        sqlStmt += "SELECT * FROM RetailSlnSch.CategoryItemMasterHier ORDER BY ParentCategoryId, SeqNum" + Environment.NewLine;
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        List<CategoryItemMasterHierModel> categoryItemHierModels = new List<CategoryItemMasterHierModel>();
        //        while (sqlDataReader.Read())
        //        {
        //            categoryItemHierModels.Add
        //            (
        //                new CategoryItemMasterHierModel
        //                {
        //                    CategoryItemMasterHierId = long.Parse(sqlDataReader["CategoryItemMasterHierId"].ToString()),
        //                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                    CategoryId = string.IsNullOrWhiteSpace(sqlDataReader["CategoryId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["CategoryId"].ToString()),
        //                    CategoryOrItem = sqlDataReader["CategoryOrItem"].ToString(),
        //                    ItemMasterId = string.IsNullOrWhiteSpace(sqlDataReader["ItemMasterId"].ToString()) ? (long?)null : long.Parse(sqlDataReader["ItemMasterId"].ToString()),
        //                    ParentCategoryId = long.Parse(sqlDataReader["ParentCategoryId"].ToString()),
        //                    ProcessType = sqlDataReader["ProcessType"].ToString(),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                }
        //             );
        //        }
        //        sqlDataReader.Close();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return categoryItemHierModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        public static List<ItemBundleModel> GetItemBundles(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                            DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
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
                            ItemBundleId = long.Parse(sqlDataReader["ItemBundleId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
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
        public static List<ItemDiscountModel> GetItemDiscounts(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemDiscountModel> itemDiscountModels = new List<ItemDiscountModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemDiscount ORDER BY CorpAcctId, ItemId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemDiscountModels.Add
                    (
                        new ItemDiscountModel
                        {
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                            ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                            DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                            BegEffDate = DateTime.Parse(sqlDataReader["BegEffDate"].ToString()),
                            EndEffDate = DateTime.Parse(sqlDataReader["EndEffDate"].ToString()),
                        }
                    );

                }
                sqlDataReader.Close();
                return itemDiscountModels;
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
        public static List<ItemMasterModel> GetItemMasters(int offsetRows, int rowCount, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
            try
            {
                string sqlStmt = "";
                sqlStmt += "WITH ItemMasterTemp AS" + Environment.NewLine;
                sqlStmt += "(" + Environment.NewLine;
                sqlStmt += $"    SELECT * FROM RetailSlnSch.ItemMaster ORDER BY ItemMasterId OFFSET {offsetRows} ROWS FETCH NEXT {rowCount} ROWS ONLY" + Environment.NewLine;
                sqlStmt += ")" + Environment.NewLine;
                sqlStmt += "SELECT * FROM ItemMasterTemp INNER JOIN RetailSlnSch.Item ON ItemMasterTemp.ItemMasterId = Item.ItemMasterId" + Environment.NewLine;
                sqlStmt += "ORDER BY ItemMasterTemp.ItemMasterId, Item.ItemId" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                ItemMasterModel itemMasterModel;
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
                sqlDataReader.Close();

                return itemMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static ItemMasterModel GetItemMaster(int itemMasterId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += $"SELECT * FROM RetailSlnSch.ItemMaster INNER JOIN RetailSlnSch.ItemMasterInfo ON ItemMaster.ItemMasterId = ItemMasterInfo.ItemMasterId WHERE ItemMaster.ItemMasterId = {itemMasterId} ORDER BY ItemMasterInfo.SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                ItemMasterModel itemMasterModel = null;
                while (sqlDataReaderRead)
                {
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
                        ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                        ProductItemId = sqlDataReader["ProductItemId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ProductItemId"].ToString()),
                        UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        ItemMasterInfoModels = new List<ItemMasterInfoModel>(),
                        ItemModels = new List<ItemModel>(),
                    };
                    while (sqlDataReaderRead && itemMasterModel.ItemMasterId == long.Parse(sqlDataReader["ItemMasterId"].ToString()))
                    {
                        itemMasterModel.ItemMasterInfoModels.Add
                        (
                            new ItemMasterInfoModel
                            {
                                ItemMasterInfoId = long.Parse(sqlDataReader["ItemMasterInfoId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                                ItemMasterInfoLabelText = sqlDataReader["ItemMasterInfoLabelText"].ToString(),
                                ItemMasterInfoText = sqlDataReader["ItemMasterInfoText"].ToString(),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                sqlStmt = $"SELECT * FROM RetailSlnSch.Item INNER JOIN RetailSlnSch.ItemInfo ON Item.ItemId = ItemInfo.ItemId WHERE Item.ItemMasterId = {itemMasterId} ORDER BY Item.ItemId, ItemInfo.SeqNum" + Environment.NewLine;
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReaderRead = sqlDataReader.Read();
                ItemModel itemModel;
                while (sqlDataReaderRead)
                {
                    itemModel = new ItemModel
                    {
                        ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        ItemInfoModels = new List<ItemInfoModel>(),
                    };
                    while (sqlDataReaderRead && itemModel.ItemId == long.Parse(sqlDataReader["ItemId"].ToString()))
                    {
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                return itemMasterModel;
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
        //public static List<ItemBundleItemModel> GetItemBundleItems(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        List<ItemBundleItemModel> itemBundleItemModels = new List<ItemBundleItemModel>();
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemBundleItem WHERE ItemBundleItem.ItemId = " + itemId + " ORDER BY ItemBundleItem.SeqNum", sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        while (sqlDataReader.Read())
        //        {
        //            itemBundleItemModels.Add
        //            (
        //                new ItemBundleItemModel
        //                {
        //                    BundleItemId = long.Parse(sqlDataReader["BundleItemId"].ToString()),
        //                    ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                }
        //            );
        //        }
        //        sqlDataReader.Close();
        //        return itemBundleItemModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
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
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT DISTINCT SearchMetaData.EntityTypeNameDesc, SearchMetaData.EntityId, SearchMetaData.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchKeyword" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "            ON SearchKeyword.SearchKeywordId = SearchMetaData.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "           AND SearchKeyword.SearchKeywordText LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT 'ITEMMASTER' AS EntityTypeNameDesc, ItemMaster.ItemMasterId, ItemMaster.ItemMasterId AS SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "         WHERE ItemMasterDesc LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT 'CATEGORY' AS EntityTypeNameDesc, Category.CategoryId, Category.CategoryId AS SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.Category" + Environment.NewLine;
                sqlStmt += "         WHERE CategoryDesc LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "      ORDER BY" + Environment.NewLine;
                sqlStmt += "               EntityTypeNameDesc" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                #endregion
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
        public static SearchForUserDataModel GetSearchForUserData(string searchText, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT Person.PersonId, Person.FirstName, Person.LastName, CorpAcct.CorpAcctName, CorpAcctLocation.CorpAcctLocationId, CorpAcctLocation.LocationName, DemogInfoAddress.AddressName, DemogInfoAddress.AddressLine1, DemogInfoAddress.AddressLine2, DemogInfoAddress.AddressLine3, DemogInfoAddress.AddressLine4, DemogInfoAddress.CityName, DemogInfoAddress.StateAbbrev, DemogInfoAddress.ZipCode, AspNetUser.Email" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.CorpAcct" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CorpAcctLocation ON CorpAcct.CorpAcctId = CorpAcctLocation.CorpAcctId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoAddress ON CorpAcctLocation.DemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.PersonExtn1 ON CorpAcct.CorpAcctId = PersonExtn1.CorpAcctId AND CorpAcctLocation.CorpAcctLocationId = PersonExtn1.CorpAcctLocationId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person ON PersonExtn1.PersonId = Person.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += $"        WHERE CorpAcct.CorpAcctName LIKE '%{searchText}%' OR Person.FirstName LIKE '%{searchText}%' OR Person.LastName LIKE '%{searchText}%' OR AspNetUser.Email LIKE '%{searchText}%'" + Environment.NewLine;
                sqlStmt += "      ORDER BY CorpAcctLocation.LocationName, AspNetUser.Email, Person.FirstName, Person.LastName" + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                SearchForUserDataModel searchForEmailAddressDataModel = new SearchForUserDataModel
                {
                    CorpAcctLocationModels = new List<CorpAcctLocationModel>(),
                    PersonModels = new List<PersonModel>(),
                    SearchText = searchText,
                };
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                bool populatePerson = true;
                CorpAcctLocationModel corpAcctLocationModel;
                while (sqlDataReaderRead)
                {
                    searchForEmailAddressDataModel.CorpAcctLocationModels.Add
                    (
                        corpAcctLocationModel = new CorpAcctLocationModel
                        {
                            CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                            LocationName = sqlDataReader["LocationName"].ToString(),
                            CorpAcctModel = new CorpAcctModel
                            {
                                CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                            },
                            DemogInfoAddressModel = new DemogInfoAddressModel
                            {
                                AddressName = sqlDataReader["AddressName"].ToString(),
                                AddressLine1 = sqlDataReader["AddressLine1"].ToString(),
                                AddressLine2 = sqlDataReader["AddressLine2"].ToString(),
                                AddressLine3 = sqlDataReader["AddressLine3"].ToString(),
                                AddressLine4 = sqlDataReader["AddressLine4"].ToString(),
                                CityName = sqlDataReader["CityName"].ToString(),
                                StateAbbrev = sqlDataReader["StateAbbrev"].ToString(),
                                ZipCode = sqlDataReader["ZipCode"].ToString(),
                            },
                        }
                    );
                    if (populatePerson)
                    {
                        while (sqlDataReaderRead && corpAcctLocationModel.LocationName == sqlDataReader["LocationName"].ToString())
                        {
                            searchForEmailAddressDataModel.PersonModels.Add
                            (
                                new PersonModel
                                {
                                    AspNetUserModel = new AspNetUserModel
                                    {
                                        Email = sqlDataReader["Email"].ToString(),
                                    },
                                    FirstName = sqlDataReader["FirstName"].ToString(),
                                    LastName = sqlDataReader["LastName"].ToString(),
                                    PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                                }
                            );
                            sqlDataReaderRead = sqlDataReader.Read();
                        }
                    }
                    else
                    {
                        while (sqlDataReaderRead && corpAcctLocationModel.LocationName == sqlDataReader["LocationName"].ToString())
                        {
                            sqlDataReaderRead = sqlDataReader.Read();
                        }
                    }
                    populatePerson = false;
                }
                return searchForEmailAddressDataModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<SearchForUserDataModel> GetSearchForUser(string searchText, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT Person.PersonId, Person.FirstName, Person.LastName, CorpAcct.CorpAcctName, CorpAcctLocation.CorpAcctLocationId, CorpAcctLocation.LocationName, DemogInfoAddress.AddressName, DemogInfoAddress.AddressLine1, DemogInfoAddress.AddressLine2, DemogInfoAddress.AddressLine3, DemogInfoAddress.AddressLine4, DemogInfoAddress.CityName, DemogInfoAddress.StateAbbrev, DemogInfoAddress.ZipCode, AspNetUser.Email" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.CorpAcct" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CorpAcctLocation ON CorpAcct.CorpAcctId = CorpAcctLocation.CorpAcctId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoAddress ON CorpAcctLocation.DemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.PersonExtn1 ON CorpAcct.CorpAcctId = PersonExtn1.CorpAcctId AND CorpAcctLocation.CorpAcctLocationId = PersonExtn1.CorpAcctLocationId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person ON PersonExtn1.PersonId = Person.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += $"        WHERE CorpAcct.CorpAcctName LIKE '%{searchText}%' OR Person.FirstName LIKE '%{searchText}%' OR Person.LastName LIKE '%{searchText}%' OR AspNetUser.Email LIKE '%{searchText}%'" + Environment.NewLine;
                sqlStmt += "      ORDER BY CorpAcctLocation.LocationName, AspNetUser.Email, Person.FirstName, Person.LastName" + Environment.NewLine;
                //sqlStmt += "UNION" + Environment.NewLine;
                //sqlStmt += "        SELECT Person.PersonId, Person.FirstName, Person.LastName, '' AS CorpAcctName, '' AS LocationName, AspNetUser.Email" + Environment.NewLine;
                //sqlStmt += "          FROM ArchLib.Person" + Environment.NewLine;
                //sqlStmt += "    INNER JOIN ArchLib.AspNetUser ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                //sqlStmt += $"        WHERE Person.FirstName LIKE '%{searchText}%' OR Person.LastName LIKE '%{searchText}%' OR AspNetUser.Email LIKE '%{searchText}%'" + Environment.NewLine;
                //sqlStmt += "      ORDER BY AspNetUser.Email, Person.FirstName, Person.LastName" + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<SearchForUserDataModel> searchForEmailAddressDataModels = new List<SearchForUserDataModel>();
                while (sqlDataReader.Read())
                {
                    searchForEmailAddressDataModels.Add
                    (
                        new SearchForUserDataModel
                        {
                            CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                            CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                            EmailAddress = sqlDataReader["Email"].ToString(),
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                            LocationName = sqlDataReader["LocationName"].ToString(),
                            PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                            CorpAcctLocationModel = new CorpAcctLocationModel
                            {
                                DemogInfoAddressModel = new DemogInfoAddressModel
                                {
                                    AddressName = sqlDataReader["AddressName"].ToString(),
                                    AddressLine1 = sqlDataReader["AddressLine1"].ToString(),
                                    AddressLine2 = sqlDataReader["AddressLine2"].ToString(),
                                    AddressLine3 = sqlDataReader["AddressLine3"].ToString(),
                                    AddressLine4 = sqlDataReader["AddressLine4"].ToString(),
                                    CityName = sqlDataReader["CityName"].ToString(),
                                    StateAbbrev = sqlDataReader["StateAbbrev"].ToString(),
                                    ZipCode = sqlDataReader["ZipCode"].ToString(),
                                },
                            },
                        }
                    );
                }
                return searchForEmailAddressDataModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<PickupLocationModel> GetPickupLocations(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
