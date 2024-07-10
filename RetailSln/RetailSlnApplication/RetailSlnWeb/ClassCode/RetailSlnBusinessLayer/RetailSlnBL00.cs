using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
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
using static System.Collections.Specialized.BitVector32;

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
                                ItemDesc = null,
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
                    WeightUnitEnum weightUnitId;
                    dimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Height").ItemAttribUnitValue);
                    weightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Weight").ItemAttribUnitValue);
                    heightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Height").ItemAttribValue);
                    lengthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Length").ItemAttribValue);
                    weightCalcValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "CalcProductWeight").ItemAttribValue);
                    weightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Weight").ItemAttribValue);
                    widthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Width").ItemAttribValue);
                    if (itemModel != null)
                    {
                        itemRate = itemModel.ItemRate.Value;
                        shoppingCartModel.ShoppingCartItems.Add
                        (
                            new ShoppingCartItemModel
                            {
                                DimensionUnitId = dimensionUnitId,
                                HeightValue = heightValue,
                                ItemDesc = itemModel.ItemDesc,
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
                            ItemDesc = null,
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
                                DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductHeight").ItemAttribUnitValue),
                                HeightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductHeight").ItemAttribValue),
                                HSNCode = itemModel.ItemAttribModelsForDisplay["HSNCode"].ItemAttribValueForDisplay,
                                ItemDesc = itemModel.ItemDesc,
                                ItemDiscountPercent = null,
                                ItemId = itemModel.ItemId,
                                ItemRate = itemModel.ItemRate,
                                ItemRateBeforeDiscount = itemModel.ItemRate,
                                ItemShortDesc = itemModel.ItemShortDesc,
                                LengthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductLength").ItemAttribValue),
                                OrderAmount = orderQty * itemModel.ItemRate,
                                OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
                                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                                OrderQty = orderQty,
                                ProductCode = itemModel.ItemAttribModelsForDisplay["ProductCode"].ItemAttribValueForDisplay,
                                WeightCalcValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "CalcProductWeight").ItemAttribValue),
                                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWeight").ItemAttribUnitValue),
                                WeightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWeight").ItemAttribValue),
                                WidthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWidth").ItemAttribValue),
                                ProductOrVolumetricWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductOrVolumetricWeight").ItemAttribValue),
                                ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductOrVolumetricWeight").ItemAttribUnitValue),
                                ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                            }
                        );
                    }
                    else
                    {
                        shoppingCartItemModel.OrderQty += orderQty;
                    }
                    shoppingCartItemModel.OrderComments = orderComments;
                    shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightCalcValue = orderQty * shoppingCartItemModel.WeightCalcValue;
                    shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
                    shoppingCartItemModel.ProductOrVolumetricWeight = orderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
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
                    CreateOrder(paymentInfoModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                    TotalAmountPaid = 0,
                    TotalDiscountAmount = 0,
                    TotalInvoiceAmount = 0,
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
                        ItemDesc = null,
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
                            ItemDesc = null,
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
                        var itemAttribValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemAttribModels.ToList().First(x => x.ItemAttribMasterModel.AttribName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemAttribValue;
                        salesTaxAmount = float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount.Value / 100f;
                        totalSalesTaxAmount += salesTaxAmount;
                        shoppingCartItem.ShoppingCartItemSummarys.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                ItemRate = float.Parse(itemAttribValue),
                                OrderAmount = salesTaxAmount,
                            }
                        );
                    }
                    paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemDesc = null,
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
                        ItemDesc = null,
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
                            ItemDesc = null,
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
                    ItemDesc = null,
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
                    ItemDesc = null,
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
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Amount Paid - Coupon",
                    OrderAmount = paymentInfoModel.CouponPaymentModel.CouponPaymentAmount.Value,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCoupon,
                }
            );
            totalAmountPaid = totalInvoiceAmount + paymentInfoModel.GiftCertPaymentModel.GiftCertPaymentAmount.Value + paymentInfoModel.CouponPaymentModel.CouponPaymentAmount.Value;
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
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
                    ItemDesc = null,
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
            paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = totalInvoiceAmount;
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
                foreach (var shoppingCartItem in paymentInfoModel.ShoppingCartModel.ShoppingCartItems)
                {
                    orderDetail = CreateOrderDetail(orderHeader.OrderHeaderId, shoppingCartItem, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddOrderDetail(orderDetail, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                TelephoneCountryId = 91,
                TelephoneNumber = long.Parse(sessionObjectModel.PhoneNumber),
            };
            return orderHeader;
        }
        private OrderDetail CreateOrderDetail(long orderHeaderId, ShoppingCartItemModel shoppingCartItemModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                ClientId= clientId,
                DimensionUnitId = (long)DimensionUnitEnum.Centimeter,
                HeightValue = shoppingCartItemModel.HeightValue.Value,
                HSNCode = shoppingCartItemModel.HSNCode,
                ItemDesc = shoppingCartItemModel.ItemDesc,

            };
            return orderDetail;
        }
    }
}
