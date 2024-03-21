using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryPDFLibrary;
using ArchitectureLibraryUtility;
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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        //GET AddToCart
        public ShoppingCartModel AddToCart(long itemId, long orderQty, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                ShoppingCartItemModel shoppingCartItemModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
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
                        ShoppingCartTotalAmount = 0,
                    };
                }
                shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
                if (shoppingCartItemModel != null)
                {
                    shoppingCartItemModel.OrderQty += orderQty;
                    shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRateBeforeDiscount;
                    shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
                    UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    return shoppingCartModel;
                }
                else
                {
                    ItemModel itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
                    float heightValue, lengthValue, weightValue, widthValue, itemRate;
                    DimensionUnitEnum dimensionUnitId;
                    WeightUnitEnum weightUnitId;
                    dimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Height").ItemAttribUnitValue);
                    weightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Weight").ItemAttribUnitValue);
                    heightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Height").ItemAttribValue);
                    lengthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Length").ItemAttribValue);
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
                                WeightUnitId = weightUnitId,
                                WeightValue = weightValue * orderQty,
                                WidthValue = widthValue,
                            }
                        );
                        UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                        httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        return shoppingCartModel;
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
        //POST AddToCart
        public ShoppingCartModel AddToCart(List<ShoppingCartItemModel> shoppingCartItemModels, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long itemId, orderQty;
                string orderComments;
                ItemModel itemModel;
                ShoppingCartModel shoppingCartModel;
                ShoppingCartItemModel shoppingCartItemModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                if (shoppingCartModel == null)
                {
                    shoppingCartModel = new ShoppingCartModel
                    {
                        //Checkout = true,
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
                        ShoppingCartTotalAmount = 0,
                    };
                }
                foreach (var shoppingCartItemModelTemp in shoppingCartItemModels)
                {
                    itemId = shoppingCartItemModelTemp.ItemId.Value;
                    orderQty = shoppingCartItemModelTemp.OrderQty.Value;
                    orderComments = shoppingCartItemModelTemp.OrderComments;
                    shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
                    if (shoppingCartItemModel == null)
                    {
                        itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
                        shoppingCartModel.ShoppingCartItems.Add
                        (
                            shoppingCartItemModel = new ShoppingCartItemModel
                            {
                                DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Height").ItemAttribUnitValue),
                                HeightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Height").ItemAttribValue),
                                HSNCode = itemModel.ItemAttributesForDisplay["HSNCode"],
                                ItemDesc = itemModel.ItemDesc,
                                ItemDiscountPercent = null,
                                ItemId = itemModel.ItemId,
                                ItemRate = itemModel.ItemRate,
                                ItemRateBeforeDiscount = itemModel.ItemRate,
                                ItemShortDesc = itemModel.ItemShortDesc,
                                LengthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Length").ItemAttribValue),
                                OrderAmount = orderQty * itemModel.ItemRate,
                                OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
                                OrderDetailTypeId = OrderDetailTypeEnum.Item,
                                OrderQty = orderQty,
                                ProductCode = itemModel.ItemAttributesForDisplay["ProductCode"],
                                WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Weight").ItemAttribUnitValue),
                                WeightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Weight").ItemAttribValue),
                                WidthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "Width").ItemAttribValue),
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
                    shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
                }
                UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return shoppingCartModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //GET CategoryListView
        public CategoryListModel CategoryListView(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                CategoryListModel categoryListModel = new CategoryListModel
                {
                    CategoryModels = ApplicationDataContext.GetCategorys(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                //Remove the first two entries - the parent for categories and Featured Items
                categoryListModel.CategoryModels.RemoveAt(0);
                categoryListModel.CategoryModels.RemoveAt(0);
                return categoryListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //GET Checkout
        public CheckoutModel Checkout(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                CheckoutModel checkoutModel = new CheckoutModel
                {
                    ContactUsModel = new ContactUsModel(),
                    CheckoutGuestModel = new CheckoutGuestModel
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
                    ShoppingCartModel = CheckoutValidate(httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                if (checkoutModel.ShoppingCartModel != null)
                {
                    //checkoutModel.ShoppingCartModel.Checkout = false;
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
                    checkoutModel.CheckoutGuestModel.CaptchaAnswerCheckoutGuest = null;
                    checkoutModel.CheckoutGuestModel.CaptchaNumberCheckoutGuest0 = httpSessionStateBase["CaptchaNumberCheckoutGuest0"].ToString();
                    checkoutModel.CheckoutGuestModel.CaptchaNumberCheckoutGuest1 = httpSessionStateBase["CaptchaNumberCheckoutGuest1"].ToString();
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
        //GET CheckoutValidate
        public ShoppingCartModel CheckoutValidate(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
                    {
                        ;
                    }
                    else
                    {
                        throw new Exception("Shopping Cart is Empty");
                    }
                }
                return shoppingCartModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //GET DeliveryInfo
        public DeliveryInfoModel DeliveryInfo(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                DeliveryInfoModel deliveryInfoModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                //shoppingCartModel.Checkout = false;
                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                if (shoppingCartModel == null)
                {
                    deliveryInfoModel = null;
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
                    {
                        deliveryInfoModel = new DeliveryInfoModel
                        {
                            DeliveryInfoDataModel = new DeliveryInfoDataModel
                            {
                                AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                                DeliveryAddressModel = new DemogInfoAddressModel
                                {
                                    BuildingTypeId = BuildingTypeEnum._,
                                    DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,//long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                                },
                                PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            },                           
                            ShoppingCartModel = shoppingCartModel,
                        };
                    }
                    else
                    {
                        deliveryInfoModel = null;
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
        //POST Delivery Info
        public void DeliveryInfo(DeliveryInfoDataModel deliveryInfoDataModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                DemogInfoCountryModel demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId);
                deliveryInfoDataModel.DeliveryAddressModel.CountryAbbrev = demogInfoCountryModel.CountryAbbrev;
                deliveryInfoDataModel.DeliveryAddressModel.CountryDesc = demogInfoCountryModel.CountryDesc;
                deliveryInfoDataModel.DeliveryAddressModel.StateAbbrev = DemogInfoCache.DemogInfoSubDivisionModels.First(x => x.DemogInfoSubDivisionId == deliveryInfoDataModel.DeliveryAddressModel.DemogInfoSubDivisionId).StateAbbrev;
                deliveryInfoDataModel.DeliveryAddressModel.DemogInfoZipPlusId = 0;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                shoppingCartModel.ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
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
                    }
                };
                ApplyDiscounts(shoppingCartModel, sessionObjectModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                long personId = sessionObjectModel.PersonId;
                AddAdditionalCharges(shoppingCartModel, deliveryInfoDataModel.DeliveryAddressModel, sessionObjectModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddTotals(shoppingCartModel.ShoppingCartSummaryItems, clientId, ipAddress, execUniqueId, loggedInUserId);
                httpSessionStateBase["DeliveryInfoDataModel"] = deliveryInfoDataModel;
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
        //GET GiftCert
        public GiftCertModel GiftCert(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                GiftCertModel giftCertModel = new GiftCertModel
                {
                    CaptchaNumber0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString(),
                    CaptchaNumber1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString(),
                };
                return giftCertModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //POST GiftCert
        public void GiftCert(ref GiftCertModel giftCertModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                if (archLibBL.ValidateCaptcha(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1", giftCertModel.CaptchaAnswer))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswer", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    LoginUserProfModel loginUserProfModel = new LoginUserProfModel
                    {
                        LoginEmailAddress = giftCertModel.SenderEmailAddress,
                        LoginPassword = giftCertModel.SenderPassword,
                    };
                    SessionObjectModel sessionObjectModel = archLibBL.LoginUserProfValidate(ref loginUserProfModel, httpSessionStateBase, modelStateDictionary, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (modelStateDictionary.IsValid)
                    {
                        string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                        CreditCardDataModel creditCardDataModel = new CreditCardDataModel
                        {
                            CreditCardAmount = giftCertModel.GiftCertAmount.ToString(),
                            CreditCardExpMM = giftCertModel.CardExpiryMM,
                            CreditCardExpYear = giftCertModel.CardExpiryYYYY,
                            CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
                            CreditCardNumber = giftCertModel.CreditCardNumber,
                            CreditCardProcessor = creditCardProcessor,
                            CreditCardSecCode = giftCertModel.CVV,
                            CreditCardTranType = "PAYMENT",
                            CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                            NameAsOnCard = giftCertModel.CardHolderName,
                        };
                        CreditCardServiceBL creditCardService = new CreditCardServiceBL();
                        giftCertModel.CreditCardProcessStatus = creditCardService.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out object creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        giftCertModel.CreditCardLast4 = creditCardDataModel.CreditCardNumberLast4;
                        giftCertModel.CreditCardProcessMessage = processMessage;
                        RegisterUserProfModel registerUserProfModel = new RegisterUserProfModel
                        {
                            RegisterEmailAddress = giftCertModel.RecipientEmailAddress,
                        };
                        string loginPassword = archLibBL.GenerateRandomKey(9);
                        bool userProfRegistered = RegisterUserProf(registerUserProfModel, loginPassword, giftCertModel.TelephoneNumber, "", "", ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId, out AspNetUserModel aspNetUserModel);
                        giftCertModel.PersonId = sessionObjectModel.PersonId;
                        giftCertModel.RecipientPersonId = (long)aspNetUserModel.PersonModel.PersonId;
                        giftCertModel.GiftCertKey = archLibBL.GenerateRandomKey(8);
                        giftCertModel.CreditCardDataId = creditCardDataModel.CreditCardDataId;
                        giftCertModel.GiftCertBalanceAmount = (float)giftCertModel.GiftCertAmount;
                        giftCertModel.RecipientEmailAddressRegistered = userProfRegistered ? $"Registered with password {loginPassword}" : "Already registered";
                        ApplicationDataContext.CreateGiftCert(giftCertModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        string giftCertDirectory = Utilities.GetServerMapPath("~/ClientSpecific/" + ArchLibCache.ClientId + "_" + ArchLibCache.ClientName + "/Documents/GiftCertificate/");
                        string inputFullFileName = giftCertDirectory + @"\Templates\" + giftCertModel.SelectedTemplateName;
                        giftCertModel.GiftCertImageFileName = giftCertDirectory + @"\" + giftCertModel.GiftCertNumber + ".jpg";
                        CreateGiftCertImage(inputFullFileName, giftCertModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                        string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", giftCertModel);
                        string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_GiftCertEmailSubject", giftCertModel);
                        string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_GiftCertEmailBody", giftCertModel);
                        emailBodyHtml += signatureHtml;
                        archLibBL.SendEmail(giftCertModel.SenderEmailAddress, emailSubjectText, emailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_GiftCertRecipientEmailBody", giftCertModel);
                        emailBodyHtml += signatureHtml;
                        archLibBL.SendEmail(giftCertModel.SenderEmailAddress, emailSubjectText, emailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    }
                    else
                    {
                    }
                }
                else
                {
                }
                if (modelStateDictionary.IsValid)
                {
                    giftCertModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ColumnCount = 3,
                        ResponseMessages = new List<string>
                        {
                            "Your purchase of gift certificate is successful",
                            "Email is sent both to sender and recipient",
                            "If you do not find it in your inbox",
                            "Please check your spam and mark the sender as safe",
                            "Should have any questions please feel free to contact us",
                        },
                        ResponseTypeId = ResponseTypeEnum.Success,
                    };
                }
                else
                {
                    giftCertModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                    };
                    archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                    giftCertModel.CaptchaAnswer = null;
                    giftCertModel.CaptchaNumber0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                    giftCertModel.CaptchaNumber1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                    archLibBL.MergeModelStateErrorMessages(modelStateDictionary);
                }
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
        //GET GiftCertBalance
        public void GiftCertBalance(string giftCertNumber, string giftCertKey, out string errorMessage, out float? giftCertBalanceAmount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                if (long.TryParse(giftCertNumber, out long temp))
                {
                    ApplicationDataContext.OpenSqlConnection();
                    GiftCertModel giftCertModel = ApplicationDataContext.GetGiftCert(giftCertNumber, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (giftCertModel.GiftCertKey == giftCertKey)
                    {
                        errorMessage = "";
                        giftCertBalanceAmount = giftCertModel.GiftCertBalanceAmount;
                    }
                    else
                    {
                        errorMessage = "Invalid Gift Cert# / Key";
                        giftCertBalanceAmount = null;
                    }
                }
                else
                {
                    errorMessage = "Invalid Gift Cert#";
                    giftCertBalanceAmount = null;
                }
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
        //GET ItemListView
        public ItemListModel ItemListView(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                ItemListModel itemListModel = new ItemListModel
                {
                    ItemModels = ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                return itemListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //GET OrderReceipt
        public OrderReceiptModel OrderReceipt(PaymentDataModel paymentDataModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                //shoppingCartModel.Checkout = false;
                OrderReceiptModel orderReceiptModel = new OrderReceiptModel
                {
                    DeliveryInfoDataModel = deliveryInfoDataModel,
                    PaymentDataModel = paymentDataModel,
                    ShoppingCartModel = shoppingCartModel,
                };
                string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailSubject", orderReceiptModel);
                string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailBody", orderReceiptModel);
                string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", orderReceiptModel);
                emailBodyHtml += signatureHtml;
                PDFUtility pDFUtility = new PDFUtility();
                pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, @"C:\Code\rramaswamy18\RetailSln\Email\" + orderReceiptModel.PaymentDataModel.OrderHeaderId + ".pdf");
                List<string> emailAttachmentFileNames = new List<string>
                {
                    @"C:\Code\rramaswamy18\RetailSln\Email\" + orderReceiptModel.PaymentDataModel.OrderHeaderId + ".pdf",
                };
                archLibBL.SendEmail(paymentDataModel.EmailAddress, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                return orderReceiptModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //GET Payment
        public PaymentModel Payment(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                //shoppingCartModel.Checkout = false;
                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
                SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
                    {
                        PaymentModel paymentModel = new PaymentModel
                        {
                            DeliveryInfoDataModel = deliveryInfoDataModel,
                            PaymentDataModel = new PaymentDataModel
                            {
                                EmailAddress = sessionObjectModel.EmailAddress,
                                UserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
                                OrderAmount = shoppingCartModel.ShoppingCartSummaryItems[shoppingCartModel.ShoppingCartSummaryItems.Count - 1].OrderAmount.Value,
                            },
                            ShoppingCartModel = shoppingCartModel,
                        };
                        return paymentModel;
                    }
                    else
                    {
                        throw new Exception("Shopping Cart is Empty");
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //POST Payment
        public void Payment(ref PaymentDataModel paymentDataModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, out object creditCardResponseObject)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            creditCardResponseObject = null;
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                float orderAmount, giftCertPaymentAmount, giftCertBalanceAmount, creditCardPaymentAmount;
                string giftCertNumberLast4 = null, creditCardNumberLast4 = null, creditCardProcessMessage = "";
                orderAmount = (float)paymentDataModel.OrderAmount;
                GiftCertModel giftCertModel = null;
                if (modelStateDictionary.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(paymentDataModel.GiftCertNumber))
                    {
                        giftCertModel = ProcessGiftCardPayment(paymentDataModel.GiftCertNumber, paymentDataModel.GiftCertKey, orderAmount, out giftCertPaymentAmount, out giftCertBalanceAmount, modelStateDictionary, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        giftCertNumberLast4 = paymentDataModel.GiftCertNumber.Substring(paymentDataModel.GiftCertNumber.Length - 4);
                        paymentDataModel.GiftCertNumberLast4 = giftCertNumberLast4;
                        creditCardPaymentAmount = orderAmount - giftCertPaymentAmount;
                    }
                    else
                    {
                        creditCardPaymentAmount = orderAmount;
                        giftCertPaymentAmount = 0;
                    }
                    paymentDataModel.GiftCertPaymentAmount = giftCertPaymentAmount;
                    paymentDataModel.CreditCardPaymentAmount = creditCardPaymentAmount;
                    if (modelStateDictionary.IsValid)
                    {
                        paymentDataModel.EmailAddress = sessionObjectModel.EmailAddress;
                        paymentDataModel.UserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName;
                        if (creditCardPaymentAmount > 0)
                        {
                            string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                            CreditCardDataModel creditCardDataModel = new CreditCardDataModel
                            {
                                CreditCardAmount = creditCardPaymentAmount.ToString("0.00"),
                                CreditCardExpMM = paymentDataModel.CardExpiryMM,
                                CreditCardExpYear = paymentDataModel.CardExpiryYYYY,
                                CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
                                CreditCardNumber = paymentDataModel.CreditCardNumber,
                                CreditCardProcessor = creditCardProcessor,
                                CreditCardSecCode = paymentDataModel.CVV,
                                CreditCardTranType = "PAYMENT",
                                CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                                EmailAddress = sessionObjectModel.EmailAddress,
                                NameAsOnCard = string.IsNullOrWhiteSpace(paymentDataModel.CardHolderName) ? sessionObjectModel.FirstName + " " + sessionObjectModel.LastName : paymentDataModel.CardHolderName,
                                TelephoneNumber = sessionObjectModel.PhoneNumber,
                            };
                            CreditCardServiceBL creditCardService = new CreditCardServiceBL();
                            paymentDataModel.AspNetRoleName = sessionObjectModel.AspNetRoleName;
                            paymentDataModel.CreditCardProcessStatus = creditCardService.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                            paymentDataModel.CreditCardProcessMessage = creditCardDataModel.ProcessMessage;
                            paymentDataModel.CreditCardNumberLast4 = creditCardDataModel.CreditCardNumberLast4;
                            paymentDataModel.CreditCardDataId = creditCardDataModel.CreditCardDataId;
                            httpSessionStateBase["CreditCardDataId"] = paymentDataModel.CreditCardDataId;
                            creditCardNumberLast4 = creditCardDataModel.CreditCardNumberLast4;
                            if (paymentDataModel.CreditCardProcessStatus)
                            {
                                creditCardProcessMessage = paymentDataModel.CreditCardProcessMessage;
                            }
                            else
                            {
                                modelStateDictionary.AddModelError("", creditCardDataModel.ProcessMessage);
                                modelStateDictionary.AddModelError("CreditCardNumber", creditCardDataModel.ProcessMessage);
                                modelStateDictionary.AddModelError("CardHolderName", creditCardDataModel.ProcessMessage);
                            }
                        }
                        else
                        {
                            paymentDataModel.CreditCardProcessStatus = true;
                            paymentDataModel.CreditCardProcessMessage = "Not Required";
                            creditCardProcessMessage = paymentDataModel.CreditCardProcessMessage;
                        }
                        httpSessionStateBase["PaymentDataModel"] = paymentDataModel;
                        if (paymentDataModel.CreditCardProcessStatus)
                        {
                            UpdatePayments(giftCertNumberLast4, giftCertPaymentAmount, creditCardNumberLast4, creditCardProcessMessage, creditCardPaymentAmount, shoppingCartModel.ShoppingCartSummaryItems, clientId, ipAddress, execUniqueId, loggedInUserId);
                            //var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
                            //if (creditCardProcessor == "TESTMODE" || creditCardProcessor == "NUVEITEST" || creditCardProcessor == "NUVEIPROD")
                            if (creditCardResponseObject == null)
                            {
                                CreateOrder(ref paymentDataModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                                ApplicationDataContext.CreatePayment(paymentDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                                if (giftCertModel != null)
                                {
                                    ApplicationDataContext.ModifyGiftCertBalance(giftCertModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                                }
                                paymentDataModel.ResponseObjectModel = new ResponseObjectModel
                                {
                                    ResponseTypeId = ResponseTypeEnum.Success,
                                    ResponseMessages = new List<string>
                                    {
                                        "Your order has been processed",
                                        "Below is the summary of the invoice",
                                        "Please check your email",
                                    },
                                };
                            }
                            else
                            {
                                //Return without doing anything
                            }
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
        public OrderReceiptModel Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
                ApplicationDataContext.OpenSqlConnection();
                PaymentDataModel paymentDataModel = (PaymentDataModel)httpSessionStateBase["PaymentDataModel"];
                paymentDataModel.CreditCardProcessMessage = razorpay_payment_id;
                creditCardServiceBL.UpdCreditCardData(paymentDataModel.CreditCardDataId, razorpay_payment_id, razorpay_order_id, razorpay_signature, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                CreateOrder(ref paymentDataModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.CreatePayment(paymentDataModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderReceiptModel orderReceiptModel = OrderReceipt(paymentDataModel, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                return orderReceiptModel;
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
        public ShoppingCartModel RemoveFromCart(int index, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
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
                        httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                        return shoppingCartModel;
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
        //GET ShoppingCartComments
        public void ShoppingCartComments(int index, string orderComments, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (index > -1 && index < shoppingCartModel.ShoppingCartItems.Count)
                    {
                        shoppingCartModel.ShoppingCartItems[index].OrderComments = orderComments;
                        httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
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
        private void AddAdditionalCharges(ShoppingCartModel shoppingCartModel, DemogInfoAddressModel demogInfoAddressModel, SessionObjectModel sessionObjectModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            List<ShoppingCartItemModel> shoppingCartSummaryItems = shoppingCartModel.ShoppingCartSummaryItems;
            float totalOrderAmount = shoppingCartModel.ShoppingCartTotalAmount.Value, discountAmount = 0;
            var corpAcctModel = RetailSlnCache.CorpAcctModels.First(x => x.CorpAcctId == sessionObjectModel.CorpAcctId);
            if (corpAcctModel != null && corpAcctModel.CorpAcctId > 0)
            {
                foreach (var discountDtlModel in corpAcctModel.DiscountDtlModels)
                {
                    discountAmount += totalOrderAmount * discountDtlModel.CorpAcctDiscountPercent / 100f;
                    shoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemRate = discountDtlModel.CorpAcctDiscountPercent,
                            ItemShortDesc = "Discount (" + discountDtlModel.CorpAcctDiscountPercent + "%)",
                            OrderAmount = -1 * totalOrderAmount * discountDtlModel.CorpAcctDiscountPercent / 100f,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                        }
                    );
                }
            }
            if (discountAmount > 0)
            {
                shoppingCartSummaryItems.Add
                (
                    new ShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemShortDesc = "Total Order Amount after Discount",
                        OrderAmount = totalOrderAmount - discountAmount,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmountAfterDiscount,
                    }
                );
            }
            totalOrderAmount -= discountAmount;
            var salesTaxListModels = GetSalesTaxListModels(demogInfoAddressModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            var deliveryChargeModel = RetailSlnCache.DeliveryChargeModels.FirstOrDefault(x => x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId && (x.DestDemogInfoSubDivisionId == null || x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId) && (x.DestDemogInfoCityId == null || x.DestDemogInfoCityId == demogInfoAddressModel.DemogInfoCityId));
            if (deliveryChargeModel == null)
            {
                deliveryChargeModel = RetailSlnCache.DeliveryChargeModels.FirstOrDefault(x => x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId && (x.DestDemogInfoSubDivisionId == null || x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId));
            }
            float shippingAndHandlingChargesByWeight = 0, shippingAndHandlingChargesByVolume = 0;
            if (deliveryChargeModel.UnitMeasure == "WEIGHT")
            {
                shippingAndHandlingChargesByWeight = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
            }
            if (deliveryChargeModel.UnitMeasure == "VOLUME")
            {
                shippingAndHandlingChargesByVolume = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
            }
            float shippingAndHandlingChargesAmount = shippingAndHandlingChargesByWeight * shoppingCartModel.TotalWeightValueRounded;
            float fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
            foreach (var salesTaxListModel in salesTaxListModels)
            {
                if (salesTaxListModel.LineItemLevelName == "SUMMARY")
                {
                    shoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemRate = totalOrderAmount,
                            ItemShortDesc = salesTaxListModel.SalesTaxCaptionId + " (" + salesTaxListModel.SalesTaxRate + "%)",
                            OrderAmount = totalOrderAmount * salesTaxListModel.SalesTaxRate / 100f,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                }
                else
                {
                    shoppingCartModel.ShoppingCartSummaryItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                            OrderAmount = 0,
                            OrderComments = null,
                        }
                    );
                    foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                    {
                        var itemAttribValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemAttribModels.ToList().First(x => x.ItemAttribMasterModel.AttribName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemAttribValue;
                        shoppingCartItem.ShoppingCartItemSummarys.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                ItemRate = float.Parse(itemAttribValue),
                                OrderAmount = float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount / 100f,
                            }
                        );
                        shoppingCartModel.ShoppingCartSummaryItems[shoppingCartModel.ShoppingCartSummaryItems.Count - 1].OrderAmount += float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount / 100f;
                    }
                }
            }
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = shippingAndHandlingChargesByWeight,
                    ItemShortDesc = "Shipping & Handling Charges " + shoppingCartModel.TotalWeightValueRounded + " " + shoppingCartModel.TotalWeightValueRoundedUnit + " - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
                    OrderAmount = shippingAndHandlingChargesAmount,
                    OrderComments = null,
                    OrderQty = shoppingCartModel.TotalWeightValueRounded,
                    OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                }
            );
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = shippingAndHandlingChargesByWeight,
                    ItemShortDesc = "Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%)",
                    OrderAmount = fuelCharges,
                    OrderComments = null,
                    OrderQty = shoppingCartModel.TotalWeightValueRounded,
                    OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                }
            );
            foreach (var salesTaxListModel in salesTaxListModels)
            {
                shoppingCartSummaryItems.Add
                (
                    new ShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = shippingAndHandlingChargesByWeight,
                        ItemShortDesc = salesTaxListModel.SalesTaxCaptionId + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
                        OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
                        OrderComments = null,
                        OrderQty = shoppingCartModel.TotalWeightValueRounded,
                        OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                    }
                );
            }
        }
        private List<SalesTaxListModel> GetSalesTaxListModels(DemogInfoAddressModel demogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                 && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
                 && x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId
                 && (demogInfoAddressModel.DemogInfoZipId >= x.DestDemogInfoZipIdFrom
                 && demogInfoAddressModel.DemogInfoZipId >= x.DestDemogInfoZipIdTo)
                );
            if (!salesTaxListModels.Any())
            {
                salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                    && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
                    && x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId
                    && x.DestDemogInfoZipIdFrom == null
                    && x.DestDemogInfoZipIdTo == null
                );
            }
            if (!salesTaxListModels.Any())
            {
                salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                    && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
                    && x.DestDemogInfoSubDivisionId == null
                    && x.DestDemogInfoZipIdFrom == null
                    && x.DestDemogInfoZipIdTo == null
                );
            }
            return salesTaxListModels;
        }
        private void AddTotals(List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            float totalInvoiceAmount = 0;
            foreach (var shoppingCartSummary in shoppingCartSummaryItems)
            {
                if (shoppingCartSummary.OrderDetailTypeId != OrderDetailTypeEnum.TotalOrderAmountAfterDiscount)
                {
                    totalInvoiceAmount += shoppingCartSummary.OrderAmount.Value;
                }
            }
            shoppingCartSummaryItems.Add
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
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Amount Paid - Gift Cert",
                    OrderAmount = 0f,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByGiftCert,
                }
            );
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Amount Paid - Credit Card",
                    OrderAmount = 0f,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCreditCard,
                }
            );
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 0,
                    ItemShortDesc = "Total Amount Paid",
                    OrderAmount = 0f,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
                }
            );
            shoppingCartSummaryItems.Add
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
        }
        private void ApplyDiscounts(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
            {
                shoppingCartItem.ShoppingCartItemSummarys = new List<ShoppingCartItemModel>();
            }
            string itemIds = "", prefixString = "";
            foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
            {
                itemIds += prefixString + shoppingCartItem.ItemId;
                prefixString = ", ";
            }
            string sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + sessionObjectModel.CorpAcctId + " AND ItemId IN(" + itemIds + ")";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ShoppingCartItemModel shoppingCartItemModel;
            while (sqlDataReader.Read())
            {
                shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.First(x => x.ItemId == long.Parse(sqlDataReader["ItemId"].ToString()));
                shoppingCartItemModel.ItemDiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString());
                shoppingCartItemModel.ItemRate = shoppingCartItemModel.ItemRateBeforeDiscount.Value * (100 - shoppingCartItemModel.ItemDiscountPercent) / 100f;
                shoppingCartItemModel.OrderAmount = shoppingCartItemModel.ItemRate * shoppingCartItemModel.OrderQty;
            }
            sqlDataReader.Close();
        }
        private void UpdatePayments(string giftCertLast4, float? giftCertPaymentAmount, string creditCardLast4, string creditCardProcessUniqueRef, float? creditCardPaymentAmount, List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShoppingCartItemModel shoppingCartItemModel;
            float totalPaymentAmount = 0;
            if (giftCertPaymentAmount != null)
            {
                shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByGiftCert);
                shoppingCartItemModel.ItemShortDesc += " " + giftCertLast4;
                shoppingCartItemModel.OrderAmount = -1 * giftCertPaymentAmount;
                totalPaymentAmount += giftCertPaymentAmount.Value;
            }
            if (creditCardPaymentAmount != null)
            {
                shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
                shoppingCartItemModel.ItemShortDesc += " " + creditCardLast4 + " " + creditCardProcessUniqueRef;
                shoppingCartItemModel.OrderAmount = -1 * creditCardPaymentAmount;
                totalPaymentAmount += creditCardPaymentAmount.Value;
            }
            shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalInvoiceAmount); //Total Invoice Amount
            float totalInvoiceAmount = shoppingCartItemModel.OrderAmount.Value;
            shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid); //Total Amount Paid
            shoppingCartItemModel.OrderAmount = -1 * totalPaymentAmount;
            shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue); //Balance Due
            shoppingCartItemModel.OrderAmount = totalInvoiceAmount - totalPaymentAmount;
        }
        private void UpdateShoppingCart(ShoppingCartModel shoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                shoppingCartModel.ShoppingCartTotalAmount = 0;
                shoppingCartModel.TotalVolumeValue = 0;
                shoppingCartModel.TotalWeightValue = 0;
                shoppingCartModel.TotalItemsCount = 0;
                foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                {
                    shoppingCartModel.ShoppingCartTotalAmount += shoppingCartItem.OrderAmount;
                    shoppingCartModel.TotalVolumeValue += shoppingCartItem.VolumeValue;
                    shoppingCartModel.TotalWeightValue += shoppingCartItem.WeightValue;
                    shoppingCartModel.TotalItemsCount += shoppingCartItem.OrderQty.Value;
                }
                shoppingCartModel.TotalWeightValueRounded = (long)Math.Ceiling(shoppingCartModel.TotalWeightValue.Value / 1000f);
                shoppingCartModel.TotalWeightValueRoundedUnit = WeightUnitEnum.Kilograms;
                shoppingCartModel.ShoppingCartSummaryItems[0].ItemShortDesc = "Total Order Amount (#" + shoppingCartModel.TotalItemsCount + ")";
                shoppingCartModel.ShoppingCartSummaryItems[0].OrderAmount = shoppingCartModel.ShoppingCartTotalAmount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private bool CreateOrder(ref PaymentDataModel paymentDataModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");

            SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
            ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
            DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
            OrderModel orderModel = new OrderModel
            {
                DeliveryInfoModel = new DeliveryInfoModel
                {
                    DeliveryInfoDataModel = deliveryInfoDataModel,
                },
                OrderHeaderModel = new OrderHeaderModel
                {
                    DimensionUnitId = DimensionUnitEnum.Centimeter,
                    OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    OrderNumber = 1,
                    OrderStatusId = OrderStatusEnum.Open,
                    PersonId = sessionObjectModel.PersonId,
                    VolumeValue = 0,
                    WeightUnitId = WeightUnitEnum.Grams,
                    WeightValue = 0,
                    OrderDetailModels = new List<OrderDetailModel>(),
                    OrderSummaryModels = new List<OrderDetailModel>(),
                },
            };
            ItemModel itemModel;
            float itemRate;
            foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
            {
                itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItem.ItemId);
                itemRate = itemModel.ItemRate.Value;
                orderModel.OrderHeaderModel.OrderDetailModels.Add
                (
                    new OrderDetailModel
                    {
                        ItemDesc = itemModel.ItemDesc,
                        ItemRate = itemRate,
                        ItemShortDesc = itemModel.ItemShortDesc,
                        ItemId = itemModel.ItemId,
                        OrderAmount = itemRate * shoppingCartItem.OrderQty.Value,
                        OrderComments = shoppingCartItem.OrderComments,
                        OrderDetailTypeId = OrderDetailTypeEnum.Item,
                        OrderQty = (long)shoppingCartItem.OrderQty,
                        VolumeValue = shoppingCartItem.VolumeValue.Value,
                        WeightValue = shoppingCartItem.WeightValue.Value,
                    }
                );
            }
            foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartSummaryItems)
            {
                orderModel.OrderHeaderModel.OrderDetailModels.Add
                (
                    new OrderDetailModel
                    {
                        ItemDesc = null,
                        ItemRate = shoppingCartItem.OrderAmount.Value,
                        ItemShortDesc = shoppingCartItem.ItemShortDesc,
                        ItemId = shoppingCartItem.ItemId,
                        OrderAmount = shoppingCartItem.OrderAmount.Value,
                        OrderComments = shoppingCartItem.OrderComments,
                        OrderDetailTypeId = shoppingCartItem.OrderDetailTypeId,
                        OrderQty = 1,
                    }
                );
            }
            ApplicationDataContext.CreateOrder(orderModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            paymentDataModel.OrderHeaderId = orderModel.OrderHeaderModel.OrderHeaderId;
            return true;
        }
        private void CreateGiftCertImage(string inputFullFileName, GiftCertModel giftCertModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            Bitmap bitmap = new Bitmap(inputFullFileName);
            Graphics graphics = Graphics.FromImage(bitmap);

            string giftAmountText = ConvertAmountToWords((long)giftCertModel.GiftCertAmount);
            graphics.DrawString(giftCertModel.RecipientFullName, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(189, 180));
            graphics.DrawString(giftAmountText, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new PointF(225, 228));
            graphics.DrawString(giftCertModel.GiftCertMessage, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(252, 270));
            graphics.DrawString("From : " + giftCertModel.SenderFullName, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(153, 342));
            graphics.DrawString("Gift Certificate# : " + giftCertModel.GiftCertNumber, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new PointF(153, 360));
            graphics.DrawString("Gift Certificate Key : " + giftCertModel.GiftCertKey, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new PointF(153, 378));
            graphics.DrawString("Login using your email address, password & use above Gift Cert# & key while making payment", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new PointF(153, 396));

            bitmap.Save(giftCertModel.GiftCertImageFileName);
            bitmap.Dispose();
        }
        private GiftCertModel ProcessGiftCardPayment(string giftCertNumber, string giftCertKey, float orderAmount, out float giftCertPaymentAmount, out float giftCertBalanceAmount, ModelStateDictionary modelStateDictionary, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            GiftCertModel giftCertModel = ApplicationDataContext.GetGiftCert(giftCertNumber, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (giftCertModel == null)
            {
                modelStateDictionary.AddModelError("GiftCertNumber", "Invalid Gift Cert#");
                modelStateDictionary.AddModelError("", "Invalid Gift Cert#");
                giftCertPaymentAmount = 0;
                giftCertBalanceAmount = 0;
            }
            else
            {
                if (giftCertModel.GiftCertKey == giftCertKey)
                {
                    if (giftCertModel.GiftCertBalanceAmount > 0)
                    {
                        if (giftCertModel.GiftCertBalanceAmount > orderAmount)
                        {//100 > 40 - Apply 40 to this payment. Balance will be 60 after this
                            giftCertPaymentAmount = orderAmount;
                        }
                        else
                        {//100 < 250 - Apply 100 to this payment. 150 from CC. Balance will be 0 after this
                            giftCertPaymentAmount = giftCertModel.GiftCertBalanceAmount;
                        }
                        giftCertBalanceAmount = giftCertModel.GiftCertBalanceAmount - giftCertPaymentAmount;
                        giftCertModel.GiftCertUsedAmount += giftCertPaymentAmount;
                        giftCertModel.GiftCertBalanceAmount = (float)giftCertModel.GiftCertAmount - giftCertModel.GiftCertUsedAmount;
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("GiftCertNumber", "Invalid Gift Cert Amount");
                        modelStateDictionary.AddModelError("", "Invalid Gift Cert Amount");
                        giftCertPaymentAmount = 0;
                        giftCertBalanceAmount = 0;
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("GiftCertKey", "Invalid Gift Cert Key");
                    modelStateDictionary.AddModelError("", "Invalid Gift Cert Key");
                    giftCertPaymentAmount = 0;
                    giftCertBalanceAmount = 0;
                }
            }
            return giftCertModel;
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
            Dictionary<string, string> creditCardKVPs;
            switch (creditCardProcessor.ToUpper())
            {
                case "NUVEI":
                    creditCardKVPs = new Dictionary<string, string>
                    {
                        { "PrivateKey", ArchLibCache.GetPrivateKey(clientId) },
                        { "NuveiRestAPIRootUri", ArchLibCache.GetApplicationDefault(clientId, "NuveiRestAPIRootUri", "") },
                        { "NuveiRequestUri", ArchLibCache.GetApplicationDefault(clientId, "NuveiRequestUri", "") },
                        { "NuveiTerminalId", ArchLibCache.GetApplicationDefault(clientId, "NuveiTerminalId", "") },
                        { "NuveiSharedSecret", ArchLibCache.GetApplicationDefault(clientId, "NuveiSharedSecret", "") },
                    };
                    break;
                case "RAZORPAYTEST":
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
    }
}
