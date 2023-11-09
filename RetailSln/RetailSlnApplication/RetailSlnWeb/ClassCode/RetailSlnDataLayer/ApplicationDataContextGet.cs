using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static CategoryModel GetCategory(long categoryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.Category WHERE CategoryId = " + categoryId + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CategoryModel categoryModel;
                if (sqlDataReader.Read())
                {
                    categoryModel = new CategoryModel
                    {
                        CategoryId = long.Parse(sqlDataReader["CategoryId"].ToString()),
                        CategoryDesc = sqlDataReader["CategoryDesc"].ToString(),
                        CategoryName = sqlDataReader["CategoryName"].ToString(),
                        CategoryStatusId = (CategoryStatusEnum)int.Parse(sqlDataReader["CategoryStatusId"].ToString()),
                        CategoryTypeId = (CategoryTypeEnum)int.Parse(sqlDataReader["CategoryTypeId"].ToString()),
                        ImageName = sqlDataReader["ImageName"].ToString(),
                        UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                    };
                }
                else
                {
                    categoryModel = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<DeliveryChargeModel> GetDeliveryCharge(float totalVolumeValue, float totalWeightValue, string zipCodeTo, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<DeliveryChargeModel> deliveryChargeModels = new List<DeliveryChargeModel>();
            try
            {
                string sqlStmt = "SELECT TOP 1 * FROM RetailSlnSch.DeliveryCharge WHERE ChargeUnitMeasure = 'WEIGHT' AND " + totalWeightValue + " < ValueTo AND ZipCodeTo = '" + zipCodeTo + "' ORDER BY ValueTo DESC" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    deliveryChargeModels.Add
                    (
                        new DeliveryChargeModel
                        {
                            DeliveryChargeId = long.Parse(sqlDataReader["DeliveryChargeId"].ToString()),
                            ChargeUnitMeasure = sqlDataReader["ChargeUnitMeasure"].ToString(),
                            DeliveryChargeAmount = float.Parse(sqlDataReader["DeliveryChargeAmount"].ToString()),
                            DeliveryChargeAmountAdditional = float.Parse(sqlDataReader["DeliveryChargeAmountAdditional"].ToString()),
                            DeliveryTime = sqlDataReader["DeliveryTime"].ToString(),
                            UnitId = long.Parse(sqlDataReader["UnitId"].ToString()),
                            ValueFrom = long.Parse(sqlDataReader["ValueFrom"].ToString()),
                            ValueTo = long.Parse(sqlDataReader["ValueTo"].ToString()),
                            DestDemogInfoZipIdFrom = long.Parse(sqlDataReader["DestDemogInfoZipIdFrom"].ToString()),
                            DestDemogInfoZipIdTo = long.Parse(sqlDataReader["DestDemogInfoZipIdTo"].ToString()),
                            FuelChargePersent = float.Parse(sqlDataReader["FuelChargePersent"].ToString()),
                            GSTPersent = float.Parse(sqlDataReader["GSTPersent"].ToString()),
                        }
                    );
                }
                sqlDataReader.Close();
                sqlStmt = "SELECT TOP 1 * FROM RetailSlnSch.DeliveryCharge WHERE ChargeUnitMeasure = 'VOLUME' AND " + totalVolumeValue + " < ValueTo AND ZipCodeTo = '" + zipCodeTo + "' ORDER BY ValueTo DESC" + Environment.NewLine;
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    deliveryChargeModels.Add
                    (
                        new DeliveryChargeModel
                        {
                            DeliveryChargeId = long.Parse(sqlDataReader["DeliveryChargeId"].ToString()),
                            ChargeUnitMeasure = sqlDataReader["ChargeUnitMeasure"].ToString(),
                            DeliveryChargeAmount = float.Parse(sqlDataReader["DeliveryChargeAmount"].ToString()),
                            DeliveryChargeAmountAdditional = float.Parse(sqlDataReader["DeliveryChargeAmountAdditional"].ToString()),
                            UnitId = long.Parse(sqlDataReader["UnitId"].ToString()),
                            ValueFrom = long.Parse(sqlDataReader["ValueFrom"].ToString()),
                            ValueTo = long.Parse(sqlDataReader["ValueTo"].ToString()),
                            DestDemogInfoZipIdFrom = long.Parse(sqlDataReader["DestDemogInfoZipIdFrom"].ToString()),
                            DestDemogInfoZipIdTo = long.Parse(sqlDataReader["DestDemogInfoZipIdTo"].ToString()),
                            FuelChargePersent = float.Parse(sqlDataReader["FuelChargePersent"].ToString()),
                            GSTPersent = float.Parse(sqlDataReader["GSTPersent"].ToString()),
                        }
                    );
                }
                sqlDataReader.Close();
                return deliveryChargeModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static ItemModel GetItem(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemModel itemModel;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.Item WHERE ItemId = " + itemId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    itemModel = new ItemModel
                    {
                        ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                        ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        ItemDesc = sqlDataReader["ItemDesc"].ToString(),
                        ImageName = sqlDataReader["ImageName"].ToString(),
                        ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                        ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                        ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                        UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                    };
                }
                else
                {
                    itemModel = null;
                }
                sqlDataReader.Close();
                return itemModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static GiftCertModel GetGiftCert(string giftCertNumber, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            GiftCertModel giftCertModel;
            SqlCommand sqlCommand = BuildSqlCommandGiftCertSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            sqlCommand.Parameters["@GiftCertNumber"].Value = giftCertNumber;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if(sqlDataReader.Read())
            {
                giftCertModel = AssignGiftCert(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                giftCertModel = null;
            }
            sqlDataReader.Close();
            return giftCertModel;
        }
        public static ItemSpecModel GetItemSpec(long itemSpecId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemSpec WHERE ItemSpecId = " + itemSpecId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ItemSpecModel itemSpecModel;
                if (sqlDataReader.Read())
                {
                    itemSpecModel = AssignItemSpecSelect(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    itemSpecModel = null;
                }
                sqlDataReader.Close();
                return itemSpecModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static float GetItemSpecMaxSeqNum(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT MAX(SeqNum) FROM RetailSlnSch.ItemSpec WHERE ItemId = " + itemId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                float seqNum;
                if (sqlDataReader.Read())
                {
                    seqNum = float.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    seqNum = 1;
                }
                sqlDataReader.Close();
                return seqNum;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static int GetOrderHeaderCount(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM RetailSlnSch.OrderHeader WHERE PersonId = " + personId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int orderHeaderCount;
                if (sqlDataReader.Read())
                {
                    orderHeaderCount = int.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    orderHeaderCount = 0;
                }
                sqlDataReader.Close();
                return orderHeaderCount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
