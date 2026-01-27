using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        public ApplSessionObjectModel LoginUserProf(long personId, string aspNetRoleName, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                PersonExtn1Model personExtn1Model = ApplicationDataContext.PersonExtn1FromPersonIdGet(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (personExtn1Model == null && aspNetRoleName == "GUESTROLE")
                {
                    personExtn1Model = new PersonExtn1Model
                    {
                        CorpAcctId = 0,
                    };
                }
                ApplSessionObjectModel applSessionObjectModel = new ApplSessionObjectModel
                {
                    CorpAcctModel = RetailSlnCache.CorpAcctModels.First(x => x.CorpAcctId == personExtn1Model.CorpAcctId),
                    TotalBalanceDue = 0,
                };
                applSessionObjectModel.CorpAcctLocationId = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels[0].CorpAcctLocationId.Value;
                return applSessionObjectModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }

        //public ApplSessionObjectModel LoginUserProf(long personId, long corpAcctLocationId, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        PersonExtn1Model personExtn1Model = ApplicationDataContext.PersonExtn1FromPersonIdGet(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplSessionObjectModel applSessionObjectModel = new ApplSessionObjectModel
        //        {
        //            CorpAcctModel = RetailSlnCache.CorpAcctModels.First(x => x.CorpAcctId == personExtn1Model.CorpAcctId),
        //            TotalBalanceDue = 0,
        //        };
        //        if (corpAcctLocationId == -1)
        //        {
        //            applSessionObjectModel.CorpAcctLocationId = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels[0].CorpAcctLocationId.Value;
        //        }
        //        else
        //        {
        //            applSessionObjectModel.CorpAcctLocationId = corpAcctLocationId;
        //        }
        //        return applSessionObjectModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        ApplicationDataContext.CloseSqlConnection();
        //    }
        //}

        public void RegisterUserProfPersonExtn1(long personId, long corpAcctId, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ApplicationDataContext.PersonExtn1Add(personId, corpAcctId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }

        public void RegisterUserProfPersonExtn1(long personId, long corpAcctId, SqlConnection sqlConnection, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.PersonExtn1Add(personId, corpAcctId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }

        public void RegisterUserExtn1(RegisterUserEmailModel registerUserEmailModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                CouponListModel couponListModel = new CouponListModel
                {
                    BegEffDate = "1900-01-01",
                    CouponNum = archLibBL.GenerateRandomKey(int.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "ReferralCouponLength")), lowerCaseFlag: false, specialCharFlag: false),
                    DiscountPercent = float.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "ReferralUserDiscount")),
                    EndEffDate = "9999-12-31",
                };
                registerUserEmailModel.CouponListModel = couponListModel;
                ApplicationDataContext.CouponListAdd(couponListModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                ReferralListModel referralListModel = new ReferralListModel
                {
                    CommissionPercent = float.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "ReferralCommission")),
                    CouponListId = couponListModel.CouponListId.Value,
                    DiscountPercent = couponListModel.DiscountPercent,
                    PersonId = registerUserEmailModel.RegisterUserModel.PersonId,
                };
                registerUserEmailModel.ReferralListModel = referralListModel;
                ApplicationDataContext.ReferralListAdd(referralListModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
    }
}
//System.Web.Mvc.Html.PartialExtensions.Partial(html, "~/Views/Orders/OrdersPartialView.cshtml", orderModel).ToString();
