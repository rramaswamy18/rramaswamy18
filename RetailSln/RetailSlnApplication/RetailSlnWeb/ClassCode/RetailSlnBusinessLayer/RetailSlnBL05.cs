﻿using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryShippingLibrary;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        public void BuildDeliveryInfoLookup(PaymentInfo1Model paymentInfoModel, bool apiFlag, bool webFlag, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            if (apiFlag)
            {
                paymentInfoModel.DeliveryAddressModel.DeliveryDemogInfoCountrys = RetailSlnCache.DeliveryDemogInfoCountrys;
                paymentInfoModel.DeliveryAddressModel.DeliveryDemogInfoCountrySubDivisions = RetailSlnCache.DeliveryDemogInfoCountryStates;
            }
            if (webFlag)
            {
                paymentInfoModel.DeliveryAddressModel.DeliveryDemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems;
                paymentInfoModel.DeliveryAddressModel.DeliveryDemogInfoSubDivisionSelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySubDivisionSelectListItems;
            }
        }
        //private void UpdateDeliveryAddressInfo(PaymentInfo1Model paymentInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    DemogInfoAddressModel demogInfoAddressModel = paymentInfoModel.DeliveryAddressModel.DeliveryAddressDataModel;
        //    SearchDataModel searchDataModel = new SearchDataModel
        //    {
        //        SearchType = "ZipCode",
        //        SearchKeyValuePairs = new Dictionary<string, string>
        //        {
        //            { "DemogInfoCountryId", demogInfoAddressModel.DemogInfoCountryId.ToString() },
        //            { "ZipCode", demogInfoAddressModel.ZipCode },
        //        },
        //    };
        //    List<Dictionary<string, string>> sqlQueryResults = archLibBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    foreach (var sqlQueryResult in sqlQueryResults)
        //    {
        //        if (
        //            sqlQueryResult["DemogInfoCountryId"] == demogInfoAddressModel.DemogInfoCountryId.ToString()
        //            && sqlQueryResult["ZipCode"] == demogInfoAddressModel.ZipCode
        //           )
        //        {
        //            demogInfoAddressModel.CityName = sqlQueryResult["CityName"];
        //            demogInfoAddressModel.CountryAbbrev = sqlQueryResult["CountryAbbrev"];
        //            demogInfoAddressModel.CountryDesc = sqlQueryResult["CountryDesc"];
        //            demogInfoAddressModel.CountyName = sqlQueryResult["CountyName"];
        //            demogInfoAddressModel.DemogInfoCityId = long.Parse(sqlQueryResult["DemogInfoCityId"]);
        //            demogInfoAddressModel.DemogInfoCountyId = long.Parse(sqlQueryResult["DemogInfoCountyId"]);
        //            demogInfoAddressModel.DemogInfoSubDivisionId = long.Parse(sqlQueryResult["DemogInfoSubDivisionId"]);
        //            demogInfoAddressModel.DemogInfoZipId = long.Parse(sqlQueryResult["DemogInfoZipId"]);
        //            demogInfoAddressModel.DemogInfoZipPlusId = long.Parse(sqlQueryResult["DemogInfoZipPlusId"]);
        //            demogInfoAddressModel.StateAbbrev = sqlQueryResult["StateAbbrev"];
        //            break;
        //        }
        //    }
        //    var demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneDemogInfoCountryId);
        //    apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
        //    demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneDemogInfoCountryId);
        //    apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneTelephoneCode = demogInfoCountryModel.TelephoneCode;
        //    apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneFormatted = "+" + apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneNum).ToString("##### #####");
        //    apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneHref = apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(apiShoppingCartModel.DeliveryInfoModel.AlternateTelephoneNum).ToString("###-###-####");
        //    apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneFormatted = "+" + apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneTelephoneCode.Value.ToString() + " " + long.Parse(apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneNum).ToString("##### #####");
        //    apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneHref = apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneTelephoneCode.Value.ToString() + "-" + long.Parse(apiShoppingCartModel.DeliveryInfoModel.PrimaryTelephoneNum).ToString("###-###-####");

        //    demogInfoAddressModel.BuildingTypeDesc = demogInfoAddressModel.BuildingTypeId == null ? "" : LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "BuildingType").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)demogInfoAddressModel.BuildingTypeId).CodeDataNameDesc;
        //    demogInfoAddressModel.BuildingTypeHouseNumber = string.IsNullOrWhiteSpace(demogInfoAddressModel.BuildingTypeDesc) ? "" : (demogInfoAddressModel.BuildingTypeDesc + " ");
        //    demogInfoAddressModel.BuildingTypeHouseNumber += string.IsNullOrWhiteSpace(demogInfoAddressModel.HouseNumber) ? "" : demogInfoAddressModel.HouseNumber.Trim();

        //    apiShoppingCartModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodDesc = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)apiShoppingCartModel.DeliveryInfoModel.DeliveryMethodModel.DeliveryMethodId).CodeDataDesc0;
        //    apiShoppingCartModel.DeliveryInfoModel.PaymentMethodModel.PaymentModeDesc = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId.First(y => y.CodeDataNameId == (int)apiShoppingCartModel.DeliveryInfoModel.PaymentMethodModel.PaymentModeId).CodeDataDesc0;
        //}
        //private void CalculateDeliveryCharges(PaymentInfoModel paymentInfoModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    DeliveryChargeModel deliveryChargeModel = GetDeliveryChargeModel(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId); ;
        //    if (deliveryChargeModel != null)
        //    {
        //        var shippingAndHandlingChargesRate = deliveryChargeModel.DeliveryChargeAmount + deliveryChargeModel.DeliveryChargeAmountAdditional;
        //        var shippingAndHandlingChargesAmount = shippingAndHandlingChargesRate * paymentInfoModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded;
        //        var fuelCharges = shippingAndHandlingChargesAmount * deliveryChargeModel.FuelChargePercent / 100f;
        //        var shoppingCartItemSummaryModelsFromCount = paymentInfoModel.ShoppingCartItemSummaryModels.Count;
        //        paymentInfoModel.ShoppingCartItemSummaryModels.Add
        //        (
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = shippingAndHandlingChargesRate,
        //                ItemShortDesc = "Shipping, Handling & Fuel Charges (" + deliveryChargeModel.FuelChargePercent + "%) " + paymentInfoModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded + " KG - " + deliveryChargeModel.DeliveryModeId + " - " + deliveryChargeModel.DeliveryTime,
        //                OrderAmount = shippingAndHandlingChargesAmount + fuelCharges,
        //                OrderComments = null,
        //                OrderQty = paymentInfoModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
        //                OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
        //            }
        //        );
        //        foreach (var salesTaxListModel in salesTaxListModels)
        //        {
        //            var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
        //            paymentInfoModel.ShoppingCartItemSummaryModels.Add
        //            (
        //                new ApiShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemRate = shippingAndHandlingChargesRate,
        //                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " on S&H, Fuel Charges (" + salesTaxListModel.SalesTaxRate + "%)",
        //                    OrderAmount = (shippingAndHandlingChargesAmount + fuelCharges) * salesTaxListModel.SalesTaxRate / 100f,
        //                    OrderComments = null,
        //                    OrderQty = paymentInfoModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.ShippingHandlingCharges,
        //                }
        //            );
        //        }
        //        if (!paymentInfoModel.ShoppingCartSummaryModel.CorpAcctModel.ShippingAndHandlingCharges)
        //        {
        //            var shoppingCartItemSummaryModelsToCount = paymentInfoModel.ShoppingCartItemSummaryModels.Count;
        //            paymentInfoModel.ShoppingCartItemSummaryModels.AddRange(paymentInfoModel.ShoppingCartItemSummaryModels.GetRange(shoppingCartItemSummaryModelsFromCount, shoppingCartItemSummaryModelsToCount - shoppingCartItemSummaryModelsFromCount));
        //            for (int i = shoppingCartItemSummaryModelsToCount; i < paymentInfoModel.ShoppingCartItemSummaryModels.Count; i++)
        //            {
        //                paymentInfoModel.ShoppingCartItemSummaryModels[i].ItemShortDesc = "Discount - " + paymentInfoModel.ShoppingCartItemSummaryModels[i].ItemShortDesc;
        //                paymentInfoModel.ShoppingCartItemSummaryModels[i].OrderAmount = -1 * paymentInfoModel.ShoppingCartItemSummaryModels[i].OrderAmount;
        //            }
        //        }
        //    }
        //}
        //private void CalculateShoppingCartItems(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ItemModel itemModel;
        //    try
        //    {
        //        //int a = 1, b = 0, c = a / b;
        //        foreach (var shoppingCartItemModel in apiShoppingCartModel.ShoppingCartItemModels)
        //        {
        //            itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == shoppingCartItemModel.ItemId);
        //            //shoppingCartItemModel.ItemId = itemModel.ItemId;
        //            shoppingCartItemModel.DimensionUnitId = (DimensionUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductHeight").ItemAttribUnitValue);
        //            shoppingCartItemModel.HeightValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductHeight").ItemAttribValue);
        //            shoppingCartItemModel.HSNCode = itemModel.ItemAttribModelsForDisplay["HSNCode"].ItemAttribValueForDisplay;
        //            shoppingCartItemModel.ItemDesc = itemModel.ItemDesc;
        //            shoppingCartItemModel.ItemDiscountAmount = 0;
        //            shoppingCartItemModel.ItemDiscountPercent = 0;
        //            shoppingCartItemModel.ItemRate = itemModel.ItemRate;
        //            shoppingCartItemModel.ItemRateBeforeDiscount = itemModel.ItemRate;
        //            shoppingCartItemModel.ItemShortDesc = itemModel.ItemShortDesc;
        //            shoppingCartItemModel.LengthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductLength").ItemAttribValue);
        //            shoppingCartItemModel.OrderAmount = shoppingCartItemModel.OrderQty * itemModel.ItemRate;
        //            //shoppingCartItemModel.OrderComments = shoppingCartItemModel.OrderComments;
        //            shoppingCartItemModel.OrderAmountBeforeDiscount = shoppingCartItemModel.OrderQty * itemModel.ItemRate;
        //            shoppingCartItemModel.OrderDetailTypeId = OrderDetailTypeEnum.Item;
        //            //shoppingCartItemModel.OrderQty = shoppingCartItemModel.OrderQty;
        //            shoppingCartItemModel.ProductCode = itemModel.ItemAttribModelsForDisplay["ProductCode"].ItemAttribValueForDisplay;
        //            shoppingCartItemModel.ProductOrVolumetricWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductOrVolumetricWeight").ItemAttribValue);
        //            shoppingCartItemModel.ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductOrVolumetricWeight").ItemAttribUnitValue);
        //            shoppingCartItemModel.ProductWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWeight").ItemAttribValue);
        //            shoppingCartItemModel.ProductWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWeight").ItemAttribUnitValue);
        //            shoppingCartItemModel.VolumetricWeight = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "VolumetricWeight").ItemAttribValue);
        //            shoppingCartItemModel.VolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "VolumetricWeight").ItemAttribUnitValue);
        //            shoppingCartItemModel.VolumeValue = shoppingCartItemModel.LengthValue * shoppingCartItemModel.WidthValue * shoppingCartItemModel.HeightValue;
        //            shoppingCartItemModel.WeightCalc = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "WeightCalc").ItemAttribValue);
        //            shoppingCartItemModel.WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "WeightCalc").ItemAttribUnitValue);
        //            shoppingCartItemModel.WidthValue = float.Parse(itemModel.ItemAttribModels.First(x => x.ItemAttribMasterModel.AttribName == "ProductWidth").ItemAttribValue);
        //            shoppingCartItemModel.ShoppingCartItemSummaryModels = new List<ApiShoppingCartItemModel>();
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private void CalculateDiscounts(ApiShoppingCartModel shoppingCartModel, long personId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int a = 1, b = 0, c = a / b;
        //        string itemIds = "", prefixString = "";
        //        foreach (var shoppingCartItem in shoppingCartModel.ShoppingCartItemModels)
        //        {
        //            itemIds += prefixString + shoppingCartItem.ItemId;
        //            prefixString = ", ";
        //        }
        //        string sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId IN(" + itemIds + ")";
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        ApiShoppingCartItemModel shoppingCartItemModel;
        //        while (sqlDataReader.Read())
        //        {
        //            shoppingCartItemModel = shoppingCartModel.ShoppingCartItemModels.First(x => x.ItemId == long.Parse(sqlDataReader["ItemId"].ToString()));
        //            shoppingCartItemModel.ItemDiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString());
        //            shoppingCartItemModel.ItemRate = shoppingCartItemModel.ItemRateBeforeDiscount.Value * (100 - shoppingCartItemModel.ItemDiscountPercent) / 100f;
        //            shoppingCartItemModel.OrderAmount = shoppingCartItemModel.ItemRate * shoppingCartItemModel.OrderQty;
        //        }
        //        sqlDataReader.Close();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private void CalculateShoppingCartTotals(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc = 0;
        //        foreach (var shoppingCartItemModel in apiShoppingCartModel.ShoppingCartItemModels)
        //        {
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount += shoppingCartItemModel.OrderQty;
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight += shoppingCartItemModel.ProductOrVolumetricWeight;
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountBeforeDiscount += shoppingCartItemModel.OrderAmountBeforeDiscount;
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalDiscountAmount += shoppingCartItemModel.ItemDiscountAmount;
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount += shoppingCartItemModel.OrderAmount;
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalVolumeValue += shoppingCartItemModel.VolumeValue;
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc += shoppingCartItemModel.WeightCalc;
        //        }
        //        apiShoppingCartModel.ShoppingCartItemSummaryModels = new List<ApiShoppingCartItemModel>
        //        {
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemRateBeforeDiscount = null,
        //                ItemShortDesc = "Total Order Amount (#" + apiShoppingCartModel.ShoppingCartSummaryModel.TotalItemsCount + ") Wt: " + apiShoppingCartModel.ShoppingCartSummaryModel.TotalWeightCalc + " Grams",
        //                OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
        //                OrderAmountBeforeDiscount = null,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.TotalOrderAmountAfterDiscount,
        //            },
        //        };
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeightRounded = (long)Math.Ceiling(apiShoppingCartModel.ShoppingCartSummaryModel.TotalProductOrVolumetricWeight.Value / 1000f);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private void CalculateSalesTax(ApiShoppingCartModel apiShoppingCartModel, List<SalesTaxListModel> salesTaxListModels, List<CodeDataModel> salesTaxCaptionIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    foreach (var salesTaxListModel in salesTaxListModels)
        //    {
        //        var salesTaxCaptionId = salesTaxCaptionIds.First(x => x.CodeDataNameId == (int)salesTaxListModel.SalesTaxCaptionId);
        //        if (salesTaxListModel.LineItemLevelName == "SUMMARY")
        //        {
        //            apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //            (
        //                new ApiShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemRate = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
        //                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0 + " (" + salesTaxListModel.SalesTaxRate + "%)",
        //                    OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount * salesTaxListModel.SalesTaxRate / 100f,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
        //                }
        //            );
        //        }
        //        else
        //        {
        //            float totalSalesTaxAmount = 0, salesTaxAmount;
        //            foreach (var shoppingCartItem in apiShoppingCartModel.ShoppingCartItemModels)
        //            {
        //                var itemAttribValue = RetailSlnCache.ItemModels.Find(x => x.ItemId == shoppingCartItem.ItemId).ItemAttribModels.ToList().First(x => x.ItemAttribMasterModel.AttribName == salesTaxListModel.SalesTaxCaptionId.ToString()).ItemAttribValue;
        //                salesTaxAmount = float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount.Value / 100f;
        //                totalSalesTaxAmount += salesTaxAmount;
        //                shoppingCartItem.ShoppingCartItemSummaryModels.Add
        //                (
        //                    new ApiShoppingCartItemModel
        //                    {
        //                        ItemShortDesc = salesTaxListModel.SalesTaxCaptionId.ToString(),
        //                        ItemRate = float.Parse(itemAttribValue),
        //                        OrderAmount = salesTaxAmount,
        //                    }
        //                );
        //                //apiShoppingCartModel.ShoppingCartItemSummaryModels[apiShoppingCartModel.ShoppingCartItemSummaryModels.Count - 1].OrderAmount += float.Parse(itemAttribValue) * shoppingCartItem.OrderAmount / 100f;
        //            }
        //            apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //            (
        //                new ApiShoppingCartItemModel
        //                {
        //                    ItemDesc = null,
        //                    ItemId = null,
        //                    ItemRate = apiShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount,
        //                    ItemShortDesc = salesTaxCaptionId.CodeDataDesc0,
        //                    OrderAmount = totalSalesTaxAmount,
        //                    OrderComments = null,
        //                    OrderQty = 1,
        //                    OrderDetailTypeId = OrderDetailTypeEnum.SalesTaxAmount,
        //                }
        //            );
        //        }
        //    }
        //}
        //private void CalculateShoppingCartSummaryTotals(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByGiftCert = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard = 0;
        //        apiShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid =
        //            apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByGiftCert +
        //            apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard
        //            ;
        //        for (int i = 0; i < apiShoppingCartModel.ShoppingCartItemSummaryModels.Count; i++)
        //        {
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount += apiShoppingCartModel.ShoppingCartItemSummaryModels[i].OrderAmount;
        //        }
        //        apiShoppingCartModel.ShoppingCartSummaryModel.BalanceDue =
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount -
        //            apiShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid
        //            ;
        //        apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //        (
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemRateBeforeDiscount = null,
        //                ItemShortDesc = "Total Invoice Amount",
        //                OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalInvoiceAmount,
        //                OrderAmountBeforeDiscount = null,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.TotalInvoiceAmount,
        //            }
        //        );
        //        apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //        (
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemRateBeforeDiscount = null,
        //                ItemShortDesc = "Amount Paid - Gift Cert",
        //                OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByGiftCert,
        //                OrderAmountBeforeDiscount = null,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByGiftCert,
        //            }
        //        );
        //        apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //        (
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemRateBeforeDiscount = null,
        //                ItemShortDesc = "Amount Paid - Credit Card",
        //                OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.AmountPaidByCreditCard,
        //                OrderAmountBeforeDiscount = null,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.AmountPaidByCreditCard,
        //            }
        //        );
        //        apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //        (
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemRateBeforeDiscount = null,
        //                ItemShortDesc = "Total Amount Paid",
        //                OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.TotalAmountPaid,
        //                OrderAmountBeforeDiscount = null,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.TotalAmountPaid,
        //            }
        //        );
        //        apiShoppingCartModel.ShoppingCartItemSummaryModels.Add
        //        (
        //            new ApiShoppingCartItemModel
        //            {
        //                ItemDesc = null,
        //                ItemId = null,
        //                ItemRate = null,
        //                ItemRateBeforeDiscount = null,
        //                ItemShortDesc = "Balance Due",
        //                OrderAmount = apiShoppingCartModel.ShoppingCartSummaryModel.BalanceDue,
        //                OrderAmountBeforeDiscount = null,
        //                OrderComments = null,
        //                OrderQty = 1,
        //                OrderDetailTypeId = OrderDetailTypeEnum.BalanceDue,
        //            }
        //        );
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private string GetUserInfoFromJwtToken(string jwtToken, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    var userName = JwtManager.GetUserNameFromToken(jwtToken);
        //    return userName;
        //}
        //private CreditCardDataModel BuildCreditCardDataModel(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    var creditCardProcessor = Utilities.GetApplicationValue("CreditCardProcessor");
        //    CreditCardDataModel creditCardDataModel = new CreditCardDataModel
        //    {
        //        CreditCardAmount = apiShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value.ToString("0.00"),
        //        CreditCardExpMM = null,
        //        CreditCardExpYear = null,
        //        CreditCardKVPs = GetCreditCardKVPs(creditCardProcessor, clientId),
        //        CreditCardNumber = null,
        //        CreditCardProcessor = creditCardProcessor,
        //        CreditCardSecCode = null,
        //        CreditCardTranType = "PAYMENT",
        //        CurrencyCode = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyAbbreviation"),
        //        EmailAddress = apiShoppingCartModel.ShoppingCartSummaryModel.EmailAddress,
        //        NameAsOnCard = "",//apiShoppingCartModel.ShoppingCartSummaryModel.,
        //        TelephoneNumber = apiShoppingCartModel.ShoppingCartSummaryModel.TelephoneNumber,
        //    };
        //    return creditCardDataModel;
        //}
        //private List<SalesTaxListModel> GetSalesTaxListModels(ApiDemogInfoAddressModel apiDemogInfoAddressModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    var salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
        //        (
        //            x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
        //         && x.DestDemogInfoCountryId == apiDemogInfoAddressModel.DemogInfoCountryId
        //         && x.DestDemogInfoSubDivisionId == apiDemogInfoAddressModel.DemogInfoSubDivisionId
        //         && apiDemogInfoAddressModel.DemogInfoZipId == x.DestDemogInfoZipId
        //        );
        //    if (!salesTaxListModels.Any())
        //    {
        //        salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
        //        (
        //            x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
        //            && x.DestDemogInfoCountryId == apiDemogInfoAddressModel.DemogInfoCountryId
        //            && x.DestDemogInfoSubDivisionId == apiDemogInfoAddressModel.DemogInfoSubDivisionId
        //            && x.DestDemogInfoZipId == null
        //        );
        //    }
        //    if (!salesTaxListModels.Any())
        //    {
        //        salesTaxListModels = ArchLibCache.SalesTaxListModels.FindAll
        //        (
        //            x => x.SrceDemogInfoCountryId == RetailSlnCache.DefaultDeliveryDemogInfoCountryId
        //            && x.DestDemogInfoCountryId == apiDemogInfoAddressModel.DemogInfoCountryId
        //            && x.DestDemogInfoSubDivisionId == null
        //            && x.DestDemogInfoZipId == null
        //        );
        //    }
        //    return salesTaxListModels;
        //}
        //private DeliveryChargeModel GetDeliveryChargeModel(ApiShoppingCartModel apiShoppingCartModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    ShippingService shippingService = new ShippingService();
        //    DeliveryChargeModel deliveryChargeModel;
        //    DemogInfoAddressModel demogInfoAddressModel = new DemogInfoAddressModel
        //    {
        //        DemogInfoCountryId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoCountryId,
        //        DemogInfoSubDivisionId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId,
        //        DemogInfoCountyId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoCountyId,
        //        DemogInfoCityId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoCityId,
        //        DemogInfoZipId = apiShoppingCartModel.DeliveryInfoModel.DeliveryAddressModel.DemogInfoZipId,
        //    };
        //    ShippingInputModel shippingInputModel = new ShippingInputModel
        //    {
        //        DestDemogInfoAddressModel = demogInfoAddressModel,
        //        SrceDemogInfoAddressModel = new DemogInfoAddressModel
        //        {
        //            DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //        },
        //        ShippingInputData = null,
        //    };
        //    deliveryChargeModel = (DeliveryChargeModel)shippingService.GetRate(shippingInputModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    return deliveryChargeModel;
        //}
    }
}