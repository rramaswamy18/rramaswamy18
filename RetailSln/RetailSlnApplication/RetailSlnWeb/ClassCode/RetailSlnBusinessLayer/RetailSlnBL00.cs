using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
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
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                if (shoppingCartModel == null)
                {
                    shoppingCartModel = new ShoppingCartModel
                    {
                        ShoppingCartItems = new List<ShoppingCartItemModel>(),
                    };
                }
                ItemModel itemModel = RetailSlnCache.ItemModels.Find(x => x.ItemId == itemId);
                float heightValue, lengthValue, weightValue, widthValue;
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
                    shoppingCartModel.ShoppingCartItems.Add
                    (
                        new ShoppingCartItemModel
                        {
                            DimensionUnitId = dimensionUnitId,
                            HeightValue = heightValue,
                            ItemDesc = itemModel.ItemDesc,
                            ItemId = itemModel.ItemId,
                            ItemRate = itemModel.ItemRate,
                            ItemShortDesc = itemModel.ItemShortDesc,
                            LengthValue = lengthValue,
                            OrderAmount = orderQty * itemModel.ItemRate,
                            OrderComments = "",
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
                    LoginUserProfModel = new LoginUserProfModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    RegisterUserProfModel = new RegisterUserProfModel(),
                    ResetPasswordModel = new ResetPasswordModel(),
                    ShoppingCartModel = CheckoutValidate(httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                checkoutModel.ShoppingCartModel.Checkout = true;
                List<string> numberSessions = new List<string>
                {
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
                checkoutModel.LoginUserProfModel.CaptchaAnswerLogin = null;
                //checkoutModel.LoginUserProfModel.CaptchaAnswerLogin = (int.Parse(httpSessionStateBase["CaptchaNumberLogin0"].ToString()) + int.Parse(httpSessionStateBase["CaptchaNumberLogin1"].ToString())).ToString();
                checkoutModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                checkoutModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                checkoutModel.RegisterUserProfModel.CaptchaAnswerRegister = null;
                checkoutModel.RegisterUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
                checkoutModel.RegisterUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
                checkoutModel.ResetPasswordModel.CaptchaAnswerResetPassword = null;
                checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
                checkoutModel.ResetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
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
                shoppingCartModel.ShoppingCartSummaryItems = null;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
                    {
                        return shoppingCartModel;
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
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                shoppingCartModel.ShoppingCartSummaryItems = new List<ShoppingCartItemModel>();
                shoppingCartModel.Checkout = false;
                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
                if (shoppingCartModel == null)
                {
                    throw new Exception("Shopping Cart is Empty");
                }
                else
                {
                    if (shoppingCartModel.ShoppingCartItems.Count > 0 && shoppingCartModel.ShoppingCartTotalAmount > 0)
                    {
                        DeliveryInfoModel deliveryInfoModel = new DeliveryInfoModel
                        {
                            DeliveryInfoDataModel = new DeliveryInfoDataModel
                            {
                                //AlternateTelephoneNum = "2345678901",
                                DeliveryAddressModel = new DemogInfoAddressModel
                                {
                                    //AddressLine1 = "A1",
                                    //AddressLine2 = "A2",
                                    BuildingTypeId = BuildingTypeEnum._,
                                    //CityName = "City",
                                    //HouseNumber = "123",
                                    //ZipCode = "12345",
                                    DemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                                    //DemogInfoSubDivisionId = 526,
                                },
                                //DeliveryInstructions = "Test Delivery Instructions",
                                //PrimaryTelephoneNum = "1234567890",
                            },                           
                            ShoppingCartModel = shoppingCartModel,
                        };
                        return deliveryInfoModel;
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
        //POST Delivery Info
        public void DeliveryInfo(DeliveryInfoDataModel deliveryInfoDataModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                deliveryInfoDataModel.DeliveryAddressModel.CountryAbbrev = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CountryAbbrev");//DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId).CountryAbbrev;
                deliveryInfoDataModel.DeliveryAddressModel.StateAbbrev = DemogInfoCache.DemogInfoSubDivisionModels.First(x => x.DemogInfoSubDivisionId == deliveryInfoDataModel.DeliveryAddressModel.DemogInfoSubDivisionId).StateAbbrev;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                ItemModel itemModel;
                float totalItemsCount = 0, totalOrderAmount = 0, totalVolumeValue = 0, totalWeightValue = 0;
                foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                {
                    itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItem.ItemId);
                    totalItemsCount += shoppingCartItem.OrderQty.Value;
                    totalOrderAmount += itemModel.ItemRate.Value * shoppingCartItem.OrderQty.Value;
                    totalVolumeValue += shoppingCartItem.VolumeValue.Value;
                    totalWeightValue += shoppingCartItem.WeightValue.Value;
                }
                shoppingCartModel.ShoppingCartSummaryItems = new List<ShoppingCartItemModel>();
                long personId = sessionObjectModel.PersonId;
                AddAdditionalCharges(totalItemsCount, totalOrderAmount, totalVolumeValue, totalWeightValue, deliveryInfoDataModel.DeliveryAddressModel.ZipCode, shoppingCartModel.ShoppingCartSummaryItems, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddFirstOrderDiscount(personId, shoppingCartModel.ShoppingCartSummaryItems, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddDiscounts(shoppingCartModel.ShoppingCartSummaryItems, clientId, ipAddress, execUniqueId, loggedInUserId);
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

                    //CaptchaAnswer = (int.Parse(httpSessionStateBase["CaptchaNumberLogin0"].ToString()) + int.Parse(httpSessionStateBase["CaptchaNumberLogin1"].ToString())).ToString(),
                    //CardExpiryMM = "09",
                    //CardExpiryYYYY = "2025",
                    //CardHolderName = "Ravi Ramaswamy",
                    //CreditCardNumber = "4111111111111111",
                    //CVV = "123",
                    //ConfirmRecipientEmailAddress = "test2@email.com",
                    //GiftCertAmount = 108,
                    //GiftCertMessage = "Happy Birthday!!!",
                    //RecipientFullName = "Mary Aguilar",
                    //RecipientEmailAddress = "test2@email.com",
                    //SenderEmailAddress = "test1@email.com",
                    //SenderPassword = "Login9#9Password",
                    //SenderFullName = "John Smith",
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
        public void GiftCert(ref GiftCertModel giftCertModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                        CreditCardDataModel creditCardDataModel = new CreditCardDataModel
                        {
                            CreditCardAmount = giftCertModel.GiftCertAmount.ToString(),
                            CreditCardExpMM = giftCertModel.CardExpiryMM,
                            CreditCardExpYear = giftCertModel.CardExpiryYYYY,
                            CreditCardKVPs = new Dictionary<string, string>
                            {
                                { "PrivateKey", ArchLibCache.GetPrivateKey(1) },
                                { "NuveiRestAPIRootUri", ArchLibCache.GetApplicationDefault(ArchLibCache.ClientId, "NuveiRestAPIRootUri", "") },
                                { "NuveiRequestUri", ArchLibCache.GetApplicationDefault(ArchLibCache.ClientId, "NuveiRequestUri", "") },
                                { "NuveiTerminalId", ArchLibCache.GetApplicationDefault(ArchLibCache.ClientId, "NuveiTerminalId", "") },
                                { "NuveiSharedSecret", ArchLibCache.GetApplicationDefault(ArchLibCache.ClientId, "NuveiSharedSecret", "") },
                            },
                            CreditCardNumber = giftCertModel.CreditCardNumber,
                            CreditCardProcessor = "NUVEI",
                            CreditCardSecCode = giftCertModel.CVV,
                            CreditCardTranType = "PAYMENT",
                            CurrencyCode = "USD",
                            NameAsOnCard = giftCertModel.CardHolderName,
                        };
                        CreditCardServiceBL creditCardService = new CreditCardServiceBL();
                        giftCertModel.CreditCardProcessStatus = creditCardService.ProcessCreditCard(creditCardDataModel, out string processMessage, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                        string inputFullFileName;
                        inputFullFileName = Utilities.GetServerMapPath("~/Documents/Templates/GiftCertificate") + @"/" + giftCertModel.SelectedTemplateName;
                        giftCertModel.GiftCertImageFileName = Utilities.GetServerMapPath("~/Documents/GiftCertificates") + @"/" + giftCertModel.GiftCertNumber + ".jpg";
                        CreateGiftCertImage(inputFullFileName, giftCertModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                        //SendGiftCertReceiptEmail(giftCertModel, userProfRegistered, loginPassword, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public OrderReceiptModel OrderReceipt(PaymentDataModel paymentDataModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                DeliveryInfoDataModel deliveryInfoDataModel = (DeliveryInfoDataModel)httpSessionStateBase["DeliveryInfoDataModel"];
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)httpSessionStateBase["ShoppingCartModel"];
                shoppingCartModel.Checkout = false;
                OrderReceiptModel orderReceiptModel = new OrderReceiptModel
                {
                    DeliveryInfoDataModel = deliveryInfoDataModel,
                    PaymentDataModel = paymentDataModel,
                    ShoppingCartModel = shoppingCartModel,
                };
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
                shoppingCartModel.Checkout = false;
                httpSessionStateBase["ShoppingCartModel"] = shoppingCartModel;
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
                            PaymentDataModel = new PaymentDataModel
                            {
                                //CardExpiryMM = "09",
                                //CardExpiryYYYY = "2025",
                                //CardHolderName = "John Miller",
                                //CreditCardNumber = "4111111111111111",
                                //CVV = "123",
                                EmailAddress = sessionObjectModel.EmailAddress,
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
        public void Payment(ref PaymentDataModel paymentDataModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
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
                                CurrencyCode = "USD",
                                NameAsOnCard = paymentDataModel.CardHolderName,
                            };
                            CreditCardServiceBL creditCardService = new CreditCardServiceBL();
                            paymentDataModel.CreditCardProcessStatus = creditCardService.ProcessCreditCard(creditCardDataModel, out string processMessage, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                            paymentDataModel.CreditCardProcessMessage = creditCardDataModel.ProcessMessage;
                            paymentDataModel.CreditCardNumberLast4 = creditCardDataModel.CreditCardNumberLast4;
                            paymentDataModel.CreditCardDataId = creditCardDataModel.CreditCardDataId;
                            creditCardNumberLast4 = creditCardDataModel.CreditCardNumberLast4;
                            if (paymentDataModel.CreditCardProcessStatus)
                            {
                                creditCardProcessMessage = paymentDataModel.CreditCardProcessMessage;
                            }
                            else
                            {
                                modelStateDictionary.AddModelError("", creditCardDataModel.ProcessMessage);
                            }
                        }
                        else
                        {
                            paymentDataModel.CreditCardProcessStatus = true;
                            paymentDataModel.CreditCardProcessMessage = "Not Required";
                            creditCardProcessMessage = paymentDataModel.CreditCardProcessMessage;
                        }
                        if (paymentDataModel.CreditCardProcessStatus)
                        {
                            UpdatePayments(giftCertNumberLast4, giftCertPaymentAmount, creditCardNumberLast4, creditCardProcessMessage, creditCardPaymentAmount, shoppingCartModel.ShoppingCartSummaryItems, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                            foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                            {
                                itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItem.ItemId);
                                orderModel.OrderHeaderModel.OrderDetailModels.Add
                                (
                                    new OrderDetailModel
                                    {
                                        ItemDesc = itemModel.ItemDesc,
                                        ItemRate = (float)itemModel.ItemRate,
                                        ItemShortDesc = itemModel.ItemShortDesc,
                                        ItemId = itemModel.ItemId,
                                        OrderAmount = itemModel.ItemRate.Value * shoppingCartItem.OrderQty.Value,
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
        ////GET OrderCategoryItem
        //public string OrderCategoryItem(long categoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string categoryName = RetailSlnCache.CategoryModels.First(x => x.CategoryId == categoryId).CategoryName;
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return categoryName;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //GET RemoveFromCart
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
        private void AddAdditionalCharges(float totalItemsCount, float totalOrderAmount, float totalVolumeValue, float totalWeightValue, string deliveryZipCode, List<ShoppingCartItemModel> shoppingCartSummaryItems, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = totalOrderAmount,
                    ItemShortDesc = "Total Order Amount (#" + totalItemsCount + ")",
                    OrderAmount = totalOrderAmount,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
                }
            );
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = totalOrderAmount,
                    ItemShortDesc = "Sales Tax Amount",
                    OrderAmount = totalOrderAmount * 0.1f,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                }
            );
            List<DeliveryChargeModel> deliveryChargeModels = ApplicationDataContext.GetDeliveryCharge(totalVolumeValue, totalWeightValue, deliveryZipCode, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            float shippingAndHandlingChargesByWeight = 0, shippingAndHandlingChargesByVolume = 0;
            foreach (var deliveryChargeModel in deliveryChargeModels)
            {
                if (deliveryChargeModel.ChargeUnitMeasure == "WEIGHT")
                {
                    shippingAndHandlingChargesByWeight = deliveryChargeModel.DeliveryChargeAmount;
                }
                if (deliveryChargeModel.ChargeUnitMeasure == "VOLUME")
                {
                    shippingAndHandlingChargesByVolume = deliveryChargeModel.DeliveryChargeAmount;
                }
            }
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 36,
                    ItemShortDesc = "Shipping & Handling Charges by Weight",
                    OrderAmount = shippingAndHandlingChargesByWeight,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                }
            );
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 36,
                    ItemShortDesc = "Shipping & Handling Charges by Volume",
                    OrderAmount = shippingAndHandlingChargesByVolume,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                }
            );
        }
        private void AddFirstOrderDiscount(long personId, List<ShoppingCartItemModel> shoppingCartSummaryItems, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //See if client is participating in First Discount 
            //If so, Get Order Count from Order Header
            //If 0 - add discount if not skip
            string firstOrderDiscount = ArchLibCache.GetApplicationDefault(ArchLibCache.ClientId, "RegisterUserProf", "FirstOrderDiscount");
            if (firstOrderDiscount != "")
            {
                try
                {
                    int orderHeaderCount = ApplicationDataContext.GetOrderHeaderCount(personId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (orderHeaderCount == 0)
                    {
                        shoppingCartSummaryItems.Add
                        (
                            new ShoppingCartItemModel
                            {
                                ItemDesc = null,
                                ItemId = null,
                                ItemRate = 18,
                                ItemShortDesc = "First Order Discount",
                                OrderAmount = -1 * float.Parse(firstOrderDiscount),
                                OrderComments = null,
                                OrderQty = 1,
                                OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                            }
                        );
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
        }
        private void AddDiscounts(List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = null,
                    ItemRate = 18,
                    ItemShortDesc = "Discount",
                    OrderAmount = -0,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.Discount,
                }
            );
        }
        private void AddTotals(List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            float totalInvoiceAmount = 0;
            foreach (var shoppingCartSummary in shoppingCartSummaryItems)
            {
                totalInvoiceAmount += shoppingCartSummary.OrderAmount.Value;
            }
            shoppingCartSummaryItems.Add
            (
                new ShoppingCartItemModel
                {
                    ItemDesc = null,
                    ItemId = 9980000,
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
                    ItemId = 9985000,
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
                    ItemId = 9986000,
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
                    ItemId = 9989000,
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
                    ItemId = 9990000,
                    ItemRate = totalInvoiceAmount,
                    ItemShortDesc = "Balance Due",
                    OrderAmount = totalInvoiceAmount,
                    OrderComments = null,
                    OrderQty = 1,
                    OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                }
            );
        }
        private void UpdatePayments(string giftCertLast4, float? giftCertPaymentAmount, string creditCardLast4, string creditCardProcessUniqueRef, float? creditCardPaymentAmount, List<ShoppingCartItemModel> shoppingCartSummaryItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShoppingCartItemModel shoppingCartItemModel;
            float totalPaymentAmount = 0;
            if (giftCertPaymentAmount != null)
            {
                shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.ItemId == 9985000);
                shoppingCartItemModel.ItemShortDesc += " " + giftCertLast4;
                shoppingCartItemModel.OrderAmount = -1 * giftCertPaymentAmount;
                totalPaymentAmount += giftCertPaymentAmount.Value;
            }
            if (creditCardPaymentAmount != null)
            {
                shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.ItemId == 9986000);
                shoppingCartItemModel.ItemShortDesc += " " + creditCardLast4 + " " + creditCardProcessUniqueRef;
                shoppingCartItemModel.OrderAmount = -1 * creditCardPaymentAmount;
                totalPaymentAmount += creditCardPaymentAmount.Value;
            }
            shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.ItemId == 9980000); //Total Invoice Amount
            float totalInvoiceAmount = shoppingCartItemModel.OrderAmount.Value;
            shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.ItemId == 9989000); //Total Payment Amount
            shoppingCartItemModel.OrderAmount = -1 * totalPaymentAmount;
            shoppingCartItemModel = shoppingCartSummaryItems.First(x => x.ItemId == 9990000); //Balance Due
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
                foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItems)
                {
                    shoppingCartModel.ShoppingCartTotalAmount += shoppingCartItem.OrderAmount;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
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

            quotient = amount / 100;
            amount = amount - quotient * 100;

            if (quotient > 0)
            {
                amountInWords += numberUnits[quotient] + " Hundered";
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
                default:
                    creditCardKVPs = new Dictionary<string, string>();
                    break;
            }

            return creditCardKVPs;
        }
    }
}
