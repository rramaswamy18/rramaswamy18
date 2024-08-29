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
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: AddToCart
        public void AddToCart(ref PaymentInfo1Model paymentInfo1Model, long itemId, long orderQty, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (paymentInfo1Model == null)
                {
                    paymentInfo1Model = new PaymentInfo1Model();
                }
                ShoppingCartModel shoppingCartModel;
                ShoppingCartItemModel shoppingCartItemModel;
                shoppingCartModel = paymentInfo1Model.ShoppingCartModel;
                if (shoppingCartModel == null)
                {
                    shoppingCartModel = new ShoppingCartModel
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
                }
                shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
                if (shoppingCartItemModel != null)
                {
                    shoppingCartItemModel.OrderQty += orderQty;
                    shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRateBeforeDiscount;
                    shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightCalcValue = orderQty * shoppingCartItemModel.WeightCalcValue;
                    shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
                    UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    return;
                }
                else
                {
                    ItemModel itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
                    float heightValue, lengthValue, weightCalcValue, weightValue, widthValue, itemRate;
                    DimensionUnitEnum dimensionUnitId;
                    WeightUnitEnum weightUnitId, weightCalcUnitId;
                    dimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Height").ItemSpecUnitValue);
                    weightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Weight").ItemSpecUnitValue);
                    heightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Height").ItemSpecValue);
                    lengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Length").ItemSpecValue);
                    weightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Weight").ItemSpecUnitValue);
                    weightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue);
                    weightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Weight").ItemSpecValue);
                    widthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "Width").ItemSpecValue);
                    if (itemModel != null)
                    {
                        itemRate = itemModel.ItemRate.Value;
                        shoppingCartModel.ShoppingCartItems.Add
                        (
                            new ShoppingCartItemModel
                            {
                                DimensionUnitId = dimensionUnitId,
                                HeightValue = heightValue,
                                ItemDiscountPercent = null,
                                ItemId = itemModel.ItemId,
                                ItemRate = itemRate,
                                ItemRateBeforeDiscount = itemRate,
                                ItemShortDesc = itemModel.ItemShortDesc,
                                LengthValue = lengthValue,
                                OrderAmount = orderQty * itemRate,
                                OrderAmountBeforeDiscount = orderQty * itemRate,
                                OrderComments = "",
                                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                                OrderQty = orderQty,
                                VolumeValue = lengthValue * widthValue * heightValue * orderQty,
                                WeightCalcUnitId = weightCalcUnitId,
                                WeightCalcValue = weightCalcValue,
                                WeightUnitId = weightUnitId,
                                WeightValue = weightValue * orderQty,
                                WidthValue = widthValue,
                            }
                        );
                        UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        return;
                    }
                    else
                    {
                        throw new Exception("Error while adding item to shopping cart itemid=" + itemId + " orderQty=" + orderQty);
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        // POST: AddToCart
        public void AddToCart(ref PaymentInfo1Model paymentInfo1Model, List<ShoppingCartItemModel> shoppingCartItemModels, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                paymentInfo1Model = paymentInfo1Model ?? new PaymentInfo1Model();
                long itemId, orderQty;
                string orderComments;
                ItemModel itemModel;
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
                foreach (var shoppingCartItemModelTemp in shoppingCartItemModels)
                {
                    itemId = shoppingCartItemModelTemp.ItemId.Value;
                    orderQty = shoppingCartItemModelTemp.OrderQty.Value;
                    orderComments = shoppingCartItemModelTemp.OrderComments;
                    shoppingCartItemModel = paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
                    if (shoppingCartItemModel == null)
                    {
                        itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
                        paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.Add
                        (
                            shoppingCartItemModel = new ShoppingCartItemModel
                            {
                                DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue),
                                HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue),
                                HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
                                ItemDiscountPercent = null,
                                ItemId = itemModel.ItemId,
                                ItemRate = itemModel.ItemRate,
                                ItemRateBeforeDiscount = itemModel.ItemRate,
                                ItemShortDesc = itemModel.ItemShortDesc,
                                LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue),
                                OrderAmount = orderQty * itemModel.ItemRate,
                                OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
                                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                                OrderQty = orderQty,
                                ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
                                WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                                WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "CalcProductWeight").ItemSpecValue),
                                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue),
                                WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue),
                                WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue),
                                ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue),
                                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
                                ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                            }
                        );
                    }
                    else
                    {
                        shoppingCartItemModel.OrderQty += orderQty;
                    }
                    shoppingCartItemModel.OrderComments = orderComments;
                    shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.VolumeValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightCalcValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightCalcValue;
                    shoppingCartItemModel.WeightValue = shoppingCartItemModel.OrderQty * shoppingCartItemModel.WeightValue;
                    shoppingCartItemModel.ProductOrVolumetricWeight = shoppingCartItemModel.OrderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
                }
                UpdateShoppingCart(paymentInfo1Model.ShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        // POST: AddToCart
        public void AddToCart2(ref PaymentInfo1Model paymentInfo1Model, ShoppingCartBundleModel shoppingCartBundleModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            for (int i = 0; i < shoppingCartBundleModel.ShoppingCartItemBundleModels.Count; i++)
            {

            }
            try
            {
                if (paymentInfo1Model == null)
                {
                    paymentInfo1Model = new PaymentInfo1Model();
                }
                //ShoppingCartModel shoppingCartModel;
                //ShoppingCartItemModel shoppingCartItemModel;
                //shoppingCartModel = paymentInfo1Model.ShoppingCartModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
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
                    ContactUsModel = new ContactUsModel(),
                    LoginUserProfGuestModel = new LoginUserProfGuestModel
                    {
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
                    RegisterUserProfModel = new RegisterUserProfModel
                    {
                        RegisterTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry")),
                    },
                    ResetPasswordModel = new ResetPasswordModel(),
                    PaymentInfoModel = paymentInfoModel,
                };
                if (checkoutModel.PaymentInfoModel != null)
                {
                    List<string> numberSessions = new List<string>
                    {
                        "CaptchaNumberCheckoutGuest0",
                        "CaptchaNumberCheckoutGuest1",
                        "CaptchaNumberLogin0",
                        "CaptchaNumberLogin1",
                        "CaptchaNumberRegister0",
                        "CaptchaNumberRegister1",
                        "CaptchaNumberResetPassword0",
                        "CaptchaNumberResetPassword1",
                        "CaptchaNumberContactUs0",
                        "CaptchaNumberContactUs1",
                    };
                    archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, numberSessions);
                    checkoutModel.ContactUsModel.CaptchaAnswerContactUs = null;
                    checkoutModel.ContactUsModel.CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString();
                    checkoutModel.ContactUsModel.CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString();
                    checkoutModel.LoginUserProfGuestModel.CaptchaAnswerLoginUserProfGuest = null;
                    checkoutModel.LoginUserProfGuestModel.CaptchaNumberLoginUserProfGuest0 = httpSessionStateBase["CaptchaNumberCheckoutGuest0"].ToString();
                    checkoutModel.LoginUserProfGuestModel.CaptchaNumberLoginUserProfGuest1 = httpSessionStateBase["CaptchaNumberCheckoutGuest1"].ToString();
                    checkoutModel.LoginUserProfModel.CaptchaAnswerLogin = null;
                    checkoutModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                    checkoutModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                    checkoutModel.RegisterUserProfModel.CaptchaAnswerRegister = null;
                    checkoutModel.RegisterUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
                    checkoutModel.RegisterUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
                    checkoutModel.ResetPasswordModel.CaptchaAnswerResetPassword = null;
                    checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
                    checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
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
        public void DeliveryInfo(ref PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                            CorpAcctModel = ((ApplSessionObjectModel)sessionObjectModel.ApplSessionObjectModel).CorpAcctModel,
                            EmailAddress = sessionObjectModel.EmailAddress,
                            FirstName = sessionObjectModel.FirstName,
                            LastName = sessionObjectModel.LastName,
                            PersonId = sessionObjectModel.PersonId,
                            TelephoneCode = null,
                            TelephoneCountryId = null,
                            TelephoneNumber = sessionObjectModel.PhoneNumber,
                        };
                        paymentInfoModel.CouponPaymentModel = new CouponPaymentModel
                        {
                            CouponNumber = "",
                            CouponPaymentAmount = 0,
                        };
                        paymentInfoModel.DeliveryAddressModel = new DemogInfoAddressModel
                        {
                            BuildingTypeId = BuildingTypeEnum._,
                            BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
                            DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
                            DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId],
                        };
                        paymentInfoModel.DeliveryDataModel = new DeliveryDataModel
                        {
                            AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                        };
                        paymentInfoModel.GiftCertPaymentModel = new GiftCertPaymentModel
                        {
                            GiftCertPaymentAmount = 0,
                        };
                        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseMessages = new List<string>(),
                            ResponseTypeId = ResponseTypeEnum.Success,
                        };
                        BuildDeliveryInfoLookup(paymentInfoModel, sessionObjectModel, apiFlag, webFlag, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public void DeliveryInfo(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                CorpAcctModel corpAcctModel = (((ApplSessionObjectModel)sessionObjectModel.ApplSessionObjectModel).CorpAcctModel);
                CalculateDiscounts(paymentInfoModel, sessionObjectModel.PersonId, corpAcctModel.CorpAcctId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        // POST: RemoveFromCart
        public void RemoveFromCart(PaymentInfo1Model paymentInfoModel, int index, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (index > -1 && index < shoppingCartModel.ShoppingCartItems.Count)
                    {
                        shoppingCartModel.ShoppingCartItems.RemoveAt(index);
                        UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public string PaymentInfo3(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount - paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                    var shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    shoppingCartSummaryItem.OrderComments = "Payment Id : " + razorpay_payment_id + " - Order Id : " + razorpay_order_id;
                    shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid;
                    shoppingCartSummaryItem = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
                    shoppingCartSummaryItem.OrderAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue;
                    CreateOrder(paymentInfoModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceDataSubject", paymentInfoModel);
                    string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderInvoiceData", paymentInfoModel);
                    string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", paymentInfoModel);
                    emailBodyHtml += signatureHtml;
                    PDFUtility pDFUtility = new PDFUtility();
                    string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
                    pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, emailDirectoryName + paymentInfoModel.OrderSummaryModel.OrderHeaderId + ".pdf");
                    List<string> emailAttachmentFileNames = new List<string>
                    {
                        emailDirectoryName + paymentInfoModel.OrderSummaryModel.OrderHeaderId + ".pdf",
                    };
                    archLibBL.SendEmail(paymentInfoModel.OrderSummaryModel.EmailAddress, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                    return null;
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
        //Private Methods
        private void UpdateShoppingCart(ShoppingCartModel shoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                shoppingCartModel.ShoppingCartSummaryItems[0].ItemShortDesc = "Total Order Amount (#" + shoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ")";
                shoppingCartModel.ShoppingCartSummaryItems[0].OrderAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private string ConvertAmountToWords(long amount)
        {
            long quotient;
            string[] numberUnits = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] numberTens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string amountInWords;

            amountInWords = "";

            quotient = amount / 1000;
            amount = amount - quotient * 1000;

            if (quotient > 0)
            {
                amountInWords += numberUnits[quotient] + " Thousand";
            }

            quotient = amount / 100;
            amount = amount - quotient * 100;

            if (quotient > 0)
            {
                amountInWords += numberUnits[quotient] + " Hundred";
            }

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
                amount = amount - quotient * 10;
                if (quotient > 0)
                {
                    if (amountInWords == "")
                    {
                        ;
                    }
                    else
                    {
                        amountInWords += " ";
                    }
                    amountInWords += numberTens[quotient];
                }
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
            }

            amountInWords += " and 00/100 Dollars.........";

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
                string sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId IN(" + itemIds + ")";
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
                        ItemShortDesc = "Total Order Amount (#" + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ") Wt: " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc + " Grams",
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
            DeliveryChargeModel deliveryChargeModel = GetDeliveryChargeModel(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId); ;
            if (deliveryChargeModel != null)
            {
                var shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
                var shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded;
                var fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
                var shoppingCartItemSummaryModelsFromCount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Count;
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
                }
                if (!corpAcctModel.ShippingAndHandlingCharges)
                {
                    var shoppingCartItemSummaryModelsToCount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Count;
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.AddRange(paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.GetRange(shoppingCartItemSummaryModelsFromCount, shoppingCartItemSummaryModelsToCount - shoppingCartItemSummaryModelsFromCount));
                    for (int i = shoppingCartItemSummaryModelsToCount; i < paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Count; i++)
                    {
                        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].ItemShortDesc = "Discount - " + paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].ItemShortDesc;
                        paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].OrderAmount = -1 * paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems[i].OrderAmount;
                    }
                }
            }
        }
        private void CalculateShoppingCartSummaryTotals(PaymentInfo1Model paymentInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            float totalInvoiceAmount = 0, totalAmountPaid;
            foreach (var shoppingCartSummaryItem in paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems)
            {
                totalInvoiceAmount += shoppingCartSummaryItem.OrderAmount.Value;
            }
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
        private void CreateOrder(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                OrderHeader orderHeader = CreateOrderHeader(paymentInfoModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.AddOrderHeader(orderHeader, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderDetail orderDetail;
                int seqNum = 0;
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItems)
                {
                    orderDetail = CreateOrderDetail(orderHeader.OrderHeaderId, ++seqNum, shoppingCartItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddOrderDetail(orderDetail, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                foreach (var shoppingCartSummaryItem in paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems)
                {
                    orderDetail = CreateOrderDetail(orderHeader.OrderHeaderId, ++seqNum, shoppingCartSummaryItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddOrderDetail(orderDetail, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                ArchLibDataContext.CreateDemogInfoAddress(paymentInfoModel.DeliveryAddressModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.DeliveryDataModel.DeliveryAddressModel = paymentInfoModel.DeliveryAddressModel;
                paymentInfoModel.DeliveryDataModel.OrderHeaderId = orderHeader.OrderHeaderId;
                paymentInfoModel.OrderSummaryModel.OrderHeaderId = orderHeader.OrderHeaderId;
                ApplicationDataContext.AddDeliveryInfo(paymentInfoModel.DeliveryDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.UpdPerson(paymentInfoModel.OrderSummaryModel.PersonId.Value, paymentInfoModel.OrderSummaryModel.FirstName, paymentInfoModel.OrderSummaryModel.LastName, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.PaymentDataModel = new PaymentData1Model
                {
                    CouponId = 0,
                    CreditCardDataId = paymentInfoModel.CreditCardDataModel.CreditCardDataId,
                    GiftCertId = 0,
                    OrderHeaderId = paymentInfoModel.OrderSummaryModel.OrderHeaderId.Value,
                };
                paymentInfoModel.OrderSummaryModel.UserFullName = (paymentInfoModel.OrderSummaryModel.FirstName + " " + paymentInfoModel.OrderSummaryModel.LastName).Trim();
                ApplicationDataContext.AddOrderPayment(paymentInfoModel.PaymentDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        private OrderHeader CreateOrderHeader(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var orderHeader = new OrderHeader
            {
                ClientId = clientId,
                EmailAddress = sessionObjectModel.EmailAddress,
                OrderCreatedByPersonId = sessionObjectModel.PersonId,
                OrderDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                OrderStatusId = (long)OrderStatusEnum.Open,
                PersonId = sessionObjectModel.PersonId,
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
                ItemDiscountAmount = shoppingCartItemModel.ItemDiscountAmount == null ? 0 :shoppingCartItemModel.ItemDiscountAmount.Value,
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
    }
}
