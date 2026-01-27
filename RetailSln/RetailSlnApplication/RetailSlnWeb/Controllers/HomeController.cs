using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Microsoft.Owin.BuilderProperties;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailSlnWeb.Controllers
{
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public partial class HomeController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: Index
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string id)
        {
            #region CodeGen Commented out
            //if (id == "CODEGEN01")
            //{
            //    CreateClassDefns();
            //}
            #endregion
            //int x = 1, y = 0, z = x / y;
            //Session.Timeout = 2;
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                string parentCategoryIdParm, pageNumParm;
                if (Session["LastVisitedParentCategoryId"] != null && long.TryParse(Session["LastVisitedParentCategoryId"].ToString(), out long parentCategoryIdTemp))
                {
                    parentCategoryIdParm = parentCategoryIdTemp.ToString();
                }
                else
                {
                    parentCategoryIdParm = null;
                }
                Session["LastVisitedParentCategoryId"] = parentCategoryIdParm;
                if (Session["LastVisitedPageNum"] != null && long.TryParse(Session["LastVisitedPageNum"].ToString(), out long pageNumTemp))
                {
                    pageNumParm = pageNumTemp.ToString();
                }
                else
                {
                    pageNumParm = null;
                }
                Session["LastVisitedPageNum"] = pageNumParm;
                string aspNetRoleNameProxy;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                if (sessionObjectModel == null)
                {
                    string absoluteUri = Request.Url.AbsoluteUri;
                    if (
                        absoluteUri.ToUpper().IndexOf("BULKORDER") > -1 || id?.ToUpper().IndexOf("BULKORDER") > -1 ||
                        absoluteUri.ToUpper().IndexOf("MARKETING") > -1 || id?.ToUpper().IndexOf("MARKETING") > -1 ||
                        absoluteUri.ToUpper().IndexOf("WHOLESALE") > -1 || id?.ToUpper().IndexOf("WHOLESALE") > -1
                       )
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        return RedirectToAction("LoginUserProf");
                    }
                    aspNetRoleNameProxy = "DEFAULTROLE";
                    //if (absoluteUri.ToUpper().IndexOf("REFERRAL") > -1 || id?.ToUpper().IndexOf("REFERRAL") > -1)
                    //{
                    //    aspNetRoleName = "REFERRALROLE";
                    //}
                    //else
                    //{
                    //    aspNetRoleName = "DEFAULTROLE";
                    //}
                }
                else
                {
                    aspNetRoleNameProxy = sessionObjectModel.AspNetRoleNameProxy;
                }
                switch (aspNetRoleNameProxy)
                {
                    case "APPLADMN1":
                    //case "MARKETINGROLE":
                    //case "SYSTADMIN":
                        actionResult = RedirectToAction("Index", "Dashboard");
                        break;
                    //case "REFERRALROLE":
                    //    OrderItemModel orderItemModel1 = retailSlnBL.OrderItem(aspNetRoleName, parentCategoryIdParm, pageNumParm, null, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //    actionResult = View("Index", orderItemModel1);
                    //    break;
                    default:
                        OrderItemModel orderItemModel = retailSlnBL.OrderItem(aspNetRoleNameProxy, parentCategoryIdParm, pageNumParm, null, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        actionResult = View("Index", orderItemModel);
                        break;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Index / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        #region CodeGen Commented out
        //private void CreateClassDefns()
        //{
        //    string directoryName = @"C:\Code\rramaswamy27\ArchLib\ArchLibSolution\ArchLib.Domain\";
        //    string directoryName2 = @"C:\Code\rramaswamy27\ArchLib\ArchLibSolution\ArchLib.Infrastructure\Configurations\";
        //    string directoryName3 = @"C:\Code\rramaswamy27\ArchLib\ArchLibSolution\ArchLib.Infrastructure\DbContexts\";
        //    string table_Schema, table_Name;
        //    StreamWriter streamWriter, streamWriter2, streamWriter3;
        //    SqlConnection sqlConnection = new SqlConnection("DATA SOURCE = .; INTEGRATED SECURITY = SSPI; INITIAL CATALOG = ArchLib");
        //    sqlConnection.Open();
        //    SqlConnection sqlConnection2 = new SqlConnection("DATA SOURCE = .; INTEGRATED SECURITY = SSPI; INITIAL CATALOG = ArchLib");
        //    sqlConnection2.Open();
        //    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME NOT IN('sysdiagrams') AND COLUMN_NAME NOT IN(TABLE_NAME + 'Id', 'Id', 'ClientId', 'AddUserId', 'AddUserName', 'AddDateTime', 'UpdUserId', '', 'UpdUserName', 'UpdDateTime') ORDER BY TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION", sqlConnection);
        //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //    bool sqlDataReaderRead = sqlDataReader.Read();
        //    streamWriter3 = new StreamWriter(directoryName3 + "ArchLibDbContext.cs");
        //    CreateDbContextHeader(streamWriter3);
        //    while (sqlDataReaderRead)
        //    {
        //        table_Schema = sqlDataReader["TABLE_SCHEMA"].ToString();
        //        table_Name = sqlDataReader["TABLE_NAME"].ToString();
        //        streamWriter = new StreamWriter(directoryName + table_Schema + @"\" + table_Name + ".cs");
        //        streamWriter2 = new StreamWriter(directoryName2 + table_Schema + @"\" + table_Name + "Configuration.cs");
        //        CreateClassDefnHeader(streamWriter, table_Schema, table_Name);
        //        CreateClassConfigHeader(streamWriter2, table_Schema, table_Name);
        //        streamWriter3.Write($"        public DbSet<{table_Name}> {table_Name}s {{ get; set; }}{Environment.NewLine}");
        //        while (sqlDataReaderRead && table_Schema == sqlDataReader["TABLE_SCHEMA"].ToString() && table_Name == sqlDataReader["TABLE_NAME"].ToString())
        //        {
        //            CreateClassDefnProperty(streamWriter, sqlDataReader);
        //            CreateClassConfigProperty(streamWriter2, sqlDataReader);
        //            sqlDataReaderRead = sqlDataReader.Read();
        //        }
        //        CreateClassConfigComputedColumn(streamWriter2, sqlConnection2, table_Schema, table_Name);
        //        CreateClassConfigUniqueConstraint(streamWriter2, sqlConnection2, table_Schema, table_Name);
        //        CreateForeignKey(streamWriter, table_Schema, table_Name, sqlConnection2);

        //        streamWriter2.Write("        }" + Environment.NewLine);
        //        streamWriter2.Write("    }" + Environment.NewLine);
        //        streamWriter2.Write("}" + Environment.NewLine);
        //        streamWriter2.Close();

        //        streamWriter.Write("    }" + Environment.NewLine);
        //        streamWriter.Write("}" + Environment.NewLine);
        //        streamWriter.Close();
        //    }

        //    streamWriter3.Write($"{Environment.NewLine}");
        //    streamWriter3.Write($"        protected override void OnModelCreating(ModelBuilder modelBuilder){Environment.NewLine}");
        //    streamWriter3.Write($"        {{{Environment.NewLine}");
        //    streamWriter3.Write($"            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchLibDbContext).Assembly);{Environment.NewLine}");
        //    streamWriter3.Write($"            base.OnModelCreating(modelBuilder);{Environment.NewLine}");
        //    streamWriter3.Write($"        }}{Environment.NewLine}");
        //    streamWriter3.Write($"{Environment.NewLine}");
        //    streamWriter3.Write($"        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder){Environment.NewLine}");
        //    streamWriter3.Write($"        {{{Environment.NewLine}");
        //    streamWriter3.Write($"            configurationBuilder.Conventions.Add(_ => new DefaultSuserNameConvention());{Environment.NewLine}");
        //    streamWriter3.Write($"        }}{Environment.NewLine}");
        //    streamWriter3.Write($"    }}{Environment.NewLine}");
        //    streamWriter3.Write($"}}{Environment.NewLine}");
        //    streamWriter3.Close();

        //    sqlDataReader.Close();
        //    sqlConnection2.Close();
        //    sqlConnection.Close();
        //}

        //private void CreateClassDefnHeader(StreamWriter streamWriter, string table_Schema, string table_Name)
        //{
        //    streamWriter.Write($"using ArchLib.Domain.Common;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Collections.Generic;{Environment.NewLine}");
        //    streamWriter.Write($"using System.ComponentModel.DataAnnotations.Schema;{Environment.NewLine}");
        //    streamWriter.Write($"using System.ComponentModel.DataAnnotations;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Linq;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Text;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Threading.Tasks;{Environment.NewLine}");
        //    streamWriter.Write($"{Environment.NewLine}");
        //    streamWriter.Write($"namespace ArchLib.Domain.{table_Schema}{Environment.NewLine}");
        //    streamWriter.Write($"{{{Environment.NewLine}");
        //    streamWriter.Write($"    public class {table_Name} : BaseEntity{Environment.NewLine}");
        //    streamWriter.Write($"    {{{Environment.NewLine}");
        //}

        //private void CreateClassConfigHeader(StreamWriter streamWriter, string table_Schema, string table_Name)
        //{
        //    streamWriter.Write($"using ArchLib.Domain.{table_Schema};{Environment.NewLine}");
        //    streamWriter.Write($"using ArchLib.Enums;{Environment.NewLine}");
        //    streamWriter.Write($"using Microsoft.EntityFrameworkCore;{Environment.NewLine}");
        //    streamWriter.Write($"using Microsoft.EntityFrameworkCore.Metadata.Builders;{Environment.NewLine}");
        //    streamWriter.Write($"using System;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Collections.Generic;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Linq;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Text;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Threading.Tasks;{Environment.NewLine}");
        //    streamWriter.Write($"namespace ArchLib.Domain.{table_Schema}{Environment.NewLine}");
        //    streamWriter.Write($"{{{Environment.NewLine}");
        //    streamWriter.Write($"    public class {table_Name}Configuration : IEntityTypeConfiguration<{table_Name}>{Environment.NewLine}");
        //    streamWriter.Write($"    {{{Environment.NewLine}");
        //    streamWriter.Write($"        public void Configure(EntityTypeBuilder<{table_Name}> entityTypeBuilder){Environment.NewLine}");
        //    streamWriter.Write($"        {{{Environment.NewLine}");
        //    streamWriter.Write($"            entityTypeBuilder.ToTable(\"{table_Name}\", SchemaType.{table_Schema}.ToString());{Environment.NewLine}");
        //    streamWriter.Write($"{Environment.NewLine}");
        //    //streamWriter.Write($"            //Primary Key{Environment.NewLine}");
        //    //streamWriter.Write($"            entityTypeBuilder.HasKey(e => e.Id){Environment.NewLine}");
        //    //streamWriter.Write($"                .HasName(\"{table_Name}_PK\");{Environment.NewLine}");
        //    //streamWriter.Write($"{Environment.NewLine}");
        //}

        //private void CreateDbContextHeader(StreamWriter streamWriter)
        //{
        //    streamWriter.Write($"using ArchLib.Domain.ArchLib;{Environment.NewLine}");
        //    streamWriter.Write($"using ArchLib.Infrastructure.Configurations;{Environment.NewLine}");
        //    streamWriter.Write($"using Microsoft.EntityFrameworkCore;{Environment.NewLine}");
        //    streamWriter.Write($"using System;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Collections.Generic;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Linq;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Text;{Environment.NewLine}");
        //    streamWriter.Write($"using System.Threading.Tasks;{Environment.NewLine}");
        //    streamWriter.Write($"{Environment.NewLine}");
        //    streamWriter.Write($"namespace ArchLib.Infrastructure.DbContexts{Environment.NewLine}");
        //    streamWriter.Write($"{{{Environment.NewLine}");
        //    streamWriter.Write($"    public class ArchLibDbContext : DbContext{Environment.NewLine}");
        //    streamWriter.Write($"    {{{Environment.NewLine}");
        //    streamWriter.Write($"        public ArchLibDbContext(DbContextOptions<ArchLibDbContext> options) : base(options){Environment.NewLine}");
        //    streamWriter.Write($"        {{{Environment.NewLine}");
        //    streamWriter.Write($"{Environment.NewLine}");
        //    streamWriter.Write($"        }}{Environment.NewLine}");
        //}

        //private void CreateClassDefnProperty(StreamWriter streamWriter, SqlDataReader sqlDataReader)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    string dataAnnotation, dataType, dataTypeComments, defaultValue;
        //    dataTypeComments = sqlDataReader["DATA_TYPE"].ToString();
        //    switch (sqlDataReader["DATA_TYPE"].ToString().ToUpper())
        //    {
        //        case "BIGINT":
        //            dataType = "long";
        //            defaultValue = "";
        //            break;
        //        case "BIT":
        //            dataType = "bool";
        //            defaultValue = "";
        //            break;
        //        case "DATE":
        //        case "DATETIME":
        //        case "DATETIME2":
        //            dataType = "DateTime";
        //            defaultValue = "";
        //            break;
        //        case "NUMERIC":
        //            dataType = "decimal";
        //            defaultValue = "";
        //            dataTypeComments += "(" + sqlDataReader["NUMERIC_PRECISION"].ToString() + ", " + sqlDataReader["NUMERIC_SCALE"].ToString() + ")";
        //            break;
        //        case "FLOAT":
        //            dataType = "float";
        //            defaultValue = "";
        //            break;
        //        case "INT": 
        //            dataType = "int";
        //            defaultValue = "";
        //            break;
        //        case "NVARCHAR":
        //        case "VARCHAR":
        //            dataType = "string";
        //            dataTypeComments += "(" + sqlDataReader["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")";
        //            if (sqlDataReader["IS_NULLABLE"].ToString() == "YES")
        //            {
        //                defaultValue = "";
        //            }
        //            else
        //            {
        //                defaultValue = " = default!;";
        //            }
        //            break;
        //        case "SMALLINT":
        //            dataType = "short";
        //            defaultValue = "";
        //            break;
        //        case "VARBINARY":
        //            dataType = "byte[]";
        //            defaultValue = "";
        //            break;
        //        default:
        //            dataType = "xyz";
        //            defaultValue = "";
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000xyz", "Table", sqlDataReader["TABLE_NAME"].ToString(), "Column", sqlDataReader["COLUMN_NAME"].ToString(), "Type", sqlDataReader["DATA_TYPE"].ToString());
        //            break;
        //    }
        //    if (sqlDataReader["IS_NULLABLE"].ToString() == "YES")
        //    {
        //        dataType += "?";
        //        dataTypeComments += " NULL";
        //        dataAnnotation = "[Column(TypeName = \"" + dataTypeComments + "\")]";
        //    }
        //    else
        //    {
        //        dataTypeComments += " NOT NULL";
        //        dataAnnotation = "[Required, Column(TypeName = \"" + dataTypeComments + "\")]";
        //    }
        //    streamWriter.Write($"        {dataAnnotation}{Environment.NewLine}");
        //    streamWriter.Write($"        public {dataType} {sqlDataReader["COLUMN_NAME"].ToString()} {{ get; set; }}{defaultValue} //{dataTypeComments}{Environment.NewLine}");
        //}

        //private void CreateClassConfigProperty(StreamWriter streamWriter, SqlDataReader sqlDataReader)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //}

        //private void CreateClassConfigComputedColumn(StreamWriter streamWriter, SqlConnection sqlConnection, string table_Schema, string table_Name)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    string sqlStmt = "";
        //    //sqlStmt += "" + Environment.NewLine;
        //    sqlStmt += $"        SELECT" + Environment.NewLine;
        //    sqlStmt += $"               SCHEMA_NAME(obj.schema_id) AS SchemaName" + Environment.NewLine;
        //    sqlStmt += $"              ,obj.name AS TableName" + Environment.NewLine;
        //    sqlStmt += $"              ,col.name AS ColumnName" + Environment.NewLine;
        //    sqlStmt += $"              ,col.definition AS ComputedColumnFormula" + Environment.NewLine;
        //    sqlStmt += $"              ,col.is_persisted AS IsPersisted" + Environment.NewLine;
        //    sqlStmt += $"          FROM" + Environment.NewLine;
        //    sqlStmt += $"               sys.computed_columns AS col" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN" + Environment.NewLine;
        //    sqlStmt += $"               sys.objects AS obj ON col.object_id = obj.object_id" + Environment.NewLine;
        //    sqlStmt += $"         WHERE SCHEMA_NAME(obj.schema_id) = '{table_Schema}'" + Environment.NewLine;
        //    sqlStmt += $"           AND obj.name = '{table_Name}'" + Environment.NewLine;
        //    sqlStmt += $"      ORDER BY" + Environment.NewLine;
        //    sqlStmt += $"               obj.name" + Environment.NewLine;
        //    sqlStmt += $"              ,col.name" + Environment.NewLine;
        //    SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //    while (sqlDataReader.Read())
        //    {
        //        streamWriter.Write($"            entityTypeBuilder.Property(p => p.{sqlDataReader["ColumnName"].ToString()}){Environment.NewLine}");
        //        streamWriter.Write($"                .HasComputedColumnSql(\"{sqlDataReader["ComputedColumnFormula"].ToString()}\");{Environment.NewLine}"); // SQL expression
        //    }
        //    sqlDataReader.Close();
        //}

        //private void CreateClassConfigUniqueConstraint(StreamWriter streamWriter, SqlConnection sqlConnection, string table_Schema, string table_Name)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    #region
        //    string sqlStmt = "";
        //    sqlStmt += $"        SELECT " + Environment.NewLine;
        //    sqlStmt += $"               sys.schemas.name AS table_schema" + Environment.NewLine;
        //    sqlStmt += $"              ,sys.tables.name AS table_name" + Environment.NewLine;
        //    sqlStmt += $"              ,sys.indexes.name" + Environment.NewLine;
        //    sqlStmt += $"              ,sys.indexes.is_primary_key" + Environment.NewLine;
        //    sqlStmt += $"              ,sys.indexes.index_id" + Environment.NewLine;
        //    sqlStmt += $"              ,sys.indexes.type_desc" + Environment.NewLine;
        //    sqlStmt += $"              ,OBJECT_NAME(index_columns.object_id) AS TableName" + Environment.NewLine;
        //    sqlStmt += $"              ,COL_NAME(index_columns.object_id, index_columns.column_id) AS ColumnName" + Environment.NewLine;
        //    sqlStmt += $"              ,index_columns.key_ordinal" + Environment.NewLine;
        //    sqlStmt += $"              ,sys.indexes.is_unique_constraint" + Environment.NewLine;
        //    sqlStmt += $"          FROM sys.indexes" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN sys.tables" + Environment.NewLine;
        //    sqlStmt += $"            ON sys.indexes.object_id = sys.tables.object_id" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN sys.schemas" + Environment.NewLine;
        //    sqlStmt += $"            ON sys.tables.schema_id = sys.schemas.schema_id" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN sys.index_columns" + Environment.NewLine;
        //    sqlStmt += $"            ON sys.indexes.object_id = index_columns.object_id" + Environment.NewLine;
        //    sqlStmt += $"           AND sys.indexes.index_id = index_columns.index_id" + Environment.NewLine;
        //    sqlStmt += $"         WHERE sys.schemas.name = '{table_Schema}'" + Environment.NewLine;
        //    sqlStmt += $"           AND sys.tables.name = '{table_Name}'" + Environment.NewLine;
        //    sqlStmt += $"      ORDER BY sys.indexes.name" + Environment.NewLine;
        //    sqlStmt += $"              ,index_columns.key_ordinal" + Environment.NewLine;
        //    SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //    #endregion
        //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //    bool sqlDataReaderRead = sqlDataReader.Read();
        //    bool is_primary_key, is_unique_constraint;
        //    string index_name, index_columns, prefixString, type_desc;
        //    while (sqlDataReaderRead)
        //    {
        //        index_name = sqlDataReader["name"].ToString();
        //        is_primary_key = bool.Parse(sqlDataReader["is_primary_key"].ToString());
        //        is_unique_constraint = bool.Parse(sqlDataReader["is_unique_constraint"].ToString());
        //        type_desc = sqlDataReader["type_desc"].ToString();
        //        index_columns = "";
        //        prefixString = "";
        //        while (sqlDataReaderRead && index_name == sqlDataReader["name"].ToString())
        //        {
        //            index_columns += prefixString + "e." + sqlDataReader["ColumnName"].ToString();
        //            prefixString = ", ";
        //            sqlDataReaderRead = sqlDataReader.Read();
        //        }
        //        streamWriter.Write($"{Environment.NewLine}");
        //        if (is_primary_key)
        //        {
        //            streamWriter.Write($"            //{index_name} ({type_desc}) Primary Key{Environment.NewLine}");
        //            streamWriter.Write($"            entityTypeBuilder.HasKey(e => new {{ {index_columns} }}){Environment.NewLine}");
        //            streamWriter.Write($"                   .HasName(\"{index_name}\");{Environment.NewLine}");
        //        }
        //        else
        //        {
        //            streamWriter.Write($"            //{index_name} ({type_desc}) Index{Environment.NewLine}");
        //            streamWriter.Write($"            entityTypeBuilder.HasIndex(e => new {{ {index_columns} }}){Environment.NewLine}");
        //            if (is_unique_constraint)
        //            {
        //                streamWriter.Write($"                   .IsUnique(){Environment.NewLine}");
        //            }
        //            if (type_desc == "CLUSTERED")
        //            {
        //                streamWriter.Write($"                   .IsClustered(){Environment.NewLine}");
        //            }
        //            streamWriter.Write($"                   .HasDatabaseName(\"{index_name}\");{Environment.NewLine}");
        //        }
        //        //streamWriter.Write($"            //{index_comments}{Environment.NewLine}");
        //        //streamWriter.Write($"            //{index_columns}{Environment.NewLine}");
        //    }
        //    sqlDataReader.Close();
        //}

        //private void CreateForeignKey(StreamWriter streamWriter, string table_Schema, string table_Name, SqlConnection sqlConnection)
        //{
        //    string sqlStmt = "";
        //    SqlCommand sqlCommand;
        //    SqlDataReader sqlDataReader;
        //    int index;
        //    #region
        //    sqlStmt += $"        SELECT" + Environment.NewLine;
        //    sqlStmt += $"               KCU1.TABLE_SCHEMA AS ReferencingSchema" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU1.TABLE_NAME AS ReferencingTableName" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU1.COLUMN_NAME AS ReferencingColumnName" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU2.TABLE_SCHEMA AS ReferencedSchema" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU2.TABLE_NAME AS ReferencedTableName" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU2.COLUMN_NAME AS ReferencedColumnName" + Environment.NewLine;
        //    sqlStmt += $"              ,RC.CONSTRAINT_NAME AS ForeignKeyName" + Environment.NewLine;
        //    sqlStmt += $"          FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 ON RC.CONSTRAINT_NAME = KCU1.CONSTRAINT_NAME" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU2 ON RC.UNIQUE_CONSTRAINT_NAME = KCU2.CONSTRAINT_NAME" + Environment.NewLine;
        //    sqlStmt += $"         WHERE" + Environment.NewLine;
        //    sqlStmt += $"               KCU2.TABLE_SCHEMA = '{table_Schema}'" + Environment.NewLine;
        //    sqlStmt += $"           AND KCU2.TABLE_NAME = '{table_Name}'" + Environment.NewLine;
        //    sqlStmt += $"      ORDER BY ReferencedSchema, ReferencedTableName, ReferencingSchema, ReferencingTableName" + Environment.NewLine;
        //    sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //    sqlDataReader = sqlCommand.ExecuteReader();
        //    #endregion
        //    index = -1;
        //    while (sqlDataReader.Read())
        //    {
        //        index++;
        //        streamWriter.Write($"        public List<{sqlDataReader["ReferencingTableName"].ToString()}>? {sqlDataReader["ReferencingTableName"].ToString()}s{index} {{ get; set; }}{Environment.NewLine}");
        //    }
        //    sqlDataReader.Close();
        //    //TODO - Reverse of above
        //    #region
        //    sqlStmt += $"        SELECT" + Environment.NewLine;
        //    sqlStmt += $"               KCU1.TABLE_SCHEMA AS ReferencingSchema" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU1.TABLE_NAME AS ReferencingTableName" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU1.COLUMN_NAME AS ReferencingColumnName" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU2.TABLE_SCHEMA AS ReferencedSchema" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU2.TABLE_NAME AS ReferencedTableName" + Environment.NewLine;
        //    sqlStmt += $"              ,KCU2.COLUMN_NAME AS ReferencedColumnName" + Environment.NewLine;
        //    sqlStmt += $"              ,RC.CONSTRAINT_NAME AS ForeignKeyName" + Environment.NewLine;
        //    sqlStmt += $"          FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 ON RC.CONSTRAINT_NAME = KCU1.CONSTRAINT_NAME" + Environment.NewLine;
        //    sqlStmt += $"    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU2 ON RC.UNIQUE_CONSTRAINT_NAME = KCU2.CONSTRAINT_NAME" + Environment.NewLine;
        //    sqlStmt += $"         WHERE" + Environment.NewLine;
        //    sqlStmt += $"               KCU1.TABLE_SCHEMA = '{table_Schema}'" + Environment.NewLine;
        //    sqlStmt += $"           AND KCU1.TABLE_NAME = '{table_Name}'" + Environment.NewLine;
        //    sqlStmt += $"      ORDER BY ReferencedSchema, ReferencedTableName, ReferencingSchema, ReferencingTableName" + Environment.NewLine;
        //    sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //    sqlDataReader = sqlCommand.ExecuteReader();
        //    #endregion
        //    index = -1;
        //    while (sqlDataReader.Read())
        //    {
        //        index++;
        //        streamWriter.Write($"        public {sqlDataReader["ReferencedTableName"].ToString()}? {sqlDataReader["ReferencedTableName"].ToString()}s{index} {{ get; set; }}{Environment.NewLine}");
        //    }
        //    sqlDataReader.Close();
        //}
        #endregion

        // GET: AboutUs
        [AllowAnonymous]
        [HttpGet]
        [Route("AboutUs")]
        public ActionResult AboutUs()
        {
            ViewData["ActionName"] = "AboutUs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                AboutUsModel AboutUsModel = archLibBL.AboutUs();
                actionResult = View("AboutUs", AboutUsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Temple Festivals / GET");
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: CheckIsAuthenticated
        [HttpGet]
        public JsonResult CheckIsAuthenticated(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            //System.Threading.Thread.Sleep(5000); //Sleep for 2 seconds to make sure session has timedout
            var sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            string aspNetUserId;
            if (sessionObjectModel != null)
            {
                aspNetUserId = sessionObjectModel.AspNetUserId;
            }
            else
            {
                aspNetUserId = string.Empty;
            }
            var isAuthenticated = User.Identity.IsAuthenticated;
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: IsAuthenticatedStatus", "isAuthenticated", isAuthenticated.ToString(), "aspNetUserId", aspNetUserId);
            //var isAuthenticated = User.Identity.IsAuthenticated && sessionObjectModel != null;
            isAuthenticated = isAuthenticated && sessionObjectModel != null;
            if (isAuthenticated)
            {
                ;
            }
            else
            {
                if (id == "1")
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Request.GetOwinContext().Authentication.SignOut();
                    Session["SessionObject"] = null;
                    Session.Abandon();
                }
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return Json(new { isAuthenticated }, JsonRequestBehavior.AllowGet);
        }

        // GET: CookiePolicy
        [AllowAnonymous]
        [HttpGet]
        [Route("CookiePolicy")]
        public ActionResult CookiePolicy()
        {
            ViewData["ActionName"] = "CookiePolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                CookiePolicyModel cookiePolicyModel = archLibBL.CookiePolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("CookiePolicy", cookiePolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: ContactUs
        [AllowAnonymous]
        [HttpGet]
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            ViewData["ActionName"] = "ContactUs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ContactUsModel contactUsModel = archLibBL.ContactUs(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ContactUs", contactUsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Update Password / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                Session["CaptchaAnswerContactUs"] = null;
                Session["CaptchaNumberContactUs0"] = null;
                Session["CaptchaNumberContactUs1"] = null;
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: ContactUs
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ContactUs(ContactUsModel contactUsModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(contactUsModel);
                archLibBL.ContactUs(ref contactUsModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberContactUs0", "CaptchaNumberContactUs1");
                contactUsModel.CaptchaAnswerContactUs = null;
                contactUsModel.CaptchaNumberContactUs0 = Session["CaptchaNumberContactUs0"].ToString();
                contactUsModel.CaptchaNumberContactUs1 = Session["CaptchaNumberContactUs1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                contactUsModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_ContactUsData", contactUsModel);
            actionResult = Json(new { success, processMessage, htmlString });
            //actionResult = PartialView("_ContactUsData", contactUsModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: Error
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        // GET: Error404
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Error404(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            string execUniqueId = Utilities.CreateExecUniqueId();
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            Session["RequestUrl"] = Request.Url.AbsoluteUri;
            ViewResult viewResult;
            Exception exception;
            if (Request.Url.AbsoluteUri.IndexOf("job_board_news") > -1)
            {
                exception = new Exception("SEO URL Not Found");
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00001000 :: 404 - SEOError - " + Request.HttpMethod + " - " + Request.Url.AbsoluteUri, exception);
                viewResult = View("SEOPage1");
            }
            else
            {
                exception = new Exception("Other URL Not Found");
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00001000 :: 404 - OtherError - " + Request.HttpMethod + " - " + Request.Url.AbsoluteUri, exception);
                viewResult = View();
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return viewResult;
        }

        // GET: FAQs
        [AllowAnonymous]
        [HttpGet]
        [Route("FAQs")]
        public ActionResult FAQs()
        {
            ViewData["ActionName"] = "FAQs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                FAQsModel fAQsModel = archLibBL.FAQs(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("FAQs", fAQsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "FAQs / GET");
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: Forbidden
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Forbidden()
        {
            return View();
            //string url = "/Home/Forbidden";
            //return JavaScript(string.Format("window.open('{0}', '_blank', 'left=100,top=100,width=500,height=500,toolbar=no,resizable=no,scrollable=yes');", url));
        }

        // GET: Home
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Home()
        {
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                actionResult = View("Home");
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Home / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        // GET: Logout
        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObject"] = null;
            Session["PaymentInfo"] = null;
            Session.Abandon();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return RedirectToAction("Index");
        }

        // GET: LoginUserProf
        [AllowAnonymous]
        [HttpGet]
        [Route("LoginUserProf")]
        public ActionResult LoginUserProf()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "LoginUserProf";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                LoginUserProfModel loginUserProfModel = archLibBL.LoginUserProf(RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                loginUserProfModel.OTPRequestModel.RequestType = "Login";
                actionResult = View("LoginUserProf", loginUserProfModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: LoginUserProfOTPRequest
        [HttpPost]
        public ActionResult LoginUserProfOTPRequest(OTPRequestModel oTPRequestModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "LoginUserProfOTPRequest";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString;
            OTPResponseModel oTPResponseModel = null;
            //int x = 1, y = 0, z = x / y;
            try
            {
                ModelState.Clear();
                TryValidateModel(oTPRequestModel);
                if (ModelState.IsValid)
                {
                    //oTPResponseModel = archLibBL.LoginUserProfOTPRequest(ref oTPRequestModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    oTPResponseModel = archLibBL.RegisterUserProfOTPRequest(ref oTPRequestModel, "LOGINUSERPROF", this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    oTPResponseModel.RequestType = "Login";
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error during OTP setup");
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPRequestData", oTPRequestModel);
            }
            if (ModelState.IsValid)
            {
                success = true;
                processMessage = "SUCCESS!!!";
            }
            else
            {
                success = false;
                processMessage = "ERROR???";
            }
            if (success)
            {
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPResponse", oTPResponseModel);
            }
            else
            {
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                oTPRequestModel.CaptchaAnswer = null;
                oTPRequestModel.CaptchaNumber0 = Session["CaptchaNumber0"].ToString();
                oTPRequestModel.CaptchaNumber1 = Session["CaptchaNumber1"].ToString();
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPRequestData", oTPRequestModel);
            }
            actionResult = Json(new { success, processMessage, htmlString });
            return actionResult;
        }

        // POST: LoginUserProfOTPResponse
        [HttpPost]
        public ActionResult LoginUserProfOTPResponse(OTPResponseModel oTPResponseModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "LoginUserProfOTPResponse";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            bool success;
            string processMessage, htmlString = "";
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(oTPResponseModel);
                if (ModelState.IsValid)
                {
                    //SessionObjectModel sessionObjectModel = archLibBL.LoginUserProfOTPResponse(ref oTPResponseModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    SessionObjectModel sessionObjectModel = archLibBL.RegisterUserProfOTPResponse(ref oTPResponseModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        if (sessionObjectModel.NewUser)
                        {
                            retailSlnBL.RegisterUserProfPersonExtn1(sessionObjectModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                        Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[sessionObjectModel.AspNetRoleName];
                        sessionObjectModel.AspNetRoleNameProxy = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
                        string currentLoggedInUserId = loggedInUserId;
                        htmlString = LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
                        success = true;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    }
                    else
                    {
                        success = false;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                    }
                }
                else
                {
                    success = false;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                ModelState.AddModelError("", "Error during OTP setup");
            }
            if (success)
            {
                processMessage = "SUCCESS!!!";
                actionResult = Json(new { success, processMessage, htmlString });
            }
            else
            {
                oTPResponseModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPResponseData", oTPResponseModel);
                actionResult = Json(new { success, processMessage, htmlString });
            }
            return actionResult;
        }

        #region Commented Code
        //// POST: LoginUserProf
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult LoginUserProf(LoginUserProfModel loginUserProfModel)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    bool success;
        //    string processMessage, htmlString;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        TryValidateModel(loginUserProfModel);
        //        string currentLoggedInUserId = loggedInUserId;
        //        SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, true, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[sessionObjectModel.AspNetRoleName];
        //        sessionObjectModel.AspNetRoleNameProxy = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
        //        if (ModelState.IsValid)
        //        {
        //            var redirectUrl = LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
        //            success = true;
        //            processMessage = "SUCCESS!!!";
        //            actionResult = Json(new { success, processMessage, redirectUrl });
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
        //        }
        //        else
        //        {
        //            loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //            };
        //            success = false;
        //            processMessage = "ERROR???";
        //            htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
        //            actionResult = Json(new { success, processMessage, htmlString });
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
        //        loginUserProfModel.CaptchaAnswerLogin = null;
        //        loginUserProfModel.CaptchaNumberLogin0 = Session["CaptchaNumberLogin0"].ToString();
        //        loginUserProfModel.CaptchaNumberLogin1 = Session["CaptchaNumberLogin1"].ToString();
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //        };
        //        htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
        //        success = false;
        //        processMessage = "ERROR???";
        //        actionResult = Json(new { success, processMessage, htmlString });
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}
        //// GET: OTP
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult OTP(string id, string emailAddress)
        //{
        //    ViewData["ActionName"] = "OTP";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    bool success;
        //    string processMessage, htmlString;
        //    string oTPExpiryDate, oTPExpiryTime, oTPExpiryDuration;
        //    try
        //    {
        //        OTPSendTypeEnum oTPSendTypeId;
        //        try
        //        {
        //            ModelState.Clear();
        //            oTPSendTypeId = (OTPSendTypeEnum)long.Parse(id);
        //            OTPModel oTPModel = archLibBL.OTP(id, emailAddress, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            success = ModelState.IsValid;
        //            if (success)
        //            {
        //                processMessage = "SUCCESS!!!";
        //                htmlString = "OTP generated successfully";
        //                oTPExpiryDate = oTPModel.OTPExpiryDate;
        //                oTPExpiryTime = oTPModel.OTPExpiryTime;
        //                oTPExpiryDuration = oTPModel.OTPExpiryDuration.ToString();
        //            }
        //            else
        //            {
        //                processMessage = "ERROR???";
        //                htmlString = "Error occurred while generating OTP";
        //                oTPExpiryDate = "Error";
        //                oTPExpiryTime = "Error";
        //                oTPExpiryDuration = "Error";
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //            success = false;
        //            processMessage = "ERROR???";
        //            htmlString = "Error while generating OTP";
        //            oTPExpiryDate = "Error";
        //            oTPExpiryTime = "Error";
        //            oTPExpiryDuration = "Error";
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        success = false;
        //        processMessage = "ERROR???";
        //        htmlString = "Error while generating OTP";
        //        oTPExpiryDate = "";
        //        oTPExpiryTime = "";
        //        oTPExpiryDuration = "";
        //    }
        //    actionResult = Json(new { success, processMessage, htmlString, oTPExpiryDate, oTPExpiryTime, oTPExpiryDuration }, JsonRequestBehavior.AllowGet);
        //    //Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
        //    return actionResult;
        //    //string url = "/Home/Forbidden";
        //    //return JavaScript(string.Format("window.open('{0}', '_blank', 'left=100,top=100,width=500,height=500,toolbar=no,resizable=no,scrollable=yes');", url));
        //}
        #endregion

        // GET: PicGallery
        [AllowAnonymous]
        [HttpGet]
        [Route("PicGallery")]
        public ActionResult PicGallery()
        {
            ViewData["ActionName"] = "PicGallery";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                PicGalleryModel picGalleryModel = archLibBL.PicGallery(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("PicGallery", picGalleryModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "PicGallery / GET");
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: PrivacyPolicy
        [AllowAnonymous]
        [HttpGet]
        [Route("PrivacyPolicy")]
        public ActionResult PrivacyPolicy()
        {
            ViewData["ActionName"] = "PrivacyPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                PrivacyPolicyModel privacyPolicyModel = archLibBL.PrivacyPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("PrivacyPolicy", privacyPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: RefundPolicy
        [AllowAnonymous]
        [HttpGet]
        [Route("RefundPolicy")]
        public ActionResult RefundPolicy()
        {
            ViewData["ActionName"] = "RefundPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                RefundPolicyModel refundPolicyModel = archLibBL.RefundPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("RefundPolicy", refundPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: RegisterUserProf
        [HttpGet]
        [Route("RegisterUserProf/{id?}")]
        public ActionResult RegisterUserProf(string id)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "RegisterUserProf";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                id = "100"; //This action will be used for regular register - There will be no source
;               RegisterUserProfModel registerUserProfModel = archLibBL.RegisterUserProf("", id, RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                registerUserProfModel.OTPRequestModel.RequestType = "Register";
                actionResult = View("RegisterUserProf", registerUserProfModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: RegisterUserProfOTPRequest
        [HttpPost]
        public ActionResult RegisterUserProfOTPRequest(OTPRequestModel oTPRequestModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "RegisterUserProfOTPRequest";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString;
            OTPResponseModel oTPResponseModel = null;
            //int x = 1, y = 0, z = x / y;
            try
            {
                ModelState.Clear();
                TryValidateModel(oTPRequestModel);
                if (ModelState.IsValid)
                {
                    oTPResponseModel = archLibBL.RegisterUserProfOTPRequest(ref oTPRequestModel, "REGISTERUSERPROF", this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    oTPResponseModel.RequestType = "Register";
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error during OTP setup");
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPRequestData", oTPRequestModel);
            }
            if (ModelState.IsValid)
            {
                success = true;
                processMessage = "SUCCESS!!!";
            }
            else
            {
                success = false;
                processMessage = "ERROR???";
            }
            if (success)
            {
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPResponse", oTPResponseModel);
            }
            else
            {
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                oTPRequestModel.CaptchaAnswer = null;
                oTPRequestModel.CaptchaNumber0 = Session["CaptchaNumber0"].ToString();
                oTPRequestModel.CaptchaNumber1 = Session["CaptchaNumber1"].ToString();
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPRequestData", oTPRequestModel);
            }
            actionResult = Json(new { success, processMessage, htmlString });
            return actionResult;
        }

        // POST: RegisterUserProfOTPResponse
        [HttpPost]
        public ActionResult RegisterUserProfOTPResponse(OTPResponseModel oTPResponseModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "RegisterUserProfOTPResponse";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            bool success;
            string processMessage, htmlString = "";
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(oTPResponseModel);
                if (ModelState.IsValid)
                {
                    SessionObjectModel sessionObjectModel = archLibBL.RegisterUserProfOTPResponse(ref oTPResponseModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        if (sessionObjectModel.NewUser)
                        {
                            retailSlnBL.RegisterUserProfPersonExtn1(sessionObjectModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                        Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[sessionObjectModel.AspNetRoleName];
                        sessionObjectModel.AspNetRoleNameProxy = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
                        string currentLoggedInUserId = loggedInUserId;
                        htmlString = LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
                        success = true;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    }
                    else
                    {
                        success = false;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                    }
                }
                else
                {
                    success = false;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                ModelState.AddModelError("", "Error during OTP setup");
            }
            if (success)
            {
                processMessage = "SUCCESS!!!";
                actionResult = Json(new { success, processMessage, htmlString });
            }
            else
            {
                oTPResponseModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPResponseData", oTPResponseModel);
                actionResult = Json(new { success, processMessage, htmlString });
            }
            return actionResult;
        }

        #region Commented Out Code
        //// GET: RegisterUserOTP
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("RegisterUserOTP/{id?}")]
        //public ActionResult RegisterUserOTP(string id)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    ViewData["ActionName"] = "RegisterUserOTP";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        RegisterUserOTPModel registerUserOTPModer = archLibBL.RegisterUserOTP(id, "100", RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("RegisterUserOTP", registerUserOTPModer);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error", responseObjectModel);
        //    }
        //    return actionResult;
        //}

        //// GET: RegisterUserOTP
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult RegisterUserOTP(RegisterUserOTPModel registerUserOTPModel)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    ViewData["ActionName"] = "RegisterUserOTP";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    bool success;
        //    string processMessage, htmlString;
        //    try
        //    {
        //        int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        TryValidateModel(registerUserOTPModel);
        //        TryValidateModel(registerUserOTPModel.OTPModel, "OTPModel");
        //        if (ModelState.IsValid)
        //        {
        //            success = true;
        //            processMessage = "SUCCESS!!!";
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Model State Success");
        //            //RegisterUserOTPModel registerUserOTPModel = archLibBL.RegisterUserOTP(id, "700", RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        else
        //        {
        //            success = false;
        //            processMessage = "ERROR???";
        //            registerUserOTPModel.OTPResponseModel = new OTPResponseModel();
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Model State Errors");
        //        }
        //        //actionResult = View("RegisterUserOTP", registerUserOTPModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        success = false;
        //        processMessage = "ERROR???";
        //        ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
        //        //actionResult = View("Error", responseObjectModel);
        //    }
        //    htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserOTPData", registerUserOTPModel);
        //    actionResult = Json(new { success, processMessage, htmlString });
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}

        //// GET: RegisterUser
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("RegisterUser/{id?}")]
        //public ActionResult RegisterUser(string id)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    ViewData["ActionName"] = "RegisterUser";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        RegisterUserModel registerUserModel = archLibBL.RegisterUser(id, "700", RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        Session["CaptchaNumberRegisterUser0"] = registerUserModel.CaptchaNumberRegisterUser0;
        //        Session["CaptchaNumberRegisterUser1"] = registerUserModel.CaptchaNumberRegisterUser1;
        //        actionResult = View("RegisterUser", registerUserModel);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error", responseObjectModel);
        //    }
        //    return actionResult;
        //}

        //// POST: RegisterUser
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult RegisterUser(RegisterUserModel registerUserModel)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    bool success;
        //    string processMessage, htmlString;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        TryValidateModel(registerUserModel);
        //        TryValidateModel(registerUserModel.DemogInfoAddressModel, "DemogInfoAddressModel");
        //        archLibBL.RegisterUser(ref registerUserModel, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        if (ModelState.IsValid)
        //        {
        //            if (!registerUserModel.RedirectToUpdatePassword)
        //            {
        //                retailSlnBL.RegisterUserProfPersonExtn1(registerUserModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            RegisterUserEmailModel registerUserEmailModel = new RegisterUserEmailModel
        //            {
        //                RegisterUserModel = registerUserModel,
        //            };
        //            retailSlnBL.RegisterUserExtn1(registerUserEmailModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            string registerUserEmailBodyHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailBody", registerUserEmailModel);
        //            string registerUserEmailSubjectHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailSubject", registerUserEmailModel);
        //            string signatureHtml = archLibBL.ViewToHtmlString(this, "_SignatureTemplate", registerUserEmailModel);
        //            registerUserEmailBodyHtml += signatureHtml;
        //            archLibBL.SendEmail(registerUserModel.RegisterEmailAddress, registerUserEmailSubjectHtml, registerUserEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            success = true;
        //            processMessage = "SUCCESS!!!";
        //            htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserSuccess", registerUserEmailModel);
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
        //        }
        //        else
        //        {
        //            success = false;
        //            processMessage = "ERROR???";
        //            var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
        //            registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsReferral : aspNetRoleModels;
        //            registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //            registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
        //            if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
        //            {
        //                registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
        //            }
        //            else
        //            {
        //                registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
        //            }
        //            htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserData", registerUserModel);
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
        //        registerUserModel.CaptchaAnswerRegisterUser = null;
        //        registerUserModel.CaptchaNumberRegisterUser0 = Session["CaptchaNumberRegisterUser0"].ToString();
        //        registerUserModel.CaptchaNumberRegisterUser1 = Session["CaptchaNumberRegisterUser1"].ToString();
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
        //        registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsReferral : aspNetRoleModels;
        //        registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //        registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
        //        if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
        //        {
        //            registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
        //        }
        //        else
        //        {
        //            registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
        //        }
        //        registerUserModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //        };
        //        success = false;
        //        processMessage = "ERROR???";
        //        htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserData", registerUserModel);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
        //    }
        //    actionResult = Json(new { success, processMessage, htmlString });
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}

        //// GET: RegisterUser
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("RegisterUserProf/{id?}")]
        //public ActionResult RegisterUserProf(string id)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    ViewData["ActionName"] = "RegisterUserProf";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        RegisterUserModel registerUserModel = archLibBL.RegisterUser(id, "100", RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        Session["CaptchaNumberRegisterUser0"] = registerUserModel.CaptchaNumberRegisterUser0;
        //        Session["CaptchaNumberRegisterUser1"] = registerUserModel.CaptchaNumberRegisterUser1;
        //        actionResult = View("RegisterUserProf", registerUserModel);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error", responseObjectModel);
        //    }
        //    return actionResult;
        //}

        //// POST: RegisterUser
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult RegisterUserProf(RegisterUserModel registerUserModel)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    bool success;
        //    string processMessage, htmlString, redirectUrl;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        TryValidateModel(registerUserModel);
        //        TryValidateModel(registerUserModel.DemogInfoAddressModel, "DemogInfoAddressModel");
        //        archLibBL.RegisterUser(ref registerUserModel, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        if (ModelState.IsValid)
        //        {
        //            if (!registerUserModel.RedirectToUpdatePassword)
        //            {
        //                retailSlnBL.RegisterUserProfPersonExtn1(registerUserModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            RegisterUserEmailModel registerUserEmailModel = new RegisterUserEmailModel
        //            {
        //                RegisterUserModel = registerUserModel,
        //            };
        //            string registerUserEmailBodyHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailBody", registerUserEmailModel);
        //            string registerUserEmailSubjectHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailSubject", registerUserEmailModel);
        //            string signatureHtml = archLibBL.ViewToHtmlString(this, "_SignatureTemplate", registerUserEmailModel);
        //            registerUserEmailBodyHtml += signatureHtml;
        //            archLibBL.SendEmail(registerUserModel.RegisterEmailAddress, registerUserEmailSubjectHtml, registerUserEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            LoginUserProfModel loginUserProfModel = new LoginUserProfModel
        //            {
        //                LoginEmailAddress = registerUserModel.RegisterEmailAddress,
        //                LoginPassword = registerUserModel.LoginPassword,
        //            };
        //            SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, false, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[sessionObjectModel.AspNetRoleName];
        //            sessionObjectModel.AspNetRoleNameProxy = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
        //            redirectUrl = LoginUserProfProcess(sessionObjectModel.AspNetUserId, sessionObjectModel);
        //            success = true;
        //            processMessage = "SUCCESS!!!";
        //            htmlString = "";
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
        //        }
        //        else
        //        {
        //            var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
        //            registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsReferral : aspNetRoleModels;
        //            registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //            registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
        //            if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
        //            {
        //                registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
        //            }
        //            else
        //            {
        //                registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
        //            }
        //            success = false;
        //            processMessage = "ERROR???";
        //            htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserProfData", registerUserModel);
        //            redirectUrl = "";
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
        //        registerUserModel.CaptchaAnswerRegisterUser = null;
        //        registerUserModel.CaptchaNumberRegisterUser0 = Session["CaptchaNumberRegisterUser0"].ToString();
        //        registerUserModel.CaptchaNumberRegisterUser1 = Session["CaptchaNumberRegisterUser1"].ToString();
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
        //        registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsReferral : aspNetRoleModels;
        //        registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //        registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
        //        if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
        //        {
        //            registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
        //        }
        //        else
        //        {
        //            registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
        //        }
        //        registerUserModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //        };
        //        success = false;
        //        processMessage = "ERROR???";
        //        htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserData", registerUserModel);
        //        redirectUrl = "";
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
        //    }
        //    actionResult = Json(new { success, processMessage, htmlString, redirectUrl });
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("ResetPassword")]
        //public ActionResult ResetPassword(string id)
        //{
        //    //int x = 1, y = 0, z = x / y;
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        ViewData["ActionName"] = "RESETPASSWORD";
        //    }
        //    else
        //    {
        //        ViewData["ActionName"] = id.ToUpper();
        //    }
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ResetPasswordModel resetPasswordModel = archLibBL.ResetPassword(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("ResetPassword", resetPasswordModel);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ModelState.AddModelError("", "Reset Password GET");
        //        archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
        //        actionResult = View("Error", responseObjectModel);
        //    }
        //    return actionResult;
        //}

        //// POST: ResetPassword
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    bool success;
        //    string processMessage, htmlString;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        TryValidateModel(resetPasswordModel);
        //        UpdatePasswordModel updatePasswordModel = archLibBL.ResetPassword(ref resetPasswordModel, RetailSlnCache.DefaultDeliveryDemogInfoCountryId, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        if (ModelState.IsValid)
        //        {
        //            success = true;
        //            processMessage = "SUCCESS!!!";
        //            //UpdatePasswordModel updatePasswordModel = archLibBL.UpdatePassword(resetPasswordModel.ResetPasswordEmailAddress, RetailSlnCache.DefaultDeliveryDemogInfoCountryId, resetPasswordModel.OTPCreatedDateTime, resetPasswordModel.OTPExpiryDateTime, resetPasswordModel.OTPExpiryDuration, resetPasswordModel.OTPSendTypeId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            //updatePasswordModel.DemogInfoAddressModel = new DemogInfoAddressModel
        //            //{
        //            //    BuildingTypeId = BuildingTypeEnum._,
        //            //    BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
        //            //    DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
        //            //    DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
        //            //    DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId],
        //            //};
        //            htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePassword", updatePasswordModel);
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
        //        }
        //        else
        //        {
        //            success = false;
        //            processMessage = "ERROR???";
        //            htmlString = archLibBL.ViewToHtmlString(this, "_ResetPasswordData", resetPasswordModel);
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1");
        //        resetPasswordModel.CaptchaAnswerResetPassword = null;
        //        resetPasswordModel.CaptchaNumberResetPassword0 = Session["CaptchaNumberResetPassword0"].ToString();
        //        resetPasswordModel.CaptchaNumberResetPassword1 = Session["CaptchaNumberResetPassword1"].ToString();
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        resetPasswordModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //        };
        //        success = false;
        //        processMessage = "ERROR???";
        //        htmlString = archLibBL.ViewToHtmlString(this, "_ResetPasswordData", resetPasswordModel);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
        //    }
        //    actionResult = Json(new { success, processMessage, htmlString });
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}
        #endregion

        // GET: ResetPasswordContactUs
        [AllowAnonymous]
        [HttpGet]
        [Route("ReturnPolicy")]
        public ActionResult ReturnPolicy()
        {
            ViewData["ActionName"] = "ReturnPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ReturnPolicyModel returnPolicyModel = archLibBL.ReturnPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ReturnPolicy", returnPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: ResetPasswordContactUs
        [AllowAnonymous]
        [HttpGet]
        [Route("ShippingPolicy")]
        public ActionResult ShippingPolicy()
        {
            ViewData["ActionName"] = "ShippingPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ShippingPolicyModel shippingPolicyModel = archLibBL.ShippingPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ShippingPolicy", shippingPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: SEOPage1
        [AllowAnonymous]
        [HttpGet]
        [Route("SEOPage1")]
        public ActionResult SEOPage1()
        {
            return View();
        }

        // GET: TermsofService
        [AllowAnonymous]
        [HttpGet]
        [Route("TermsofService")]
        public ActionResult TermsofService()
        {
            ViewData["ActionName"] = "TermsofService";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                TermsofServiceModel termsofServiceModel = archLibBL.TermsofService(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("TermsofService", termsofServiceModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: Testimonials
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Testimonials()
        {
            ViewData["ActionName"] = "Testimonials";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                TestimonialsModel testimonialsModel = archLibBL.Testimonials(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Testimonials", testimonialsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Testimonials / GET");
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: Unauthorized
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return RedirectToAction("LoginUserProf");
        }

        #region Commented Out Code
        //// POST: UpdatePassword
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult UpdatePassword(UpdatePasswordModel updatePasswordModel)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ActionResult actionResult;
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    bool success;
        //    string processMessage, htmlString;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        TryValidateModel(updatePasswordModel);
        //        TryValidateModel(updatePasswordModel.DemogInfoAddressModel, "DemogInfoAddressModel");
        //        string currentLoggedInUserId = loggedInUserId;
        //        archLibBL.UpdatePassword(ref updatePasswordModel, false, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        if (ModelState.IsValid)
        //        {
        //            LoginUserProfModel loginUserProfModel = new LoginUserProfModel
        //            {
        //                LoginEmailAddress = updatePasswordModel.EmailAddress,
        //                LoginPassword = updatePasswordModel.LoginPassword,
        //            };
        //            SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, false, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (ModelState.IsValid)
        //            {
        //                var redirectUrl = LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
        //                success = true;
        //                processMessage = "SUCCESS!!!";
        //                actionResult = Json(new { success, processMessage, redirectUrl });
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
        //            }
        //            else
        //            {
        //                success = false;
        //                processMessage = "ERROR???";
        //                var demogInfoCountryId = updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId == null ? RetailSlnCache.DefaultDeliveryDemogInfoCountryId : updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId.Value;
        //                updatePasswordModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //                updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
        //                updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryId);
        //                archLibBL.UpdatePasswordPostData(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
        //                actionResult = Json(new { success, processMessage, htmlString });
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
        //            }
        //        }
        //        else
        //        {
        //            success = false;
        //            processMessage = "ERROR???";
        //            var demogInfoCountryId = updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId == null ? RetailSlnCache.DefaultDeliveryDemogInfoCountryId : updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId.Value;
        //            updatePasswordModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //            updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
        //            updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryId);
        //            archLibBL.UpdatePasswordPostData(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
        //            actionResult = Json(new { success, processMessage, htmlString });
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        archLibBL.UpdatePasswordPostData(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        var demogInfoCountryId = updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId == null ? RetailSlnCache.DefaultDeliveryDemogInfoCountryId : updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId.Value;
        //        updatePasswordModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
        //        updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems;
        //        updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryId);
        //        updatePasswordModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //        };
        //        //actionResult = PartialView("_UpdatePasswordData", updatePasswordModel);
        //        htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
        //        success = false;
        //        processMessage = "ERROR???";
        //        actionResult = Json(new { success, processMessage, htmlString });
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
        //    }
        //    //htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}
        #endregion

        // GET: UserProfile
        [Authorize]
        [AjaxAuthorize]
        [HttpGet]
        [Route("UserProfile")]
        public ActionResult UserProfile()
        {
            ViewData["ActionName"] = "UserProfile";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                string aspNetUserId = ((SessionObjectModel)Session["SessionObject"]).AspNetUserId;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before BL");
                PersonModel personModel = archLibBL.UserProfile(aspNetUserId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("UserProfile", personModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After BL");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "UserProfile / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        private string LoginUserProfProcess(string currentLoggedInUserId, SessionObjectModel sessionObjectModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ApplSessionObjectModel applSessionObjectModel;
            applSessionObjectModel = retailSlnBL.LoginUserProf(sessionObjectModel.PersonId, sessionObjectModel.AspNetRoleName, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            sessionObjectModel.ApplSessionObjectModel = applSessionObjectModel;
            SessionObjectModel createForSessionObject = archLibBL.CopySessionObject(sessionObjectModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            applSessionObjectModel = retailSlnBL.LoginUserProf(createForSessionObject.PersonId, createForSessionObject.AspNetRoleName, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            createForSessionObject.ApplSessionObjectModel = applSessionObjectModel;
            Session["SessionObject"] = sessionObjectModel;
            Session["CreateForSessionObject"] = createForSessionObject;
            Session.Timeout = int.Parse(ConfigurationManager.AppSettings["AccessTokenExpiryMinutes"]);
            var identity = new ClaimsIdentity
            (
                new[]
                {
                    new Claim(ClaimTypes.Name, sessionObjectModel.FirstName + " " + sessionObjectModel.LastName),
                    new Claim(ClaimTypes.Email, sessionObjectModel.EmailAddress),
                    new Claim(ClaimTypes.Role, sessionObjectModel.AspNetRoleName),
                    //new Claim(ClaimTypes.Country, "India"),
                },
                "ApplicationCookie"
            );
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignIn(identity);
            string redirectUrl;
            Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[sessionObjectModel.AspNetRoleNameProxy];
            redirectUrl = Url.Action(aspNetRoleKVPs["ActionName00"].KVPValueData, aspNetRoleKVPs["ControllerName00"].KVPValueData);
            ShoppingCartModel shoppingCartModel = (ShoppingCartModel)Session["ShoppingCart"];
            if (createForSessionObject.AspNetRoleName != "GUESTROLE")
            {
                //retailSlnBL.ShoppingCartWIPCreate(shoppingCartModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            //Take a look at the below logic Begin
            //if (currentLoggedInUserId != createForSessionObject.AspNetUserId)
            //{
            //    if (currentLoggedInUserId != "")
            //    {
            //    }
            //}
            //Take a look at the below logic End
            //actionResult = Json(new { success, processMessage, redirectUrl });
            //redirectUrl = Url.Action(actionName, controllerName);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return redirectUrl;
        }
    }
}
