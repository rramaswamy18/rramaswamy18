using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryShippingLibrary;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: BusinessInfo
        public BusinessInfoModel BusinessInfo(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                BusinessInfoModel businessInfoModel = RetailSlnCache.BusinessInfoModel;
                return businessInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // GET: Categories
        public List<CategoryModel> Categorys(string parentCategoryIdParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                List<CategoryItemMasterHierModel> categoryItemHierModels = RetailSlnCache.CategoryItemMasterHierModels.FindAll(x => x.ParentCategoryId == parentCategoryId).OrderBy(x => x.SeqNum).ToList();
                List<CategoryModel> categoryModels = new List<CategoryModel>();
                foreach (var categoryItemHierModel in categoryItemHierModels)
                {
                    categoryModels.Add(categoryItemHierModel.CategoryModel);
                }
                return categoryModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // GET: Items
        public List<ItemMasterModel> Items(string categoryIdParm, string pageNumParm, string rowCountParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(categoryIdParm, out long categoryId);
                int.TryParse(pageNumParm, out int pageNum);
                pageNum = pageNum > 0 ? (pageNum = pageNum - 1) : pageNum;
                int.TryParse(rowCountParm, out int rowCount);
                rowCount = rowCount == 0 || rowCount > 50 ? 50 : rowCount;
                List<CategoryItemMasterHierModel> categoryItemHierModels = RetailSlnCache.CategoryItemMasterHierModels.FindAll(x => x.ParentCategoryId == categoryId).Skip(pageNum * rowCount).Take(rowCount).OrderBy(x => x.SeqNum).ToList();
                List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
                foreach (var categoryItemHierModel in categoryItemHierModels)
                {
                    itemMasterModels.Add(categoryItemHierModel.ItemMasterModel);
                }
                return itemMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // GET: LoginUserProf
        public ApiLoginUserProfModel LoginUserProf(string loginEmailAddress, string loginPassword, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int a = 1, b = 0, c = a / b;
                loginEmailAddress = loginEmailAddress.Trim().ToLower();
                ArchLibDataContext.OpenSqlConnection();
                string privateKey = ArchLibCache.GetPrivateKey(clientId);
                loginPassword = EncryptDecrypt.EncryptDataMd5(loginPassword, privateKey);
                ApiLoginUserProfModel apiLoginUserProfModel = new ApiLoginUserProfModel();
                PersonModel personModel = ArchLibDataContext.SelectLoginUserProf(loginEmailAddress, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (personModel != null)
                {
                    long passwordExpiry = long.Parse(DateTime.Parse(personModel.AspNetUserModel.PasswordExpiry).ToString("yyyyMMddHHmmss"));
                    if (personModel.AspNetUserModel.LoginPassword == loginPassword && passwordExpiry >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) && personModel.StatusId == GenericStatusEnum.Active && personModel.AspNetUserModel.UserStatusId == UserStatusEnum.Active)
                    {
                        PersonExtn1Model personExtn1Model = ApplicationDataContext.GetPersonExtn1FromPersonId(personModel.PersonId.Value, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        apiLoginUserProfModel = new ApiLoginUserProfModel
                        {
                            ClientId = clientId,
                            AspNetRoleId = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.AspNetRoleId,
                            AspNetRoleName = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.AspNetRoleName,
                            AspNetUserId = personModel.AspNetUserModel.AspNetUserId,
                            EmailAddress = loginEmailAddress,
                            FirstName = personModel.FirstName,
                            JwtToken = JwtManager.GenerateToken(loginEmailAddress),
                            LastName = personModel.LastName,
                            NicknameFirst = personModel.NicknameFirst,
                            NicknameLast = personModel.NicknameLast,
                            PersonId = personModel.PersonId.Value,
                            PhoneNumber = personModel.AspNetUserModel.PhoneNumber,
                            TelephoneCode = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == personModel.AspNetUserModel.TelephoneCountryId.Value).TelephoneCode.Value,
                            TelephoneCountryId = personModel.AspNetUserModel.TelephoneCountryId.Value,
                            CorpAcctModel = personExtn1Model.CorpAcctModel,
                            DefaultDeliveryDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                            DeliveryCountrys = RetailSlnCache.DeliveryDemogInfoCountrys,
                            DeliveryCountryStates = RetailSlnCache.DeliveryDemogInfoCountryStates,
                            DeliveryMethods = RetailSlnCache.DeliveryMethods,
                            PaymentModes = personExtn1Model.CorpAcctModel.CreditSale ? RetailSlnCache.PaymentMethodsCreditSale : RetailSlnCache.PaymentMethods,
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>(),
                                ResponseMessages = new List<string>(),
                                ResponseTypeId = ResponseTypeEnum.Success,
                                ValidationSummaryMessage = "",
                            }
                        };
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        return apiLoginUserProfModel;
                    }
                    else
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Unable to login using credentials passed");
                        throw new Exception("Unable to login using credentials passed");
                    }
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Unable to login using credentials passed");
                    throw new Exception("Unable to login using credentials passed");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ArchLibDataContext.CloseSqlConnection();
            }
        }

        // GET: LoginUserProf
        public ApiLoginUserProfModel SessionInfo(string jwtToken, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int a = 1, b = 0, c = a / b;
                var username = GetUserInfoFromJwtToken(jwtToken, clientId, ipAddress, execUniqueId, loggedInUserId);
                ArchLibDataContext.OpenSqlConnection();
                string privateKey = ArchLibCache.GetPrivateKey(clientId);
                ApiLoginUserProfModel apiLoginUserProfModel = new ApiLoginUserProfModel();
                PersonModel personModel = ArchLibDataContext.SelectLoginUserProf(username, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (personModel != null)
                {
                    long passwordExpiry = long.Parse(DateTime.Parse(personModel.AspNetUserModel.PasswordExpiry).ToString("yyyyMMddHHmmss"));
                    PersonExtn1Model personExtn1Model = ApplicationDataContext.GetPersonExtn1FromPersonId(personModel.PersonId.Value, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    apiLoginUserProfModel = new ApiLoginUserProfModel
                    {
                        ClientId = clientId,
                        AspNetRoleId = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.AspNetRoleId,
                        AspNetRoleName = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.AspNetRoleName,
                        AspNetUserId = personModel.AspNetUserModel.AspNetUserId,
                        EmailAddress = username,
                        FirstName = personModel.FirstName,
                        JwtToken = jwtToken,
                        LastName = personModel.LastName,
                        NicknameFirst = personModel.NicknameFirst,
                        NicknameLast = personModel.NicknameLast,
                        PersonId = personModel.PersonId.Value,
                        PhoneNumber = personModel.AspNetUserModel.PhoneNumber,
                        TelephoneCode = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == personModel.AspNetUserModel.TelephoneCountryId.Value).TelephoneCode.Value,
                        TelephoneCountryId = personModel.AspNetUserModel.TelephoneCountryId.Value,
                        CorpAcctModel = personExtn1Model.CorpAcctModel,
                        DefaultDeliveryDemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                        DeliveryCountrys = RetailSlnCache.DeliveryDemogInfoCountrys,
                        DeliveryCountryStates = RetailSlnCache.DeliveryDemogInfoCountryStates,
                        DeliveryMethods = RetailSlnCache.DeliveryMethods,
                        PaymentModes = personExtn1Model.CorpAcctModel.CreditSale ? RetailSlnCache.PaymentMethodsCreditSale : RetailSlnCache.PaymentMethods,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            PropertyErrorsKVP = new List<KeyValuePair<string, List<string>>>(),
                            ResponseMessages = new List<string>(),
                            ResponseTypeId = ResponseTypeEnum.Success,
                            ValidationSummaryMessage = "",
                        }
                    };
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    return apiLoginUserProfModel;
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Unable to login using credentials passed");
                    throw new Exception("Invalid username from token");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ArchLibDataContext.CloseSqlConnection();
            }
        }

        // GET: CreateShoppingCartInput
        public ApiShoppingCartModel CreateShoppingCartInput()
        {
            ApiShoppingCartModel apiShoppingCartModel = new ApiShoppingCartModel
            {
                DeliveryInfoModel = new ApiDeliveryInfoModel
                {
                    AlternateTelephoneDemogInfoCountryId = 106,
                    AlternateTelephoneNum = "234567890",
                    AlternateTelephoneTelephoneCode = 91,
                    CreateDeliveryAddress = true,
                    DeliveryAddressModel = new ApiDemogInfoAddressModel
                    {
                        ClientId = 97,
                        AddressLine1 = "4250 Canyon Crest Rd W",
                        BuildingTypeId = BuildingTypeEnum._,
                        CityName = "CHENNAI",
                        CountryAbbrev = "IND",
                        CountyName = "IND",
                        CountryDesc = "India",
                        DemogInfoAddressId = 0,
                        DemogInfoCityId = 0,
                        DemogInfoCountryId = 106,
                        DemogInfoCountyId = 0,
                        DemogInfoSubDivisionId = 0,
                        DemogInfoZipId = 0,
                        DemogInfoZipPlusId = 0,
                        FromDate = null,
                        HouseNumber = "",
                        StateAbbrev = "TN",
                        ToDate = null,
                        ZipCode = "600001",
                        ZipPlus4 = "",
                    },
                    DeliveryMethodModel = new ApiDeliveryMethodModel
                    {
                        DeliveryMethodId = DeliveryMethodEnum.ShipToDeliveryAddress,
                    },
                    PaymentMethodModel = new ApiPaymentMethodModel
                    {
                        PaymentModeId = PaymentModeEnum.PaymentGateway,
                    },
                },
                ShoppingCartItemModels = new List<ApiShoppingCartItemModel>
                {
                    new ApiShoppingCartItemModel
                    {
                        ItemId = 9,
                        OrderQty = 9,
                        OrderComments = "Ummachi Kapathu 1",
                    },
                    new ApiShoppingCartItemModel
                    {
                        ItemId = 18,
                        OrderQty = 18,
                        OrderComments = "Ummachi Kapathu 2",
                    },
                    new ApiShoppingCartItemModel
                    {
                        ItemId = 27,
                        OrderQty = 27,
                        OrderComments = "Ummachi Kapathu 3",
                    },
                },
            };
            return apiShoppingCartModel;
        }

        // POST: ProcessShoppingCart
        public void ProcessShoppingCart(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                var username = GetUserInfoFromJwtToken(apiShoppingCartModel.JwtToken, clientId, ipAddress, execUniqueId, loggedInUserId);
                apiShoppingCartModel.JwtToken = null;
                if (username == null)
                {
                    username = "rramaswamy1@hotmail.com";
                }
                if (username == null)
                {
                    throw new Exception("Invalid access");
                }
                ApplicationDataContext.OpenSqlConnection();
                ApplicationDataContext.GetPersonInfoFromEmailAddress(username, out PersonModel personModel, out CorpAcctModel corpAcctModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (personModel == null)
                {
                    throw new Exception("Invalid user");
                }
                apiShoppingCartModel.ShoppingCartSummaryModel = new ApiShoppingCartSummaryModel
                {
                    CorpAcctModel = corpAcctModel,
                    EmailAddress = username,
                    PersonId = personModel.PersonId,
                    FirstName = personModel.FirstName,
                    LastName = personModel.LastName,
                };
                apiShoppingCartModel.BusinessInfoModel = BusinessInfo(null, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                UpdateDeliveryAddressInfo(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateShoppingCartItems(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateDiscounts(apiShoppingCartModel, apiShoppingCartModel.ShoppingCartSummaryModel.PersonId.Value, apiShoppingCartModel.ShoppingCartSummaryModel.CorpAcctModel.CorpAcctId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalculateShoppingCartTotals(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                var salesTaxListModels = GetSalesTaxListModels(apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                var salesTaxCaptionIds = LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("SalesTaxType", "");
                CalculateSalesTax(apiShoppingCartModel, salesTaxListModels, salesTaxCaptionIds, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (apiShoppingCartModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                {
                    ;
                }
                else
                {
                    CalculateDeliveryCharges(apiShoppingCartModel, salesTaxListModels, salesTaxCaptionIds, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                CalculateShoppingCartSummaryTotals(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                var creditCardDataModel = BuildCreditCardDataModel(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                CreditCardServiceBL creditCardServiceBL = new CreditCardServiceBL();
                var creditCardProcessStatus = creditCardServiceBL.ProcessCreditCard(creditCardDataModel, ApplicationDataContext.SqlConnectionObject, out string processMessage, out object creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                creditCardDataModel.CreditCardKVPs = null;
                apiShoppingCartModel.CreditCardDataModel = creditCardDataModel;
                apiShoppingCartModel.RazorPayResponse = (RazorPayResponse)creditCardResponseObject;
                ApplicationDataContext.CloseSqlConnection();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // START : Private Methods
        private void CalculateDeliveryCharges(ApiShoppingCartModel apiShoppingCartModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            DeliveryChargeModel deliveryChargeModel = GetDeliveryChargeModel(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId); ;
            if (deliveryChargeModel != null)
            {
                var shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
                var shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded;
                var fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
                var shoppingCartItemSummaryModelsFromCount = apiShoppingCartModel.ShoppingCartItemSummaryModels.Count;
                apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                (
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = shippingAndHandlingChargesRate,
                        ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
                        OrderAmount = shippingAndHandlingChargesAmount + fuelCharges,
                        OrderComments = null,
                        OrderQty = apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                        OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                    }
                );
                foreach (var salesTaxListModel in salesTaxListModels)
                {
                    var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                    apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                    (
                        new ApiShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemRate = shippingAndHandlingChargesRate,
                            ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
                            OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
                            OrderComments = null,
                            OrderQty = apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
                            OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
                        }
                    );
                }
                if (!apiShoppingCartModel.ShoppingCartSummaryModel.CorpAcctModel.ShippingAndHandlingCharges)
                {
                    var shoppingCartItemSummaryModelsToCount = apiShoppingCartModel.ShoppingCartItemSummaryModels.Count;
                    apiShoppingCartModel.ShoppingCartItemSummaryModels.AddRange(apiShoppingCartModel.ShoppingCartItemSummaryModels.GetRange(shoppingCartItemSummaryModelsFromCount, shoppingCartItemSummaryModelsToCount - shoppingCartItemSummaryModelsFromCount));
                    for (int i = shoppingCartItemSummaryModelsToCount; i < apiShoppingCartModel.ShoppingCartItemSummaryModels.Count; i++)
                    {
                        apiShoppingCartModel.ShoppingCartItemSummaryModels[i].ItemShortDesc = "Discount - " + apiShoppingCartModel.ShoppingCartItemSummaryModels[i].ItemShortDesc;
                        apiShoppingCartModel.ShoppingCartItemSummaryModels[i].OrderAmount = -1 * apiShoppingCartModel.ShoppingCartItemSummaryModels[i].OrderAmount;
                    }
                }
            }
        }

        private void CalculateShoppingCartItems(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemModel itemModel;
            try
            {
                //int a = 1, b = 0, c = a / b;
                foreach (var shoppingCartItemModel in apiShoppingCartModel.ShoppingCartItemModels)
                {
                    itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItemModel.ItemId);
                    //shoppingCartItemModel.ItemId = itemModel.ItemId;
                    shoppingCartItemModel.DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecUnitValue);
                    shoppingCartItemModel.HeightValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductHeight").ItemSpecValue);
                    shoppingCartItemModel.HSNCode = itemModel.ItemSpecModelsForDisplay["HSNCode"].ItemSpecValueForDisplay;
                    shoppingCartItemModel.ItemDesc = itemModel.ItemDesc;
                    shoppingCartItemModel.ItemDiscountAmount = 0;
                    shoppingCartItemModel.ItemDiscountPercent = 0;
                    shoppingCartItemModel.ItemRate = itemModel.ItemRate;
                    shoppingCartItemModel.ItemRateBeforeDiscount = itemModel.ItemRate;
                    shoppingCartItemModel.ItemShortDesc = itemModel.ItemShortDesc;
                    shoppingCartItemModel.LengthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductLength").ItemSpecValue);
                    shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * itemModel.ItemRate;
                    //shoppingCartItemModel.OrderComments = shoppingCartItemModel.OrderComments;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * itemModel.ItemRate;
                    shoppingCartItemModel.OrderDetailTypeId = OrderDetailTypeEnum.Item;
                    //shoppingCartItemModel.OrderQty = shoppingCartItemModel.OrderQty;
                    shoppingCartItemModel.ProductCode = itemModel.ItemSpecModelsForDisplay["ProductCode"].ItemSpecValueForDisplay;
                    shoppingCartItemModel.ProductOrVolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecValue);
                    shoppingCartItemModel.ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductOrVolumetricWeight").ItemSpecUnitValue);
                    shoppingCartItemModel.ProductWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecValue);
                    shoppingCartItemModel.ProductWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWeight").ItemSpecUnitValue);
                    shoppingCartItemModel.VolumetricWeight = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "VolumetricWeight").ItemSpecValue);
                    shoppingCartItemModel.VolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "VolumetricWeight").ItemSpecUnitValue);
                    shoppingCartItemModel.VolumeValue = shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightCalc = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "WeightCalc").ItemSpecValue);
                    shoppingCartItemModel.WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "WeightCalc").ItemSpecUnitValue);
                    shoppingCartItemModel.WidthValue = float.Parse(itemModel.ItemSpecModels.First(x => x.ItemSpecMasterModel.SpecName == "ProductWidth").ItemSpecValue);
                    shoppingCartItemModel.ShoppingCartItemSummaryModels = new List<ApiShoppingCartItemModel>();
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        private void CalculateDiscounts(ApiShoppingCartModel shoppingCartModel, long personId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int a = 1, b = 0, c = a / b;
                string itemIds = "", prefixString = "";
                foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItemModels)
                {
                    itemIds += prefixString + shoppingCartItem.ItemId;
                    prefixString = ", ";
                }
                string sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId IN(" + itemIds + ")";
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ApiShoppingCartItemModel shoppingCartItemModel;
                while (sqlDataReader.Read())
                {
                    shoppingCartItemModel = shoppingCartModel.ShoppingCartItemModels.First(x => x.ItemId == long.Parse(sqlDataReader["ItemId"].ToString()));
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

        private void CalculateShoppingCartTotals(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
                foreach (var shoppingCartItemModel in apiShoppingCartModel.ShoppingCartItemModels)
                {
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount += shoppingCartItemModel.OrderQty;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue += shoppingCartItemModel.VolumeValue;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalc;
                }
                apiShoppingCartModel.ShoppingCartItemSummaryModels = new List<ApiShoppingCartItemModel>
                {
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Total Order Amount (#" + apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ") Wt: " + apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc + " Grams",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmountAfterDiscount,
                    },
                };
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        private void CalculateSalesTax(ApiShoppingCartModel apiShoppingCartModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var salesTaxListModel in salesTaxListModels)
            {
                var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
                if (salesTaxListModel.LineItemLevelName == "SUMMARY")
                {
                    apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                    (
                        new ApiShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemRate = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
                            ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
                            OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount * salesTaxListModel.SalesTaxRate / 100f,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                }
                else
                {
                    float totalSalesTaxAmount = 0, salesTaxAmount;
                    foreach (var shoppingCartItem in apiShoppingCartModel.ShoppingCartItemModels)
                    {
                        var itemSpecValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemSpecModels.ToList().First(x => x.ItemSpecMasterModel.SpecName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemSpecValue;
                        salesTaxAmount = float.Parse(itemSpecValue) * shoppingCartItem.OrderAmount.Value / 100f;
                        totalSalesTaxAmount += salesTaxAmount;
                        shoppingCartItem.ShoppingCartItemSummaryModels.Add
                        (
                            new ApiShoppingCartItemModel
                            {
                                ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                ItemRate = float.Parse(itemSpecValue),
                                OrderAmount = salesTaxAmount,
                            }
                        );
                        //apiShoppingCartModel.ShoppingCartItemSummaryModels[apiShoppingCartModel.ShoppingCartItemSummaryModels.Count - 1].OrderAmount += float.Parse(itemSpecValue) * shoppingCartItem.OrderAmount / 100f;
                    }
                    apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                    (
                        new ApiShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemRate = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
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

        private void CalculateShoppingCartSummaryTotals(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByGiftCert = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid =
                    apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByGiftCert +
                    apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard
                    ;
                for (int i = 0; i < apiShoppingCartModel.ShoppingCartItemSummaryModels.Count; i++)
                {
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount += apiShoppingCartModel.ShoppingCartItemSummaryModels[i].OrderAmount;
                }
                apiShoppingCartModel.ShoppingCartSummaryModel.BalanceDue =
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount -
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid
                    ;
                apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                (
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Total Invoice Amount",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
                    }
                );
                apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                (
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Amount Paid - Gift Cert",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByGiftCert,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByGiftCert,
                    }
                );
                apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                (
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Amount Paid - Credit Card",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCreditCard,
                    }
                );
                apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                (
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Total Amount Paid",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
                    }
                );
                apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                (
                    new ApiShoppingCartItemModel
                    {
                        ItemDesc = null,
                        ItemId = null,
                        ItemRate = null,
                        ItemRateBeforeDiscount = null,
                        ItemShortDesc = "Balance Due",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
                    }
                );
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        private string GetUserInfoFromJwtToken(string jwtToken, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var userName = JwtManager.GetUserNameFromToken(jwtToken);
            return userName;
        }

        private void UpdateDeliveryAddressInfo(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibBL archLibBL = new ArchLibBL();
            ApiDemogInfoAddressModel apiDemogInfoAddressModel = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel;
            SearchDataModel searchDataModel = new SearchDataModel
            {
                SearchType = "ZipCode",
                SearchKeyValuePairs = new Dictionary<string, string>
                {
                    { "DemogInfoCountryId", apiDemogInfoAddressModel.DemogInfoCountryId.ToString() },
                    { "ZipCode", apiDemogInfoAddressModel.ZipCode },
                },
            };
            List<Dictionary<string, string>> sqlQueryResults = archLibBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            foreach (var sqlQueryResult in sqlQueryResults)
            {
                if (
                    sqlQueryResult["DemogInfoCountryId"] == apiDemogInfoAddressModel.DemogInfoCountryId.ToString()
                    && sqlQueryResult["ZipCode"] == apiDemogInfoAddressModel.ZipCode
                   )
                {
                    apiDemogInfoAddressModel.CityName = sqlQueryResult["CityName"];
                    apiDemogInfoAddressModel.CountryAbbrev = sqlQueryResult["CountryAbbrev"];
                    apiDemogInfoAddressModel.CountryDesc = sqlQueryResult["CountryDesc"];
                    apiDemogInfoAddressModel.CountyName = sqlQueryResult["CountyName"];
                    apiDemogInfoAddressModel.DemogInfoCityId = long.Parse(sqlQueryResult["DemogInfoCityId"]);
                    apiDemogInfoAddressModel.DemogInfoCountyId = long.Parse(sqlQueryResult["DemogInfoCountyId"]);
                    apiDemogInfoAddressModel.DemogInfoSubDivisionId = long.Parse(sqlQueryResult["DemogInfoSubDivisionId"]);
                    apiDemogInfoAddressModel.DemogInfoZipId = long.Parse(sqlQueryResult["DemogInfoZipId"]);
                    apiDemogInfoAddressModel.DemogInfoZipPlusId = long.Parse(sqlQueryResult["DemogInfoZipPlusId"]);
                    apiDemogInfoAddressModel.StateAbbrev = sqlQueryResult["StateAbbrev"];
                    break;
                }
            }
            var demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneDemogInfoCountryId);
            apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
            demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneDemogInfoCountryId);
            apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;

            apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneFormatted = "+" + apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneNum).ToString("##### #####");
            apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneHref = apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneNum).ToString("###-###-####");

            apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneFormatted = "+" + apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneNum).ToString("##### #####");
            apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneHref = apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneNum).ToString("###-###-####");

            apiDemogInfoAddressModel.BuildingTypeDesc = apiDemogInfoAddressModel.BuildingTypeId == null ? "" : LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "BuildingType").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)apiDemogInfoAddressModel.BuildingTypeId).CodeDataNameDesc;
            apiDemogInfoAddressModel.BuildingTypeHouseNumber = string.IsNullOrWhiteSpace(apiDemogInfoAddressModel.BuildingTypeDesc) ? "" : (apiDemogInfoAddressModel.BuildingTypeDesc + " ");
            apiDemogInfoAddressModel.BuildingTypeHouseNumber += string.IsNullOrWhiteSpace(apiDemogInfoAddressModel.HouseNumber) ? "" : apiDemogInfoAddressModel.HouseNumber.Trim();

            apiShoppingCartModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodDesc = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)apiShoppingCartModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodId).CodeDataDesc0;
            apiShoppingCartModel.DeliveryInfoModel.PaymentMethodModel.PaymentModeDesc = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)apiShoppingCartModel.DeliveryInfoModel.PaymentMethodModel.PaymentModeId).CodeDataDesc0;
        }

        private CreditCardDataModel BuildCreditCardDataModel(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
            CreditCardDataModel creditCardDataModel = new CreditCardDataModel
            {
                CreditCardAmount = apiShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString("0.00"),
                CreditCardExpMM = null,
                CreditCardExpYear = null,
                CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
                CreditCardNumber = null,
                CreditCardProcessor = creditCardProcessor,
                CreditCardSecCode = null,
                CreditCardTranType = "PAYMENT",
                CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                EmailAddress = apiShoppingCartModel.ShoppingCartSummaryModel.EmailAddress,
                NameAsOnCard = "",//apiShoppingCartModel.ShoppingCartSummaryModel.,
                TelephoneNumber = apiShoppingCartModel.ShoppingCartSummaryModel.TelephoneNumber,
            };
            return creditCardDataModel;
        }

        private List<SalesTaxListModel> GetSalesTaxListModels(ApiDemogInfoAddressModel apiDemogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                 && x.DestDemogInfoCountryId == apiDemogInfoAddressModel.DemogInfoCountryId
                 && x.DestDemogInfoSubDivisionId == apiDemogInfoAddressModel.DemogInfoSubDivisionId
                 && apiDemogInfoAddressModel.DemogInfoZipId == x.DestDemogInfoZipId
                );
            if (!salesTaxListModels.Any())
            {
                salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                    && x.DestDemogInfoCountryId == apiDemogInfoAddressModel.DemogInfoCountryId
                    && x.DestDemogInfoSubDivisionId == apiDemogInfoAddressModel.DemogInfoSubDivisionId
                    && x.DestDemogInfoZipId == null
                );
            }
            if (!salesTaxListModels.Any())
            {
                salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
                (
                    x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
                    && x.DestDemogInfoCountryId == apiDemogInfoAddressModel.DemogInfoCountryId
                    && x.DestDemogInfoSubDivisionId == null
                    && x.DestDemogInfoZipId == null
                );
            }
            return salesTaxListModels;
        }

        private DeliveryChargeModel GetDeliveryChargeModel(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShippingService shippingService = new ShippingService();
            DeliveryChargeModel deliveryChargeModel;
            DemogInfoAddressModel demogInfoAddressModel = new DemogInfoAddressModel
            {
                DemogInfoCountryId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoCountryId,
                DemogInfoSubDivisionId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId,
                DemogInfoCountyId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoCountyId,
                DemogInfoCityId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoCityId,
                DemogInfoZipId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoZipId,
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
    }
}
