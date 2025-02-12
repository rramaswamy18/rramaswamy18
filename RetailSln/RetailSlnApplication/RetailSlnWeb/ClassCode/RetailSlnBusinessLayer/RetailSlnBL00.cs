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
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: AddToCart
        public string AddToCart(ref PaymentInfo1Model paymentInfo1Model, string itemIdParm, string orderQtyParm, bool createOrderWIP, bool apiFlag, bool webFlag, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
                    paymentInfo1Model = paymentInfo1Model ?? new PaymentInfo1Model();
                    ApplicationDataContext.OpenSqlConnection();
                    ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
                    long corpAcctId = applSessionObjectModel.CorpAcctModel.CorpAcctId.Value;
                    if (createOrderWIP)
                    {
                        CreateOrderWIP(ref paymentInfo1Model, applSessionObjectModel.CorpAcctLocationId, itemId, orderQty, apiFlag, webFlag, ApplicationDataContext.SqlConnectionObject, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    float itemDiscountPercent, heightValue, lengthValue, widthValue;
                    string dimensionValue;
                    DimensionUnitEnum dimensionUnitValue;
                    ItemBundleModel itemBundleModel;
                    ShoppingCartItemModel shoppingCartItemModel;
                    paymentInfo1Model.ShoppingCartModel = paymentInfo1Model.ShoppingCartModel ?? new ShoppingCartModel
                    {
                        ShoppingCartItems = new List<ShoppingCartItemModel>(),
                        ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
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
                            },
                        },
                    };
                    if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                    {
                        itemBundleModel = RetailSlnCache.ItemBundleModels.First(x => x.ItemId == itemId);
                        //itemDiscountPercent = itemBundleModel.DiscountPercent;
                    }
                    else
                    {
                        itemBundleModel = null;
                        //itemDiscountPercent = 0;
                    }
                    itemDiscountPercent = GetItemDiscountPercent(itemId, corpAcctId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    dimensionValue = itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue;
                    float.TryParse(dimensionValue, out heightValue);
                    dimensionValue = itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue;
                    float.TryParse(dimensionValue, out lengthValue);
                    dimensionValue = itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue;
                    float.TryParse(dimensionValue, out widthValue);
                    try
                    {
                        dimensionUnitValue = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue);
                    }
                    catch
                    {
                        dimensionUnitValue = DimensionUnitEnum.Centimeter;
                    }
                    shoppingCartItemModel = paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
                    if (shoppingCartItemModel == null)
                    {
                        paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.Add(shoppingCartItemModel = CreateShoppingCartItem(itemModel, itemBundleModel, dimensionUnitValue, heightValue, itemDiscountPercent, lengthValue, orderQty, widthValue, clientId, ipAddress, execUniqueId, loggedInUserId));
                    }
                    else
                    {
                        shoppingCartItemModel.OrderQty += orderQty;
                    }
                    shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.VolumeValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightCalcValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightCalcValue;
                    shoppingCartItemModel.WeightValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightValue;
                    shoppingCartItemModel.ProductOrVolumetricWeight = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
                    //ApplicationDataContext.OpenSqlConnection();
                    UpdateShoppingCart(ref paymentInfo1Model, paymentInfo1Model.ShoppingCartModel, apiFlag, webFlag, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                }
                return errorMessage;
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
        // GET: Checkout
        public CheckoutModel Checkout(PaymentInfo1Model paymentInfoModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    LoginUserProfGuestModel = new LoginUserProfGuestModel
                    {
                        LoginUserProfGuestTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry")),
                        OTPModel = new OTPModel
                        {

                        },
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    LoginUserProfModel = new LoginUserProfModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    OTPModel = new OTPModel
                    {
                        OTPTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry")),
                        OTPTelephoneCountryIdConfirm = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry")),
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
                    checkoutModel.OTPModel.CaptchaAnswerOTP = null;
                    checkoutModel.OTPModel.CaptchaNumberOTP0 = httpSessionStateBase["CaptchaNumberOTP0"].ToString();
                    checkoutModel.OTPModel.CaptchaNumberOTP1 = httpSessionStateBase["CaptchaNumberOTP1"].ToString();
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
        // GET: CheckoutValidate
        public void CheckoutValidate(PaymentInfo1Model paymentInfoModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfoModel = paymentInfoModel ?? new PaymentInfo1Model();
                ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount > 0)
                    {
                        ;
                    }
                    else
                    {
                        throw new Exception("Shopping Cart is Empty");
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
        // GET: DeliveryInfo
        public void DeliveryInfo(ref PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfoModel = paymentInfoModel ?? new PaymentInfo1Model();
                if (paymentInfoModel.ShoppingCartModel == null)
                {
                    paymentInfoModel = new PaymentInfo1Model
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>
                            {
                                new KeyValuePair<string, List<string>>
                                (
                                    "",
                                    new List<string>
                                    {
                                        "Invalid shopping cart (Null)",
                                    }
                                ),
                            },
                            ResponseTypeId = ResponseTypeEnum.Error,
                            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                        },
                    };
                }
                else
                {
                    if (paymentInfoModel.ShoppingCartModel.ShoppingCartItems.Count > 0 && paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount > 0)
                    {
                        paymentInfoModel.OrderSummaryModel = new OrderSummaryModel
                        {
                            AspNetUserId = sessionObjectModel.AspNetUserId,
                            //AuthorizedSignature = ArchLibCache.GetApplicationDefault(clientId, "AuthorizedSignature", ""),
                            //AuthorizedSignatureText = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "AuthorizedSignatureFont", "")),
                            CorpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel,
                            CreatedByEmailAddress = sessionObjectModel.EmailAddress.ToLower(),
                            CreatedByFirstName = sessionObjectModel.FirstName,
                            CreatedByLastName = sessionObjectModel.LastName,
                            EmailAddress = createForSessionObject.EmailAddress.ToLower(),
                            FirstName = createForSessionObject.FirstName,
                            LastName = createForSessionObject.LastName,
                            PersonId = createForSessionObject.PersonId,
                            TelephoneCode = null,
                            TelephoneCountryId = createForSessionObject.TelephoneCountryId.Value,
                            TelephoneNumber = createForSessionObject.PhoneNumber,
                        };
                        paymentInfoModel.CouponPaymentModel = new CouponPaymentModel
                        {
                            CouponNumber = "",
                            CouponPaymentAmount = 0,
                        };
                        paymentInfoModel.DeliveryAddressModel = paymentInfoModel.DeliveryAddressModel ?? new DemogInfoAddressModel
                        {
                            BuildingTypeId = BuildingTypeEnum._,
                            BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
                            DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
                            DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId],
                        };
                        paymentInfoModel.DeliveryDataModel = paymentInfoModel.DeliveryDataModel ?? new DeliveryDataModel
                        {
                            AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                        };
                        if (
                            string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.AddressLine1) &&
                            string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.CityName) &&
                            string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode)
                           )
                        {
                            var applSessionObjectModel = (ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel;
                            var corpAcctLocationModel = applSessionObjectModel.CorpAcctModel.CorpAcctLocationModels.First(x => x.CorpAcctLocationId == applSessionObjectModel.CorpAcctLocationId);
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
                                paymentInfoModel.DeliveryDataModel.AlternateTelephoneNum = corpAcctLocationModel.AlternateTelephoneNumber.Value.ToString();
                            }
                            catch
                            {
                            }
                        }
                        paymentInfoModel.GiftCertPaymentModel = new GiftCertPaymentModel
                        {
                            GiftCertPaymentAmount = 0,
                        };
                        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseMessages = new List<string>(),
                            ResponseTypeId = ResponseTypeEnum.Success,
                        };
                        BuildDeliveryInfoLookup(paymentInfoModel, createForSessionObject, apiFlag, webFlag, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    else
                    {
                        paymentInfoModel = new PaymentInfo1Model
                        {
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>
                                {
                                    new KeyValuePair<string, List<string>>
                                    (
                                        "",
                                        new List<string>
                                        {
                                            "Invalid shopping cart - no items",
                                        }
                                    ),
                                },
                                ResponseTypeId = ResponseTypeEnum.Error,
                                ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                            },
                        };
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
        // POST: DeliveryInfo
        public void DeliveryInfo(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                CorpAcctModel corpAcctModel = (((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel);
                CalculateDiscounts(paymentInfoModel, createForSessionObject.PersonId, corpAcctModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateShoppingCartTotals(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                var salesTaxListModels = GetSalesTaxListModels(paymentInfoModel.DeliveryAddressModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                var salesTaxCaptionIds = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("SalesTaxType", "");
                CalculateSalesTax(paymentInfoModel, salesTaxListModels, salesTaxCaptionIds, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (paymentInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                {
                    ;
                }
                else
                {
                    CalculateDeliveryCharges(paymentInfoModel, corpAcctModel, salesTaxListModels, salesTaxCaptionIds, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                CalculateShoppingCartSummaryTotals(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                BuildCreditCardDataModel(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public OrderCategoryItemModel OrderCategoryItem(string aspNetRoleName, string parentCategoryIdParm, string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (string.IsNullOrWhiteSpace(pageNumParm) || string.IsNullOrWhiteSpace(pageSizeParm))
                {
                    pageNumParm = "1";
                    pageSizeParm = "45";
                }
                long? parentCategoryId;
                try
                {
                    parentCategoryId = int.Parse(parentCategoryIdParm);
                }
                catch
                {
                    //For now hardcode based on role - Modify code to get it from Asp KVP table
                    //Until data in this table needs to be organizws
                    switch (aspNetRoleName)
                    {
                        case "BULKORDERSROLE":
                        case "MARKETINGROLE":
                        case "PRIESTROLE":
                            parentCategoryId = 102;
                            break;
                        case "WHOLESALEROLE":
                            parentCategoryId = 120;
                            break;
                        default:
                            parentCategoryId = 100;
                            break;
                    }
                }
                string viewName;
                switch (aspNetRoleName)
                {
                    case "BULKORDERSROLE":
                    case "PRIESTROLE":
                    case "WHOLESALEROLE":
                        viewName = "_OrderItem1";
                        break;
                    case "APPLADMN1":
                    case "MARKETINGROLE":
                    case "SYSTADMIN":
                        viewName = "_OrderItem2";
                        break;
                    default:
                        viewName = "_OrderItem0";
                        break;
                }
                int.TryParse(pageNumParm, out int pageNum);
                int.TryParse(pageSizeParm, out int pageSize);
                pageSize = pageSize == 0 ? 45 : pageSize;
                if (RetailSlnCache.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][0].FirstOrDefault(x => x.CategoryId == parentCategoryId.Value) == null)
                {
                    throw new ApplicationException("This page is backdated. Reloading page.");
                }
                List<CategoryItemMasterHierModel> categoryItemMasterHierModels;
                try
                {
                    categoryItemMasterHierModels = RetailSlnCache.AspNetRoleParentCategoryCategoryItemMasterHierModels[aspNetRoleName][parentCategoryId.Value];
                }
                catch
                {
                    categoryItemMasterHierModels = new List<CategoryItemMasterHierModel>();
                }
                int totalRowCount = categoryItemMasterHierModels.Count;
                int pageCount = totalRowCount / pageSize;
                if (totalRowCount % pageSize != 0)
                {
                    pageCount++;
                }
                OrderCategoryItemModel orderCategoryItemModel = new OrderCategoryItemModel
                {
                    //CategoryModels = RetailSlnCache.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][0],
                    CategoryItemMasterHierModels = categoryItemMasterHierModels.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNum = pageNum,
                    PageSize = pageSize,
                    ParentCategoryId = parentCategoryId.Value,
                    ParentCategoryModel = RetailSlnCache.CategoryModels.First(x => x.CategoryId == parentCategoryId.Value),
                    TotalRowCount = totalRowCount,
                    ViewName = viewName,
                };
                ApplicationDataContext.OpenSqlConnection();
                ItemDiscountModel itemDiscountModel;
                long? corpAcctId = ((ApplSessionObjectModel)sessionObjectModel?.ApplSessionObjectModel)?.CorpAcctModel.CorpAcctId;
                if (corpAcctId == null)
                {
                    corpAcctId = 0;
                }
                string itemIds = "", prefixString = "";
                foreach (var categoryItemMasterHierModel in orderCategoryItemModel.CategoryItemMasterHierModels)
                {
                    foreach (var itemModel in categoryItemMasterHierModel.ItemMasterModel.ItemModels)
                    {
                        itemIds += prefixString + itemModel.ItemId;
                        prefixString = ",";
                    }
                }
                List<ItemDiscountModel> itemDiscountModels = GetItemDiscountsPercent(itemIds, corpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                foreach (var categoryItemMasterHierModel in orderCategoryItemModel.CategoryItemMasterHierModels)
                {
                    foreach (var itemModel in categoryItemMasterHierModel.ItemMasterModel.ItemModels)
                    {
                        itemModel.ItemDiscountModels = new List<ItemDiscountModel>();
                        itemDiscountModel = itemDiscountModels.FirstOrDefault(x => x.CorpAcctId == corpAcctId && x.ItemId == itemModel.ItemId);
                        if (itemDiscountModel != null)
                        {
                            itemModel.ItemDiscountModels.Add(itemDiscountModel);
                        }
                    }
                }
                if (viewName == "_OrderItem2")
                {
                    orderCategoryItemModel.UserAddEditModel = new UserAddEditModel
                    {
                        AlternateTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                        TelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                    };
                }
                return orderCategoryItemModel;
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
        public OrderCategoryItemListModel OrderCategoryItemList(string aspNetRoleName, string parentCategoryIdParm, string pageNumParm, string rowCountParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                var aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                if (string.IsNullOrWhiteSpace(parentCategoryIdParm))
                {
                    parentCategoryIdParm = aspNetRoleKVPs["ParentCategoryId02"].KVPValueData;
                }
                if (string.IsNullOrWhiteSpace(pageNumParm) || string.IsNullOrWhiteSpace(rowCountParm))
                {
                    pageNumParm = aspNetRoleKVPs["PageNum02"].KVPValueData;
                    rowCountParm = aspNetRoleKVPs["RowCount02"].KVPValueData;
                }
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                int.TryParse(pageNumParm, out int pageNum);
                pageNum = pageNum == 0 ? 1 : pageNum;
                int.TryParse(rowCountParm, out int rowCount);
                rowCount = rowCount == 0 ? 45 : rowCount;
                OrderCategoryItemListModel orderCategoryItemListModel = new OrderCategoryItemListModel
                {
                    ParentCategoryId = parentCategoryId,
                    PageNum = pageNum,
                    RowCount = rowCount,
                    //ItemMasterModels = RetailSlnCache.CategoryItemMasterHierModels.FindAll
                    //                   (x => x.ParentCategoryId == parentCategoryId && x.ItemMasterId != null)
                    //                   .OrderBy(x => x.SeqNum).Skip((pageNum - 1) * rowCount).Take(rowCount)
                    //                   .Select(r => r.ItemMasterModel).ToList(),
                };
                return orderCategoryItemListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
        }
        // POST: RemoveFromCart
        public void RemoveFromCart(ref PaymentInfo1Model paymentInfo1Model, int index, bool apiFlag, bool webFlag, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel = paymentInfo1Model.ShoppingCartModel;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (index > -1 && index < shoppingCartModel.ShoppingCartItems.Count)
                    {
                        shoppingCartModel.ShoppingCartItems.RemoveAt(index);
                        ApplicationDataContext.OpenSqlConnection();
                        UpdateShoppingCart(ref paymentInfo1Model, shoppingCartModel, apiFlag, webFlag, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        //httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                        return;
                    }
                    else
                    {
                        throw new Exception("Invalid index in remove from cart");
                    }
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
        // GET : ShoppingCartComments
        public void ShoppingCartComments(PaymentInfo1Model paymentInfoModel, int index, string orderComments, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = paymentInfoModel.ShoppingCartModel;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (index > -1 && index < shoppingCartModel.ShoppingCartItems.Count)
                    {
                        shoppingCartModel.ShoppingCartItems[index].OrderComments = orderComments;
                        return;
                    }
                    else
                    {
                        throw new Exception("Invalid index shopping cart comments");
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        // POST : PaymentInfo1
        public string PaymentInfo1(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//CreditSales
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                paymentInfoModel.CompleteOrderModel.PaymentAmount = paymentInfoModel.CompleteOrderModel.PaymentAmount ?? 0;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard += paymentInfoModel.CompleteOrderModel.PaymentAmount;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid += paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = (float)Math.Round(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value, 0);
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount - paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value;
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");

                ShoppingCartItemModel shoppingCartItemModel;

                shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalInvoiceAmount);
                shoppingCartItemModel.OrderAmount = (float)Math.Round(shoppingCartItemModel.OrderAmount.Value, 0);

                shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
                shoppingCartItemModel.OrderAmount += paymentInfoModel.CompleteOrderModel.PaymentAmount;
                shoppingCartItemModel.OrderAmount = (float)Math.Round(shoppingCartItemModel.OrderAmount.Value, 0);

                shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                shoppingCartItemModel.OrderAmount += paymentInfoModel.CompleteOrderModel.PaymentAmount;
                shoppingCartItemModel.OrderAmount = (float)Math.Round(shoppingCartItemModel.OrderAmount.Value, 0);

                shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderAmount - paymentInfoModel.CompleteOrderModel.PaymentAmount;
                shoppingCartItemModel.OrderAmount = (float)Math.Round(shoppingCartItemModel.OrderAmount.Value, 0);

                long codeDataNameId = paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
                CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
                paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;
                CreateOrder(paymentInfoModel, "", "", sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                string htmlString = CreateInvoice(paymentInfoModel, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public RazorPayResponse PaymentInfo2(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//Razorpay
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            object creditCardResponseObject = null;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
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
        public string PaymentInfo3(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid += float.Parse(paymentInfoModel.CreditCardDataModel.CreditCardAmount);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = (float)Math.Round(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value, 2);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = (float)Math.Round(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value, 2);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount - paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                    var shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    shoppingCartSummaryItem.OrderComments = "Payment Id : " + razorpay_payment_id + " - Order Id : " + razorpay_order_id;
                    shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue;
                    string amountInWords;
                    switch (clientId)
                    {
                        case 97:
                            amountInWords = ConvertAmountToWordsRupees(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value);
                            break;
                        default:
                            amountInWords = ConvertAmountToWordsDollars(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value);
                            break;
                    }
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountInWords = amountInWords;
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");
                    long codeDataNameId = paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
                    CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;
                    CreateOrder(paymentInfoModel, razorpay_payment_id, razorpay_order_id, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string htmlString = CreateInvoice(paymentInfoModel, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    return htmlString;
                }
                else
                {
                    throw new Exception("Error while validating Payment Gateway");
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
        // POST : PaymentInfo5
        public string PaymentInfo5(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {//RazorpayReturn
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            string htmlString;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                //creditCardProcessor = "NUVEIPROD";
                CreditCardDataModel creditCardDataModel = new CreditCardDataModel
                {
                    CreditCardAmount = paymentInfoModel.CreditCardProcessModel.CreditCardAmount.Value.ToString("0.00"),
                    CreditCardExpMM = paymentInfoModel.CreditCardProcessModel.CardExpiryMM,
                    CreditCardExpYear = paymentInfoModel.CreditCardProcessModel.CardExpiryYYYY,
                    CreditCardNumber = paymentInfoModel.CreditCardProcessModel.CreditCardNumber,
                    CreditCardNumberLast4 = paymentInfoModel.CreditCardProcessModel.CreditCardNumber.Substring(paymentInfoModel.CreditCardProcessModel.CreditCardNumber.Length - 4),
                    CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId, ipAddress, execUniqueId, loggedInUserId),
                    CreditCardProcessor = creditCardProcessor,
                    CreditCardSecCode = paymentInfoModel.CreditCardProcessModel.CVV,
                    CreditCardTranType = "PAYMENT",
                    CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                    NameAsOnCard = paymentInfoModel.CreditCardProcessModel.CardHolderName,
                };
                //creditCardDataModel.CreditCardAmount = "0.09";
                //creditCardDataModel.CreditCardAmount = null;
                //paymentInfoModel.CreditCardDataModel.CreditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                //paymentInfoModel.CreditCardDataModel.CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation");
                //paymentInfoModel.CreditCardDataModel.CreditCardKVPs = GetCreditCardKVPs(paymentInfoModel.CreditCardDataModel.CreditCardProcessor, clientId, ipAddress, execUniqueId, loggedInUserId);
                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
                var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out object creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                var creditCardLast4 = paymentInfoModel.CreditCardDataModel.CreditCardNumberLast4;
                var creditCardProcessMessage = processMessage;
                if (creditCardProcessStatus)
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid += float.Parse(paymentInfoModel.CreditCardDataModel.CreditCardAmount);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = (float)Math.Round(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value, 2);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid = (float)Math.Round(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value, 2);
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount - paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid.Value;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                    var shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    shoppingCartSummaryItem.OrderComments = "Reference# : " + processMessage;
                    shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue;
                    string amountInWords;
                    switch (clientId)
                    {
                        case 97:
                            amountInWords = ConvertAmountToWordsRupees(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value);
                            break;
                        default:
                            amountInWords = ConvertAmountToWordsDollars(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount.Value);
                            break;
                    }
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountInWords = amountInWords;
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextId"));
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextValue = ArchLibCache.GetApplicationDefault(clientId, "Business", "AuthorizedSignatureTextValue");
                    long codeDataNameId = paymentInfoModel.OrderSummaryModel.AuthorizedSignatureTextId;
                    CodeDataModel codeDataModel = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc("SignatureText", execUniqueId).First(x => x.CodeDataNameId == codeDataNameId);
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontFamily = codeDataModel.CodeDataNameDesc;
                    paymentInfoModel.OrderSummaryModel.AuthorizedSignatureFontSize = codeDataModel.CodeDataDesc1;
                    CreateOrder(paymentInfoModel, "", "", sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = CreateInvoice(paymentInfoModel, controller, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    modelStateDictionary.AddModelError("", "Error while processing credit card");
                    modelStateDictionary.AddModelError("", processMessage);
                    paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(controller, "_PaymentInfo4", paymentInfoModel);
                }
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
        // GET : SearchForEmailAddress
        public SearchForUserDataModel SearchForUserData(string searchText, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SearchForUserDataModel searchForUserDataModel;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                searchForUserDataModel = ApplicationDataContext.GetSearchForUserData(searchText, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                searchForUserDataModel = new SearchForUserDataModel
                {
                    SearchText = searchText,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            "Error while searching " + searchText,
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
            return searchForUserDataModel;
        }
        // GET : SearchForEmailAddress
        public SearchForItemModel SearchForItem(string parentCategoryIdParm, string searchText, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SearchForItemModel searchForItemModel;
            try
            {
                long? parentCategoryId = long.Parse(parentCategoryIdParm);
                string aspNetRoleName = sessionObjectModel.AspNetRoleName;
                List<CategoryItemMasterHierModel> categoryItemMasterHierModels = RetailSlnCache.AspNetRoleParentCategoryCategoryItemMasterHierModels[aspNetRoleName][parentCategoryId.Value];
                List<CategoryItemMasterHierModel> categoryItemMasterHierModelsSearch = categoryItemMasterHierModels.FindAll(x => x.ItemMasterModel.ItemMasterDesc.ToUpper().Contains(searchText.ToUpper()));
                searchForItemModel = new SearchForItemModel
                {
                    ParentCategoryId = parentCategoryId.Value,
                    ItemMasterModels = new List<ItemMasterModel>(),
                    //ItemMasterModels = categoryItemMasterHierModels.FindAll(x => x.ItemMasterModel.ItemMasterDesc.ToUpper().Contains(searchText.ToUpper())),
                };
                foreach (var categoryItemMasterHierModelSearch in categoryItemMasterHierModelsSearch)
                {
                    searchForItemModel.ItemMasterModels.Add(categoryItemMasterHierModelSearch.ItemMasterModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                searchForItemModel = new SearchForItemModel
                {
                    ParentCategoryId = long.Parse(parentCategoryIdParm),
                    SearchText = searchText,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            "Error while searching " + searchText,
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
            return searchForItemModel;
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
                    SearchMetaDataModels = ApplicationDataContext.GetSearchMetaDatas(searchKeywordText, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
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
        // POST : UserAddEdit
        public void UserAddEdit(ref UserAddEditModel userAddEditModel, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                var personCount = ApplicationDataContext.GetPersonCountForCorpAcctId(userAddEditModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                var corpAcctId = userAddEditModel.CorpAcctId;
                CorpAcctModel corpAcctModel = RetailSlnCache.CorpAcctModels.First(x => x.CorpAcctId == corpAcctId);
                string userName;
                switch (userAddEditModel.LoginTypeId)
                {
                    case LoginTypeEnum.LoginId:
                        userAddEditModel.LoginId = corpAcctModel.CorpAcctKey + ++personCount;
                        userName = userAddEditModel.LoginId;
                        break;
                    case LoginTypeEnum.EmailAddress:
                        userName = userAddEditModel.EmailAddress;
                        userAddEditModel.LoginId = null;
                        break;
                    case LoginTypeEnum.TelephoneNumberWithExtension:
                        userName = userAddEditModel.EmailAddress;
                        userAddEditModel.LoginId = null;
                        break;
                    default:
                        userName = userAddEditModel.EmailAddress;
                        userAddEditModel.LoginId = null;
                        break;
                }
                AspNetUserModel aspNetUserModel = new AspNetUserModel
                {
                    AlternateTelephoneCountryId = userAddEditModel.AlternateTelephoneCountryId,
                    AlternateTelephoneNumber = userAddEditModel.AlternateTelephoneNumber,
                    AspNetRoleUserTypeId = userAddEditModel.AspNetRoleUserTypeId,
                    Email = userAddEditModel.EmailAddress,
                    FirstName = userAddEditModel.FirstName,
                    LastName = userAddEditModel.LastName,
                    LoginId = userAddEditModel.LoginId,
                    LoginTypeId = userAddEditModel.LoginTypeId,
                    PhoneNumber = userAddEditModel.TelephoneNumber,
                    TelephoneCountryId = userAddEditModel.TelephoneCountryId,
                    UserName = userName,
                };
                ArchLibBL archLibBL = new ArchLibBL();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before ArchLib UserAddEdit");
                archLibBL.UserAddEdit(ref aspNetUserModel, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (modelStateDictionary.IsValid)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After ArchLib UserAddEdit Success");
                    ApplicationDataContext.AddPersonExtn1(aspNetUserModel.PersonModel.PersonId.Value, userAddEditModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_UserAddEditEmailSubject", userAddEditModel);
                    string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_UserAddEditEmailBody", userAddEditModel);
                    string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", userAddEditModel);
                    string toEmailAddresss = userAddEditModel.EmailAddress + ";" + sessionObjectModel.EmailAddress;
                    archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml + signatureHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    userAddEditModel = new UserAddEditModel
                    {
                        AlternateTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                        TelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                    };
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After ArchLib UserAddEdit Error");
                    modelStateDictionary.AddModelError("", "Error occurred while saving User");
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
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        public void LoadOrderWIP(bool apiFlag, bool webFlag, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (sessionObjectModel == null)
                {
                }
                else
                {
                    ApplicationDataContext.OpenSqlConnection();
                    //Get Max Order Header Id for the logged in user
                    long? orderHeaderWIPId = ApplicationDataContext.GetMaxOrderHeaderWIPId(sessionObjectModel.PersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (orderHeaderWIPId != null)
                    {
                        //Get WIP Order Header & Detail
                        OrderHeaderWIPModel orderHeaderWIPModel = ApplicationDataContext.GetOrderHeaderWIP(orderHeaderWIPId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (orderHeaderWIPModel != null)
                        {
                            ArchLibBL archLibBL = new ArchLibBL();
                            createForSessionObject = archLibBL.BuildSessionObject(orderHeaderWIPModel.CreatedForPersonId, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                            ApplSessionObjectModel applSessionObjectModel = LoginUserProf(createForSessionObject.PersonId, orderHeaderWIPModel.CorpAcctLocationId, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                            createForSessionObject.ApplSessionObjectModel = applSessionObjectModel;
                            httpSessionStateBase["CreateForSessionObject"] = createForSessionObject;
                            //Build Shopping Cart
                            PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)httpSessionStateBase["PaymentInfo"];
                            foreach (var orderDetailWIPModel in orderHeaderWIPModel.OrderDetailWIPModels)
                            {
                                AddToCart(ref paymentInfoModel, orderDetailWIPModel.ItemId.ToString(), orderDetailWIPModel.OrderQty.ToString(), false, apiFlag, webFlag, sessionObjectModel, createForSessionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                            }
                            paymentInfoModel.OrderHeaderWIPModel = orderHeaderWIPModel;
                            httpSessionStateBase["PaymentInfo"] = paymentInfoModel;
                        }
                    }
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
        //Private Methods
        private void CreateOrderWIP(ref PaymentInfo1Model paymentInfoModel, long corpAcctLocationId, long itemId, long orderQty, bool apiFlag, bool webFlag, SqlConnection sqlConnection, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    if (paymentInfoModel.OrderHeaderWIPModel == null)
                    {
                        //Create Order Header WIP
                        paymentInfoModel.OrderHeaderWIPModel = CreateOrderHeaderWIPModel(corpAcctLocationId, sessionObjectModel, createForSessionObject, controller, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.AddOrderHeaderWIP(paymentInfoModel.OrderHeaderWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                            SeqNum = paymentInfoModel.OrderHeaderWIPModel.OrderDetailWIPModels.Count + 1,
                        }
                    );
                    ApplicationDataContext.AddOrderDetailWIP(orderDetailWIPModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
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
        private OrderHeaderWIPModel CreateOrderHeaderWIPModel(long corpAcctLocationId, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            OrderHeaderWIPModel orderHeaderWIPModel = new OrderHeaderWIPModel
            {
                ClientId = clientId,
                CreatedForPersonId = createForSessionObject.PersonId,
                OrderDateTime = null,
                OrderStatusId = null,
                PersonId = sessionObjectModel.PersonId,
                OrderDetailWIPModels = new List<OrderDetailWIPModel>()
            };
            return orderHeaderWIPModel;
        }
        private ShoppingCartItemModel CreateShoppingCartItem(ItemModel itemModel, ItemBundleModel itemBundleModel, DimensionUnitEnum? dimensionUnitValue, float? heightValue, float? itemDiscountPercent, float? lengthValue, long? orderQty, float? widthValue, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShoppingCartItemModel shoppingCartItemModel = new ShoppingCartItemModel
            {
                DimensionUnitId = dimensionUnitValue,//(DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue),
                HeightValue = heightValue,//float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue),
                HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
                ItemBundleModel = itemBundleModel,
                ItemDiscountPercent = itemDiscountPercent,
                ItemId = itemModel.ItemId,
                ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                ItemRate = itemModel.ItemRate * (100 - itemDiscountPercent) / 100,
                ItemRateBeforeDiscount = itemModel.ItemRate,
                ItemShortDesc = itemModel.ItemShortDesc,
                LengthValue = lengthValue,//float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue),
                OrderAmount = orderQty * itemModel.ItemRate,
                OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                OrderQty = orderQty,
                ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
                WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue),
                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue),
                WidthValue = widthValue,//float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue),
                ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue),
                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
                ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
            };
            return shoppingCartItemModel;
        }
        public void UpdateShoppingCart(ref PaymentInfo1Model paymentInfoModel, bool apiFlag, bool webFlag, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (paymentInfoModel != null && paymentInfoModel.ShoppingCartModel != null)
                {
                    ApplicationDataContext.OpenSqlConnection();
                    UpdateShoppingCart(ref paymentInfoModel, apiFlag, webFlag, ApplicationDataContext.SqlConnectionObject, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.CloseSqlConnection();
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exit");
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
        private void UpdateShoppingCart(ref PaymentInfo1Model paymentInfoModel, bool apiFlag, bool webFlag, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");

            try
            {
                CorpAcctModel corpAcctModel = (((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel);
                CalculateDiscounts(paymentInfoModel, createForSessionObject.PersonId, corpAcctModel.CorpAcctId.Value, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateShoppingCartTotals(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (paymentInfoModel.DeliveryAddressModel == null)
                {
                    paymentInfoModel.DeliveryAddressModel = new DemogInfoAddressModel { BuildingTypeId = BuildingTypeEnum._ };
                }
                if (paymentInfoModel.GiftCertPaymentModel == null)
                {
                    paymentInfoModel.GiftCertPaymentModel = new GiftCertPaymentModel { GiftCertPaymentAmount = 0 };
                }
                if (paymentInfoModel.CouponPaymentModel == null)
                {
                    paymentInfoModel.CouponPaymentModel = new CouponPaymentModel { CouponPaymentAmount = 0 };
                }
                var salesTaxListModels = GetSalesTaxListModels(paymentInfoModel.DeliveryAddressModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                var salesTaxCaptionIds = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("SalesTaxType", "");
                CalculateSalesTax(paymentInfoModel, salesTaxListModels, salesTaxCaptionIds, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (paymentInfoModel.DeliveryMethodModel != null)
                {
                    if (paymentInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                    {
                        ;
                    }
                    else
                    {
                        CalculateDeliveryCharges(paymentInfoModel, corpAcctModel, salesTaxListModels, salesTaxCaptionIds, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                CalculateShoppingCartSummaryTotals(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private void UpdateShoppingCart(ref PaymentInfo1Model paymentInfo1Model, ShoppingCartModel shoppingCartModel, bool apiFlag, bool webFlag, SqlConnection sqlConnection, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                shoppingCartModel.ShoppingCartSummaryModel = new ShoppingCartSummaryModel
                {
                    AmountPaidByCreditCard = 0,
                    AmountPaidByGiftCert = 0,
                    BalanceDue = 0,
                    BalanceDueFormatted = "",
                    TotalAmountPaid = 0,
                    TotalDiscountAmount = 0,
                    TotalInvoiceAmount = 0,
                    TotalInvoiceAmountFormatted = "",
                    TotalItemsCount = 0,
                    TotalOrderAmount = 0,
                    TotalOrderAmountBeforeDiscount = 0,
                    TotalProductOrVolumetricWeight = 0,
                    TotalProductOrVolumetricWeightRounded = 0,
                    TotalVolumeValue = 0,
                    TotalWeightCalc = 0,
                };
                foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                {
                    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItem.OrderAmount;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue += shoppingCartItem.VolumeValue;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItem.WeightCalcValue;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount += shoppingCartItem.OrderQty.Value;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItem.ProductOrVolumetricWeight;
                }
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
                shoppingCartModel.ShoppingCartSummaryItems[0].ItemShortDesc = "Total Order Amount (Total Qty : " + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ")";
                shoppingCartModel.ShoppingCartSummaryItems[0].OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                if (!(paymentInfo1Model == null || sessionObjectModel == null || createForSessionObject == null))
                {
                    UpdateShoppingCart(ref paymentInfo1Model, apiFlag, webFlag, sqlConnection, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private string ConvertAmountToWordsRupees(float amountParm)
        {
            long amount = (long)amountParm, quotient;
            string amountInWords = "", amountInWordsTemp;

            quotient = amount / 100000;
            amount = amount - quotient * 100000;
            amountInWords += ConvertHundredsToWords(quotient, "Lakh");

            quotient = amount / 1000;
            amount = amount - quotient * 1000;
            amountInWordsTemp = ConvertHundredsToWords(quotient, "Thousand");
            if (amountInWords != "")
            {
                amountInWords += " ";
            }
            amountInWords += amountInWordsTemp;

            quotient = amount / 100;
            amount = amount - quotient * 100;
            amountInWordsTemp = ConvertHundredsToWords(quotient, "Hundred");
            if (amountInWords != "")
            {
                if (amountInWordsTemp != "")
                {
                    amountInWords += " ";
                }
            }
            amountInWords += amountInWordsTemp;

            amountInWordsTemp = ConvertHundredsToWords(amount, "");
            if (amountInWords != "")
            {
                if (amountInWordsTemp != "")
                {
                    amountInWords += " ";
                }
            }
            amountInWords += amountInWordsTemp;

            amount = (long)Math.Round((amountParm - Math.Truncate(amountParm)) * 100);
            if (amount > 0)
            {
                amountInWords += "And";
            }
            amountInWordsTemp = ConvertHundredsToWords(amount, "");
            if (amountInWords != "")
            {
                if (amountInWordsTemp != "")
                {
                    amountInWords += " ";
                }
            }
            amountInWords += amountInWordsTemp + " Rupees";

            return amountInWords;
        }
        private string ConvertAmountToWordsDollars(float amountParm)
        {
            long amount = (long)amountParm, quotient;
            string amountInWords = "", amountInWordsTemp;

            quotient = amount / 1000000;
            amount = amount - quotient * 1000000;
            amountInWords += ConvertHundredsToWords(quotient, "Million");

            quotient = amount / 1000;
            amount = amount - quotient * 1000;
            amountInWordsTemp = ConvertHundredsToWords(quotient, "Thousand");
            if (amountInWords != "")
            {
                amountInWords += " ";
            }
            amountInWords += amountInWordsTemp;

            quotient = amount / 100;
            amount = amount - quotient * 100;
            amountInWordsTemp = ConvertHundredsToWords(quotient, "Hundred");
            if (amountInWords != "")
            {
                if (amountInWordsTemp != "")
                {
                    amountInWords += " ";
                }
            }
            amountInWords += amountInWordsTemp;

            amountInWordsTemp = ConvertHundredsToWords(amount, "");
            if (amountInWords != "")
            {
                if (amountInWordsTemp != "")
                {
                    amountInWords += " ";
                }
            }
            amountInWords += amountInWordsTemp;

            amount = (long)Math.Round((amountParm - Math.Truncate(amountParm)) * 100);
            if (amount > 0)
            {
                amountInWords += "And";
            }
            amountInWordsTemp = ConvertHundredsToWords(amount, "");
            if (amountInWords != "")
            {
                if (amountInWordsTemp != "")
                {
                    amountInWords += " ";
                }
            }
            amountInWords += amountInWordsTemp + " Dollars";

            return amountInWords;
        }
        private string ConvertHundredsToWords(long amount, string amountUnit)
        {
            long quotient;
            string[] numberUnits = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] numberTens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string amountInWords = "";
            if (amount > 0)
            {
                if (amount > 0 && amount < 20)
                {
                    if (amountInWords == "")
                    {
                        ;
                    }
                    else
                    {
                        amountInWords += " ";
                    }
                    amountInWords += numberUnits[amount];
                }
                else
                {
                    quotient = amount / 10;
                    amountInWords += numberTens[quotient];
                    amount = amount - quotient * 10;
                    if (amount > 0)
                    {
                        amountInWords += " " + numberUnits[amount];
                    }
                }
                amountInWords += " " + amountUnit;
            }
            return amountInWords;
        }
        private Dictionary<string, string> GetCreditCardKVPs(string creditCardProcessor, long clientId)
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
        private float GetItemDiscountPercent(long itemId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int a = 1, b = 0, c = a / b;
                string sqlStmt;
                sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId IN(" + itemId + ")";
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                float itemDiscountPercent;
                if (sqlDataReader.Read())
                {
                    itemDiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString());
                }
                else
                {
                    itemDiscountPercent = 0;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return itemDiscountPercent;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private List<ItemDiscountModel> GetItemDiscountsPercent(string itemIds, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List <ItemDiscountModel> itemDiscountModels = new List <ItemDiscountModel>();
            if (itemIds != "")
            {
                try
                {
                    //int a = 1, b = 0, c = a / b;
                    string sqlStmt;
                    sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId IN(" + itemIds + ")";
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
                }
                catch (Exception exception)
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                    throw;
                }
            }
            return itemDiscountModels;
        }
        private void CalculateDiscounts(PaymentInfo1Model paymentInfoModel, long personId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int a = 1, b = 0, c = a / b;
                string itemIds = "", prefixString = "";
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItems)
                {
                    itemIds += prefixString + shoppingCartItem.ItemId;
                    prefixString = ", ";
                }
                string sqlStmt;
                if (itemIds == "")
                {
                    sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE 1 = 0";
                }
                else
                {
                    sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId IN(" + itemIds + ")";
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ShoppingCartItemModel shoppingCartItemModel;
                while (sqlDataReader.Read())
                {
                    shoppingCartItemModel = paymentInfoModel.ShoppingCartModel.ShoppingCartItems.First(x => x.ItemId == long.Parse(sqlDataReader["ItemId"].ToString()));
                    shoppingCartItemModel.ItemDiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString());
                    shoppingCartItemModel.ItemRate = shoppingCartItemModel.ItemRateBeforeDiscount.Value * (100 - shoppingCartItemModel.ItemDiscountPercent) / 100f;
                    shoppingCartItemModel.OrderAmount = shoppingCartItemModel.ItemRate * shoppingCartItemModel.OrderQty;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private void CalculateShoppingCartTotals(PaymentInfo1Model paymentInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                #region Create ShoppingCartSummaryModel
                var shoppingCartModel = paymentInfoModel.ShoppingCartModel;
                shoppingCartModel.ShoppingCartSummaryModel = new ShoppingCartSummaryModel
                {
                    AmountPaidByCreditCard = 0,
                    AmountPaidByGiftCert = 0,
                    BalanceDue = 0,
                    BalanceDueFormatted = "",
                    TotalAmountPaid = 0,
                    TotalDiscountAmount = 0,
                    TotalInvoiceAmount = 0,
                    TotalInvoiceAmountFormatted = "",
                    TotalItemsCount = 0,
                    TotalOrderAmount = 0,
                    TotalOrderAmountBeforeDiscount = 0,
                    TotalProductOrVolumetricWeight = 0,
                    TotalProductOrVolumetricWeightRounded = 0,
                    TotalVolumeValue = 0,
                    TotalWeightCalc = 0,
                };
                foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                {
                    shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItem.OrderAmount;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue += shoppingCartItem.VolumeValue;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItem.WeightCalcValue;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount += shoppingCartItem.OrderQty.Value;
                    shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItem.ProductOrVolumetricWeight;
                }
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                shoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
                shoppingCartModel.ShoppingCartSummaryItems[0].ItemShortDesc = "Total Order Amount (Total Qty : " + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ")";
                shoppingCartModel.ShoppingCartSummaryItems[0].OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
                #endregion
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItems)
                {
                    shoppingCartItem.ShoppingCartItemSummarys = new List<ShoppingCartItemModel>();
                }
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
                {
                    new ShoppingCartItemModel
                    {
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Total Order Amount (Total Qty : " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ")",
                        //ItemShortDesc = "Total Order Amount (#" + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ") Wt: " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc + " Grams",
                        OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmountAfterDiscount,
                    },
                };
                paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private void CalculateSalesTax(PaymentInfo1Model paymentInfoModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var salesTaxListModel in salesTaxListModels)
            {
                var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                if (salesTaxListModel.LineItemLevelName == "SUMMARY")
                {
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemId = null,
                            ItemRate = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                            ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
                            OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount * salesTaxListModel.SalesTaxRate / 100f,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                }
                else
                {
                    float totalSalesTaxAmount = 0, salesTaxAmount;
                    foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItems)
                    {
                        var itemSpecValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemSpecModels.ToList().First(x => x.ItemSpecMasterModel.SpecName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemSpecValue;
                        salesTaxAmount = float.Parse(itemSpecValue) * shoppingCartItem.OrderAmount.Value / 100f;
                        totalSalesTaxAmount += salesTaxAmount;
                        shoppingCartItem.ShoppingCartItemSummarys.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                ItemRate = float.Parse(itemSpecValue),
                                OrderAmount = salesTaxAmount,
                            }
                        );
                    }
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemId = null,
                            ItemRate = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                            ItemShortDesc = salesTaxCaptionId.CodeDataDesc0,
                            OrderAmount = totalSalesTaxAmount,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                }
            }
        }
        private List<SalesTaxListModel> GetSalesTaxListModels(DemogInfoAddressModel demogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        private DeliveryChargeModel GetDeliveryChargeModel(PaymentInfo1Model paymentInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        private void CalculateDeliveryCharges(PaymentInfo1Model paymentInfoModel, CorpAcctModel corpAcctModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            if (corpAcctModel.ShippingAndHandlingCharges == YesNoEnum.Yes)
            {

                DeliveryChargeModel deliveryChargeModel = GetDeliveryChargeModel(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId); ;
                if (deliveryChargeModel != null)
                {
                    var shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
                    var shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded;
                    var fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
                    var shoppingCartItemSummaryModelsFromCount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Count;
                    List<ShoppingCartItemModel> shoppingCartItemModelTemps = new List<ShoppingCartItemModel>();
                    //ShoppingCartItemModel shoppingCartItemModelTemp;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemId = null,
                            ItemRate = shippingAndHandlingChargesRate,
                            ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
                            OrderAmount = shippingAndHandlingChargesAmount + fuelCharges,
                            OrderComments = null,
                            OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                            OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                        }
                    );
                    //shoppingCartItemModelTemps.Add
                    //(
                    //    new ShoppingCartItemModel
                    //    {
                    //        ItemId = null,
                    //        ItemRate = shippingAndHandlingChargesRate,
                    //        ItemShortDesc = "Discount - Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
                    //        OrderAmount = -1 * (shippingAndHandlingChargesAmount + fuelCharges),
                    //        OrderComments = null,
                    //        OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                    //        OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                    //    }
                    //);
                    foreach (var salesTaxListModel in salesTaxListModels)
                    {
                        var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemId = null,
                                ItemRate = shippingAndHandlingChargesRate,
                                ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
                                OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
                                OrderComments = null,
                                OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                            }
                        );
                        //shoppingCartItemModelTemps.Add
                        //(
                        //    new ShoppingCartItemModel
                        //    {
                        //        ItemId = null,
                        //        ItemRate = shippingAndHandlingChargesRate,
                        //        ItemShortDesc = "Discount - " + salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
                        //        OrderAmount = -1 * (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
                        //        OrderComments = null,
                        //        OrderQty = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                        //        OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                        //    }
                        //);
                    }
                }
                //if (!corpAcctModel.ShippingAndHandlingCharges)
                //{
                //    foreach (var shoppingCartItemModelTemp in shoppingCartItemModelTemps)
                //    {
                //        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add(shoppingCartItemModelTemp);
                //    }
                //    //var shoppingCartItemSummaryModelsToCount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Count;
                //    //paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.InsertRange(shoppingCartItemSummaryModelsToCount, paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.GetRange(shoppingCartItemSummaryModelsFromCount, shoppingCartItemSummaryModelsToCount - shoppingCartItemSummaryModelsFromCount));
                //    //for (int i = shoppingCartItemSummaryModelsToCount; i < paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Count; i++)
                //    //{
                //    //    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].ItemShortDesc = "Discount - " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].ItemShortDesc;
                //    //    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].OrderAmount = -1 * paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].OrderAmount;
                //    //}
                //}
            }
        }
        private void CalculateShoppingCartSummaryTotals(PaymentInfo1Model paymentInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            float totalInvoiceAmount = 0, totalAmountPaid;
            foreach (var shoppingCartSummaryItem in paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems)
            {
                totalInvoiceAmount += shoppingCartSummaryItem.OrderAmount.Value;
            }
            totalInvoiceAmount = (float)Math.Round(totalInvoiceAmount, 2, MidpointRounding.AwayFromZero);
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemId = null,
                    ItemRate = totalInvoiceAmount,
                    ItemShortDesc = "Total Invoice Amount",
                    OrderAmount = totalInvoiceAmount,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
                }
            );
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Amount Paid",
                    OrderAmount = 0,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCreditCard,
                }
            );
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Amount Paid - Gift Cert",
                    OrderAmount = paymentInfoModel.GiftCertPaymentModel.GiftCertPaymentAmount.Value,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByGiftCert,
                }
            );
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Amount Paid - Coupon",
                    OrderAmount = paymentInfoModel.CouponPaymentModel.CouponPaymentAmount.Value,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCoupon,
                }
            );
            totalAmountPaid = paymentInfoModel.GiftCertPaymentModel.GiftCertPaymentAmount.Value + paymentInfoModel.CouponPaymentModel.CouponPaymentAmount.Value;
            totalAmountPaid = (float)Math.Round(totalAmountPaid, 2, MidpointRounding.AwayFromZero);
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Total Amount Paid",
                    OrderAmount = totalAmountPaid,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
                }
            );
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemId = null,
                    ItemRate = totalInvoiceAmount,
                    ItemShortDesc = "Balance Due",
                    OrderAmount = totalInvoiceAmount,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                }
            );
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = totalInvoiceAmount;
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = totalInvoiceAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = totalInvoiceAmount;
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmountFormatted = totalInvoiceAmount.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        }
        private void BuildCreditCardDataModel(PaymentInfo1Model paymentInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
            CreditCardDataModel creditCardDataModel = new CreditCardDataModel
            {
                CreditCardAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString("0.00"),
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
            paymentInfoModel.CreditCardDataModel = creditCardDataModel;
            return;
        }
        private string CreateInvoice(PaymentInfo1Model paymentInfoModel, Controller controller, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ArchLibBL archLibBL = new ArchLibBL();
                string orderFileName = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString();
                if (paymentInfoModel.OrderSummaryModel.InvoiceTypeId == 100)
                {
                }
                else
                {
                    orderFileName += "_OrderForm";
                    paymentInfoModel.OrderSummaryModel.InvoiceType = "Order Form";
                }
                paymentInfoModel.OrderSummaryModel.DownloadFileNamePdf = orderFileName + ".pdf";
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
                paymentInfoModel.OrderSummaryModel.DownloadFullFileNamePdf = pDFFullFileName;
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
                archLibBL.SendEmail(toEmailAddresss, emailSubjectText, emailBodyHtml + signatureHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                string htmlString = archLibBL.ViewToHtmlString(controller, "_OrderInvoice", paymentInfoModel);
                if (paymentInfoModel.OrderSummaryModel.InvoiceTypeId == 100)
                {
                }
                else
                {
                    paymentInfoModel.OrderSummaryModel.InvoiceTypeId = 100;
                    paymentInfoModel.OrderSummaryModel.InvoiceType = "Tax Invoice";
                    emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
                    //signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", paymentInfoModel);
                    orderFileName = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value.ToString();
                    streamWriter = new StreamWriter(invoiceDirectoryName + orderFileName + ".html");
                    streamWriter.Write(emailBodyHtml);
                    streamWriter.Write(Environment.NewLine);
                    streamWriter.Close();
                    pDFFullFileName = invoiceDirectoryName + orderFileName + ".pdf";
                    pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, pDFFullFileName);
                }
                return htmlString;
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
        private void CreateOrder(PaymentInfo1Model paymentInfoModel, string razorpay_payment_id, string razorpay_order_id, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                OrderHeader orderHeader = CreateOrderHeader(paymentInfoModel, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.AddOrderHeader(orderHeader, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderDetail orderDetail;
                OrderDetailItemBundle orderDetailItemBundle;
                int seqNum = 0, seqNumBundle = 0;
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItems)
                {
                    orderDetail = CreateOrderDetail(orderHeader.OrderHeaderId, ++seqNum, shoppingCartItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddOrderDetail(orderDetail, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (shoppingCartItem.ItemBundleModel != null)
                    {
                        foreach (var itemBundleItemModel in shoppingCartItem.ItemBundleModel.ItemBundleItemModels)
                        {
                            orderDetailItemBundle = CreateOrderDetailItemBundleItem(orderDetail.OrderDetailId, shoppingCartItem.OrderQty.Value, ++seqNumBundle, shoppingCartItem.ItemBundleModel, itemBundleItemModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                            ApplicationDataContext.AddOrderDetailItemBundle(orderDetailItemBundle, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                    }
                }
                foreach (var shoppingCartSummaryItem in paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems)
                {
                    orderDetail = CreateOrderDetail(orderHeader.OrderHeaderId, ++seqNum, shoppingCartSummaryItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddOrderDetail(orderDetail, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                ArchLibDataContext.CreateDemogInfoAddress(paymentInfoModel.DeliveryAddressModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.DeliveryDataModel.DeliveryAddressModel = paymentInfoModel.DeliveryAddressModel;
                paymentInfoModel.DeliveryDataModel.OrderHeaderId = orderHeader.OrderHeaderId;
                paymentInfoModel.DeliveryDataModel.DeliveryMethodId = (long?)paymentInfoModel.DeliveryMethodModel.DeliveryMethodId;
                paymentInfoModel.DeliveryDataModel.PickupLocationId = paymentInfoModel.DeliveryMethodModel.PickupLocationId;
                paymentInfoModel.OrderSummaryModel.OrderHeaderId = orderHeader.OrderHeaderId;
                paymentInfoModel.OrderSummaryModel.OrderDateTime = orderHeader.OrderDateTime;
                ApplicationDataContext.AddOrderDelivery(paymentInfoModel.DeliveryDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.UpdPerson(paymentInfoModel.OrderSummaryModel.PersonId.Value, paymentInfoModel.OrderSummaryModel.FirstName, paymentInfoModel.OrderSummaryModel.LastName, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.PaymentDataModel = new PaymentData1Model
                {
                    CouponId = 0,
                    CreditCardDataId = paymentInfoModel.CreditCardDataModel.CreditCardDataId,
                    GiftCertId = 0,
                    OrderHeaderId = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value,
                    PaymentModeId = (long)paymentInfoModel.PaymentModeModel.PaymentModeId,
                    PaymentReferenceNumber = razorpay_payment_id == "" ? "" : "Ref:&nbsp;" + razorpay_payment_id + "<br />Num:&nbsp;" + razorpay_order_id,
                };
                paymentInfoModel.OrderSummaryModel.UserFullName = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim();
                ApplicationDataContext.AddOrderPayment(paymentInfoModel.PaymentDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                CorpAcctModel corpAcctModel;
                paymentInfoModel.OrderSummaryModel.InvoiceTypeId = 100;
                //paymentInfoModel.OrderSummaryModel.InvoiceType = "Tax Invoice";
                if (sessionObjectModel.PersonId != createForSessionObject.PersonId && paymentInfoModel.PaymentModeModel.PaymentModeId == PaymentModeEnum.CreditSale)
                {
                    corpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
                    if (corpAcctModel.OrderApprovalRequired == YesNoEnum.Yes)
                    {
                        paymentInfoModel.OrderSummaryModel.InvoiceTypeId = 200;
                        //paymentInfoModel.OrderSummaryModel.InvoiceType = "Order Form";
                        //Add or Upd Order Approval
                        string approverComments = $"This order is approved by {paymentInfoModel.OrderSummaryModel.FirstName} {paymentInfoModel.OrderSummaryModel.LastName} [{paymentInfoModel.OrderSummaryModel.EmailAddress}] on {paymentInfoModel.OrderSummaryModel.CreatedByFirstName} {paymentInfoModel.OrderSummaryModel.CreatedByLastName} [{paymentInfoModel.OrderSummaryModel.CreatedByEmailAddress}] user's device";
                        string approvalStatusNameDesc = "COMPLETED";
                        paymentInfoModel.OrderApprovalModel = CreateOrderApprovalModel(approvalStatusNameDesc, approverComments, paymentInfoModel.CompleteOrderModel.ApproverSignatureTextId, paymentInfoModel.CompleteOrderModel.ApproverSignatureTextValue, paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.AddOrderApproval(paymentInfoModel.OrderApprovalModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        private OrderApprovalModel CreateOrderApprovalModel(string approvalStatusNameDesc, string approverComments, long? approverSignatureTextId, string approverSignatureTextValue, long orderHeaderId, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var betterRandom = new BetterRandom();
            OrderApprovalModel orderApporovalModel = new OrderApprovalModel
            {
                ClientId = clientId,
                ApprovalOTPCode = null,//(betterRandom.NextInt() % 1000000).ToString("000000"),
                ApprovalOTPCompletedDateTime = null,
                ApprovalOTPCreatedDateTime = null,//currentDateTime,
                ApprovalOTPExpiryDateTime = null,//DateTime.Parse(currentDateTime).AddMinutes(11).ToString("yyyy-MM-dd HH:mm:ss"),
                ApprovalOTPExpiryDuration = null,//11,
                ApprovalOTPSendTypeId = null,//OTPSendTypeEnum.Email,
                ApprovalRequestedByPersonId = sessionObjectModel.PersonId,
                ApprovalRequestedByFirstName = sessionObjectModel.FirstName,
                ApprovalRequestedByLastName = sessionObjectModel.LastName,
                ApprovalRequestedDateTime = currentDateTime,
                ApprovalRequestedForPersonId = createForSessionObject.PersonId,
                ApprovalRequestedForFirstName = createForSessionObject.FirstName,
                ApprovalRequestedForLastName = createForSessionObject.LastName,
                ApprovalStatusNameDesc = approvalStatusNameDesc,
                ApprovedByPersonId = sessionObjectModel.PersonId,
                ApprovedDateTime = currentDateTime,
                ApproverComments = approverComments,
                ApproverConsent = true,
                ApproverInitialsTextId = null,
                ApproverInitialsTextValue = null,
                ApproverSignatureTextId = approverSignatureTextId,
                ApproverSignatureTextValue = approverSignatureTextValue,
                Comments = null,
                OrderHeaderId = orderHeaderId,
            };
            return orderApporovalModel;
        }
        private OrderHeader CreateOrderHeader(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var orderHeader = new OrderHeader
            {
                ClientId = clientId,
                CreatedForPersonId = createForSessionObject.PersonId,
                OrderDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                OrderStatusId = (long)OrderStatusEnum.Open,
                PersonId = sessionObjectModel.PersonId,
                SaveThisAddress = paymentInfoModel.OrderSummaryModel.SaveThisAddress,
            };
            return orderHeader;
        }
        private OrderDetail CreateOrderDetail(long orderHeaderId, int seqNum, ShoppingCartItemModel shoppingCartItemModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                OrderHeaderId = orderHeaderId,
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
            return orderDetail;
        }
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
    }
}
