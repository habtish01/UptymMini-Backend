
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Uptym.Data.DbModels.CMSSchema;
using Uptym.Data.DbModels.ConfigurationSchema;
using Uptym.Data.DbModels.EquipmentSchema;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.DbModels.sp;
using Uptym.Data.DbModels.SubscriptionSchema;



namespace Uptym.Data.DataContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // set application user relations
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            });

            // set application role relations
            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // set application role primary key
                b.HasKey(u => u.Id);
                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            // set application user role primary key
            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.HasKey(u => u.Id);
            });

            // set application role relations
            modelBuilder.Entity<Customer>(b =>
            {
                // set application role primary key
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Email)
                    .IsUnique();
                // Each Customer can have many Users
                b.HasMany(e => e.Users)
                    .WithOne(e => e.Customer)
                    .HasForeignKey(cu => cu.CustomerID);

                // Each Customer can have many Billings
                b.HasMany(e => e.Billings)
                    .WithOne(e => e.Customer)
                    .HasForeignKey(cu => cu.CustomerId)
                    .IsRequired();
            });

            modelBuilder.Entity<PlanPermission>(b =>
            {
                b.HasKey(u => new { u.PlanId, u.PermissionId });
            });



            // Update Identity Schema
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Security");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles", "Security");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles", "Security");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins", "Security");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims", "Security");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens", "Security");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims", "Security");
            modelBuilder.Entity<Customer>().ToTable("Customers", "Subscription");
            modelBuilder.Entity<PlanPermission>().ToTable("PlanPermissions", "Subscription");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty property in modelBuilder.Model.GetEntityTypes()
                           .SelectMany(t => t.GetProperties())
                           .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetDefaultValue(0);
                property.SetColumnType("decimal(18, 4)");
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                    .SelectMany(t => t.GetProperties())
                                    .Where(p => p.ClrType == typeof(bool)))
            {
                property.SetDefaultValue(false);
            }

        }

        #region Subscription
        public DbSet<UserTransactionHistory> UserTransactionHistories { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<UserTransactionType> UserTransactionTypes { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PlanPermission> PlanPermissions { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<MenuPlan> MenuPlans { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<UpcomingPayment> UpcomingPayments { get; set; }
        #endregion

        #region CMS Schema
        public DbSet<UsefulResource> UsefulResources { get; set; }
        public DbSet<InquiryQuestion> InquiryQuestions { get; set; }
        public DbSet<InquiryQuestionReply> InquiryQuestionReplies { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; }
        public DbSet<ChannelVideo> ChannelVideos { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        #endregion

        #region Equipment
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentContract> EquipmentContracts { get; set; }
        public DbSet<EquipmentOperator> EquipmentOperators { get; set; }
        public DbSet<ProblemType> ProblemTypes { get; set; }
        public DbSet<SubProblemType> SubProblemTypes { get; set; }
        public DbSet<EquipmentLookup> EquipmentLookups { get; set; }
        public DbSet<MetadataTasks> MetadataTasks { get; set; }
        public DbSet<EquipmentLookupSchedule> EquipmentSchedules { get; set; }
        public DbSet<AutoSchedule> AutoSchedule { get; set; }
        public DbSet<ErrorCode> ErrorCodes { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Sparepart> Spareparts { get; set; }

        public DbSet<SparepartInventoryDetail> SparepartInventoryDetail { get; set; }
        public DbSet<SparepartEquipmentDetail> SparepartEquipmentDetail { get; set; }

        public DbSet<EquipmentLookupSparepart> EquipmentLookupSparepart { get; set; }
        public DbSet<SparepartCategories> SparepartCategories { get; set; }
        public DbSet<SparepartSubCategories> SparepartSubCategories { get; set; }
        public DbSet<EquipmentScheduleType> EquipmentScheduleTypes { get; set; }
        public DbSet<EquipmentDocumentation> EquipmentDocumentations { get; set; }
        public DbSet<EquipmentScheduleInterval> EquipmentScheduleIntervals { get; set; }
        public DbSet<Metrics> Metrics { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<DbModels.EquipmentSchema.Tasks> Tasks { get; set; }
        public DbSet<DbModels.MaintenanceSchema.MaintenanceTasks> MaintenanceTasks { get; set; }
        public DbSet<AutoScheduleAssignedEngineers> AutoScheduleAssignedEngineers { get; set; }
        #endregion

        #region Maintenance

        public DbSet<MaintenanceAction> MaintenanceActions { get; set; }
        public DbSet<WorkOrderHeader> WorkOrderHeaders { get; set; }
        public DbSet<WorkOrderSparepart> WorkOrderSpareparts { get; set; }

        public DbSet<AssignedEngineer> AssignedEngineers { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderStatus> WorkOrderStatus { get; set; }
        public DbSet<WorkOrderTaskList> WorkOrderTaskLists { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<WorkOrderStatusType> WorkOrderStatusTypes { get; set; }
        public DbSet<WorkOrderType> WorkOrderTypes { get; set; }
        public DbSet<WorkOrderMaintenanceCost> WorkOrderMaintenanceCosts { get; set; }
        
        public DbSet<NewFreqMaintainedReport> NewFreqMaintainedReport { get; set; }
        #endregion

        #region MetaData
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<CountryPeriod> CountryPeriods { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<HealthFacilityType> HealthFacilityTypes { get; set; }
        public DbSet<EquipmentLookupDocumentation> EquipmentLookupDocumentations { get; set; }
        public DbSet<DocumentationType> DocumentationTypes { get; set; }

        public DbSet<HealthFacility> HealthFacilties { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TaskListDetail> TaskListDetails { get; set; }
        public DbSet<TaskListType> TaskListType { get; set; }
        public DbSet<Widget> Widgets { get; set; }

        #endregion

        public  DbSet<TestNumber> TestNumbers { get; set; }

        public DbSet<WorkOrderSummary> WorkOrderSummary { get; set; }

        public DbSet<EquipmentSummary> EquipmentSummary { get; set; }

        public DbSet<InstrumentTotalSummary> InstrumentTotalSummary { get; set; }

        public DbSet<PreventiveCompliance> PreventiveCompliance { get; set; }

        public DbSet<InspectionCompliance> InspectionCompliance { get; set; }
        public DbSet<MaintenanceMeanTimeSummary> MaintenanceMeanTimeSummary { get; set; }

        public DbSet<UserPreference> UserPreferences { get; set; }

        public DbSet<MaintenanceCost> MaintenanceCost { get; set; }
        public DbSet<OperatorFailure> OperatorFailure { get; set; }
        public DbSet<EquipmentLocation> EquipmentLocation { get; set; }
        
    }
}
