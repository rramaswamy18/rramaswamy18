using ArchitectureLibraryUtility;
using SchoolPrdCacheBusinessLayer;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdCacheData
{
    public static class SchoolPrdCache
    {
        public static List<ClassDetailModel> ClassDetailModels { set; get; }
        public static List<ClassFeesModel> ClassFeesModels { set; get; }
        public static List<ClassListModel> ClassListModels { set; get; }
        public static List<ClassScheduleModel> ClassScheduleModels { set; get; }
        public static List<ClassSessionModel> ClassSessionModels { set; get; }
        public static List<HolidayModel> HolidayModels { set; get; }
        public static List<InitialSignatureModel> InitialSignatureModels { set; get; }
        public static List<InitialSignatureDetailModel> InitialSignatureDetailModels { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SchoolPrdCacheBL schoolPrdCacheBL = new SchoolPrdCacheBL();
            schoolPrdCacheBL.Initialize(out List<ClassDetailModel> classDetailModels, out List<ClassFeesModel> classFeesModels, out List<ClassListModel> classListModels, out List<ClassScheduleModel> classScheduleModels, out List<ClassSessionModel> classSessionModels, out List<HolidayModel> holidayModels, out List<InitialSignatureModel> initialSignatureModels, out List<InitialSignatureDetailModel> initialSignatureDetailModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            ClassDetailModels = classDetailModels;
            ClassFeesModels = classFeesModels;
            ClassListModels = classListModels;
            ClassScheduleModels = classScheduleModels;
            ClassSessionModels = classSessionModels;
            HolidayModels = holidayModels;
            InitialSignatureModels = initialSignatureModels;
            InitialSignatureDetailModels = initialSignatureDetailModels;
            BuildCacheModels(classDetailModels, classFeesModels, classListModels, classScheduleModels, classSessionModels, initialSignatureModels, initialSignatureDetailModels, ipAddress, execUniqueId, loggedInUserId);
        }
        private static void BuildCacheModels(List<ClassDetailModel> classDetailModels, List<ClassFeesModel> classFeesModels, List<ClassListModel> classListModels, List<ClassScheduleModel> classScheduleModels, List<ClassSessionModel> classSessionModels, List<InitialSignatureModel> initialSignatureModels, List<InitialSignatureDetailModel> initialSignatureDetailModels, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var classListModel in classListModels)
            {
                classListModel.ClassSessionModels = classSessionModels.FindAll(x => x.ClassListId == classListModel.ClassListId);
                classListModel.ClassFeesModels = classFeesModels.FindAll(x => x.ClassListId == classListModel.ClassListId);
            }
            foreach (var classSessionModel in classSessionModels)
            {
                classSessionModel.ClassListModel = classListModels.First(x => x.ClassListId == classSessionModel.ClassListId);
                classSessionModel.ClassDetailModels = classDetailModels.FindAll(x => x.ClassSessionId == classSessionModel.ClassSessionId);
                classSessionModel.ClassScheduleModels = classScheduleModels.FindAll(x => x.ClassSessionId == classSessionModel.ClassSessionId);
            }
            foreach (var classScheduleModel in classScheduleModels)
            {
                classScheduleModel.ClassSessionModel = classSessionModels.First(x => x.ClassSessionId == classScheduleModel.ClassSessionId);
            }
            foreach (var classFeesModel in classFeesModels)
            {
                classFeesModel.ClassListModel = classListModels.First(x => x.ClassListId == classFeesModel.ClassListId);
            }
            foreach (var initialSignatureModel in initialSignatureModels)
            {
                initialSignatureModel.InitialSignatureDetailModels = initialSignatureDetailModels.FindAll(x => x.InitialSignatureId == initialSignatureModel.InitialSignatureId);
            }
            foreach (var initialSignatureDetailModel in initialSignatureDetailModels)
            {
                initialSignatureDetailModel.InitialSignatureModel = initialSignatureModels.First(x => x.InitialSignatureId == initialSignatureDetailModel.InitialSignatureId);
            }
        }
    }
}
