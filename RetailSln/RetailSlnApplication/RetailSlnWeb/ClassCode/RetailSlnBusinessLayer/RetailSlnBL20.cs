using ArchitectureLibraryDocumentService;
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
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        //GET Category
        public CategoryModel Category(long? categoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                CategoryModel categoryModel;
                if (categoryId == null)
                {
                    categoryModel = new CategoryModel
                    {
                        CategoryTypeId = CategoryTypeEnum.RegularCategory,
                        CategoryStatusId = CategoryStatusEnum.Active,
                    };
                }
                else
                {
                    categoryModel = ApplicationDataContext.GetCategory((long)categoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                return categoryModel;
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
        //POST Category
        public void Category(ref CategoryModel categoryModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                if (categoryModel.CategoryId == null)
                {
                    ApplicationDataContext.CreateCategory(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    categoryModel.ImageName = UploadFile(categoryModel.CategoryId.Value, null, "Category", "~/Documents/Images/Category", categoryModel.HttpPostedFileBase, 252, 252, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.ModifyCategoryImageName(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    categoryModel = new CategoryModel
                    {
                        CategoryStatusId = CategoryStatusEnum.Active,
                        CategoryTypeId = CategoryTypeEnum.RegularCategory,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                            ResponseMessages = new List<string>
                            {
                                "Category added successfully",
                                "Please continue with the next category",
                            },
                        }
                    };
                }
                else
                {
                    ApplicationDataContext.ModifyCategory(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (categoryModel.HttpPostedFileBase != null)
                    {
                        categoryModel.ImageName = UploadFile(categoryModel.CategoryId.Value, categoryModel.ImageName, "Category", "~/Documents/Images/Category", categoryModel.HttpPostedFileBase, 252, 252, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.ModifyCategoryImageName(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    categoryModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                        ResponseMessages = new List<string>
                        {
                            "Category updated successfully",
                            "Please continue to edit the category",
                        },
                    };
                }
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
        //GET CategoryHierList
        public CategoryHierListModel CategoryHierList(long parentCategoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            var categoryLayoutModel = RetailSlnCache.CategoryLayoutModels[parentCategoryId];
            var parentCategoryModel = categoryLayoutModel.ParentCategoryModel;
            CategoryHierListModel categoryHierListModel = new CategoryHierListModel();
            categoryHierListModel.CategoryModels = categoryLayoutModel.CategoryModels;
            //RetailSlnCache.CategoryHierModels.First(x => x.ParentCategoryId == parentCategoryId).ParentCategoryModel.CategoryModels;
            return categoryHierListModel;
        }
        //GET CategoryItem
        public CategoryItemListModel CategoryItem(long parentCategoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                CategoryItemListModel categoryItemListModel = new CategoryItemListModel
                {
                    ParentCategoryModel = ApplicationDataContext.GetCategory(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ItemModelsAssigned = ApplicationDataContext.GetItemsAssigned(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ItemModelsUnassigned = ApplicationDataContext.GetItemsUnassigned(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                return categoryItemListModel;
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
        //POST CategoryItem
        public void CategoryItem(ref CategoryItemListModel categoryItemListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                modelStateDictionary.Clear();
                categoryItemListModel.ResponseObjectModel = new ResponseObjectModel();
                ApplicationDataContext.OpenSqlConnection();
                if (categoryItemListModel.ParentCategoryModel.CategoryTypeId == CategoryTypeEnum.FeaturedItem)
                {
                    long assignedCount = categoryItemListModel.Assigned == null ? 0 : categoryItemListModel.Assigned.Count;
                    long unassignedCount = categoryItemListModel.Unassigned == null ? 0 : categoryItemListModel.Unassigned.Count;
                    if (assignedCount + unassignedCount > 4)
                    {
                        modelStateDictionary.AddModelError("", "Total items to be selected for Featurd Items cannot exceed 4");
                        categoryItemListModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
                    }
                }
                if (modelStateDictionary.IsValid)
                {
                    ApplicationDataContext.CreateCategoryItem(categoryItemListModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    categoryItemListModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = "Items Assign/Unassign completed successfully!!!",
                    };
                }
                categoryItemListModel.ParentCategoryModel = ApplicationDataContext.GetCategory((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                categoryItemListModel.ItemModelsAssigned = ApplicationDataContext.GetItemsAssigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                categoryItemListModel.ItemModelsUnassigned = ApplicationDataContext.GetItemsUnassigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                modelStateDictionary.AddModelError("", "Error while saving assign/unassign");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "Should issue exists, please contact our support personnel");
                categoryItemListModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                };
                categoryItemListModel.ParentCategoryModel = ApplicationDataContext.GetCategory((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                categoryItemListModel.ItemModelsAssigned = ApplicationDataContext.GetItemsAssigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                categoryItemListModel.ItemModelsUnassigned = ApplicationDataContext.GetItemsUnassigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
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
            return;
        }
        //GET CategoryList
        public CategoryListModel CategoryList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                //categoryListModel.CategoryModels.Remove(categoryListModel.CategoryModels.First(x => x.CategoryId == 0));
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
        //GET Item
        public ItemModel Item(long? itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                ItemModel itemModel;
                if (itemId == null)
                {
                    itemModel = new ItemModel
                    {
                        ItemAttribModels = new List<ItemAttribModel>(),
                        ItemStatusId = ItemStatusEnum.InStock,
                        ItemTypeId = ItemTypeEnum.RegularItem,
                    };
                    foreach (var itemAttribMasterModel in RetailSlnCache.ItemAttribMasterModels)
                    {
                        itemModel.ItemAttribModels.Add
                        (
                            new ItemAttribModel
                            {
                                ItemAttribId = -1,
                                ItemAttribMasterId = itemAttribMasterModel.ItemAttribMasterId.Value,
                                ItemAttribMasterModel = itemAttribMasterModel,
                                ItemAttribValue = "",
                            }
                        );
                    }
                }
                else
                {
                    itemModel = ApplicationDataContext.GetItem((long)itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                return itemModel;
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
        //POST Item
        public void Item(ref ItemModel itemModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                if (itemModel.ItemId == null)
                {
                    ApplicationDataContext.AddItem(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    itemModel.ImageName = UploadFile(itemModel.ItemId.Value, null, "Item", "~/Documents/Images/Items", itemModel.HttpPostedFileBase, 180, 180, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.UpdItemImageName(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //Create ItemAttrib Rows from Master (all rows)
                    ApplicationDataContext.AddItemAttribs(itemModel.ItemId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //Create rows for ItemImage (3 rows) & ItemImageSecSet (9 rows)
                    int i, j;
                    string[] imageDescs = { "Front View", "Top View", "Side View" };
                    int[] imageHeights = { 450, 630, 810 };
                    List<ItemImageModel> itemImageModels = new List<ItemImageModel>();
                    ItemImageModel itemImageModel;
                    for (i = 0; i < imageDescs.Length; i++)
                    {
                        itemImageModels.Add
                        (
                            itemImageModel = new ItemImageModel
                            {
                                ImageDesc = imageDescs[i],
                                SeqNum = i + 1,
                                ItemImageSrcSetModels = new List<ItemImageSrcSetModel>(),
                            }
                        );
                        for (j = 0; j < imageHeights.Length; j++)
                        {
                            itemImageModel.ItemImageSrcSetModels.Add
                            (
                                new ItemImageSrcSetModel
                                {
                                    ImageHeight = imageHeights[j],
                                    ImageHeightUnit = "px",
                                    ImageName = "",
                                    ImageWidth = imageHeights[j],
                                    ImageWidthUnit = "px",
                                    SeqNum = j + 1,
                                }
                            );
                        }
                    }
                    string imageExtension = itemModel.ImageName.Substring(itemModel.ImageName.IndexOf('.'));
                    ApplicationDataContext.AddItemImagesItemImageSrcSets(itemModel.ItemId.Value, itemImageModels, imageExtension, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    itemModel = new ItemModel
                    {
                        ItemStatusId = ItemStatusEnum.InStock,
                        ItemTypeId = ItemTypeEnum.RegularItem,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                            ResponseMessages = new List<string>
                            {
                                "Item added successfully",
                                "Please continue with the next item",
                            },
                        }
                    };
                }
                else
                {
                    ApplicationDataContext.ModifyItem(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (itemModel.HttpPostedFileBase != null)
                    {
                        itemModel.ImageName = UploadFile(itemModel.ItemId.Value, itemModel.ImageName, "Item", "~/Documents/Images/Items", itemModel.HttpPostedFileBase, 180, 180, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.ModifyItemImageName(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    itemModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                        ResponseMessages = new List<string>
                        {
                            "Item edited successfully",
                            "Please continue with editing item",
                        },
                    };
                }
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
        //GET ItemAttribList
        public ItemAttribListModel ItemAttribList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ItemAttribListModel itemAttribListModel = new ItemAttribListModel
                {
                    ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
                    ItemAttribModels = ApplicationDataContext.GetItemAttribs(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                return itemAttribListModel;
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
        //POST ItemAttribList
        public void ItemAttribList(ref ItemAttribListModel itemAttribListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ApplicationDataContext.UpdItemAttribs(itemAttribListModel.ItemAttribModels, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                itemAttribListModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseMessages = new List<string>
                    {
                        "Item attributes updated successfully!!!",
                        "Please continue to edit as necessry",
                        "If you are done with editing click the appropriate menu",
                    },
                    ResponseTypeId = ResponseTypeEnum.Success
                };
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemAttribListModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Error,
                    ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                };
                modelStateDictionary.AddModelError("", "Error while updating Item Attributes");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, please contact support personnel");
                return;
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
        //GET ItemBundleItemList
        public ItemBundleItemListModel ItemBundleItemList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ItemBundleItemListModel itemBundleItemListModel = new ItemBundleItemListModel
                {
                    ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
                    ItemBundleItemModels = ApplicationDataContext.GetItemBundleItems(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                foreach (var itemBundleItemModel in itemBundleItemListModel.ItemBundleItemModels)
                {
                    itemBundleItemModel.BundledItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemBundleItemModel.BundledItemId);
                }
                return itemBundleItemListModel;
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
        //GET ItemImageList
        public ItemImageListModel ItemImageList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ItemImageListModel itemImageListModel = new ItemImageListModel
                {
                    ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
                    ItemImageModels = ApplicationDataContext.GetItemImages(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                return itemImageListModel;
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
        //POST ItemImageList
        public void ItemImageList(ref ItemImageListModel itemImageListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ArchLibDocumentBL archLibDocumentBL = new ArchLibDocumentBL();
                string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"ItemImages\";
                int i;
                int[] resizedHeight = { 0, 0, 0 }, resizedWidth = { 0, 0, 0 };
                string[] serverFullFileName = { "", "", "" };
                foreach (var itemImageModel in itemImageListModel.ItemImageModels)
                {
                    if (itemImageModel.HttpPostedFileBase != null)
                    {
                        //Resize the uploaded image to the 3 sizes and replace the files
                        for (i = 0; i < itemImageModel.ItemImageSrcSetModels.Count; i++)
                        {
                            resizedHeight[i] = itemImageModel.ItemImageSrcSetModels[i].ImageHeight;
                            resizedWidth[i] = itemImageModel.ItemImageSrcSetModels[i].ImageWidth;
                            serverFullFileName[i] = documentsImagesDirectoryName + itemImageModel.ItemImageSrcSetModels[i].ImageName;
                        }
                        archLibDocumentBL.CreateResizedImageFile(itemImageModel.HttpPostedFileBase, resizedHeight, resizedWidth, serverFullFileName, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                }
                long itemId = itemImageListModel.ItemModel.ItemId.Value;
                itemImageListModel = new ItemImageListModel
                {
                    ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
                    ItemImageModels = ApplicationDataContext.GetItemImages(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                return;
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
        //GET ItemList
        public ItemListModel ItemList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    ItemModels = RetailSlnCache.ItemModels,//ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
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
        //GET ItemSpec
        public ItemSpecModel ItemSpec(long? itemSpecId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ItemSpecModel itemSpecModel;
                if (itemSpecId == null)
                {
                    itemSpecModel = new ItemSpecModel();
                }
                else
                {
                    ApplicationDataContext.OpenSqlConnection();
                    itemSpecModel = ApplicationDataContext.GetItemSpec(itemSpecId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (itemSpecModel == null)
                    {
                        modelStateDictionary.AddModelError("", "Error while getting Item Spec for Id " + itemSpecId);
                    }
                }
                return itemSpecModel;
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
        //POST ItemSpec
        public void ItemSpec(ref ItemSpecModel itemSpecModel, ref ItemSpecListModel itemSpecListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                itemSpecModel.ResponseObjectModel = new ResponseObjectModel();
                if (itemSpecModel.ItemSpecId == null)
                {
                    itemSpecModel.SeqNum = ApplicationDataContext.GetItemSpecMaxSeqNum(itemSpecModel.ItemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    itemSpecModel.SeqNum++;
                    ApplicationDataContext.AddItemSpec(itemSpecModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    itemSpecModel.ResponseObjectModel.ValidationSummaryMessage = "Item spec added successfully!!!";
                }
                else
                {
                    ApplicationDataContext.UpdItemSpec(itemSpecModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    itemSpecModel.ResponseObjectModel.ValidationSummaryMessage = "Item spec updated successfully!!!";
                }
                itemSpecListModel.ItemSpecModels = ApplicationDataContext.GetItemSpecs(itemSpecModel.ItemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemSpecModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
                modelStateDictionary.AddModelError("", "Error while saving Item Spec");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, please contact support personnel");
                //throw;
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
        //DELETE ItemSpec
        public ItemSpecListAddEditModel ItemSpecDelete(long itemSpecId, long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemSpecListAddEditModel itemSpecListAddEditModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                ApplicationDataContext.ItemSpecDelete(itemSpecId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                itemSpecListAddEditModel = new ItemSpecListAddEditModel
                {
                    ItemSpecListModel = ItemSpecList(itemId, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ItemSpecModel = new ItemSpecModel
                    {
                        ItemId = itemId,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ValidationSummaryMessage = "Item spec deleted successfully!!!",
                        },
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemSpecListAddEditModel = new ItemSpecListAddEditModel
                {
                    ItemSpecModel = new ItemSpecModel
                    {
                        ItemId = itemId,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                        },
                    },
                };
                modelStateDictionary.AddModelError("", "Error while deleting Item Spec");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, please contact support personnel");
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
            return itemSpecListAddEditModel;
        }
        //GET ItemSpecList
        public ItemSpecListModel ItemSpecList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ItemSpecListModel itemSpecListModel = new ItemSpecListModel
                {
                    ItemId = itemId,
                    ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
                    ItemSpecModels = ItemSpecs(itemId, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
                };
                itemSpecListModel.ItemSpecModels.Insert(0, new ItemSpecModel { ItemSpecLabelText = "Attribute(s)" });
                return itemSpecListModel;
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
        //GET ItemSpecListAddEditModel
        public ItemSpecListAddEditModel ItemSpecListAddEdit(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                ItemSpecListAddEditModel itemDescListAddEditModel = new ItemSpecListAddEditModel
                {
                    ItemSpecListModel = ItemSpecList(itemId, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ItemSpecModel = new ItemSpecModel
                    {
                        ItemId = itemId,
                    },
                };
                return itemDescListAddEditModel;
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
        //ItemSpecs
        public List<ItemSpecModel> ItemSpecs(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                List<ItemSpecModel> itemSpecModels = ApplicationDataContext.GetItemSpecs(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

                return itemSpecModels;
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
        //GET OrderList
        public List<OrderListModel> OrderList(long? personId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                List<OrderListModel> orderListModels = ApplicationDataContext.GetOrderLists(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId );
                return orderListModels;
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
        //GET RefreshCache
        public void RefreshCacheSubmit(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                RetailSlnCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
                //int x = 1, y = 0, z = x / y;
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
                }
                catch
                {

                }
            }
        }
        //GET GiftCertList
        public GiftCertListModel GiftCertList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                GiftCertListModel giftCertListModel = new GiftCertListModel();
                giftCertListModel.GiftCertModels = ApplicationDataContext.GetGiftCerts(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                return giftCertListModel;
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
        public void AssignItemAttribMaster(ItemAttribListModel itemAttribListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i = -1;
            foreach (var itemAttribMasterModel in RetailSlnCache.ItemAttribMasterModels)
            {
                i++;
                itemAttribListModel.ItemAttribModels[i].ItemAttribMasterId = itemAttribMasterModel.ItemAttribMasterId.Value;
                itemAttribListModel.ItemAttribModels[i].ItemAttribMasterModel = itemAttribMasterModel;
            }
        }
        private string UploadFile(long id, string serverFileName, string fileNamePrefix, string serverRelativeUrl, HttpPostedFileBase httpPostedFileBase,  int resizedWidth, int resizedHeight, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string serverPathName = Utilities.GetServerMapPath(serverRelativeUrl);
            if (!string.IsNullOrWhiteSpace(serverFileName))
            {
                File.Delete(serverPathName + @"\" + serverFileName);
            }
            string imageFileName = "";
            using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
            {
                byte[] fileData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
                using (var memoryStream = new MemoryStream(fileData))
                {
                    var originalImage = Image.FromStream(memoryStream);
                    var resizedImage = new Bitmap(originalImage, resizedWidth, resizedHeight);
                    ImageConverter imageConverter = new ImageConverter();
                    var contentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
                    imageFileName = fileNamePrefix + id + Path.GetExtension(httpPostedFileBase.FileName);
                    string fullFileName = serverPathName + @"\" + imageFileName;
                    using (FileStream fileStream = File.Create(fullFileName))
                    {
                        fileStream.Write(contentByteData, 0, contentByteData.Length);
                        fileStream.Close();
                    }
                    memoryStream.Close();
                }
                binaryReader.Close();
            }
            return imageFileName;
        }
    }
}
