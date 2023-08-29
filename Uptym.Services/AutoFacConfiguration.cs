using Autofac;
using Uptym.Core.Interfaces;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.sp;
using Uptym.DTO.Common;
using Uptym.Repositories.CMS.Article;
using Uptym.Repositories.CMS.ArticleImage;
using Uptym.Repositories.CMS.ChannelVideo;
using Uptym.Repositories.CMS.ContactInfo;
using Uptym.Repositories.CMS.Feature;
using Uptym.Repositories.CMS.FrequentlyAskedQuestion;
using Uptym.Repositories.CMS.InquiryQuestion;
using Uptym.Repositories.CMS.InquiryQuestionReply;
using Uptym.Repositories.CMS.UsefulResource;
using Uptym.Repositories.Configuration.Configuration;
using Uptym.Repositories.Configuration.ConfigurationAudit;
using Uptym.Repositories.Configuration.Menu;
using Uptym.Repositories.Configuration.MenuPlan;
using Uptym.Repositories.Configuration.MenuRole;
using Uptym.Repositories.Dashboard;
using Uptym.Repositories.Equipment;
using Uptym.Repositories.Equipment.AutoScheduleAssignedEngineers;
using Uptym.Repositories.Generics;
using Uptym.Repositories.Maintenance;
using Uptym.Repositories.Maintenance.AssignedEngineer;
using Uptym.Repositories.Maintenance.MaintenanceTasks;
using Uptym.Repositories.Metadata;
using Uptym.Repositories.Metadata.Sparepart;
using Uptym.Repositories.Metadata.SparepartEquipmentDetail;
using Uptym.Repositories.Metadata.SparepartInventoryDetail;
using Uptym.Repositories.Metadata.SparepartCategory;
using Uptym.Repositories.Metadata.SparepartSubCategory;
using Uptym.Repositories.Metadata.MetadataTasks;
using Uptym.Repositories.Metadata.SubProblemType;
using Uptym.Repositories.Security.Role;
using Uptym.Repositories.Security.UserRole;
using Uptym.Repositories.Security.UserTransactionHistory;
using Uptym.Repositories.Subscription.Billing;
using Uptym.Repositories.Subscription.Customer;
using Uptym.Repositories.Subscription.CustomerType;
using Uptym.Repositories.Subscription.Membership;
using Uptym.Repositories.Subscription.PaymentType;
using Uptym.Repositories.Subscription.Plan;
using Uptym.Repositories.Subscription.PlanPermission;
using Uptym.Repositories.Subscription.UpcomingPayment;
using Uptym.Repositories.UOW;
using Uptym.Services.CMS.Article;
using Uptym.Services.CMS.ChannelVideo;
using Uptym.Services.CMS.ContactInfo;
using Uptym.Services.CMS.Feature;
using Uptym.Services.CMS.FrequentlyAskedQuestion;
using Uptym.Services.CMS.InquiryQuestion;
using Uptym.Services.CMS.UsefulResource;
using Uptym.Services.Configuration;
using Uptym.Services.Configuration.Configuration;
using Uptym.Services.Configuration.Menu;
using Uptym.Services.Dashboard;
using Uptym.Services.Equipment;
using Uptym.Services.Metadata.SparepartCategory;
using Uptym.Services.Metadata.SparepartSubCategory;
using Uptym.Services.Equipment.AutoScheduleAssignedEngineer;
using Uptym.Services.Global.DataFilter;
using Uptym.Services.Global.FileService;
using Uptym.Services.Global.General;
using Uptym.Services.Global.HangFire;
using Uptym.Services.Global.SendEmail;
using Uptym.Services.Global.Twilio;
using Uptym.Services.Global.UploadFiles;
using Uptym.Services.Maintenance;
using Uptym.Services.Maintenance.ScheduleTrigger;
using Uptym.Services.Maintenance.ScheduleTriggers;
using Uptym.Services.Maintenance.WorkOrderTaskLists;
using Uptym.Services.Metadata;
using Uptym.Services.Metadata.SubProblemType;
using Uptym.Services.Security.Account;
using Uptym.Services.Security.Role;
using Uptym.Services.Security.User;
using Uptym.Services.Subscription.Billing;
using Uptym.Services.Subscription.Customer;
using Uptym.Services.Subscription.CustomerType;
using Uptym.Services.Subscription.Membership;
using Uptym.Services.Subscription.PaymentType;
using Uptym.Services.Subscription.Plan;
using Uptym.Services.Subscription.PlanPermission;
using Uptym.Services.Subscription.UpcomingPayment;

namespace Uptym.Services
{
    public class AutoFacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // Register unit of work
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>)).InstancePerDependency();
            //// Register DTO
            builder.RegisterType<ResponseDTO>().As<IResponseDTO>().InstancePerLifetimeScope();
            //// Register Generic Services
            builder.RegisterGeneric(typeof(FileService<>)).As(typeof(IFileService<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(DataFilterService<>)).As(typeof(IDataFilterService<>)).InstancePerDependency();
            // Register general services
            builder.RegisterType<UploadFilesService>().As<IUploadFilesService>().InstancePerDependency();
            builder.RegisterType<TwilioService>().As<ITwilioService>().InstancePerDependency();
            builder.RegisterType<HangfireService>().As<IHangfireService>().InstancePerDependency();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerDependency();
            // Dashboard
            builder.RegisterType<DashboardService>().As<IDashboardService>().InstancePerDependency();
            builder.RegisterType<DashboardRepository>().As<IDashboardRepository>().InstancePerDependency();

            builder.RegisterType<GRepository<TestNumber>>().As<IGRepository<TestNumber>>().InstancePerDependency();
            builder.RegisterType<GRepository<WorkOrderSummary>>().As<IGRepository<WorkOrderSummary>>().InstancePerDependency();
            builder.RegisterType<GRepository<EquipmentSummary>>().As<IGRepository<EquipmentSummary>>().InstancePerDependency();
            builder.RegisterType<GRepository<InstrumentTotalSummary>>().As<IGRepository<InstrumentTotalSummary>>().InstancePerDependency();

            builder.RegisterType<GRepository<MaintenanceMeanTimeSummary>>().As<IGRepository<MaintenanceMeanTimeSummary>>().InstancePerDependency();
            builder.RegisterType<GRepository<PreventiveCompliance>>().As<IGRepository<PreventiveCompliance>>().InstancePerDependency();
            builder.RegisterType<GRepository<InspectionCompliance>>().As<IGRepository<InspectionCompliance>>().InstancePerDependency();
            builder.RegisterType<GRepository<NewFreqMaintainedReport>>().As<IGRepository<NewFreqMaintainedReport>>().InstancePerDependency();
            builder.RegisterType<GRepository<OperatorFailure>>().As<IGRepository<OperatorFailure>>().InstancePerDependency();
            builder.RegisterType<GRepository<MaintenanceCost>>().As<IGRepository<MaintenanceCost>>().InstancePerDependency();
            builder.RegisterType<GRepository<EquipmentLocation>>().As<IGRepository<EquipmentLocation>>().InstancePerDependency();

            // General
            builder.RegisterType<GeneralService>().As<IGeneralService>().InstancePerDependency();

            #region Configuration
            // Configuration
            builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationAuditRepository>().As<IConfigurationAuditRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuRoleRepository>().As<IMenuRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuPlanRepository>().As<IMenuPlanRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<UserPreferenceRepository>().As<IUserPreferenceRepository>().InstancePerLifetimeScope();

            #endregion

            #region Security Schema
            // Account
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            // User
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            // User Roles
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            // Roles
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            // History
            builder.RegisterType<UserTransactionHistoryRepository>().As<IUserTransactionHistoryRepository>().InstancePerLifetimeScope();
            #endregion

            #region Metadata

            // Configuration
            builder.RegisterType<ManufacturerRepository>().As<IManufacturerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentCategoryRepository>().As<IEquipmentCategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentCategoryService>().As<IEquipmentCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentTypeRepository>().As<IEquipmentTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentTypeService>().As<IEquipmentTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentLookupRepository>().As<IEquipmentLookupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentLookupService>().As<IEquipmentLookupService>().InstancePerLifetimeScope();

            builder.RegisterType<TaskListDetailRepository>().As<ITaskListDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TaskListDetailService>().As<ITaskListDetailService>().InstancePerLifetimeScope();

            builder.RegisterType<TaskListRepository>().As<ITaskListRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TaskListService>().As<ITaskListService>().InstancePerLifetimeScope();

            builder.RegisterType<HealthFacilityRepository>().As<IHealthFacilityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HealthFacilityService>().As<IHealthFacilityService>().InstancePerLifetimeScope();

            builder.RegisterType<HealthFacilityTypeRepository>().As<IHealthFacilityTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HealthFacilityTypeService>().As<IHealthFacilityTypeService>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentLookupDocumentationRepository>().As<IEquipmentLookupDocumentationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentLookupDocumentationService>().As<IEquipmentLookupDocumentationService>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentLookupSchedulesRepository>().As<IEquipmentLookupSchedulesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentLookupScheduleService>().As<IEquipmentLookupScheduleService>().InstancePerLifetimeScope();


            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();

            builder.RegisterType<RegionRepository>().As<IRegionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RegionService>().As<IRegionService>().InstancePerLifetimeScope();

            builder.RegisterType<ErrorCodeRepository>().As<IErrorCodeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ErrorCodeService>().As<IErrorCodeService>().InstancePerLifetimeScope();

            builder.RegisterType<ProblemTypeRepository>().As<IProblemTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProblemTypeService>().As<IProblemTypeService>().InstancePerLifetimeScope();

            builder.RegisterType<SubProblemTypeRepository>().As<ISubProblemTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SubProblemTypeService>().As<ISubProblemTypeService>().InstancePerLifetimeScope();

            builder.RegisterType<SparepartRepository>().As<ISparepartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SparepartEquipmentDetailRepository>().As<ISparepartEquipmentDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SparepartInventoryDetailRepository>().As<ISparepartInventoryDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentLookupSparepartRepository>().As<IEquipmentLookupSparepartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SparepartService>().As<ISparepartService>().InstancePerLifetimeScope();

            builder.RegisterType<SparepartCategoriesService>().As<ISparepartCategoriesService>().InstancePerLifetimeScope();
            builder.RegisterType<SparepartCategoriesRepository>().As<ISparepartCategoriesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SparepartSubCategoriesService>().As<ISparepartSubCategoriesService>().InstancePerLifetimeScope();
            builder.RegisterType<SparepartSubCategoriesRepository>().As<ISparepartSubCategoriesRepository>().InstancePerLifetimeScope();

            builder.RegisterType<WidgetRepository>().As<IWidgetRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OperatorRepository>().As<IOperatorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OperatorService>().As<IOperatorService>().InstancePerLifetimeScope();

            #endregion

            #region Equipment

            builder.RegisterType<EquipmentRepository>().As<IEquipmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentService>().As<IEquipmentService>().InstancePerLifetimeScope();

            builder.RegisterType<AutoScheduleRepository>().As<IAutoScheduleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoScheduleService>().As<IAutoScheduleService>().InstancePerLifetimeScope();

            builder.RegisterType<ScheduleService>().As<IScheduleService>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentContractRepository>().As<IEquipmentContractRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentContractService>().As<IEquipmentContractService>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentDocumentationRepository>().As<IEquipmentDocumentationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentDocumentationService>().As<IEquipmentDocumentationService>().InstancePerLifetimeScope();

            builder.RegisterType<EquipmentOperatorRepository>().As<IEquipmentOperatorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EquipmentOperatorService>().As<IEquipmentOperatorService>().InstancePerLifetimeScope();


            builder.RegisterType<TimesheetRepository>().As<ITimesheetRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TimesheetService>().As<ITimesheetService>().InstancePerLifetimeScope();

            builder.RegisterType<TaskRepository>().As<ITaskRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TaskService>().As<ITaskService>().InstancePerLifetimeScope();

            builder.RegisterType<AutoScheduleAssignedEngineersRepository>().As<IAutoScheduleAssignedEngineersRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoScheduleAssignedEngineersService>().As<IAutoScheduleAssignedEngineersService>().InstancePerLifetimeScope(); builder.RegisterType<AutoScheduleAssignedEngineersRepository>().As<IAutoScheduleAssignedEngineersRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceTasksRepository>().As<IMaintenanceTasksRepository>().InstancePerLifetimeScope(); builder.RegisterType<AutoScheduleAssignedEngineersRepository>().As<IAutoScheduleAssignedEngineersRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceTasksService>().As<IMaintenanceTasksService>().InstancePerLifetimeScope();
            #endregion
            builder.RegisterType<MetadataTasksRepository>().As<IMetadataTasksRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MetadataTasksService>().As<IMetadataTasksService>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceCheckListDetailRepository>().As<IMaintenanceCheckListDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceCheckListDetailService>().As<IMaintenanceCheckListDetailService>().InstancePerLifetimeScope();

            builder.RegisterType<MaintenanceCheckListService>().As<IMaintenanceCheckListService>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceCheckListRepository>().As<IMaintenanceCheckListRepository>().InstancePerLifetimeScope();

            #region Maintenance
            builder.RegisterType<WorkOrderHeaderRepository>().As<IWorkOrderHeaderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkOrderHeaderService>().As<IWorkOrderHeaderService>().InstancePerLifetimeScope();


            builder.RegisterType<MaintenanceActionRepository>().As<IMaintenanceActionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceActionService>().As<IMaintenanceActionService>().InstancePerLifetimeScope();

            builder.RegisterType<WorkOrderSparepartRepository>().As<IWorkOrderSparepartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkOrderSparepartService>().As<IWorkOrderSparepartService>().InstancePerLifetimeScope();

            builder.RegisterType<WorkOrderRepository>().As<IWorkOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkOrderService>().As<IWorkOrderService>().InstancePerLifetimeScope();

            builder.RegisterType<ScheduleTriggerService>().As<IScheduleTriggerService>().InstancePerLifetimeScope();

            builder.RegisterType<WorkOrderStatusRepository>().As<IWorkOrderStatusRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkOrderStatusService>().As<IWorkOrderStatusService>().InstancePerLifetimeScope();

            builder.RegisterType<AssignedEngineerRepository>().As<IAssignedEngineerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AssignedEngineerService>().As<IAssignedEngineerService>().InstancePerLifetimeScope();

            // Calendar
            builder.RegisterType<CalendarService>().As<ICalendarService>().InstancePerLifetimeScope();

            builder.RegisterType<WorkOrderMaintenanceCostRepository>().As<IWorkOrderMaintenanceCostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkOrderMaintenanceCostService>().As<IWorkOrderMaintenanceCostService>().InstancePerLifetimeScope();

            builder.RegisterType<WorkOrderTaskListRepository>().As<IWorkOrderTaskListRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkOrderTaskListService>().As<IWorkOrderTaskListService>().InstancePerLifetimeScope();
            #endregion

            #region User Mangement
            // Customer
            builder.RegisterType<CustomerTypeRepository>().As<ICustomerTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerTypeService>().As<ICustomerTypeService>().InstancePerLifetimeScope();
            // Customer
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            // Plan
            builder.RegisterType<PlanRepository>().As<IPlanRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PlanService>().As<IPlanService>().InstancePerLifetimeScope();

            // PlanPermission
            builder.RegisterType<PlanPermissionRepository>().As<IPlanPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PlanPermissionService>().As<IPlanPermissionService>().InstancePerLifetimeScope();

            // Membership
            builder.RegisterType<MembershipRepository>().As<IMembershipRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerLifetimeScope();

            // Billing
            builder.RegisterType<BillingRepository>().As<IBillingRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BillingService>().As<IBillingService>().InstancePerLifetimeScope();

            // PaymentType
            builder.RegisterType<PaymentTypeRepository>().As<IPaymentTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentTypeService>().As<IPaymentTypeService>().InstancePerLifetimeScope();

            // UpcomingPayment
            builder.RegisterType<UpcomingPaymentRepository>().As<IUpcomingPaymentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UpcomingPaymentService>().As<IUpcomingPaymentService>().InstancePerLifetimeScope();

            #endregion

            #region CMS Schema

            // Article
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<ArticleImageRepository>().As<IArticleImageRepository>().InstancePerLifetimeScope();

            //UsefulResource
            builder.RegisterType<UsefulResourceRepository>().As<IUsefulResourceRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UsefulResourceService>().As<IUsefulResourceService>().InstancePerLifetimeScope();

            //FrequentlyAskedQuestion
            builder.RegisterType<FrequentlyAskedQuestionRepository>().As<IFrequentlyAskedQuestionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FrequentlyAskedQuestionService>().As<IFrequentlyAskedQuestionService>().InstancePerLifetimeScope();

            //ChannelVideo
            builder.RegisterType<ChannelVideoRepository>().As<IChannelVideoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChannelVideoService>().As<IChannelVideoService>().InstancePerLifetimeScope();

            //InquiryQuestion
            builder.RegisterType<InquiryQuestionRepository>().As<IInquiryQuestionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<InquiryQuestionService>().As<IInquiryQuestionService>().InstancePerLifetimeScope();

            //InquiryQuestionReply
            builder.RegisterType<InquiryQuestionReplyRepository>().As<IInquiryQuestionReplyRepository>().InstancePerLifetimeScope();

            //ContactInfo
            builder.RegisterType<ContactInfoRepository>().As<IContactInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ContactInfoService>().As<IContactInfoService>().InstancePerLifetimeScope();

            // Feature
            builder.RegisterType<FeatureRepository>().As<IFeatureRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FeatureService>().As<IFeatureService>().InstancePerLifetimeScope();

            #endregion


        }
    }
}
