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
using RetailSlnWeb.Controllers;
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
//using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
//using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        #region GET / POST
        #region New Code 11/25/2025
        // GET: AddToCart
        public string AddToCart(ref ShoppingCartModel shoppingCartModel, AddToCartModel addToCartModel, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                string errorMessage = AddToCart(ref shoppingCartModel, addToCartModel, createOrderWIP, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public CheckoutModel Checkout(ShoppingCartModel shoppingCartModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                CheckoutModel checkoutModel = null;
                if (CheckoutValidate(shoppingCartModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId))
                {
                    archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
                    checkoutModel = new CheckoutModel
                    {
                        OTPRequestModel = new OTPRequestModel
                        {
                            CaptchaNumber0 = httpSessionStateBase["CaptchaNumber0"].ToString(),
                            CaptchaNumber1 = httpSessionStateBase["CaptchaNumber1"].ToString(),
                            DefaultDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            RequestType = "Checkout",
                            TelephoneCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                        }
                    };
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
        public DeliveryInfoModel Checkout(SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            DeliveryInfoModel deliveryInfoModel;
            CorpAcctModel corpAcctModel = (((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel);
            try
            {
                deliveryInfoModel = (DeliveryInfoModel)httpSessionStateBase["DeliveryInfo"];
                if (deliveryInfoModel == null)
                {
                    deliveryInfoModel = new DeliveryInfoModel
                    {
                        CompleteOrderModel = new CompleteOrderModel
                        {
                        },
                        CouponPaymentModel = new CouponPaymentModel
                        {
                        },
                        DeliveryAddressModel = new DemogInfoAddressModel
                        {
                            BuildingTypeId = BuildingTypeEnum._,
                            DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                        },
                        DeliveryDataModel = new DeliveryDataModel
                        {
                            AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            PrimaryTelephoneDemogInfoCountryId = createForSessionObject.TelephoneCountryId,
                            PrimaryTelephoneNum = createForSessionObject.PhoneNumber,
                        },
                        DeliveryMethodModel = new DeliveryMethodModel
                        {
                        },
                        GiftCertPaymentModel = new GiftCertPaymentModel
                        {
                        },
                        OrderSummaryModel = new OrderSummaryModel
                        {
                            EmailAddress = createForSessionObject.EmailAddress,
                            EmailExists = string.IsNullOrWhiteSpace(createForSessionObject.EmailAddress) ? false : true,
                            InvoiceTypeId = corpAcctModel.CorpAcctId > 0 ? InvoiceTypeEnum.Quotation : InvoiceTypeEnum.FinalInvoice,
                        },
                        PaymentModeModel = new PaymentModeModel
                        {
                        },
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseMessages = new List<string>(),
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                    if (createForSessionObject.AspNetRoleName != "GUESTROLE")
                    {
                        if (string.IsNullOrWhiteSpace(deliveryInfoModel.OrderSummaryModel.FirstName))
                        {
                            deliveryInfoModel.OrderSummaryModel.FirstName = createForSessionObject?.FirstName;
                        }
                        if (string.IsNullOrWhiteSpace(deliveryInfoModel.OrderSummaryModel.LastName))
                        {
                            deliveryInfoModel.OrderSummaryModel.LastName = createForSessionObject?.LastName;
                        }
                        if (string.IsNullOrWhiteSpace(deliveryInfoModel.DeliveryAddressModel.AddressLine1))
                        {
                            deliveryInfoModel.DeliveryAddressModel.AddressLine1 = createForSessionObject?.DemogInfoAddressModel?.AddressLine1;
                        }
                        if (string.IsNullOrWhiteSpace(deliveryInfoModel.DeliveryAddressModel.AddressLine2))
                        {
                            deliveryInfoModel.DeliveryAddressModel.AddressLine2 = createForSessionObject?.DemogInfoAddressModel?.AddressLine2;
                        }
                        if (string.IsNullOrWhiteSpace(deliveryInfoModel.DeliveryAddressModel.CityName))
                        {
                            deliveryInfoModel.DeliveryAddressModel.CityName = createForSessionObject?.DemogInfoAddressModel?.CityName;
                        }
                        if (string.IsNullOrWhiteSpace(deliveryInfoModel.DeliveryAddressModel.ZipCode))
                        {
                            deliveryInfoModel.DeliveryAddressModel.ZipCode = createForSessionObject?.DemogInfoAddressModel?.ZipCode;
                        }
                        if (deliveryInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId == null)
                        {
                            deliveryInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = createForSessionObject?.DemogInfoAddressModel?.DemogInfoSubDivisionId;
                        }
                    }
                }
                return deliveryInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        // POST: DeliveryInfo
        public PaymentInfoModel DeliveryInfo(ShoppingCartModel shoppingCartModel, DeliveryInfoModel deliveryInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                PaymentInfoModel paymentInfoModel = new PaymentInfoModel
                {
                    DeliveryInfoModel = deliveryInfoModel,
                };
                foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
                {
                    shoppingCartItemModel.ShoppingCartItemSummarys = new List<ShoppingCartItemModel>();
                }
                UpdateDeliveryAddressInfo(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                shoppingCartModel.ShoppingCartItemModelsSummary.RemoveRange(1, shoppingCartModel.ShoppingCartItemModelsSummary.Count - 1);
                CalculateTotalOrderAmount(shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateAdditionalCharges(shoppingCartModel, deliveryInfoModel, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateDiscounts(shoppingCartModel, deliveryInfoModel, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                var applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
                CorpAcctModel corpAcctModel = (((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel);
                List<SalesTaxListModel> salesTaxListModels = GetSalesTaxListModels(deliveryInfoModel.DeliveryAddressModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                List<CodeDataModel> salesTaxCaptionIds = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("SalesTaxType", "");
                CalculateSalesTax(shoppingCartModel, deliveryInfoModel, salesTaxListModels, salesTaxCaptionIds, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                try
                {
                    int indexOf1 = deliveryInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.IndexOf(';');
                    string deliveryMethodId = deliveryInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.Substring(0, indexOf1);
                    deliveryInfoModel.DeliveryMethodModel.DeliveryMethodId = (DeliveryMethodEnum)long.Parse(deliveryMethodId);
                    if (deliveryInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                    {
                        int indexOf2 = deliveryInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.IndexOf(';', indexOf1 + 1);
                        string pickupLocationId = deliveryInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.Substring(indexOf1 + 1, indexOf2 - indexOf1 - 1);
                        deliveryInfoModel.DeliveryMethodModel.PickupLocationId = long.Parse(pickupLocationId);
                        deliveryInfoModel.DeliveryMethodModel.PickupLocationDemogInfoAddressModels = applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes ? RetailSlnCache.PickupLocationDemogInfoAddressModels2 : RetailSlnCache.PickupLocationDemogInfoAddressModels1;
                    }
                    else
                    {
                        deliveryInfoModel.DeliveryMethodModel.PickupLocationId = 0;
                    }
                }
                catch
                {
                    deliveryInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId = null;
                    deliveryInfoModel.DeliveryMethodModel.DeliveryMethodId = null;
                    deliveryInfoModel.DeliveryMethodModel.PickupLocationId = null;
                }
                if (deliveryInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                {
                    ;
                }
                else
                {
                    CalculateDeliveryCharges(shoppingCartModel, deliveryInfoModel, corpAcctModel, salesTaxListModels, salesTaxCaptionIds, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                ShoppingCartItemModel shoppingCartItemModelBalanceDue = CalculateTotalInvoiceAmount(shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                GetCreditCardKVPs(creditCardProcessor, out Dictionary<string, string> creditCardKVPs, out Dictionary<string, string> creditCardDataKVPs, clientId, ipAddress, execUniqueId, loggedInUserId);
                shoppingCartModel.Checkout = true;
                paymentInfoModel.CreditCardDataModel = new CreditCardDataModel
                {
                    CreditCardKVPs = creditCardKVPs,
                    CreditCardDataKVPs = creditCardDataKVPs,
                    CreditCardProcessor = creditCardProcessor,
                    CreditCardTranType = "PURCHASE",
                    CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                    CreditCardZipCode = deliveryInfoModel.DeliveryAddressModel.ZipCode,
                    EmailAddress = deliveryInfoModel.OrderSummaryModel.EmailAddress,
                    NameAsOnCard = (deliveryInfoModel.OrderSummaryModel.FirstName + " " + deliveryInfoModel.OrderSummaryModel.LastName).Trim(),
                    TelephoneNumber = deliveryInfoModel.DeliveryDataModel.PrimaryTelephoneNum,
                    TelephoneNumberCode = deliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode,
                    TelephoneNumberCountryId = deliveryInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId,
                    TelephoneNumberFormatted = deliveryInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted,
                };
                //INR round up to int
                if (paymentInfoModel.CreditCardDataModel.CurrencyCode == "INR")
                {
                    shoppingCartItemModelBalanceDue.OrderAmountRounded = (long)Math.Ceiling(shoppingCartItemModelBalanceDue.OrderAmount.Value);
                    paymentInfoModel.CreditCardDataModel.CreditCardAmount = shoppingCartItemModelBalanceDue.OrderAmountRounded.Value.ToString("0.00");
                    paymentInfoModel.CreditCardDataModel.CreditCardAmountFormatted =
                        shoppingCartItemModelBalanceDue.OrderAmountRounded.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                }
                else
                {
                    paymentInfoModel.CreditCardDataModel.CreditCardAmount = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString();
                    paymentInfoModel.CreditCardDataModel.CreditCardAmountFormatted =
                        shoppingCartItemModelBalanceDue.OrderAmountRounded.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                }
                return paymentInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                return null;
            }
        }
        #endregion
        #endregion
        #region Commented Out Code
        //// GET: Checkout
        //public void Checkout(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfoModel = paymentInfoModel ?? new PaymentInfoModel();
        //        if (paymentInfoModel.ShoppingCartModel == null)
        //        {
        //            modelStateDictionary.AddModelError("", "Invalid shopping cart (Null)");
        //        }
        //        else
        //        {
        //            if (paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count > 0 && paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount > 0)
        //            {
        //                BuildDeliveryInfoLookupData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseMessages = new List<string>(),
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                };
        //            }
        //            else
        //            {
        //                modelStateDictionary.AddModelError("", "Empty shopping cart");
        //            }
        //        }
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
        #region PRIVATE
        // PRIVATE : AddItemToShoppingCart
        private void AddItemToShoppingCart(ref ShoppingCartModel shoppingCartModel, AddToCartModel addToCartModel, bool createShoppingCartWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                float itemSeqNum;
                CreateShoppingCartModel(ref shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                ShoppingCartItemModel shoppingCartItemModel = shoppingCartModel.ShoppingCartItemModels.FirstOrDefault(x => x.ItemId == addToCartModel.ItemId && x.ParentItemId == addToCartModel.ParentItemId && x.DoNotBreakBundle == addToCartModel.DoNotBreakBundle);
                if (shoppingCartItemModel == null)
                {
                    CreateShoppingCartItemModel(ref shoppingCartItemModel, addToCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    shoppingCartModel.ShoppingCartItemModels.Add(shoppingCartItemModel);
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createShoppingCartWIP)
                    {
                        ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                        itemSeqNum = shoppingCartModel.ShoppingCartWIPSeqNum;
                        ShoppingCartWIPAdd(addToCartModel.DoNotBreakBundle, applSessionObjectModel.CorpAcctLocationId, ref itemSeqNum, shoppingCartItemModel, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        shoppingCartModel.ShoppingCartWIPSeqNum = itemSeqNum;
                    }
                }
                else
                {
                    CreateShoppingCartItemModel(ref shoppingCartItemModel, addToCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createShoppingCartWIP)
                    {
                        ShoppingCartWIPUpd(addToCartModel.DoNotBreakBundle, shoppingCartItemModel, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                CalculateTotalOrderAmount(shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // PRIVATE: AddToCart
        private string AddToCart(ref ShoppingCartModel shoppingCartModel, AddToCartModel addToCartModel, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                        if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                        {
                            int orderQtyBundleCount = 0;
                            int orderQtyBundle;
                            foreach (var shoppingCartItemBundleModel in addToCartModel.ShoppingCartItemBundleModels)
                            {
                                int.TryParse(shoppingCartItemBundleModel.OrderQtyParm, out orderQtyBundle);
                                if (orderQtyBundle != 0)
                                {
                                    orderQtyBundleCount++;
                                }
                            }
                            if (orderQtyBundleCount == 0)
                            {
                                errorMessage += "Enter quantity for bundle item(s)";
                            }
                            addToCartModel.ParentItemId = itemId;
                        }
                        else
                        {
                            addToCartModel.ParentItemId = 0;
                        }
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
                    AddItemToShoppingCart(ref shoppingCartModel, addToCartModel, createOrderWIP, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // PRIVATE : CalculateTotalInvoiceAmount
        private ShoppingCartItemModel CalculateTotalInvoiceAmount(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            /*
            1. Calculate Total Invoice Amount
            2. Add all Summarys
            3. Add Total Order Amount + Additional Charges + Sales Tax + Delivery Charges + Taxes on Delivery Charges
            4. Subtract Coupon + GC Payment
            5. Create Balance Due
            6. Update Shopping Cart Summary
            */
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
                float totalInvoiceAmount = 0;
                foreach (var shoppingCartItemModelSummary in shoppingCartModel.ShoppingCartItemModelsSummary)
                {
                    totalInvoiceAmount += shoppingCartItemModelSummary.OrderAmount.Value;
                }
                shoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = totalInvoiceAmount;
                shoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountFormatted = totalInvoiceAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartModel.ShoppingCartItemModelsSummary.Add
                (
                    new ShoppingCartItemModel
                    {
                        OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount,
                        OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountFormatted,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
                        ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalInvoiceAmount).CodeDataDesc0,
                    }
                );
                shoppingCartModel.ShoppingCartItemModelsSummary.Add
                (
                    new ShoppingCartItemModel
                    {
                        OrderAmount = 0,
                        OrderAmountFormatted = 0.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
                        ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalAmountPaid).CodeDataDesc0,
                    }
                );
                shoppingCartModel.ShoppingCartSummaryModel.BalanceDue = totalInvoiceAmount;
                shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountFormatted;
                ShoppingCartItemModel shoppingCartItemModelBalanceDue;
                shoppingCartModel.ShoppingCartItemModelsSummary.Add
                (
                    shoppingCartItemModelBalanceDue = new ShoppingCartItemModel
                    {
                        OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
                        OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted,
                        OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                        ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
                    }
                );
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return shoppingCartItemModelBalanceDue;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount = 0;
                //shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
                //foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
                //{
                //    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
                //    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
                //    shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
                //    shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
                //    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount += shoppingCartItemModel.OrderQty;
                //    shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalcValue;
                //}
                //shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                //shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                //shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
                ////shoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                ////shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                //var shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                ////if (shoppingCartItemModelSummary != null)
                ////{
                //shoppingCartItemModelSummary.OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                //shoppingCartItemModelSummary.OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                //shoppingCartItemModelSummary.ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalOrderAmount).CodeDataDesc0;
                //shoppingCartItemModelSummary.ItemShortDesc += " (" + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + " / " + shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount + ")";
                ////}
                //shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                //if (shoppingCartItemModelSummary == null)
                //{
                //    shoppingCartModel.ShoppingCartItemModelsSummary.Add
                //    (
                //        new ShoppingCartItemModel
                //        {
                //            OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
                //            OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted,
                //            OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                //            ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
                //        }
                //    );
                //}
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while calculating total order amount");
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateTotalOrderAmount
        private void CalculateTotalOrderAmount(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
                shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
                shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
                shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
                shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount = 0;
                shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
                foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
                {
                    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount += shoppingCartItemModel.OrderQty;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalcValue;
                }
                shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
                //shoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                //shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                var shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                //if (shoppingCartItemModelSummary != null)
                //{
                shoppingCartItemModelSummary.OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                shoppingCartItemModelSummary.OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                shoppingCartItemModelSummary.ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalOrderAmount).CodeDataDesc0;
                shoppingCartItemModelSummary.ItemShortDesc += " (" + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + " / " + shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount + ")";
                //}
                //Do this in Total Invoice Amount
                //shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                //if (shoppingCartItemModelSummary == null)
                //{
                //    shoppingCartModel.ShoppingCartItemModelsSummary.Add
                //    (
                //        new ShoppingCartItemModel
                //        {
                //            OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
                //            OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted,
                //            OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                //            ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
                //        }
                //    );
                //}
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while calculating total order amount");
                throw;
            }
            finally
            {
            }
        }
        #region Commented Out Code
        //private void CalculateTotalInvoiceAmount(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
        //        foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
        //        {
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount += shoppingCartItemModel.OrderQty;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalcValue;
        //        }
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
        //        //shoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
        //        //shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
        //        var shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
        //        //if (shoppingCartItemModelSummary != null)
        //        //{
        //        shoppingCartItemModelSummary.OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
        //        shoppingCartItemModelSummary.OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
        //        shoppingCartItemModelSummary.ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalOrderAmount).CodeDataDesc0;
        //        shoppingCartItemModelSummary.ItemShortDesc += " (" + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + " / " + shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount + ")";
        //        //}
        //        shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
        //        if (shoppingCartItemModelSummary == null)
        //        {
        //            shoppingCartModel.ShoppingCartItemModelsSummary.Add
        //            (
        //                new ShoppingCartItemModel
        //                {
        //                    OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
        //                    OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
        //                    ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
        //                }
        //            );
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        modelStateDictionary.AddModelError("", "Error while calculating total order amount");
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        #region Commented Out Code
        // PRIVATE : CalculateTotalOrderAmount
        //private void CalculateTotalOrderAmount(ref ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount = 0;
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
        //        foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
        //        {
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount += shoppingCartItemModel.OrderQty;
        //            shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalcValue;
        //        }
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
        //        shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
        //        shoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
        //        shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
        //        var shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
        //        //if (shoppingCartItemModelSummary != null)
        //        //{
        //        shoppingCartItemModelSummary.OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
        //        shoppingCartItemModelSummary.OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
        //        shoppingCartItemModelSummary.ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalOrderAmount).CodeDataDesc0;
        //        shoppingCartItemModelSummary.ItemShortDesc += " (" + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + " / " + shoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount + ")";
        //        //}
        //        //Do the below later in Total Invoice Payment
        //        //shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
        //        //if (shoppingCartItemModelSummary == null)
        //        //{
        //        //    shoppingCartModel.ShoppingCartItemModelsSummary.Add
        //        //    (
        //        //        new ShoppingCartItemModel
        //        //        {
        //        //            OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
        //        //            OrderAmountFormatted = shoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted,
        //        //            OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
        //        //            ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
        //        //        }
        //        //    );
        //        //}
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        modelStateDictionary.AddModelError("", "Error while calculating total order amount");
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        // PRIVATE : CheckoutValidate
        private bool CheckoutValidate(ShoppingCartModel shoppingCartModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool returnValue;
                if (shoppingCartModel == null)
                {
                    returnValue = false;
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItemModels.Count > 0 && shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount > 0)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return returnValue;
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
        // PRIVATE : CreatePaymentInfoModel
        private void CreateShoppingCartModel(ref ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                shoppingCartModel = shoppingCartModel ?? new ShoppingCartModel();
                shoppingCartModel = shoppingCartModel ?? new ShoppingCartModel
                {
                    Checkout = false,
                    ShowDetail = false,
                };
                shoppingCartModel.ShoppingCartItemModels = shoppingCartModel.ShoppingCartItemModels ?? new List<ShoppingCartItemModel>();
                shoppingCartModel.ShoppingCartItemModelsSummary = shoppingCartModel.ShoppingCartItemModelsSummary ?? new List<ShoppingCartItemModel>
                {
                    new ShoppingCartItemModel
                    {
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
                    }
                };
                shoppingCartModel.ShoppingCartSummaryModel = shoppingCartModel.ShoppingCartSummaryModel ?? new ShoppingCartSummaryModel
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
        // PRIVATE : CalculateAdditionalCharges
        private void CalculateAdditionalCharges(ShoppingCartModel shoppingCartModel, DeliveryInfoModel deliveryInfoModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            /*
            1. If Additional Charges > 0
            2. If does not exist in summary add new
            3. Update amount and format amount
            */
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (deliveryInfoModel.OrderSummaryModel.AdditionalCharges != null && deliveryInfoModel.OrderSummaryModel.AdditionalCharges > 0)
                {
                    ShoppingCartItemModel shoppingCartItemModelAdditionalCharges = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AdditionalCharges);
                    if (shoppingCartItemModelAdditionalCharges == null)
                    {
                        shoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            shoppingCartItemModelAdditionalCharges = new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = deliveryInfoModel.OrderSummaryModel.AdditionalCharges,
                                ItemShortDesc = "Additional Charges",
                                OrderComments = null,
                                OrderQty = 1,
                                OrderDetailTypeId = OrderDetailTypeEnum.AdditionalCharges,
                            }
                        );
                    }
                    shoppingCartItemModelAdditionalCharges.OrderAmount = deliveryInfoModel.OrderSummaryModel.AdditionalCharges;
                    shoppingCartItemModelAdditionalCharges.OrderAmountFormatted = deliveryInfoModel.OrderSummaryModel.AdditionalCharges.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while processing additional charges");
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateDiscounts
        private void CalculateDiscounts(ShoppingCartModel shoppingCartModel, DeliveryInfoModel deliveryInfoModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            /*
            1. If Coupon# exists in Coupon List add line item for Coupon Discount
            2. For referrer commission will be calculated later
            3. Commission 3% and Coupon 2%
            4. Order will calculate for 2%
            5. Commission and Coupon need not coexists
            6. For Commision there will be coupon not the other way
            */
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                CouponListModel couponListModel = ApplicationDataContext.CouponListGet(deliveryInfoModel.CouponPaymentModel.CouponNumber, deliveryInfoModel.OrderSummaryModel.OrderDateTime, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (couponListModel != null)
                {
                    var shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                    float orderAmount = -1 * shoppingCartItemModelSummary.OrderAmount.Value * couponListModel.DiscountPercent / 100;
                    var shoppingCartItemModelSummaryDiscount = shoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.Discount);
                    if (shoppingCartItemModelSummaryDiscount != null)
                    {//Customer could have changed the Coupon#
                        shoppingCartModel.ShoppingCartItemModelsSummary.Remove(shoppingCartItemModelSummaryDiscount);
                    }
                    shoppingCartModel.ShoppingCartItemModelsSummary.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemId = null,
                            ItemRate = 0,
                            ItemShortDesc = "Coupon Discount " + couponListModel.DiscountPercent + "%",
                            OrderAmount = orderAmount,
                            OrderAmountFormatted = orderAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            OrderComments = null,
                            OrderQty = shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                            OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                        }
                    );
                }
                #region Will handle referral List
                //ReferralListModel referralListModel = ApplicationDataContext.ReferralListGet(createForSessionObject.PersonId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                //if (referralListModel == null)
                //{
                //    if (!string.IsNullOrWhiteSpace(deliveryInfoModel.CouponPaymentModel?.CouponNumber))
                //    {
                //    }
                //}
                //else
                //{
                //    var shoppingCartItemModelSummary = shoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
                //    float orderAmount = -1 * shoppingCartItemModelSummary.OrderAmount.Value * (referralListModel.CommissionPercent + referralListModel.DiscountPercent) / 100;
                //    shoppingCartModel.ShoppingCartItemModelsSummary.Add
                //    (
                //        new ShoppingCartItemModel
                //        {
                //            ItemId = null,
                //            ItemRate = 0,
                //            ItemShortDesc = "Referral Fee " + (referralListModel.CommissionPercent + referralListModel.DiscountPercent) + "%",
                //            OrderAmount = orderAmount,
                //            OrderAmountFormatted = orderAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                //            OrderComments = null,
                //            OrderQty = shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                //            OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                //        }
                //    );
                //}
                #endregion
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while calculating discounts");
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateSalesTax
        private void CalculateDeliveryCharges(ShoppingCartModel shoppingCartModel, DeliveryInfoModel deliveryInfoModel, CorpAcctModel corpAcctModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (corpAcctModel.ShippingAndHandlingCharges == YesNoEnum.Yes)
                {
                    DeliveryChargeModel deliveryChargeModel = GetDeliveryChargeModel(deliveryInfoModel.DeliveryAddressModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (deliveryChargeModel != null)
                    {
                        var shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
                        var shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded;
                        var fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
                        var shoppingCartItemSummaryModelsFromCount = shoppingCartModel.ShoppingCartItemModelsSummary.Count;
                        float shippingAndHandlingFuelSalesTaxAmountTotal = 0;
                        List<ShoppingCartItemModel> shoppingCartItemModelTemps = new List<ShoppingCartItemModel>();
                        shoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = shippingAndHandlingChargesRate,
                                ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
                                OrderAmount = shippingAndHandlingChargesAmount + fuelCharges,
                                OrderAmountFormatted = (shippingAndHandlingChargesAmount + fuelCharges).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                OrderComments = null,
                                OrderQty = shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                OrderDetailTypeId = OrderDetailTypeEnum.DoNotShow,
                            }
                        );
                        shippingAndHandlingFuelSalesTaxAmountTotal = shippingAndHandlingChargesAmount + fuelCharges;
                        foreach (var salesTaxListModel in salesTaxListModels)
                        {
                            var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                            shoppingCartModel.ShoppingCartItemModelsSummary.Add
                            (
                                new ShoppingCartItemModel
                                {
                                    ItemId = null,
                                    ItemRate = shippingAndHandlingChargesRate,
                                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
                                    OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
                                    OrderAmountFormatted = ((shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                    OrderComments = null,
                                    OrderQty = shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                    OrderDetailTypeId = OrderDetailTypeEnum.DoNotShow,
                                }
                            );
                            shippingAndHandlingFuelSalesTaxAmountTotal += (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f;
                        }
                        shoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = shippingAndHandlingChargesRate,
                                ItemShortDesc = "Shipping, Handling, Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + shippingAndHandlingFuelSalesTaxAmountTotal + " " + shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime + " with Sales Tax",
                                OrderAmount = shippingAndHandlingFuelSalesTaxAmountTotal,
                                OrderAmountFormatted = (shippingAndHandlingChargesAmount + fuelCharges).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                OrderComments = null,
                                OrderQty = shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                            }
                        );
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while calculating sales tax");
                throw;
            }
            finally
            {
            }
        }
        // PRIVATE : CalculateSalesTax
        private void CalculateSalesTax(ShoppingCartModel shoppingCartModel, DeliveryInfoModel deliveryInfoModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                bool totalSalesTaxAmountFlag = false;
                float? orderAmout, salesTaxAmount, totalSalesTaxAmount = 0;
                CodeDataModel salesTaxCaptionId;
                foreach (var salesTaxListModel in salesTaxListModels)
                {
                    salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                    if (salesTaxListModel.LineItemLevelName == "SUMMARY")
                    {
                        orderAmout = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount * salesTaxListModel.SalesTaxRate / 100f;
                        shoppingCartModel.ShoppingCartItemModelsSummary.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
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
                        totalSalesTaxAmountFlag = true;
                        foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
                        {
                            var itemSpecValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItemModel.ItemId).ItemItemSpecModels[salesTaxListModel.SalesTaxCaptionId.ToString()].ItemSpecValue;
                            salesTaxAmount = float.Parse(itemSpecValue) * shoppingCartItemModel.OrderAmount.Value / 100f;
                            totalSalesTaxAmount += salesTaxAmount;
                            shoppingCartItemModel.ShoppingCartItemSummarys.Add
                            (
                                new ShoppingCartItemModel
                                {
                                    ItemId = null,
                                    ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                    ItemRate = float.Parse(itemSpecValue),
                                    ItemRateFormatted = (float.Parse(itemSpecValue) / 100f).ToString("#0.00%"),
                                    OrderAmount = salesTaxAmount,
                                    OrderAmountFormatted = salesTaxAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                                    OrderDetailTypeId = OrderDetailTypeEnum.DoNotShow,
                                }
                            );
                        }
                    }
                }
                if (totalSalesTaxAmountFlag)
                {
                    shoppingCartModel.ShoppingCartItemModelsSummary.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemId = null,
                            ItemShortDesc = "Sales Tax",
                            ItemRate = null,
                            ItemRateFormatted = null,
                            OrderAmount = totalSalesTaxAmount,
                            OrderAmountFormatted = totalSalesTaxAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while calculating sales tax");
                throw;
            }
            finally
            {
            }
        }
        #region Commenting Code for now - Need this after fixing
        //// PRIVATE : CalculateSalesTax
        //private void CalculateSalesTax(ShoppingCartModel shoppingCartModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        float? orderAmout;
        //        foreach (var salesTaxListModel in salesTaxListModels)
        //        {
        //            var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
        //            if (salesTaxListModel.LineItemLevelName == "SUMMARY")
        //            {
        //                orderAmout = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount * salesTaxListModel.SalesTaxRate / 100f;
        //                shoppingCartModel.ShoppingCartItemModelsSummary.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        ItemId = null,
        //                        ItemRate = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
        //                        ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
        //                        OrderAmount = orderAmout,
        //                        OrderAmountFormatted = orderAmout.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //                        OrderComments = null,
        //                        OrderQty = 1,
        //                        OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
        //                    }
        //                );
        //            }
        //            else
        //            {
        //                float totalSalesTaxAmount = 0, salesTaxAmount;
        //                foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
        //                {
        //                    var itemSpecValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItemModel.ItemId).ItemItemSpecModels[salesTaxListModel.SalesTaxCaptionId.ToString()].ItemSpecValue;
        //                    salesTaxAmount = float.Parse(itemSpecValue) * shoppingCartItemModel.OrderAmount.Value / 100f;
        //                    totalSalesTaxAmount += salesTaxAmount;
        //                    shoppingCartItemModel.ShoppingCartItemSummarys.Add
        //                    (
        //                        new ShoppingCartItemModel
        //                        {
        //                            ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
        //                            ItemRate = float.Parse(itemSpecValue),
        //                            ItemRateFormatted = (float.Parse(itemSpecValue) / 100f).ToString("#0.00%"),
        //                            OrderAmount = salesTaxAmount,
        //                            OrderAmountFormatted = salesTaxAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //                        }
        //                    );
        //                }
        //                shoppingCartModel.ShoppingCartItemModelsSummary.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        ItemId = null,
        //                        ItemRate = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
        //                        ItemShortDesc = salesTaxCaptionId.CodeDataDesc0,
        //                        OrderAmount = totalSalesTaxAmount,
        //                        OrderAmountFormatted = totalSalesTaxAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //                        OrderComments = null,
        //                        OrderQty = 1,
        //                        OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
        //                    }
        //                );
        //            }
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        modelStateDictionary.AddModelError("", "Error while calculating sales tax");
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        //// PUBLIC: CreateInvoice
        //public void CreateInvoiceProd(PaymentInfoModel paymentInfoModel, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //If it is not Final Invoice - create invoice as Html and Pdf file and email
        //    //Set the Invoice Type to Final - create invoice as Html and Pdf
        //    //Just in case reset the invoice type
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ArchLibBL archLibBL = new ArchLibBL();
        //        string orderFileName = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString();
        //        CodeDataModel codeDataModel;
        //        if (paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
        //        {
        //            codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("InvoiceType", execUniqueId).First(x => x.CodeDataNameId == 900);
        //        }
        //        else
        //        {
        //            codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("InvoiceType", execUniqueId).First(x => x.CodeDataNameId == 100);
        //        }
        //        paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceType = codeDataModel.CodeDataDesc0;
        //        orderFileName += "_" + (int)paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId;
        //        paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceFileNamePdf = orderFileName + ".pdf";
        //        string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceDataSubject", paymentInfoModel);
        //        string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
        //        string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", paymentInfoModel);
        //        PDFUtility pDFUtility = new PDFUtility();
        //        string invoiceDirectoryName = Utilities.GetServerMapPath("~/Invoices/");
        //        StreamWriter streamWriter = new StreamWriter(invoiceDirectoryName + orderFileName + ".html");
        //        streamWriter.Write(emailBodyHtml);
        //        streamWriter.Write(Environment.NewLine);
        //        streamWriter.Close();
        //        string pDFFullFileName = invoiceDirectoryName + orderFileName + ".pdf";
        //        paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceFullFileNamePdf = pDFFullFileName;
        //        pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
        //        List<string> emailAttachmentFileNames = new List<string>
        //        {
        //            pDFFullFileName,
        //        };
        //        var toEmailAddresss = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.EmailAddress;// + ";" + ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", "ToEmailAddress");
        //        if (createForSessionObject.EmailAddress.ToLower() != sessionObjectModel.EmailAddress.ToLower())
        //        {
        //            toEmailAddresss += ";" + sessionObjectModel.EmailAddress;
        //        }
        //        //archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceHtmlString = emailBodyHtml;
        //        if (paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
        //        {
        //        }
        //        else
        //        {
        //            paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId = InvoiceTypeEnum.FinalInvoice;
        //            paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceType = "Tax Invoice";
        //            emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
        //            orderFileName = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString() + "_" + (int)paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId;
        //            streamWriter = new StreamWriter(invoiceDirectoryName + orderFileName + ".html");
        //            streamWriter.Write(emailBodyHtml);
        //            streamWriter.Write(Environment.NewLine);
        //            streamWriter.Close();
        //            pDFFullFileName = invoiceDirectoryName + orderFileName + ".pdf";
        //            pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
        //        }
        //        paymentInfoModel = null;
        //        return;
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
        // GET : ItemBundleData
        public ItemBundleDataModel ItemBundleData(long parentItemId, PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemBundleDataModel itemBundleDataModel;
            try
            {
                ItemModel itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == parentItemId);
                ParentItemBundleModel parentItemBundleModel;
                if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                {
                    parentItemBundleModel = RetailSlnCache.ParentItemBundleModels[parentItemId];
                }
                else
                {
                    parentItemBundleModel = null;
                }
                itemBundleDataModel = new ItemBundleDataModel
                {
                    CurrencySymbol = RetailSlnCache.CurrencySymbol,
                    //ItemModel = itemModel,
                    ParentItemBundleModel = parentItemBundleModel,
                    //ParentItemId = parentItemId,
                    //ShoppingCartItemBundleModels = shoppingCartItemBundleModels,
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
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Bundle", "parentItemId", parentItemId.ToString());
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
        public ItemCatalogModel ItemCatalog(string parentCategoryIdParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                var aspNetRoleNameProxy = createForSessionObject?.AspNetRoleNameProxy;
                if (string.IsNullOrEmpty(aspNetRoleNameProxy))
                {
                    aspNetRoleNameProxy = "DEFAULTROLE";
                }
                Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleNameProxy];
                if (parentCategoryId == 0)
                {
                    parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId01"].KVPValueData);
                }
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                string itemCatalogHtmlDirName = Utilities.GetServerMapPath("~/Files/ItemCatalog/");
                string itemCatalogHtmlFileName = $@"ItemCatalog_{aspNetRoleNameProxy}_{corpAcctId}_{parentCategoryId}.html";
                ItemCatalogModel itemCatalogModel = new ItemCatalogModel
                {
                    ItemCatalogHtmlFileName = itemCatalogHtmlFileName,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                return itemCatalogModel;
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
        // PUBLIC: ItemCatalogCreateAll
        public void ItemCatalogCreateAll(SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string itemCatalogFilesPath = Utilities.GetServerMapPath("~/Files/ItemCatalog/");
                DirectoryInfo directoryInfo = new DirectoryInfo(itemCatalogFilesPath);
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    if (fileInfo.FullName.IndexOf("@Temp.txt") == -1)
                    {
                        fileInfo.Delete();
                    }
                }

                // Create an instance of the controller
                controller = new BaseController(); // Replace HomeController with your controller name

                // Create a controller context (optional, but good practice for some scenarios)
                HttpContextWrapper httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Base"); // Replace Home with your controller name
                routeData.Values.Add("action", "Index"); // Replace StartupAction with your action name

                var requestContext = new RequestContext(httpContextWrapper, routeData);
                controller.ControllerContext = new ControllerContext(requestContext, controller);

                ItemCatalogCreate(itemCatalogFilesPath, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> itemDiscountModels);
                itemDiscountModels = itemDiscountModels ?? new Dictionary<long, ItemDiscountModel>();
                ItemMasterAttributesModel itemMasterAttributesModel = new ItemMasterAttributesModel
                {
                    ItemMasterId = itemMasterId,
                    OrderItem1Model = new OrderItem1Model
                    {
                        ItemDiscountModels = itemDiscountModels,
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
        public OrderItemModel OrderItem(string aspNetRoleNameProxy, string parentCategoryIdParm, string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                aspNetRoleNameProxy = sessionObjectModel?.AspNetRoleNameProxy;
                if (string.IsNullOrEmpty(aspNetRoleNameProxy))
                {
                    aspNetRoleNameProxy = "DEFAULTROLE";
                }
                Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleNameProxy];
                if (parentCategoryId == 0)
                {
                    parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId00"].KVPValueData);
                }
                string viewName = aspNetRoleKVPs["ViewName00"].KVPValueData;
                httpSessionStateBase["LastVisitedParentCategoryId"] = parentCategoryId;
                httpSessionStateBase["LastVisitedPageNum"] = pageNum;
                Dictionary<long, List<CategoryItemMasterHierModel>> parentCategoryItemMasterModels;
                List<CategoryItemMasterHierModel> itemMasterModels;
                if (RetailSlnCache.AspNetRoleParentCategoryItemMasterModels.TryGetValue(aspNetRoleNameProxy, out parentCategoryItemMasterModels))
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
                long totalRowCount = RetailSlnCache.AspNetRoleParentCategoryItemMasterModels[aspNetRoleNameProxy][parentCategoryId].Count;
                long pageCount = totalRowCount / pageSize;
                if (totalRowCount % pageSize != 0)
                {
                    pageCount++;
                }
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCart"];
                CreateShoppingCartModel(ref shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                shoppingCartModel.Checkout = false;
                httpSessionStateBase["ShoppingCart"] = shoppingCartModel;
                CalculateTotalOrderAmount(shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                string categoryOrItem = "Item";
                RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> itemDiscountModels);
                itemDiscountModels = itemDiscountModels == null ? new Dictionary<long, ItemDiscountModel>() : itemDiscountModels;
                OrderItemModel orderItemModel = new OrderItemModel
                {
                    AspNetRoleName = aspNetRoleNameProxy,
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
        #region Commenting Code for now - Need this after fixing
        //// GET : PaymentInfo4
        //public void PaymentInfo4(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{//Stripe
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        ShoppingCartItemModel shoppingCartItemModelBalanceDue;
        //        shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
        //        shoppingCartItemModelBalanceDue.OrderAmountRounded = (long)(shoppingCartItemModelBalanceDue.OrderAmount * 100);
        //        paymentInfoModel.ShoppingCartModel.Checkout = true;
        //        string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //        GetCreditCardKVPs(creditCardProcessor, out Dictionary<string, string> creditCardKVPs, out Dictionary<string, string> creditCardDataKVPs, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        paymentInfoModel.CreditCardDataModel = new CreditCardDataModel
        //        {
        //            CreditCardAmount = shoppingCartItemModelBalanceDue.OrderAmountRounded.ToString(),
        //            CreditCardAmountFormatted = shoppingCartItemModelBalanceDue.OrderAmountFormatted,
        //            CreditCardKVPs = creditCardKVPs,
        //            CreditCardDataKVPs = creditCardDataKVPs,
        //            CreditCardProcessor = creditCardProcessor,
        //            CreditCardTranType = "PURCHASE",
        //            CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
        //            CreditCardZipCode = paymentInfoModel.DeliveryAddressModel.ZipCode,
        //            EmailAddress = paymentInfoModel.OrderSummaryModel.EmailAddress,
        //            NameAsOnCard = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim(),
        //            TelephoneNumber = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum,
        //            TelephoneNumberCode = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode,
        //            TelephoneNumberCountryId = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId,
        //            TelephoneNumberFormatted = paymentInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted,
        //        };
        //        return;
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
        //// POST : PaymentInfo4Success
        //public void PaymentInfo4Success(PaymentInfoModel paymentInfoModel, string paymentIntent_status, string paymentIntent_payment_method, string paymentIntent_id, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{//RazorpayReturn
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        paymentInfoModel.CreditCardDataModel.CreditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //        paymentInfoModel.CreditCardDataModel.ProcessMessage = paymentIntent_status;
        //        paymentInfoModel.CreditCardDataModel.RequestData = "";
        //        string responseDataJsonString = "";
        //        responseDataJsonString += "{" + Environment.NewLine;
        //        responseDataJsonString += "CreditCardAmount: \"" + paymentInfoModel.CreditCardDataModel.CreditCardAmount + "\"" + Environment.NewLine;
        //        responseDataJsonString += "paymentIntent_payment_method: \"" + paymentIntent_payment_method + "\"" + Environment.NewLine;
        //        responseDataJsonString += "paymentIntent_id: \"" + paymentIntent_id + "\"" + Environment.NewLine;
        //        responseDataJsonString += "}" + Environment.NewLine;
        //        paymentInfoModel.CreditCardDataModel.ResponseData = responseDataJsonString;
        //        paymentInfoModel.CreditCardDataModel.StatusNameDesc = "SUCCESS";
        //        creditCardServiceBL.CreateCreditCardData(paymentInfoModel.CreditCardDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

        //        ShoppingCartItemModel shoppingCartItemModelAmountPaid, shoppingCartItemModelBalanceDue;//, shoppingCartItemModelTotalInvoiceAmount;
        //        shoppingCartItemModelAmountPaid = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
        //        shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);

        //        shoppingCartItemModelAmountPaid.OrderAmount = shoppingCartItemModelBalanceDue.OrderAmount;
        //        shoppingCartItemModelAmountPaid.OrderAmountFormatted = shoppingCartItemModelAmountPaid.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        shoppingCartItemModelAmountPaid.OrderComments = "Id " + paymentIntent_id + "; Method " + paymentIntent_payment_method;

        //        shoppingCartItemModelBalanceDue.OrderAmount = 0;
        //        shoppingCartItemModelBalanceDue.OrderAmountFormatted = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");

        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = shoppingCartItemModelAmountPaid.OrderAmount;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaidFormatted = shoppingCartItemModelAmountPaid.OrderAmountFormatted;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = shoppingCartItemModelBalanceDue.OrderAmount;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = shoppingCartItemModelBalanceDue.OrderAmountFormatted;

        //        AssignAuthorizedData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);

        //        Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
        //        paymentRefOptions["PaymentRefOption1"] = new Dictionary<string, string>();
        //        paymentRefOptions["PaymentRefOption1"]["Key"] = "Id";
        //        paymentRefOptions["PaymentRefOption1"]["Value"] = paymentIntent_id;
        //        paymentRefOptions["PaymentRefOption2"] = new Dictionary<string, string>();
        //        paymentRefOptions["PaymentRefOption2"]["Key"] = "Method";
        //        paymentRefOptions["PaymentRefOption1"]["Value"] = paymentIntent_payment_method;

        //        CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        paymentInfoModel.HideShoppingCart = true;
        //        paymentInfoModel.ShoppingCartModel.Checkout = true;
        //        OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        return;
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
        // POST : PaymentInfo1
        public string PaymentInfo1(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//CreditSale
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                //paymentInfoModel.CompleteOrderModel.PaymentAmount = paymentInfoModel.CompleteOrderModel.PaymentAmount ?? 0;
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");

                //long codeDataNameId = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
                //CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaidFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");

                Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
                CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                //CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                //paymentInfoModel.HideShoppingCart = true;
                paymentInfoModel.ShoppingCartModel.Checkout = true;

                //if (paymentInfoModel.OrderHeaderWIPModel != null && paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId != null && paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId != 0)
                //{
                //    OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                //}

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
                //BuildCreditCardDataModel(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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

                    //AssignAuthorizedData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);

                    Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
                    paymentRefOptions["PaymentRefOption1"] = new Dictionary<string, string>();
                    paymentRefOptions["PaymentRefOption1"]["Ord# : "] = razorpay_order_id;
                    paymentRefOptions["PaymentRefOption2"] = new Dictionary<string, string>();
                    paymentRefOptions["PaymentRefOption2"]["Ref# : "] = razorpay_payment_id;
                    CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //paymentInfoModel.HideShoppingCart = true;
                    paymentInfoModel.ShoppingCartModel.Checkout = true;
                    //OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        }        // POST : PaymentInfo9
        public void PaymentInfo9(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Credit Card
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                paymentInfoModel.CreditCardDataModel.CreditCardAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString();
                paymentInfoModel.CreditCardDataModel.CreditCardAmountFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
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

                    //AssignAuthorizedData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);

                    Dictionary<string, Dictionary<string, string>> paymentRefOptions = new Dictionary<string, Dictionary<string, string>>();
                    paymentRefOptions["PaymentRefOption1"] = new Dictionary<string, string>();
                    paymentRefOptions["PaymentRefOption1"]["Ref# : "] = creditCardDataModel.ProcessMessagesSuccess["PaymentRefNum"];
                    paymentRefOptions["PaymentRefOption2"] = new Dictionary<string, string>();
                    CreateOrder(paymentInfoModel, paymentRefOptions, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //paymentInfoModel.HideShoppingCart = true;
                    paymentInfoModel.ShoppingCartModel.Checkout = true;
                    //OrderWIPDel(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // PUBLIC : PaymentInfo9Validate
        public void PaymentInfo9Validate(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Credit Card
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                CreditCardDataModel creditCardDataModel = paymentInfoModel.CreditCardDataModel;
                long.TryParse(creditCardDataModel.CreditCardNumber, out long tempLong);
                if (tempLong > 0)
                {
                    if (creditCardDataModel.CreditCardNumber.Length >= 13 && creditCardDataModel.CreditCardNumber.Length <= 19)
                    {
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("CreditCardNumber", "Invalid credit card number");
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("CreditCardNumber", "Invalid credit card number");
                }
                long.TryParse(creditCardDataModel.CreditCardSecCode, out tempLong);
                if (tempLong > 0)
                {
                    if (creditCardDataModel.CreditCardSecCode.Length >= 3 && creditCardDataModel.CreditCardSecCode.Length <= 4)
                    {
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("CreditCardSecCode", "Invalid CVV (Sec Code)");
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("CreditCardSecCode", "Invalid CVV (Sec Code)");
                }
                long.TryParse(creditCardDataModel.CreditCardExpMM, out tempLong);
                if (tempLong > 0 && tempLong < 13)
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CreditCardExpMM", "Invalid month");
                }
                long currentYear = DateTime.Now.Year;
                long currentYearMonth = long.Parse(DateTime.Now.ToString("yyyyMM"));
                long.TryParse(creditCardDataModel.CreditCardExpYear, out tempLong);
                if (tempLong >= currentYear && tempLong < currentYear + 15)
                {
                    if (long.Parse(creditCardDataModel.CreditCardExpYear + creditCardDataModel.CreditCardExpMM) >= currentYearMonth)
                    {
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("CreditCardExpMM", "Invalid month");
                        modelStateDictionary.AddModelError("CreditCardExpYear", "Invalid year");
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("CreditCardExpYear", "Invalid year");
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
        #region Commenting Code for now - Need this after fixing
        //// PRIVATE: AssignAuthorized
        //private void AssignAuthorizedData(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
        //    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");

        //    long codeDataNameId = paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
        //    CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
        //    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
        //    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;

        //    return;
        //}
        // POST: RemoveFromCart
        public void RemoveFromCart(ShoppingCartModel shoppingCartModel, int removeFromIndex, bool createOrderWIP, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (removeFromIndex > -1 && removeFromIndex < shoppingCartModel.ShoppingCartItemModels.Count)
                {
                    if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && shoppingCartModel.ShoppingCartItemModels.Count > 0)
                    {
                        ApplicationDataContext.OpenSqlConnection();
                        var sqlStmtDel = BuildShoppingCartWIPDelIds(shoppingCartModel.ShoppingCartItemModels[removeFromIndex], sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId);
                        ApplicationDataContext.ShoppingCartWIPDel(sqlStmtDel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.CloseSqlConnection();
                    }
                    shoppingCartModel.ShoppingCartItemModels.RemoveAt(removeFromIndex);
                    CalculateTotalOrderAmount(shoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        #endregion
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
                RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> itemDiscountModels);
                itemDiscountModels = itemDiscountModels ?? new Dictionary<long, ItemDiscountModel>();
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
                        ItemDiscountModels = itemDiscountModels,
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
        // PUBLIC: CreateShoppingCartWIP
        public void ShoppingCartWIPCreate(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                /*
                1. If Shopping Cart has items insert into table
                2. Above is coming - logging in after adding to cart
                3. Probably delete existing items in the table
                4. If Shoppig Cart has no items
                5. Get items from table
                6. Add it to cart
                */
                ApplicationDataContext.OpenSqlConnection();
                if (shoppingCartModel?.ShoppingCartItemModels?.Count > 0)
                {
                    //Do we need to delete all items in the table prior to insert
                    //This might avoid duplicates
                    ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
                    float itemSeqNum = shoppingCartModel.ShoppingCartWIPSeqNum;
                    foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
                    {
                        ShoppingCartWIPAdd(shoppingCartItemModel.DoNotBreakBundle, applSessionObjectModel.CorpAcctLocationId, ref itemSeqNum, shoppingCartItemModel, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    shoppingCartModel.ShoppingCartWIPSeqNum = itemSeqNum;
                }
                //AddToCartModel addToCartModel = new AddToCartModel();
                //foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
                //{
                //    addToCartModel.ItemIdParm = shoppingCartItemModel.ItemIdParm;
                //    addToCartModel.OrderQtyParm = shoppingCartItemModel.OrderQtyParm;
                //    addToCartModel.DoNotBreakBundleParm = shoppingCartItemModel.DoNotBreakBundle.ToString();
                //    AddToCart(ref shoppingCartModel, addToCartModel, false, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                //}
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                //ApplicationDataContext.CloseSqlConnection();
            }
        }
        #region Commented Out Code
        //// PUBLIC: BuildPaymentInfoFromDeliveryInfoPost
        //public void BuildPaymentInfoFromDeliveryInfoPost(ref PaymentInfoModel paymentInfoModel, PaymentInfoModel paymentInfoModelTemp, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    if (sessionObjectModel == null || createForSessionObject == null || paymentInfoModelTemp == null)
        //    {
        //        modelStateDictionary.AddModelError("", "Invalid session - User not logged in");
        //    }
        //    else
        //    {
        //        //sessionObjectModel = null;
        //        try
        //        {
        //            int indexOf1 = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.IndexOf(';');
        //            string deliveryMethodId = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.Substring(0, indexOf1);
        //            paymentInfoModel.DeliveryMethodModel.DeliveryMethodId = (DeliveryMethodEnum)long.Parse(deliveryMethodId);
        //            var applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
        //            var corpAcctLocationModel = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels.First(x => x.CorpAcctLocationId == applSessionObjectModel.CorpAcctLocationId);
        //            if (paymentInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
        //            {
        //                int indexOf2 = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.IndexOf(';', indexOf1 + 1);
        //                string pickupLocationId = paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId.Substring(indexOf1 + 1, indexOf2 - indexOf1 - 1);
        //                paymentInfoModel.DeliveryMethodModel.PickupLocationId = long.Parse(pickupLocationId);
        //                paymentInfoModel.DeliveryMethodModel.PickupLocationDemogInfoAddressModels = applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes ? RetailSlnCache.PickupLocationDemogInfoAddressModels2 : RetailSlnCache.PickupLocationDemogInfoAddressModels1;
        //            }
        //            else
        //            {
        //                paymentInfoModel.DeliveryMethodModel.PickupLocationId = 0;
        //            }
        //        }
        //        catch
        //        {
        //            paymentInfoModel.DeliveryMethodModel.DeliveryMethodIdPickupLocationId = null;
        //            paymentInfoModel.DeliveryMethodModel.DeliveryMethodId = null;
        //            paymentInfoModel.DeliveryMethodModel.PickupLocationId = null;
        //        }
        //        paymentInfoModel.OrderSummaryModel.AspNetUserId = paymentInfoModelTemp.OrderSummaryModel.AspNetUserId;
        //        paymentInfoModel.OrderSummaryModel.CorpAcctModel = paymentInfoModelTemp.OrderSummaryModel.CorpAcctModel;
        //        paymentInfoModel.OrderSummaryModel.CreatedByEmailAddress = paymentInfoModelTemp.OrderSummaryModel.CreatedByEmailAddress;
        //        paymentInfoModel.OrderSummaryModel.CreatedByFirstName = paymentInfoModelTemp.OrderSummaryModel.CreatedByFirstName;
        //        paymentInfoModel.OrderSummaryModel.CreatedByLastName = paymentInfoModelTemp.OrderSummaryModel.CreatedByLastName;
        //        //paymentInfoModel.OrderSummaryModel.EmailAddress = paymentInfoModelTemp.OrderSummaryModel.EmailAddress;
        //        paymentInfoModel.OrderSummaryModel.InvoiceTypeId = paymentInfoModelTemp.OrderSummaryModel.InvoiceTypeId;
        //        paymentInfoModel.OrderSummaryModel.OrderDateTime = paymentInfoModelTemp.OrderSummaryModel.OrderDateTime;
        //        paymentInfoModel.OrderSummaryModel.OrderHeaderId = paymentInfoModelTemp.OrderSummaryModel.OrderHeaderId;
        //        paymentInfoModel.OrderSummaryModel.PersonId = paymentInfoModelTemp.OrderSummaryModel.PersonId;
        //        paymentInfoModel.OrderSummaryModel.TelephoneCode = paymentInfoModelTemp.OrderSummaryModel.TelephoneCode;
        //        paymentInfoModel.OrderSummaryModel.TelephoneCountryId = paymentInfoModelTemp.OrderSummaryModel.TelephoneCountryId;
        //        paymentInfoModel.OrderSummaryModel.TelephoneNumber = paymentInfoModelTemp.OrderSummaryModel.TelephoneNumber;
        //        paymentInfoModel.ShoppingCartModel = paymentInfoModelTemp.ShoppingCartModel;
        //        paymentInfoModel.CreditCardDataModel = paymentInfoModelTemp.CreditCardDataModel;
        //        paymentInfoModel.OrderHeaderWIPModel = paymentInfoModelTemp.OrderHeaderWIPModel;
        //        if (
        //            string.IsNullOrWhiteSpace(paymentInfoModel.GiftCertPaymentModel.GiftCertNumber) &&
        //            string.IsNullOrWhiteSpace(paymentInfoModel.GiftCertPaymentModel.GiftCertKey)
        //           )
        //        {
        //            ;
        //        }
        //        else
        //        {
        //            if (long.TryParse(paymentInfoModel.GiftCertPaymentModel.GiftCertNumber, out long tempLong))
        //            {
        //                if (paymentInfoModel.GiftCertPaymentModel.GiftCertNumber.Length == 18 && paymentInfoModel.GiftCertPaymentModel.GiftCertKey.Length == 9)
        //                {
        //                    ;
        //                }
        //                else
        //                {
        //                    modelStateDictionary.AddModelError("GiftCertPaymentModel.GiftCertNumber", "Please enter valid Gift Cert#");
        //                    modelStateDictionary.AddModelError("GiftCertPaymentModel.GiftCertKey", "Please enter valid Gift Cert Key");
        //                }
        //            }
        //            else
        //            {
        //                modelStateDictionary.AddModelError("GiftCertPaymentModel.GiftCertNumber", "Please enter valid Gift Cert#");
        //            }
        //        }
        //        paymentInfoModel.OrderSummaryModel.UserFullName = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim();
        //        UpdateDeliveryAddressInfo(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //}
        //// PUBLIC: BuildDeliveryInfoLookupData
        //public void BuildDeliveryInfoLookupData(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    var applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
        //    var corpAcctLocationModel = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels.First(x => x.CorpAcctLocationId == applSessionObjectModel.CorpAcctLocationId);
        //    InvoiceTypeEnum? invoiceTypeId = paymentInfoModel.OrderSummaryModel.InvoiceTypeId;
        //    DemogInfoAddressModel createdForDemogInfoAddressModel = createForSessionObject.DemogInfoAddressModel;
        //    paymentInfoModel.DeliveryAddressModel = paymentInfoModel.DeliveryAddressModel ?? new DemogInfoAddressModel();
        //    if (string.IsNullOrEmpty(paymentInfoModel.DeliveryAddressModel.AddressLine1) && string.IsNullOrEmpty(paymentInfoModel.DeliveryAddressModel.AddressLine2))
        //    {//Populate the address with the one in the profile
        //        var deliveryDemogInfoCountryModel = RetailSlnCache.DeliveryDemogInfoCountryModels.FirstOrDefault(x => x.DemogInfoCountryId == createdForDemogInfoAddressModel.DemogInfoCountryId);
        //        if (deliveryDemogInfoCountryModel?.DemogInfoCountryId == createdForDemogInfoAddressModel.DemogInfoCountryId)
        //        {
        //            paymentInfoModel.DeliveryAddressModel.BuildingTypeId = createdForDemogInfoAddressModel.BuildingTypeId;
        //            paymentInfoModel.DeliveryAddressModel.AddressLine1 = createdForDemogInfoAddressModel.AddressLine1;
        //            paymentInfoModel.DeliveryAddressModel.AddressLine2 = createdForDemogInfoAddressModel.AddressLine2;
        //            paymentInfoModel.DeliveryAddressModel.CityName = createdForDemogInfoAddressModel.CityName;
        //            paymentInfoModel.DeliveryAddressModel.CountryAbbrev = createdForDemogInfoAddressModel.CountryAbbrev;
        //            paymentInfoModel.DeliveryAddressModel.CountryDesc = createdForDemogInfoAddressModel.CountryDesc;
        //            paymentInfoModel.DeliveryAddressModel.DemogInfoCityId = createdForDemogInfoAddressModel.DemogInfoCityId;
        //            paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId = createdForDemogInfoAddressModel.DemogInfoCountryId;
        //            paymentInfoModel.DeliveryAddressModel.DemogInfoCountyId = createdForDemogInfoAddressModel.DemogInfoCountyId;
        //            paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = createdForDemogInfoAddressModel.DemogInfoSubDivisionId;
        //            paymentInfoModel.DeliveryAddressModel.DemogInfoZipId = createdForDemogInfoAddressModel.DemogInfoZipId;
        //            paymentInfoModel.DeliveryAddressModel.StateAbbrev = createdForDemogInfoAddressModel.StateAbbrev;
        //            paymentInfoModel.DeliveryAddressModel.ZipCode = createdForDemogInfoAddressModel.ZipCode;
        //            paymentInfoModel.DeliveryAddressModel.HouseNumber = createdForDemogInfoAddressModel.HouseNumber;
        //        }
        //    }
        //    paymentInfoModel.DeliveryAddressModel.BuildingTypeId = paymentInfoModel.DeliveryAddressModel.BuildingTypeId ?? BuildingTypeEnum._;
        //    paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId = paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId ?? RetailSlnCache.DefaultDeliveryDemogInfoCountryId;
        //    paymentInfoModel.DeliveryAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //    paymentInfoModel.DeliveryAddressModel.DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems;
        //    paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
        //    paymentInfoModel.DeliveryDataModel = paymentInfoModel.DeliveryDataModel ?? new DeliveryDataModel
        //    {
        //        AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //        PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //    };
        //    if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum))
        //    {
        //        paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId = createForSessionObject.TelephoneCountryId;
        //        paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum = createForSessionObject.PhoneNumber;
        //    }
        //    paymentInfoModel.DeliveryMethodModel = paymentInfoModel.DeliveryMethodModel ?? new DeliveryMethodModel
        //    {
        //    };
        //    paymentInfoModel.DeliveryMethodModel.DeliveryMethods = RetailSlnCache.DeliveryMethodsList[applSessionObjectModel.CorpAcctModel.CreditSale.Value];
        //    paymentInfoModel.DeliveryMethodModel.PickupLocationDemogInfoAddressModels = applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes ? RetailSlnCache.PickupLocationDemogInfoAddressModels2 : RetailSlnCache.PickupLocationDemogInfoAddressModels1;
        //    int i, fromIndex, toIndex;
        //    if (applSessionObjectModel.CorpAcctModel.CreditSale == YesNoEnum.Yes)
        //    {
        //        fromIndex = RetailSlnCache.DeliveryMethodSelectListItems.Count - 1;
        //        toIndex = RetailSlnCache.DeliveryMethodSelectListItems.Count;
        //    }
        //    else
        //    {
        //        fromIndex = 0;
        //        toIndex = RetailSlnCache.DeliveryMethodSelectListItems.Count - 1;
        //    }
        //    paymentInfoModel.DeliveryMethodModel.DeliveryMethodPickupLocationSelectListItems = new List<SelectListItem>();
        //    for (i = fromIndex; i < toIndex; i++)
        //    {
        //        paymentInfoModel.DeliveryMethodModel.DeliveryMethodPickupLocationSelectListItems.Add(RetailSlnCache.DeliveryMethodSelectListItems[i]);
        //    }
        //    paymentInfoModel.OrderSummaryModel = paymentInfoModel.OrderSummaryModel ?? new OrderSummaryModel();
        //    paymentInfoModel.OrderSummaryModel.AspNetUserId = sessionObjectModel.AspNetUserId;
        //    paymentInfoModel.OrderSummaryModel.CorpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
        //    paymentInfoModel.OrderSummaryModel.CreatedByEmailAddress = sessionObjectModel.EmailAddress.ToLower();
        //    paymentInfoModel.OrderSummaryModel.CreatedByFirstName = sessionObjectModel.FirstName;
        //    paymentInfoModel.OrderSummaryModel.CreatedByLastName = sessionObjectModel.LastName;
        //    paymentInfoModel.OrderSummaryModel.EmailAddress = createForSessionObject.EmailAddress.ToLower();
        //    paymentInfoModel.OrderSummaryModel.FirstName = createForSessionObject.FirstName;
        //    paymentInfoModel.OrderSummaryModel.InvoiceTypeId = invoiceTypeId.Value;
        //    paymentInfoModel.OrderSummaryModel.LastName = createForSessionObject.LastName;
        //    paymentInfoModel.OrderSummaryModel.PersonId = createForSessionObject.PersonId;
        //    paymentInfoModel.OrderSummaryModel.TelephoneCode = null;
        //    paymentInfoModel.OrderSummaryModel.TelephoneCountryId = createForSessionObject.TelephoneCountryId.Value;
        //    paymentInfoModel.OrderSummaryModel.TelephoneNumber = createForSessionObject.PhoneNumber;
        //    paymentInfoModel.GiftCertPaymentModel = new GiftCertPaymentModel
        //    {
        //        GiftCertPaymentAmount = 0,
        //    };
        //    paymentInfoModel.PaymentModeModel = paymentInfoModel.PaymentModeModel ?? new PaymentModeModel
        //    {
        //    };
        //    paymentInfoModel.PaymentModeModel.PaymentModes = RetailSlnCache.PaymentModesList[applSessionObjectModel.CorpAcctModel.CreditSale.Value];
        //    if (
        //        string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.AddressLine1) &&
        //        string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.CityName) &&
        //        string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode)
        //       )
        //    {
        //        if (corpAcctLocationModel.CorpAcctId == 0)
        //        {
        //            paymentInfoModel.DeliveryAddressModel.AddressName = "";
        //        }
        //        else
        //        {
        //            paymentInfoModel.DeliveryAddressModel.AddressName = corpAcctLocationModel.LocationName;
        //        }
        //        paymentInfoModel.DeliveryAddressModel.AddressLine1 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine1;
        //        paymentInfoModel.DeliveryAddressModel.AddressLine2 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine2;
        //        paymentInfoModel.DeliveryAddressModel.AddressLine3 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine3;
        //        paymentInfoModel.DeliveryAddressModel.AddressLine4 = corpAcctLocationModel.DemogInfoAddressModel.AddressLine4;
        //        paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = corpAcctLocationModel.DemogInfoAddressModel.DemogInfoSubDivisionId;
        //        paymentInfoModel.DeliveryAddressModel.CityName = corpAcctLocationModel.DemogInfoAddressModel.CityName;
        //        paymentInfoModel.DeliveryAddressModel.StateAbbrev = corpAcctLocationModel.DemogInfoAddressModel.StateAbbrev;
        //        paymentInfoModel.DeliveryAddressModel.ZipCode = corpAcctLocationModel.DemogInfoAddressModel.ZipCode;
        //        try
        //        {
        //            paymentInfoModel.OrderSummaryModel.TelephoneCountryId = corpAcctLocationModel.PrimaryTelephoneCountryId.Value;
        //            paymentInfoModel.OrderSummaryModel.TelephoneNumber = corpAcctLocationModel.PrimaryTelephoneNumber.Value.ToString();
        //            paymentInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId = corpAcctLocationModel.PrimaryTelephoneCountryId;
        //            paymentInfoModel.DeliveryDataModel.PrimaryTelephoneNum = corpAcctLocationModel.PrimaryTelephoneNumber.Value.ToString();
        //            paymentInfoModel.DeliveryDataModel.AlternateTelephoneDemogInfoCountryId = corpAcctLocationModel.AlternateTelephoneCountryId;
        //            paymentInfoModel.DeliveryDataModel.AlternateTelephoneNum = corpAcctLocationModel.AlternateTelephoneNumber == null ? null : corpAcctLocationModel.AlternateTelephoneNumber.Value.ToString();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    return;
        //}
        #endregion
        #region Commenting Code for now - Need this after fixing
        //// PUBLIC: CreateOrderWIP
        //public void CreateOrderWIP(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //From session write to database items in shopping cart
        //    //Clear shopping cart from session
        //    //Load shopping cart from database into session - This will load what was added prior to this
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
        //                    DoNotBreakBundle = shoppingCartItemModel.DoNotBreakBundle,
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
        //                AddToCartModel addToCartModel = new AddToCartModel
        //                {
        //                    ItemIdParm = orderDetailWIPModel.ItemId.ToString(),
        //                    OrderQtyParm = orderDetailWIPModel.OrderQty.ToString(),
        //                };
        //                AddToCart(ref paymentInfoModel, addToCartModel, false, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        #endregion

        #region Commenting Code for now - Need this after fixing
        #region PRIVATE
        //// PRIVATE : AddItemToShoppingCart
        //private void AddItemToShoppingCart(ref PaymentInfoModel paymentInfoModel, AddToCartModel addToCartModel, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        CreatePaymentInfoModel(ref paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ShoppingCartItemModel shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.FirstOrDefault(x => x.ItemId == addToCartModel.ItemId && x.ParentItemId == addToCartModel.ParentItemId && x.DoNotBreakBundle == addToCartModel.DoNotBreakBundle);
        //        if (shoppingCartItemModel == null)
        //        {
        //            CreateShoppingCartItemModel(ref shoppingCartItemModel, addToCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Add(shoppingCartItemModel);
        //            if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createOrderWIP)
        //            {
        //                //ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject?.ApplSessionObjectModel;
        //                //OrderWIPAdd(ref paymentInfoModel, applSessionObjectModel.CorpAcctLocationId, addToCartModel, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //        }
        //        else
        //        {
        //            CreateShoppingCartItemModel(ref shoppingCartItemModel, addToCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (createForSessionObject != null && createForSessionObject.AspNetRoleName != "GUESTROLE" && createOrderWIP)
        //            {
        //                //OrderDetailWIPUpdate(ref paymentInfoModel, shoppingCartItemModel, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        //// PRIVATE: AddToCart
        //private string AddToCart(ref PaymentInfoModel paymentInfoModel, AddToCartModel addToCartModel, bool createOrderWIP, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string errorMessage = "";
        //        ItemModel itemModel = null;
        //        if (long.TryParse(addToCartModel.ItemIdParm, out long itemId))
        //        {
        //            addToCartModel.ItemId = itemId;
        //            itemModel = RetailSlnCache.ItemModels.FirstOrDefault(x => x.ItemId == addToCartModel.ItemId);
        //            if (itemModel == null)
        //            {
        //                errorMessage += "Select valid item;";
        //            }
        //            else
        //            {
        //                addToCartModel.ItemModel = itemModel;
        //                if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //                {
        //                    int orderQtyBundleCount = 0;
        //                    int orderQtyBundle;
        //                    foreach (var shoppingCartItemBundleModel in addToCartModel.ShoppingCartItemBundleModels)
        //                    {
        //                        int.TryParse(shoppingCartItemBundleModel.OrderQtyParm, out orderQtyBundle);
        //                        if (orderQtyBundle != 0)
        //                        {
        //                            orderQtyBundleCount++;
        //                        }
        //                    }
        //                    if (orderQtyBundleCount == 0)
        //                    {
        //                        errorMessage += "Enter quantity for bundle item(s)";
        //                    }
        //                    addToCartModel.ParentItemId = itemId;
        //                }
        //                else
        //                {
        //                    addToCartModel.ParentItemId = 0;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            errorMessage += "Select valid item;";
        //        }
        //        if (!long.TryParse(addToCartModel.OrderQtyParm, out long orderQty))
        //        {
        //            errorMessage += "Enter quantity;";
        //        }
        //        else
        //        {
        //            addToCartModel.OrderQty = orderQty;
        //        }
        //        if (errorMessage == "")
        //        {
        //            AddItemToShoppingCart(ref paymentInfoModel, addToCartModel, createOrderWIP, sqlConnection, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        //// PRIVATE : CalculateTotalOrderAmount
        //private void CalculateTotalOrderAmount(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount = 0;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
        //        foreach (var shoppingCartItemModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels)
        //        {
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount += shoppingCartItemModel.OrderQty;
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalcValue;
        //        }
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
        //        var shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount);
        //        if (shoppingCartItemModelSummary != null)
        //        {
        //            shoppingCartItemModelSummary.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
        //            shoppingCartItemModelSummary.OrderAmountFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
        //            shoppingCartItemModelSummary.ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalOrderAmount).CodeDataDesc0;
        //            shoppingCartItemModelSummary.ItemShortDesc += " (" + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + " / " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderQtyCount + ")";
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        modelStateDictionary.AddModelError("", "Error while calculating total order amount");
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion
        #region Commenting Code for now - Need this after fixing
        // PRIVATE: BuildShoppingCartWIPDelIds
        private string BuildShoppingCartWIPDelIds(ShoppingCartItemModel shoppingCartItemModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId)
        {
            string sqlStmtDel = "";
            sqlStmtDel += shoppingCartItemModel.ShoppingCartWIPId;
            if (shoppingCartItemModel.ShoppingCartItemBundleModels != null)
            {
                foreach (var shoppingCartItemBundleModel in shoppingCartItemModel.ShoppingCartItemBundleModels)
                {
                    sqlStmtDel += ", " + shoppingCartItemBundleModel.ShoppingCartWIPId;
                }
            }
            return sqlStmtDel;
        }
        // PRIVATE: BuildShoppingCartWIPDelIds
        private string BuildShoppingCartWIPDelIds(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId)
        {
            string sqlStmtDel = "", prefixString = "";
            foreach (var shoppingCartItemModel in shoppingCartModel.ShoppingCartItemModels)
            {
                sqlStmtDel += prefixString + shoppingCartItemModel.ShoppingCartWIPId;
                prefixString = ", ";
                if (shoppingCartItemModel.ShoppingCartItemBundleModels != null)
                {
                    foreach (var shoppingCartItemBundleModel in shoppingCartItemModel.ShoppingCartItemBundleModels)
                    {
                        sqlStmtDel += ", " + shoppingCartItemBundleModel.ShoppingCartWIPId;
                    }
                }
            }
            return sqlStmtDel;
        }
        //// PRIVATE : BuildCreditCardDataModel
        //private void BuildCreditCardDataModel(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //        ShoppingCartItemModel shoppingCartItemModelBalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
        //        paymentInfoModel.CreditCardDataModel = new CreditCardDataModel
        //        {
        //            CreditCardAmount = shoppingCartItemModelBalanceDue.OrderAmount.Value.ToString("0.00"),
        //            CreditCardExpMM = null,
        //            CreditCardExpYear = null,
        //            CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            CreditCardNumber = null,
        //            CreditCardProcessor = creditCardProcessor,
        //            CreditCardSecCode = null,
        //            CreditCardTranType = "PAYMENT",
        //            CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
        //            EmailAddress = paymentInfoModel.OrderSummaryModel.EmailAddress,
        //            NameAsOnCard = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim(),
        //            TelephoneNumber = paymentInfoModel.OrderSummaryModel.TelephoneNumber,
        //        };
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
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
        // PRIVATE: CreateInvoice
        private void CreateInvoice(PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                string orderFileName = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString();
                CodeDataModel codeDataModel;
                if (paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
                {
                    codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("InvoiceType", execUniqueId).First(x => x.CodeDataNameId == 900);
                }
                else
                {
                    codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("InvoiceType", execUniqueId).First(x => x.CodeDataNameId == 100);
                }
                paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceType = codeDataModel.CodeDataDesc0;
                orderFileName += "_" + (int)paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId;
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceFileNamePdf = orderFileName + ".pdf";
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
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceFullFileNamePdf = pDFFullFileName;
                pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
                List<string> emailAttachmentFileNames = new List<string>
                {
                    pDFFullFileName,
                };
                var toEmailAddresss = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.EmailAddress;// + ";" + ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", "ToEmailAddress");
                if (createForSessionObject.EmailAddress.ToLower() != sessionObjectModel.EmailAddress.ToLower())
                {
                    toEmailAddresss += ";" + sessionObjectModel.EmailAddress;
                }
                //archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceHtmlString = emailBodyHtml;
                if (paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId == InvoiceTypeEnum.FinalInvoice)
                {
                }
                else
                {
                    paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId = InvoiceTypeEnum.FinalInvoice;
                    paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceType = "Tax Invoice";
                    emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
                    orderFileName = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString() + "_" + (int)paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId;
                    streamWriter = new StreamWriter(invoiceDirectoryName + orderFileName + ".html");
                    streamWriter.Write(emailBodyHtml);
                    streamWriter.Write(Environment.NewLine);
                    streamWriter.Close();
                    pDFFullFileName = invoiceDirectoryName + orderFileName + ".pdf";
                    pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
                }
                OrderInvoiceModel orderInvoiceModel = new OrderInvoiceModel
                {
                    InvoiceFileNamePdf = orderFileName + ".pdf",
                    InvoiceFullFileNamePdf = pDFFullFileName,
                    InvoiceHtmlString = emailBodyHtml,
                    OrderHeaderId = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId.Value,
                };
                httpSessionStateBase["OrderInvoice"] = orderInvoiceModel;
                httpSessionStateBase["DeliveryInfo"] = null;
                httpSessionStateBase["PaymentInfo"] = null;
                httpSessionStateBase["ShoppingCart"] = null;
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
        private void CreateOrder(PaymentInfoModel paymentInfoModel, Dictionary<string, Dictionary<string, string>> paymentRefOptions, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ArchLibDataContext.CreateDemogInfoAddress(paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (createForSessionObject.AspNetRoleName == "GUESTROLE")
                {
                    if (!paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.EmailExists)
                    {//Create new user
                        archLibBL.CreateNewUser(paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId.Value, paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneNum, paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.EmailAddress, paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.FirstName, paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.LastName, paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoAddressId, "DEFAULTROLE", UserStatusEnum.Active, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                CorpAcctModel corpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
                //paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderDateTime = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderDateTime ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                OrderHeader orderHeader = CreateOrderHeader(paymentInfoModel, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderDateTime = orderHeader.OrderDateTime;
                paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.CorpAcctModel = corpAcctModel;
                ApplicationDataContext.OrderHeaderAdd(orderHeader, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId = orderHeader.OrderHeaderId;
                OrderHeaderSummary orderHeaderSummary = CreateOrderHeaderSummary(paymentInfoModel, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                orderHeaderSummary.OrderHeaderId = orderHeader.OrderHeaderId;
                ApplicationDataContext.OrderHeaderSummaryAdd(orderHeaderSummary, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderDetail orderDetail;
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
                //ArchLibDataContext.CreateDemogInfoAddress(paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.DeliveryAddressModel = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel;
                paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.OrderHeaderId = orderHeader.OrderHeaderId;
                paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.DeliveryMethodId = (long?)paymentInfoModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodId;
                paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PickupLocationId = paymentInfoModel.DeliveryInfoModel.DeliveryMethodModel.PickupLocationId;
                paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId = orderHeader.OrderHeaderId;
                ApplicationDataContext.OrderDeliveryAdd(paymentInfoModel.DeliveryInfoModel.DeliveryDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.PaymentDataModel = new PaymentDataModel
                {
                    CouponId = 0,
                    CreditCardDataId = paymentInfoModel.CreditCardDataModel.CreditCardDataId,
                    GiftCertId = 0,
                    OrderHeaderId = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.OrderHeaderId.Value,
                    PaymentModeId = (long)paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeId,
                    PaymentRefOptions = paymentRefOptions,
                };
                ApplicationDataContext.OrderPaymentAdd(paymentInfoModel.PaymentDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                PersonModel personModel = new PersonModel
                {
                    FirstName = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.FirstName,
                    HomeDemogInfoAddressId = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoAddressId,
                    LastName = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.LastName,
                    PersonId = createForSessionObject.PersonId,
                    StatusId = GenericStatusEnum.Active,
                };
                //ArchLibDataContext.UpdPerson(createForSessionObject.PersonId, paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.FirstName, paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.LastName, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                ArchLibDataContext.UpdPerson(personModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                CreateInvoice(paymentInfoModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                var sqlStmtDel = BuildShoppingCartWIPDelIds(paymentInfoModel.ShoppingCartModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId);
                ApplicationDataContext.ShoppingCartWIPDel(sqlStmtDel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                createForSessionObject.FirstName = personModel.FirstName;
                createForSessionObject.DemogInfoAddressModel.DemogInfoAddressId = personModel.HomeDemogInfoAddressId.Value;
                createForSessionObject.DemogInfoAddressModel.AddressLine1 = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.AddressLine1;
                createForSessionObject.DemogInfoAddressModel.AddressLine2 = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.AddressLine2;
                createForSessionObject.DemogInfoAddressModel.CityName = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.CityName;
                createForSessionObject.DemogInfoAddressModel.ZipCode = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.ZipCode;
                createForSessionObject.DemogInfoAddressModel.DemogInfoSubDivisionId = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId.Value;
                createForSessionObject.LastName = personModel.LastName;
                if (createForSessionObject.PersonId == sessionObjectModel.PersonId)
                {
                    sessionObjectModel.FirstName = personModel.FirstName;
                    sessionObjectModel.DemogInfoAddressModel.DemogInfoAddressId = personModel.HomeDemogInfoAddressId.Value;
                    sessionObjectModel.DemogInfoAddressModel.AddressLine1 = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.AddressLine1;
                    sessionObjectModel.DemogInfoAddressModel.AddressLine2 = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.AddressLine2;
                    sessionObjectModel.DemogInfoAddressModel.CityName = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.CityName;
                    sessionObjectModel.DemogInfoAddressModel.ZipCode = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.ZipCode;
                    sessionObjectModel.DemogInfoAddressModel.DemogInfoSubDivisionId = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId.Value;
                    sessionObjectModel.LastName = personModel.LastName;
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
                    DoNotBreakBundle = shoppingCartItemModel.DoNotBreakBundle,
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
                InvoiceTypeId = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId.Value,
                OrderDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
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
                InvoiceTypeId = paymentInfoModel.DeliveryInfoModel.OrderSummaryModel.InvoiceTypeId.Value,
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
        #endregion
        // PRIVATE: ItemCatalogAndItemCatalogItemCreate
        private void ItemCatalogCreate(string itemCatalogFilesPath, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long corpAcctId;
                string aspNetRoleName;
                List<CodeDataModel> corpAcctTypes = LookupCache.CodeDataModels.FindAll(x => x.CodeTypeId == 204);
                CodeDataModel corpAcctType;
                Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs;
                foreach (var corpAcctModel in RetailSlnCache.CorpAcctModels)
                {
                    if (corpAcctModel.CorpAcctId == 0)
                    {
                        ItemCatalogItemCreate("APPLADMN1", corpAcctModel.CorpAcctId.Value, itemCatalogFilesPath, sessionObjectModel, createForSessionObjectModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ItemCatalogItemCreate("DEFAULTROLE", corpAcctModel.CorpAcctId.Value, itemCatalogFilesPath, sessionObjectModel, createForSessionObjectModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                        ItemCatalogItemCreate(aspNetRoleName, corpAcctId, itemCatalogFilesPath, sessionObjectModel, createForSessionObjectModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // PRIVATE: ItemCatalogItemCreate
        private void ItemCatalogItemCreate(string aspNetRoleName, long corpAcctId, string itemCatalogFilesPath, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long parentCategoryId, itemCount;
                string htmlFileName, htmlString, parentCategoryDesc;//, pDFFullFileName;
                Dictionary<long, List<CategoryItemMasterHierModel>> parentCategoryItemMasterModels;
                ItemCatalogFileModel itemCatalogFileModel = new ItemCatalogFileModel();
                List<CategoryItemMasterHierModel> categoryCategoryItemMasterHierModels = RetailSlnCache.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][0];
                List<CategoryItemMasterHierModel> categoryItemMasterHierModels;
                StreamWriter streamWriter;
                ArchLibBL archLibBL = new ArchLibBL();
                PDFUtility pDFUtility = new PDFUtility();
                foreach (var categoryCategoryItemMasterHierModel in categoryCategoryItemMasterHierModels)
                {
                    parentCategoryId = categoryCategoryItemMasterHierModel.CategoryModel.CategoryId.Value;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Begin", "AspNetRoleName", aspNetRoleName, "CorpAcctId", corpAcctId.ToString(), "ParentCategoryId", parentCategoryId.ToString());
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
                    RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> itemDiscountModels);
                    itemDiscountModels = itemDiscountModels == null ? new Dictionary<long, ItemDiscountModel>() : itemDiscountModels;
                    itemCount = 0;
                    foreach (var categoryItemMasterHierModel in categoryItemMasterHierModels)
                    {
                        itemCount += categoryItemMasterHierModel.ItemMasterModel.ItemModels.Count;
                    }
                    itemCatalogFileModel = new ItemCatalogFileModel
                    {
                        CategoryCategoryItemMasterHierModels = categoryCategoryItemMasterHierModels,
                        CategoryItemMasterHierModels = categoryItemMasterHierModels,
                        CurrencySymbol = RetailSlnCache.CurrencySymbol,
                        ItemDiscountModels = itemDiscountModels,
                        ItemMasterCount = categoryItemMasterHierModels.Count,
                        ItemCount = itemCount,
                        ParentCategoryDesc = parentCategoryDesc,
                        ParentCategoryId = null,
                        PDFFlag = false,
                    };
                    htmlFileName = itemCatalogFilesPath + $@"\ItemCatalog_{aspNetRoleName}_{corpAcctId}_{parentCategoryId}.html";
                    htmlString = archLibBL.ViewToHtmlString(controller, "_ItemCatalogFile", itemCatalogFileModel);
                    streamWriter = new StreamWriter(htmlFileName);
                    streamWriter.Write(htmlString);
                    streamWriter.Close();
                    if (aspNetRoleName == "DEFAULTROLE" && corpAcctId == 0 && parentCategoryId == 100)
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Pdf Begin", "AspNetRoleName", aspNetRoleName, "CorpAcctId", corpAcctId.ToString(), "ParentCategoryId", parentCategoryId.ToString());
                        itemCatalogFileModel = new ItemCatalogFileModel
                        {
                            CategoryCategoryItemMasterHierModels = null,
                            CategoryItemMasterHierModels = categoryItemMasterHierModels,
                            CurrencySymbol = RetailSlnCache.CurrencySymbol,
                            ItemDiscountModels = null,
                            ItemMasterCount = categoryItemMasterHierModels.Count,
                            ItemCount = itemCount,
                            ParentCategoryDesc = parentCategoryDesc,
                            ParentCategoryId = null,
                            PDFFlag = true,
                        };
                        htmlString = archLibBL.ViewToHtmlString(controller, "ItemCatalogPdf", itemCatalogFileModel);
                        //pDFFullFileName = itemCatalogFilesPath + $@"\ItemCatalog_{aspNetRoleName}_{corpAcctId}_{parentCategoryId}.pdf";
                        //pDFFullFileName = itemCatalogFilesPath + $@"\ItemCatalog.pdf";
                        //pDFUtility.GeneratePDFFromHtmlString(htmlString, pDFFullFileName);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Pdf End");
                    }
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: End");
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
        #region Commenting Code for now - Need this after fixing
        //// PRIVATE : CreatePaymentInfoModel
        //private void CreatePaymentInfoModel(ref PaymentInfoModel paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfoModel = paymentInfoModel ?? new PaymentInfoModel();
        //        paymentInfoModel.CreditCardDataModel = paymentInfoModel.CreditCardDataModel ?? new CreditCardDataModel();
        //        paymentInfoModel.OrderHeaderWIPModel = paymentInfoModel.OrderHeaderWIPModel ?? new OrderHeaderWIPModel();
        //        paymentInfoModel.OrderSummaryModel = paymentInfoModel.OrderSummaryModel ?? new OrderSummaryModel();
        //        paymentInfoModel.OrderSummaryModel.OrderDateTime = paymentInfoModel.OrderSummaryModel.OrderDateTime ?? DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
        //        paymentInfoModel.OrderSummaryModel.InvoiceTypeId = paymentInfoModel.OrderSummaryModel.InvoiceTypeId ?? InvoiceTypeEnum.FinalInvoice;
        //        //paymentInfoModel.ShoppingCartModel = paymentInfoModel.ShoppingCartModel ?? new ShoppingCartModel
        //        //{
        //        //    Checkout = false,
        //        //    ShowDetail = false,
        //        //};
        //        //paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels ?? new List<ShoppingCartItemModel>();
        //        //paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary ?? new List<ShoppingCartItemModel>
        //        //{
        //        //    new ShoppingCartItemModel
        //        //    {
        //        //        OrderQty = 1,
        //        //        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //        //    }
        //        //};
        //        //paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel ?? new ShoppingCartSummaryModel
        //        //{
        //        //    TotalItemsCount = 0,
        //        //    TotalOrderAmount = 0,
        //        //    TotalOrderAmountFormatted = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
        //        //    TotalOrderQtyCount = 0,
        //        //};
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //}
        #endregion
        // PRIVATE : CreateShoppingCartItemModel
        private void CreateShoppingCartItemModel(ref ShoppingCartItemModel shoppingCartItemModel, AddToCartModel addToCartModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i;
            long /*orderQty, */corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
            float itemRate, itemRateBeforeDiscount, productOrVolumetricWeight, weightCalcValue, weightValue;
            //string orderQtyParm;
            ItemModel itemModel = addToCartModel.ItemModel;
            List<ShoppingCartItemModel> shoppingCartItemBundleModelsFromCache;
            RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> corpAcctItemDiscountModels);
            corpAcctItemDiscountModels = corpAcctItemDiscountModels ?? new Dictionary<long, ItemDiscountModel>();
            corpAcctItemDiscountModels.TryGetValue(itemModel.ItemId.Value, out ItemDiscountModel itemDiscountModel);
            itemDiscountModel = itemDiscountModel ?? new ItemDiscountModel { DiscountPercent = 0 };
            if (shoppingCartItemModel == null)
            {
                shoppingCartItemModel = CreateShoppingCartItemModelObject(addToCartModel, itemModel, itemDiscountModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (addToCartModel.DoNotBreakBundle)
                {
                    itemRateBeforeDiscount = addToCartModel.ItemModel.ItemRate.Value;
                    productOrVolumetricWeight = addToCartModel.OrderQty * float.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecValue);
                    weightCalcValue = addToCartModel.OrderQty * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                    weightValue = addToCartModel.OrderQty * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                    if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                    {
                        shoppingCartItemModel.ShoppingCartItemBundleModels = new List<ShoppingCartItemModel>();
                        shoppingCartItemBundleModelsFromCache = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].ShoppingCartItemBundleModels;
                        for (i = 0; i < shoppingCartItemBundleModelsFromCache.Count; i++)
                        {
                            addToCartModel.ShoppingCartItemBundleModels[i].OrderQty = 1;
                            addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm = "1";
                            shoppingCartItemModel.ShoppingCartItemBundleModels.Add(CreateShoppingCartItemBundleModelObject(i, addToCartModel, shoppingCartItemBundleModelsFromCache, itemModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId));
                        }
                    }
                }
                else
                {
                    itemRateBeforeDiscount = 0;
                    productOrVolumetricWeight = 0;
                    weightCalcValue = 0;
                    weightValue = 0;
                    shoppingCartItemModel.ShoppingCartItemBundleModels = new List<ShoppingCartItemModel>();
                    shoppingCartItemBundleModelsFromCache = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].ShoppingCartItemBundleModels;
                    for (i = 0; i < shoppingCartItemBundleModelsFromCache.Count; i++)
                    {
                        addToCartModel.ShoppingCartItemBundleModels[i].OrderQty = long.Parse(addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm);
                        itemRateBeforeDiscount += (shoppingCartItemBundleModelsFromCache[i].ItemRate * addToCartModel.ShoppingCartItemBundleModels[i].OrderQty).Value;
                        productOrVolumetricWeight += addToCartModel.ShoppingCartItemBundleModels[i].OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecValue);
                        weightCalcValue += addToCartModel.ShoppingCartItemBundleModels[i].OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                        weightValue += addToCartModel.ShoppingCartItemBundleModels[i].OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                        shoppingCartItemModel.ShoppingCartItemBundleModels.Add(CreateShoppingCartItemBundleModelObject(i, addToCartModel, shoppingCartItemBundleModelsFromCache, itemModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId));
                    }
                }
                itemRate = itemRateBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
                shoppingCartItemModel.ItemRateBeforeDiscount = itemRateBeforeDiscount;
                shoppingCartItemModel.ItemRateBeforeDiscountFormatted = itemRateBeforeDiscount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModel.ItemRate = itemRate;
                shoppingCartItemModel.ItemRateFormatted = itemRate.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModel.OrderAmount = itemRate * addToCartModel.OrderQty;
                shoppingCartItemModel.OrderAmountBeforeDiscount = itemRateBeforeDiscount * addToCartModel.OrderQty;
                shoppingCartItemModel.OrderAmountFormatted = shoppingCartItemModel.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModel.OrderAmountRounded = (long)(shoppingCartItemModel.OrderAmount * 100);
                shoppingCartItemModel.OrderQty = addToCartModel.OrderQty;
                shoppingCartItemModel.OrderQtyParm = addToCartModel.OrderQtyParm;
                shoppingCartItemModel.ProductOrVolumetricWeight = productOrVolumetricWeight;
                shoppingCartItemModel.WeightCalcValue = weightCalcValue;
                shoppingCartItemModel.WeightValue = weightValue;
            }
            else
            {
                #region
                if (addToCartModel.DoNotBreakBundle)
                {
                    itemRateBeforeDiscount = addToCartModel.ItemModel.ItemRate.Value;
                    shoppingCartItemModel.OrderQty += addToCartModel.OrderQty;
                    shoppingCartItemModel.OrderQtyParm = shoppingCartItemModel.OrderQty.ToString();
                    productOrVolumetricWeight = shoppingCartItemModel.OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecValue);
                    weightCalcValue = shoppingCartItemModel.OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                    weightValue = shoppingCartItemModel.OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                }
                else
                {
                    itemRateBeforeDiscount = 0;
                    productOrVolumetricWeight = 0;
                    weightCalcValue = 0;
                    weightValue = 0;
                    shoppingCartItemModel.OrderQty = 1;
                    shoppingCartItemModel.OrderQtyParm = "1";
                    shoppingCartItemBundleModelsFromCache = RetailSlnCache.ParentItemBundleModels[itemModel.ItemId.Value].ShoppingCartItemBundleModels;
                    for (i = 0; i < shoppingCartItemBundleModelsFromCache.Count; i++)
                    {
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty += long.Parse(addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm);
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount = shoppingCartItemBundleModelsFromCache[i].ItemRate * shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty;
                        shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmountFormatted = shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                        itemRateBeforeDiscount += (shoppingCartItemBundleModelsFromCache[i].ItemRate * shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty).Value;
                        productOrVolumetricWeight += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecValue);
                        weightCalcValue += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                        weightValue += shoppingCartItemModel.ShoppingCartItemBundleModels[i].OrderQty.Value * float.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecValue);
                    }
                }
                itemRate = itemRateBeforeDiscount * (100 - itemDiscountModel.DiscountPercent) / 100;
                shoppingCartItemModel.ItemRate = itemRate;
                shoppingCartItemModel.ItemRateFormatted = shoppingCartItemModel.ItemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModel.OrderAmount = itemRate * shoppingCartItemModel.OrderQty;
                shoppingCartItemModel.OrderAmountBeforeDiscount = itemRateBeforeDiscount * shoppingCartItemModel.OrderQty;
                shoppingCartItemModel.OrderAmountFormatted = shoppingCartItemModel.OrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartItemModel.OrderAmountRounded = (long)(shoppingCartItemModel.OrderAmount * 100);
                shoppingCartItemModel.ProductOrVolumetricWeight = productOrVolumetricWeight;
                shoppingCartItemModel.WeightCalcValue = weightCalcValue;
                shoppingCartItemModel.WeightValue = weightValue;
                #endregion
            }
        }
        // PRIVATE : CreateShoppingCartItemModelObject
        private ShoppingCartItemModel CreateShoppingCartItemModelObject(AddToCartModel addToCartModel, ItemModel itemModel, ItemDiscountModel itemDiscountModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var shoppingCartItemModel = new ShoppingCartItemModel
            {
                #region
                ClientId = clientId,
                DoNotBreakBundle = addToCartModel.DoNotBreakBundle,
                HSNCode = itemModel.ItemItemSpecModels["HSNCode"].ItemSpecValueForDisplay,
                ImageName = itemModel.ItemMasterModel.ImageName,
                ItemDiscountAmount = null,//itemDiscountModel.DiscountPercent * itemRateBeforeDiscount * orderQty / 100,
                ItemDiscountPercent = itemDiscountModel.DiscountPercent,
                ItemDiscountPercentFormatted = itemDiscountModel.DiscountPercent.ToString("#0.00") + "%",
                ItemId = itemModel.ItemId,
                ItemIdParm = addToCartModel.ItemIdParm,
                ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                ItemMasterDesc = itemModel.ItemMasterModel.ItemMasterDesc,
                ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
                ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
                ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
                ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
                ItemRate = null,//itemRate,
                ItemRateBeforeDiscount = null,//itemRateBeforeDiscount,
                ItemRateBeforeDiscountFormatted = null,//itemRateBeforeDiscount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                ItemRateFormatted = null,//itemRate.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                ItemShortDesc = null,
                ItemTypeId = itemModel.ItemTypeId.Value,
                OrderAmount = null,
                OrderAmountBeforeDiscount = null,
                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                OrderQty = null,//orderQty,
                OrderQtyParm = null,//orderQtyParm,
                ParentItemId = addToCartModel.ParentItemId,
                ProductCode = itemModel.ItemItemSpecModels["ProductCode"].ItemSpecValueForDisplay,
                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecUnitValue),
                WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecUnitValue),
                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecUnitValue),
                ShoppingCartItemBundleModels = null,
                ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                #endregion
            };
            return shoppingCartItemModel;
        }
        // PRIVATE : CreateShoppingCartItemBundleModelObject
        private ShoppingCartItemModel CreateShoppingCartItemBundleModelObject(int i, AddToCartModel addToCartModel, List<ShoppingCartItemModel> shoppingCartItemBundleModelsFromCache, ItemModel itemModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            float orderAmount = shoppingCartItemBundleModelsFromCache[i].ItemRate.Value * addToCartModel.ShoppingCartItemBundleModels[i].OrderQty.Value;
            var shoppingCartItemBundleModel = new ShoppingCartItemModel
            {
                #region
                ClientId = clientId,
                DoNotBreakBundle = addToCartModel.DoNotBreakBundle,
                HSNCode = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemItemSpecModels["HSNCode"].ItemSpecValueForDisplay,
                ImageName = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemMasterModel.ImageName,
                ItemDiscountAmount = null,
                ItemDiscountPercent = null,
                ItemDiscountPercentFormatted = null,
                ItemId = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemId,
                ItemIdParm = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemId.ToString(),
                ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                ItemMasterDesc = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemMasterModel.ItemMasterDesc,
                ItemMasterDesc0 = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemMasterModel.ItemMasterDesc0,
                ItemMasterDesc1 = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemMasterModel.ItemMasterDesc1,
                ItemMasterDesc2 = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemMasterModel.ItemMasterDesc2,
                ItemMasterDesc3 = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemMasterModel.ItemMasterDesc3,
                ItemRate = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemRate,
                ItemRateBeforeDiscount = null,
                ItemRateBeforeDiscountFormatted = null,
                ItemRateFormatted = shoppingCartItemBundleModelsFromCache[i].ItemModel.ItemRateFormatted,
                ItemShortDesc = null,
                ItemTypeId = itemModel.ItemTypeId.Value,
                OrderAmount = orderAmount,
                OrderAmountBeforeDiscount = null,
                OrderAmountFormatted = orderAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                OrderQty = addToCartModel.ShoppingCartItemBundleModels[i].OrderQty,
                OrderQtyParm = addToCartModel.ShoppingCartItemBundleModels[i].OrderQtyParm,
                ParentItemId = addToCartModel.ParentItemId,
                ProductCode = itemModel.ItemItemSpecModels["ProductCode"].ItemSpecValueForDisplay,
                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecUnitValue),
                WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecUnitValue),
                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecUnitValue),
                ShoppingCartItemBundleModels = null,
                ShoppingCartItemSummarys = null,
                #endregion
            };
            return shoppingCartItemBundleModel;
        }
        #region Commenting Code for now - Need this after fixing
        //// PRIVATE : CreateShoppingCartTotals
        //private void CreateShoppingCartTotals(ref PaymentInfoModel paymentInfoModel, float amountPaid, string amountPaidComments, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalTaxAmount = 0;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalShippingAndHandlingChargesAmount = 0;
        //        float totalInvoiceAmount = 0, totalDiscountAmount = 0;
        //        foreach (ShoppingCartItemModel shoppingCartItemModelSummaryTemp in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary)
        //        {
        //            if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.TotalOrderAmount)
        //            {
        //                totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //            }
        //            if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.Discount)
        //            {
        //                totalDiscountAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //                totalInvoiceAmount += totalDiscountAmount;
        //            }
        //            if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.SalesTaxAmount)
        //            {
        //                totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalTaxAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //            }
        //            if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.ShippingHandlingCharges)
        //            {
        //                totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalShippingAndHandlingChargesAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //            }
        //            if (shoppingCartItemModelSummaryTemp.OrderDetailTypeId == OrderDetailTypeEnum.AdditionalCharges)
        //            {
        //                totalInvoiceAmount += shoppingCartItemModelSummaryTemp.OrderAmount.Value;
        //            }
        //        }
        //        ShoppingCartItemModel shoppingCartItemModelSummary;
        //        shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalInvoiceAmount);
        //        if (shoppingCartItemModelSummary == null)
        //        {
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
        //            (
        //                shoppingCartItemModelSummary = new ShoppingCartItemModel
        //                {
        //                    ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalInvoiceAmount).CodeDataDesc0,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
        //                }
        //            );
        //        }
        //        totalInvoiceAmount = (float)Math.Round(totalInvoiceAmount, RetailSlnCache.RoundingDigitCount);
        //        shoppingCartItemModelSummary.OrderAmount = totalInvoiceAmount;
        //        shoppingCartItemModelSummary.OrderAmountFormatted = totalInvoiceAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = shoppingCartItemModelSummary.OrderAmount;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountFormatted = shoppingCartItemModelSummary.OrderAmountFormatted;
        //        shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
        //        if (shoppingCartItemModelSummary == null)
        //        {
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
        //            (
        //                shoppingCartItemModelSummary = new ShoppingCartItemModel
        //                {
        //                    ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.TotalAmountPaid).CodeDataDesc0,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
        //                }
        //            );
        //        }
        //        shoppingCartItemModelSummary.OrderAmount = amountPaid;
        //        shoppingCartItemModelSummary.OrderAmountFormatted = amountPaid.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        shoppingCartItemModelSummary.OrderComments = amountPaidComments;
        //        shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
        //        if (shoppingCartItemModelSummary == null)
        //        {
        //            paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
        //            (
        //                shoppingCartItemModelSummary = new ShoppingCartItemModel
        //                {
        //                    ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.BalanceDue).CodeDataDesc0,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
        //                }
        //            );
        //        }
        //        shoppingCartItemModelSummary.OrderAmount = totalInvoiceAmount - amountPaid;
        //        shoppingCartItemModelSummary.OrderAmountFormatted = (totalInvoiceAmount - amountPaid).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        if (totalDiscountAmount > 0)
        //        {
        //            shoppingCartItemModelSummary = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.FirstOrDefault(x => x.OrderDetailTypeId == OrderDetailTypeEnum.DiscountAmount);
        //            if (shoppingCartItemModelSummary == null)
        //            {
        //                paymentInfoModel.ShoppingCartModel.ShoppingCartItemModelsSummary.Add
        //                (
        //                    shoppingCartItemModelSummary = new ShoppingCartItemModel
        //                    {
        //                        ItemShortDesc = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 213 && x.CodeDataNameId == (int)OrderDetailTypeEnum.DiscountAmount).CodeDataDesc0,
        //                        OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
        //                    }
        //                );
        //            }
        //            shoppingCartItemModelSummary.OrderAmount = totalDiscountAmount;
        //            shoppingCartItemModelSummary.OrderAmountFormatted = totalDiscountAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        }
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = totalInvoiceAmount - amountPaid;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = (totalInvoiceAmount - amountPaid).ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = totalInvoiceAmount;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = totalDiscountAmount;
        //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = amountPaid;
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //}
        //// PRIVATE : GetCreditCardKVPs
        //private Dictionary<string, string> GetCreditCardKVPs(string creditCardProcessor, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    creditCardProcessor = creditCardProcessor.ToUpper();
        //    Dictionary<string, string> creditCardKVPs;
        //    switch (creditCardProcessor)
        //    {
        //        case "NUVEIPROD":
        //        case "NUVEITEST":
        //            creditCardKVPs = new Dictionary<string, string>
        //            {
        //                { "PrivateKey", ArchLibCache.GetPrivateKey(clientId) },
        //                { "NuveiRestAPIRootUri", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "RestAPIRootUri") },
        //                { "NuveiRequestUri", ArchLibCache.GetApplicationDefault(clientId,creditCardProcessor, "RequestUri") },
        //                { "NuveiTerminalId", ArchLibCache.GetApplicationDefault(clientId,creditCardProcessor, "TerminalId") },
        //                { "NuveiSharedSecret", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "SharedSecret") },
        //            };
        //            break;
        //        case "RAZORPAYTEST":
        //        case "RAZORPAYPROD":
        //            creditCardKVPs = new Dictionary<string, string>
        //            {
        //                { "PrivateKey", ArchLibCache.GetPrivateKey(clientId) },
        //                { "ApiKey", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "ApiKey") },
        //                { "ApiSecret", ArchLibCache.GetApplicationDefault(clientId, creditCardProcessor, "ApiSecret") },
        //            };
        //            break;
        //        default:
        //            creditCardKVPs = new Dictionary<string, string>();
        //            var applicationDefaultModels = ArchLibCache.ApplicationDefaultModels.FindAll(x => x.ClientId == clientId && x.KVPKey == creditCardProcessor && x.SeqNum <= 20);
        //            foreach (var applicationDefaultModel in applicationDefaultModels)
        //            {
        //                creditCardKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
        //            }
        //            break;
        //    }
        //    return creditCardKVPs;
        //}
        #endregion
        // PRIVATE : GetCreditCardKVPs
        private void GetCreditCardKVPs(string creditCardProcessor, out Dictionary<string, string> creditCardKVPs, out Dictionary<string, string> creditCardDataKVPs, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            creditCardProcessor = creditCardProcessor.ToUpper();
            creditCardKVPs = new Dictionary<string, string>();
            creditCardDataKVPs = new Dictionary<string, string>();
            foreach (var applicationDefaultModel in ArchLibCache.ApplicationDefaultModels)
            {
                if (applicationDefaultModel.KVPKey == creditCardProcessor + "CLIENT" || applicationDefaultModel.KVPKey == creditCardProcessor)
                {
                    creditCardKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
                }
                else
                {
                    if (applicationDefaultModel.KVPKey == creditCardProcessor + "SERVER")
                    {
                        creditCardDataKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
                    }
                    //else
                    //{
                    //    if (applicationDefaultModel.KVPKey == creditCardProcessor)
                    //    {
                    //        creditCardKVPs[applicationDefaultModel.KVPSubKey] = applicationDefaultModel.KVPValue;
                    //    }
                    //}
                }
            }
            return;
        }
        // PRIVATE: OrderWIPAdd
        private void ShoppingCartWIPAdd(bool doNotBreakBundle, long corpAcctLocationId, ref float itemSeqNum, ShoppingCartItemModel shoppingCartItemModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    ShoppingCartWIPModel shoppingCartWIPModel;
                    //float bundleItemSeqNum = 0;
                    shoppingCartWIPModel = new ShoppingCartWIPModel
                    {
                        ClientId = clientId,
                        //BundleItemSeqNum = 0,
                        CorpAcctLocationId = corpAcctLocationId,
                        CreatedForPersonId = createForSessionObject.PersonId,
                        DoNotBreakBundle = doNotBreakBundle,
                        ItemId = shoppingCartItemModel.ItemId.Value,
                        OrderQty = shoppingCartItemModel.OrderQty.Value,
                        ParentItemId = shoppingCartItemModel.ParentItemId.Value,
                        PersonId = sessionObjectModel.PersonId,
                        ItemSeqNum = ++itemSeqNum,
                    };
                    ApplicationDataContext.ShoppingCartWIPAdd(shoppingCartWIPModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    shoppingCartItemModel.ShoppingCartWIPId = shoppingCartWIPModel.ShoppingCartWIPId.Value;
                    if (shoppingCartItemModel.ShoppingCartItemBundleModels != null && shoppingCartItemModel.ShoppingCartItemBundleModels.Count > 0)
                    {
                        foreach (var shoppingCartItemBundleModel in shoppingCartItemModel.ShoppingCartItemBundleModels)
                        {
                            shoppingCartWIPModel = new ShoppingCartWIPModel
                            {
                                ClientId = clientId,
                                //BundleItemSeqNum = ++bundleItemSeqNum,
                                CorpAcctLocationId = corpAcctLocationId,
                                CreatedForPersonId = createForSessionObject.PersonId,
                                DoNotBreakBundle = doNotBreakBundle,
                                ItemId = shoppingCartItemBundleModel.ItemId.Value,
                                OrderQty = shoppingCartItemBundleModel.OrderQty.Value,
                                ParentItemId = shoppingCartItemBundleModel.ParentItemId.Value,
                                PersonId = sessionObjectModel.PersonId,
                                ItemSeqNum = ++itemSeqNum,
                            };
                            ApplicationDataContext.ShoppingCartWIPAdd(shoppingCartWIPModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                            shoppingCartItemBundleModel.ShoppingCartWIPId = shoppingCartWIPModel.ShoppingCartWIPId.Value;
                        }
                    }
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
        // PRIVATE: OrderWIPAdd
        private void ShoppingCartWIPUpd(bool doNotBreakBundle, ShoppingCartItemModel shoppingCartItemModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    string sqlStmtBundle = "";
                    sqlStmtBundle += $"      SELECT {shoppingCartItemModel.ShoppingCartWIPId} AS ShoppingCartWIPId, {shoppingCartItemModel.OrderQty} AS OrderQty" + Environment.NewLine;
                    if (shoppingCartItemModel.ShoppingCartItemBundleModels != null && shoppingCartItemModel.ShoppingCartItemBundleModels.Count > 0)
                    {
                        foreach (var shoppingCartItemBundleModel in shoppingCartItemModel.ShoppingCartItemBundleModels)
                        {
                            sqlStmtBundle += $"UNION SELECT {shoppingCartItemBundleModel.ShoppingCartWIPId} AS ShoppingCartWIPId, {shoppingCartItemBundleModel.OrderQty} AS OrderQty" + Environment.NewLine;
                        }
                    }
                    ApplicationDataContext.ShoppingCartWIPUpd(sqlStmtBundle, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        #region Commenting Code for now - Need this after fixing
        //// PRIVATE: OrderWIPAdd
        //private void OrderWIPAdd(ref PaymentInfoModel paymentInfoModel, long corpAcctLocationId, AddToCartModel addToCartModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        //                    DoNotBreakBundle = addToCartModel.DoNotBreakBundle,
        //                    ItemId = addToCartModel.ItemModel.ItemId.Value,
        //                    ItemRate = addToCartModel.ItemModel.ItemRate.Value,
        //                    OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
        //                    OrderQty = addToCartModel.OrderQty,
        //                    ParentItemId = addToCartModel.ParentItemId,
        //                    SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
        //                }
        //            );
        //            ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (addToCartModel.ItemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //            {
        //                foreach (var shoppingCartItemBundleModel in paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.First(x => x.ItemId == addToCartModel.ItemModel.ItemId).ShoppingCartItemBundleModels)
        //                {
        //                    orderDetailWIPModel = new OrderDetailWIPModel
        //                    {
        //                        ClientId = clientId,
        //                        DoNotBreakBundle = addToCartModel.DoNotBreakBundle,
        //                        ItemId = shoppingCartItemBundleModel.ItemId.Value,
        //                        ItemRate = shoppingCartItemBundleModel.ItemRate.Value,
        //                        OrderHeaderWIPId = paymentInfoModel.OrderHeaderWIPModel.OrderHeaderWIPId.Value,
        //                        OrderQty = shoppingCartItemBundleModel.OrderQty.Value,
        //                        ParentItemId = addToCartModel.ParentItemId,
        //                        SeqNum = ++paymentInfoModel.OrderHeaderWIPModel.MaxSeqNum,
        //                    };
        //                    ApplicationDataContext.OrderDetailWIPAdd(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                }
        //            }
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
        //// PRIVATE : OrderHeaderWIPModelCreate
        //private OrderHeaderWIPModel OrderHeaderWIPModelCreate(long corpAcctLocationId, InvoiceTypeEnum? invoiceTypeId, string orderDateTime, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    OrderHeaderWIPModel orderHeaderWIPModel = new OrderHeaderWIPModel
        //    {
        //        ClientId = clientId,
        //        CorpAcctLocationId = corpAcctLocationId,
        //        CreatedForPersonId = createForSessionObject.PersonId,
        //        InvoiceTypeId = (long)invoiceTypeId,
        //        MaxSeqNum = 0,
        //        OrderDateTime = orderDateTime,
        //        OrderStatusId = null,
        //        PersonId = sessionObjectModel.PersonId,
        //        OrderDetailWIPModels = new List<OrderDetailWIPModel>()
        //    };
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return orderHeaderWIPModel;
        //}
        //// PRIVATE : OrderDetailWIPDel
        //private void OrderDetailWIPDel(long orderDetailWIPId, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OrderDetailWIPDel(orderDetailWIPId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        //// PRIVATE : OrderDetailWIPUpdate
        //private void OrderDetailWIPUpdate(ref PaymentInfoModel paymentInfoModel, ShoppingCartItemModel shoppingCartItemModel, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        OrderDetailWIPModel orderDetailWIPModel = paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.First(x => x.ItemId == shoppingCartItemModel.ItemId);
        //        orderDetailWIPModel.OrderQty = shoppingCartItemModel.OrderQty.Value;
        //        ApplicationDataContext.OrderDetailWIPUpd(orderDetailWIPModel.OrderHeaderWIPId, shoppingCartItemModel.ItemId.Value, shoppingCartItemModel.OrderQty.Value, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        //// PRIVATE : OrderWIPDel
        //private void OrderWIPDel(PaymentInfoModel paymentInfoModel, SqlConnection sqlConnection, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OrderDetailWIPsDelete(sessionObjectModel.PersonId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.OrderHeaderWIPDelete(sessionObjectModel.PersonId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        #region Commenting Code for now - Need this after fixing
        // PRIVATE : GetDeliveryChargeModel
        private DeliveryChargeModel GetDeliveryChargeModel(DemogInfoAddressModel deliveryAddressModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShippingService shippingService = new ShippingService();
            DeliveryChargeModel deliveryChargeModel;
            DemogInfoAddressModel demogInfoAddressModel = new DemogInfoAddressModel
            {
                DemogInfoCountryId = deliveryAddressModel.DemogInfoCountryId,
                DemogInfoSubDivisionId = deliveryAddressModel.DemogInfoSubDivisionId,
                DemogInfoCountyId = deliveryAddressModel.DemogInfoCountyId,
                DemogInfoCityId = deliveryAddressModel.DemogInfoCityId,
                DemogInfoZipId = deliveryAddressModel.DemogInfoZipId,
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
        #endregion
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
            DemogInfoAddressModel demogInfoAddressModel = paymentInfoModel.DeliveryInfoModel.DeliveryAddressModel;
            SearchDataModel searchDataModel = new SearchDataModel
            {
                SearchType = "ZipCode",
                SearchKeyValuePairs = new Dictionary<string, string>
                {
                    { "DemogInfoCountryId", demogInfoAddressModel.DemogInfoCountryId == null ? "0" : demogInfoAddressModel.DemogInfoCountryId.ToString() },
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
            //DemogInfoCountryModel demogInfoCountryModel;
            //try
            //{
            //    demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneDemogInfoCountryId);
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneFormatted = "+" + paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneNum).ToString("##### #####");
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneHref = paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneNum).ToString("###-###-####");
            //}
            //catch
            //{
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = null;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneFormatted = null;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneTelephoneCode = null;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.AlternateTelephoneHref = null;
            //}
            //try
            //{
            //    demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.FirstOrDefault(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneDemogInfoCountryId);
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted = "+" + paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneNum).ToString(demogInfoCountryModel.TelephoneFormat);
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneHref = paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneNum).ToString("###-###-####");
            //}
            //catch
            //{
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = null;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneFormatted = null;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneTelephoneCode = null;
            //    paymentInfoModel.DeliveryInfoModel.DeliveryDataModel.PrimaryTelephoneHref = null;
            //}

            //demogInfoAddressModel.BuildingTypeDesc = demogInfoAddressModel.BuildingTypeId == null ? "" : LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "BuildingType").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)demogInfoAddressModel.BuildingTypeId).CodeDataNameDesc;
            //demogInfoAddressModel.BuildingTypeHouseNumber = string.IsNullOrWhiteSpace(demogInfoAddressModel.BuildingTypeDesc) ? "" : (demogInfoAddressModel.BuildingTypeDesc + " ");
            //demogInfoAddressModel.BuildingTypeHouseNumber += string.IsNullOrWhiteSpace(demogInfoAddressModel.HouseNumber) ? "" : demogInfoAddressModel.HouseNumber.Trim();

            //paymentInfoModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodDesc = paymentInfoModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodId == null ? "" : LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)paymentInfoModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodId).CodeDataDesc0;

            //if (paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeId == null)
            //{
            //    paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeDesc = "";
            //    paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeDesc1 = "";
            //}
            //else
            //{
            //    var codeDataModel = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeId);
            //    if (codeDataModel != null)
            //    {
            //        paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeDesc = codeDataModel.CodeDataDesc0;
            //        paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeDesc1 = codeDataModel.CodeDataDesc2;
            //    }
            //    else
            //    {
            //        paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeDesc = "";
            //        paymentInfoModel.DeliveryInfoModel.PaymentModeModel.PaymentModeDesc1 = "";
            //    }
            //}
        }
        #endregion
    }
}
