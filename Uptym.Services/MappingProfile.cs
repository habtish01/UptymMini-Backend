using System;
using System.Collections.Generic;
using AutoMapper;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.CMSSchema;
using Uptym.Data.DbModels.EquipmentSchema;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.DbModels.SubscriptionSchema;
using Uptym.Data.DbModels.sp;
using Uptym.DTO.CMS.Article;
using Uptym.DTO.CMS.ArticleImage;
using Uptym.DTO.CMS.ChannelVideo;
using Uptym.DTO.CMS.ContactInfo;
using Uptym.DTO.CMS.Feature;
using Uptym.DTO.CMS.FrequentlyAskedQuestion;
using Uptym.DTO.CMS.InquiryQuestion;
using Uptym.DTO.CMS.InquiryQuestionReply;
using Uptym.DTO.CMS.UsefulResource;
using Uptym.DTO.Common;
using Uptym.DTO.Configuration;
using Uptym.DTO.Configuration.ConfigurationAudit;
using Uptym.DTO.Equipment.DynamicEquipment;
using Uptym.DTO.Configuration.Menu;
using Uptym.DTO.Equipment.Equipment;
using Uptym.DTO.Maintenance;
using Uptym.DTO.Maintenance.Calendar;
using Uptym.DTO.Maintenance.WorkOrder;
using Uptym.DTO.Maintenance.WorkOrderHeader;
using Uptym.DTO.Maintenance.WorkOrderStatus;
using Uptym.DTO.Metadata;
using Uptym.DTO.Metadata.AutoSchedule;
using Uptym.DTO.Metadata.EquipmentCategory;
using Uptym.DTO.Metadata.EquipmentContract;
using Uptym.DTO.Metadata.EquipmentLookup;
using Uptym.DTO.Metadata.EquipmentLookupDocumentation;
using Uptym.DTO.Metadata.EquipmentSchedule;
using Uptym.DTO.Metadata.ErrorCode;
using Uptym.DTO.Metadata.HealthFacility;
using Uptym.DTO.Metadata.HealthFacilityType;
using Uptym.DTO.Metadata.Manufacturer;
using Uptym.DTO.Metadata.ProblemType;
using Uptym.DTO.Metadata.Sparepart;
using Uptym.DTO.Metadata.SparepartCategory;
using Uptym.DTO.Metadata.TaskList;
using Uptym.DTO.Metadata.TaskListDetail;

using Uptym.DTO.Security;
using Uptym.DTO.Subscription.Billing;
using Uptym.DTO.Subscription.Customer;
using Uptym.DTO.Subscription.CustomerType;
using Uptym.DTO.Subscription.Membership;
using Uptym.DTO.Subscription.PaymentType;
using Uptym.DTO.Subscription.Permission;
using Uptym.DTO.Subscription.Plan;
using Uptym.DTO.Subscription.PlanPermission;
using static Uptym.DTO.Equipment.Equipment.FunctionalEquipmentDto;
using Uptym.DTO.Equipment.EquipmentDocumentation;
using Uptym.DTO.Metadata.EquipmentType;
using Uptym.DTO.Metadata.Operator;
using Uptym.DTO.Equipment.EquipmentOperator;
using Uptym.DTO.Equipment.Timesheet;
using Uptym.DTO.Equipment.Task;
using Uptym.DTO.Equipment.AutoScheduleAssignedEngineers;
using Uptym.DTO.Metadata.MaintenanceCheckList;

namespace Uptym.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<ApplicationUser, AuthorizedUserDto>().ReverseMap();

            #region "Metadata Mappings"
            //Equipment Category
            CreateMap<EquipmentCategory, EquipmentCategoryDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<EquipmentCategory, ExportEquipmentCategoryDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();


            CreateMap<HealthFacility, ImportHealthFacilityDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
              "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
              .ReverseMap();

            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, ExportFunctionalEquipmentFilterDto>().ReverseMap();
            CreateMap<EquipmentCategory, EquipmentCategoryDrp>().ReverseMap();
            // CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, ExportFunctionalEquipmentDto >().ReverseMap();
            // CreateMap<Equipment, EquipmentCategoryDrp>().ReverseMap();

            CreateMap<Manufacturer, ManufacturerDto>()
                  .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                  "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                  .ReverseMap(); ;
            CreateMap<Manufacturer, ManufacturerDrp>().ReverseMap();
            CreateMap<Manufacturer, GetAllManufacturerGroupDrp>().ReverseMap();

            CreateMap<Manufacturer, ExportManufacturerDto>()
             .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
             "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
             .ReverseMap();
            CreateMap<EquipmentType, EquipmentTypeDto>()
             .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
             "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
             .ReverseMap();
            CreateMap<EquipmentType, EquipmentTypeDrp>().ReverseMap();
            CreateMap<EquipmentType, ExportEquipmentTypeDto>().ReverseMap();


            //Health Facility
            CreateMap<HealthFacility, HealthFacilityDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<HealthFacility, ExportHealthFacilityDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();

            CreateMap<WorkOrderHeader, ExportVendorMaintenanceDto>().ReverseMap();
            CreateMap<WorkOrderHeader, ExportWorkOrderHeaderReportFilterDto>().ReverseMap();
            CreateMap<WorkOrderHeader, ExportCurativeMaintenanceFilterDto>().ReverseMap();
            CreateMap<WorkOrderHeader, ExportWorkOrderHeaderReportFilterDto>().ReverseMap();


            CreateMap<HealthFacility, HealthFacilityFilterDto>().ReverseMap();
            CreateMap<HealthFacility, HealthFacilityDrp>()
                .ReverseMap();
            CreateMap<HealthFacility, SectionDto>().ReverseMap();

            CreateMap<HealthFacility, HealthFacilityDrp>().ReverseMap();

            //Health Facility Type
            CreateMap<HealthFacilityType, HealthFacilityTypeDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<HealthFacilityType, ExportHealthFacilityTypeDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<HealthFacilityType, HealthFacilityTypeDrp>().ReverseMap();

            //Country
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<Country, ExportCountryDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<Country, CountryDrp>().ReverseMap();


            //CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, ImportEquipmentDto>()
            // .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
            // "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
            // .ReverseMap();


            //CreateMap<MaintenanceAction, CountryDrp>().ReverseMap();

            // Region
            CreateMap<Region, RegionDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<Region, ExportRegionDto>()
               .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
               .ReverseMap();
            CreateMap<Region, RegionDrp>().ReverseMap();
            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, ImportEquipmentDto>().ReverseMap();



            // main

            CreateMap<MaintenanceAction, MaintenanceActionDto>()
               .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
               .ReverseMap();
            CreateMap<MaintenanceAction, MaintenanceActionDrp>().ReverseMap();
            #endregion

            #region "Equipment Mappings"
            //Equipment Lookups
            CreateMap<EquipmentLookup, EquipmentLookupDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<EquipmentLookup, ExportEquipmentLookupDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<EquipmentLookup, EquipmentLookupDrp>()
                            .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentType.Name))
                            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                            .ForMember(dest => dest.EquipmentCategoryName, opt => opt.MapFrom(src => src.EquipmentCategory.Name))
                            .ReverseMap();
            //Problem Type
            CreateMap<ProblemType, ProblemTypeDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<ProblemType, ExportProblemTypeDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<ProblemType, ProblemTypeDrp>().ReverseMap();

            //Spareparts
            CreateMap<Sparepart, SparepartDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();

            // CreateMap<Sparepart, SparepartEquipmentDetail>()
            //.ForMember(dest => dest.SparePartId, opt => opt.MapFrom(src => src.Id))
            //.ReverseMap();

            CreateMap<Data.DbModels.MetadataSchema.SparepartCategories, SparepartCategoriesDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.MetadataSchema.SparepartCategories, ExportSparepartCategoryDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
              "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
              .ReverseMap();

            CreateMap<Data.DbModels.MetadataSchema.SparepartSubCategories, SparepartSubCategoriesDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.MetadataSchema.SparepartSubCategories, ExportSparepartSubCategoryDto>()
               .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
               "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
               .ReverseMap();

            CreateMap<Data.DbModels.MetadataSchema.SparepartInventoryDetail, SparepartInventoryDetailDto>().ReverseMap();
            CreateMap <Data.DbModels.MetadataSchema.SparepartEquipmentDetail, SparepartEquipmentDetailDto >().ReverseMap();

            CreateMap<Sparepart, ExportSparepartDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<Sparepart, SparepartDrp>().ReverseMap();

            CreateMap<EquipmentLookupSparepart, EquipmentLookupSparepartDto>().ReverseMap();



            //Error Codes
            CreateMap<ErrorCode, ErrorCodeDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<ErrorCode, ExportErrorCodeDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<ErrorCode, ErrorCodeDrp>().ReverseMap();
            //Equipment Schedule
            CreateMap<EquipmentLookupSchedule, EquipmentLookupScheduleDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<EquipmentLookupSchedule, ExportEquipmentLookupScheduleDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();

            // Operators
            CreateMap<Operator, OperatorDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
               "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
               .ReverseMap();
            CreateMap<Operator, ExportOperatorDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<Operator, OperatorDrp>().ReverseMap();
            //CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, EquipmentDto>().ReverseMap();

            //Equipment Documentation Lookup
            CreateMap<EquipmentLookupDocumentation, EquipmentLookupDocumentationDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<EquipmentLookupDocumentation, ExportEquipmentLookupDocumentationDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<EquipmentLookupDocumentation, EquipmentLookupDocumentationDrp>().ReverseMap();

            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, EquipmentDto>()
                        .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.HealthFacility.Name))
                        .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.EquipmentLookup.Name))
                        .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentLookup.EquipmentType.Name))
                        .ForMember(dest => dest.EquipmentCatagoryName, opt => opt.MapFrom(src => src.EquipmentLookup.EquipmentCategory.Name))
                        .ForMember(dest => dest.EquipmentManufacturerName, opt => opt.MapFrom(src => src.EquipmentLookup.Manufacturer.Name))
                        .ForMember(dest => dest.EquipmentStatusId, opt => opt.MapFrom(src => src.EquipmentStatusId))


                        .ReverseMap();

            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, EquipmentWorkOrderHeaderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.UseDestinationValue())
                 .ReverseMap();

            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, DynamicEquipmentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.UseDestinationValue())
               .ForMember(dest => dest.MachineCurrent, opt => opt.MapFrom(src => src.MachineCurrent))
            
                .ForMember(dest => dest.EquipDetails, opt => opt.MapFrom(src => src.EquipDetails))
             .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, DynamicEquipmentDto>()
            .ForMember(dest => dest.WorkOrderHeaderId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

            #endregion



            #region Configuration
            CreateMap<Data.DbModels.ConfigurationSchema.Configuration, ConfigurationDto>().ReverseMap();
            CreateMap<Data.DbModels.ConfigurationSchema.Configuration, Data.DbModels.ConfigurationSchema.ConfigurationAudit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ReverseMap();
            CreateMap<Data.DbModels.ConfigurationSchema.ConfigurationAudit, ConfigurationAuditDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<Data.DbModels.ConfigurationSchema.ConfigurationAudit, ExportConfigurationAuditDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                .ReverseMap();

            CreateMap<Data.DbModels.ConfigurationSchema.Menu, MenuDto>().ReverseMap();

            #endregion

            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<ApplicationUser, AuthorizedUserDto>().ReverseMap();
            CreateMap<ApplicationUser, ExportUserDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDetailsDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UserDrp>().ReverseMap();
            CreateMap<ApplicationUser, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerDrp>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src == null ?
                "System" : $"{src.FirstName} " + $"{src.LastName}"))
                .ReverseMap();



            #region User Management
            CreateMap<Data.DbModels.SubscriptionSchema.Customer, CustomerDto>().ReverseMap();
            CreateMap<Data.DbModels.SubscriptionSchema.Customer, ExportCustomerDto>().ReverseMap();
            CreateMap<Plan, PlanDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();

            // CustomerType

            CreateMap<CustomerType, CustomerTypeDto>().ReverseMap();

            // PlanPermission
            CreateMap<PlanPermission, PlanPermissionDto>().ReverseMap();
            CreateMap<PlanPermission, PlanPermissionDrp>().ReverseMap();

            // Membership
            CreateMap<Membership, MembershipDto>().ReverseMap();

            // Billing
            CreateMap<Billing, BillingDto>().ReverseMap();
            CreateMap<Billing, AutoBillingDto>().ReverseMap();
            CreateMap<Billing, ExportBillingDto>().ReverseMap();

            // PaymentType
            CreateMap<PaymentType, PaymentTypeDto>().ReverseMap();

            // Billing
            CreateMap<UpcomingPayment, UpcomingPaymentDto>().ReverseMap();
            CreateMap<UpcomingPayment, ExportUpcomingPaymentDto>().ReverseMap();

            // Role
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();


            #endregion

            #region CMS Schema
            // Article
            CreateMap<ArticleImage, ArticleImageDto>().ReverseMap();
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.ArticleImageDtos, opt => opt.MapFrom(src => src.ArticleImages))
                .ReverseMap();

            //UsefulResource
            CreateMap<UsefulResource, UsefulResourceDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
              .ReverseMap();

            //FrequentlyAskedQuestion
            CreateMap<FrequentlyAskedQuestion, FrequentlyAskedQuestionDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
              .ReverseMap();
            CreateMap<FrequentlyAskedQuestion, FrequentlyAskedQuestionDrp>().ReverseMap();

            //ChannelVideo
            CreateMap<ChannelVideo, ChannelVideoDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
              .ReverseMap();

            //InquiryQuestion
            CreateMap<InquiryQuestion, InquiryQuestionDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
              .ForMember(dest => dest.InquiryQuestionReplyDtos, opt => opt.MapFrom(src => src.InquiryQuestionReplies))
              .ReverseMap();
            CreateMap<InquiryQuestion, InquiryQuestionDrp>().ReverseMap();

            //InquiryQuestionReply
            CreateMap<InquiryQuestionReply, InquiryQuestionReplyDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
              .ReverseMap();
            CreateMap<InquiryQuestionReply, InquiryQuestionReplyDrp>().ReverseMap();

            //ContactInfo
            CreateMap<ContactInfo, ContactInfoDto>()
             .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
             .ReverseMap();

            //Feature
            CreateMap<Feature, FeatureDto>()
               .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
               .ReverseMap();
            #endregion

            #region "Maintenance Mappings"
            //Work Order Header
            //CreateMap<WorkOrderHeader, WorkOrderHeaderDto>()
            //    .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
            //    "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
            //    .ReverseMap();
            // Work Orders
            CreateMap<WorkOrder, WorkOrderDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ForMember(dest => dest.AssignedEnginneerName, opt => opt.MapFrom(src => src.AssignedEngineer == null ?
                "System" : $"{src.AssignedEngineer.FirstName} " + $"{src.AssignedEngineer.LastName}"))
                .ForMember(dest => dest.ContractorName, opt => opt.MapFrom(src => src.Contractor == null ?
                "System" : $"{src.Contractor.FirstName} " + $"{src.Contractor.LastName}"))
                .ForMember(dest => dest.MaintenanceHeadName, opt => opt.MapFrom(src => src.MaintenanceHead == null ?
                "System" : $"{src.MaintenanceHead.FirstName} " + $"{src.MaintenanceHead.LastName}"))
                .ForMember(dest => dest.ManagerPriorityName, opt => opt.MapFrom(src => src.ManagerPriority == null ?
                "Unknown" : $"{src.ManagerPriority.Name} "))
                .ReverseMap();


            //CreateMap<AssignedEngineer, AssignedEngineerDto>()
            //    .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
            //    "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
            //    .ForMember(dest => dest.AssignedEnginneerName, opt => opt.MapFrom(src => src.Engineers == null ?
            //    "System" : $"{src.Engineers.FirstName} " + $"{src.Engineers.LastName}"))

            //    .ReverseMap();


            CreateMap<WorkOrderDto, ScheduleDto>()
                .ReverseMap();


            CreateMap<WorkOrder, ExportWorkOrderDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();
            CreateMap<WorkOrder, WorkOrderDrp>().ReverseMap();
            CreateMap<WorkOrder, WorkOrderListForEngineerDto>()
                       .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                       "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                       .ForMember(dest => dest.AssignedEnginneerName, opt => opt.MapFrom(src => src.AssignedEngineer == null ?
                       "System" : $"{src.AssignedEngineer.FirstName} " + $"{src.AssignedEngineer.LastName}"))
                       .ForMember(dest => dest.ContractorName, opt => opt.MapFrom(src => src.Contractor == null ?
                       "System" : $"{src.Contractor.FirstName} " + $"{src.Contractor.LastName}"))
                       .ForMember(dest => dest.MaintenanceHeadName, opt => opt.MapFrom(src => src.MaintenanceHead == null ?
                       "System" : $"{src.MaintenanceHead.FirstName} " + $"{src.MaintenanceHead.LastName}"))
                       .ForMember(dest => dest.ManagerPriorityName, opt => opt.MapFrom(src => src.ManagerPriority == null ?
                       "Unknown" : $"{src.ManagerPriority.Name} "))
                        .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.WorkOrderHeader.Equipment.HealthFacility.Name))
                       .ReverseMap();
            // Work Order Status
            CreateMap<WorkOrderStatus, WorkOrderStatusDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ForMember(dest => dest.WorkOrderTitle, opt => opt.MapFrom(src => src.WorkOrder == null ?
                "Unknown" : $"{src.WorkOrder.Title} "))
                .ForMember(dest => dest.WorkOrderStatusTypeName, opt => opt.MapFrom(src => src.WorkOrderStatusType == null ?
                "Unknown" : $"{src.WorkOrderStatusType.Name} "))
                .ForMember(dest => dest.By, opt => opt.MapFrom(src => $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ReverseMap();


            //CreateMap<WorkOrder, WorkOrderDto>().ReverseMap();

            CreateMap<Data.DbModels.MaintenanceSchema.WorkOrderSparepart, WorkOrderSparepartDto>()
               .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
               .ReverseMap();
            //CreateMap<Data.DbModels.MaintenanceSchema.WorkOrderSparepart, ExportWorkOrderSparepartDto>()
            //       .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
            //       .ReverseMap();
            CreateMap<Data.DbModels.MaintenanceSchema.WorkOrderSparepart, WorkOrderHeaderDrp>().ReverseMap();
            CreateMap<WorkOrderHeader, WorkOrderHeaderDto>().ReverseMap();

            //    CreateMap<WorkOrderSparepart, WorkOrderSparepartDto>().ReverseMap();



            CreateMap<WorkOrderHeader, WorkOrderHeader>().ReverseMap();


            CreateMap<Data.DbModels.MaintenanceSchema.WorkOrderMaintenanceCost, WorkOrderMaintenanceCostDto>()
              .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
              .ReverseMap();
            CreateMap<Data.DbModels.MaintenanceSchema.WorkOrderMaintenanceCost, WorkOrderHeaderDrp>().ReverseMap();


            CreateMap<Data.DbModels.MetadataSchema.MaintenanceCheckList, MaintenanceCheckListDto>().ReverseMap();
            CreateMap<Data.DbModels.MaintenanceSchema.AssignedEngineer, AssignedEngineerDto>().ReverseMap();
            CreateMap<Data.DbModels.MaintenanceSchema.MaintenanceTasks, MaintenanceTasksDto>().ReverseMap();
            CreateMap<Data.DbModels.MetadataSchema.MetadataTasks, MetadataTasksDto>().ReverseMap();
            CreateMap<Data.DbModels.MetadataSchema.MaintenanceCheckListDetail, DTO.Metadata.MaintenanceCheckListDetail.MaintenanceCheckListDetailDto>().ReverseMap();
            CreateMap<MaintenanceTasks, DTO.Metadata.MaintenanceCheckListDetail.MaintenanceTaskDetailDto > ().ReverseMap();
            CreateMap<Tasks, DTO.Metadata.MaintenanceCheckListDetail.MaintenanceTaskDetailDto> ().ReverseMap();
            CreateMap<Data.DbModels.MetadataSchema.SubProblemType, SubProblemTypeDto>().ReverseMap();

            //CreateMap<WorkOrderStatus, WorkOrderStatusDto>().ReverseMap();
            CreateMap<MaintenanceAction, MaintenanceActionDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, EquipmentWorkOrderHeaderDto>()
                .ForMember(dest => dest.WorkOrderHeaderId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, WorkOrderHeaderWithWorkordersDto>()
                .ForMember(dest => dest.WorkOrderHeader_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WorkOrderHeader_Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.WorkOrderHeader_Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.WorkOrderHeader_EquipmentFailureDate, opt => opt.MapFrom(src => src.EquipmentFailureDate))
                .ForMember(dest => dest.WorkOrderHeader_EquipmentId, opt => opt.MapFrom(src => src.EquipmentId))
                .ForMember(dest => dest.WorkOrderHeader_EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
                .ForMember(dest => dest.WorkOrderHeader_WorkorderTypeId, opt => opt.MapFrom(src => src.WorkorderTypeId))
                .ForMember(dest => dest.Equipments_EquipmentName, opt => opt.MapFrom(src => src.Equipment.Name))
                .ForMember(dest => dest.Equipments_Tag, opt => opt.MapFrom(src => src.Equipment.Tag))
                .ForMember(dest => dest.WorkOrderHeader_ErrorCodeId, opt => opt.MapFrom(src => src.ErrorCodeId))
                .ForMember(dest => dest.WorkOrderHeader_IsClosed, opt => opt.MapFrom(src => src.IsClosed))
                .ForMember(dest => dest.WorkOrderHeader_ProblemTypeId, opt => opt.MapFrom(src => src.ProblemTypeId))
                .ForMember(dest => dest.WorkOrderHeader_ReportedById, opt => opt.MapFrom(src => src.ReportedById))
                .ForMember(dest => dest.WorkOrderHeader_ReportedDate, opt => opt.MapFrom(src => src.ReportedDate))
                .ForMember(dest => dest.WorkOrderHeader_ReporterPriorityId, opt => opt.MapFrom(src => src.ReporterPriority))
                .ForMember(dest => dest.WorkOrderHeader_AutoScheduleId, opt => opt.MapFrom(src => src.AutoScheduleId))
                // .ForMember(dest => dest.WorkOrderHeader_AutoScheduleMetrics, opt => opt.MapFrom(src => src.AutoSchedule.MetricsId))
                .ReverseMap();

            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, WorkOrderHeaderWithWorkordersDto>()
                .ForMember(dest => dest.equipments_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.equipments_StatusId, opt => opt.MapFrom(src => src.EquipmentStatusId))
                .ForMember(dest => dest.Equipments_FacilityId, opt => opt.MapFrom(src => src.HealthFacilityId))
                .ForMember(dest => dest.Equipments_Tag, opt => opt.MapFrom(src => src.Tag))
                .ReverseMap();


            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrder, WorkOrderHeaderWithWorkordersDto>()
                .ForMember(dest => dest.WorkOrder_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WorkOrder_MaintenanceHeadId, opt => opt.MapFrom(src => src.MaintenanceHeadId))
                 .ForMember(dest => dest.WorkOrder_PlannedStartDate, opt => opt.MapFrom(src => src.PlannedStartDate))
                .ForMember(dest => dest.WorkOrder_PlannedEndDate, opt => opt.MapFrom(src => src.PlannedEndDate))
                .ForMember(dest => dest.WorkOrder_Remark, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.WorkOrder_Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.WorkOrder_AssignedEngineerId, opt => opt.MapFrom(src => src.AssignedEngineerId))
                .ForMember(dest => dest.WorkOrder_WorkOrderHeaderId, opt => opt.MapFrom(src => src.WorkOrderHeaderId))
                .ForMember(dest => dest.WorkOrder_ContractorId, opt => opt.MapFrom(src => src.ContractorId))
                 .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderStatus, WorkOrderHeaderWithWorkordersDto>()
                .ForMember(dest => dest.equipments_StatusId, opt => opt.MapFrom(src => src.EquipmentStatusId))
                .ForMember(dest => dest.WorkOrderStatus_Status, opt => opt.MapFrom(src => src.EquipmentStatusId))
                .ForMember(dest => dest.WorkOrder_Id, opt => opt.MapFrom(src => src.WorkOrderId))
                .ForMember(dest => dest.WorkOrderHeader_Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.WorkOrder_Remark, opt => opt.MapFrom(src => src.Remark))
                 .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, CompleteWorkorderInformationDto>()
                .ForMember(dest => dest.WorkOrderHeader_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WorkOrderHeader_Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.WorkOrderHeader_Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.WorkOrderHeader_EquipmentFailureDate, opt => opt.MapFrom(src => src.EquipmentFailureDate))
                .ForMember(dest => dest.WorkOrderHeader_EquipmentId, opt => opt.MapFrom(src => src.EquipmentId))
                .ForMember(dest => dest.WorkOrderHeader_EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
                .ForMember(dest => dest.WorkOrderHeader_WorkorderTypeId, opt => opt.MapFrom(src => src.WorkorderTypeId))
                .ForMember(dest => dest.Equipments_EquipmentName, opt => opt.MapFrom(src => src.Equipment.Name))
                .ForMember(dest => dest.Equipments_Tag, opt => opt.MapFrom(src => src.Equipment.Tag))
                .ForMember(dest => dest.WorkOrderHeader_ErrorCodeId, opt => opt.MapFrom(src => src.ErrorCodeId))
                .ForMember(dest => dest.WorkOrderHeader_IsClosed, opt => opt.MapFrom(src => src.IsClosed))
                .ForMember(dest => dest.WorkOrderHeader_ProblemTypeId, opt => opt.MapFrom(src => src.ProblemTypeId))
                .ForMember(dest => dest.WorkOrderHeader_ReportedById, opt => opt.MapFrom(src => src.ReportedById))
                .ForMember(dest => dest.WorkOrderHeader_ReportedDate, opt => opt.MapFrom(src => src.ReportedDate))
                .ForMember(dest => dest.WorkOrderHeader_ReporterPriorityId, opt => opt.MapFrom(src => src.ReporterPriority))
                .ForMember(dest => dest.Equipments_FacilityName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))
                .ReverseMap();

         //   CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrder, DynamicWorkorderDto>()
         //.ForMember(dest => dest.WorkOrder_Id, opt => opt.MapFrom(src => src.Id))
         //.ForMember(dest => dest.MaintenanceTasks, opt => opt.MapFrom(src => src.MaintenanceTasks))
         //.ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, DynamicWorkorderDto>()
               .ForMember(dest => dest.WorkOrderHeader_Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.WorkOrderHeader_Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.WorkOrderHeader_Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.WorkOrderHeader_EquipmentFailureDate, opt => opt.MapFrom(src => src.EquipmentFailureDate))
               .ForMember(dest => dest.WorkOrderHeader_EquipmentId, opt => opt.MapFrom(src => src.EquipmentId))
               .ForMember(dest => dest.WorkOrderHeader_EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
               .ForMember(dest => dest.WorkOrderHeader_WorkorderTypeId, opt => opt.MapFrom(src => src.WorkorderTypeId))
               .ForMember(dest => dest.Equipments_EquipmentName, opt => opt.MapFrom(src => src.Equipment.Name))
               .ForMember(dest => dest.Equipments_Tag, opt => opt.MapFrom(src => src.Equipment.Tag))
                 .ForMember(dest => dest.Equipments_Units, opt => opt.MapFrom(src => src.Equipment.Units))
               .ForMember(dest => dest.Equipments_FacilityName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))
               .ForMember(dest => dest.WorkOrderHeader_ErrorCodeId, opt => opt.MapFrom(src => src.ErrorCodeId))
               .ForMember(dest => dest.WorkOrderHeader_IsClosed, opt => opt.MapFrom(src => src.IsClosed))
               .ForMember(dest => dest.WorkOrderHeader_ProblemTypeId, opt => opt.MapFrom(src => src.ProblemTypeId))
               .ForMember(dest => dest.WorkOrderHeader_ReportedById, opt => opt.MapFrom(src => src.ReportedById))
               .ForMember(dest => dest.WorkOrderHeader_ReportedDate, opt => opt.MapFrom(src => src.ReportedDate))
               .ForMember(dest => dest.WorkOrderHeader_ReporterPriorityId, opt => opt.MapFrom(src => src.ReporterPriorityId))
               .ForMember(dest => dest.WorkOrderHeader_AutoScheduleId, opt => opt.MapFrom(src => src.AutoScheduleId))
               // .ForMember(dest => dest.WorkOrderHeader_AutoScheduleMetrics, opt => opt.MapFrom(src => src.AutoSchedule.MetricsId))
               .ForMember(dest => dest.WorkOrderHeader_AutoScheduledto, opt => opt.MapFrom(src => src.AutoSchedule.AssignedToId))
                .ForMember(dest => dest.WorkOrderHeader_ScheduleApproval, opt => opt.MapFrom(src => src.ScheduleApproval))
                 .ForMember(dest => dest.WorkOrderHeader_FailureTime, opt => opt.MapFrom(src => src.FailureTime))
                   .ForMember(dest => dest.WorkOrderHeader_CreatedOnDate, opt => opt.MapFrom(src => src.CreatedOn))
               .ReverseMap();

           // CreateMap<Uptym.Data.DbModels.EquipmentSchema.AutoSchedule, DynamicWorkorderDto>()
           //.ForMember(dest => dest.WorkOrderHeader_AutoScheduleIntervalId, opt => opt.MapFrom(src => src.IntervalId))
           
           //.ReverseMap();
            CreateMap<AutoScheduleFilterDto, DynamicWorkorderDto>()
             .ForMember(dest => dest.WorkOrderHeader_AutoScheduleIntervalId, opt => opt.MapFrom(src => src.IntervalId))
           .ReverseMap();
            //Auto Schedule 
            CreateMap<AutoSchedule, AutoScheduleDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
                "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
                .ForMember(dest => dest.AssignedEngineerIds, opt => opt.MapFrom(src => MapAutoScheduleAssignedEngineers(src.AutoScheduleAssignedEngineers)))
                .ReverseMap();

            CreateMap<AutoSchedule, ExportAutoScheduleDto>().ReverseMap();

            CreateMap<AutoScheduleAssignedEngineers, AutoScheduleAssignedEngineersDto>().ReverseMap();

            CreateMap<Tasks, TaskDto>().ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ?
            "System" : $"{src.Creator.FirstName} " + $"{src.Creator.LastName}"))
            .ReverseMap();

            CreateMap<EquipmentContract, EquipmentContractDto>().ReverseMap();
            CreateMap<EquipmentContract, EquipmentContractDrp>().ReverseMap();
            CreateMap<EquipmentOperator, EquipmentOperatorDto>().ReverseMap();
            CreateMap<EquipmentOperator, EquipmentOperatorDrp>().ReverseMap();
            CreateMap<EquipmentDocumentation, EquipmentDocumentationDto>().ReverseMap();
            CreateMap<Timesheet, TimesheetDto>().ReverseMap();
            // Mainteannce Acrion
            CreateMap<MaintenanceAction, MaintenanceActionDto>()
                           .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator == null ? "System" : $"{src.Creator.FirstName} {src.Creator.LastName}"))
                           .ReverseMap();

            CreateMap<MaintenanceAction, MaintenanceActionDrp>().ReverseMap();

            // Calendar
            CreateMap<WorkOrder, CalendarDto>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.PlannedStartDate))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.PlannedEndDate))
                .ReverseMap();
            CreateMap<DynamicWorkorderDto, MainCalendarDto>()
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.WorkOrder_PlannedStartDate))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.WorkOrder_PlannedEndDate))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.WorkOrderHeader_Title))
            .ReverseMap();




            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, FunctionalEquipmentDto>()
            .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.EquipmentLookup.Name))
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.EquipmentLookup.Manufacturer.Name))
            .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
            //.ForMember(dest => dest.InstalledDate, opt => opt.MapFrom(src => src.InstalledDate))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.HealthFacility.Code))
            .ForMember(dest => dest.LastPreventiveMaintenanceDate, opt => opt.MapFrom(src => src.LastPreventiveMaintenanceDate))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.HealthFacility.Region.Country.Name))
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.HealthFacility.Region.Name))
            .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.HealthFacility.Name))
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.EquipmentStatusId))

            .ReverseMap();
            //
            //
            CreateMap<Uptym.Data.DbModels.EquipmentSchema.Equipment, FunctionalEquipmentFilterDto>()
             .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.EquipmentLookup.Name))
             .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.EquipmentLookup.Manufacturer.Name))
             .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
             //.ForMember(dest => dest.InstalledDate, opt => opt.MapFrom(src => src.InstalledDate))
             .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.HealthFacility.Code))
             .ForMember(dest => dest.LastPreventiveMaintenanceDate, opt => opt.MapFrom(src => src.LastPreventiveMaintenanceDate))
             .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.HealthFacility.Region.Country.Name))
             .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.HealthFacility.Region.Name))
             .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.HealthFacility.Name))

             .ReverseMap();


            CreateMap<Uptym.Data.DbModels.sp.NewFreqMaintainedReport, FreqMaintainedDataDto>()
           .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.OperatorName))
            .ForMember(dest => dest.NumberOfFailure, opt => opt.MapFrom(src => src.NumberOfFailure))

                .ReverseMap();

            //

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, CurativeMaintenanceDto>()
             .ForMember(dest => dest.SerialNumbeName, opt => opt.MapFrom(src => src.Equipment.SerialNumber))
             .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
             .ForMember(dest => dest.ManufactureDate, opt => opt.MapFrom(src => src.Equipment.ManufactureDate))
             .ForMember(dest => dest.InstalledBy, opt => opt.MapFrom(src => src.Equipment.InstalledBy))
             .ForMember(dest => dest.InstalledDate, opt => opt.MapFrom(src => src.Equipment.InstalledDate))
         .ForMember(dest => dest.HealthFacilitiesName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))


             .ReverseMap();
            //


            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, CurativeMaintenanceFilterDto>()
             .ForMember(dest => dest.SerialNumbeName, opt => opt.MapFrom(src => src.Equipment.SerialNumber))
             .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
             .ForMember(dest => dest.ManufactureDate, opt => opt.MapFrom(src => src.Equipment.ManufactureDate))
             .ForMember(dest => dest.InstalledBy, opt => opt.MapFrom(src => src.Equipment.InstalledBy))
             .ForMember(dest => dest.InstalledDate, opt => opt.MapFrom(src => src.Equipment.InstalledDate))
              .ForMember(dest => dest.HealthFacilitiesName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))


             .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, VendorMaintenanceFilterDto>()
           .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
           .ForMember(dest => dest.ManufactureName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Manufacturer.Name))
           .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Name))
           .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Country.Name))
           .ForMember(dest => dest.InstalledDate, opt => opt.MapFrom(src => src.Equipment.InstalledDate))
           .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.ReportedBy.Customer.FirstName + src.ReportedBy.Customer.LastName))
          .ForMember(dest => dest.HealthFacilitiesName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))
           .ReverseMap();

            CreateMap<WorkOrder, EquipmentForMaintenceDetailsDto>()
            .ForMember(dest => dest.ErrorCodeName, opt => opt.MapFrom(src => src.WorkOrderHeader.ErrorCode.Name))
            .ForMember(dest => dest.ProblemTypeName, opt => opt.MapFrom(src => src.WorkOrderHeader.ProblemType.Name))
            .ForMember(dest => dest.EquipmentFailureDate, opt => opt.MapFrom(src => src.WorkOrderHeader.EquipmentFailureDate))
            .ForMember(dest => dest.ReportedDate, opt => opt.MapFrom(src => src.WorkOrderHeader.ReportedDate))
            .ForMember(dest => dest.MaintenanceAction, opt => opt.MapFrom(src => src.MaintenanceActionNotes))
            .ReverseMap();

            //  CreateMap<WorkOrder, WorkOrderSparepartDto>().ReverseMap();

            //CreateMap<WorkOrder, WorkOrderSparepartDto>()
            //.ForMember(dest => dest.SparePartName, opt => opt.MapFrom(src => src.WorkOrderHeader.Equipment.EquipmentLookup.Spareparts.Name))


            //.ReverseMap();
            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.MaintenanceAction, MaintenanceActionRepDto>()
            .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.EquipmentLookup.Name))
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.EquipmentLookup.Manufacturer.Name))
            .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.SerialNumber))
             .ForMember(dest => dest.WorkOrderStatusType, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderStatus))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.MaintenanceActionDate, opt => opt.MapFrom(src => src.MaintenanceActionDate))
            // .ForMember(dest => dest.LastPreventiveMaintenanceDate, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.LastPreventiveMaintenanceDate))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.HealthFacility.Region.Country.Name))
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.HealthFacility.Region.Name))
            .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.HealthFacility.Name))
            .ReverseMap();


            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.MaintenanceAction, MaintenanceActionRepFilterDto>()
          .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.EquipmentLookup.Name))
          .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.EquipmentLookup.Manufacturer.Name))
          .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.SerialNumber))
           .ForMember(dest => dest.WorkOrderStatusType, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderStatus))
          .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
          .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
          .ForMember(dest => dest.MaintenanceActionDate, opt => opt.MapFrom(src => src.MaintenanceActionDate))
          // .ForMember(dest => dest.LastPreventiveMaintenanceDate, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.LastPreventiveMaintenanceDate))
          .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.HealthFacility.Region.Country.Name))
          .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.HealthFacility.Region.Name))
          .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderHeader.Equipment.HealthFacility.Name))
          .ReverseMap();



            CreateMap<WorkOrder, EquipmentForMaintenceDetailsDto>()
              .ForMember(dest => dest.ErrorCodeName, opt => opt.MapFrom(src => src.WorkOrderHeader.ErrorCode.Name))
              .ForMember(dest => dest.ProblemTypeName, opt => opt.MapFrom(src => src.WorkOrderHeader.ProblemType.Name))
              .ForMember(dest => dest.EquipmentFailureDate, opt => opt.MapFrom(src => src.WorkOrderHeader.EquipmentFailureDate))
              .ForMember(dest => dest.ReportedDate, opt => opt.MapFrom(src => src.WorkOrderHeader.ReportedDate))
                 .ForMember(dest => dest.MaintenanceAction, opt => opt.MapFrom(src => src.MaintenanceActionNotes))
              .ReverseMap();


            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, VendorMaintenanceDto>()
            .ForMember(dest => dest.EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
            .ForMember(dest => dest.ManufactureName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Manufacturer.Name))
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Name))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Country.Name))
            .ForMember(dest => dest.InstalledDate, opt => opt.MapFrom(src => src.Equipment.InstalledDate))
           .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.ReportedBy.Customer.FirstName + src.ReportedBy.Customer.LastName))
            .ForMember(dest => dest.HealthFacilitiesName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))


            .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, WorkOrderHeaderReportDto>()
              .ForMember(dest => dest.WorkOrderHeader_EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
             .ForMember(dest => dest.WorkOrderHeader_Title, opt => opt.MapFrom(src => src.Title))
             .ForMember(dest => dest.WorkOrderHeader_Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Equipments_Tag, opt => opt.MapFrom(src => src.Equipment.Tag))
             .ForMember(dest => dest.WorkOrderHeader_EquipmentFailureDate, opt => opt.MapFrom(src => src.EquipmentFailureDate))
             .ForMember(dest => dest.WorkOrderHeader_ReportedDate, opt => opt.MapFrom(src => src.ReportedDate))
              .ForMember(dest => dest.WorkOrderStatus_endDate, opt => opt.MapFrom(src => src.UpdatedOn))

              .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Manufacturer.Name))
               .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Name))
               .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Country.Name))
              .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.ReportedBy.Customer.FirstName + src.ReportedBy.Customer.LastName))
              .ForMember(dest => dest.HealthFacilitiesName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))


             .ReverseMap();

            CreateMap<Uptym.Data.DbModels.MaintenanceSchema.WorkOrderHeader, WorkOrderHeaderReportFilterDto>()
                .ForMember(dest => dest.WorkOrderHeader_EquipmentLookupName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Name))
               .ForMember(dest => dest.WorkOrderHeader_Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.WorkOrderHeader_Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Equipments_Tag, opt => opt.MapFrom(src => src.Equipment.Tag))
               .ForMember(dest => dest.WorkOrderHeader_EquipmentFailureDate, opt => opt.MapFrom(src => src.EquipmentFailureDate))
               .ForMember(dest => dest.WorkOrderHeader_ReportedDate, opt => opt.MapFrom(src => src.ReportedDate))
                .ForMember(dest => dest.WorkOrderStatus_endDate, opt => opt.MapFrom(src => src.GetWSDate))
                 .ForMember(dest => dest.WorkOrderHeader_IsClosed, opt => opt.MapFrom(src => src.IsClosed))
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Equipment.EquipmentLookup.Manufacturer.Name))
                 .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Name))
                 .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Region.Country.Name))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.ReportedBy.Customer.FirstName + src.ReportedBy.Customer.LastName))
                .ForMember(dest => dest.HealthFacilitiesName, opt => opt.MapFrom(src => src.Equipment.HealthFacility.Name))
               .ReverseMap();

            CreateMap<Tasks, WorkOrderTaskList>().ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id));
            #endregion

        }

        public List<int> MapAutoScheduleAssignedEngineers(ICollection<AutoScheduleAssignedEngineers> src)
        {
            List<int> assignedEngineers = new List<int>();

            foreach (var i in src)
            {
                assignedEngineers.Add(i.AssignedEngineerId);
            }
            return assignedEngineers;
        }
    }


}
