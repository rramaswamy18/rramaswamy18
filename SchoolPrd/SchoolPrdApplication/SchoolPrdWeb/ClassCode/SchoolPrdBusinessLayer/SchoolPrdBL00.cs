using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryTemplate;
using ArchitectureLibraryUtility;
using SchoolPrdCacheData;
using SchoolPrdDataLayer;
using SchoolPrdEnumerations;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SchoolPrdBusinessLayer
{
    public partial class SchoolPrdBL
    {
        //GET ClassEnrollList
        public ClassEnrollListModel ClassEnrollList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ClassEnrollListModel classEnrollListModel = new ClassEnrollListModel
                {
                    ResponseObjectModel = new ResponseObjectModel(),
                };
                ApplicationDataContext.OpenSqlConnection();
                try
                {
                    classEnrollListModel.ClassEnrollModels = ApplicationDataContext.GetClassEnrolls(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                catch
                {
                    classEnrollListModel.ClassEnrollModels = new List<ClassEnrollModel>();
                    modelStateDictionary.AddModelError("", "Error while loading class enroll list");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return classEnrollListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
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
        //GET ClassEnroll
        public ClassEnrollModel ClassEnroll(long? classEnrollId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ClassEnrollModel classEnrollModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                if (classEnrollId == null)
                {
                    classEnrollModel = new ClassEnrollModel
                    {
                        ResponseObjectModel = new ResponseObjectModel(),
                    };
                }
                else
                {
                    classEnrollModel = ApplicationDataContext.GetClassEnroll(classEnrollId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                classEnrollModel = new ClassEnrollModel
                {
                    ResponseObjectModel = new ResponseObjectModel { ColumnCount = 3 }
                };
                modelStateDictionary.AddModelError("", "Error while loading class enroll record");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, contact support");
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
            return classEnrollModel;
        }
        //POST ClassEnroll
        public void ClassEnroll(ref ClassEnrollModel classEnrollModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                if (classEnrollModel.ClassEnrollId == null)
                {
                    ApplicationDataContext.AddClassEnroll(classEnrollModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    classEnrollModel = new ClassEnrollModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ColumnCount = 3,
                            ResponseMessages = new List<string>
                            {
                                "Class enroll record added successfully",
                                "Continue to add a new record",
                                "Refresh Cache when needed",
                            },
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    };
                }
                else
                {
                    ApplicationDataContext.UpdClassEnroll(classEnrollModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    classEnrollModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ColumnCount = 3,
                        ResponseMessages = new List<string>
                        {
                            "Class enroll record updated successfully",
                            "Continue to update the record",
                            "Refresh Cache when needed",
                        },
                        ResponseTypeId = ResponseTypeEnum.Success,
                    };
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                classEnrollModel.ResponseObjectModel = new ResponseObjectModel { ColumnCount = 3, ListStyleType = "decimal" };
                modelStateDictionary.AddModelError("", "Error while saving Class List");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, contact support");
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
        //GET ClassListList
        public ClassListListModel ClassListList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ClassListListModel classListListModel = new ClassListListModel
                {
                    ResponseObjectModel = new ResponseObjectModel(),
                };
                ApplicationDataContext.OpenSqlConnection();
                try
                {
                    classListListModel.ClassListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                catch
                {
                    classListListModel.ClassListModels = new List<ClassListModel>();
                    modelStateDictionary.AddModelError("", "Error while loading Class List List");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return classListListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
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
        //GET ClassList
        public ClassListModel ClassList(long? classListId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ClassListModel classListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                if (classListId == null)
                {
                    classListModel = new ClassListModel
                    {
                        ResponseObjectModel = new ResponseObjectModel(),
                    };
                }
                else
                {
                    classListModel = ApplicationDataContext.GetClassList(classListId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                classListModel = new ClassListModel
                {
                    ResponseObjectModel = new ResponseObjectModel { ColumnCount = 3 }
                };
                modelStateDictionary.AddModelError("", "Error while loading class list record");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, contact support");
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
            return classListModel;
        }
        //POST ClassList
        public void ClassList(ref ClassListModel classListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                if (classListModel.ClassListId == null)
                {
                    ApplicationDataContext.AddClassList(classListModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    classListModel = new ClassListModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ColumnCount = 3,
                            ResponseMessages = new List<string>
                            {
                                "Class list record added successfully",
                                "Continue to add a new record",
                                "Refresh Cache when needed",
                            },
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    };
                }
                else
                {
                    ApplicationDataContext.UpdClassList(classListModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    classListModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ColumnCount = 3,
                        ResponseMessages = new List<string>
                        {
                            "Class list record updated successfully",
                            "Continue to update the record",
                            "Refresh Cache when needed",
                        },
                        ResponseTypeId = ResponseTypeEnum.Success,
                    };
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                classListModel.ResponseObjectModel = new ResponseObjectModel { ColumnCount = 3, ListStyleType = "decimal" };
                modelStateDictionary.AddModelError("", "Error while saving Class List");
                modelStateDictionary.AddModelError("", "Please try again");
                modelStateDictionary.AddModelError("", "If problem persists, contact support");
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
        //GET ClassSessionList
        public ClassSessionListModel ClassSessionList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ClassSessionListModel classSessionListModel = new ClassSessionListModel
                {
                    ClassSessionModels = ApplicationDataContext.GetClassSessions(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    //ClassSessionModels = SchoolPrdCache.ClassSessionModels,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return classSessionListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
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

        //Review the below
        //GET HolidayList
        public HolidayListModel HolidayList(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                HolidayListModel holidayListModel = new HolidayListModel
                {
                    //HolidayModels = ApplicationDataContext.GetHolidays(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    HolidayModels = SchoolPrdCache.HolidayModels,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return holidayListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
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
        //ClassScheduleList GET
        public ClassScheduleListModel ClassScheduleList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ClassScheduleListModel classScheduleListModel = new ClassScheduleListModel
                {
                    ResponseObjectModel = new ResponseObjectModel(),
                };
                ApplicationDataContext.OpenSqlConnection();
                try
                {
                    classScheduleListModel.ClassScheduleModels = ApplicationDataContext.GetClassSchedules(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                catch
                {
                    classScheduleListModel.ClassScheduleModels = new List<ClassScheduleModel>();
                    modelStateDictionary.AddModelError("", "Error while loading Class List List");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return classScheduleListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
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
        //GET ClassList
        public ClassScheduleModel ClassSchedule(long? classScheduleId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ClassScheduleModel classScheduleModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                if (classScheduleId == null)
                {
                    classScheduleModel = new ClassScheduleModel
                    {
                        ResponseObjectModel = new ResponseObjectModel(),
                    };
                }
                else
                {
                    classScheduleModel = ApplicationDataContext.GetClassSchedule(classScheduleId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                classScheduleModel = new ClassScheduleModel
                {
                    ResponseObjectModel = new ResponseObjectModel(),
                };
                modelStateDictionary.AddModelError("", "Error while loading Class List");
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
            return classScheduleModel;
        }
        //ClassScheduleList POST 
        public void ClassScheduleList(ref ClassScheduleListModel classScheduleListModel, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x1 = 1, y1 = 0, z1 = x1 / y1;
                ValidateClassSchedules(classScheduleListModel.ClassScheduleModels, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (modelStateDictionary.IsValid)
                {
                    try
                    {
                        ApplicationDataContext.OpenSqlConnection();
                        //Loop through the input and build a new collection - added new and modified
                        //List<ClassScheduleModel> classScheduleModels = new List<ClassScheduleModel>();
                        ClassScheduleModel classScheduleModelFromCache;
                        foreach (var classScheduleModel in classScheduleListModel.ClassScheduleModels)
                        {
                            if (classScheduleModel.ClassScheduleId == -1)
                            {
                                if (!string.IsNullOrWhiteSpace(classScheduleModel.StartDate) && !string.IsNullOrWhiteSpace(classScheduleModel.GraduationDate))
                                {
                                    ApplicationDataContext.CreateClassSchedule(classScheduleModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                                    classScheduleModelFromCache = new ClassScheduleModel
                                    {
                                        ClassScheduleId = classScheduleModel.ClassScheduleId,
                                        ClassScheduleDesc = "Schedule " + classScheduleModel.ClassScheduleId,
                                        ClassSessionId = classScheduleModel.ClassSessionId,
                                        GraduationDate = DateTime.Parse(classScheduleModel.GraduationDate).ToString("yyyy-MM-dd"),
                                        StartDate = DateTime.Parse(classScheduleModel.StartDate).ToString("yyyy-MM-dd"),
                                        StatusId = classScheduleModel.StatusId,
                                        ClassSessionModel = new ClassSessionModel
                                        {
                                            ClassSessionId = classScheduleModel.ClassSessionId,
                                            ClassSessionDesc = SchoolPrdCache.ClassSessionModels.First(x => x.ClassSessionId == classScheduleModel.ClassSessionId).ClassSessionDesc,
                                        },
                                    };
                                    SchoolPrdCache.ClassScheduleModels.Add(classScheduleModelFromCache);
                                    classScheduleModel.ClassScheduleId = -1;
                                    classScheduleModel.ClassScheduleDesc = "";
                                    classScheduleModel.StartDate = "";
                                    classScheduleModel.GraduationDate = "";
                                }
                            }
                            else
                            {
                                classScheduleModelFromCache = SchoolPrdCache.ClassScheduleModels.First(x => x.ClassScheduleId == classScheduleModel.ClassScheduleId);
                                if (classScheduleModelFromCache.StartDate != classScheduleModel.StartDate || classScheduleModelFromCache.GraduationDate != classScheduleModel.GraduationDate)
                                {
                                    ApplicationDataContext.ModifyClassSchedule(classScheduleModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                                    classScheduleModelFromCache.StartDate = classScheduleModel.StartDate;
                                    classScheduleModelFromCache.GraduationDate = classScheduleModel.GraduationDate;
                                }
                            }
                        }
                        SchoolPrdCache.ClassScheduleModels = SchoolPrdCache.ClassScheduleModels.OrderBy(x => x.StartDate).ThenBy(y => y.ClassSessionId).ToList();
                        BuildClassScheduleListModel(ref classScheduleListModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                        classScheduleListModel.ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
                        {
                            ResponseMessages = new List<string>
                            {
                                "Add / Edit for Class Schedule completed successfully",
                                "Feel free to continue to add new class schedule or edit existing ones",
                            },
                            ResponseTypeId = ResponseTypeEnum.Success,
                            ValidationSummaryMessage = "Process completed successfully!!!",
                        };
                    }
                    catch
                    {
                        modelStateDictionary.AddModelError("", "Error while saving to database");
                        modelStateDictionary.AddModelError("", "Please contact support personnel");
                        classScheduleListModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Error,
                            ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                        };
                    }
                }
                else
                {
                    classScheduleListModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                    };
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
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
        //ClassSessionList  GET
    }
}
