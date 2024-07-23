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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public class RetailSlnBLBackup
    {
        #region
        //// GET: AddToCart
        //public void AddToCart(ref PaymentInfo1Model paymentInfo1Model, long itemId, long orderQty, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        if (paymentInfo1Model == null)
        //        {
        //            paymentInfo1Model = new PaymentInfo1Model();
        //        }
        //        ShoppingCartModel shoppingCartModel;
        //        ShoppingCartItemModel shoppingCartItemModel;
        //        shoppingCartModel = paymentInfo1Model.ShoppingCartModel;
        //        if (shoppingCartModel == null)
        //        {
        //            shoppingCartModel = new ShoppingCartModel
        //            {
        //                ShoppingCartItems = new List<ShoppingCartItemModel>(),
        //                ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        //                {
        //                    new ShoppingCartItemModel
        //                    {
        //                        ItemDesc = null,
        //                        ItemId = null,
        //                        ItemRate = null,
        //                        ItemRateBeforeDiscount = null,
        //                        ItemShortDesc = null,
        //                        OrderAmount = null,
        //                        OrderAmountBeforeDiscount = null,
        //                        OrderComments = null,
        //                        OrderQty = 1,
        //                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //                    },
        //                },
        //                ShoppingCartTotalAmount = 0,
        //            };
        //        }
        //        shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        //        if (shoppingCartItemModel != null)
        //        {
        //            shoppingCartItemModel.OrderQty += orderQty;
        //            shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
        //            shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRateBeforeDiscount;
        //            shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        //            shoppingCartItemModel.WeightCalcValue = orderQty * shoppingCartItemModel.WeightCalcValue;
        //            shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
        //            UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            return;
        //        }
        //        else
        //        {
        //            ItemModel itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
        //            float heightValue, lengthValue, weightCalcValue, weightValue, widthValue, itemRate;
        //            DimensionUnitEnum dimensionUnitId;
        //            WeightUnitEnum weightUnitId;
        //            dimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Height").ItemSpecUnitValue);
        //            weightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Weight").ItemSpecUnitValue);
        //            heightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Height").ItemSpecValue);
        //            lengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Length").ItemSpecValue);
        //            weightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "CalcProductWeight").ItemSpecValue);
        //            weightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Weight").ItemSpecValue);
        //            widthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Width").ItemSpecValue);
        //            if (itemModel != null)
        //            {
        //                itemRate = itemModel.ItemRate.Value;
        //                shoppingCartModel.ShoppingCartItems.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        DimensionUnitId = dimensionUnitId,
        //                        HeightValue = heightValue,
        //                        ItemDesc = itemModel.ItemDesc,
        //                        ItemDiscountPercent = null,
        //                        ItemId = itemModel.ItemId,
        //                        ItemRate = itemRate,
        //                        ItemRateBeforeDiscount = itemRate,
        //                        ItemShortDesc = itemModel.ItemShortDesc,
        //                        LengthValue = lengthValue,
        //                        OrderAmount = orderQty * itemRate,
        //                        OrderAmountBeforeDiscount = orderQty * itemRate,
        //                        OrderComments = "",
        //                        OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //                        OrderQty = orderQty,
        //                        VolumeValue = lengthValue * widthValue * heightValue * orderQty,
        //                        WeightCalcValue = weightCalcValue,
        //                        WeightUnitId = weightUnitId,
        //                        WeightValue = weightValue * orderQty,
        //                        WidthValue = widthValue,
        //                    }
        //                );
        //                UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //                return;
        //            }
        //            else
        //            {
        //                throw new Exception("Error while adding item to shopping cart itemid=" + itemId + " orderQty=" + orderQty);
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //// POST: AddToCart
        //public void AddToCart(ref PaymentInfo1Model paymentInfo1Model, List<ShoppingCartItemModel> shoppingCartItemModels, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        //        //ShoppingCartModel shoppingCartModel;
        //        ShoppingCartItemModel shoppingCartItemModel;
        //        //shoppingCartModel = paymentInfo1Model.ShoppingCartModel;
        //        if (paymentInfo1Model.ShoppingCartModel == null)
        //        {
        //            paymentInfo1Model.ShoppingCartModel = new ShoppingCartModel
        //            {
        //                BackToTop = true,
        //                Checkout = true,
        //                ShoppingCartItems = new List<ShoppingCartItemModel>(),
        //                ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        //                {
        //                    new ShoppingCartItemModel
        //                    {
        //                        ItemDesc = null,
        //                        ItemId = null,
        //                        ItemRate = null,
        //                        ItemRateBeforeDiscount = null,
        //                        ItemShortDesc = null,
        //                        OrderAmount = null,
        //                        OrderAmountBeforeDiscount = null,
        //                        OrderComments = null,
        //                        OrderQty = 1,
        //                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //                    },
        //                },
        //                ShoppingCartTotalAmount = 0,
        //            };
        //        }
        //        foreach (var shoppingCartItemModelTemp in shoppingCartItemModels)
        //        {
        //            itemId = shoppingCartItemModelTemp.ItemId.Value;
        //            orderQty = shoppingCartItemModelTemp.OrderQty.Value;
        //            orderComments = shoppingCartItemModelTemp.OrderComments;
        //            shoppingCartItemModel = paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        //            if (shoppingCartItemModel == null)
        //            {
        //                itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
        //                paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.Add
        //                (
        //                    shoppingCartItemModel = new ShoppingCartItemModel
        //                    {
        //                        DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductHeight").ItemSpecUnitValue),
        //                        HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductHeight").ItemSpecValue),
        //                        HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        //                        ItemDesc = itemModel.ItemDesc,
        //                        ItemDiscountPercent = null,
        //                        ItemId = itemModel.ItemId,
        //                        ItemRate = itemModel.ItemRate,
        //                        ItemRateBeforeDiscount = itemModel.ItemRate,
        //                        ItemShortDesc = itemModel.ItemShortDesc,
        //                        LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductLength").ItemSpecValue),
        //                        OrderAmount = orderQty * itemModel.ItemRate,
        //                        OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
        //                        OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //                        OrderQty = orderQty,
        //                        ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        //                        WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "CalcProductWeight").ItemSpecValue),
        //                        WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductWeight").ItemSpecUnitValue),
        //                        WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductWeight").ItemSpecValue),
        //                        WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductWidth").ItemSpecValue),
        //                        ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductOrVolumetricWeight").ItemSpecValue),
        //                        ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        //                    }
        //                );
        //            }
        //            else
        //            {
        //                shoppingCartItemModel.OrderQty += orderQty;
        //            }
        //            shoppingCartItemModel.OrderComments = orderComments;
        //            shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
        //            shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRate;
        //            shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        //            shoppingCartItemModel.WeightCalcValue = orderQty * shoppingCartItemModel.WeightCalcValue;
        //            shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
        //            shoppingCartItemModel.ProductOrVolumetricWeight = orderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
        //        }
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
        //// GET: Checkout
        //public CheckoutModel Checkout(PaymentInfo1Model paymentInfoModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        CheckoutValidate(paymentInfoModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        CheckoutModel checkoutModel = new CheckoutModel
        //        {
        //            ContactUsModel = new ContactUsModel(),
        //            LoginUserProfGuestModel = new LoginUserProfGuestModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                },
        //            },
        //            LoginUserProfModel = new LoginUserProfModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                },
        //            },
        //            RegisterUserProfModel = new RegisterUserProfModel
        //            {
        //                RegisterTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry")),
        //            },
        //            ResetPasswordModel = new ResetPasswordModel(),
        //            PaymentInfoModel = paymentInfoModel,
        //        };
        //        if (checkoutModel.PaymentInfoModel != null)
        //        {
        //            checkoutModel.PaymentInfoModel.ShoppingCartModel.BackToTop = false;
        //            checkoutModel.PaymentInfoModel.ShoppingCartModel.Checkout = false;
        //            List<string> numberSessions = new List<string>
        //            {
        //                "CaptchaNumberCheckoutGuest0",
        //                "CaptchaNumberCheckoutGuest1",
        //                "CaptchaNumberLogin0",
        //                "CaptchaNumberLogin1",
        //                "CaptchaNumberRegister0",
        //                "CaptchaNumberRegister1",
        //                "CaptchaNumberResetPassword0",
        //                "CaptchaNumberResetPassword1",
        //                "CaptchaNumberContactUs0",
        //                "CaptchaNumberContactUs1",
        //            };
        //            archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, numberSessions);
        //            checkoutModel.ContactUsModel.CaptchaAnswerContactUs = null;
        //            checkoutModel.ContactUsModel.CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString();
        //            checkoutModel.ContactUsModel.CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString();
        //            checkoutModel.LoginUserProfGuestModel.CaptchaAnswerLoginUserProfGuest = null;
        //            checkoutModel.LoginUserProfGuestModel.CaptchaNumberLoginUserProfGuest0 = httpSessionStateBase["CaptchaNumberCheckoutGuest0"].ToString();
        //            checkoutModel.LoginUserProfGuestModel.CaptchaNumberLoginUserProfGuest1 = httpSessionStateBase["CaptchaNumberCheckoutGuest1"].ToString();
        //            checkoutModel.LoginUserProfModel.CaptchaAnswerLogin = null;
        //            checkoutModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
        //            checkoutModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
        //            checkoutModel.RegisterUserProfModel.CaptchaAnswerRegister = null;
        //            checkoutModel.RegisterUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
        //            checkoutModel.RegisterUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
        //            checkoutModel.ResetPasswordModel.CaptchaAnswerResetPassword = null;
        //            checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
        //            checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
        //        }
        //        return checkoutModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public void CheckoutValidate(PaymentInfo1Model paymentInfoModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfoModel = paymentInfoModel ?? new PaymentInfo1Model();
        //        ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
        //        if (shoppingCartModel == null)
        //        {
        //            throw new Exception("Shopping Cart is Empty");
        //        }
        //        else
        //        {
        //            if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
        //            {
        //                ;
        //            }
        //            else
        //            {
        //                throw new Exception("Shopping Cart is Empty");
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
        //#region
        //////GET AddToCart
        ////public ShoppingCartModel AddToCart(long itemId, long orderQty, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        ShoppingCartModel shoppingCartModel;
        ////        ShoppingCartItemModel shoppingCartItemModel;
        ////        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        if (shoppingCartModel == null)
        ////        {
        ////            shoppingCartModel = new ShoppingCartModel
        ////            {
        ////                ShoppingCartItems = new List<ShoppingCartItemModel>(),
        ////                ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        ////                {
        ////                    new ShoppingCartItemModel
        ////                    {
        ////                        ItemDesc = null,
        ////                        ItemId = null,
        ////                        ItemRate = null,
        ////                        ItemRateBeforeDiscount = null,
        ////                        ItemShortDesc = null,
        ////                        OrderAmount = null,
        ////                        OrderAmountBeforeDiscount = null,
        ////                        OrderComments = null,
        ////                        OrderQty = 1,
        ////                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        ////                    },
        ////                },
        ////                ShoppingCartTotalAmount = 0,
        ////            };
        ////        }
        ////        shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        ////        if (shoppingCartItemModel != null)
        ////        {
        ////            shoppingCartItemModel.OrderQty += orderQty;
        ////            shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
        ////            shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRateBeforeDiscount;
        ////            shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        ////            shoppingCartItemModel.WeightCalcValue = orderQty * shoppingCartItemModel.WeightCalcValue;
        ////            shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
        ////            UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////            return shoppingCartModel;
        ////        }
        ////        else
        ////        {
        ////            ItemModel itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
        ////            float heightValue, lengthValue, weightCalcValue, weightValue, widthValue, itemRate;
        ////            DimensionUnitEnum dimensionUnitId;
        ////            WeightUnitEnum weightUnitId;
        ////            dimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Height").ItemSpecUnitValue);
        ////            weightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Weight").ItemSpecUnitValue);
        ////            heightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Height").ItemSpecValue);
        ////            lengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Length").ItemSpecValue);
        ////            weightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "CalcProductWeight").ItemSpecValue);
        ////            weightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Weight").ItemSpecValue);
        ////            widthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "Width").ItemSpecValue);
        ////            if (itemModel != null)
        ////            {
        ////                itemRate = itemModel.ItemRate.Value;
        ////                shoppingCartModel.ShoppingCartItems.Add
        ////                (
        ////                    new ShoppingCartItemModel
        ////                    {
        ////                        DimensionUnitId = dimensionUnitId,
        ////                        HeightValue = heightValue,
        ////                        ItemDesc = itemModel.ItemDesc,
        ////                        ItemDiscountPercent = null,
        ////                        ItemId = itemModel.ItemId,
        ////                        ItemRate = itemRate,
        ////                        ItemRateBeforeDiscount = itemRate,
        ////                        ItemShortDesc = itemModel.ItemShortDesc,
        ////                        LengthValue = lengthValue,
        ////                        OrderAmount = orderQty * itemRate,
        ////                        OrderAmountBeforeDiscount = orderQty * itemRate,
        ////                        OrderComments = "",
        ////                        OrderDetailTypeId = OrderDetailTypeEnum.Item,
        ////                        OrderQty = orderQty,
        ////                        VolumeValue = lengthValue * widthValue * heightValue * orderQty,
        ////                        WeightCalcValue = weightCalcValue,
        ////                        WeightUnitId = weightUnitId,
        ////                        WeightValue = weightValue * orderQty,
        ////                        WidthValue = widthValue,
        ////                    }
        ////                );
        ////                UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
        ////                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        ////                return shoppingCartModel;
        ////            }
        ////            else
        ////            {
        ////                throw new Exception("Error while adding item to shopping cart itemid=" + itemId + " orderQty=" + orderQty);
        ////            }
        ////        }
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        //////POST AddToCart
        ////public ShoppingCartModel AddToCart(List<ShoppingCartItemModel> shoppingCartItemModels, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        long itemId, orderQty;
        ////        string orderComments;
        ////        ItemModel itemModel;
        ////        ShoppingCartModel shoppingCartModel;
        ////        ShoppingCartItemModel shoppingCartItemModel;
        ////        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        if (shoppingCartModel == null)
        ////        {
        ////            shoppingCartModel = new ShoppingCartModel
        ////            {
        ////                //Checkout = true,
        ////                ShoppingCartItems = new List<ShoppingCartItemModel>(),
        ////                ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        ////                {
        ////                    new ShoppingCartItemModel
        ////                    {
        ////                        ItemDesc = null,
        ////                        ItemId = null,
        ////                        ItemRate = null,
        ////                        ItemRateBeforeDiscount = null,
        ////                        ItemShortDesc = null,
        ////                        OrderAmount = null,
        ////                        OrderAmountBeforeDiscount = null,
        ////                        OrderComments = null,
        ////                        OrderQty = 1,
        ////                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        ////                    },
        ////                },
        ////                ShoppingCartTotalAmount = 0,
        ////            };
        ////        }
        ////        foreach (var shoppingCartItemModelTemp in shoppingCartItemModels)
        ////        {
        ////            itemId = shoppingCartItemModelTemp.ItemId.Value;
        ////            orderQty = shoppingCartItemModelTemp.OrderQty.Value;
        ////            orderComments = shoppingCartItemModelTemp.OrderComments;
        ////            shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        ////            if (shoppingCartItemModel == null)
        ////            {
        ////                itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
        ////                shoppingCartModel.ShoppingCartItems.Add
        ////                (
        ////                    shoppingCartItemModel = new ShoppingCartItemModel
        ////                    {
        ////                        DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductHeight").ItemSpecUnitValue),
        ////                        HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductHeight").ItemSpecValue),
        ////                        HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay,
        ////                        ItemDesc = itemModel.ItemDesc,
        ////                        ItemDiscountPercent = null,
        ////                        ItemId = itemModel.ItemId,
        ////                        ItemRate = itemModel.ItemRate,
        ////                        ItemRateBeforeDiscount = itemModel.ItemRate,
        ////                        ItemShortDesc = itemModel.ItemShortDesc,
        ////                        LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductLength").ItemSpecValue),
        ////                        OrderAmount = orderQty * itemModel.ItemRate,
        ////                        OrderAmountBeforeDiscount = orderQty * itemModel.ItemRate,
        ////                        OrderDetailTypeId = OrderDetailTypeEnum.Item,
        ////                        OrderQty = orderQty,
        ////                        ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay,
        ////                        WeightCalcValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "CalcProductWeight").ItemSpecValue),
        ////                        WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductWeight").ItemSpecUnitValue),
        ////                        WeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductWeight").ItemSpecValue),
        ////                        WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductWidth").ItemSpecValue),
        ////                        ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductOrVolumetricWeight").ItemSpecValue),
        ////                        ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.AttribName == "ProductOrVolumetricWeight").ItemSpecUnitValue),
        ////                    }
        ////                );
        ////            }
        ////            else
        ////            {
        ////                shoppingCartItemModel.OrderQty += orderQty;
        ////            }
        ////            shoppingCartItemModel.OrderComments = orderComments;
        ////            shoppingCartItemModel.OrderAmount = orderQty * shoppingCartItemModel.ItemRate;
        ////            shoppingCartItemModel.OrderAmountBeforeDiscount = orderQty * shoppingCartItemModel.ItemRate;
        ////            shoppingCartItemModel.VolumeValue = orderQty * shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        ////            shoppingCartItemModel.WeightCalcValue = orderQty * shoppingCartItemModel.WeightCalcValue;
        ////            shoppingCartItemModel.WeightValue = orderQty * shoppingCartItemModel.WeightValue;
        ////            shoppingCartItemModel.ProductOrVolumetricWeight = orderQty * shoppingCartItemModel.ProductOrVolumetricWeight;
        ////        }
        ////        UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
        ////        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        ////        return shoppingCartModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        //////GET CategoryListView
        ////public CategoryListModel CategoryListView(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        //int x = 1, y = 0, z = x / y;
        ////        ApplicationDataContext.OpenSqlConnection();
        ////        CategoryListModel categoryListModel = new CategoryListModel
        ////        {
        ////            CategoryModels = ApplicationDataContext.GetCategorys(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        ////        };
        ////        //Remove the first two entries - the parent for categories and Featured Items
        ////        categoryListModel.CategoryModels.RemoveAt(0);
        ////        categoryListModel.CategoryModels.RemoveAt(0);
        ////        return categoryListModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        ////        throw;
        ////    }
        ////    finally
        ////    {
        ////        try
        ////        {
        ////            ApplicationDataContext.CloseSqlConnection();
        ////        }
        ////        catch
        ////        {

        ////        }
        ////    }
        ////}
        //////GET Checkout
        ////public CheckoutModel Checkout(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    ArchLibBL archLibBL = new ArchLibBL();
        ////    try
        ////    {
        ////        CheckoutModel checkoutModel = new CheckoutModel
        ////        {
        ////            ContactUsModel = new ContactUsModel(),
        ////            LoginUserProfGuestModel = new LoginUserProfGuestModel
        ////            {
        ////                ResponseObjectModel = new ResponseObjectModel
        ////                {
        ////                    ResponseTypeId = ResponseTypeEnum.Success,
        ////                },
        ////            },
        ////            LoginUserProfModel = new LoginUserProfModel
        ////            {
        ////                ResponseObjectModel = new ResponseObjectModel
        ////                {
        ////                    ResponseTypeId = ResponseTypeEnum.Success,
        ////                },
        ////            },
        ////            RegisterUserProfModel = new RegisterUserProfModel
        ////            {
        ////                RegisterTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry")),
        ////            },
        ////            ResetPasswordModel = new ResetPasswordModel(),
        ////            ShoppingCartModel = CheckoutValidate(httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
        ////        };
        ////        if (checkoutModel.ShoppingCartModel != null)
        ////        {
        ////            //checkoutModel.ShoppingCartModel.Checkout = false;
        ////            List<string> numberSessions = new List<string>
        ////            {
        ////                "CaptchaNumberCheckoutGuest0",
        ////                "CaptchaNumberCheckoutGuest1",
        ////                "CaptchaNumberLogin0",
        ////                "CaptchaNumberLogin1",
        ////                "CaptchaNumberRegister0",
        ////                "CaptchaNumberRegister1",
        ////                "CaptchaNumberResetPassword0",
        ////                "CaptchaNumberResetPassword1",
        ////                "CaptchaNumberContactUs0",
        ////                "CaptchaNumberContactUs1",
        ////            };
        ////            archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, numberSessions);
        ////            checkoutModel.ContactUsModel.CaptchaAnswerContactUs = null;
        ////            checkoutModel.ContactUsModel.CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString();
        ////            checkoutModel.ContactUsModel.CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString();
        ////            checkoutModel.LoginUserProfGuestModel.CaptchaAnswerLoginUserProfGuest = null;
        ////            checkoutModel.LoginUserProfGuestModel.CaptchaNumberLoginUserProfGuest0 = httpSessionStateBase["CaptchaNumberCheckoutGuest0"].ToString();
        ////            checkoutModel.LoginUserProfGuestModel.CaptchaNumberLoginUserProfGuest1 = httpSessionStateBase["CaptchaNumberCheckoutGuest1"].ToString();
        ////            checkoutModel.LoginUserProfModel.CaptchaAnswerLogin = null;
        ////            checkoutModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
        ////            checkoutModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
        ////            checkoutModel.RegisterUserProfModel.CaptchaAnswerRegister = null;
        ////            checkoutModel.RegisterUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
        ////            checkoutModel.RegisterUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
        ////            checkoutModel.ResetPasswordModel.CaptchaAnswerResetPassword = null;
        ////            checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
        ////            checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
        ////        }
        ////        return checkoutModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        ////GET CheckoutValidate
        ////public ShoppingCartModel CheckoutValidate(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    //int x = 1, y = 0, z = x / y;
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        ShoppingCartModel shoppingCartModel;
        ////        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        if (shoppingCartModel == null)
        ////        {
        ////            throw new Exception("Shopping Cart is Empty");
        ////        }
        ////        else
        ////        {
        ////            if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
        ////            {
        ////                ;
        ////            }
        ////            else
        ////            {
        ////                throw new Exception("Shopping Cart is Empty");
        ////            }
        ////        }
        ////        return shoppingCartModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        //////GET DeliveryInfoBackup
        ////public DeliveryInfoModel DeliveryInfoBackup(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    //int x = 1, y = 0, z = x / y;
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        ShoppingCartModel shoppingCartModel;
        ////        DeliveryInfoModel deliveryInfoModel;
        ////        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        //shoppingCartModel.Checkout = false;
        ////        httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
        ////        if (shoppingCartModel == null)
        ////        {
        ////            deliveryInfoModel = null;
        ////        }
        ////        else
        ////        {
        ////            if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
        ////            {
        ////                deliveryInfoModel = new DeliveryInfoModel
        ////                {
        ////                    DeliveryInfoDataModel = new DeliveryInfoDataModel
        ////                    {
        ////                        AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        ////                        DeliveryAddressModel = new DemogInfoAddressModel
        ////                        {
        ////                            BuildingTypeId = BuildingTypeEnum._,
        ////                            DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,//long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
        ////                        },
        ////                        PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        ////                    },
        ////                    ShoppingCartModel = shoppingCartModel,
        ////                };
        ////            }
        ////            else
        ////            {
        ////                deliveryInfoModel = null;
        ////            }
        ////        }
        ////        return deliveryInfoModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        //////POST Delivery Info
        ////public PaymentInfoModel DeliveryInfoBackup(DeliveryInfoDataModel deliveryInfoDataModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        ApplicationDataContext.OpenSqlConnection();
        ////        SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        ////        ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        shoppingCartModel.ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        ////        {
        ////            new ShoppingCartItemModel
        ////            {
        ////                ItemDesc = null,
        ////                ItemId = null,
        ////                ItemRate = null,
        ////                ItemRateBeforeDiscount = null,
        ////                ItemShortDesc = null,
        ////                OrderAmount = null,
        ////                OrderAmountBeforeDiscount = null,
        ////                OrderComments = null,
        ////                OrderQty = 1,
        ////                OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        ////            }
        ////        };
        ////        deliveryInfoDataModel.AlternateTelephoneTelephoneCode = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == deliveryInfoDataModel.AlternateTelephoneDemogInfoCountryId).TelephoneCode;
        ////        deliveryInfoDataModel.PrimaryTelephoneTelephoneCode = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == deliveryInfoDataModel.PrimaryTelephoneDemogInfoCountryId).TelephoneCode;
        ////        ApplyDiscounts(shoppingCartModel, sessionObjectModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        long personId = sessionObjectModel.PersonId;
        ////        AddAdditionalCharges(shoppingCartModel, deliveryInfoDataModel, sessionObjectModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        AddTotals(shoppingCartModel.ShoppingCartSummaryItems, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        List<CodeDataModel> codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId;
        ////        var deliveryMethodId = (int)deliveryInfoDataModel.DeliveryMethodId;
        ////        CodeDataModel codeDataModel = codeDataModels.First(x => x.CodeDataNameId == deliveryMethodId);
        ////        var deliveryMethodName = codeDataModel.CodeDataDesc0;
        ////        codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId;
        ////        var paymentModeId = (int)deliveryInfoDataModel.PaymentModeId;
        ////        codeDataModel = codeDataModels.First(x => x.CodeDataNameId == paymentModeId);
        ////        var paymentModeName = codeDataModel.CodeDataDesc0;
        ////        deliveryInfoDataModel.DeliveryMethodName = deliveryMethodName;
        ////        deliveryInfoDataModel.PaymentModeName = paymentModeName;
        ////        if (deliveryInfoDataModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
        ////        {
        ////            deliveryInfoDataModel.CreateDeliveryAddress = false;
        ////            if (deliveryInfoDataModel.PaymentModeId == PaymentModeEnum.COD)
        ////            {
        ////                deliveryInfoDataModel.PaymentModeDesc = "Pay at the store at time of pickup";
        ////                deliveryInfoDataModel.PaymentModeDesc = codeDataModel.CodeDataDesc3;
        ////            }
        ////            else
        ////            {
        ////                deliveryInfoDataModel.PaymentModeDesc = "You will be redirected to a page outside of our domain";
        ////                deliveryInfoDataModel.PaymentModeDesc = codeDataModel.CodeDataDesc2;
        ////            }
        ////        }
        ////        else
        ////        {
        ////            deliveryInfoDataModel.CreateDeliveryAddress = true;
        ////            if (deliveryInfoDataModel.PaymentModeId == PaymentModeEnum.COD)
        ////            {
        ////                deliveryInfoDataModel.PaymentModeDesc = "Our staff will call you and cofirm prior to shipping";
        ////            }
        ////            else
        ////            {
        ////                deliveryInfoDataModel.PaymentModeDesc = "You will be redirected to a page outside of our domain";
        ////            }
        ////            deliveryInfoDataModel.PaymentModeDesc = codeDataModel.CodeDataDesc2;
        ////        }
        ////        httpSessionStateBase["DeliveryInfoDataModel"] = deliveryInfoDataModel;
        ////        PaymentInfoModel paymentInfoModel = new PaymentInfoModel
        ////        {
        ////            DeliveryInfoDataModel = deliveryInfoDataModel,
        ////            GiftCertPaymentModel = new GiftCertPaymentModel(),
        ////            PaymentSummaryDataModel = new PaymentSummaryDataModel
        ////            {
        ////                EmailAddress = sessionObjectModel.EmailAddress,
        ////                UserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
        ////            },
        ////            ShoppingCartModel = shoppingCartModel,
        ////        };
        ////        return paymentInfoModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        ////        throw;
        ////    }
        ////    finally
        ////    {
        ////        ApplicationDataContext.CloseSqlConnection();
        ////    }
        ////}
        //// GET: DeliveryInfo
        //public void DeliveryInfo(ref PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    //int x = 1, y = 0, z = x / y;
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfoModel = paymentInfoModel ?? new PaymentInfo1Model();
        //        if (paymentInfoModel.ShoppingCartModel == null)
        //        {
        //            paymentInfoModel = new PaymentInfo1Model
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>
        //                    {
        //                        new KeyValuePair<string, List<string>>
        //                        (
        //                            "",
        //                            new List<string>
        //                            {
        //                                "Invalid shopping cart (Null)",
        //                            }
        //                        ),
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Error,
        //                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            if (paymentInfoModel.ShoppingCartModel.ShoppingCartItems.Count > 0 && paymentInfoModel.ShoppingCartModel.ShoppingCartTotalAmount > 0)
        //            {
        //                paymentInfoModel.ShoppingCartModel.Checkout = false;
        //                paymentInfoModel.ShoppingCartModel.Checkout = false;
        //                paymentInfoModel.CouponPaymentModel = new CouponPaymentModel
        //                {
        //                    CouponNumber = "",
        //                };
        //                paymentInfoModel.DeliveryAddressModel = new DemogInfoAddressModel
        //                {
        //                    BuildingTypeId = BuildingTypeEnum._,
        //                    BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
        //                    DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //                    DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
        //                    DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId],
        //                };
        //                paymentInfoModel.DeliveryDataModel = new DeliveryDataModel
        //                {
        //                    AlternateTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //                    PrimaryTelephoneDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //                };
        //                paymentInfoModel.GiftCertPaymentModel = new GiftCertPaymentModel
        //                {
        //                };
        //                paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseMessages = new List<string>(),
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                };
        //                retailSlnBL.BuildDeliveryInfoLookup(paymentInfoModel, sessionObjectModel, apiFlag, webFlag, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            else
        //            {
        //                paymentInfoModel = new PaymentInfo1Model
        //                {
        //                    ResponseObjectModel = new ResponseObjectModel
        //                    {
        //                        PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>
        //                        {
        //                            new KeyValuePair<string, List<string>>
        //                            (
        //                                "",
        //                                new List<string>
        //                                {
        //                                    "Invalid shopping cart - no items",
        //                                }
        //                            ),
        //                        },
        //                        ResponseTypeId = ResponseTypeEnum.Error,
        //                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //                    },
        //                };
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
        ////POST Delivery Info
        //public void DeliveryInfo(PaymentInfo1Model paymentInfoModel, SessionObjectModel sessionObjectModel, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ResponseMessages = new List<string>(),
        //            ResponseTypeId = ResponseTypeEnum.Success,
        //            PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>(),
        //            ValidationSummaryMessage = "",
        //        };
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ResponseMessages = new List<string>(),
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //            PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>()
        //            {
        //                new KeyValuePair<string, List<string>>("", new List<string> { "Error while processing delivery info" }),
        //            },
        //            ValidationSummaryMessage = "",
        //        };
        //    }
        //    return;
        //}
        ////POST Delivery Info
        //public void DeliveryInfo(PaymentInfo1Model paymentInfoModel, ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        BuildDeliveryInfoLookup(paymentInfoModel, shoppingCartModel, sessionObjectModel, apiFlag, webFlag, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        UpdateDeliveryAddressInfo(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    #region
        //    //try
        //    //{
        //    //    ApplicationDataContext.OpenSqlConnection();
        //    //    SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        //    //    ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        //    //    shoppingCartModel.ShoppingCartSummaryItems = new List<ShoppingCartItemModel>
        //    //    {
        //    //        new ShoppingCartItemModel
        //    //        {
        //    //            ItemDesc = null,
        //    //            ItemId = null,
        //    //            ItemRate = null,
        //    //            ItemRateBeforeDiscount = null,
        //    //            ItemShortDesc = null,
        //    //            OrderAmount = null,
        //    //            OrderAmountBeforeDiscount = null,
        //    //            OrderComments = null,
        //    //            OrderQty = 1,
        //    //            OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
        //    //        }
        //    //    };
        //    //    paymentInfoModel.AlternateTelephoneTelephoneCode = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.AlternateTelephoneDemogInfoCountryId).TelephoneCode;
        //    //    paymentInfoModel.PrimaryTelephoneTelephoneCode = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.PrimaryTelephoneDemogInfoCountryId).TelephoneCode;
        //    //    ApplyDiscounts(shoppingCartModel, sessionObjectModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    //    UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    //    long personId = sessionObjectModel.PersonId;
        //    //    AddAdditionalCharges(shoppingCartModel, paymentInfoModel, sessionObjectModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    //    AddTotals(shoppingCartModel.ShoppingCartSummaryItems, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    //    List<CodeDataModel> codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId;
        //    //    var deliveryMethodId = (int)paymentInfoModel.DeliveryMethodId;
        //    //    CodeDataModel codeDataModel = codeDataModels.First(x => x.CodeDataNameId == deliveryMethodId);
        //    //    var deliveryMethodName = codeDataModel.CodeDataDesc0;
        //    //    codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId;
        //    //    var paymentModeId = (int)paymentInfoModel.PaymentModeId;
        //    //    codeDataModel = codeDataModels.First(x => x.CodeDataNameId == paymentModeId);
        //    //    var paymentModeName = codeDataModel.CodeDataDesc0;
        //    //    paymentInfoModel.DeliveryMethodName = deliveryMethodName;
        //    //    paymentInfoModel.PaymentModeName = paymentModeName;
        //    //    if (paymentInfoModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
        //    //    {
        //    //        paymentInfoModel.CreateDeliveryAddress = false;
        //    //        if (paymentInfoModel.PaymentModeId == PaymentModeEnum.COD)
        //    //        {
        //    //            paymentInfoModel.PaymentModeDesc = "Pay at the store at time of pickup";
        //    //            paymentInfoModel.PaymentModeDesc = codeDataModel.CodeDataDesc3;
        //    //        }
        //    //        else
        //    //        {
        //    //            paymentInfoModel.PaymentModeDesc = "You will be redirected to a page outside of our domain";
        //    //            paymentInfoModel.PaymentModeDesc = codeDataModel.CodeDataDesc2;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        paymentInfoModel.CreateDeliveryAddress = true;
        //    //        if (paymentInfoModel.PaymentModeId == PaymentModeEnum.COD)
        //    //        {
        //    //            paymentInfoModel.PaymentModeDesc = "Our staff will call you and cofirm prior to shipping";
        //    //        }
        //    //        else
        //    //        {
        //    //            paymentInfoModel.PaymentModeDesc = "You will be redirected to a page outside of our domain";
        //    //        }
        //    //        paymentInfoModel.PaymentModeDesc = codeDataModel.CodeDataDesc2;
        //    //    }
        //    //    httpSessionStateBase["DeliveryInfoDataModel"] = paymentInfoModel;
        //    //    PaymentInfoModel paymentInfoModel = new PaymentInfoModel
        //    //    {
        //    //        DeliveryInfoDataModel = paymentInfoModel,
        //    //        GiftCertPaymentModel = new GiftCertPaymentModel(),
        //    //        PaymentSummaryDataModel = new PaymentSummaryDataModel
        //    //        {
        //    //            EmailAddress = sessionObjectModel.EmailAddress,
        //    //            UserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
        //    //        },
        //    //        ShoppingCartModel = shoppingCartModel,
        //    //    };
        //    //    return paymentInfoModel;
        //    //}
        //    //catch (Exception exception)
        //    //{
        //    //    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //    //    throw;
        //    //}
        //    //finally
        //    //{
        //    //    ApplicationDataContext.CloseSqlConnection();
        //    //}
        //    #endregion
        //}
        ////GET GiftCert
        //public GiftCertModel GiftCert(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
        //        GiftCertModel giftCertModel = new GiftCertModel
        //        {
        //            CaptchaNumber0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString(),
        //            CaptchaNumber1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString(),
        //        };
        //        return giftCertModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        ////POST GiftCert
        //public void GiftCert(ref GiftCertModel giftCertModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (archLibBL.ValidateCaptcha(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1", giftCertModel.CaptchaAnswer))
        //        {
        //        }
        //        else
        //        {
        //            modelStateDictionary.AddModelError("CaptchaAnswer", "Incorrect captcha answer");
        //        }
        //        if (modelStateDictionary.IsValid)
        //        {
        //            LoginUserProfModel loginUserProfModel = new LoginUserProfModel
        //            {
        //                LoginEmailAddress = giftCertModel.SenderEmailAddress,
        //                LoginPassword = giftCertModel.SenderPassword,
        //            };
        //            SessionObjectModel sessionObjectModel = archLibBL.LoginUserProfValidate(ref loginUserProfModel, httpSessionStateBase, modelStateDictionary, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (modelStateDictionary.IsValid)
        //            {
        //                string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //                CreditCardDataModel creditCardDataModel = new CreditCardDataModel
        //                {
        //                    CreditCardAmount = giftCertModel.GiftCertAmount.ToString(),
        //                    CreditCardExpMM = giftCertModel.CardExpiryMM,
        //                    CreditCardExpYear = giftCertModel.CardExpiryYYYY,
        //                    CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
        //                    CreditCardNumber = giftCertModel.CreditCardNumber,
        //                    CreditCardProcessor = creditCardProcessor,
        //                    CreditCardSecCode = giftCertModel.CVV,
        //                    CreditCardTranType = "PAYMENT",
        //                    CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
        //                    NameAsOnCard = giftCertModel.CardHolderName,
        //                };
        //                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
        //                giftCertModel.CreditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out object creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                giftCertModel.CreditCardLast4 = creditCardDataModel.CreditCardNumberLast4;
        //                giftCertModel.CreditCardProcessMessage = processMessage;
        //                RegisterUserProfModel registerUserProfModel = new RegisterUserProfModel
        //                {
        //                    RegisterEmailAddress = giftCertModel.RecipientEmailAddress,
        //                };
        //                string loginPassword = archLibBL.GenerateRandomKey(9);
        //                bool userProfRegistered = RegisterUserProf(registerUserProfModel, loginPassword, giftCertModel.TelephoneNumber, "", "", ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId, out AspNetUserModel aspNetUserModel);
        //                giftCertModel.PersonId = sessionObjectModel.PersonId;
        //                giftCertModel.RecipientPersonId = (long)aspNetUserModel.PersonModel.PersonId;
        //                giftCertModel.GiftCertKey = archLibBL.GenerateRandomKey(8);
        //                giftCertModel.CreditCardDataId = creditCardDataModel.CreditCardDataId;
        //                giftCertModel.GiftCertBalanceAmount = (float)giftCertModel.GiftCertAmount;
        //                giftCertModel.RecipientEmailAddressRegistered = userProfRegistered ? $"Registered with password {loginPassword}" : "Already registered";
        //                ApplicationDataContext.CreateGiftCert(giftCertModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                string giftCertDirectory = Utilities.GetServerMapPath("~/ClientSpecific/" + ArchLibCache.ClientId + "_" + ArchLibCache.ClientName + "/Documents/GiftCertificate/");
        //                string inputFullFileName = giftCertDirectory + @"\Templates\" + giftCertModel.SelectedTemplateName;
        //                giftCertModel.GiftCertImageFileName = giftCertDirectory + @"\" + giftCertModel.GiftCertNumber + ".jpg";
        //                CreateGiftCertImage(inputFullFileName, giftCertModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", giftCertModel);
        //                string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_GiftCertEmailSubject", giftCertModel);
        //                string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_GiftCertEmailBody", giftCertModel);
        //                emailBodyHtml += signatureHtml;
        //                archLibBL.SendEmail(giftCertModel.SenderEmailAddress, emailSubjectText, emailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_GiftCertRecipientEmailBody", giftCertModel);
        //                emailBodyHtml += signatureHtml;
        //                archLibBL.SendEmail(giftCertModel.SenderEmailAddress, emailSubjectText, emailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //            }
        //            else
        //            {
        //            }
        //        }
        //        else
        //        {
        //        }
        //        if (modelStateDictionary.IsValid)
        //        {
        //            giftCertModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Your purchase of gift certificate is successful",
        //                    "Email is sent both to sender and recipient",
        //                    "If you do not find it in your inbox",
        //                    "Please check your spam and mark the sender as safe",
        //                    "Should have any questions please feel free to contact us",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        else
        //        {
        //            giftCertModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //                ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
        //            };
        //            archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
        //            giftCertModel.CaptchaAnswer = null;
        //            giftCertModel.CaptchaNumber0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
        //            giftCertModel.CaptchaNumber1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
        //            archLibBL.MergeModelStateErrorMessages(modelStateDictionary);
        //        }
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
        ////GET GiftCertBalance
        //public void GiftCertBalance(string giftCertNumber, string giftCertKey, out string errorMessage, out float? giftCertBalanceAmount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        if (long.TryParse(giftCertNumber, out long temp))
        //        {
        //            ApplicationDataContext.OpenSqlConnection();
        //            GiftCertModel giftCertModel = ApplicationDataContext.GetGiftCert(giftCertNumber, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (giftCertModel.GiftCertKey == giftCertKey)
        //            {
        //                errorMessage = "";
        //                giftCertBalanceAmount = giftCertModel.GiftCertBalanceAmount;
        //            }
        //            else
        //            {
        //                errorMessage = "Invalid Gift Cert# / Key";
        //                giftCertBalanceAmount = null;
        //            }
        //        }
        //        else
        //        {
        //            errorMessage = "Invalid Gift Cert#";
        //            giftCertBalanceAmount = null;
        //        }
        //        return;
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
        ////GET ItemListView
        //public ItemListModel ItemListView(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        ItemListModel itemListModel = new ItemListModel
        //        {
        //            ItemModels = ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        return itemListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////
        //////GET OrderReceipt
        ////public OrderReceiptModel OrderReceipt(PaymentDataModel paymentDataModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    ArchLibBL archLibBL = new ArchLibBL();
        ////    try
        ////    {
        ////        DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
        ////        ShoppingCartModel shoppingCartModel;
        ////        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        //shoppingCartModel.Checkout = false;
        ////        OrderReceiptModel orderReceiptModel = new OrderReceiptModel
        ////        {
        ////            DeliveryInfoDataModel = deliveryInfoDataModel,
        ////            PaymentDataModel = paymentDataModel,
        ////            ShoppingCartModel = shoppingCartModel,
        ////        };
        ////        string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailSubject", orderReceiptModel);
        ////        string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailBody", orderReceiptModel);
        ////        string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", orderReceiptModel);
        ////        emailBodyHtml += signatureHtml;
        ////        PDFUtility pDFUtility = new PDFUtility();
        ////        pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, @"C:\Code\rramaswamy18\RetailSln\Email\" + orderReceiptModel.PaymentDataModel.OrderHeaderId + ".pdf");
        ////        List<string> emailAttachmentFileNames = new List<string>
        ////        {
        ////            @"C:\Code\rramaswamy18\RetailSln\Email\" + orderReceiptModel.PaymentDataModel.OrderHeaderId + ".pdf",
        ////        };
        ////        archLibBL.SendEmail(paymentDataModel.EmailAddress, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        return orderReceiptModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        ////GET Payment
        ////public PaymentModel PaymentInfo(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        ShoppingCartModel shoppingCartModel;
        ////        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        ////        //shoppingCartModel.Checkout = false;
        ////        httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
        ////        DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
        ////        SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        ////        ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)sessionObjectModel.ApplSessionObjectModel;
        ////        if (shoppingCartModel == null)
        ////        {
        ////            throw new Exception("Shopping Cart is Empty");
        ////        }
        ////        else
        ////        {
        ////            if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
        ////            {
        ////                PaymentModel paymentModel = new PaymentModel
        ////                {
        ////                    DeliveryInfoDataModel = deliveryInfoDataModel,
        ////                    PaymentDataModel = new PaymentDataModel
        ////                    {
        ////                        CorpAcctId = applSessionObjectModel.CorpAcctModel.CorpAcctId,
        ////                        EmailAddress = sessionObjectModel.EmailAddress,
        ////                        PaymentModeId = deliveryInfoDataModel.PaymentModeId,
        ////                        UserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
        ////                        OrderAmount = shoppingCartModel.ShoppingCartSummaryItems[shoppingCartModel.ShoppingCartSummaryItems.Count - 1].OrderAmount.Value,
        ////                    },
        ////                    ShoppingCartModel = shoppingCartModel,
        ////                };
        ////                return paymentModel;
        ////            }
        ////            else
        ////            {
        ////                throw new Exception("Shopping Cart is Empty");
        ////            }
        ////        }
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        ////        throw;
        ////    }
        ////}
        //#endregion
        ////POST PaymentInfo1
        //public string PaymentInfo1(PaymentInfoModel paymentInfoModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
        //        DeliveryInfoDataModel deliveryInfoDataModel = paymentInfoModel.DeliveryInfoDataModel;
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        //        if (shoppingCartModel == null)
        //        {
        //            throw new Exception("Shopping Cart is Empty");
        //        }
        //        else
        //        {
        //            if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
        //            {
        //                CreateOrder(deliveryInfoDataModel, shoppingCartModel, sessionObjectModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                paymentInfoModel.PaymentSummaryDataModel = new PaymentSummaryDataModel
        //                {
        //                    BalanceDue = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue).OrderAmount,
        //                    CardHolderName = "",
        //                    CreditCardNumberLast4 = "",
        //                    CreditCardPaymentAmount = 0,
        //                    CreditCardProcessMessage = "Not Applicable",
        //                    CreditCardProcessStatus = "",
        //                    EmailAddress = sessionObjectModel.EmailAddress,
        //                    GiftCertNumberLast4 = paymentInfoModel.GiftCertPaymentModel.GiftCertNumberLast4,
        //                    GiftCertPaymentAmount = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByGiftCert).OrderAmount ?? 0,
        //                    OrderHeaderId = deliveryInfoDataModel.OrderHeaderId,
        //                    UserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
        //                };
        //                string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailSubject", paymentInfoModel);
        //                string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailBody", paymentInfoModel);
        //                string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", paymentInfoModel);
        //                emailBodyHtml += signatureHtml;
        //                PDFUtility pDFUtility = new PDFUtility();
        //                string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //                pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, emailDirectoryName + paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId + ".pdf");
        //                List<string> emailAttachmentFileNames = new List<string>
        //                {
        //                    emailDirectoryName + paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId + ".pdf",
        //                };
        //                archLibBL.SendEmail(paymentInfoModel.PaymentSummaryDataModel.EmailAddress, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                return emailBodyHtml;
        //            }
        //            else
        //            {
        //                throw new Exception("Shopping Cart is Empty");
        //            }
        //        }
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
        ////POST PaymentInfo2
        //public RazorPayResponse PaymentInfo2(PaymentInfoModel paymentInfoModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //Razorpay
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    object creditCardResponseObject = null;
        //    try
        //    {
        //        ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
        //        DeliveryInfoDataModel deliveryInfoDataModel = paymentInfoModel.DeliveryInfoDataModel;
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        //        paymentInfoModel.PaymentSummaryDataModel = new PaymentSummaryDataModel();
        //        if (shoppingCartModel == null)
        //        {
        //            modelStateDictionary.AddModelError("", "Shopping Cart is Empty");
        //        }
        //        else
        //        {
        //            if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
        //            {
        //                string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //                CreditCardDataModel creditCardDataModel = new CreditCardDataModel
        //                {
        //                    CreditCardAmount = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue).OrderAmount.Value.ToString("0.00"),
        //                    CreditCardExpMM = null,
        //                    CreditCardExpYear = null,
        //                    CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
        //                    CreditCardNumber = null,
        //                    CreditCardProcessor = creditCardProcessor,
        //                    CreditCardSecCode = null,
        //                    CreditCardTranType = "PAYMENT",
        //                    CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
        //                    EmailAddress = sessionObjectModel.EmailAddress,
        //                    NameAsOnCard = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
        //                    TelephoneNumber = sessionObjectModel.PhoneNumber,
        //                };
        //                ApplicationDataContext.OpenSqlConnection();
        //                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
        //                var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                if (!creditCardProcessStatus)
        //                {
        //                    modelStateDictionary.AddModelError("", processMessage);
        //                }
        //                else
        //                {
        //                    paymentInfoModel.PaymentSummaryDataModel.BalanceDue = 0;
        //                    paymentInfoModel.PaymentSummaryDataModel.CreditCardDataId = creditCardDataModel.CreditCardDataId;
        //                    paymentInfoModel.PaymentSummaryDataModel.CreditCardPaymentAmount = float.Parse(creditCardDataModel.CreditCardAmount);
        //                    paymentInfoModel.PaymentSummaryDataModel.EmailAddress = creditCardDataModel.EmailAddress;
        //                    paymentInfoModel.PaymentSummaryDataModel.UserFullName = creditCardDataModel.NameAsOnCard;
        //                    httpSessionStateBase["PaymentInfoModel"] = paymentInfoModel;
        //                }
        //            }
        //            else
        //            {
        //                modelStateDictionary.AddModelError("", "Shopping Cart is Empty");
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        modelStateDictionary.AddModelError("", "Error occurred during processing - Payment Info2");
        //    }
        //    finally
        //    {
        //        ApplicationDataContext.CloseSqlConnection();
        //    }
        //    return (RazorPayResponse)creditCardResponseObject;
        //}
        ////POST PaymentInfo3
        //public string PaymentInfo3(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
        //    CreditCardRazorPayBL razorPayIntegration = new CreditCardRazorPayBL();
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (razorPayIntegration.CheckPaymentSuccess(razorpay_payment_id, razorpay_order_id, razorpay_signature))
        //        {
        //            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)httpSessionStateBase["PaymentInfoModel"];
        //            ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        //            DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
        //            SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        //            creditCardServiceBL.UpdCreditCardData(paymentInfoModel.PaymentSummaryDataModel.CreditCardDataId, razorpay_payment_id, razorpay_order_id, razorpay_signature, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            CreateOrder(deliveryInfoDataModel, shoppingCartModel, sessionObjectModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId = deliveryInfoDataModel.OrderHeaderId;
        //            paymentInfoModel.PaymentSummaryDataModel.CreditCardProcessStatus = "Success";
        //            paymentInfoModel.PaymentSummaryDataModel.CreditCardProcessMessage = razorpay_order_id + "<br />" + razorpay_payment_id;
        //            ApplicationDataContext.CreatePayment(paymentInfoModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            paymentInfoModel.PaymentSummaryDataModel.GiftCertPaymentAmount = 0;
        //            string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailSubject", paymentInfoModel);
        //            string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailBody", paymentInfoModel);
        //            string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateEmail", paymentInfoModel);
        //            emailBodyHtml += signatureHtml;
        //            PDFUtility pDFUtility = new PDFUtility();
        //            string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //            pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, emailDirectoryName + paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId + ".pdf");
        //            List<string> emailAttachmentFileNames = new List<string>
        //            {
        //                emailDirectoryName + paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId + ".pdf",
        //            };
        //            archLibBL.SendEmail(paymentInfoModel.PaymentSummaryDataModel.EmailAddress, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            return emailBodyHtml;
        //        }
        //        else
        //        {
        //            throw new Exception("Error while validating Payment Success");
        //        }
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
        ////POST PaymentInfo5
        //public string PaymentInfo5(CreditCardProcessModel creditCardProcessModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    string emailBodyHtml;
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        string creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //        CreditCardDataModel creditCardDataModel = new CreditCardDataModel
        //        {
        //            CreditCardAmount = creditCardProcessModel.CreditCardPaymentAmount.Value.ToString("0.00"),
        //            CreditCardExpMM = creditCardProcessModel.CardExpiryMM,
        //            CreditCardExpYear = creditCardProcessModel.CardExpiryYYYY,
        //            CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
        //            CreditCardNumber = creditCardProcessModel.CreditCardNumber,
        //            CreditCardProcessor = creditCardProcessor,
        //            CreditCardSecCode = creditCardProcessModel.CVV,
        //            CreditCardTranType = "PAYMENT",
        //            CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
        //            NameAsOnCard = creditCardProcessModel.CardHolderName,
        //        };
        //        CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
        //        var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out object creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        var creditCardLast4 = creditCardDataModel.CreditCardNumberLast4;
        //        var creditCardProcessMessage = processMessage;
        //        if (creditCardProcessStatus)
        //        {
        //            PaymentInfoModel paymentInfoModel = new PaymentInfoModel
        //            {
        //                DeliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"],
        //                GiftCertPaymentModel = new GiftCertPaymentModel(),
        //                PaymentSummaryDataModel = new PaymentSummaryDataModel
        //                {
        //                    BalanceDue = 0,
        //                    CardHolderName = creditCardProcessModel.CardHolderName,
        //                    CreditCardDataId = 999,
        //                    CreditCardNumberLast4 = creditCardProcessModel.CreditCardNumberLast4,
        //                    CreditCardProcessMessage = creditCardDataModel.ProcessMessage,
        //                    CreditCardPaymentAmount = creditCardProcessModel.CreditCardPaymentAmount,
        //                    CreditCardProcessStatus = creditCardDataModel.StatusNameDesc,
        //                    EmailAddress = creditCardProcessModel.EmailAddress,
        //                    GiftCertPaymentAmount = 0,
        //                },
        //                ShoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"],
        //            };
        //            DeliveryInfoDataModel deliveryInfoDataModel = paymentInfoModel.DeliveryInfoDataModel;
        //            ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
        //            SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
        //            ShoppingCartItemModel shoppingCartSummaryItemAmountPaidByCreditCard, shoppingCartSummaryItemTotalInvoiceAmount, shoppingCartSummaryItemBalanceDue, shoppingCartSummaryItemTotalAmountPaid;
        //            shoppingCartSummaryItemTotalInvoiceAmount = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalInvoiceAmount);
        //            shoppingCartSummaryItemTotalAmountPaid = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid);
        //            shoppingCartSummaryItemAmountPaidByCreditCard = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
        //            shoppingCartSummaryItemBalanceDue = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue);
        //            shoppingCartSummaryItemAmountPaidByCreditCard.OrderAmount = shoppingCartSummaryItemTotalInvoiceAmount.OrderAmount;
        //            shoppingCartSummaryItemAmountPaidByCreditCard.OrderAmount = shoppingCartSummaryItemTotalInvoiceAmount.OrderAmount;
        //            shoppingCartSummaryItemTotalAmountPaid.OrderAmount = shoppingCartSummaryItemTotalInvoiceAmount.OrderAmount;
        //            shoppingCartSummaryItemBalanceDue.OrderAmount = 0;
        //            CreateOrder(deliveryInfoDataModel, shoppingCartModel, sessionObjectModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId = deliveryInfoDataModel.OrderHeaderId;
        //            paymentInfoModel.PaymentSummaryDataModel.CreditCardProcessStatus = "Success";
        //            string emailSubjectText = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailSubject", paymentInfoModel);
        //            emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_OrderReceiptEmailBody", paymentInfoModel);
        //            string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplateOrderEmail", paymentInfoModel);
        //            emailBodyHtml += signatureHtml;
        //            PDFUtility pDFUtility = new PDFUtility();
        //            string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //            pDFUtility.GeneratePDFFromHtmlString(emailBodyHtml, emailDirectoryName + paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId + ".pdf");
        //            List<string> emailAttachmentFileNames = new List<string>
        //            {
        //                emailDirectoryName + paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId + ".pdf",
        //            };
        //            archLibBL.SendEmail(paymentInfoModel.PaymentSummaryDataModel.EmailAddress, emailSubjectText, emailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        else
        //        {
        //            modelStateDictionary.AddModelError("", "Error while processing credit card");
        //            modelStateDictionary.AddModelError("", creditCardProcessMessage);
        //            emailBodyHtml = "";
        //        }
        //        return emailBodyHtml;
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
        //public void RemoveFromCart(PaymentInfo1Model paymentInfoModel, int index, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ShoppingCartModel shoppingCartModel = paymentInfoModel.ShoppingCartModel;
        //        if (shoppingCartModel == null)
        //        {
        //            throw new Exception("Shopping Cart is Empty");
        //        }
        //        else
        //        {
        //            if (index > -1 && index < shoppingCartModel.ShoppingCartItems.Count)
        //            {
        //                shoppingCartModel.ShoppingCartItems.RemoveAt(index);
        //                UpdateShoppingCart(shoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                //httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
        //                return;
        //            }
        //            else
        //            {
        //                throw new Exception("Invalid index in remove from cart");
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        ////GET ShoppingCartComments
        //public void ShoppingCartComments(int index, string orderComments, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ShoppingCartModel shoppingCartModel;
        //        shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
        //        if (shoppingCartModel == null)
        //        {
        //            throw new Exception("Shopping Cart is Empty");
        //        }
        //        else
        //        {
        //            if (index > -1 && index < shoppingCartModel.ShoppingCartItems.Count)
        //            {
        //                shoppingCartModel.ShoppingCartItems[index].OrderComments = orderComments;
        //                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
        //                return;
        //            }
        //            else
        //            {
        //                throw new Exception("Invalid index shopping cart comments");
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }

        //}
        //private void AddAdditionalCharges(ShoppingCartModel shoppingCartModel, DeliveryInfoDataModel deliveryInfoDataModel, SessionObjectModel sessionObjectModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    List<ShoppingCartItemModel> shoppingCartSummaryItems = shoppingCartModel.ShoppingCartSummaryItems;
        //    ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)sessionObjectModel.ApplSessionObjectModel;
        //    float totalOrderAmount = shoppingCartModel.ShoppingCartTotalAmount.Value, discountAmount = 0;
        //    var corpAcctModel = RetailSlnCache.CorpAcctModels.First(x => x.CorpAcctId == applSessionObjectModel.CorpAcctModel.CorpAcctId);
        //    if (corpAcctModel != null && corpAcctModel.CorpAcctId > 0)
        //    {
        //        foreach (var discountDtlModel in corpAcctModel.DiscountDtlModels)
        //        {
        //            discountAmount += totalOrderAmount * discountDtlModel.CorpAcctDiscountPercent / 100f;
        //            shoppingCartSummaryItems.Add
        //            (
        //                new ShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemRate = discountDtlModel.CorpAcctDiscountPercent,
        //                    ItemShortDesc = "Discount (" + discountDtlModel.CorpAcctDiscountPercent + "%)",
        //                    OrderAmount = -1 * totalOrderAmount * discountDtlModel.CorpAcctDiscountPercent / 100f,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.Discount,
        //                }
        //            );
        //        }
        //    }
        //    if (discountAmount > 0)
        //    {
        //        shoppingCartSummaryItems.Add
        //        (
        //            new ShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemShortDesc = "Total Order Amount after Discount",
        //                OrderAmount = totalOrderAmount - discountAmount,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmountAfterDiscount,
        //            }
        //        );
        //    }
        //    totalOrderAmount -= discountAmount;
        //    DemogInfoAddressModel demogInfoAddressModel = deliveryInfoDataModel.DeliveryAddressModel;
        //    var salesTaxListModels = GetSalesTaxListModels(demogInfoAddressModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    float shippingAndHandlingChargesRate = 0, shippingAndHandlingChargesAmount, fuelCharges;
        //    //float /*shippingAndHandlingChargesByWeight = 0, shippingAndHandlingChargesByVolume = 0, */shippingAndHandlingChargesAmount, fuelCharges;
        //    DeliveryChargeModel deliveryChargeModel = null;
        //    if (deliveryInfoDataModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
        //    {
        //        shippingAndHandlingChargesAmount = 0;
        //        fuelCharges = 0;
        //    }
        //    else
        //    {
        //        ShippingService shippingService = new ShippingService();
        //        ShippingInputModel shippingInputModel = new ShippingInputModel
        //        {
        //            DestDemogInfoAddressModel = demogInfoAddressModel,
        //            SrceDemogInfoAddressModel = new DemogInfoAddressModel
        //            {
        //                DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //            },
        //            ShippingInputData = null,
        //        };
        //        deliveryChargeModel = (DeliveryChargeModel)shippingService.GetRate(shippingInputModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
        //        shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * shoppingCartModel.TotalProductOrVolumetricWeightRounded;
        //        fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
        //    }
        //    var salesTaxCaptionIds = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("SalesTaxType", "");
        //    foreach (var salesTaxListModel in salesTaxListModels)
        //    {
        //        var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
        //        if (salesTaxListModel.LineItemLevelName == "SUMMARY")
        //        {
        //            shoppingCartSummaryItems.Add
        //            (
        //                new ShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemRate = totalOrderAmount,
        //                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
        //                    OrderAmount = totalOrderAmount * salesTaxListModel.SalesTaxRate / 100f,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
        //                }
        //            );
        //        }
        //        else
        //        {
        //            shoppingCartModel.ShoppingCartSummaryItems.Add
        //            (
        //                new ShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0,
        //                    OrderAmount = 0,
        //                    OrderComments = null,
        //                }
        //            );
        //            foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
        //            {
        //                var itemSpecValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemSpecModels.ToList().First(x => x.ItemSpecMasterModel.AttribName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemSpecValue;
        //                shoppingCartItem.ShoppingCartItemSummarys.Add
        //                (
        //                    new ShoppingCartItemModel
        //                    {
        //                        ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
        //                        ItemRate = float.Parse(itemSpecValue),
        //                        OrderAmount = float.Parse(itemSpecValue) * shoppingCartItem.OrderAmount / 100f,
        //                    }
        //                );
        //                shoppingCartModel.ShoppingCartSummaryItems[shoppingCartModel.ShoppingCartSummaryItems.Count - 1].OrderAmount += float.Parse(itemSpecValue) * shoppingCartItem.OrderAmount / 100f;
        //            }
        //        }
        //    }
        //    if (deliveryChargeModel != null)
        //    {
        //        shoppingCartSummaryItems.Add
        //        (
        //            new ShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = shippingAndHandlingChargesRate,
        //                ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + " - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
        //                OrderAmount = shippingAndHandlingChargesAmount + fuelCharges,
        //                OrderComments = null,
        //                OrderQty = shoppingCartModel.TotalWeightValueRounded,
        //                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
        //            }
        //        );
        //    }
        //    foreach (var salesTaxListModel in salesTaxListModels)
        //    {
        //        var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
        //        shoppingCartSummaryItems.Add
        //        (
        //            new ShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = shippingAndHandlingChargesRate,
        //                ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
        //                OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
        //                OrderComments = null,
        //                OrderQty = shoppingCartModel.TotalWeightValueRounded,
        //                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
        //            }
        //        );
        //    }
        //    if (corpAcctModel != null && !corpAcctModel.ShippingAndHandlingCharges)
        //    {
        //        shoppingCartSummaryItems.Add
        //        (
        //            new ShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = shippingAndHandlingChargesRate,
        //                ItemShortDesc = "Discount - Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + " - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
        //                OrderAmount = -1 * (shippingAndHandlingChargesAmount + fuelCharges),
        //                OrderComments = null,
        //                OrderQty = shoppingCartModel.TotalWeightValueRounded,
        //                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
        //            }
        //        );
        //        foreach (var salesTaxListModel in salesTaxListModels)
        //        {
        //            shoppingCartSummaryItems.Add
        //            (
        //                new ShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemRate = shippingAndHandlingChargesRate,
        //                    ItemShortDesc = "Discount - " + salesTaxListModel.SalesTaxCaptionId + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
        //                    OrderAmount = -1 * ((shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f),
        //                    OrderComments = null,
        //                    OrderQty = shoppingCartModel.TotalWeightValueRounded,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
        //                }
        //            );
        //        }
        //    }
        //}
        ////private DeliveryChargeModel GetDeliveryChargeModel(DemogInfoAddressModel demogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    DeliveryChargeModel deliveryChargeModel = null;
        ////    List<DeliveryChargeModel> deliveryChargeModelsSearch = null, deliveryChargeModelsTemp;
        ////    deliveryChargeModelsSearch = ArchLibCache.DeliveryChargeModels.FindAll(x => x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId);
        ////    if (deliveryChargeModelsSearch.Count == 1)
        ////    {
        ////    }
        ////    else
        ////    {
        ////        if (deliveryChargeModelsSearch.Count > 0)
        ////        {
        ////            deliveryChargeModelsSearch = deliveryChargeModelsSearch.FindAll(x => x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId);
        ////            if (deliveryChargeModelsSearch.Count > 0)
        ////            {
        ////                deliveryChargeModelsTemp = deliveryChargeModelsSearch.FindAll(x => x.DestDemogInfoCountyId == demogInfoAddressModel.DemogInfoCountyId);
        ////                if (deliveryChargeModelsTemp.Count == 0)
        ////                {
        ////                    deliveryChargeModelsSearch = deliveryChargeModelsSearch.FindAll(x => x.DestDemogInfoCountyId == null);
        ////                }
        ////            }
        ////            if (deliveryChargeModelsSearch.Count > 0)
        ////            {
        ////                deliveryChargeModelsTemp = deliveryChargeModelsSearch.FindAll(x => x.DestDemogInfoCityId == demogInfoAddressModel.DemogInfoCityId);
        ////                if (deliveryChargeModelsTemp.Count == 0)
        ////                {
        ////                    deliveryChargeModelsTemp = deliveryChargeModelsSearch.FindAll(x => x.DestDemogInfoCityId == null);
        ////                }
        ////                if (deliveryChargeModelsTemp.Count > 1)
        ////                {
        ////                    deliveryChargeModelsSearch = deliveryChargeModelsTemp.FindAll(x => demogInfoAddressModel.DemogInfoZipId >= x.DestDemogInfoZipIdFrom && demogInfoAddressModel.DemogInfoZipId <= x.DestDemogInfoZipIdTo);
        ////                }
        ////                else
        ////                {
        ////                    deliveryChargeModelsSearch = deliveryChargeModelsTemp;
        ////                }
        ////            }
        ////        }
        ////    }
        ////    deliveryChargeModel = deliveryChargeModelsSearch[0];
        ////    return deliveryChargeModel;
        ////}
        //private void AddTotals(List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    float totalInvoiceAmount = 0;
        //    foreach (var shoppingCartSummary in shoppingCartSummaryItems)
        //    {
        //        if (shoppingCartSummary.OrderDetailTypeId != OrderDetailTypeEnum.TotalOrderAmountAfterDiscount)
        //        {
        //            totalInvoiceAmount += shoppingCartSummary.OrderAmount.Value;
        //        }
        //    }
        //    shoppingCartSummaryItems.Add
        //    (
        //        new ShoppingCartItemModel
        //        {
        //            ItemDesc = null,
        //            ItemId = null,
        //            ItemRate = totalInvoiceAmount,
        //            ItemShortDesc = "Total Invoice Amount",
        //            OrderAmount = totalInvoiceAmount,
        //            OrderComments = null,
        //            OrderQty = 1,
        //            OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
        //        }
        //    );
        //    shoppingCartSummaryItems.Add
        //    (
        //        new ShoppingCartItemModel
        //        {
        //            ItemDesc = null,
        //            ItemId = null,
        //            ItemRate = 0,
        //            ItemShortDesc = "Amount Paid - Gift Cert",
        //            OrderAmount = 0f,
        //            OrderComments = null,
        //            OrderQty = 1,
        //            OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByGiftCert,
        //        }
        //    );
        //    shoppingCartSummaryItems.Add
        //    (
        //        new ShoppingCartItemModel
        //        {
        //            ItemDesc = null,
        //            ItemId = null,
        //            ItemRate = 0,
        //            ItemShortDesc = "Amount Paid - Credit Card",
        //            OrderAmount = 0f,
        //            OrderComments = null,
        //            OrderQty = 1,
        //            OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCreditCard,
        //        }
        //    );
        //    shoppingCartSummaryItems.Add
        //    (
        //        new ShoppingCartItemModel
        //        {
        //            ItemDesc = null,
        //            ItemId = null,
        //            ItemRate = 0,
        //            ItemShortDesc = "Total Amount Paid",
        //            OrderAmount = 0f,
        //            OrderComments = null,
        //            OrderQty = 1,
        //            OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
        //        }
        //    );
        //    shoppingCartSummaryItems.Add
        //    (
        //        new ShoppingCartItemModel
        //        {
        //            ItemDesc = null,
        //            ItemId = null,
        //            ItemRate = totalInvoiceAmount,
        //            ItemShortDesc = "Balance Due",
        //            OrderAmount = totalInvoiceAmount,
        //            OrderComments = null,
        //            OrderQty = 1,
        //            OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
        //        }
        //    );
        //}
        //private void ApplyDiscounts(ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
        //    {
        //        shoppingCartItem.ShoppingCartItemSummarys = new List<ShoppingCartItemModel>();
        //    }
        //    string itemIds = "", prefixString = "";
        //    foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
        //    {
        //        itemIds += prefixString + shoppingCartItem.ItemId;
        //        prefixString = ", ";
        //    }
        //    ApplSessionObjectModel applSessionObjectModel = (ApplSessionObjectModel)sessionObjectModel.ApplSessionObjectModel;
        //    string sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + applSessionObjectModel.CorpAcctModel.CorpAcctId + " AND ItemId IN(" + itemIds + ")";
        //    SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //    ShoppingCartItemModel shoppingCartItemModel;
        //    while (sqlDataReader.Read())
        //    {
        //        shoppingCartItemModel = shoppingCartModel.ShoppingCartItems.First(x => x.ItemId == long.Parse(sqlDataReader["ItemId"].ToString()));
        //        shoppingCartItemModel.ItemDiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString());
        //        shoppingCartItemModel.ItemRate = shoppingCartItemModel.ItemRateBeforeDiscount.Value * (100 - shoppingCartItemModel.ItemDiscountPercent) / 100f;
        //        shoppingCartItemModel.OrderAmount = shoppingCartItemModel.ItemRate * shoppingCartItemModel.OrderQty;
        //    }
        //    sqlDataReader.Close();
        //}
        //private void CreateOrder(DeliveryInfoDataModel deliveryInfoDataModel, ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        OrderModel orderModel = CreateOrderModel(deliveryInfoDataModel, shoppingCartModel, sessionObjectModel, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CreateOrder(orderModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        deliveryInfoDataModel.OrderHeaderId = orderModel.OrderHeaderModel.OrderHeaderId;
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
        //private OrderModel CreateOrderModel(DeliveryInfoDataModel deliveryInfoDataModel, ShoppingCartModel shoppingCartModel, SessionObjectModel sessionObjectModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        OrderModel orderModel = new OrderModel
        //        {
        //            DeliveryInfoModel = new DeliveryInfoModel
        //            {
        //                DeliveryInfoDataModel = deliveryInfoDataModel,
        //            },
        //            OrderHeaderModel = new OrderHeaderModel
        //            {
        //                DimensionUnitId = DimensionUnitEnum.Centimeter,
        //                OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //                OrderNumber = 1,
        //                OrderStatusId = OrderStatusEnum.Open,
        //                PersonId = sessionObjectModel.PersonId,
        //                VolumeValue = 0,
        //                WeightUnitId = WeightUnitEnum.Grams,
        //                WeightValue = 0,
        //                OrderDetailModels = new List<OrderDetailModel>(),
        //                OrderSummaryModels = new List<OrderDetailModel>(),
        //            },
        //        };
        //        ItemModel itemModel;
        //        float itemRate;
        //        foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
        //        {
        //            itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItem.ItemId);
        //            itemRate = itemModel.ItemRate.Value;
        //            orderModel.OrderHeaderModel.OrderDetailModels.Add
        //            (
        //                new OrderDetailModel
        //                {
        //                    ItemDesc = itemModel.ItemDesc,
        //                    ItemRate = itemRate,
        //                    ItemShortDesc = itemModel.ItemShortDesc,
        //                    ItemId = itemModel.ItemId,
        //                    OrderAmount = itemRate * shoppingCartItem.OrderQty.Value,
        //                    OrderComments = shoppingCartItem.OrderComments,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.Item,
        //                    OrderQty = (long)shoppingCartItem.OrderQty,
        //                    VolumeValue = shoppingCartItem.VolumeValue.Value,
        //                    WeightValue = shoppingCartItem.WeightValue.Value,
        //                }
        //            );
        //        }
        //        foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartSummaryItems)
        //        {
        //            orderModel.OrderHeaderModel.OrderDetailModels.Add
        //            (
        //                new OrderDetailModel
        //                {
        //                    ItemDesc = null,
        //                    ItemRate = shoppingCartItem.OrderAmount.Value,
        //                    ItemShortDesc = shoppingCartItem.ItemShortDesc,
        //                    ItemId = shoppingCartItem.ItemId,
        //                    OrderAmount = shoppingCartItem.OrderAmount.Value,
        //                    OrderComments = shoppingCartItem.OrderComments,
        //                    OrderDetailTypeId = shoppingCartItem.OrderDetailTypeId,
        //                    OrderQty = 1,
        //                }
        //            );
        //        }
        //        return orderModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private void CreateGiftCertImage(string inputFullFileName, GiftCertModel giftCertModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    Bitmap bitmap = new Bitmap(inputFullFileName);
        //    Graphics graphics = Graphics.FromImage(bitmap);

        //    string giftAmountText = ConvertAmountToWords((long)giftCertModel.GiftCertAmount);
        //    graphics.DrawString(giftCertModel.RecipientFullName, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(189, 180));
        //    graphics.DrawString(giftAmountText, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new PointF(225, 228));
        //    graphics.DrawString(giftCertModel.GiftCertMessage, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(252, 270));
        //    graphics.DrawString("From : " + giftCertModel.SenderFullName, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(153, 342));
        //    graphics.DrawString("Gift Certificate# : " + giftCertModel.GiftCertNumber, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new PointF(153, 360));
        //    graphics.DrawString("Gift Certificate Key : " + giftCertModel.GiftCertKey, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new PointF(153, 378));
        //    graphics.DrawString("Login using your email address, password & use above Gift Cert# & key while making payment", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new PointF(153, 396));

        //    bitmap.Save(giftCertModel.GiftCertImageFileName);
        //    bitmap.Dispose();
        //}
        //private GiftCertModel ProcessGiftCardPayment(string giftCertNumber, string giftCertKey, float orderAmount, out float giftCertPaymentAmount, out float giftCertBalanceAmount, ModelStateDictionary modelStateDictionary, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    GiftCertModel giftCertModel = ApplicationDataContext.GetGiftCert(giftCertNumber, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    if (giftCertModel == null)
        //    {
        //        modelStateDictionary.AddModelError("GiftCertNumber", "Invalid Gift Cert#");
        //        modelStateDictionary.AddModelError("", "Invalid Gift Cert#");
        //        giftCertPaymentAmount = 0;
        //        giftCertBalanceAmount = 0;
        //    }
        //    else
        //    {
        //        if (giftCertModel.GiftCertKey == giftCertKey)
        //        {
        //            if (giftCertModel.GiftCertBalanceAmount > 0)
        //            {
        //                if (giftCertModel.GiftCertBalanceAmount > orderAmount)
        //                {//100 > 40 - Apply 40 to this payment. Balance will be 60 after this
        //                    giftCertPaymentAmount = orderAmount;
        //                }
        //                else
        //                {//100 < 250 - Apply 100 to this payment. 150 from CC. Balance will be 0 after this
        //                    giftCertPaymentAmount = giftCertModel.GiftCertBalanceAmount;
        //                }
        //                giftCertBalanceAmount = giftCertModel.GiftCertBalanceAmount - giftCertPaymentAmount;
        //                giftCertModel.GiftCertUsedAmount += giftCertPaymentAmount;
        //                giftCertModel.GiftCertBalanceAmount = (float)giftCertModel.GiftCertAmount - giftCertModel.GiftCertUsedAmount;
        //            }
        //            else
        //            {
        //                modelStateDictionary.AddModelError("GiftCertNumber", "Invalid Gift Cert Amount");
        //                modelStateDictionary.AddModelError("", "Invalid Gift Cert Amount");
        //                giftCertPaymentAmount = 0;
        //                giftCertBalanceAmount = 0;
        //            }
        //        }
        //        else
        //        {
        //            modelStateDictionary.AddModelError("GiftCertKey", "Invalid Gift Cert Key");
        //            modelStateDictionary.AddModelError("", "Invalid Gift Cert Key");
        //            giftCertPaymentAmount = 0;
        //            giftCertBalanceAmount = 0;
        //        }
        //    }
        //    return giftCertModel;
        //}
        //private List<SalesTaxListModel> GetSalesTaxListModels(DemogInfoAddressModel demogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    var salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
        //        (
        //            x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
        //         && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
        //         && x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId
        //         && demogInfoAddressModel.DemogInfoZipId == x.DestDemogInfoZipId
        //        );
        //    if (!salesTaxListModels.Any())
        //    {
        //        salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
        //        (
        //            x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
        //            && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
        //            && x.DestDemogInfoSubDivisionId == demogInfoAddressModel.DemogInfoSubDivisionId
        //            && x.DestDemogInfoZipId == null
        //        );
        //    }
        //    if (!salesTaxListModels.Any())
        //    {
        //        salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
        //        (
        //            x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
        //            && x.DestDemogInfoCountryId == demogInfoAddressModel.DemogInfoCountryId
        //            && x.DestDemogInfoSubDivisionId == null
        //            && x.DestDemogInfoZipId == null
        //        );
        //    }
        //    return salesTaxListModels;
        //}
        //private void UpdatePayments(string giftCertLast4, float? giftCertPaymentAmount, string creditCardLast4, string creditCardProcessUniqueRef, float? creditCardPaymentAmount, List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    ShoppingCartItemModel shoppingCartItemModel;
        //    float totalPaymentAmount = 0;
        //    if (giftCertPaymentAmount != null)
        //    {
        //        shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByGiftCert);
        //        shoppingCartItemModel.ItemShortDesc += " " + giftCertLast4;
        //        shoppingCartItemModel.OrderAmount = -1 * giftCertPaymentAmount;
        //        totalPaymentAmount += giftCertPaymentAmount.Value;
        //    }
        //    if (creditCardPaymentAmount != null)
        //    {
        //        shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.AmountPaidByCreditCard);
        //        shoppingCartItemModel.ItemShortDesc += " " + creditCardLast4 + " " + creditCardProcessUniqueRef;
        //        shoppingCartItemModel.OrderAmount = -1 * creditCardPaymentAmount;
        //        totalPaymentAmount += creditCardPaymentAmount.Value;
        //    }
        //    shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalInvoiceAmount); //Total Invoice Amount
        //    float totalInvoiceAmount = shoppingCartItemModel.OrderAmount.Value;
        //    shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.TotalAmountPaid); //Total Amount Paid
        //    shoppingCartItemModel.OrderAmount = -1 * totalPaymentAmount;
        //    shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue); //Balance Due
        //    shoppingCartItemModel.OrderAmount = totalInvoiceAmount - totalPaymentAmount;
        //}
        //private void UpdateShoppingCart(ShoppingCartModel shoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        shoppingCartModel.ShoppingCartTotalAmount = 0;
        //        shoppingCartModel.TotalVolumeValue = 0;
        //        shoppingCartModel.TotalWeightValue = 0;
        //        shoppingCartModel.TotalItemsCount = 0;
        //        shoppingCartModel.TotalProductOrVolumetricWeight = 0;
        //        foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
        //        {
        //            shoppingCartModel.ShoppingCartTotalAmount += shoppingCartItem.OrderAmount;
        //            shoppingCartModel.TotalVolumeValue += shoppingCartItem.VolumeValue;
        //            shoppingCartModel.TotalWeightValue += shoppingCartItem.WeightCalcValue;
        //            shoppingCartModel.TotalItemsCount += shoppingCartItem.OrderQty.Value;
        //            shoppingCartModel.TotalProductOrVolumetricWeight += shoppingCartItem.ProductOrVolumetricWeight;
        //        }
        //        shoppingCartModel.TotalWeightValueRounded = (long)Math.Ceiling(shoppingCartModel.TotalWeightValue.Value / 1000f);
        //        shoppingCartModel.TotalWeightValueRoundedUnit = WeightUnitEnum.Kilograms;
        //        shoppingCartModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(shoppingCartModel.TotalProductOrVolumetricWeight.Value / 1000f);
        //        shoppingCartModel.TotalProductOrVolumetricWeightRoundedUnit = WeightUnitEnum.Kilograms;
        //        shoppingCartModel.ShoppingCartSummaryItems[0].ItemShortDesc = "Total Order Amount (#" + shoppingCartModel.TotalItemsCount + ")";
        //        shoppingCartModel.ShoppingCartSummaryItems[0].OrderAmount = shoppingCartModel.ShoppingCartTotalAmount;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private string ConvertAmountToWords(long amount)
        //{
        //    long quotient;
        //    string[] numberUnits = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        //    string[] numberTens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        //    string amountInWords;

        //    amountInWords = "";

        //    quotient = amount / 1000;
        //    amount = amount - quotient * 1000;

        //    if (quotient > 0)
        //    {
        //        amountInWords += numberUnits[quotient] + " Thousand";
        //    }

        //    quotient = amount / 100;
        //    amount = amount - quotient * 100;

        //    if (quotient > 0)
        //    {
        //        amountInWords += numberUnits[quotient] + " Hundred";
        //    }

        //    if (amount > 0 && amount < 20)
        //    {
        //        if (amountInWords == "")
        //        {
        //            ;
        //        }
        //        else
        //        {
        //            amountInWords += " ";
        //        }
        //        amountInWords += numberUnits[amount];
        //    }
        //    else
        //    {
        //        quotient = amount / 10;
        //        amount = amount - quotient * 10;
        //        if (quotient > 0)
        //        {
        //            if (amountInWords == "")
        //            {
        //                ;
        //            }
        //            else
        //            {
        //                amountInWords += " ";
        //            }
        //            amountInWords += numberTens[quotient];
        //        }
        //        if (amount > 0 && amount < 20)
        //        {
        //            if (amountInWords == "")
        //            {
        //                ;
        //            }
        //            else
        //            {
        //                amountInWords += " ";
        //            }
        //            amountInWords += numberUnits[amount];
        //        }
        //    }

        //    amountInWords += " and 00/100 Dollars.........";

        //    return amountInWords;
        //}
        //private Dictionary<string, string> GetCreditCardKVPs(string creditCardProcessor, long clientId)
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
        //            break;
        //    }

        //    return creditCardKVPs;
        //}
        #endregion
    }
}
