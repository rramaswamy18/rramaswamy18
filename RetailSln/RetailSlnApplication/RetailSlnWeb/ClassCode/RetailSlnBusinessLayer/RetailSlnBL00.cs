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
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        #region GET / POST
        // GET: AddToCart
        public string AddToCart(ref PaymentInfoModel paymentInfoModel, string itemIdParm, string orderQtyParm, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                string errorMessage = AddToCart(ref paymentInfoModel, itemIdParm, orderQtyParm, createOrderWIP, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // GET: AddToCart
        public string AddToCart(ref PaymentInfoModel paymentInfoModel, string itemIdParm, string orderQtyParm, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string errorMessage = "";
                ItemModel itemModel = null;
                if (long.TryParse(itemIdParm, out long itemId))
                {
                    itemModel = RetailSlnCache.ItemModels.FirstOrDefault(x => x.ItemId == itemId);
                    if (itemModel == null)
                    {
                        errorMessage += "Select valid item;";
                    }
                }
                else
                {
                    errorMessage += "Select valid item;";
                }
                if (!long.TryParse(orderQtyParm, out long orderQty))
                {
                    errorMessage += "Enter order quantity;";
                }
                if (errorMessage == "")
                {
                    AddItemToShoppingCart(ref paymentInfoModel, itemModel, orderQty, createOrderWIP, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                if (paymentInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
                {
                    paymentInfoModel.OrderSummaryModel.InvoiceType = "Tax Invoice";
                }
                else
                {
                    paymentInfoModel.OrderSummaryModel.InvoiceType = "Order Form";
                }
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
                var toEmailAddresss = paymentInfoModel.OrderSummaryModel.EmailAddress + ";" + ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", "ToEmailAddress");
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
                //CalculateDiscounts(paymentInfoModel, createForSessionObject.PersonId, corpAcctModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                //CalculateShoppingCartTotals(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                //CalculateShoppingCartSummaryTotals(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                //BuildCreditCardDataModel(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // GET: ItemMasterAttributes
        public ItemMasterAttributesModel ItemMasterAttributes(long itemMasterId, long tabId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ItemMasterAttributesModel itemMasterAttributesModel = new ItemMasterAttributesModel
                {
                    ItemMasterId = itemMasterId,
                    TabId = tabId,
                    ItemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == itemMasterId),
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
        // GET : ItemBundleItemData
        public ItemBundleDataModel ItemBundleData(long itemId, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemBundleDataModel itemBundleDataModel;
            try
            {
                var itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId);
                itemBundleDataModel = new ItemBundleDataModel
                {
                    ItemMasterId = itemModel.ItemMasterId,
                    ItemBundleModel = RetailSlnCache.ItemBundleModels.FirstOrDefault(x => x.ItemId == itemId),
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
                            "Error occurred while populating for Item " + itemId,
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
        public ItemCatalogModel ItemCatalog(SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ItemCatalogModel itemCatalogModel = new ItemCatalogModel
            {
                ItemMasterModels = RetailSlnCache.ItemMasterModels.OrderBy(x => x.ItemMasterDesc).ToList(),
                ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Success,
                },
            };
            return itemCatalogModel;
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
                //For now hardcode based on role - Modify code to get it from Asp KVP table
                //Until data in this table needs to be organizws
                #region
                if (parentCategoryId == 0)
                {
                    switch (aspNetRoleName)
                    {
                        case "BULKORDERSROLE":
                        case "MARKETINGROLE":
                            parentCategoryId = 102;
                            break;
                        case "WHOLESALEROLE":
                            parentCategoryId = 120;
                            break;
                        case "PRIESTROLE":
                        default:
                            parentCategoryId = 90;
                            break;
                    }
                }
                string viewName;
                switch (aspNetRoleName)
                {
                    case "BULKORDERSROLE":
                    case "WHOLESALEROLE":
                        viewName = "_OrderItem1";
                        break;
                    case "APPLADMN1":
                    case "MARKETINGROLE":
                    case "SYSTADMIN":
                        viewName = "_OrderItem2";
                        break;
                    case "PRIESTROLE":
                    default:
                        viewName = "_OrderItem0";
                        break;
                }
                #endregion
                //For now use default for priest
                if (aspNetRoleName == "PRIESTROLE" || aspNetRoleName == "GUESTROLE")
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
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
                //itemMasterModels = RetailSlnCache.AspNetRoleParentCategoryItemMasterModels[aspNetRoleName][parentCategoryId].Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
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
                OrderItemModel orderItemModel = new OrderItemModel
                {
                    AspNetRoleName = aspNetRoleName,
                    CategoryItemMasterHierModels = itemMasterModels,
                    PageCount = pageCount,
                    PageNum = pageNum,
                    PageSize = pageSize,
                    ParentCategoryId = parentCategoryId,
                    ParentCategoryModel = RetailSlnCache.CategoryModels.First(x => x.CategoryId == parentCategoryId),
                    //ShoppingCartModel = paymentInfoModel.ShoppingCartModel,
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

                CreateOrder(paymentInfoModel, "", "", ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.HideShoppingCart = true;
                paymentInfoModel.ShoppingCartModel.Checkout = true;

                if (paymentInfoModel.OrderHeaderWIPModel != null && paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId != null && paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId != 0)
                {
                    DeleteOrderWIP(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                    creditCardServiceBL.UpdCreditCardData(paymentInfoModel.CreditCardDataModel.CreditCardDataId, razorpay_payment_id, razorpay_order_id, razorpay_signature, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

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

                    CreateOrder(paymentInfoModel, razorpay_order_id, razorpay_payment_id, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    paymentInfoModel.HideShoppingCart = true;
                    paymentInfoModel.ShoppingCartModel.Checkout = true;
                    DeleteOrderWIP(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public SearchResultModel SearchResult(string searchKeywordText, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SearchResultModel searchResultModel;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
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
        public void ShoppingCartComments(PaymentInfoModel paymentInfoModel, int index, string orderComments, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels[index].OrderComments = orderComments;
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
                        orderHeaderWIPModel = CreateOrderHeaderWIPModel(applSessionObjectModel.CorpAcctLocationId, paymentInfoModel.OrderSummaryModel.InvoiceTypeId, paymentInfoModel.OrderSummaryModel.OrderDateTime, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                            OrderHeaderWIPId = orderHeaderWIPId.Value,
                            OrderQty = shoppingCartItemModel.OrderQty.Value,
                            SeqNum = ++maxSeqNum,
                        };
                        ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModelTemp, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                        AddToCart(ref paymentInfoModel, orderDetailWIPModel.ItemId.ToString(), orderDetailWIPModel.OrderQty.ToString(), false, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // PRIVATE : AddItemToShoppingCart
        private void AddItemToShoppingCart(ref PaymentInfoModel paymentInfoModel, ItemModel itemModel, long orderQty, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                ShoppingCartItemModel shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId);
                if (shoppingCartItemModel == null)
                {
                    CreateShoppingCartItemModel(ref shoppingCartItemModel, itemModel, null, orderQty, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Add(shoppingCartItemModel);
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createOrderWIP)
                    {
                        ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                        CreateOrderWIP(ref paymentInfoModel, applSessionObjectModel.CorpAcctLocationId, itemModel.ItemId.Value, orderQty, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                else
                {
                    CreateShoppingCartItemModel(ref shoppingCartItemModel, itemModel, null, shoppingCartItemModel.OrderQty + orderQty, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (createForSessionObject != null && createOrderWIP)
                    {
                        ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                        UpdateOrderDetailWIP(ref paymentInfoModel, itemModel.ItemId.Value, shoppingCartItemModel.OrderQty.Value, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        private void CreateOrder(PaymentInfoModel paymentInfoModel, string paymentOrderId, string paymentId, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                OrderDetailItemBundle orderDetailItemBundle;
                int seqNum = 0, seqNumBundle = 0;
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary)
                {
                    orderDetail = CreateOrderDetail(orderHeaderSummary.OrderHeaderSummaryId, ++seqNum, shoppingCartItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.OrderDetailAdd(orderDetail, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (shoppingCartItem.ItemBundleModel != null)
                    {
                        foreach (var itemBundleItemModel in shoppingCartItem.ItemBundleModel.ItemBundleItemModels)
                        {
                            orderDetailItemBundle = CreateOrderDetailItemBundleItem(orderDetail.OrderDetailId, shoppingCartItem.OrderQty.Value, ++seqNumBundle, shoppingCartItem.ItemBundleModel, itemBundleItemModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                            ApplicationDataContext.OrderDetailItemBundleAdd(orderDetailItemBundle, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                    PaymentRefNumCaption1 = string.IsNullOrWhiteSpace(paymentId) ? "" : "Ref#:",
                    PaymentRefNumData1 = string.IsNullOrWhiteSpace(paymentId) ? "" : paymentId,
                    PaymentRefNumCaption2 = string.IsNullOrWhiteSpace(paymentOrderId) ? "" : "Ord#",
                    PaymentRefNumData2 = string.IsNullOrWhiteSpace(paymentOrderId) ? "" : paymentOrderId,
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
        private OrderDetail CreateOrderDetail(long orderHeaderSummaryId, int seqNum, ShoppingCartItemModel shoppingCartItemModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    HeightValue = shoppingCartItemModel.HeightValue == null ? 0 : shoppingCartItemModel.HeightValue.Value,
                    HSNCode = shoppingCartItemModel.HSNCode,
                    ItemDiscountAmount = shoppingCartItemModel.ItemDiscountAmount == null ? 0 : shoppingCartItemModel.ItemDiscountAmount.Value,
                    ItemId = shoppingCartItemModel.ItemId,
                    ItemRate = shoppingCartItemModel.ItemRate == null ? 0 : shoppingCartItemModel.ItemRate.Value,
                    ItemRateBeforeDiscount = shoppingCartItemModel.ItemRateBeforeDiscount == null ? 0 : shoppingCartItemModel.ItemRateBeforeDiscount.Value,
                    ItemShortDesc = shoppingCartItemModel.ItemShortDesc,
                    LengthValue = shoppingCartItemModel.LengthValue == null ? 0 : shoppingCartItemModel.LengthValue.Value,
                    OrderAmount = shoppingCartItemModel.OrderAmount == null ? 0 : shoppingCartItemModel.OrderAmount.Value,
                    OrderAmountBeforeDiscount = shoppingCartItemModel.OrderAmountBeforeDiscount == null ? 0 : shoppingCartItemModel.OrderAmountBeforeDiscount.Value,
                    OrderComments = shoppingCartItemModel.OrderComments,
                    OrderDetailTypeId = shoppingCartItemModel.OrderDetailTypeId,
                    OrderHeaderSummaryId = orderHeaderSummaryId,
                    OrderQty = shoppingCartItemModel.OrderQty == null ? 0 : shoppingCartItemModel.OrderQty.Value,
                    ProductCode = shoppingCartItemModel.ProductCode,
                    ProductOrVolumetricWeight = shoppingCartItemModel.ProductOrVolumetricWeight == null ? 0 : shoppingCartItemModel.ProductOrVolumetricWeight.Value,
                    ProductOrVolumetricWeightUnitId = shoppingCartItemModel.ProductOrVolumetricWeightUnitId == null ? 0 : shoppingCartItemModel.ProductOrVolumetricWeightUnitId.Value,
                    SeqNum = seqNum,
                    VolumeValue = shoppingCartItemModel.VolumeValue == null ? 0 : shoppingCartItemModel.VolumeValue.Value,
                    WeightCalcUnitId = shoppingCartItemModel.WeightCalcUnitId == null ? 0 : shoppingCartItemModel.WeightCalcUnitId.Value,
                    WeightCalcValue = shoppingCartItemModel.WeightCalcValue == null ? 0 : shoppingCartItemModel.WeightCalcValue.Value,
                    WeightUnitId = shoppingCartItemModel.WeightUnitId == null ? 0 : shoppingCartItemModel.WeightUnitId.Value,
                    WeightValue = shoppingCartItemModel.WeightValue == null ? 0 : shoppingCartItemModel.WeightValue.Value,
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
        // PRIVATE: CreateOrderDetailItemBundleItem
        private OrderDetailItemBundle CreateOrderDetailItemBundleItem(long orderDetailId, long orderQty, int seqNum, ItemBundleModel itemBundleModel, ItemBundleItemModel itemBundleItemModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            float itemRate = itemBundleItemModel.ItemModel.ItemRate.Value;
            float itemRateBeforeDiscount = itemRate * (100 - itemBundleModel.DiscountPercent) / 100;
            float orderAmount = itemRate * orderQty;
            float orderAmountBeforeDiscount = itemRateBeforeDiscount * orderQty;
            OrderDetailItemBundle orderDetailItemBundle = new OrderDetailItemBundle
            {
                ClientId = clientId,
                DiscountPercent = itemBundleModel.DiscountPercent,
                ItemBundleId = itemBundleModel.ItemBundleId,
                ItemBundleItemId = itemBundleItemModel.ItemBundleItemId,
                ItemId = itemBundleItemModel.ItemId,
                ItemMasterDesc = itemBundleItemModel.ItemModel.ItemMasterModel.ItemMasterDesc,
                ItemRate = itemRate,
                ItemRateBeforeDiscount = itemRateBeforeDiscount,
                OrderAmount = orderAmount,
                OrderAmountBeforeDiscount = orderAmountBeforeDiscount,
                OrderDetailId = orderDetailId,
                OrderQty = orderQty,
                SeqNum = seqNum,
            };
            return orderDetailItemBundle;
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
        // PRIVATE: CreateOrderWIP
        private void CreateOrderWIP(ref PaymentInfoModel paymentInfoModel, long corpAcctLocationId, long itemId, long orderQty, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                        paymentInfoModel.OrderHeaderWIPModel = CreateOrderHeaderWIPModel(corpAcctLocationId, paymentInfoModel.OrderSummaryModel.InvoiceTypeId, paymentInfoModel.OrderSummaryModel.OrderDateTime, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.OrderHeaderWIPAdd(paymentInfoModel.OrderHeaderWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    OrderDetailWIPModel orderDetailWIPModel;
                    paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.Add
                    (
                        orderDetailWIPModel = new OrderDetailWIPModel
                        {
                            ClientId = clientId,
                            ItemId = itemId,
                            OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
                            OrderQty = orderQty,
                            SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
                        }
                    );
                    ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // PRIVATE : CreateOrderHeaderWIPModel
        private OrderHeaderWIPModel CreateOrderHeaderWIPModel(long corpAcctLocationId, InvoiceTypeEnum? invoiceTypeId, string orderDateTime, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = null,
                        OrderAmount = null,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
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
        // PRIVATE : CreateShoppingCartItemModel
        private void CreateShoppingCartItemModel(ref ShoppingCartItemModel shoppingCartItemModel, ItemModel itemModel, ItemBundleModel itemBundleModel, long? orderQty, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
            float? itemRate, itemRateBeforeDiscount, orderAmount, orderAmountBeforeDiscount;
            ItemDiscountModel itemDiscountModel = ApplicationDataContext.ItemDiscountGet(corpAcctId, itemModel.ItemId.Value, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            itemRateBeforeDiscount = itemModel.ItemRate;
            itemRate = itemRateBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
            if (shoppingCartItemModel == null)
            {
                shoppingCartItemModel = new ShoppingCartItemModel
                {
                    HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
                    ItemBundleModel = itemBundleModel,
                    ItemDiscountAmount = itemDiscountModel.DiscountPercent * itemRateBeforeDiscount * orderQty / 100,
                    ItemDiscountPercent = itemDiscountModel.DiscountPercent,
                    ItemDiscountPercentFormatted = itemDiscountModel.DiscountPercent.ToString("#0.00") + "%",
                    ItemId = itemModel.ItemId,
                    ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                    ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
                    ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
                    ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
                    ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
                    ItemRate = itemRate,
                    ItemRateBeforeDiscount = itemRateBeforeDiscount,
                    ItemRateBeforeDiscountFormatted = itemRateBeforeDiscount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                    ItemRateFormatted = itemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                    ItemShortDesc = itemModel.ItemShortDesc,
                    OrderDetailTypeId = OrderDetailTypeEnum.Item,
                    ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
                    ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
                    WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                    WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                    ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                };
            }
            orderAmountBeforeDiscount = orderQty * itemRateBeforeDiscount;
            orderAmount = orderQty * itemRate;
            //orderAmount = orderAmountBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
            shoppingCartItemModel.OrderQty = orderQty;
            shoppingCartItemModel.OrderAmount = orderAmount;
            shoppingCartItemModel.OrderAmountBeforeDiscount = orderAmountBeforeDiscount;
            shoppingCartItemModel.OrderAmountFormatted = orderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            shoppingCartItemModel.ProductOrVolumetricWeight = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
            shoppingCartItemModel.WeightCalcValue = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
            shoppingCartItemModel.WeightValue = orderQty * float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
            return;
        }
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
                    break;
            }

            return creditCardKVPs;
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
        private void DeleteOrderWIP(PaymentInfoModel paymentInfoModel, SqlConnection sqlConnection, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        // PRIVATE : GetItemDiscounts
        //private void GetItemDiscounts(OrderItemModel orderItemModel, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    string itemIds = "", prefixString = "";
        //    foreach (var categoryItemMasterHierModel in orderItemModel.CategoryItemMasterHierModels)
        //    {
        //        foreach (var itemModel in categoryItemMasterHierModel.ItemMasterModel.ItemModels)
        //        {
        //            itemIds += prefixString + itemModel.ItemId;
        //            prefixString = ",";
        //        }
        //    }
        //    ApplicationDataContext.OpenSqlConnection();
        //    List<ItemDiscountModel> itemDiscountModels = ApplicationDataContext.ItemDiscountList(corpAcctId, itemIds, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    ItemDiscountModel itemDiscountModel;
        //    foreach (var categoryItemMasterHierModel in orderItemModel.CategoryItemMasterHierModels)
        //    {
        //        foreach (var itemModel in categoryItemMasterHierModel.ItemMasterModel.ItemModels)
        //        {
        //            itemModel.ItemDiscountModels = new List<ItemDiscountModel>();
        //        }
        //        foreach (var itemModel in categoryItemMasterHierModel.ItemMasterModel.ItemModels)
        //        {
        //            itemDiscountModel = itemDiscountModels.FirstOrDefault(x => x.CorpAcctId == corpAcctId && x.ItemId == itemModel.ItemId);
        //            if (itemDiscountModel != null)
        //            {
        //                itemModel.ItemDiscountModels.Add(itemDiscountModel);
        //            }
        //        }
        //    }
        //}
        // PRIVATE : GetCorpAcctId
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

            demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId);
            try
            {
                demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId);
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
        // PRIVATE : UpdateDeliveryAddressInfo
        private void UpdateOrderDetailWIP(ref PaymentInfoModel paymentInfoModel, long itemId, long orderQty, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                OrderDetailWIPModel orderDetailWIPModel = paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.First(x => x.ItemId == itemId);
                orderDetailWIPModel.OrderQty = orderQty;
                ApplicationDataContext.OrderDetailWIPUpd(orderDetailWIPModel.OrderHeaderWIPId, itemId, orderQty, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        #endregion
    }
}
