using SchoolPrdDataLayer;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdCacheBusinessLayer
{
    public class SchoolPrdCacheBL
    {
        public void Initialize(out List<ClassDetailModel> classDetailModels, out List<ClassFeesModel> classFeesModels, out List<ClassListModel> classListModels, out List<ClassScheduleModel> classScheduleModels, out List<ClassSessionModel> classSessionModels, out List<HolidayModel> holidayModels, out List<InitialSignatureModel> initialSignatureModels, out List<InitialSignatureDetailModel> initialSignatureDetailModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ApplicationDataContext.OpenSqlConnection();
            classDetailModels = ApplicationDataContext.GetClassDetails(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            classFeesModels = ApplicationDataContext.GetClassFeess(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            classListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            classScheduleModels = ApplicationDataContext.GetClassSchedules(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            classSessionModels = ApplicationDataContext.GetClassSessions(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            holidayModels = ApplicationDataContext.GetHolidays(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            initialSignatureModels = ApplicationDataContext.GetInitialSignatures(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            initialSignatureDetailModels = ApplicationDataContext.GetInitialSignatureDetails(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            ApplicationDataContext.CloseSqlConnection();
        }
    }
}
