using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryPDFLibrary;
using ArchitectureLibraryShippingLibrary;
using ArchitectureLibraryUtility;
using Newtonsoft.Json.Linq;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        #region GET / POST
        #region
        //// GET: AddToCart
        //public string AddToCart(ref PaymentInfoModel paymentInfoModel, string itemIdParm, string orderQtyParm, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        long.TryParse(itemIdParm, out long itemId);
        //        //ItemModel itemModel = RetailSlnCache.ItemModels.FirstOrDefault(x => x.ItemId == itemId);
        //        string errorMessage = AddToCart(ref paymentInfoModel, itemIdParm, orderQtyParm, createOrderWIP, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return errorMessage;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        ApplicationDataContext.CloseSqlConnection();
        //    }
        //}
        #endregion
        // GET: AddToCart
        public string AddToCart(ref PaymentInfoModel paymentInfoModel, AddToCartModel addToCartModel, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                bool.TryParse(addToCartModel.DoNotBreakBundleParm, out bool doNotBreakBundle);
                addToCartModel.DoNotBreakBundle = doNotBreakBundle;
                ApplicationDataContext.OpenSqlConnection();
                string errorMessage = AddToCart(ref paymentInfoModel, addToCartModel, createOrderWIP, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return errorMessage;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET: Checkout
        public CheckoutModel Checkout(PaymentInfoModel paymentInfoModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                CheckoutValidate(paymentInfoModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                CheckoutModel checkoutModel = new CheckoutModel
                {
                    LoginUserProfModel = new LoginUserProfModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    PaymentInfoModel = paymentInfoModel,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                    },
                };
                if (checkoutModel.PaymentInfoModel != null)
                {
                    List<string> numberSessions = new List<string>
                    {
                        "CaptchaNumberOTP0",
                        "CaptchaNumberOTP1",
                        "CaptchaNumberLogin0",
                        "CaptchaNumberLogin1",
                    };
                    archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, numberSessions);
                    checkoutModel.LoginUserProfModel.CaptchaAnswerLogin = null;
                    checkoutModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                    checkoutModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                }
                return checkoutModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        // GET: Checkout
        public void Checkout(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfoModel = paymentInfoModel ?? new PaymentInfoModel();
                if (paymentInfoModel.ShoppingCartModel == null)
                {
                    modelStateDictionary.AddModelError("", "Invalid shopping cart (Null)");
                }
                else
                {
                    if (paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count > 0 && paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount > 0)
                    {
                        BuildDeliveryInfoLookupData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseMessages = new List<string>(),
                            ResponseTypeId = ResponseTypeEnum.Success,
                        };
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("", "Empty shopping cart");
                    }
                }
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        // PUBLIC: CreateInvoice
        public void CreateInvoice(ref PaymentInfoModel paymentInfoModel, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //If it is not Final Invoice - create invoice as Html and Pdf file and email
            //Set the Invoice Type to Final - create invoice as Html and Pdf
            //Just in case reset the invoice type
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ArchLibBL archLibBL = new ArchLibBL();
                string orderFileName = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString();
                CodeDataModel codeDataModel;
                if (paymentInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
                {
                    codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("InvoiceType", execUniqueId).First(x => x.CodeDataNameId == 900);
                }
                else
                {
                    codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("InvoiceType", execUniqueId).First(x => x.CodeDataNameId == 100);
                }
                paymentInfoModel.OrderSummaryModel.InvoiceType = codeDataModel.CodeDataDesc0;
                orderFileName += "_" + (int)paymentInfoModel.OrderSummaryModel.InvoiceTypeId;
                paymentInfoModel.OrderSummaryModel.InvoiceFileNamePdf = orderFileName + ".pdf";
                string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceDataSubject", paymentInfoModel);
                string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
                string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", paymentInfoModel);
                PDFUtility pDFUtility = new PDFUtility();
                string invoiceDirectoryName = Utilities.GetServerMapPath("~/Invoices/");
                StreamWriter streamWriter = new StreamWriter(invoiceDirectoryName + orderFileName + ".html");
                streamWriter.Write(emailBodyHtml);
                streamWriter.Write(Environment.NewLine);
                streamWriter.Close();
                string pDFFullFileName = invoiceDirectoryName + orderFileName + ".pdf";
                paymentInfoModel.OrderSummaryModel.InvoiceFullFileNamePdf = pDFFullFileName;
                pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
                List<string> emailAttachmentFileNames = new List<string>
                {
                    pDFFullFileName,
                };
                var toEmailAddresss = paymentInfoModel.OrderSummaryModel.EmailAddress;// + ";" + ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", "ToEmailAddress");
                if (createForSessionObject.EmailAddress.ToLower() != sessionObjectModel.EmailAddress.ToLower())
                {
                    toEmailAddresss += ";" + sessionObjectModel.EmailAddress;
                }
                //archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.OrderSummaryModel.InvoiceHtmlString = emailBodyHtml;
                if (paymentInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
                {
                }
                else
                {
                    paymentInfoModel.OrderSummaryModel.InvoiceTypeId = InvoiceTypeEnum.FinalInvoice;
                    paymentInfoModel.OrderSummaryModel.InvoiceType = "Tax Invoice";
                    emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
                    orderFileName = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString() + "_" + (int)paymentInfoModel.OrderSummaryModel.InvoiceTypeId;
                    streamWriter = new StreamWriter(invoiceDirectoryName + orderFileName + ".html");
                    streamWriter.Write(emailBodyHtml);
                    streamWriter.Write(Environment.NewLine);
                    streamWriter.Close();
                    pDFFullFileName = invoiceDirectoryName + orderFileName + ".pdf";
                    pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
                }
                paymentInfoModel = null;
                return;
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
        // POST: DeliveryInfo
        public void DeliveryInfo(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.RemoveRange(1, paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Count - 1);
                CalculateDiscounts(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                CorpAcctModel corpAcctModel = (((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel);
                List<SalesTaxListModel> salesTaxListModels = GetSalesTaxListModels(paymentInfoModel.DeliveryAddressModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                List<CodeDataModel> salesTaxCaptionIds = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("SalesTaxType", "");
                CalculateSalesTax(paymentInfoModel, salesTaxListModels, salesTaxCaptionIds, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (paymentInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                {
                    ;
                }
                else
                {
                    CalculateDeliveryCharges(paymentInfoModel, corpAcctModel, salesTaxListModels, salesTaxCaptionIds, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                CreateShoppingCartTotals(ref paymentInfoModel, 0, "", sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                PaymentInfo4(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseMessages = new List<string>(),
                    ResponseTypeId = ResponseTypeEnum.Success,
                    PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>(),
                    ValidationSummaryMessage = "",
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseMessages = new List<string>(),
                    ResponseTypeId = ResponseTypeEnum.Error,
                    PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>()
                    {
                        new KeyValuePair<string, List<string>>("", new List<string> { "Error while processing delivery info" }),
                    },
                    ValidationSummaryMessage = "",
                };
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
            return;
        }
        // GET : ItemBundleData
        public ItemBundleDataModel ItemBundleData(long parentItemId, PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemBundleDataModel itemBundleDataModel;
            try
            {
                ShoppingCartItemModel shoppingCartItemModel;
                List<ShoppingCartItemModel> shoppingCartItemBundleModels = null;
                ItemModel itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == parentItemId);
                if (paymentInfoModel != null && paymentInfoModel.ShoppingCartModel != null && paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels != null)
                {
                    shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.FirstOrDefault(x => x.ItemId == parentItemId);
                    if (shoppingCartItemModel != null)
                    {
                        shoppingCartItemBundleModels = shoppingCartItemModel.ShoppingCartItemBundleModels;
                    }
                }
                if (shoppingCartItemBundleModels == null)
                {
                    shoppingCartItemBundleModels = RetailSlnCache.ParentItemBundleModels[parentItemId].ShoppingCartItemBundleModels;
                }
                itemBundleDataModel = new ItemBundleDataModel
                {
                    CurrencySymbol = RetailSlnCache.CurrencySymbol,
                    ItemModel = itemModel,
                    ParentItemId = parentItemId,
                    ShoppingCartItemBundleModels = shoppingCartItemBundleModels,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemBundleDataModel = new ItemBundleDataModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            "Error occurred while populating for Item " + parentItemId,
                            exception.Message,
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    }
                };
            }
            finally
            {
            }
            return itemBundleDataModel;
        }
        // GET: ItemCatalog
        public OrderItemDataModel ItemCatalog(string parentCategoryIdParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                string orderItemFilesPath = Utilities.GetServerMapPath("~/Files/OrderItem/");
                DirectoryInfo directoryInfo = new DirectoryInfo(orderItemFilesPath);
                FileInfo[] fileInfos = directoryInfo.GetFiles();
                if (fileInfos.Length == 0 || (fileInfos.Length == 1 && fileInfos[0].FullName.IndexOf("@Temp.txt") > -1))
                {
                    CreateOrderItemFiles(orderItemFilesPath, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                var aspNetRoleName = createForSessionObject?.AspNetRoleName;
                if (string.IsNullOrEmpty(aspNetRoleName))
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                if (aspNetRoleName != aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData)
                {
                    aspNetRoleName = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
                    aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                }
                if (parentCategoryId == 0)
                {
                    parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId01"].KVPValueData);
                }
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                //string orderItemDataHtmlFileName = $@"{orderItemFilesPath}\OrderItem_{aspNetRoleName}_{corpAcctId}_{parentCategoryId}.html";
                string orderItemDataHtmlFileName = $@"OrderItem_{aspNetRoleName}_{corpAcctId}_{parentCategoryId}.html";
                OrderItemDataModel orderItemDataModel = new OrderItemDataModel
                {
                    OrderItemDataHtmlFileName = orderItemDataHtmlFileName,
                    OrderItemDataHtmlString = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                return orderItemDataModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // GET: ItemMasterAttributes
        public ItemMasterAttributesModel ItemMasterAttributes(long itemMasterId, PaymentInfoModel paymentInfoModel, long tabId, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                ItemMasterModel itemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == itemMasterId);
                long itemId = itemMasterModel.ItemModels[0].ItemId.Value;
                ItemMasterAttributesModel itemMasterAttributesModel = new ItemMasterAttributesModel
                {
                    ItemMasterId = itemMasterId,
                    OrderItem1Model = new OrderItem1Model
                    {
                        ItemDiscountModels = RetailSlnCache.CorpAcctItemDiscountModels[corpAcctId],
                        ItemMasterModel = itemMasterModel,
                        ItemBundleDataModel = ItemBundleData(itemId, paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
                    },
                    TabId = tabId,
                };
                return itemMasterAttributesModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // GET: OrderItem
        public OrderItemModel OrderItem(string aspNetRoleName, string parentCategoryIdParm, string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                int.TryParse(pageNumParm, out int pageNum);
                int.TryParse(pageSizeParm, out int pageSize);
                pageNum = pageNum == 0 ? 1 : pageNum;
                pageSize = pageSize == 0 ? 45 : pageSize;
                aspNetRoleName = sessionObjectModel?.AspNetRoleName;
                if (string.IsNullOrEmpty(aspNetRoleName))
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                if (aspNetRoleName != aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData)
                {
                    aspNetRoleName = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
                    aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                }
                if (parentCategoryId == 0)
                {
                    parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId00"].KVPValueData);
                }
                string viewName = aspNetRoleKVPs["ViewName00"].KVPValueData;
                httpSessionStateBase["LastVisitedParentCategoryId"] = parentCategoryId;
                httpSessionStateBase["LastVisitedPageNum"] = pageNum;
                Dictionary<long, List<CategoryItemMasterHierModel>> parentCategoryItemMasterModels;
                List<CategoryItemMasterHierModel> itemMasterModels;
                if (RetailSlnCache.AspNetRoleParentCategoryItemMasterModels.TryGetValue(aspNetRoleName, out parentCategoryItemMasterModels))
                {
                    if (parentCategoryItemMasterModels.TryGetValue(parentCategoryId, out itemMasterModels))
                    {
                        itemMasterModels = itemMasterModels.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {
                        itemMasterModels = new List<CategoryItemMasterHierModel>();
                    }
                }
                else
                {
                    itemMasterModels = new List<CategoryItemMasterHierModel>();
                }
                long totalRowCount = RetailSlnCache.AspNetRoleParentCategoryItemMasterModels[aspNetRoleName][parentCategoryId].Count;
                long pageCount = totalRowCount / pageSize;
                if (totalRowCount % pageSize != 0)
                {
                    pageCount++;
                }
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)httpSessionStateBase["PaymentInfo"];
                CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                httpSessionStateBase["PaymentInfo"] = paymentInfoModel;
                CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                string categoryOrItem = "Item";
                RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> itemDiscountModels);
                itemDiscountModels = itemDiscountModels == null ? new Dictionary<long, ItemDiscountModel>() : itemDiscountModels;
                OrderItemModel orderItemModel = new OrderItemModel
                {
                    AspNetRoleName = aspNetRoleName,
                    CategoryOrItem = categoryOrItem,
                    CorpAcctid = corpAcctId,
                    CategoryItemMasterHierModels = itemMasterModels,
                    ImageCountPerRow = int.Parse(ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", categoryOrItem + "ImageCountPerRow")),
                    ImageDivWidth = ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", categoryOrItem + "ImageDivWidth"),
                    ImageHeight = ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", categoryOrItem + "ImageHeight"),
                    ImageWidth = ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", categoryOrItem + "ImageWidth"),
                    ItemDiscountModels = itemDiscountModels,
                    PageCount = pageCount,
                    PageNum = pageNum,
                    PageSize = pageSize,
                    ParentCategoryId = parentCategoryId,
                    ParentCategoryModel = RetailSlnCache.CategoryModels.First(x => x.CategoryId == parentCategoryId),
                    TotalRowCount = totalRowCount,
                    ViewName = viewName,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                //GetItemDiscounts(orderItemModel, controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                return orderItemModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // POST : PaymentInfo1
        public string PaymentInfo1(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//CreditSales
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                //paymentInfoModel.CompleteOrderModel.PaymentAmount = paymentInfoModel.CompleteOrderModel.PaymentAmount ?? 0;
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");

                long codeDataNameId = paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
                CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;

                Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
                CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.HideShoppingCart = true;
                paymentInfoModel.ShoppingCartModel.Checkout = true;

                if (paymentInfoModel.OrderHeaderWIPModel != null && paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId != null && paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId != 0)
                {
                    OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }

                string htmlString = "";
                return htmlString;
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
        // POST : PaymentInfo2
        public RazorPayResponse PaymentInfo2(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Razorpay
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            object creditCardResponseObject = null;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                BuildCreditCardDataModel(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                CreditCardDataModel creditCardDataModel = paymentInfoModel.CreditCardDataModel;
                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
                var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (creditCardProcessStatus)
                {
                    return (RazorPayResponse)creditCardResponseObject;
                }
                else
                {
                    return null;
                }
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
        // POST : PaymentInfo3
        public void PaymentInfo3(PaymentInfoModel paymentInfoModel, string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//RazorpayReturn
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
            CreditCardRazorPayBL razorPayIntegration = new CreditCardRazorPayBL();
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                if (razorPayIntegration.CheckPaymentSuccess(razorpay_payment_id, razorpay_order_id, razorpay_signature))
                {
                    Dictionary<string, string> processMessages = new Dictionary<string, string>();
                    processMessages[razorpay_payment_id] = razorpay_payment_id;
                    processMessages[razorpay_order_id] = razorpay_order_id;
                    processMessages[razorpay_signature] = razorpay_signature;
                    creditCardServiceBL.UpdCreditCardData(paymentInfoModel.CreditCardDataModel.CreditCardDataId, processMessages, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

                    ShoppingCartItemModel shoppingCartItemModelAmountPaid, shoppingCartItemModelBalanceDue;//, shoppingCartItemModelTotalInvoiceAmount;
                    shoppingCartItemModelAmountPaid = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                    shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);

                    shoppingCartItemModelAmountPaid.OrderAmount = shoppingCartItemModelBalanceDue.OrderAmount;
                    shoppingCartItemModelAmountPaid.OrderAmountFormatted = shoppingCartItemModelAmountPaid.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                    shoppingCartItemModelAmountPaid.OrderComments = "Ref# " + razorpay_payment_id + "; Ord# " + razorpay_order_id;//"Ord# : " + razorpay_order_id + " Ref# : " + razorpay_payment_id;

                    shoppingCartItemModelBalanceDue.OrderAmount = 0;
                    shoppingCartItemModelBalanceDue.OrderAmountFormatted = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");

                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = shoppingCartItemModelAmountPaid.OrderAmount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaidFormatted = shoppingCartItemModelAmountPaid.OrderAmountFormatted;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartItemModelBalanceDue.OrderAmount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartItemModelBalanceDue.OrderAmountFormatted;

                    AssignAuthorizedData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);

                    Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
                    paymentRefOptions["PaymentRefOption1"]["Key"] = "Ord #";
                    paymentRefOptions["PaymentRefOption1"]["Value"] = razorpay_order_id;
                    paymentRefOptions["PaymentRefOption2"] = new Dictionary<string, string>();
                    paymentRefOptions["PaymentRefOption2"]["Key"] = "Ref#:";
                    paymentRefOptions["PaymentRefOption1"]["Value"] = razorpay_payment_id;
                    CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    paymentInfoModel.HideShoppingCart = true;
                    paymentInfoModel.ShoppingCartModel.Checkout = true;
                    OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                return;
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
        // GET : PaymentInfo4
        public void PaymentInfo4(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Stripe
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ShoppingCartItemModel shoppingCartItemModelBalanceDue;
                shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                shoppingCartItemModelBalanceDue.OrderAmountRounded = (long)(shoppingCartItemModelBalanceDue.OrderAmount * 100);
                paymentInfoModel.ShoppingCartModel.Checkout = true;
                string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                GetCreditCardKVPs(creditCardProcessor, out Dictionary<string, string> creditCardKVPs, out Dictionary<string, string> creditCardDataKVPs, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.CreditCardDataModel = new CreditCardDataModel
                {
                    CreditCardAmount = shoppingCartItemModelBalanceDue.OrderAmountRounded.ToString(),
                    CreditCardAmountFormatted = shoppingCartItemModelBalanceDue.OrderAmountFormatted,
                    CreditCardKVPs = creditCardKVPs,
                    CreditCardDataKVPs = creditCardDataKVPs,
                    CreditCardProcessor = creditCardProcessor,
                    CreditCardTranType = "PURCHASE",
                    CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                    CreditCardZipCode = paymentInfoModel.DeliveryAddressModel.ZipCode,
                    EmailAddress = paymentInfoModel.OrderSummaryModel.EmailAddress,
                    NameAsOnCard = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim(),
                    TelephoneNumber = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum,
                    TelephoneNumberCode = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode,
                    TelephoneNumberCountryId = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId,
                    TelephoneNumberFormatted = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted,
                };
                return;
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
        // POST : PaymentInfo4Success
        public void PaymentInfo4Success(PaymentInfoModel paymentInfoModel, string paymentIntent_status, string paymentIntent_payment_method, string paymentIntent_id, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//RazorpayReturn
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                paymentInfoModel.CreditCardDataModel.CreditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                paymentInfoModel.CreditCardDataModel.ProcessMessage = paymentIntent_status;
                paymentInfoModel.CreditCardDataModel.RequestData = "";
                string responseDataJsonString = "";
                responseDataJsonString += "{" + Environment.NewLine;
                responseDataJsonString += "CreditCardAmount: \"" + paymentInfoModel.CreditCardDataModel.CreditCardAmount + "\"" + Environment.NewLine;
                responseDataJsonString += "paymentIntent_payment_method: \"" + paymentIntent_payment_method + "\"" + Environment.NewLine;
                responseDataJsonString += "paymentIntent_id: \"" + paymentIntent_id + "\"" + Environment.NewLine;
                responseDataJsonString += "}" + Environment.NewLine;
                paymentInfoModel.CreditCardDataModel.ResponseData = responseDataJsonString;
                paymentInfoModel.CreditCardDataModel.StatusNameDesc = "SUCCESS";
                creditCardServiceBL.CreateCreditCardData(paymentInfoModel.CreditCardDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

                ShoppingCartItemModel shoppingCartItemModelAmountPaid, shoppingCartItemModelBalanceDue;//, shoppingCartItemModelTotalInvoiceAmount;
                shoppingCartItemModelAmountPaid = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);

                shoppingCartItemModelAmountPaid.OrderAmount = shoppingCartItemModelBalanceDue.OrderAmount;
                shoppingCartItemModelAmountPaid.OrderAmountFormatted = shoppingCartItemModelAmountPaid.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModelAmountPaid.OrderComments = "Id " + paymentIntent_id + "; Method " + paymentIntent_payment_method;

                shoppingCartItemModelBalanceDue.OrderAmount = 0;
                shoppingCartItemModelBalanceDue.OrderAmountFormatted = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");

                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = shoppingCartItemModelAmountPaid.OrderAmount;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaidFormatted = shoppingCartItemModelAmountPaid.OrderAmountFormatted;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartItemModelBalanceDue.OrderAmount;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartItemModelBalanceDue.OrderAmountFormatted;

                AssignAuthorizedData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);

                Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
                paymentRefOptions["PaymentRefOption1"]["Key"] = "Id";
                paymentRefOptions["PaymentRefOption1"]["Value"] = paymentIntent_id;
                paymentRefOptions["PaymentRefOption2"] = new Dictionary<string, string>();
                paymentRefOptions["PaymentRefOption2"]["Key"] = "Method";
                paymentRefOptions["PaymentRefOption1"]["Value"] = paymentIntent_payment_method;

                CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.HideShoppingCart = true;
                paymentInfoModel.ShoppingCartModel.Checkout = true;
                OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
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
        // GET : PaymentInfo9
        public void PaymentInfo9(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Credit Card
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //There is nothing to be done right now
                return;
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
        // GET : PaymentInfo9Process
        public void PaymentInfo9Process(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Credit Card
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                CreditCardDataModel creditCardDataModel = paymentInfoModel.CreditCardDataModel;
                creditCardDataModel.CreditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
                ApplicationDataContext.OpenSqlConnection();
                var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out object creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                var creditCardLast4 = paymentInfoModel.CreditCardDataModel.CreditCardNumberLast4;
                var creditCardProcessMessage = creditCardDataModel.ProcessMessage;
                if (creditCardProcessStatus)
                {
                    Dictionary<string, string> processMessages = new Dictionary<string, string>();
                    creditCardServiceBL.UpdCreditCardData(paymentInfoModel.CreditCardDataModel.CreditCardDataId, processMessages, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

                    ShoppingCartItemModel shoppingCartItemModelAmountPaid, shoppingCartItemModelBalanceDue;//, shoppingCartItemModelTotalInvoiceAmount;
                    shoppingCartItemModelAmountPaid = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                    shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);

                    shoppingCartItemModelAmountPaid.OrderAmount = shoppingCartItemModelBalanceDue.OrderAmount;
                    shoppingCartItemModelAmountPaid.OrderAmountFormatted = shoppingCartItemModelAmountPaid.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                    //shoppingCartItemModelAmountPaid.OrderComments = "Ref# " + razorpay_payment_id + "; Ord# " + razorpay_order_id;//"Ord# : " + razorpay_order_id + " Ref# : " + razorpay_payment_id;

                    shoppingCartItemModelBalanceDue.OrderAmount = 0;
                    shoppingCartItemModelBalanceDue.OrderAmountFormatted = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");

                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = shoppingCartItemModelAmountPaid.OrderAmount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaidFormatted = shoppingCartItemModelAmountPaid.OrderAmountFormatted;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartItemModelBalanceDue.OrderAmount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartItemModelBalanceDue.OrderAmountFormatted;

                    AssignAuthorizedData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);

                    Dictionary<string, Dictionary<string, string>> paymentRefoptions = new Dictionary<string, Dictionary<string, string>>();
                    paymentRefoptions["PaymentRefOption1"] = new Dictionary<string, string>();
                    paymentRefoptions["PaymentRefOption1"]["Ref# : "] = creditCardDataModel.ProcessMessagesSuccess["PaymentRefNum"];
                    paymentRefoptions["PaymentRefOption2"] = new Dictionary<string, string>();
                    CreateOrder(paymentInfoModel, paymentRefoptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    paymentInfoModel.HideShoppingCart = true;
                    paymentInfoModel.ShoppingCartModel.Checkout = true;
                    OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    foreach (var processMessagesFailure in creditCardDataModel.ProcessMessagesFailure)
                    {
                        modelStateDictionary.AddModelError(processMessagesFailure.Key, processMessagesFailure.Value);
                    }
                }
                return;
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
        // PRIVATE: AssignAuthorized
        private void AssignAuthorizedData(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
            paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");

            long codeDataNameId = paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
            CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
            paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
            paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;

            return;
        }
        // POST: RemoveFromCart
        public void RemoveFromCart(ref PaymentInfoModel paymentInfoModel, int removeFromIndex, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (removeFromIndex > -1 && removeFromIndex < paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count)
                {
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && paymentInfoModel?.OrderHeaderWIPModel?.OrderDetailWIPModels?.Count > 0)
                    {
                        ApplicationDataContext.OpenSqlConnection();
                        var orderDetailWIPModel = paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels[removeFromIndex];
                        OrderDetailWIPDel(orderDetailWIPModel.OrderDetailWIPId.Value, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.CloseSqlConnection();
                    }
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.RemoveAt(removeFromIndex);
                    CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    throw new Exception("Invalid index in remove from cart");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : SearchResult
        public SearchResultModel SearchResult(string searchKeywordText, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SearchResultModel searchResultModel;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                searchResultModel = new SearchResultModel
                {
                    SearchKeywordText = searchKeywordText,
                    SearchMetaDataModels = ApplicationDataContext.SearchMetaDatasGet(searchKeywordText, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    CategoryListModel = new CategoryListModel
                    {
                        CategoryModels = new List<CategoryModel>(),
                    },
                    ItemMasterListModel = new ItemMasterListModel
                    {
                        ItemDiscountModels = RetailSlnCache.CorpAcctItemDiscountModels[corpAcctId],
                        ItemMasterModels = new List<ItemMasterModel>(),
                    },
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                foreach (var searchMetaDataModel in searchResultModel.SearchMetaDataModels)
                {
                    if (searchMetaDataModel.EntityTypeNameDesc == "CATEGORY")
                    {
                        searchResultModel.CategoryListModel.CategoryModels.Add(RetailSlnCache.CategoryModels.First(x => x.CategoryId == searchMetaDataModel.EntityId));
                    }
                    else
                    {
                        searchResultModel.ItemMasterListModel.ItemMasterModels.Add(RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == searchMetaDataModel.EntityId));
                    }
                }
                //List<long> searchIds = new List<long> { 0, 9, 18 };
                //List<ItemModel> searchItems = RetailSlnCache.ItemModels.Where(x => searchIds.Contains(x.ItemId.Value)).ToList();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                searchResultModel = new SearchResultModel
                {
                    SearchKeywordText = searchKeywordText,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            "Error while searching " + searchKeywordText,
                            exception.Message,
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    }
                };
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
            return searchResultModel;
        }
        // GET : ShoppingCartComments
        public void ShoppingCartComments(PaymentInfoModel paymentInfoModel, string indexParm, string bundleIndexParm, string orderComments, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int.TryParse(indexParm, out int index);
                int.TryParse(bundleIndexParm, out int bundleIndex);
                if (bundleIndex == -1)
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels[index].OrderComments = orderComments;
                }
                else
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels[index].ShoppingCartItemBundleModels[bundleIndex].OrderComments = orderComments;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
        }
        #endregion

        #region PUBLIC
        // PUBLIC: BuildPaymentInfoFromDeliveryInfoPost
        public void BuildPaymentInfoFromDeliveryInfoPost(ref PaymentInfoModel paymentInfoModel, PaymentInfoModel paymentInfoModelTemp, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            if (sessionObjectModel == null || createForSessionObject == null || paymentInfoModelTemp == null)
            {
                modelStateDictionary.AddModelError("", "Invalid session - User not logged in");
            }
            else
            {
                //sessionObjectModel = null;
                try
                {
                    int indexOf1 = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.IndexOf(';');
                    string deliveryMethodId = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.Substring(0, indexOf1);
                    paymentInfoModel.DeliveryMethodModel.DeliveryMethodId = (DeliveryMethodEnum)long.Parse(deliveryMethodId);
                    var applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
                    var corpAcctLocationModel = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels.First(x => x.CorpAcctLocationId == applSessionObjectModel.CorpAcctLocationId);
                    if (paymentInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                    {
                        int indexOf2 = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.IndexOf(';', indexOf1 + 1);
                        string pickupLocationId = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.Substring(indexOf1 + 1, indexOf2 - indexOf1 - 1);
                        paymentInfoModel.DeliveryMethodModel.PickupLocationId = long.Parse(pickupLocationId);
                        paymentInfoModel.DeliveryMethodModel.PickupLocationDemogInfoAddressModels = applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes ? RetailSlnCache.PickupLocationDemogInfoAddressModels2 : RetailSlnCache.PickupLocationDemogInfoAddressModels1;
                    }
                    else
                    {
                        paymentInfoModel.DeliveryMethodModel.PickupLocationId = 0;
                    }
                }
                catch
                {
                    paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId = null;
                    paymentInfoModel.DeliveryMethodModel.DeliveryMethodId = null;
                    paymentInfoModel.DeliveryMethodModel.PickupLocationId = null;
                }
                paymentInfoModel.OrderSummaryModel.AspNetUserId = paymentInfoModelTemp.OrderSummaryModel.AspNetUserId;
                paymentInfoModel.OrderSummaryModel.CorpAcctModel = paymentInfoModelTemp.OrderSummaryModel.CorpAcctModel;
                paymentInfoModel.OrderSummaryModel.CreatedByEmailAddress = paymentInfoModelTemp.OrderSummaryModel.CreatedByEmailAddress;
                paymentInfoModel.OrderSummaryModel.CreatedByFirstName = paymentInfoModelTemp.OrderSummaryModel.CreatedByFirstName;
                paymentInfoModel.OrderSummaryModel.CreatedByLastName = paymentInfoModelTemp.OrderSummaryModel.CreatedByLastName;
                paymentInfoModel.OrderSummaryModel.EmailAddress = paymentInfoModelTemp.OrderSummaryModel.EmailAddress;
                paymentInfoModel.OrderSummaryModel.InvoiceTypeId = paymentInfoModelTemp.OrderSummaryModel.InvoiceTypeId;
                paymentInfoModel.OrderSummaryModel.OrderDateTime = paymentInfoModelTemp.OrderSummaryModel.OrderDateTime;
                paymentInfoModel.OrderSummaryModel.OrderHeaderId = paymentInfoModelTemp.OrderSummaryModel.OrderHeaderId;
                paymentInfoModel.OrderSummaryModel.PersonId = paymentInfoModelTemp.OrderSummaryModel.PersonId;
                paymentInfoModel.OrderSummaryModel.TelephoneCode = paymentInfoModelTemp.OrderSummaryModel.TelephoneCode;
                paymentInfoModel.OrderSummaryModel.TelephoneCountryId = paymentInfoModelTemp.OrderSummaryModel.TelephoneCountryId;
                paymentInfoModel.OrderSummaryModel.TelephoneNumber = paymentInfoModelTemp.OrderSummaryModel.TelephoneNumber;
                paymentInfoModel.ShoppingCartModel = paymentInfoModelTemp.ShoppingCartModel;
                paymentInfoModel.CreditCardDataModel = paymentInfoModelTemp.CreditCardDataModel;
                paymentInfoModel.OrderHeaderWIPModel = paymentInfoModelTemp.OrderHeaderWIPModel;
                if (
                    string.IsNullOrWhiteSpace(paymentInfoModel.GiftCertPaymentModel.GiftCertNumber) &&
                    string.IsNullOrWhiteSpace(paymentInfoModel.GiftCertPaymentModel.GiftCertKey)
                   )
                {
                    ;
                }
                else
                {
                    if (long.TryParse(paymentInfoModel.GiftCertPaymentModel.GiftCertNumber, out long tempLong))
                    {
                        if (paymentInfoModel.GiftCertPaymentModel.GiftCertNumber.Length == 18 && paymentInfoModel.GiftCertPaymentModel.GiftCertKey.Length == 9)
                        {
                            ;
                        }
                        else
                        {
                            modelStateDictionary.AddModelError("GiftCertPaymentModel.GiftCertNumber", "Please enter valid Gift Cert#");
                            modelStateDictionary.AddModelError("GiftCertPaymentModel.GiftCertKey", "Please enter valid Gift Cert Key");
                        }
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("GiftCertPaymentModel.GiftCertNumber", "Please enter valid Gift Cert#");
                    }
                }
                paymentInfoModel.OrderSummaryModel.UserFullName = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim();
                UpdateDeliveryAddressInfo(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
        }
        // PUBLIC: BuildDeliveryInfoLookupData
        public void BuildDeliveryInfoLookupData(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
            var corpAcctLocationModel = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels.First(x => x.CorpAcctLocationId == applSessionObjectModel.CorpAcctLocationId);
            InvoiceTypeEnum? invoiceTypeId = paymentInfoModel.OrderSummaryModel.InvoiceTypeId;
            DemogInfoAddressModel createdForDemogInfoAddressModel = createForSessionObject.DemogInfoAddressModel;
            paymentInfoModel.DeliveryAddressModel = paymentInfoModel.DeliveryAddressModel ?? new DemogInfoAddressModel();
            if (string.IsNullOrEmpty(paymentInfoModel.DeliveryAddressModel.AddressLine1) && string.IsNullOrEmpty(paymentInfoModel.DeliveryAddressModel.AddressLine2))
            {//Populate the address with the one in the profile
                var deliveryDemogInfoCountryModel = RetailSlnCache.DeliveryDemogInfoCountryModels.FirstOrDefault(x => x.DemogInfoCountryId == createdForDemogInfoAddressModel.DemogInfoCountryId);
                if (deliveryDemogInfoCountryModel?.DemogInfoCountryId == createdForDemogInfoAddressModel.DemogInfoCountryId)
                {
                    paymentInfoModel.DeliveryAddressModel.BuildingTypeId = createdForDemogInfoAddressModel.BuildingTypeId;
                    paymentInfoModel.DeliveryAddressModel.AddressLine1 = createdForDemogInfoAddressModel.AddressLine1;
                    paymentInfoModel.DeliveryAddressModel.AddressLine2 = createdForDemogInfoAddressModel.AddressLine2;
                    paymentInfoModel.DeliveryAddressModel.CityName = createdForDemogInfoAddressModel.CityName;
                    paymentInfoModel.DeliveryAddressModel.CountryAbbrev = createdForDemogInfoAddressModel.CountryAbbrev;
                    paymentInfoModel.DeliveryAddressModel.CountryDesc = createdForDemogInfoAddressModel.CountryDesc;
                    paymentInfoModel.DeliveryAddressModel.DemogInfoCityId = createdForDemogInfoAddressModel.DemogInfoCityId;
                    paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId = createdForDemogInfoAddressModel.DemogInfoCountryId;
                    paymentInfoModel.DeliveryAddressModel.DemogInfoCountyId = createdForDemogInfoAddressModel.DemogInfoCountyId;
                    paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = createdForDemogInfoAddressModel.DemogInfoSubDivisionId;
                    paymentInfoModel.DeliveryAddressModel.DemogInfoZipId = createdForDemogInfoAddressModel.DemogInfoZipId;
                    paymentInfoModel.DeliveryAddressModel.StateAbbrev = createdForDemogInfoAddressModel.StateAbbrev;
                    paymentInfoModel.DeliveryAddressModel.ZipCode = createdForDemogInfoAddressModel.ZipCode;
                    paymentInfoModel.DeliveryAddressModel.HouseNumber = createdForDemogInfoAddressModel.HouseNumber;
                }
            }
            paymentInfoModel.DeliveryAddressModel.BuildingTypeId = paymentInfoModel.DeliveryAddressModel.BuildingTypeId ?? BuildingTypeEnum._;
            paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId = paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId ?? RetailSlnCache.DefaultDeliveryDemogInfoCountryId;
            paymentInfoModel.DeliveryAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
            paymentInfoModel.DeliveryAddressModel.DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems;
            paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
            paymentInfoModel.DeliveryDataModel = paymentInfoModel.DeliveryDataModel ?? new DeliveryDataModel
            {
                AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
            };
            if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum))
            {
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId = createForSessionObject.TelephoneCountryId;
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum = createForSessionObject.PhoneNumber;
            }
            paymentInfoModel.DeliveryMethodModel = paymentInfoModel.DeliveryMethodModel ?? new DeliveryMethodModel
            {
            };
            paymentInfoModel.DeliveryMethodModel.DeliveryMethods = RetailSlnCache.DeliveryMethodsList[applSessionObjectModel.CorpAcctModel.CreditSale.Value];
            paymentInfoModel.DeliveryMethodModel.PickupLocationDemogInfoAddressModels = applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes ? RetailSlnCache.PickupLocationDemogInfoAddressModels2 : RetailSlnCache.PickupLocationDemogInfoAddressModels1;
            int i, fromIndex, toIndex;
            if (applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes)
            {
                fromIndex = RetailSlnCache.DeliveryMethodSelectListItems.Count - 1;
                toIndex = RetailSlnCache.DeliveryMethodSelectListItems.Count;
            }
            else
            {
                fromIndex = 0;
                toIndex = RetailSlnCache.DeliveryMethodSelectListItems.Count - 1;
            }
            paymentInfoModel.DeliveryMethodModel.DeliveryMethodPickupLocationSelectListItems = new List<SelectListItem>();
            for (i = fromIndex; i < toIndex; i++)
            {
                paymentInfoModel.DeliveryMethodModel.DeliveryMethodPickupLocationSelectListItems.Add(RetailSlnCache.DeliveryMethodSelectListItems[i]);
            }
            paymentInfoModel.OrderSummaryModel = paymentInfoModel.OrderSummaryModel ?? new OrderSummaryModel();
            paymentInfoModel.OrderSummaryModel.AspNetUserId = sessionObjectModel.AspNetUserId;
            paymentInfoModel.OrderSummaryModel.CorpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
            paymentInfoModel.OrderSummaryModel.CreatedByEmailAddress = sessionObjectModel.EmailAddress.ToLower();
            paymentInfoModel.OrderSummaryModel.CreatedByFirstName = sessionObjectModel.FirstName;
            paymentInfoModel.OrderSummaryModel.CreatedByLastName = sessionObjectModel.LastName;
            paymentInfoModel.OrderSummaryModel.EmailAddress = createForSessionObject.EmailAddress.ToLower();
            paymentInfoModel.OrderSummaryModel.FirstName = createForSessionObject.FirstName;
            paymentInfoModel.OrderSummaryModel.InvoiceTypeId = invoiceTypeId.Value;
            paymentInfoModel.OrderSummaryModel.LastName = createForSessionObject.LastName;
            paymentInfoModel.OrderSummaryModel.PersonId = createForSessionObject.PersonId;
            paymentInfoModel.OrderSummaryModel.TelephoneCode = null;
            paymentInfoModel.OrderSummaryModel.TelephoneCountryId = createForSessionObject.TelephoneCountryId.Value;
            paymentInfoModel.OrderSummaryModel.TelephoneNumber = createForSessionObject.PhoneNumber;
            paymentInfoModel.GiftCertPaymentModel = new GiftCertPaymentModel
            {
                GiftCertPaymentAmount = 0,
            };
            paymentInfoModel.PaymentModeModel = paymentInfoModel.PaymentModeModel ?? new PaymentModeModel
            {
            };
            paymentInfoModel.PaymentModeModel.PaymentModes = RetailSlnCache.PaymentModesList[applSessionObjectModel.CorpAcctModel.CreditSale.Value];
            if (
                string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.AddressLine1) &&
                string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.CityName) &&
                string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode)
               )
            {
                if (corpAcctLocationModel.CorpAcctId == 0)
                {
                    paymentInfoModel.DeliveryAddressModel.AddressName = "";
                }
                else
                {
                    paymentInfoModel.DeliveryAddressModel.AddressName = corpAcctLocationModel.LocationName;
                }
                paymentInfoModel.DeliveryAddressModel.AddressLine1 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine1;
                paymentInfoModel.DeliveryAddressModel.AddressLine2 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine2;
                paymentInfoModel.DeliveryAddressModel.AddressLine3 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine3;
                paymentInfoModel.DeliveryAddressModel.AddressLine4 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine4;
                paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = corpAcctLocationModel.DemogInfoAddressModel.DemogInfoSubDivisionId;
                paymentInfoModel.DeliveryAddressModel.CityName = corpAcctLocationModel.DemogInfoAddressModel.CityName;
                paymentInfoModel.DeliveryAddressModel.StateAbbrev = corpAcctLocationModel.DemogInfoAddressModel.StateAbbrev;
                paymentInfoModel.DeliveryAddressModel.ZipCode = corpAcctLocationModel.DemogInfoAddressModel.ZipCode;
                try
                {
                    paymentInfoModel.OrderSummaryModel.TelephoneCountryId = corpAcctLocationModel.PrimaryTelephoneCountryId.Value;
                    paymentInfoModel.OrderSummaryModel.TelephoneNumber = corpAcctLocationModel.PrimaryTelephoneNumber.Value.ToString();
                    paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId = corpAcctLocationModel.PrimaryTelephoneCountryId;
                    paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum = corpAcctLocationModel.PrimaryTelephoneNumber.Value.ToString();
                    paymentInfoModel.DeliveryDataModel.AlternateTelephoneDemogInfoCountryId = corpAcctLocationModel.AlternateTelephoneCountryId;
                    paymentInfoModel.DeliveryDataModel.AlternateTelephoneNum = corpAcctLocationModel.AlternateTelephoneNumber == null ? null : corpAcctLocationModel.AlternateTelephoneNumber.Value.ToString();
                }
                catch
                {
                }
            }
            return;
        }
        #region
        //// PUBLIC: CreateOrderWIP
        //public void CreateOrderWIP(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //From session write to database items in shopping cart
        //    //Clear shopping cart from session
        //    //Load shopping cart from database into session
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.OpenSqlConnection();
        //        SqlConnection sqlConnection = ApplicationDataContext.SqlConnectionObject;
        //        ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
        //        long? orderHeaderWIPId;
        //        float maxSeqNum;
        //        OrderHeaderWIPModel orderHeaderWIPModel;
        //        if (paymentInfoModel?.ShoppingCartModel?.ShoppingCartItemModels?.Count > 0)
        //        {//If Session has items add it to database - This has been added prior to logging in
        //            //Get Max Order Header Id for the created for user
        //            orderHeaderWIPId = ApplicationDataContext.OrderHeaderWIPMaxIdGet(createForSessionObject.PersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (orderHeaderWIPId.HasValue)
        //            {
        //                maxSeqNum = ApplicationDataContext.OrderDetailWIPMaxSeqNumGet(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                //orderHeaderWIPModel = ApplicationDataContext.OrderHeaderWIPGet(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            else
        //            {
        //                maxSeqNum = 0;
        //                orderHeaderWIPModel = OrderHeaderWIPModelCreate(applSessionObjectModel.CorpAcctLocationId, paymentInfoModel.OrderSummaryModel.InvoiceTypeId, paymentInfoModel.OrderSummaryModel.OrderDateTime, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                ApplicationDataContext.OrderHeaderWIPAdd(orderHeaderWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                orderHeaderWIPId = orderHeaderWIPModel.OrderHeaderWIPId;
        //            }
        //            OrderDetailWIPModel orderDetailWIPModelTemp;
        //            foreach (var shoppingCartItemModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels)
        //            {
        //                orderDetailWIPModelTemp = new OrderDetailWIPModel
        //                {
        //                    ClientId = clientId,
        //                    ItemId = shoppingCartItemModel.ItemId.Value,
        //                    ItemRate = shoppingCartItemModel.ItemRate.Value,
        //                    OrderHeaderWIPId = orderHeaderWIPId.Value,
        //                    OrderQty = shoppingCartItemModel.OrderQty.Value,
        //                    ParentItemId = shoppingCartItemModel.ParentItemId.Value,
        //                    SeqNum = ++maxSeqNum,
        //                    ShoppingCartItemBundleModels = new List<OrderDetailWIPModel>(),
        //                };
        //                ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModelTemp, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                //if (shoppingCartItemModel.ParentItemId == 0)
        //                //{
        //                //}
        //                //else
        //                //{
        //                //    foreach (var shoppingCartItemBundleModel in shoppingCartItemModel.ShoppingCartItemBundleModels)
        //                //    {
        //                //        new OrderDetailWIPModel
        //                //        {
        //                //            ClientId = clientId,
        //                //            ItemId = shoppingCartItemBundleModel.ItemId.Value,
        //                //            ItemRate = shoppingCartItemBundleModel.ItemRate.Value,
        //                //            OrderHeaderWIPId = orderHeaderWIPId.Value,
        //                //            OrderQty = shoppingCartItemBundleModel.OrderQty.Value,
        //                //            ParentItemId = shoppingCartItemModel.ItemId.Value,
        //                //            SeqNum = ++maxSeqNum,
        //                //            ShoppingCartItemBundleModels = null,
        //                //        };
        //                //        ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModelTemp, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                //    }
        //                //}
        //            }
        //            paymentInfoModel.ShoppingCartModel = null;
        //            CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        orderHeaderWIPId = ApplicationDataContext.OrderHeaderWIPMaxIdGet(createForSessionObject.PersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        if (orderHeaderWIPId.HasValue)
        //        {
        //            paymentInfoModel.OrderHeaderWIPModel = ApplicationDataContext.OrderHeaderWIPGet(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            foreach (var orderDetailWIPModel in paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels)
        //            {
        //                paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum = orderDetailWIPModel.SeqNum;
        //                AddToCart(ref paymentInfoModel, orderDetailWIPModel.ItemId.ToString(), orderDetailWIPModel.OrderQty.ToString(), false, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //        }
        //        CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        httpSessionStateBase["PaymentInfo"] = paymentInfoModel;
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
        #endregion
        // PUBLIC: CreateOrderWIP
        public void CreateOrderWIP(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //From session write to database items in shopping cart
            //Clear shopping cart from session
            //Load shopping cart from database into session
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.OpenSqlConnection();
                SqlConnection sqlConnection = ApplicationDataContext.SqlConnectionObject;
                ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                long? orderHeaderWIPId;
                float maxSeqNum;
                OrderHeaderWIPModel orderHeaderWIPModel;
                if (paymentInfoModel?.ShoppingCartModel?.ShoppingCartItemModels?.Count > 0)
                {//If Session has items add it to database - This has been added prior to logging in
                    //Get Max Order Header Id for the created for user
                    orderHeaderWIPId = ApplicationDataContext.OrderHeaderWIPMaxIdGet(createForSessionObject.PersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (orderHeaderWIPId.HasValue)
                    {
                        maxSeqNum = ApplicationDataContext.OrderDetailWIPMaxSeqNumGet(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        //orderHeaderWIPModel = ApplicationDataContext.OrderHeaderWIPGet(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    else
                    {
                        maxSeqNum = 0;
                        orderHeaderWIPModel = OrderHeaderWIPModelCreate(applSessionObjectModel.CorpAcctLocationId, paymentInfoModel.OrderSummaryModel.InvoiceTypeId, paymentInfoModel.OrderSummaryModel.OrderDateTime, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.OrderHeaderWIPAdd(orderHeaderWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                        orderHeaderWIPId = orderHeaderWIPModel.OrderHeaderWIPId;
                    }
                    OrderDetailWIPModel orderDetailWIPModelTemp;
                    foreach (var shoppingCartItemModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels)
                    {
                        orderDetailWIPModelTemp = new OrderDetailWIPModel
                        {
                            ClientId = clientId,
                            ItemId = shoppingCartItemModel.ItemId.Value,
                            ItemRate = shoppingCartItemModel.ItemRate.Value,
                            OrderHeaderWIPId = orderHeaderWIPId.Value,
                            OrderQty = shoppingCartItemModel.OrderQty.Value,
                            ParentItemId = shoppingCartItemModel.ParentItemId.Value,
                            SeqNum = ++maxSeqNum,
                            ShoppingCartItemBundleModels = new List<OrderDetailWIPModel>(),
                        };
                        ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModelTemp, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                        //if (shoppingCartItemModel.ParentItemId == 0)
                        //{
                        //}
                        //else
                        //{
                        //    foreach (var shoppingCartItemBundleModel in shoppingCartItemModel.ShoppingCartItemBundleModels)
                        //    {
                        //        new OrderDetailWIPModel
                        //        {
                        //            ClientId = clientId,
                        //            ItemId = shoppingCartItemBundleModel.ItemId.Value,
                        //            ItemRate = shoppingCartItemBundleModel.ItemRate.Value,
                        //            OrderHeaderWIPId = orderHeaderWIPId.Value,
                        //            OrderQty = shoppingCartItemBundleModel.OrderQty.Value,
                        //            ParentItemId = shoppingCartItemModel.ItemId.Value,
                        //            SeqNum = ++maxSeqNum,
                        //            ShoppingCartItemBundleModels = null,
                        //        };
                        //        ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModelTemp, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                        //    }
                        //}
                    }
                    paymentInfoModel.ShoppingCartModel = null;
                    CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                orderHeaderWIPId = ApplicationDataContext.OrderHeaderWIPMaxIdGet(createForSessionObject.PersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (orderHeaderWIPId.HasValue)
                {
                    paymentInfoModel.OrderHeaderWIPModel = ApplicationDataContext.OrderHeaderWIPGet(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    foreach (var orderDetailWIPModel in paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels)
                    {
                        paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum = orderDetailWIPModel.SeqNum;
                        AddToCartModel addToCartModel = new AddToCartModel
                        {
                            ItemIdParm = orderDetailWIPModel.ItemId.ToString(),
                            OrderQtyParm = orderDetailWIPModel.OrderQty.ToString(),
                        };
                        AddToCart(ref paymentInfoModel, addToCartModel, false, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                httpSessionStateBase["PaymentInfo"] = paymentInfoModel;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
        #endregion

        #region PRIVATE
        #region
        // PRIVATE : AddItemToShoppingCart
        //private void AddItemToShoppingCart(ref PaymentInfoModel paymentInfoModel, ItemModel itemModel, long orderQty, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ShoppingCartItemModel shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId);
        //        if (shoppingCartItemModel == null)
        //        {
        //            CreateShoppingCartItemModel(ref shoppingCartItemModel, itemModel, orderQty, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Add(shoppingCartItemModel);
        //            if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createOrderWIP)
        //            {
        //                ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
        //                OrderWIPAdd(ref paymentInfoModel, applSessionObjectModel.CorpAcctLocationId, itemModel, orderQty, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //        }
        //        else
        //        {
        //            CreateShoppingCartItemModel(ref shoppingCartItemModel, itemModel, shoppingCartItemModel.OrderQty + orderQty, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (createForSessionObject != null && createOrderWIP)
        //            {
        //                ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
        //                OrderDetailWIPUpdate(ref paymentInfoModel, itemModel.ItemId.Value, shoppingCartItemModel.OrderQty.Value, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //        }
        //        CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        // PRIVATE : AddItemToShoppingCart
        private void AddItemToShoppingCart(ref PaymentInfoModel paymentInfoModel, AddToCartModel addToCartModel, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long parentItemId;
                if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                {
                    if (addToCartModel.DoNotBreakBundle)
                    {
                        parentItemId = 0;
                    }
                    else
                    {
                        parentItemId = addToCartModel.ItemId;
                    }
                }
                else
                {
                    parentItemId = 0;
                }
                CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                ShoppingCartItemModel shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.FirstOrDefault(x => x.ItemId == addToCartModel.ItemId && x.ParentItemId == parentItemId);
                if (shoppingCartItemModel == null)
                {
                    CreateShoppingCartItemModel(ref shoppingCartItemModel, addToCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Add(shoppingCartItemModel);
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createOrderWIP)
                    {
                        //ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                        //OrderWIPAdd(ref paymentInfoModel, applSessionObjectModel.CorpAcctLocationId, addToCartModel, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                else
                {
                    CreateShoppingCartItemModel(ref shoppingCartItemModel, addToCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createOrderWIP)
                    {
                        ////ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                        //OrderDetailWIPUpdate(ref paymentInfoModel, shoppingCartItemModel, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                CalculateTotalOrderAmount(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        #region
        // PRIVATE: AddToCart
        //private string AddToCart(ref PaymentInfoModel paymentInfoModel, string itemIdParm, string orderQtyParm, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string errorMessage = "";
        //        ItemModel itemModel = null;
        //        if (long.TryParse(itemIdParm, out long itemId))
        //        {
        //            itemModel = RetailSlnCache.ItemModels.FirstOrDefault(x => x.ItemId == itemId);
        //            if (itemModel == null)
        //            {
        //                errorMessage += "Select valid item;";
        //            }
        //        }
        //        else
        //        {
        //            errorMessage += "Select valid item;";
        //        }
        //        if (!long.TryParse(orderQtyParm, out long orderQty))
        //        {
        //            errorMessage += "Enter quantity;";
        //        }
        //        if (errorMessage == "")
        //        {
        //            AddItemToShoppingCart(ref paymentInfoModel, itemModel, orderQty, createOrderWIP, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return errorMessage;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        // PRIVATE: AddToCart
        private string AddToCart(ref PaymentInfoModel paymentInfoModel, AddToCartModel addToCartModel, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string errorMessage = "";
                ItemModel itemModel = null;
                if (long.TryParse(addToCartModel.ItemIdParm, out long itemId))
                {
                    addToCartModel.ItemId = itemId;
                    itemModel = RetailSlnCache.ItemModels.FirstOrDefault(x => x.ItemId == addToCartModel.ItemId);
                    if (itemModel == null)
                    {
                        errorMessage += "Select valid item;";
                    }
                    else
                    {
                        addToCartModel.ItemModel = itemModel;
                    }
                }
                else
                {
                    errorMessage += "Select valid item;";
                }
                if (!long.TryParse(addToCartModel.OrderQtyParm, out long orderQty))
                {
                    errorMessage += "Enter quantity;";
                }
                else
                {
                    addToCartModel.OrderQty = orderQty;
                }
                if (errorMessage == "")
                {
                    AddItemToShoppingCart(ref paymentInfoModel, addToCartModel, createOrderWIP, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return errorMessage;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : BuildCreditCardDataModel
        private void BuildCreditCardDataModel(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                ShoppingCartItemModel shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                paymentInfoModel.CreditCardDataModel = new CreditCardDataModel
                {
                    CreditCardAmount = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString("0.00"),
                    CreditCardExpMM = null,
                    CreditCardExpYear = null,
                    CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId, ipAddress, execUniqueId, loggedInUserId),
                    CreditCardNumber = null,
                    CreditCardProcessor = creditCardProcessor,
                    CreditCardSecCode = null,
                    CreditCardTranType = "PAYMENT",
                    CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                    EmailAddress = paymentInfoModel.OrderSummaryModel.EmailAddress,
                    NameAsOnCard = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim(),
                    TelephoneNumber = paymentInfoModel.OrderSummaryModel.TelephoneNumber,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateDiscounts
        private void CalculateDiscounts(PaymentInfoModel paymentInfoModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                PriestListModel priestListModel = ApplicationDataContext.PriestListGet(createForSessionObject.PersonId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (priestListModel == null)
                {
                    if (!string.IsNullOrWhiteSpace(paymentInfoModel.CouponPaymentModel?.CouponNumber))
                    {
                        CouponListModel couponListModel = ApplicationDataContext.CouponListGet(paymentInfoModel.CouponPaymentModel.CouponNumber, paymentInfoModel.OrderSummaryModel.OrderDateTime, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (couponListModel != null)
                        {
                            var shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                            float orderAmount = -1 * shoppingCartItemModelSummary.OrderAmount.Value * couponListModel.DiscountPercent / 100;
                            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                            (
                                new ShoppingCartItemModel
                                {
                                    ItemId = null,
                                    ItemRate = 0,
                                    ItemShortDesc = "Coupon Discount " + couponListModel.DiscountPercent + "%",
                                    OrderAmount = orderAmount,
                                    OrderAmountFormatted = orderAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                    OrderComments = null,
                                    OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                    OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                                }
                            );
                        }
                    }
                }
                else
                {
                    var shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                    float orderAmount = -1 * shoppingCartItemModelSummary.OrderAmount.Value * (priestListModel.CommissionPercent + priestListModel.DiscountPercent) / 100;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemId = null,
                            ItemRate = 0,
                            ItemShortDesc = "Purohit Sahayam " + (priestListModel.CommissionPercent + priestListModel.DiscountPercent) + "%",
                            OrderAmount = orderAmount,
                            OrderAmountFormatted = orderAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            OrderComments = null,
                            OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                            OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                        }
                    );
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateDeliveryCharges
        private void CalculateDeliveryCharges(PaymentInfoModel paymentInfoModel, CorpAcctModel corpAcctModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (corpAcctModel.ShippingAndHandlingCharges == YesNoEnum.Yes)
                {
                    DeliveryChargeModel deliveryChargeModel = GetDeliveryChargeModel(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (deliveryChargeModel != null)
                    {
                        var shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
                        var shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded;
                        var fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
                        var shoppingCartItemSummaryModelsFromCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Count;
                        float shippingAndHandlingFuelSalesTaxAmountTotal = 0;
                        List<ShoppingCartItemModel> shoppingCartItemModelTemps = new List<ShoppingCartItemModel>();
                        paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = shippingAndHandlingChargesRate,
                                ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
                                OrderAmount = shippingAndHandlingChargesAmount + fuelCharges,
                                OrderAmountFormatted = (shippingAndHandlingChargesAmount + fuelCharges).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                OrderComments = null,
                                OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                OrderDetailTypeId = OrderDetailTypeEnum.DoNotShow,
                            }
                        );
                        shippingAndHandlingFuelSalesTaxAmountTotal = shippingAndHandlingChargesAmount + fuelCharges;
                        foreach (var salesTaxListModel in salesTaxListModels)
                        {
                            var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                            (
                                new ShoppingCartItemModel
                                {
                                    ItemId = null,
                                    ItemRate = shippingAndHandlingChargesRate,
                                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
                                    OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
                                    OrderAmountFormatted = ((shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                    OrderComments = null,
                                    OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                    OrderDetailTypeId = OrderDetailTypeEnum.DoNotShow,
                                }
                            );
                            shippingAndHandlingFuelSalesTaxAmountTotal += (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f;
                        }
                        paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = shippingAndHandlingChargesRate,
                                ItemShortDesc = "Shipping, Handling, Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + shippingAndHandlingFuelSalesTaxAmountTotal + " " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime + " with GST",
                                OrderAmount = shippingAndHandlingFuelSalesTaxAmountTotal,
                                OrderAmountFormatted = (shippingAndHandlingChargesAmount + fuelCharges).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                OrderComments = null,
                                OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                            }
                        );
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateSalesTax
        private void CalculateSalesTax(PaymentInfoModel paymentInfoModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                float? orderAmout;
                foreach (var salesTaxListModel in salesTaxListModels)
                {
                    var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                    if (salesTaxListModel.LineItemLevelName == "SUMMARY")
                    {
                        orderAmout = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount * salesTaxListModel.SalesTaxRate / 100f;
                        paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                                ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
                                OrderAmount = orderAmout,
                                OrderAmountFormatted = orderAmout.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                OrderComments = null,
                                OrderQty = 1,
                                OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                            }
                        );
                    }
                    else
                    {
                        float totalSalesTaxAmount = 0, salesTaxAmount;
                        foreach (var shoppingCartItemModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels)
                        {
                            var itemSpecValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItemModel.ItemId).ItemSpecModels.ToList().First(x => x.ItemSpecMasterModel.SpecName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemSpecValue;
                            salesTaxAmount = float.Parse(itemSpecValue) * shoppingCartItemModel.OrderAmount.Value / 100f;
                            totalSalesTaxAmount += salesTaxAmount;
                            shoppingCartItemModel.ShoppingCartItemSummarys.Add
                            (
                                new ShoppingCartItemModel
                                {
                                    ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                    ItemRate = float.Parse(itemSpecValue),
                                    ItemRateFormatted = (float.Parse(itemSpecValue) / 100f).ToString("#0.00%"),
                                    OrderAmount = salesTaxAmount,
                                    OrderAmountFormatted = salesTaxAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                }
                            );
                        }
                        paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                                ItemShortDesc = salesTaxCaptionId.CodeDataDesc0,
                                OrderAmount = totalSalesTaxAmount,
                                OrderAmountFormatted = totalSalesTaxAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                OrderComments = null,
                                OrderQty = 1,
                                OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                            }
                        );
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateTotalOrderAmount
        private void CalculateTotalOrderAmount(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
                foreach (var shoppingCartItemModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels)
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount += shoppingCartItemModel.OrderQty;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalcValue;
                }
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
                var shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                if (shoppingCartItemModelSummary != null)
                {
                    shoppingCartItemModelSummary.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                    shoppingCartItemModelSummary.OrderAmountFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                    shoppingCartItemModelSummary.ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalOrderAmount).CodeDataDesc0;
                    shoppingCartItemModelSummary.ItemShortDesc += " (" + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + " / " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount + ")";
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE: CheckoutValidate
        private void CheckoutValidate(PaymentInfoModel paymentInfoModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                paymentInfoModel = paymentInfoModel ?? new PaymentInfoModel();
                ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItemModels.Count > 0 && shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount > 0)
                    {
                        ;
                    }
                    else
                    {
                        throw new Exception("Shopping Cart is Empty");
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
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
        // PRIVATE: CreateOrder
        //private void CreateOrder(PaymentInfoModel paymentInfoModel, string paymentOrderIdCaption, string paymentOrderId, string paymentIdCaption, string paymentId, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        private void CreateOrder(PaymentInfoModel paymentInfoModel, Dictionary<string, Dictionary<string, string>> paymentRefOptions, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                CorpAcctModel corpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
                //paymentInfoModel.OrderSummaryModel.OrderDateTime = paymentInfoModel.OrderSummaryModel.OrderDateTime ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                OrderHeader orderHeader = CreateOrderHeader(paymentInfoModel, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.OrderHeaderWIPModel = paymentInfoModel.OrderHeaderWIPModel ?? new OrderHeaderWIPModel();
                paymentInfoModel.OrderHeaderWIPModel.OrderDateTime = paymentInfoModel.OrderSummaryModel.OrderDateTime;
                ApplicationDataContext.OrderHeaderAdd(orderHeader, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderHeaderSummary orderHeaderSummary = CreateOrderHeaderSummary(paymentInfoModel, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                orderHeaderSummary.OrderHeaderId = orderHeader.OrderHeaderId;
                ApplicationDataContext.OrderHeaderSummaryAdd(orderHeaderSummary, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderDetail orderDetail;
                //OrderDetailItemBundle orderDetailItemBundle;
                float seqNum = 0;
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels)
                {
                    orderDetail = CreateOrderDetail(orderHeaderSummary.OrderHeaderSummaryId, ++seqNum, shoppingCartItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.OrderDetailAdd(orderDetail, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (shoppingCartItem.ShoppingCartItemBundleModels != null)
                    {
                        foreach (var shoppingCartItemBundleModel in shoppingCartItem.ShoppingCartItemBundleModels)
                        {
                            //orderDetail = CreateOrderDetail(orderDetail.OrderDetailId, ++seqNum, shoppingCartItemBundleModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                            //ApplicationDataContext.OrderDetailItemBundleAdd(orderDetailItemBundle, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                            //orderDetail = CreateOrderDetail(orderHeaderSummary.OrderHeaderSummaryId, ++seqNum, shoppingCartItem, shoppingCartItemBundleModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                            orderDetail = CreateOrderDetail(orderHeaderSummary.OrderHeaderSummaryId, ++seqNum, shoppingCartItemBundleModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                            ApplicationDataContext.OrderDetailAdd(orderDetail, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                    }
                }
                foreach (var shoppingCartSummaryItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary)
                {
                    orderDetail = CreateOrderDetail(orderHeaderSummary.OrderHeaderSummaryId, ++seqNum, shoppingCartSummaryItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.OrderDetailAdd(orderDetail, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                ArchLibDataContext.CreateDemogInfoAddress(paymentInfoModel.DeliveryAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.DeliveryDataModel.DeliveryAddressModel = paymentInfoModel.DeliveryAddressModel;
                paymentInfoModel.DeliveryDataModel.OrderHeaderId = orderHeader.OrderHeaderId;
                paymentInfoModel.DeliveryDataModel.DeliveryMethodId = (long?)paymentInfoModel.DeliveryMethodModel.DeliveryMethodId;
                paymentInfoModel.DeliveryDataModel.PickupLocationId = paymentInfoModel.DeliveryMethodModel.PickupLocationId;
                paymentInfoModel.OrderSummaryModel.OrderHeaderId = orderHeader.OrderHeaderId;
                ApplicationDataContext.OrderDeliveryAdd(paymentInfoModel.DeliveryDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.PaymentDataModel = new PaymentDataModel
                {
                    CouponId = 0,
                    CreditCardDataId = paymentInfoModel.CreditCardDataModel.CreditCardDataId,
                    GiftCertId = 0,
                    OrderHeaderId = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value,
                    PaymentModeId = (long)paymentInfoModel.PaymentModeModel.PaymentModeId,
                    PaymentRefOptions = paymentRefOptions,
                    //PaymentRefNumCaption1 = string.IsNullOrWhiteSpace(paymentId) ? "" : paymentIdCaption,
                    //PaymentRefNumData1 = string.IsNullOrWhiteSpace(paymentId) ? "" : paymentId,
                    //PaymentRefNumCaption2 = string.IsNullOrWhiteSpace(paymentOrderId) ? "" : paymentOrderIdCaption,
                    //PaymentRefNumData2 = string.IsNullOrWhiteSpace(paymentOrderId) ? "" : paymentOrderId,
                };
                ApplicationDataContext.OrderPaymentAdd(paymentInfoModel.PaymentDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                ArchLibDataContext.UpdPerson(paymentInfoModel.OrderSummaryModel.PersonId.Value, paymentInfoModel.OrderSummaryModel.FirstName, paymentInfoModel.OrderSummaryModel.LastName, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
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
        // PRIVATE: CreateOrderDetail
        private OrderDetail CreateOrderDetail(long orderHeaderSummaryId, float seqNum, ShoppingCartItemModel shoppingCartItemModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                OrderDetail orderDetail = new OrderDetail
                {
                    ClientId = clientId,
                    DimensionUnitId = DimensionUnitEnum.Centimeter,
                    DiscountPercent = shoppingCartItemModel.ItemDiscountPercent == null ? 0 :shoppingCartItemModel.ItemDiscountPercent.Value,
                    DiscountPercentOriginal = shoppingCartItemModel.ItemDiscountPercent == null ? 0 : shoppingCartItemModel.ItemDiscountPercent.Value,
                    HeightValue = shoppingCartItemModel.HeightValue == null ? 0 : shoppingCartItemModel.HeightValue.Value,
                    HSNCode = shoppingCartItemModel.HSNCode,
                    ItemDiscountAmount = shoppingCartItemModel.ItemDiscountAmount == null ? 0 : shoppingCartItemModel.ItemDiscountAmount.Value,
                    ItemId = shoppingCartItemModel.ItemId,
                    ItemItemSpecsForDisplay = shoppingCartItemModel.ItemItemSpecsForDisplay,
                    ItemMasterDesc0 = shoppingCartItemModel.ItemMasterDesc0,
                    ItemMasterDesc1 = shoppingCartItemModel.ItemMasterDesc1,
                    ItemMasterDesc2 = shoppingCartItemModel.ItemMasterDesc2,
                    ItemMasterDesc3 = shoppingCartItemModel.ItemMasterDesc3,
                    ItemRate = shoppingCartItemModel.ItemRate == null ? 0 : shoppingCartItemModel.ItemRate.Value,
                    ItemRateBeforeDiscount = shoppingCartItemModel.ItemRateBeforeDiscount == null ? 0 : shoppingCartItemModel.ItemRateBeforeDiscount.Value,
                    ItemRateOriginal = shoppingCartItemModel.ItemRate == null ? 0 : shoppingCartItemModel.ItemRate.Value,
                    LengthValue = shoppingCartItemModel.LengthValue == null ? 0 : shoppingCartItemModel.LengthValue.Value,
                    OrderAmount = shoppingCartItemModel.OrderAmount == null ? 0 : shoppingCartItemModel.OrderAmount.Value,
                    OrderAmountBeforeDiscount = shoppingCartItemModel.OrderAmountBeforeDiscount == null ? 0 : shoppingCartItemModel.OrderAmountBeforeDiscount.Value,
                    OrderComments = shoppingCartItemModel.OrderComments,
                    OrderDetailTypeId = shoppingCartItemModel.OrderDetailTypeId,
                    OrderHeaderSummaryId = orderHeaderSummaryId,
                    OrderQty = shoppingCartItemModel.OrderQty == null ? 0 : shoppingCartItemModel.OrderQty.Value,
                    ParentItemId = shoppingCartItemModel.ParentItemId == null ? 0 : shoppingCartItemModel.ParentItemId.Value,
                    ProductCode = shoppingCartItemModel.ProductCode,
                    ProductOrVolumetricWeight = shoppingCartItemModel.ProductOrVolumetricWeight == null ? 0 : shoppingCartItemModel.ProductOrVolumetricWeight.Value,
                    ProductOrVolumetricWeightUnitId = shoppingCartItemModel.ProductOrVolumetricWeightUnitId == null ? 0 : shoppingCartItemModel.ProductOrVolumetricWeightUnitId.Value,
                    SeqNum = seqNum,
                    VolumeValue = shoppingCartItemModel.VolumeValue == null ? 0 : shoppingCartItemModel.VolumeValue.Value,
                    WeightCalcUnitId = shoppingCartItemModel.WeightCalcUnitId == null ? 0 : shoppingCartItemModel.WeightCalcUnitId.Value,
                    WeightCalcValue = shoppingCartItemModel.WeightCalcValue == null ? 0 : shoppingCartItemModel.WeightCalcValue.Value,
                    WeightUnitId = shoppingCartItemModel.WeightUnitId == null ? 0 : shoppingCartItemModel.WeightUnitId.Value,
                    WeightValue = shoppingCartItemModel.WeightValue == null ? 0 : shoppingCartItemModel.WeightValue.Value,
                    WidthValue = shoppingCartItemModel.WidthValue == null ? 0 : shoppingCartItemModel.WidthValue.Value,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return orderDetail;
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
        // PRIVATE: CreateOrderDetail
        private OrderDetail CreateOrderDetail(long orderHeaderSummaryId, float seqNum, ShoppingCartItemModel shoppingCartItemModel, ShoppingCartItemModel shoppingCartItemBundleModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                OrderDetail orderDetail = new OrderDetail
                {
                    ClientId = clientId,
                    DimensionUnitId = DimensionUnitEnum.Centimeter,
                    DiscountPercent = shoppingCartItemModel.ItemDiscountPercent == null ? 0 : shoppingCartItemModel.ItemDiscountPercent.Value,
                    DiscountPercentOriginal = shoppingCartItemModel.ItemDiscountPercent == null ? 0 : shoppingCartItemModel.ItemDiscountPercent.Value,
                    HeightValue = shoppingCartItemModel.HeightValue == null ? 0 : shoppingCartItemModel.HeightValue.Value,
                    HSNCode = shoppingCartItemModel.HSNCode,
                    ItemDiscountAmount = shoppingCartItemModel.ItemDiscountAmount == null ? 0 : shoppingCartItemModel.ItemDiscountAmount.Value,
                    ItemId = shoppingCartItemModel.ItemId,
                    ItemItemSpecsForDisplay = shoppingCartItemModel.ItemItemSpecsForDisplay,
                    ItemMasterDesc0 = shoppingCartItemModel.ItemMasterDesc0,
                    ItemMasterDesc1 = shoppingCartItemModel.ItemMasterDesc1,
                    ItemMasterDesc2 = shoppingCartItemModel.ItemMasterDesc2,
                    ItemMasterDesc3 = shoppingCartItemModel.ItemMasterDesc3,
                    ItemRate = shoppingCartItemModel.ItemRate == null ? 0 : shoppingCartItemModel.ItemRate.Value,
                    ItemRateBeforeDiscount = shoppingCartItemModel.ItemRateBeforeDiscount == null ? 0 : shoppingCartItemModel.ItemRateBeforeDiscount.Value,
                    ItemRateOriginal = shoppingCartItemModel.ItemRate == null ? 0 : shoppingCartItemModel.ItemRate.Value,
                    LengthValue = shoppingCartItemModel.LengthValue == null ? 0 : shoppingCartItemModel.LengthValue.Value,
                    OrderAmount = shoppingCartItemModel.OrderAmount == null ? 0 : shoppingCartItemModel.OrderAmount.Value,
                    OrderAmountBeforeDiscount = shoppingCartItemModel.OrderAmountBeforeDiscount == null ? 0 : shoppingCartItemModel.OrderAmountBeforeDiscount.Value,
                    OrderComments = shoppingCartItemModel.OrderComments,
                    OrderDetailTypeId = shoppingCartItemModel.OrderDetailTypeId,
                    OrderHeaderSummaryId = orderHeaderSummaryId,
                    OrderQty = shoppingCartItemModel.OrderQty == null ? 0 : shoppingCartItemModel.OrderQty.Value,
                    ParentItemId = shoppingCartItemModel.ParentItemId == null ? 0 : shoppingCartItemModel.ParentItemId.Value,
                    ProductCode = shoppingCartItemModel.ProductCode,
                    ProductOrVolumetricWeight = shoppingCartItemModel.ProductOrVolumetricWeight == null ? 0 : shoppingCartItemModel.ProductOrVolumetricWeight.Value,
                    ProductOrVolumetricWeightUnitId = shoppingCartItemModel.ProductOrVolumetricWeightUnitId == null ? 0 : shoppingCartItemModel.ProductOrVolumetricWeightUnitId.Value,
                    SeqNum = seqNum,
                    VolumeValue = shoppingCartItemModel.VolumeValue == null ? 0 : shoppingCartItemModel.VolumeValue.Value,
                    WeightCalcUnitId = shoppingCartItemModel.WeightCalcUnitId == null ? 0 : shoppingCartItemModel.WeightCalcUnitId.Value,
                    WeightCalcValue = shoppingCartItemModel.WeightCalcValue == null ? 0 : shoppingCartItemModel.WeightCalcValue.Value,
                    WeightUnitId = shoppingCartItemModel.WeightUnitId == null ? 0 : shoppingCartItemModel.WeightUnitId.Value,
                    WeightValue = shoppingCartItemModel.WeightValue == null ? 0 : shoppingCartItemModel.WeightValue.Value,
                    WidthValue = shoppingCartItemModel.WidthValue == null ? 0 : shoppingCartItemModel.WidthValue.Value,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return orderDetail;
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
        // PRIVATE: CreateOrderHeader
        private OrderHeader CreateOrderHeader(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var orderHeader = new OrderHeader
            {
                ClientId = clientId,
                CreatedForPersonId = createForSessionObject.PersonId,
                InvoiceTypeId = paymentInfoModel.OrderSummaryModel.InvoiceTypeId.Value,
                OrderDateTime = paymentInfoModel.OrderSummaryModel.OrderDateTime,
                OrderStatusId = (long)OrderStatusEnum.Open,
                PersonId = sessionObjectModel.PersonId,
                SaveThisAddress = true,//paymentInfoModel.OrderSummaryModel.SaveThisAddress,
            };
            return orderHeader;
        }
        // PRIVATE: CreateOrderHeaderSummary
        private OrderHeaderSummary CreateOrderHeaderSummary(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var orderHeaderSummary = new OrderHeaderSummary
            {
                ClientId = clientId,
                BalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value,
                InvoiceTypeId = paymentInfoModel.OrderSummaryModel.InvoiceTypeId.Value,
                //OrderHeaderId = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value,
                ShippingAndHandlingCharges = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalShippingAndHandlingChargesAmount,
                TotalAmountPaid = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value,
                TotalDiscountAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount.Value,
                TotalInvoiceAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value,
                TotalOrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value,
                TotalTaxAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalTaxAmount,
            };
            return orderHeaderSummary;
        }
        // PRIVATE: CreateOrderItemFiles
        private void CreateOrderItemFiles(string orderItemFilesPath, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long corpAcctId;
                string aspNetRoleName;
                ArchLibBL archLibBL = new ArchLibBL();
                List<CodeDataModel> corpAcctTypes = LookupCache.CodeDataModels.FindAll(x => x.CodeTypeId == 204);
                CodeDataModel corpAcctType;
                OrderItemFileModel orderItemFileModel = new OrderItemFileModel();
                Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs;
                foreach (var corpAcctModel in RetailSlnCache.CorpAcctModels)
                {
                    if (corpAcctModel.CorpAcctId == 0)
                    {
                        CreateOrderItemFiles("APPLADMN1", corpAcctModel.CorpAcctId.Value, orderItemFilesPath, sessionObjectModel, createForSessionObjectModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        CreateOrderItemFiles("DEFAULTROLE", corpAcctModel.CorpAcctId.Value, orderItemFilesPath, sessionObjectModel, createForSessionObjectModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    else
                    {
                        corpAcctType = corpAcctTypes.First(x => x.CodeDataNameId == (int)corpAcctModel.CorpAcctTypeId);
                        aspNetRoleName = corpAcctType.CodeDataDesc1;
                        aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                        if (aspNetRoleName != aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData)
                        {
                            aspNetRoleName = aspNetRoleKVPs[aspNetRoleName].KVPValueData;
                            aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                        }
                        corpAcctId = corpAcctModel.CorpAcctId.Value;
                        CreateOrderItemFiles(aspNetRoleName, corpAcctId, orderItemFilesPath, sessionObjectModel, createForSessionObjectModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                return;
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
        // PRIVATE: CreateOrderItemFiles
        private void CreateOrderItemFiles(string aspNetRoleName, long corpAcctId, string orderItemFilesPath, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            long parentCategoryId;
            string htmlString, parentCategoryDesc;
            ArchLibBL archLibBL = new ArchLibBL();
            Dictionary<long, List<CategoryItemMasterHierModel>> parentCategoryItemMasterModels;
            List<CategoryItemMasterHierModel> categoryCategoryItemMasterHierModels;
            List<CategoryItemMasterHierModel> categoryItemMasterHierModels;
            StreamWriter streamWriter;
            OrderItemFileModel orderItemFileModel = new OrderItemFileModel();
            categoryCategoryItemMasterHierModels = RetailSlnCache.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][0];
            foreach (var categoryCategoryItemMasterHierModel in categoryCategoryItemMasterHierModels)
            {
                parentCategoryId = categoryCategoryItemMasterHierModel.CategoryModel.CategoryId.Value;
                parentCategoryDesc = categoryCategoryItemMasterHierModel.CategoryModel.CategoryDesc;
                if (RetailSlnCache.AspNetRoleParentCategoryItemMasterModels.TryGetValue(aspNetRoleName, out parentCategoryItemMasterModels))
                {
                    if (parentCategoryItemMasterModels.TryGetValue(parentCategoryId, out categoryItemMasterHierModels))
                    {
                    }
                    else
                    {
                        categoryItemMasterHierModels = new List<CategoryItemMasterHierModel>();
                    }
                }
                else
                {
                    categoryItemMasterHierModels = new List<CategoryItemMasterHierModel>();
                }
                orderItemFileModel.CategoryCategoryItemMasterHierModels = categoryCategoryItemMasterHierModels;
                orderItemFileModel.CategoryItemMasterHierModels = categoryItemMasterHierModels;
                orderItemFileModel.ParentCategoryId = null;
                orderItemFileModel.ParentCategoryDesc = parentCategoryDesc;
                htmlString = archLibBL.ViewToHtmlString(controller, "_OrderItemFile", orderItemFileModel);
                streamWriter = new StreamWriter(orderItemFilesPath + $@"\OrderItem_{aspNetRoleName}_{corpAcctId}_{parentCategoryId}.html");
                streamWriter.Write(htmlString);
                streamWriter.Close();
            }
        }
        // PRIVATE : CreatePaymentInfoModel
        private void CreatePaymentInfoModel(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfoModel = paymentInfoModel ?? new PaymentInfoModel();
                paymentInfoModel.CreditCardDataModel = paymentInfoModel.CreditCardDataModel ?? new CreditCardDataModel();
                paymentInfoModel.OrderHeaderWIPModel = paymentInfoModel.OrderHeaderWIPModel ?? new OrderHeaderWIPModel();
                paymentInfoModel.OrderSummaryModel = paymentInfoModel.OrderSummaryModel ?? new OrderSummaryModel();
                paymentInfoModel.OrderSummaryModel.OrderDateTime = paymentInfoModel.OrderSummaryModel.OrderDateTime ?? DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                paymentInfoModel.OrderSummaryModel.InvoiceTypeId = paymentInfoModel.OrderSummaryModel.InvoiceTypeId ?? InvoiceTypeEnum.FinalInvoice;
                paymentInfoModel.ShoppingCartModel = paymentInfoModel.ShoppingCartModel ?? new ShoppingCartModel
                {
                    Checkout = false,
                    ShowDetail = false,
                };
                paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels ?? new List<ShoppingCartItemModel>();
                paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary ?? new List<ShoppingCartItemModel>
                {
                    new ShoppingCartItemModel
                    {
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
                    }
                };
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel ?? new ShoppingCartSummaryModel
                {
                    TotalItemsCount = 0,
                    TotalOrderAmount = 0,
                    TotalOrderAmountFormatted = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                    TotalOrderQtyCount = 0,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
        }
        #region
        //// PRIVATE : CreateShoppingCartItemModel
        //private void CreateShoppingCartItemModel(ref ShoppingCartItemModel shoppingCartItemModel, ItemModel itemModel, long? orderQty, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    long parentItemId;
        //    float? itemRate, itemRateBeforeDiscount, orderAmount, orderAmountBeforeDiscount;
        //    ItemDiscountModel itemDiscountModel;
        //    List<ShoppingCartItemModel> shoppingCartItemBundleModels;
        //    if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //    {
        //        parentItemId = itemModel.ItemId.Value;
        //        shoppingCartItemBundleModels = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].ShoppingCartItemBundleModels;
        //        itemRateBeforeDiscount = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].TotalOrderAmount;
        //    }
        //    else
        //    {
        //        parentItemId = 0;
        //        shoppingCartItemBundleModels = null;
        //        itemRateBeforeDiscount = itemModel.ItemRate;
        //    }
        //    RetailSlnCache.CorpAcctItemDiscountModels[corpAcctId].TryGetValue(itemModel.ItemId.Value, out itemDiscountModel);
        //    if (itemDiscountModel == null)
        //    {
        //        itemDiscountModel = new ItemDiscountModel { DiscountPercent = 0 };
        //    }
        //    itemRate = itemRateBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
        //    if (shoppingCartItemModel == null)
        //    {
        //        shoppingCartItemModel = new ShoppingCartItemModel
        //        {
        //            HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //            ItemDiscountAmount = itemDiscountModel.DiscountPercent * itemRateBeforeDiscount * orderQty / 100,
        //            ItemDiscountPercent = itemDiscountModel.DiscountPercent,
        //            ItemDiscountPercentFormatted = itemDiscountModel.DiscountPercent.ToString("#0.00") + "%",
        //            ItemId = itemModel.ItemId,
        //            ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
        //            ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
        //            ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
        //            ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
        //            ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
        //            ItemRate = itemRate,
        //            ItemRateBeforeDiscount = itemRateBeforeDiscount,
        //            ItemRateBeforeDiscountFormatted = itemRateBeforeDiscount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //            ItemRateFormatted = itemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //            ItemShortDesc = null,//itemModel.ItemShortDesc,
        //            ItemTypeId = itemModel.ItemTypeId.Value,
        //            OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //            ParentItemId = 0,//parentItemId,
        //            ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //            ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //            WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //            WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //            ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
        //        };
        //    }
        //    orderAmountBeforeDiscount = orderQty * itemRateBeforeDiscount;
        //    orderAmount = orderQty * itemRate;
        //    shoppingCartItemModel.OrderQty = orderQty;
        //    shoppingCartItemModel.OrderAmount = orderAmount;
        //    shoppingCartItemModel.OrderAmountBeforeDiscount = orderAmountBeforeDiscount;
        //    shoppingCartItemModel.OrderAmountFormatted = orderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //    shoppingCartItemModel.ProductOrVolumetricWeight = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
        //    shoppingCartItemModel.WeightCalcValue = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
        //    shoppingCartItemModel.WeightValue = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
        //    shoppingCartItemModel.ShoppingCartItemBundleModels = shoppingCartItemBundleModels;
        //    return;
        //}
        #endregion
        // PRIVATE : CreateShoppingCartItemModel
        private void CreateShoppingCartItemModel(ref ShoppingCartItemModel shoppingCartItemModel, AddToCartModel addToCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i;
            long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
            long orderQty, orderQtyForBundle, parentItemId;
            string orderQtyParm;
            float? itemRate, itemRateBeforeDiscount, orderAmount, orderAmountBeforeDiscount, productOrVolumetricWeight, weightCalcValue, weightValue;
            ItemModel itemModel = addToCartModel.ItemModel, itemModelForBundle;
            ItemDiscountModel itemDiscountModel;
            List<ShoppingCartItemModel> shoppingCartItemBundleModelsFromCache;
            long.TryParse(addToCartModel.OrderQtyParm, out orderQty);
            if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
            {
                if (addToCartModel.DoNotBreakBundle)
                {
                    parentItemId = 0;
                    shoppingCartItemBundleModelsFromCache = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].ShoppingCartItemBundleModels;
                    itemRateBeforeDiscount = itemModel.ItemRate;
                }
                else
                {
                    parentItemId = itemModel.ItemId.Value;
                    shoppingCartItemBundleModelsFromCache = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].ShoppingCartItemBundleModels;
                    itemRateBeforeDiscount = 0;
                }
            }
            else
            {
                parentItemId = 0;
                shoppingCartItemBundleModelsFromCache = null;
                itemRateBeforeDiscount = itemModel.ItemRate;
            }
            RetailSlnCache.CorpAcctItemDiscountModels[corpAcctId].TryGetValue(itemModel.ItemId.Value, out itemDiscountModel);
            if (itemDiscountModel == null)
            {
                itemDiscountModel = new ItemDiscountModel { DiscountPercent = 0 };
            }
            itemRate = itemRateBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
            if (shoppingCartItemModel == null)
            {
                productOrVolumetricWeight = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
                weightCalcValue = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                weightValue = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                shoppingCartItemModel = new ShoppingCartItemModel
                {
                    #region
                    ClientId = clientId,
                    HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
                    ImageName = itemModel.ItemMasterModel.ImageName,
                    ItemDiscountAmount = itemDiscountModel.DiscountPercent * itemRateBeforeDiscount * orderQty / 100,
                    ItemDiscountPercent = itemDiscountModel.DiscountPercent,
                    ItemDiscountPercentFormatted = itemDiscountModel.DiscountPercent.ToString("#0.00") + "%",
                    ItemId = itemModel.ItemId,
                    ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                    ItemMasterDesc = itemModel.ItemMasterModel.ItemMasterDesc,
                    ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
                    ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
                    ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
                    ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
                    ItemRateBeforeDiscount = itemRateBeforeDiscount,
                    ItemRateBeforeDiscountFormatted = itemRateBeforeDiscount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                    ItemRateFormatted = itemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                    ItemShortDesc = null,//itemModel.ItemShortDesc,
                    ItemTypeId = itemModel.ItemTypeId.Value,
                    OrderDetailTypeId = OrderDetailTypeEnum.Item,
                    OrderQty = orderQty,
                    ParentItemId = parentItemId,
                    ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
                    ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
                    WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                    WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                    ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                    #endregion
                };
                if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                {
                    if (!addToCartModel.DoNotBreakBundle)
                    {
                        itemRateBeforeDiscount = 0;
                        productOrVolumetricWeight = 0;
                        weightCalcValue = 0;
                        weightValue = 0;
                    }
                    #region
                    shoppingCartItemModel.ShoppingCartItemBundleModels = new List<ShoppingCartItemModel>();
                    orderQtyForBundle = 1;
                    orderQtyParm = "1";
                    for (i = 0; i < shoppingCartItemBundleModelsFromCache.Count; i++)
                    {
                        if (!addToCartModel.DoNotBreakBundle)
                        {
                            long.TryParse(addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm, out orderQtyForBundle);
                            orderQtyParm = addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm;
                        }
                        itemModelForBundle = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItemBundleModelsFromCache[i].ItemId);
                        #region Deep Copy 
                        shoppingCartItemModel.ShoppingCartItemBundleModels.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ClientId = shoppingCartItemBundleModelsFromCache[i].ClientId,
                                DimensionUnitId = shoppingCartItemBundleModelsFromCache[i].DimensionUnitId,
                                HeightValue = shoppingCartItemBundleModelsFromCache[i].HeightValue,
                                HSNCode = shoppingCartItemBundleModelsFromCache[i].HSNCode,
                                ImageName = shoppingCartItemBundleModelsFromCache[i].ImageName,
                                ItemDiscountAmount = 0,
                                ItemDiscountPercent = 0,
                                ItemDiscountPercentFormatted = "",
                                ItemId = shoppingCartItemBundleModelsFromCache[i].ItemId,//addToCartModel.ItemId,
                                ItemIdParm = shoppingCartItemBundleModelsFromCache[i].ItemId.ToString(),//addToCartModel.ItemIdParm,
                                ItemItemSpecsForDisplay = shoppingCartItemBundleModelsFromCache[i].ItemItemSpecsForDisplay,
                                ItemMasterDesc = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc,
                                ItemMasterDesc0 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc0,
                                ItemMasterDesc1 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc1,
                                ItemMasterDesc2 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc2,
                                ItemMasterDesc3 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc3,
                                ItemRate = shoppingCartItemBundleModelsFromCache[i].ItemRate,
                                ItemRateBeforeDiscount = shoppingCartItemBundleModelsFromCache[i].ItemRateBeforeDiscount,
                                ItemRateBeforeDiscountFormatted = shoppingCartItemBundleModelsFromCache[i].ItemRateBeforeDiscountFormatted,
                                ItemRateFormatted = shoppingCartItemBundleModelsFromCache[i].ItemRateFormatted,
                                ItemShortDesc = shoppingCartItemBundleModelsFromCache[i].ItemShortDesc,
                                ItemTypeId = shoppingCartItemBundleModelsFromCache[i].ItemTypeId,
                                LengthValue = shoppingCartItemBundleModelsFromCache[i].LengthValue,
                                OrderAmount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle,
                                OrderAmountBeforeDiscount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle,
                                OrderAmountFormatted = (shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle).Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo),
                                OrderAmountRounded = (long)(shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle * 100),
                                OrderComments = "",
                                OrderDetailTypeId = shoppingCartItemBundleModelsFromCache[i].OrderDetailTypeId,
                                OrderQty = orderQtyForBundle,
                                OrderQtyParm = orderQtyParm,
                                ParentItemId = addToCartModel.ItemId,//shoppingCartItemBundleModelsFromCache[i].ParentItemId,
                                ProductCode = shoppingCartItemBundleModelsFromCache[i].ProductCode,
                                ProductOrVolumetricWeight = shoppingCartItemBundleModelsFromCache[i].ProductOrVolumetricWeight,
                                ProductOrVolumetricWeightUnitId = shoppingCartItemBundleModelsFromCache[i].ProductOrVolumetricWeightUnitId,
                                ShoppingCartItemBundleModels = null,
                                ShoppingCartItemSummarys = null,
                                VolumeValue = shoppingCartItemBundleModelsFromCache[i].VolumeValue,
                                WeightCalcUnitId = shoppingCartItemBundleModelsFromCache[i].WeightUnitId,
                                WeightCalcValue = shoppingCartItemBundleModelsFromCache[i].WeightCalcValue,
                                WeightUnitId = shoppingCartItemBundleModelsFromCache[i].WeightUnitId,
                                WidthValue = shoppingCartItemBundleModelsFromCache[i].WidthValue,
                                WeightValue = shoppingCartItemBundleModelsFromCache[i].WidthValue,
                            }
                        );
                        if (!addToCartModel.DoNotBreakBundle)
                        {
                            itemRateBeforeDiscount += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount;
                            productOrVolumetricWeight += orderQtyForBundle * float.Parse(itemModelForBundle.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
                            weightCalcValue += orderQtyForBundle * float.Parse(itemModelForBundle.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                            weightValue += orderQtyForBundle * float.Parse(itemModelForBundle.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                        }
                        #endregion
                    }
                    itemRate = itemRateBeforeDiscount;
                    #endregion
                }
            }
            else
            {
                if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle && !addToCartModel.DoNotBreakBundle)
                {
                    itemRateBeforeDiscount = 0;
                    productOrVolumetricWeight = 0;
                    weightCalcValue = 0;
                    weightValue = 0;
                    for (i = 0; i < addToCartModel.ShoppingCartItemBundleModels.Count; i++)
                    {
                        long.TryParse(addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm, out orderQtyForBundle);
                        itemModelForBundle = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItemBundleModelsFromCache[i].ItemId);

                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQtyParm = addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm;
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty = orderQtyForBundle;
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle;
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountBeforeDiscount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle;
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountFormatted = (shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle).Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo);
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountRounded = (long)(shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQtyForBundle * 100);

                        itemRateBeforeDiscount += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount;
                        productOrVolumetricWeight += orderQtyForBundle * float.Parse(itemModelForBundle.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
                        weightCalcValue += orderQtyForBundle * float.Parse(itemModelForBundle.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                        weightValue += orderQtyForBundle * float.Parse(itemModelForBundle.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                    }
                    itemRate = itemRateBeforeDiscount;
                }
                else
                {
                    orderQty = shoppingCartItemModel.OrderQty.Value + orderQty;
                    shoppingCartItemModel.OrderQty = orderQty;
                    productOrVolumetricWeight = shoppingCartItemModel.OrderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
                    weightCalcValue = shoppingCartItemModel.OrderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                    weightValue = shoppingCartItemModel.OrderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                }
            }
            shoppingCartItemModel.ItemRate = itemRate;
            orderAmountBeforeDiscount = orderQty * itemRateBeforeDiscount;
            orderAmount = orderQty * itemRate;
            shoppingCartItemModel.OrderAmount = orderAmount;
            shoppingCartItemModel.OrderAmountBeforeDiscount = orderAmountBeforeDiscount;
            shoppingCartItemModel.OrderAmountFormatted = orderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            shoppingCartItemModel.OrderAmountRounded = (long)(shoppingCartItemModel.OrderAmount * 100);
            shoppingCartItemModel.ProductOrVolumetricWeight = productOrVolumetricWeight;
            shoppingCartItemModel.WeightCalcValue = weightCalcValue;
            shoppingCartItemModel.WeightValue = weightValue;
            return;
        }
        // PRIVATE : CreateShoppingCartItemModel
        //private void CreateShoppingCartItemModelNew(ref ShoppingCartItemModel shoppingCartItemModel, AddToCartModel addToCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    int i;
        //    long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    long orderQty, parentItemId;
        //    float? itemRate, itemRateBeforeDiscount, orderAmount, orderAmountBeforeDiscount;
        //    ItemDiscountModel itemDiscountModel;
        //    List<ShoppingCartItemModel> shoppingCartItemBundleModelsFromCache;
        //    if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //    {
        //        parentItemId = addToCartModel.ItemModel.ItemId.Value;
        //        shoppingCartItemModel.ShoppingCartItemBundleModels = new List<ShoppingCartItemModel>();
        //        shoppingCartItemBundleModelsFromCache = RetailSlnCache.ParentItemBundleModels[addToCartModel.ItemModel.ItemId.Value].ShoppingCartItemBundleModels;
        //        RetailSlnCache.CorpAcctItemDiscountModels[corpAcctId].TryGetValue(addToCartModel.ItemModel.ItemId.Value, out itemDiscountModel);
        //        if (itemDiscountModel == null)
        //        {
        //            itemDiscountModel = new ItemDiscountModel { DiscountPercent = 0 };
        //        }
        //        itemRate = itemRateBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
        //        if (shoppingCartItemModel == null)
        //        {
        //            shoppingCartItemModel = new ShoppingCartItemModel
        //            {
        //                HSNCode = addToCartModel.ItemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //                ItemDiscountAmount = itemDiscountModel.DiscountPercent * itemRateBeforeDiscount * addToCartModel.OrderQty / 100,
        //                ItemDiscountPercent = itemDiscountModel.DiscountPercent,
        //                ItemDiscountPercentFormatted = itemDiscountModel.DiscountPercent.ToString("#0.00") + "%",
        //                ItemId = addToCartModel.ItemModel.ItemId,
        //                ItemItemSpecsForDisplay = addToCartModel.ItemModel.ItemItemSpecsForDisplay,
        //                ItemMasterDesc0 = addToCartModel.ItemModel.ItemMasterModel.ItemMasterDesc0,
        //                ItemMasterDesc1 = addToCartModel.ItemModel.ItemMasterModel.ItemMasterDesc1,
        //                ItemMasterDesc2 = addToCartModel.ItemModel.ItemMasterModel.ItemMasterDesc2,
        //                ItemMasterDesc3 = addToCartModel.ItemModel.ItemMasterModel.ItemMasterDesc3,
        //                ItemRate = itemRate,
        //                ItemRateBeforeDiscount = itemRateBeforeDiscount,
        //                ItemRateBeforeDiscountFormatted = itemRateBeforeDiscount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //                ItemRateFormatted = itemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //                ItemShortDesc = null,//itemModel.ItemShortDesc,
        //                ItemTypeId = addToCartModel.ItemModel.ItemTypeId.Value,
        //                OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //                ParentItemId = parentItemId,
        //                ProductCode = addToCartModel.ItemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(addToCartModel.ItemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //                WeightCalcUnitId = (WeightUnitEnum)int.Parse(addToCartModel.ItemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //                WeightUnitId = (WeightUnitEnum)int.Parse(addToCartModel.ItemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //                ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
        //            };
        //        }
        //        orderAmountBeforeDiscount = addToCartModel.OrderQty * itemRateBeforeDiscount;
        //        orderAmount = addToCartModel.OrderQty * itemRate;
        //        itemRateBeforeDiscount = 0;
        //        if (shoppingCartItemModel.ShoppingCartItemBundleModels.Count == 0)
        //        {
        //            for (i = 0; i < shoppingCartItemBundleModelsFromCache.Count; i++)
        //            {
        //                long.TryParse(addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm, out orderQty);
        //                #region Deep Copy 
        //                shoppingCartItemModel.ShoppingCartItemBundleModels.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        ClientId = shoppingCartItemBundleModelsFromCache[i].ClientId,
        //                        DimensionUnitId = shoppingCartItemBundleModelsFromCache[i].DimensionUnitId,
        //                        HeightValue = shoppingCartItemBundleModelsFromCache[i].HeightValue,
        //                        HSNCode = shoppingCartItemBundleModelsFromCache[i].HSNCode,
        //                        ItemDiscountAmount = 0,
        //                        ItemDiscountPercent = 0,
        //                        ItemDiscountPercentFormatted = "",
        //                        ItemId = addToCartModel.ItemId,
        //                        ItemIdParm = addToCartModel.ItemIdParm,
        //                        ItemItemSpecsForDisplay = shoppingCartItemBundleModelsFromCache[i].ItemItemSpecsForDisplay,
        //                        ItemMasterDesc0 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc0,
        //                        ItemMasterDesc1 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc1,
        //                        ItemMasterDesc2 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc2,
        //                        ItemMasterDesc3 = shoppingCartItemBundleModelsFromCache[i].ItemMasterDesc3,
        //                        ItemRate = shoppingCartItemBundleModelsFromCache[i].ItemRate,
        //                        ItemRateBeforeDiscount = shoppingCartItemBundleModelsFromCache[i].ItemRateBeforeDiscount,
        //                        ItemRateBeforeDiscountFormatted = shoppingCartItemBundleModelsFromCache[i].ItemRateBeforeDiscountFormatted,
        //                        ItemRateFormatted = shoppingCartItemBundleModelsFromCache[i].ItemRateFormatted,
        //                        ItemShortDesc = shoppingCartItemBundleModelsFromCache[i].ItemShortDesc,
        //                        ItemTypeId = shoppingCartItemBundleModelsFromCache[i].ItemTypeId,
        //                        LengthValue = shoppingCartItemBundleModelsFromCache[i].LengthValue,
        //                        OrderAmount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty,
        //                        OrderAmountBeforeDiscount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty,
        //                        OrderAmountFormatted = (shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty).Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo),
        //                        OrderAmountRounded = (long)(shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty * 100),
        //                        OrderComments = "",
        //                        OrderDetailTypeId = shoppingCartItemBundleModelsFromCache[i].OrderDetailTypeId,
        //                        OrderQty = orderQty,
        //                        OrderQtyParm = addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm,
        //                        ParentItemId = shoppingCartItemBundleModelsFromCache[i].ParentItemId,
        //                        ProductCode = shoppingCartItemBundleModelsFromCache[i].ProductCode,
        //                        ProductOrVolumetricWeight = shoppingCartItemBundleModelsFromCache[i].ProductOrVolumetricWeight,
        //                        ProductOrVolumetricWeightUnitId = shoppingCartItemBundleModelsFromCache[i].ProductOrVolumetricWeightUnitId,
        //                        ShoppingCartItemBundleModels = null,
        //                        ShoppingCartItemSummarys = null,
        //                        VolumeValue = shoppingCartItemBundleModelsFromCache[i].VolumeValue,
        //                        WeightCalcUnitId = shoppingCartItemBundleModelsFromCache[i].WeightUnitId,
        //                        WeightCalcValue = shoppingCartItemBundleModelsFromCache[i].WeightCalcValue,
        //                        WeightUnitId = shoppingCartItemBundleModelsFromCache[i].WeightUnitId,
        //                        WidthValue = shoppingCartItemBundleModelsFromCache[i].WidthValue,
        //                        WeightValue = shoppingCartItemBundleModelsFromCache[i].WidthValue,
        //                    }
        //                );
        //                itemRateBeforeDiscount += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount;
        //                #endregion
        //            }
        //        }
        //        else
        //        {//Update the shopping cart
        //            for (i = 0; i < addToCartModel.ShoppingCartItemBundleModels.Count; i++)
        //            {
        //                long.TryParse(addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm, out orderQty);
        //                shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty = orderQty;
        //                shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQtyParm = addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm;
        //                shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty;
        //                shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountBeforeDiscount = shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty;
        //                shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountFormatted = (shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty).Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo);
        //                shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountRounded = (long)(shoppingCartItemBundleModelsFromCache[i].ItemRate * orderQty * 100);
        //                itemRateBeforeDiscount += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        parentItemId = 0;
        //        shoppingCartItemBundleModelsFromCache = null;
        //        itemRateBeforeDiscount = addToCartModel.ItemModel.ItemRate;
        //    }
        //    shoppingCartItemModel.OrderQty = addToCartModel.OrderQty;
        //    shoppingCartItemModel.OrderAmount = orderAmount;
        //    shoppingCartItemModel.OrderAmountBeforeDiscount = orderAmountBeforeDiscount;
        //    shoppingCartItemModel.OrderAmountFormatted = orderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //    shoppingCartItemModel.ProductOrVolumetricWeight = addToCartModel.OrderQty * float.Parse(addToCartModel.ItemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
        //    shoppingCartItemModel.WeightCalcValue = addToCartModel.OrderQty * float.Parse(addToCartModel.ItemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
        //    shoppingCartItemModel.WeightValue = addToCartModel.OrderQty * float.Parse(addToCartModel.ItemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
        //    shoppingCartItemModel.ShoppingCartItemBundleModels = shoppingCartItemBundleModelsFromCache;
        //    return;
        //}
        // PRIVATE : CreateShoppingCartSummary
        private void CreateShoppingCartTotals(ref PaymentInfoModel paymentInfoModel, float amountPaid, string amountPaidComments, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalTaxAmount = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalShippingAndHandlingChargesAmount = 0;
                float totalInvoiceAmount = 0, totalDiscountAmount = 0;
                foreach (ShoppingCartItemModel shoppingCartItemModelSummaryTemp in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary)
                {
                    if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount)
                    {
                        totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
                    }
                    if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.Discount)
                    {
                        totalDiscountAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
                        totalInvoiceAmount += totalDiscountAmount;
                    }
                    if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.SalesTaxAmount)
                    {
                        totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
                        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalTaxAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
                    }
                    if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.ShippingHandlingCharges)
                    {
                        totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
                        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalShippingAndHandlingChargesAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
                    }
                }
                ShoppingCartItemModel shoppingCartItemModelSummary;
                shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalInvoiceAmount);
                if (shoppingCartItemModelSummary == null)
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                    (
                        shoppingCartItemModelSummary = new ShoppingCartItemModel
                        {
                            ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalInvoiceAmount).CodeDataDesc0,
                            OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
                        }
                    );
                }
                totalInvoiceAmount = (int)Math.Round(totalInvoiceAmount, RetailSlnCache.RoundingDigitCount);
                shoppingCartItemModelSummary.OrderAmount = totalInvoiceAmount;
                shoppingCartItemModelSummary.OrderAmountFormatted = totalInvoiceAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = shoppingCartItemModelSummary.OrderAmount;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountFormatted = shoppingCartItemModelSummary.OrderAmountFormatted;
                shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                if (shoppingCartItemModelSummary == null)
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                    (
                        shoppingCartItemModelSummary = new ShoppingCartItemModel
                        {
                            ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalAmountPaid).CodeDataDesc0,
                            OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
                        }
                    );
                }
                shoppingCartItemModelSummary.OrderAmount = amountPaid;
                shoppingCartItemModelSummary.OrderAmountFormatted = amountPaid.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModelSummary.OrderComments = amountPaidComments;
                shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                if (shoppingCartItemModelSummary == null)
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
                    (
                        shoppingCartItemModelSummary = new ShoppingCartItemModel
                        {
                            ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
                            OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                        }
                    );
                }
                shoppingCartItemModelSummary.OrderAmount = totalInvoiceAmount - amountPaid;
                shoppingCartItemModelSummary.OrderAmountFormatted = (totalInvoiceAmount - amountPaid).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = totalInvoiceAmount - amountPaid;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = (totalInvoiceAmount - amountPaid).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = totalInvoiceAmount;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = amountPaid;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
        }
        // PRIVATE : GetCreditCardKVPs
        private Dictionary<string, string> GetCreditCardKVPs(string creditCardProcessor, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            creditCardProcessor = creditCardProcessor.ToUpper();
            Dictionary<string, string> creditCardKVPs;
            switch (creditCardProcessor)
            {
                case "NUVEIPROD":
                case "NUVEITEST":
                    creditCardKVPs = new Dictionary<string, string>
                    {
                        { "PrivateKey", ArchLibCache.GetPrivateKey(clientId) },
                        { "NuveiRestAPIRootUri", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "RestAPIRootUri") },
                        { "NuveiRequestUri", ArchLibCache.GetApplicationDefault(clientId,creditCardProcessor, "RequestUri") },
                        { "NuveiTerminalId", ArchLibCache.GetApplicationDefault(clientId,creditCardProcessor, "TerminalId") },
                        { "NuveiSharedSecret", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "SharedSecret") },
                    };
                    break;
                case "RAZORPAYTEST":
                case "RAZORPAYPROD":
                    creditCardKVPs = new Dictionary<string, string>
                    {
                        { "PrivateKey", ArchLibCache.GetPrivateKey(clientId) },
                        { "ApiKey", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "ApiKey") },
                        { "ApiSecret", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "ApiSecret") },
                    };
                    break;
                default:
                    creditCardKVPs = new Dictionary<string, string>();
                    var applicationDefaultModels = ArchLibCache.ApplicationDefaultModels.FindAll(x => x.ClientId == clientId && x.KVPKey == creditCardProcessor && x.SeqNum <= 20);
                    foreach (var applicationDefaultModel in applicationDefaultModels)
                    {
                        creditCardKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
                    }
                    break;
            }

            return creditCardKVPs;
        }
        // PRIVATE : GetCreditCardKVPs
        private void GetCreditCardKVPs(string creditCardProcessor, out Dictionary<string, string> creditCardKVPs, out Dictionary<string, string> creditCardDataKVPs, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            creditCardProcessor = creditCardProcessor.ToUpper();
            creditCardKVPs = new Dictionary<string, string>();
            creditCardDataKVPs = new Dictionary<string, string>();
            foreach (var applicationDefaultModel in ArchLibCache.ApplicationDefaultModels)
            {
                if (applicationDefaultModel.KVPKey == creditCardProcessor + "CLIENT")
                {
                    creditCardKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
                }
                else
                {
                    if (applicationDefaultModel.KVPKey == creditCardProcessor + "SERVER")
                    {
                        creditCardDataKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
                    }
                }
            }
            return;
        }
        #region
        //// PRIVATE: OrderWIPAdd
        //private void OrderWIPAdd(ref PaymentInfoModel paymentInfoModel, long corpAcctLocationId, ItemModel itemModel, long orderQty, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        if (sessionObjectModel == null || createForSessionObject == null)
        //        {
        //        }
        //        else
        //        {
        //            if (paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId == null)
        //            {
        //                //Create Order Header WIP
        //                paymentInfoModel.OrderHeaderWIPModel = OrderHeaderWIPModelCreate(corpAcctLocationId, paymentInfoModel.OrderSummaryModel.InvoiceTypeId, paymentInfoModel.OrderSummaryModel.OrderDateTime, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                ApplicationDataContext.OrderHeaderWIPAdd(paymentInfoModel.OrderHeaderWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            OrderDetailWIPModel orderDetailWIPModel;
        //            paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.Add
        //            (
        //                orderDetailWIPModel = new OrderDetailWIPModel
        //                {
        //                    ClientId = clientId,
        //                    ItemId = itemModel.ItemId.Value,
        //                    ItemRate = itemModel.ItemRate.Value,
        //                    OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
        //                    OrderQty = orderQty,
        //                    ParentItemId = 0,
        //                    SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
        //                }
        //            );
        //            ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            //if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //            //{
        //            //    foreach (var shoppingCartItemBundleModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.First(x => x.ItemId == itemModel.ItemId).ShoppingCartItemBundleModels)
        //            //    {
        //            //        orderDetailWIPModel = new OrderDetailWIPModel
        //            //        {
        //            //            ClientId = clientId,
        //            //            ItemId = shoppingCartItemBundleModel.ItemId.Value,
        //            //            ItemRate = shoppingCartItemBundleModel.ItemRate.Value,
        //            //            OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
        //            //            OrderQty = orderQty,
        //            //            ParentItemId = itemModel.ItemId.Value,
        //            //            SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
        //            //        };
        //            //        ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            //    }
        //            //}
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        // PRIVATE: OrderWIPAdd
        private void OrderWIPAdd(ref PaymentInfoModel paymentInfoModel, long corpAcctLocationId, AddToCartModel addToCartModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (sessionObjectModel == null || createForSessionObject == null)
                {
                }
                else
                {
                    if (paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId == null)
                    {
                        //Create Order Header WIP
                        paymentInfoModel.OrderHeaderWIPModel = OrderHeaderWIPModelCreate(corpAcctLocationId, paymentInfoModel.OrderSummaryModel.InvoiceTypeId, paymentInfoModel.OrderSummaryModel.OrderDateTime, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.OrderHeaderWIPAdd(paymentInfoModel.OrderHeaderWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    OrderDetailWIPModel orderDetailWIPModel;
                    paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.Add
                    (
                        orderDetailWIPModel = new OrderDetailWIPModel
                        {
                            ClientId = clientId,
                            ItemId = addToCartModel.ItemModel.ItemId.Value,
                            ItemRate = addToCartModel.ItemModel.ItemRate.Value,
                            OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
                            OrderQty = addToCartModel.OrderQty,
                            ParentItemId = 0,
                            SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
                        }
                    );
                    ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                    //{
                    //    foreach (var shoppingCartItemBundleModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.First(x => x.ItemId == itemModel.ItemId).ShoppingCartItemBundleModels)
                    //    {
                    //        orderDetailWIPModel = new OrderDetailWIPModel
                    //        {
                    //            ClientId = clientId,
                    //            ItemId = shoppingCartItemBundleModel.ItemId.Value,
                    //            ItemRate = shoppingCartItemBundleModel.ItemRate.Value,
                    //            OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
                    //            OrderQty = orderQty,
                    //            ParentItemId = itemModel.ItemId.Value,
                    //            SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
                    //        };
                    //        ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //    }
                    //}
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
        // PRIVATE : OrderHeaderWIPModelCreate
        private OrderHeaderWIPModel OrderHeaderWIPModelCreate(long corpAcctLocationId, InvoiceTypeEnum? invoiceTypeId, string orderDateTime, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            OrderHeaderWIPModel orderHeaderWIPModel = new OrderHeaderWIPModel
            {
                ClientId = clientId,
                CorpAcctLocationId = corpAcctLocationId,
                CreatedForPersonId = createForSessionObject.PersonId,
                InvoiceTypeId = (long)invoiceTypeId,
                MaxSeqNum = 0,
                OrderDateTime = orderDateTime,
                OrderStatusId = null,
                PersonId = sessionObjectModel.PersonId,
                OrderDetailWIPModels = new List<OrderDetailWIPModel>()
            };
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return orderHeaderWIPModel;
        }
        // PRIVATE : OrderDetailWIPDel
        private void OrderDetailWIPDel(long orderDetailWIPId, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OrderDetailWIPDel(orderDetailWIPId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
        #region
        //// PRIVATE : OrderDetailWIPUpdate
        //private void OrderDetailWIPUpdate(ref PaymentInfoModel paymentInfoModel, long itemId, long orderQty, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        OrderDetailWIPModel orderDetailWIPModel = paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.First(x => x.ItemId == itemId);
        //        orderDetailWIPModel.OrderQty = orderQty;
        //        ApplicationDataContext.OrderDetailWIPUpd(orderDetailWIPModel.OrderHeaderWIPId, itemId, orderQty, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        // PRIVATE : OrderDetailWIPUpdate
        private void OrderDetailWIPUpdate(ref PaymentInfoModel paymentInfoModel, ShoppingCartItemModel shoppingCartItemModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                OrderDetailWIPModel orderDetailWIPModel = paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.First(x => x.ItemId == shoppingCartItemModel.ItemId);
                orderDetailWIPModel.OrderQty = shoppingCartItemModel.OrderQty.Value;
                ApplicationDataContext.OrderDetailWIPUpd(orderDetailWIPModel.OrderHeaderWIPId, shoppingCartItemModel.ItemId.Value, shoppingCartItemModel.OrderQty.Value, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
        // PRIVATE : OrderWIPDel
        private void OrderWIPDel(PaymentInfoModel paymentInfoModel, SqlConnection sqlConnection, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OrderDetailWIPsDelete(sessionObjectModel.PersonId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.OrderHeaderWIPDelete(sessionObjectModel.PersonId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        //PRIVATE : GetCorpAcctId
        private long GetCorpAcctId(Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            long? corpAcctId = ((ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel)?.CorpAcctModel.CorpAcctId;
            if (corpAcctId == null)
            {
                corpAcctId = 0;
            }
            return corpAcctId.Value;
        }
        // PRIVATE : DeliveryChargeModel
        private DeliveryChargeModel GetDeliveryChargeModel(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShippingService shippingService = new ShippingService();
            DeliveryChargeModel deliveryChargeModel;
            DemogInfoAddressModel demogInfoAddressModel = new DemogInfoAddressModel
            {
                DemogInfoCountryId = paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId,
                DemogInfoSubDivisionId = paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId,
                DemogInfoCountyId = paymentInfoModel.DeliveryAddressModel.DemogInfoCountyId,
                DemogInfoCityId = paymentInfoModel.DeliveryAddressModel.DemogInfoCityId,
                DemogInfoZipId = paymentInfoModel.DeliveryAddressModel.DemogInfoZipId,
            };
            ShippingInputModel shippingInputModel = new ShippingInputModel
            {
                DestDemogInfoAddressModel = demogInfoAddressModel,
                SrceDemogInfoAddressModel = new DemogInfoAddressModel
                {
                    DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                },
                ShippingInputData = null,
            };
            deliveryChargeModel = (DeliveryChargeModel)shippingService.GetRate(shippingInputModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            return deliveryChargeModel;
        }
        // PRIVATE : GetSalesTaxListModels
        private List<SalesTaxListModel> GetSalesTaxListModels(DemogInfoAddressModel demogInfoAddressModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                 && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
                 && x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId
                 && demogInfoAddressModel.DemogInfoZipId == x.DestDemogInfoZipId
                );
            if (!salesTaxListModels.Any())
            {
                salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                    && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
                    && x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId
                    && x.DestDemogInfoZipId == null
                );
            }
            if (!salesTaxListModels.Any())
            {
                salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                    && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
                    && x.DestDemogInfoSubDivisionId == null
                    && x.DestDemogInfoZipId == null
                );
            }
            return salesTaxListModels;
        }
        // PRIVATE : UpdateDeliveryAddressInfo
        private void UpdateDeliveryAddressInfo(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibBL archLibBL = new ArchLibBL();
            DemogInfoAddressModel demogInfoAddressModel = paymentInfoModel.DeliveryAddressModel;
            SearchDataModel searchDataModel = new SearchDataModel
            {
                SearchType = "ZipCode",
                SearchKeyValuePairs = new Dictionary<string, string>
                {
                    { "DemogInfoCountryId", demogInfoAddressModel.DemogInfoCountryId.ToString() },
                    { "ZipCode", demogInfoAddressModel.ZipCode },
                },
            };
            List<Dictionary<string, string>> sqlQueryResults = archLibBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            foreach (var sqlQueryResult in sqlQueryResults)
            {
                if (
                    sqlQueryResult["DemogInfoCountryId"] == demogInfoAddressModel.DemogInfoCountryId.ToString()
                    && sqlQueryResult["ZipCode"] == demogInfoAddressModel.ZipCode
                   )
                {
                    demogInfoAddressModel.CityName = sqlQueryResult["CityName"];
                    demogInfoAddressModel.CountryAbbrev = sqlQueryResult["CountryAbbrev"];
                    demogInfoAddressModel.CountryDesc = sqlQueryResult["CountryDesc"];
                    demogInfoAddressModel.CountyName = sqlQueryResult["CountyName"];
                    demogInfoAddressModel.DemogInfoCityId = long.Parse(sqlQueryResult["DemogInfoCityId"]);
                    demogInfoAddressModel.DemogInfoCountyId = long.Parse(sqlQueryResult["DemogInfoCountyId"]);
                    demogInfoAddressModel.DemogInfoSubDivisionId = long.Parse(sqlQueryResult["DemogInfoSubDivisionId"]);
                    demogInfoAddressModel.DemogInfoZipId = long.Parse(sqlQueryResult["DemogInfoZipId"]);
                    demogInfoAddressModel.DemogInfoZipPlusId = long.Parse(sqlQueryResult["DemogInfoZipPlusId"]);
                    demogInfoAddressModel.StateAbbrev = sqlQueryResult["StateAbbrev"];
                    break;
                }
            }
            DemogInfoCountryModel demogInfoCountryModel;
            try
            {
                demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryDataModel.AlternateTelephoneDemogInfoCountryId);
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneFormatted = "+" + paymentInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(paymentInfoModel.DeliveryDataModel.AlternateTelephoneNum).ToString("##### #####");
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneHref = paymentInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(paymentInfoModel.DeliveryDataModel.AlternateTelephoneNum).ToString("###-###-####");
            }
            catch
            {
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = null;
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneFormatted = null;
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = null;
                paymentInfoModel.DeliveryDataModel.AlternateTelephoneHref = null;
            }
            try
            {
                demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.FirstOrDefault(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId);
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted = "+" + paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum).ToString("##### #####");
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneHref = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum).ToString("###-###-####");
            }
            catch
            {
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = null;
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted = null;
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = null;
                paymentInfoModel.DeliveryDataModel.PrimaryTelephoneHref = null;
            }

            demogInfoAddressModel.BuildingTypeDesc = demogInfoAddressModel.BuildingTypeId == null ? "" : LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "BuildingType").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)demogInfoAddressModel.BuildingTypeId).CodeDataNameDesc;
            demogInfoAddressModel.BuildingTypeHouseNumber = string.IsNullOrWhiteSpace(demogInfoAddressModel.BuildingTypeDesc) ? "" : (demogInfoAddressModel.BuildingTypeDesc + " ");
            demogInfoAddressModel.BuildingTypeHouseNumber += string.IsNullOrWhiteSpace(demogInfoAddressModel.HouseNumber) ? "" : demogInfoAddressModel.HouseNumber.Trim();

            paymentInfoModel.DeliveryMethodModel.DeliveryMethodDesc = paymentInfoModel.DeliveryMethodModel.DeliveryMethodId == null ? "" : LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)paymentInfoModel.DeliveryMethodModel.DeliveryMethodId).CodeDataDesc0;

            if (paymentInfoModel.PaymentModeModel.PaymentModeId == null)
            {
                paymentInfoModel.PaymentModeModel.PaymentModeDesc = "";
                paymentInfoModel.PaymentModeModel.PaymentModeDesc1 = "";
            }
            else
            {
                var codeDataModel = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)paymentInfoModel.PaymentModeModel.PaymentModeId);
                if (codeDataModel != null)
                {
                    paymentInfoModel.PaymentModeModel.PaymentModeDesc = codeDataModel.CodeDataDesc0;
                    paymentInfoModel.PaymentModeModel.PaymentModeDesc1 = codeDataModel.CodeDataDesc2;
                }
                else
                {
                    paymentInfoModel.PaymentModeModel.PaymentModeDesc = "";
                    paymentInfoModel.PaymentModeModel.PaymentModeDesc1 = "";
                }
            }
        }
        #endregion

        #region Commented Code
        #region
        //// PRIVATE: CreateOrderDetailItemBundleItem
        //private OrderDetailItemBundle CreateOrderDetailItemBundleItem(long orderDetailId, float seqNum, ShoppingCartItemModel shoppingCartItemBundleModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    OrderDetailItemBundle orderDetailItemBundle = new OrderDetailItemBundle
        //    {
        //        ClientId = clientId,
        //        //DiscountPercent = itemBundleModel.DiscountPercent,
        //        //ItemBundleId = shoppingCartItemBundleModel.ItemBundleId,
        //        //ItemBundleItemId = shoppingCartItemBundleModel.ItemBundleItemId,
        //        //ItemId = shoppingCartItemBundleModel.ItemId,
        //        //ItemMasterDesc = ItemMasterDesc,
        //        //ItemRate = itemRate,
        //        //ItemRateBeforeDiscount = itemRateBeforeDiscount,
        //        //OrderAmount = orderAmount,
        //        //OrderAmountBeforeDiscount = orderAmountBeforeDiscount,
        //        //OrderDetailId = orderDetailId,
        //        //OrderQty = orderQty,
        //        //SeqNum = seqNum,
        //    };
        //    return orderDetailItemBundle;
        //}
        #endregion
        #region
        //// POST: AddToCart2
        //public void AddToCart2(ref PaymentInfoModel paymentInfo1Model, ShoppingCartItemModel shoppingCartItemModelTemp, ShoppingCartBundleModel shoppingCartBundleModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfo1Model = paymentInfo1Model ?? new PaymentInfoModel();
        //        long itemId, orderQty;
        //        string orderComments;
        //        ItemModel itemModel;
        //        ShoppingCartItemModel shoppingCartItemModel;
        //        paymentInfo1Model.ShoppingCartModel = paymentInfo1Model.ShoppingCartModel ?? new ShoppingCartModel
        //        {
        //            ShoppingCartItems = new List<ShoppingCartItemModel>(),
        //            ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        //            {
        //                new ShoppingCartItemModel
        //                {
        //                    ItemId = null,
        //                    ItemRate = null,
        //                    ItemRateBeforeDiscount = null,
        //                    ItemShortDesc = null,
        //                    OrderAmount = null,
        //                    OrderAmountBeforeDiscount = null,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //                },
        //            },
        //        };
        //        itemId = shoppingCartItemModelTemp.ItemId.Value;
        //        orderQty = shoppingCartItemModelTemp.OrderQty.Value;
        //        orderComments = shoppingCartItemModelTemp.OrderComments;
        //        itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
        //        List<ShoppingCartItemModel> shoppingCartItemModels;
        //        if (shoppingCartBundleModel != null)
        //        {//When adding a bundle always add it as a new line item in the cart
        //            shoppingCartItemModels = CalculateBundleOrderAmount(shoppingCartBundleModel, out float itemRateBeforeDiscount, out float discountPercent, out float itemRate, out float orderAmount, out float orderAmountBeforeDiscount, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            shoppingCartItemModel = CreateShoppingCartItemModel(itemModel, itemRateBeforeDiscount, discountPercent, itemRate, orderQty, orderAmountBeforeDiscount, orderAmount, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            shoppingCartItemModel.ShoppingCartItemBundleItemModels = shoppingCartItemModels;
        //            //shoppingCartItemModel.OrderAmount = orderAmount;
        //            //shoppingCartItemModel.OrderAmountBeforeDiscount = orderAmountBeforeDiscount;
        //        }
        //        else
        //        {
        //            shoppingCartItemModel = paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        //            if (shoppingCartItemModel == null)
        //            {
        //                shoppingCartItemModel = CreateShoppingCartItemModel(itemModel, orderQty, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            else
        //            {
        //                shoppingCartItemModel.OrderQty += orderQty;
        //            }
        //            //shoppingCartItemModel.ItemRate = itemModel.ItemRate;
        //            //shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
        //            //shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
        //        }
        //        paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.Add(shoppingCartItemModel);
        //        shoppingCartItemModel.OrderComments = orderComments;
        //        shoppingCartItemModel.VolumeValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        //        shoppingCartItemModel.WeightCalcValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightCalcValue;
        //        shoppingCartItemModel.WeightValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightValue;
        //        shoppingCartItemModel.ProductOrVolumetricWeight = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
        //        UpdateShoppingCart(paymentInfo1Model.ShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #region
        //// POST: AddToCart2
        //public void AddToCart2(ref PaymentInfo1Model paymentInfo1Model, ShoppingCartItemModel shoppingCartItemModelTemp, ShoppingCartBundleModel shoppingCartBundleModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfo1Model = paymentInfo1Model ?? new PaymentInfo1Model();
        //        long itemId, orderQty;
        //        string orderComments;
        //        ItemModel itemModel;
        //        ShoppingCartItemModel shoppingCartItemModel;
        //        paymentInfo1Model.ShoppingCartModel = paymentInfo1Model.ShoppingCartModel ?? new ShoppingCartModel
        //        {
        //            ShoppingCartItems = new List<ShoppingCartItemModel>(),
        //            ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        //            {
        //                new ShoppingCartItemModel
        //                {
        //                    ItemId = null,
        //                    ItemRate = null,
        //                    ItemRateBeforeDiscount = null,
        //                    ItemShortDesc = null,
        //                    OrderAmount = null,
        //                    OrderAmountBeforeDiscount = null,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //                },
        //            },
        //        };
        //        itemId = shoppingCartItemModelTemp.ItemId.Value;
        //        orderQty = shoppingCartItemModelTemp.OrderQty.Value;
        //        orderComments = shoppingCartItemModelTemp.OrderComments;
        //        itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
        //        List<ShoppingCartItemModel> shoppingCartItemModels;
        //        if (shoppingCartBundleModel != null)
        //        {//When adding a bundle always add it as a new line item in the cart
        //            shoppingCartItemModels = CalculateBundleOrderAmount(shoppingCartBundleModel, out float itemRateBeforeDiscount, out float discountPercent, out float itemRate, out float orderAmount, out float orderAmountBeforeDiscount, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            shoppingCartItemModel = CreateShoppingCartItemModel(itemModel, itemRateBeforeDiscount, discountPercent, itemRate, orderQty, orderAmountBeforeDiscount, orderAmount, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            shoppingCartItemModel.ShoppingCartItemBundleItemModels = shoppingCartItemModels;
        //            //shoppingCartItemModel.OrderAmount = orderAmount;
        //            //shoppingCartItemModel.OrderAmountBeforeDiscount = orderAmountBeforeDiscount;
        //        }
        //        else
        //        {
        //            shoppingCartItemModel = paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        //            if (shoppingCartItemModel == null)
        //            {
        //                shoppingCartItemModel = CreateShoppingCartItemModel(itemModel, orderQty, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            else
        //            {
        //                shoppingCartItemModel.OrderQty += orderQty;
        //            }
        //            //shoppingCartItemModel.ItemRate = itemModel.ItemRate;
        //            //shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
        //            //shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
        //        }
        //        paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.Add(shoppingCartItemModel);
        //        shoppingCartItemModel.OrderComments = orderComments;
        //        shoppingCartItemModel.VolumeValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        //        shoppingCartItemModel.WeightCalcValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightCalcValue;
        //        shoppingCartItemModel.WeightValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightValue;
        //        shoppingCartItemModel.ProductOrVolumetricWeight = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
        //        UpdateShoppingCart(paymentInfo1Model.ShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #region
        //// POST: AddToCart
        //public string AddToCart2(ref PaymentInfoModel paymentInfoModel, AddToCartModel addToCartModel, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string itemIdParm, orderQtyParm;
        //        itemIdParm = addToCartModel.ItemId.ToString();
        //        orderQtyParm = addToCartModel.OrderQty.ToString();
        //        float itemRateParm = CalculateBundleOrderAmount(addToCartModel.ShoppingCartItemBundleModels, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        string errorMessage = AddToCart(ref paymentInfoModel, itemIdParm, itemRateParm, null, orderQtyParm, createOrderWIP, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        return errorMessage;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        #region
        //private ShoppingCartItemModel CreateShoppingCartItemModel(ItemModel itemModel, float itemRateBeforeDiscount, float discountPercent, float itemRate, long orderQty, float orderAmountBeforeDiscount, float orderAmount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    ShoppingCartItemModel shoppingCartItemModel = new ShoppingCartItemModel
        //    {
        //        DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue),
        //        HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue),
        //        HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //        ItemDiscountPercent = discountPercent,
        //        ItemId = itemModel.ItemId,
        //        ItemRate = itemRate,
        //        ItemRateBeforeDiscount = itemRateBeforeDiscount,
        //        ItemShortDesc = itemModel.ItemShortDesc,
        //        LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue),
        //        OrderAmount = orderAmount,
        //        OrderAmountBeforeDiscount = orderAmountBeforeDiscount,
        //        OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //        OrderQty = orderQty,
        //        ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //        WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //        WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue),
        //        WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //        WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue),
        //        WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue),
        //        ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue),
        //        ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //        ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
        //    };
        //    return shoppingCartItemModel;
        //}
        #endregion
        #region
        //private ShoppingCartItemModel CreateShoppingCartItemModel(ItemModel itemModel, long orderQty, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    ShoppingCartItemModel shoppingCartItemModel = new ShoppingCartItemModel
        //    {
        //        DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue),
        //        HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue),
        //        HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //        ItemDiscountPercent = 0,
        //        ItemId = itemModel.ItemId,
        //        ItemRate = itemModel.ItemRate,
        //        ItemRateBeforeDiscount = itemModel.ItemRate,
        //        ItemShortDesc = itemModel.ItemShortDesc,
        //        LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue),
        //        //OrderAmount = orderQty * itemModel.ItemRate,
        //        //OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
        //        OrderAmount = orderQty * itemModel.ItemRate,
        //        OrderAmountBeforeDiscount = itemModel.ItemRate,
        //        OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //        OrderQty = orderQty,
        //        ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //        WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //        WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue),
        //        WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //        WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue),
        //        WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue),
        //        ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue),
        //        ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //        ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
        //    };
        //    return shoppingCartItemModel;
        //}
        #endregion
        #region
        //private List<ShoppingCartItemModel> CalculateBundleOrderAmount(AddToCartModel shoppingCartBundleModel, out float itemRateBeforeDiscount, out float discountPercent, out float itemRate, out float orderAmount, out float orderAmountBeforeDiscount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        float itemRateTemp, itemRateBeforeDiscountTemp, orderAmountTemp, orderAmountBeforeDiscountTemp;
        //        discountPercent = 0;// shoppingCartBundleModel.DiscountPercent;
        //        ItemModel itemModel;
        //        itemRateBeforeDiscount = 0;
        //        itemRate = 0;
        //        orderAmount = 0;
        //        orderAmountBeforeDiscount = 0;
        //        foreach (var shoppingCartItemBundleModel in shoppingCartBundleModel.ShoppingCartItemBundleModels)
        //        {
        //            if (shoppingCartItemBundleModel.OrderQty == 0)
        //            {
        //                discountPercent = 0;
        //                break;
        //            }
        //        }
        //        List<ShoppingCartItemModel> shoppingCartItemBundleItemModels = new List<ShoppingCartItemModel>();
        //        int tempNum = 0;
        //        System.Diagnostics.Debug.WriteLine("tempNum" + "\t" + "itemRateBeforeDiscount" + "\t" + "discountPercent" + "\t" + "itemRate" + "\t" + "shoppingCartBundleModel.OrderQty" + "\t" + "orderAmountBeforeDiscount" + "\t" + "orderAmount" + "\t" + "Quantity" + "\t" + "itemModel.ItemId" + "\t" + "itemModel.ItemShortDesc");
        //        foreach (var shoppingCartItemBundleModel in shoppingCartBundleModel.ShoppingCartItemBundleModels)
        //        {
        //            if (shoppingCartItemBundleModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //            {
        //            }
        //            else
        //            {
        //                itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItemBundleModel.ItemId);
        //                itemRateTemp = itemModel.ItemRate.Value;
        //                itemRateBeforeDiscountTemp = shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.Quantity * itemRateTemp;
        //                orderAmountTemp = shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.OrderQty * itemRateTemp * (100 - discountPercent) / 100;
        //                orderAmountBeforeDiscountTemp = shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.OrderQty * itemRateTemp;
        //                itemRate += shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.Quantity * itemRateTemp * (100 - discountPercent) / 100;
        //                itemRateBeforeDiscount += shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.Quantity * itemRateTemp;
        //                orderAmount += shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.OrderQty * itemRateTemp * (100 - discountPercent) / 100;
        //                orderAmountBeforeDiscount += shoppingCartItemBundleModel.OrderQty == 0 ? 0 : shoppingCartItemBundleModel.OrderQty * itemRateTemp;
        //                shoppingCartItemBundleItemModels.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        //ItemBundleId = shoppingCartItemBundleModel.ItemBundleId,
        //                        //ItemBundleItemId = shoppingCartItemBundleModel.ItemBundleItemId,
        //                        ItemDiscountPercent = discountPercent,
        //                        ItemId = itemModel.ItemId,
        //                        ItemShortDesc = itemModel.ItemMasterModel.ItemMasterDesc,
        //                        ItemRate = itemRateTemp,
        //                        ItemRateBeforeDiscount = itemRateBeforeDiscountTemp,
        //                        OrderAmount = orderAmountTemp,
        //                        OrderAmountBeforeDiscount = orderAmountBeforeDiscountTemp,
        //                        OrderQty = shoppingCartItemBundleModel.OrderQty,
        //                    }
        //                );
        //                tempNum++;
        //                System.Diagnostics.Debug.WriteLine(tempNum + "\t" + itemRateBeforeDiscount + "\t" + discountPercent + "\t" + itemRate + "\t" + shoppingCartBundleModel.OrderQty + "\t" + orderAmountBeforeDiscount + "\t" + orderAmount + "\t" + shoppingCartItemBundleModel.Quantity + "\t" + itemModel.ItemId + "\t" + itemModel.ItemShortDesc);
        //            }
        //        }
        //        return shoppingCartItemBundleItemModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #region
        //private List<ShoppingCartItemModel> CalculateBundleOrderAmount(long itemId, long orderQty, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ItemBundleModel itemBundleModel = RetailSlnCache.ItemBundleModels.First(x => x.ItemId == itemId);
        //        List<ShoppingCartItemModel> shoppingCartItemBundleItemModels = new List<ShoppingCartItemModel>();
        //        int tempNum = 0;
        //        System.Diagnostics.Debug.WriteLine("tempNum" + "\t" + "itemRateBeforeDiscount" + "\t" + "discountPercent" + "\t" + "itemRate" + "\t" + "shoppingCartBundleModel.OrderQty" + "\t" + "orderAmountBeforeDiscount" + "\t" + "orderAmount" + "\t" + "Quantity" + "\t" + "itemModel.ItemId" + "\t" + "itemModel.ItemShortDesc");
        //        foreach (var itemBundleItemModel in itemBundleModel.ItemBundleItemModels)
        //        {
        //            if (itemBundleItemModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //            {
        //            }
        //            else
        //            {
        //                shoppingCartItemBundleItemModels.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        //ItemBundleId = itemBundleItemModel.ItemBundleId,
        //                        //ItemBundleItemId = itemBundleItemModel.ItemBundleItemId,
        //                        //ItemDiscountPercent = itemBundleModel.DiscountPercent,
        //                        //ItemId = itemId,
        //                        //ItemShortDesc = itemBundleItemModel.ItemModel.ItemMasterModel.ItemMasterDesc,
        //                        //ItemRate = itemRateTemp,
        //                        //ItemRateBeforeDiscount = itemRateBeforeDiscountTemp,
        //                        //OrderAmount = orderAmountTemp,
        //                        //OrderAmountBeforeDiscount = orderAmountBeforeDiscountTemp,
        //                        //OrderQty = orderQty,
        //                    }
        //                );
        //                tempNum++;
        //                //System.Diagnostics.Debug.WriteLine(tempNum + "\t" + itemRateBeforeDiscount + "\t" + discountPercent + "\t" + itemRate + "\t" + shoppingCartBundleModel.OrderQty + "\t" + orderAmountBeforeDiscount + "\t" + orderAmount + "\t" + itemBundleItemModel.Quantity + "\t" + itemModel.ItemId + "\t" + itemModel.ItemShortDesc);
        //            }
        //        }
        //        return shoppingCartItemBundleItemModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #region
        //// POST: AddToCart2
        //public void AddToCart2Backup(ref PaymentInfo1Model paymentInfo1Model, ShoppingCartBundleModel shoppingCartBundleModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    float discountPercent = shoppingCartBundleModel.DiscountPercent, orderAmount = 0;
        //    ItemModel itemModel;
        //    try
        //    {
        //        if (paymentInfo1Model == null)
        //        {
        //            paymentInfo1Model = new PaymentInfo1Model();
        //        }
        //        paymentInfo1Model.ShoppingCartModel = paymentInfo1Model.ShoppingCartModel ?? new ShoppingCartModel
        //        {
        //            ShoppingCartItems = new List<ShoppingCartItemModel>(),
        //            ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        //            {
        //                new ShoppingCartItemModel
        //                {
        //                    ItemId = null,
        //                    ItemRate = null,
        //                    ItemRateBeforeDiscount = null,
        //                    ItemShortDesc = null,
        //                    OrderAmount = null,
        //                    OrderAmountBeforeDiscount = null,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //                },
        //            },
        //        };
        //        ShoppingCartModel shoppingCartModel = paymentInfo1Model.ShoppingCartModel;
        //        //ShoppingCartItemModel shoppingCartItemModel;
        //        foreach (var shoppingCartItemBundleModel in shoppingCartBundleModel.ShoppingCartItemBundleModels)
        //        {
        //            if (shoppingCartItemBundleModel.OrderQty == 0)
        //            {
        //                discountPercent = 0;
        //                break;
        //            }
        //        }
        //        long orderQty;
        //        float itemRate;
        //        foreach (var shoppingCartItemBundleModel in shoppingCartBundleModel.ShoppingCartItemBundleModels)
        //        {
        //            if (shoppingCartItemBundleModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //            {
        //            }
        //            else
        //            {
        //                itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItemBundleModel.ItemId);
        //                itemRate = itemModel.ItemRate.Value;
        //                orderAmount += shoppingCartBundleModel.OrderQty * itemRate * (100 - discountPercent) / 100;
        //            }
        //        }
        //        orderQty = shoppingCartBundleModel.OrderQty;
        //        itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartBundleModel.ItemId);
        //        itemRate = itemModel.ItemRate.Value;
        //        //var abc0DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue);
        //        //var abc1HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue);
        //        //var abc2HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay;
        //        //float? abc3ItemDiscountPercent = null;
        //        //var abc4ItemId = itemModel.ItemId;
        //        //var abc5ItemRate = itemModel.ItemRate;
        //        //var abc6ItemRateBeforeDiscount = itemModel.ItemRate;
        //        //var abc7ItemShortDesc = itemModel.ItemShortDesc;
        //        //var abc8LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue);
        //        //var abc9OrderAmount = orderQty * itemModel.ItemRate;
        //        //var abcaOrderAmountBeforeDiscount = orderQty * itemModel.ItemRate;
        //        //var abcbOrderDetailTypeId = OrderDetailTypeEnum.Item;
        //        //var abccOrderQty = orderQty;
        //        //var abcdProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay;
        //        //var abceWeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue);
        //        //var abcfWeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue);
        //        //var abcgWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue);
        //        //var abchWeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
        //        //var abciWidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue);
        //        //var abcjProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
        //        //var abckProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue);
        //        //ShoppingCartItemSummarys = new List<ShoppingCartItemModel>();
        //        shoppingCartModel.ShoppingCartItems.Add
        //        (
        //            new ShoppingCartItemModel
        //            {
        //                DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue),
        //                HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue),
        //                HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //                ItemDiscountPercent = null,
        //                ItemId = itemModel.ItemId,
        //                ItemRate = itemModel.ItemRate,
        //                ItemRateBeforeDiscount = itemModel.ItemRate,
        //                ItemShortDesc = itemModel.ItemShortDesc,
        //                LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue),
        //                OrderAmount = orderQty * itemModel.ItemRate,
        //                OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
        //                OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //                OrderQty = orderQty,
        //                ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //                WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //                WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue),
        //                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //                WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue),
        //                WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue),
        //                ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue),
        //                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //                ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
        //            }
        //        );
        //        UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #region
        //// PRIVATE : AddItemBundleItemsToShoppingCart
        //private float AddItemBundleItemsToShoppingCart(long parentItemId, out List<ShoppingCartItemModel> shoppingCartItemBundleModels, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    List<ItemModel> itemModels = RetailSlnCache.ParentItemBundleModels[parentItemId].ItemModels;
        //    shoppingCartItemBundleModels = new List<ShoppingCartItemModel>();
        //    float itemRateParm = 0, itemRate, orderAmount;
        //    ShoppingCartItemModel shoppingCartItemBundleModel;
        //    foreach (var itemModel in itemModels)
        //    {
        //        itemRate = itemModel.ItemRate.Value;
        //        orderAmount = itemRate; //Adding from main page quantity is always 1
        //        itemRateParm += itemModel.ItemRate.Value;
        //        shoppingCartItemBundleModel = new ShoppingCartItemModel
        //        {
        //            HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //            ItemDiscountAmount = 0,
        //            ItemDiscountPercent = 0,
        //            ItemDiscountPercentFormatted = "",
        //            ItemId = itemModel.ItemId,
        //            ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
        //            ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
        //            ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
        //            ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
        //            ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
        //            ItemRate = itemRate,
        //            ItemRateBeforeDiscount = itemRate,
        //            ItemRateBeforeDiscountFormatted = itemRate.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //            ItemRateFormatted = itemRate.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //            ItemShortDesc = null,//itemModel.ItemShortDesc,
        //            OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //            ParentItemId = parentItemId,
        //            ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //            ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //            WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //            WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
        //            ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
        //        };
        //        shoppingCartItemBundleModels.Add(shoppingCartItemBundleModel);
        //    }
        //    return itemRateParm;
        //}
        #endregion
        #region
        //// PRIVATE : CalculateBundleOrderAmount
        //private float CalculateBundleOrderAmount(List<ShoppingCartItemModel> shoppingCartItemBundleModels, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        float orderAmount = 0;
        //        ItemModel itemModel;
        //        foreach (var shoppingCartItemBundleModel in shoppingCartItemBundleModels)
        //        {
        //            itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItemBundleModel.ItemId);
        //            orderAmount += (float)(shoppingCartItemBundleModel.OrderQty * itemModel.ItemRate.Value);
        //        }
        //        return orderAmount;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #endregion
    }
}
