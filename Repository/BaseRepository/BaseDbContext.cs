using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Cares.Models.DomainModels;
using FRS.Models.DomainModels;
using FRS.Models.IdentityModels;
using FRS.Models.LoggerModels;
using FRS.Models.MenuModels;
using Microsoft.Practices.Unity;

namespace FRS.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private IUnityContainer container;
        #endregion

        #region Protected


        #endregion

        #region Constructor
        public BaseDbContext()
        {
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty(object entity, string propertyName, bool isCollection = false)
        {
            if (!isCollection)
            {
                Entry(entity).Reference(propertyName).Load();
            }
            else
            {
                Entry(entity).Collection(propertyName).Load();
            }
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            string propertyName = PropertyReference.GetPropertyName(propertyExpression);
            LoadProperty(entity, propertyName, isCollection);
        }

        #endregion

        #region Public

        public BaseDbContext(IUnityContainer container, string connectionString)
            : base(connectionString)
        {
            this.container = container;
        }

        #region Logger
        /// <summary>
        /// Logs
        /// </summary>
        public DbSet<Log> Logs { get; set; }
        /// <summary>
        /// Log Categories
        /// </summary>
        public DbSet<LogCategory> LogCategories { get; set; }
        /// <summary>
        /// Category Logs
        /// </summary>
        public DbSet<CategoryLog> CategoryLogs { get; set; }
        #endregion
        #region Menu Rights and Security
        /// <summary>
        /// Menu Rights
        /// </summary>
        public DbSet<MenuRight> MenuRights { get; set; }
        /// <summary>
        /// Menu
        /// </summary>
        public DbSet<Menu> Menus { get; set; }
        #endregion

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<AspNetUser> Users { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// User Claims
        /// </summary>
        public DbSet<UserClaim> UserClaims{ get; set; }

        /// <summary>
        /// User Logins
        /// </summary>
        public DbSet<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// WebApi Users
        /// </summary>
        public DbSet<WebApiUser> WebApiUsers { get; set; }
        public DbSet<DomainLicenseDetail> DomainLicenseDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadMetaData> LoadMetaDatas { get; set; }
        public DbSet<MT940Balance> MT940Balances { get; set; }
        public DbSet<MT940CustomerStatement> MT940CustomerStatements { get; set; }
        public DbSet<MT940CustomerStatementTransaction> MT940CustomerStatementTransactions { get; set; }
        public DbSet<MT940Load> MT940Loads { get; set; }
        public DbSet<MT940LoadDetail> MT940LoadDetail { get; set; }
        public DbSet<LoadType> LoadTypes { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<FileContent> FileContents { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<OracleGLEntry> OracleGLEntries { get; set; }
        public DbSet<OracleGLLoad> OracleGLLoads { get; set; }
        public DbSet<LoadStatu> LoadStatus { get; set; }
        public DbSet<ReconciledMapping> ReconciledMappings { get; set; }
        /// <summary>
        /// Execute store procedure for creating d
        /// </summary>
        public void ExecuteCreateDefaultData(string userId, long userDomainKey)
        {
            var domainkKeyParameter = new ObjectParameter("domainKey", userDomainKey);
            var userIdParameter = new ObjectParameter("userId", userId);
            ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("copyDefaultData", domainkKeyParameter, userIdParameter);            
        }

        /// <summary>
        /// Get Root Parent Hire Group
        /// </summary>
        public ObjectResult<long?> GetRootParentHireGroup(long? hireGroupDetailId, long? userDomainKey)
        {
            var hireGroupDetailIdParameter = hireGroupDetailId.HasValue ?
                new ObjectParameter("HireGroupDetailId", hireGroupDetailId) :
                new ObjectParameter("HireGroupDetailId", typeof(long));

            var userDomainKeyParameter = userDomainKey.HasValue ?
                new ObjectParameter("UserDomainKey", userDomainKey) :
                new ObjectParameter("UserDomainKey", typeof(long));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<long?>("GetRootParentHireGroup", hireGroupDetailIdParameter, userDomainKeyParameter);
        }

        #endregion
    }
}