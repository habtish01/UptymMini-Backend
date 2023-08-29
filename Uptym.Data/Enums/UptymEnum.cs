using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.Enums
{
    #region Application
    public enum ContinentEnum
    {
        Asia = 1,
        Africa = 2,
        NorthAmerica = 3,
        SouthAmerica = 4,
        Antarctica = 5,
        Europe = 6,
        Australia = 7
    }
    public enum CountryPeriodEnum
    {
        Weekly = 1,
        Monthly = 2,
        Quarterly = 3,
        Annualy = 4
    }

    public enum ApplicationRolesEnum
    {
        SuperAdmin = 1,
        Finance = 2,
        Admin = 3,
        MaintenanceManager = 4,
        Facility = 5,
        Engineer = 6,
        Encoders = 7,
        ProductionManager = 8,
        LeadEngineer =10,
        SeniorEngineer = 11,
        Coordinator = 12
    }
    public enum UserRoleLevelsEnum
    {
        MaintenanceManager = 1,
        Facility = 2,
        Engineer = 3,
        Encoders = 4
    }

    public enum UserStatusEnum
    {
        Active = 1,
        NotActive = 2,
        Locked = 3
    }
    public enum UserTransactionTypesEnum
    {
        NameChanging = 1,
        IsActiveChanging = 2,
        EmailChanging = 3,
        PasswordChanging = 4,
        PhoneChanging = 5,
        AddressChanging = 6,
        AccountLock = 7,
        Login = 8,
        Logout = 9,
        ForgetPassword = 10,
        ResetPassword = 11,
    }
    #endregion

    #region Static Lookups
    public enum EquipmentStatusEnum
    {
        Functional = 1,
        PartialyFunctional = 2,
        NonFunctional = 3,
    }
    public enum ScheduleIntervalEnum
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Year = 4,
    }
    public enum MetricesEnum
    {
        Number = 1,
        YesNo = 2,
        Descriptive = 3,
     }

    public enum ContractTypeEnum
    {
        Warranty = 1,
        Service = 2,
    }
    public enum PriorityEnum
    {
        Low = 1,
        Medium = 2,
        High = 3,
    }
    public enum AutoScheduleStatus
    {
        Active = 1,
        NotActive = 2,
        Failed = 3,
        Canceled = 4,
        Success = 5,
    }

    public enum WorkOrderTypeEnum
    {
        Curative = 1,
        Preventive = 2,
        ScheduledPreventive = 3,
        Installation = 4,
        Upgrade = 5,
        Inspection = 6,
    }
    public enum WorkOrderStatusTypeEnum
    {
        New = 1,
        Scheduled = 2,
        Maintained = 3,
        Accepted = 4,
        Refused = 5,
        Approved = 6,
        Confirmed = 7,
        Rejected = 8,
        Forwarded = 9,

    }
    public enum MaintenanceCheckListTypeEnum
    {
        Master = 1,
        Customer = 2
    }
    public enum EquipmentScheduleTypeEnum
    {
        Preventive = 1,
        Inspection = 2
    }

    public enum AssignedtoEnum
    {
        HealthFacilityUser = 1,
        Engineer =2,
     }
    #endregion

    public enum DocumentationTypesEnum
    {
        Installation = 1,
        Troubleshoot = 2,
        UserManual = 3,
        OperationsManual = 4,
        TechnicalManual = 5,
        TechnicalBook = 6
    }

    #region User Management
    public enum CustomerTypesEnum
    {
        FreelanceEngineer = 1,
        Vendors = 2,
        Facilities = 3,
    }

    public enum PlansEnum
    {
        Trial = 1,
        Basic = 2,
        Premium = 3,
        Business = 4,
        Analytics = 5,
    }
    public enum PermissionsEnum
    {
        EquipementManagement = 1,
        MaintenanceManagement = 2,
        Reports = 3,
        Analytics = 4,
        ManageHealthFacilities = 5,
        ViewJobs = 6,
        BuildProfile = 7,
        ApplyJobs = 8,
        ManageMetaData = 9,
    }
    public enum ActionOfAuditEnum
    {
        Create = 1,
        Update = 2,
        Activate = 3,
        Deactivate = 4,
        CreateFromDuplicate = 5
    }
    public enum MembershipStatusEnum
    {
        Request = 1,
        Expired = 2,
        Active = 3,
        Disabled = 4
    }
    public enum BillingStatusEnum
    {
        InProgress = 1,
        Active = 2, // Approved
        NotActive = 3,
        Locked = 4,
        Expired = 5
    }

    public enum PaymentTypesEnum
    {
        ManualPayment = 1,
        PayPal = 2,
        Stripe = 3,
    }

    public enum NormalStatusEnum
    {
        Active = 1,
        NotActive = 2,
        Locked = 3,
    }
    public enum ReminderStatusEnum
    {
        Sent = 1,
        NotSent = 2,
    }
    public enum PaypalPlanStatusEnum
    {
        Active = 1,
        Inactive = 2
    }
    public enum ExpirationStatusEnum
    {
        Active = 1,
        Expired = 2,
        ExtraExpired = 3
    }
    #endregion

    #region Widgets
    public enum WidgetTagEnum
    {
        //workorder
        NumberOfNewWorkOrder = 1,
        NumberOfWorkOrderAwaitingConfirmation = 2,
        NumberOfUnhandledInspection = 3,
        NumberOfOverdueWorkOrder = 4,
        NumberOfOpenWorkOrder = 5,
        NumberOfClosedWorkOrder=15,
        //equipment
        NumberOfNotFunctionalEquipment = 6,
        //OperatorFailure
        NumberOfFailureByOperator = 16,
        //MaintenanceCost


        //Instrument Total Summary
        AvgInstrumentUptime = 7,
        AvgInstrumentDowntime = 8,

        //
        Mtbf = 9,
        Mrttr = 10,

        //PMC
        Pmc = 11,

        EquipmentContratSummary = 12,
        FrequentlyMaintainedInstruments = 13,

         // IMC
         Imc = 14
    }

    #endregion
}
