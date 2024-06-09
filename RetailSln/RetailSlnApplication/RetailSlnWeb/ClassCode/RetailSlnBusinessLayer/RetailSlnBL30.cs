using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
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
                BusinessInfoModel businessInfoModel = new BusinessInfoModel
                {
                    BaseUrl = ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", ""),
                    BusinessName1 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName1", ""),
                    BusinessName2 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName2", ""),
                    DemogInfoAddressModels = new List<DemogInfoAddressModel>(),
                    LogoImageName = "Image_000.webp",
                    LogoRelativeUrl = "/ClientSpecific/" + clientId + "/" + ArchLibCache.ClientName + "/Documents/Images/",
                };
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
                List<CategoryItemHierModel> categoryItemHierModels = RetailSlnCache.CategoryItemHierModels.FindAll(x => x.ParentCategoryId == parentCategoryId).OrderBy(x => x.SeqNum).ToList();
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
        public List<ItemModel> Items(string categoryIdParm, string pageNumParm, string rowCountParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                List<CategoryItemHierModel> categoryItemHierModels = RetailSlnCache.CategoryItemHierModels.FindAll(x => x.ParentCategoryId == categoryId).Skip(pageNum * rowCount).Take(rowCount).OrderBy(x => x.SeqNum).ToList();
                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (var categoryItemHierModel in categoryItemHierModels)
                {
                    itemModels.Add(categoryItemHierModel.ItemModel);
                }
                return itemModels;
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
                bool success = false;
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
                            DeliveryCountrys = RetailSlnCache.DeliveryCountrys,
                            DeliveryCountryStates = RetailSlnCache.DeliveryCountryStates,
                            DeliveryMethods = RetailSlnCache.DeliveryMethods,
                            PaymentModes = personExtn1Model.CorpAcctModel.CreditSale ? RetailSlnCache.PaymentMethodsCreditSale : RetailSlnCache.PaymentMethods,
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                ResponseMessages = new List<string>(),
                                ResponseTypeId = ResponseTypeEnum.Success,
                                ValidationSummaryMessage = "",
                            }
                        };
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Success");
                        success = true;
                    }
                    else
                    {
                        personModel.ResponseObjectModel.ResponseMessages = new List<string>
                        {
                            "Unable to login using credentials passed",
                        };
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Unable to login");
                    }
                }
                ArchLibDataContext.CloseSqlConnection();
                if (success)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Success");
                }
                else
                {
                    personModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                    personModel.ResponseObjectModel.ResponseMessages = new List<string>
                    {
                        "Error occurred while validating username / password",
                    };
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Error ocurred");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return apiLoginUserProfModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // POST: ProcessShoppingCart
        public void ProcessShoppingCart(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                var username = retailSlnBL.GetUserInfoFromJwtToken(apiShoppingCartModel.JwtToken, clientId, ipAddress, execUniqueId, loggedInUserId);
                apiShoppingCartModel.JwtToken = null;
                username = "test1@email.com";
                if (username == null)
                {
                    throw new Exception("Invalid access");
                }
                ApplicationDataContext.OpenSqlConnection();
                ApplicationDataContext.GetPersonInfoFromEmailAddress(username, out long personId, out CorpAcctModel corpAcctModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (personId == 0)
                {
                    throw new Exception("Invalid user");
                }
                apiShoppingCartModel.ShoppingCartSummaryModel = new ApiShoppingCartSummaryModel
                {
                    PersonId = personId,
                    CorpAcctModel = corpAcctModel,
                };
                UpdateDeliveryAddressInfo(apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                        ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + " - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
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
                    shoppingCartItemModel.DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductHeight").ItemAttribUnitValue);
                    shoppingCartItemModel.HeightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductHeight").ItemAttribValue);
                    shoppingCartItemModel.HSNCode = itemModel.ItemAttribModelsForDisplay["HSNCode"].ItemAttribValueForDisplay;
                    shoppingCartItemModel.ItemDesc = itemModel.ItemDesc;
                    shoppingCartItemModel.ItemDiscountAmount = null;
                    shoppingCartItemModel.ItemDiscountPercent = null;
                    shoppingCartItemModel.ItemRate = itemModel.ItemRate;
                    shoppingCartItemModel.ItemRateBeforeDiscount = itemModel.ItemRate;
                    shoppingCartItemModel.ItemShortDesc = itemModel.ItemShortDesc;
                    shoppingCartItemModel.LengthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductLength").ItemAttribValue);
                    shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * itemModel.ItemRate;
                    //shoppingCartItemModel.OrderComments = shoppingCartItemModel.OrderComments;
                    shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * itemModel.ItemRate;
                    shoppingCartItemModel.OrderDetailTypeId = OrderDetailTypeEnum.Item;
                    //shoppingCartItemModel.OrderQty = shoppingCartItemModel.OrderQty;
                    shoppingCartItemModel.ProductCode = itemModel.ItemAttribModelsForDisplay["ProductCode"].ItemAttribValueForDisplay;
                    shoppingCartItemModel.ProductOrVolumetricWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductOrVolumetricWeight").ItemAttribValue);
                    shoppingCartItemModel.ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductOrVolumetricWeight").ItemAttribUnitValue);
                    shoppingCartItemModel.ProductWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWeight").ItemAttribValue);
                    shoppingCartItemModel.ProductWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWeight").ItemAttribUnitValue);
                    shoppingCartItemModel.VolumetricWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "VolumetricWeight").ItemAttribValue);
                    shoppingCartItemModel.VolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "VolumetricWeight").ItemAttribUnitValue);
                    shoppingCartItemModel.VolumeValue = shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
                    shoppingCartItemModel.WeightCalc = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "WeightCalc").ItemAttribValue);
                    shoppingCartItemModel.WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "WeightCalc").ItemAttribUnitValue);
                    shoppingCartItemModel.WidthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWidth").ItemAttribValue);
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
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue = 0;
                apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
                foreach (var shoppingCartItemModel in apiShoppingCartModel.ShoppingCartItemModels)
                {
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount += shoppingCartItemModel.OrderQty;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
                    apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount += shoppingCartItemModel.OrderAmount;
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
                        ItemShortDesc = "Total Order Amount",
                        OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount,
                        OrderAmountBeforeDiscount = null,
                        OrderComments = null,
                        OrderQty = 1,
                        OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmount,
                    }
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
                            ItemRate = apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount,
                            ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
                            OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount * salesTaxListModel.SalesTaxRate / 100f,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                }
                else
                {
                    apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
                    (
                        new ApiShoppingCartItemModel
                        {
                            ItemDesc = null,
                            ItemId = null,
                            ItemRate = apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount,
                            ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
                            OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalShoppingCartAmount * salesTaxListModel.SalesTaxRate / 100f,
                            OrderComments = null,
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
                        }
                    );
                    foreach (var shoppingCartItem in apiShoppingCartModel.ShoppingCartItemModels)
                    {
                        var itemAttribValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemAttribModels.ToList().First(x => x.ItemAttribMasterModel.AttribName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemAttribValue;
                        shoppingCartItem.ShoppingCartItemSummaryModels.Add
                        (
                            new ApiShoppingCartItemModel
                            {
                                ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
                                ItemRate = float.Parse(itemAttribValue),
                                OrderAmount = float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount / 100f,
                            }
                        );
                        apiShoppingCartModel.ShoppingCartItemSummaryModels[apiShoppingCartModel.ShoppingCartItemSummaryModels.Count - 1].OrderAmount += float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount / 100f;
                    }
                }
            }
        }

        public string GetUserInfoFromJwtToken(string jwtToken, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var userName = JwtManager.GetUserNameFromToken(jwtToken);
            return userName;
        }

        public void UpdateDeliveryAddressInfo(ApiDemogInfoAddressModel apiDemogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibBL archLibBL = new ArchLibBL();
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
        }

        // START : Private Methods
        private CreditCardDataModel BuildCreditCardDataModel(ApiDemogInfoAddressModel apiDemogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            CreditCardDataModel creditCardDataModel = new CreditCardDataModel
            {
                //CreditCardAmount = shoppingCartModel.ShoppingCartSummaryItems.First(x => x.OrderDetailTypeId == OrderDetailTypeEnum.BalanceDue).OrderAmount.Value.ToString("0.00"),
                //CreditCardExpMM = null,
                //CreditCardExpYear = null,
                //CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
                //CreditCardNumber = null,
                //CreditCardProcessor = creditCardProcessor,
                //CreditCardSecCode = null,
                //CreditCardTranType = "PAYMENT",
                //CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
                //EmailAddress = sessionObjectModel.EmailAddress,
                //NameAsOnCard = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName,
                //TelephoneNumber = sessionObjectModel.PhoneNumber,
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
