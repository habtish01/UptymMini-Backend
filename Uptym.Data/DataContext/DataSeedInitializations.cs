using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Uptym.Data.DbModels.ConfigurationSchema;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.DbModels.SubscriptionSchema;
using Uptym.Data.Enums;
using Uptym.Data.ThirdPartyInfo;

namespace Uptym.Data.DataContext
{
    public class DataSeedInitializations
    {
        private static AppDbContext _appDbContext;
        private static UserManager<ApplicationUser> _userManager;
        private static IServiceProvider _serviceProvider;

        public static void Seed(AppDbContext appDbContext, IServiceProvider serviceProvider)
        {
            _appDbContext = appDbContext;
            _appDbContext.Database.EnsureCreated();
            //_appDbContext.Database.Migrate();
            _serviceProvider = serviceProvider;

            var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            SeedConfiguration();
            SeedApplicationRoles();
            SeedApplicationSuperAdmin();
            SeedUserTransactionTypes();
            SeedMenu();

            SeedContinents(); 
            //SeedCountries();
            //SeedCountryPeriods();

            //SeedContactInfo();
            SeedFeatures();
            SeedCustomerTypes();
            SeedPaymentTypes();
            SeedPlanPermissions();

            // Seed Metadatas
             SeedDocumentationTypes();



            //Seed Equipments
            SeedEquipmentScheduleType();
            SeedEquipmentScheduleInverval();
            SeedMetrices();
            //Seed Maintenance
            SeedPriority();
            SeedWorkOrderType();
            SeedWorkOrderStatusType();
        }
        private static void SeedConfiguration()
        {
            var config = _appDbContext.Configurations.FirstOrDefault();
            if (config == null)
            {
                var defaultConfig = new DbModels.ConfigurationSchema.Configuration()
                {
                    AccountLoginAttempts = 5,
                    NumOfDaysToChangePassword = 30,
                    PasswordExpiryTime = 24 * 60,
                    TimeToSessionTimeOut = 60 * 60,
                    UserPhotosize = 80000,
                    AttachmentsMaxSize = 80000,
                    TrialPeriodDays = 14,
                    ReminderDays = 3,
                };

                _appDbContext.Configurations.Add(defaultConfig);
            }
        }
        private static void SeedMenu()
        {
            var menus = _appDbContext.Menus.ToList();
            if (menus == null || menus.Count == 0)
            {
                #region Opian Users Sidebar
                _appDbContext.Menus.Add(new Menu
                {   //Id = 1
                    Path = "/dashboard/main",
                    Title = "Dashboard",
                    IconType = "material-icons-two-tone",
                    Icon = "home",
                    Class = "",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 5
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 2
                    Path = "",
                    Title = "User Management",
                    IconType = "material-icons-two-tone",
                    Icon = "people",
                    Class = "menu-toggle",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 5
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 3
                    Path = "/customer/user-roles",
                    Title = "User Roles",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 2,
                    Order = 5
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 4
                    Path = "/customer/customer-types",
                    Title = "Customer Types",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 2,
                    Order = 5
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 5
                    Path = "/user/users",
                    Title = "Opian Users",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 2,
                    Order = 5
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 6
                    Path = "",
                    Title = "Customer Managment",
                    IconType = "material-icons-two-tone",
                    Icon = "attach_money",
                    Class = "menu-toggle",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 6
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 7
                    Path = "/payment/membership-plans",
                    Title = "Membership Plans",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 6,
                    Order = 7
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 8
                    Path = "/payment/list",
                    Title = "Payment List",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 6,
                    Order = 8
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 9
                    Path = "/payment/upcoming",
                    Title = "Upcoming Payments",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 6,
                    Order = 9
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 10
                    Path = "/customer/customers",
                    Title = "Customer List",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 6,
                    Order = 10
                });
                _appDbContext.SaveChanges();


                _appDbContext.Menus.Add(new Menu
                {   //Id = 11
                    Path = "",
                    Title = "Settings",
                    IconType = "material-icons-two-tone",
                    Icon = "home",
                    Class = "menu-toggle",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 10
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 12
                    Path = "/metadata/countries",
                    Title = "Country",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 11
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 13
                    Path = "/metadata/regions",
                    Title = "Regions",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 12
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 14
                    Path = "/metadata/manufacturers",
                    Title = "Manufacturers",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 13
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 15
                    Path = "/metadata/health-facilities",
                    Title = "Health Facilities",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 14
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 16
                    Path = "/metadata/health-facility-types",
                    Title = "Health Facility Types",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 15
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 17
                    Path = "/metadata/equipment-categories",
                    Title = "Equipment Categories",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 16
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 18
                    Path = "/metadata/equipment-lookup",
                    Title = "Equipment Lookup",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 11,
                    Order = 17
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 19
                    Path = "",
                    Title = "Configuration",
                    IconType = "material-icons-two-tone",
                    Icon = "settings",
                    Class = "menu-toggle",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 18
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 20
                    Path = "/configuration/edit",
                    Title = "Settings",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 19,
                    Order = 19
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 21
                    Path = "/configuration/audits",
                    Title = "Configuration Audits",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 19,
                    Order = 20
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 22
                    Path = "/configuration/menus",
                    Title = "Sidebar Menu",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 19,
                    Order = 21
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 23
                    Path = "",
                    Title = "CMS",
                    IconType = "material-icons-two-tone",
                    Icon = "web",
                    Class = "menu-toggle",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 22
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 24
                    Path = "/CMS/articles",
                    Title = "Articles",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 23
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 25
                    Path = "/CMS/inquiry-questions",
                    Title = "Inquiry Questions",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 24
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 26
                    Path = "/CMS/frequently-asked-questions",
                    Title = "Frequently Asked Questions",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 25
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 27
                    Path = "/CMS/useful-resources",
                    Title = "Useful Resources",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 26
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 28
                    Path = "/CMS/channel-videos",
                    Title = "Channel Videos",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 27
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 29
                    Path = "/CMS/features",
                    Title = "Features",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 28
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 30
                    Path = "/CMS/contact-info",
                    Title = "Contact Info",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 23,
                    Order = 29
                });
                _appDbContext.SaveChanges();
                #endregion

                #region Admin Sidebar
                _appDbContext.Menus.Add(new Menu
                {   //Id = 31
                    Path = "/subscription/membership",
                    Title = "Subscription",
                    IconType = "material-icons-two-tone",
                    Icon = "description",
                    Class = "",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 30
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 32
                    Path = "/user/users",
                    Title = "User Management",
                    IconType = "material-icons-two-tone",
                    Icon = "people",
                    Class = "",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 31
                });
                _appDbContext.SaveChanges();
                #endregion

                #region Normal User Sidebar
                _appDbContext.Menus.Add(new Menu
                {   //Id = 33
                    Path = "/equipment/dynamic-equipments",
                    Title = "Equipment",
                    IconType = "material-icons-two-tone",
                    Icon = "people",
                    Class = "",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 2
                });
                _appDbContext.SaveChanges();
                _appDbContext.Menus.Add(new Menu
                {   //Id = 34
                    Path = "/maintenance/dynamic-workorder",
                    Title = "Work Order",
                    IconType = "material-icons-two-tone",
                    Icon = "people",
                    Class = "",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 1
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 35
                    Path = "",
                    Title = "Report",
                    IconType = "material-icons-two-tone",
                    Icon = "web",
                    Class = "menu-toggle",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 34
                });
                _appDbContext.SaveChanges();


                _appDbContext.Menus.Add(new Menu
                {   //Id = 36
                    Path = "/report/listofequipments",
                    Title = "Equipment by Functionality Status",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 35,
                    Order = 35
                });
                _appDbContext.SaveChanges();


                _appDbContext.Menus.Add(new Menu
                {   //Id = 37
                    Path = "/report/curativeMaintenance",
                    Title = "Equipment by Maintenance Type",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 35,
                    Order = 36
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 38
                    Path = "/report/vendorMaintenance",
                    Title = "Equipments by Manufacturer",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 35,
                    Order = 37
                });
                _appDbContext.SaveChanges();

                _appDbContext.Menus.Add(new Menu
                {   //Id = 39
                    Path = "/report/workOrderHeaders",
                    Title = "WorKOrder Turnaround Time",
                    IconType = "",
                    Icon = "",
                    Class = "ml-menu",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 35,
                    Order = 38
                });
                _appDbContext.SaveChanges();


                _appDbContext.Menus.Add(new Menu
                {   //Id = 40
                    Path = "/calendar/view",
                    Title = "Calendar",
                    IconType = "material-icons-two-tone",
                    Icon = "people",
                    Class = "",
                    GroupTitle = false,
                    Badge = "",
                    BadgeClass = "",
                    ParentMenuId = 0,
                    Order = 39
                });
                _appDbContext.SaveChanges();
                #endregion



                var menuRoles = _appDbContext.MenuRoles.ToList();
                if (menuRoles == null || menuRoles.Count == 0)
                {
                    var defaultMenuRoles = new List<MenuRole>()
                    {
                        new MenuRole { MenuId = 2, RoleId = (int)ApplicationRolesEnum.SuperAdmin},  // User Management

                        new MenuRole { MenuId = 6, RoleId = (int)ApplicationRolesEnum.Finance},     // Customer Managment

                        new MenuRole { MenuId = 11, RoleId = (int)ApplicationRolesEnum.SuperAdmin}, // Settings
                        new MenuRole { MenuId = 19, RoleId = (int)ApplicationRolesEnum.SuperAdmin}, // Configuration
                        new MenuRole { MenuId = 23, RoleId = (int)ApplicationRolesEnum.SuperAdmin}, // CMS

                        new MenuRole { MenuId = 31, RoleId = (int)ApplicationRolesEnum.Admin},      // Subscription
                        new MenuRole { MenuId = 32, RoleId = (int)ApplicationRolesEnum.Admin},      // User Management

                        new MenuRole { MenuId = 33, RoleId = (int)ApplicationRolesEnum.Facility},           // Equipments
                        new MenuRole { MenuId = 33, RoleId = (int)ApplicationRolesEnum.MaintenanceManager},

                        new MenuRole { MenuId = 34, RoleId = (int)ApplicationRolesEnum.MaintenanceManager}, // Workorder
                        //new MenuRole { MenuId = 34, RoleId = (int)ApplicationRolesEnum.Facility},
                        new MenuRole { MenuId = 34, RoleId = (int)ApplicationRolesEnum.Engineer},

                        new MenuRole { MenuId = 35, RoleId = (int)ApplicationRolesEnum.MaintenanceManager}, // Report
                        new MenuRole { MenuId = 35, RoleId = (int)ApplicationRolesEnum.Facility},
                        new MenuRole { MenuId = 35, RoleId = (int)ApplicationRolesEnum.Engineer},

                        new MenuRole { MenuId = 40, RoleId = (int)ApplicationRolesEnum.MaintenanceManager}, // Callendar
                        new MenuRole { MenuId = 40, RoleId = (int)ApplicationRolesEnum.Facility},
                        new MenuRole { MenuId = 40, RoleId = (int)ApplicationRolesEnum.Engineer},
                    };
                    _appDbContext.MenuRoles.AddRange(defaultMenuRoles);
                    _appDbContext.SaveChanges();
                }

            }
        }

        private static void SeedContactInfo()
        {
            var contactInfo = _appDbContext.ContactInfos.FirstOrDefault();
            if (contactInfo == null)
            {
                var defaultContactInfo = new DbModels.CMSSchema.ContactInfo()
                {
                    Email = "Zelalem@Opianhealth.com",
                    Phone = "+251911430408",
                    Address = "Cape Verdi Street, Across Rakan Mall, KT Building, 10th Floor",
                    CreatedBy = null,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };

                _appDbContext.ContactInfos.Add(defaultContactInfo);
            }
        }
        private static void SeedFeatures()
        {
            var features = _appDbContext.Features.ToList();
            if (features == null || features.Count == 0)
            {
                var defaultFeatures = new List<DbModels.CMSSchema.Feature>()
                {
                    new DbModels.CMSSchema.Feature
                    {
                        Title = "Planning and Preparation",
                        Description = " Focuses on data collection efforts to populate a standardized template",
                        LogoPath = "\\SeedingResources\\Features\\data-analytics.svg",
                        CreatedBy = null,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    },
                    new DbModels.CMSSchema.Feature
                    {
                        Title = "Reliability and Reduction of time",
                        Description = "Reduces forecasting analysis time to minutes and improves the reliability of the forecast",
                        LogoPath = "\\SeedingResources\\Features\\consult.svg",
                        CreatedBy = null,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    },
                    new DbModels.CMSSchema.Feature
                    {
                        Title = "Demand and Supply Planning",
                        Description = "Promotes development of evidence -based supply plans",
                        LogoPath = "\\SeedingResources\\Features\\data-analytics.svg",
                        CreatedBy = null,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    },
                    new DbModels.CMSSchema.Feature
                    {
                        Title = "Resource analysis",
                        Description = "Provides the opportunity to identify inefficiencies within a laboratory network",
                        LogoPath = "\\SeedingResources\\Features\\internet-things.svg",
                        CreatedBy = null,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    },
                    new DbModels.CMSSchema.Feature
                    {
                        Title = "Validates Consumption",
                        Description = "Consumption with service rates",
                        LogoPath = "\\SeedingResources\\Features\\machine-learning.svg",
                        CreatedBy = null,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    }
                };

                _appDbContext.Features.AddRange(defaultFeatures);
            }
        }

        private static void SeedApplicationRoles()
        {
            var items = _appDbContext.Roles.ToList();
            if (items == null || items.Count == 0)
            {
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    RoleType = 0,
                });
                _appDbContext.SaveChanges();
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "Finance",
                    NormalizedName = "FINANCE",
                    RoleType = 1,
                });
                _appDbContext.SaveChanges();
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    RoleType = 0,
                });
                _appDbContext.SaveChanges();
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "MaintenanceManager",
                    NormalizedName = "MAINTENANCEMANAGER",
                    RoleType = 2,
                });
                _appDbContext.SaveChanges();
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "Facility",
                    NormalizedName = "FACILITY",
                    RoleType = 2,
                });
                _appDbContext.SaveChanges();
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "Engineer",
                    NormalizedName = "ENGINEER",
                    RoleType = 2,
                });
                _appDbContext.SaveChanges();
                _appDbContext.Roles.Add(new ApplicationRole
                {
                    Name = "Encoders",
                    NormalizedName = "ENCODERS",
                    RoleType = 2,
                });
                _appDbContext.SaveChanges();
            }
        }
        private static void SeedApplicationSuperAdmin()
        {
            var superAdmin = _userManager.FindByNameAsync("admin@gmail.com");
            if (superAdmin.Result == null)
            {
                var applicationUser = new ApplicationUser()
                {
                    EmailConfirmed = true,
                    Status = UserStatusEnum.Active.ToString(),
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    LockoutEnabled = false,
                    CreatedBy = null,
                    CreatedOn = DateTime.Now,
                    NextPasswordExpiryDate = DateTime.Now.AddDays(30)
                };
                try
                {
                    var result = _userManager.CreateAsync(applicationUser, "Admin@2020");
                    if (result.Result.Succeeded)
                    {
                        superAdmin = _userManager.FindByEmailAsync("admin@gmail.com");
                        _appDbContext.UserRoles.Add(new ApplicationUserRole { RoleId = (int)ApplicationRolesEnum.SuperAdmin, UserId = superAdmin.Result.Id });
                    }

                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }

            var financeUser = _userManager.FindByNameAsync("finance@gmail.com");
            if (financeUser.Result == null)
            {
                var applicationUser = new ApplicationUser()
                {
                    EmailConfirmed = true,
                    Status = UserStatusEnum.Active.ToString(),
                    FirstName = "Finance",
                    LastName = "User",
                    UserName = "finance@gmail.com",
                    Email = "finance@gmail.com",
                    LockoutEnabled = false,
                    CreatedBy = null,
                    CreatedOn = DateTime.Now,
                    NextPasswordExpiryDate = DateTime.Now.AddDays(30)
                };
                try
                {
                    var result = _userManager.CreateAsync(applicationUser, "Finance@2020");
                    if (result.Result.Succeeded)
                    {
                        financeUser = _userManager.FindByEmailAsync("finance@gmail.com");
                        _appDbContext.UserRoles.Add(new ApplicationUserRole { RoleId = (int)ApplicationRolesEnum.Finance, UserId = financeUser.Result.Id });
                    }

                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }

        }

        private static void SeedCustomerTypes()
        {
            var items = _appDbContext.CustomerTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(CustomerTypesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.CustomerTypes.Add(new CustomerType() { Name = names[i], CreatedOn = DateTime.Now });
                }
            }
        }
        private static void SeedPaymentTypes()
        {
            var items = _appDbContext.PaymentTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(PaymentTypesEnum));
                PaymentTypesEnum[] values = (PaymentTypesEnum[])Enum.GetValues(typeof(PaymentTypesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.PaymentTypes.Add(new PaymentType() { Id = (int)values[i], Name = names[i] });
                    _appDbContext.SaveChanges();
                }

            }
        }
        private static void SeedPlanPermissions()
        {
            var planItems = _appDbContext.Plans.ToList();
            if (planItems == null || planItems.Count == 0)
            {
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "Trial",
                    Price = 0,
                    PlanMonths = 0,
                    CreatedOn = DateTime.Now,
                    Description = "",
                    PlanTypeId = (int)PlansEnum.Trial,
                    Status = PaypalPlanStatusEnum.Active.ToString()
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "1 Month Basic",
                    Price = 9,
                    PlanMonths = 1,
                    Description = "Monthly Basic Plan",
                    PaypalPlanId = "P-1VG11070XF481370RL76CFCI",
                    PlanTypeId = (int)PlansEnum.Basic,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "6 Month Basic",
                    Price = 39,
                    PlanMonths = 6,
                    Description = "6 Monthly Basic Plan",
                    PaypalPlanId = "P-8P4440872T0578332L76CHAQ",
                    PlanTypeId = (int)PlansEnum.Basic,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "Annual Basic",
                    Price = 69,
                    PlanMonths = 12,
                    Description = "Annual Basic Plan",
                    PaypalPlanId = "P-9T767526RE845222TL76CHSQ",
                    PlanTypeId = (int)PlansEnum.Basic,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "1 Month Premium",
                    Price = 19,
                    PlanMonths = 1,
                    Description = "Monthly Premium Plan",
                    PaypalPlanId = "P-48D39319UE3835255L76CIQA",
                    PlanTypeId = (int)PlansEnum.Premium,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "6 Month Premium",
                    Price = 79,
                    PlanMonths = 6,
                    Description = "6 Monthly Premium Plan",
                    PaypalPlanId = "P-0JT78035PE732673NL76CI3A",
                    PlanTypeId = (int)PlansEnum.Premium,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "Annual Premium",
                    Price = 139,
                    PlanMonths = 12,
                    Description = "Annual Premium Plan",
                    PaypalPlanId = "P-6E059490A91770914L76CJCQ",
                    PlanTypeId = (int)PlansEnum.Premium,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "1 Month Business",
                    Price = 39,
                    PlanMonths = 1,
                    Description = "Monthly Business Plan",
                    PaypalPlanId = "P-44066753VU715500DL76CJQA",
                    PlanTypeId = (int)PlansEnum.Business,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "6 Months Business",
                    Price = 159,
                    PlanMonths = 6,
                    Description = "6 Monthly Business Plan",
                    PaypalPlanId = "P-9DR4692082036170CL76CKLI",
                    PlanTypeId = (int)PlansEnum.Business,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
                _appDbContext.Plans.Add(new Plan
                {
                    Name = "Annual Business",
                    Price = 279,
                    PlanMonths = 12,
                    Description = "Annual Business Plan",
                    PaypalPlanId = "P-9AY43146D6334505VL76CKTI",
                    PlanTypeId = (int)PlansEnum.Business,
                    Status = PaypalPlanStatusEnum.Active.ToString(),
                    CreatedOn = DateTime.Now
                });
                _appDbContext.SaveChanges();
            }

            var permissionItems = _appDbContext.Permissions.ToList();
            if (permissionItems == null || permissionItems.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(PermissionsEnum));
                PermissionsEnum[] values = (PermissionsEnum[])Enum.GetValues(typeof(PermissionsEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.Permissions.Add(new Permission() { Name = names[i], Id = (int)values[i], Description = "" });
                }
                _appDbContext.SaveChanges();
            }

            var planPermissionItems = _appDbContext.PlanPermissions.ToList();
            if (planPermissionItems == null || planPermissionItems.Count == 0)
            {
                var defaultPlanPermissions = new List<PlanPermission>()
                {
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.EquipementManagement },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.MaintenanceManagement },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.Reports },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.Analytics },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.ManageHealthFacilities },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.ViewJobs },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.BuildProfile },
                    new PlanPermission { PlanId = (int)PlansEnum.Trial, PermissionId = (int)PermissionsEnum.ApplyJobs },
                };
                _appDbContext.PlanPermissions.AddRange(defaultPlanPermissions);
                _appDbContext.SaveChanges();
            }


        }

        private static void SeedUserTransactionTypes()
        {
            var items = _appDbContext.UserTransactionTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(UserTransactionTypesEnum));
                UserTransactionTypesEnum[] values = (UserTransactionTypesEnum[])Enum.GetValues(typeof(UserTransactionTypesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.UserTransactionTypes.Add(new DbModels.MaintenanceSchema.UserTransactionType() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();

            }
        }

        #region  Metadata

        private static void SeedContinents()
        {
            var items = _appDbContext.Continents.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(ContinentEnum));
                ContinentEnum[] values = (ContinentEnum[])Enum.GetValues(typeof(ContinentEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.Continents.Add(new Continent() { Name = names[i], Id = (int)values[i] });
                }
            }
        }

        // Helper Functions
        private static int GetContinentId(string region, string subregion)
        {
            if (region == ContinentEnum.Asia.ToString())
            {
                return (int)ContinentEnum.Asia;
            }
            else if (region == ContinentEnum.Africa.ToString())
            {
                return (int)ContinentEnum.Africa;
            }
            else if (region == "Americas" && subregion == "Northern America")
            {
                return (int)ContinentEnum.NorthAmerica;
            }
            else if (region == "Americas" && subregion == "South America")
            {
                return (int)ContinentEnum.SouthAmerica;
            }
            else if (region == "Polar")
            {
                return (int)ContinentEnum.Antarctica;
            }
            else if (region == ContinentEnum.Europe.ToString())
            {
                return (int)ContinentEnum.Europe;
            }
            else if (region == "Oceania" && subregion.Trim().ToLower().Contains(ContinentEnum.Australia.ToString().ToLower()))
            {
                return (int)ContinentEnum.Australia;
            }
            return 0;
        }

        private static void SeedCountries()
        {
            List<Country> allcountry = _appDbContext.Countries.ToList();

            if (allcountry == null || allcountry.Count() == 0)
            {
                // Get all regions
                var regions = SeedRegionCountries();

                HttpClient http = new HttpClient();
                var data = http.GetAsync("https://restcountries.eu/rest/v2/all").Result.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<CountryInfoApi>>(data);
                foreach (var country in model)
                {
                    int continentId = GetContinentId(country.Region, country.Subregion);
                    if (continentId <= 0)
                    {
                        continue;
                    }
                    _appDbContext.Countries.Add(new Country()
                    {
                        ContinentId = continentId,
                        CountryPeriodId = (int)CountryPeriodEnum.Monthly,
                        IsDeleted = false,
                        IsActive = true,
                        CreatedOn = DateTime.Now,
                        Name = country.Name,
                        ShortCode = country.Alpha3Code,
                        ShortName = country.Alpha3Code,
                        Flag = country.Flag,
                        NativeName = country.NativeName,
                        Population = country.Population,
                        Latitude = country.Latlng.Count() > 0 ? country.Latlng[0].ToString() : null,
                        Longitude = country.Latlng.Count() > 0 ? country.Latlng[1].ToString() : null,
                        CurrencyCode = country.Currencies.Count() > 0 ? country.Currencies[0].Code : null,
                        CallingCode = country.CallingCodes.Count() > 0 && !string.IsNullOrEmpty(country.CallingCodes[0]) ? $"+{country.CallingCodes[0]}" : null,
                        Regions = regions.Where(x => x.Key == country.Name).SelectMany(x => x.Value).ToList().ConvertAll(x =>
                        {
                            return new Region
                            {
                                Name = x,
                                ShortName = x,
                                IsDeleted = false,
                                IsActive = true,
                                CreatedOn = DateTime.Now,
                            };
                        })
                    });
                }

            }

        }
        private static void SeedCountryPeriods()
        {
            var items = _appDbContext.CountryPeriods.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(CountryPeriodEnum));
                CountryPeriodEnum[] values = (CountryPeriodEnum[])Enum.GetValues(typeof(CountryPeriodEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.CountryPeriods.Add(new CountryPeriod() { Name = names[i], Id = (int)values[i] });
                    _appDbContext.SaveChanges();

                }
            }
        }
        private static Dictionary<string, List<string>> SeedRegionCountries()
        {
            HttpClient http = new HttpClient();
            var data = http.GetAsync("https://raw.githubusercontent.com/russ666/all-countries-and-cities-json/6ee538beca8914133259b401ba47a550313e8984/countries.json").Result.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);
            return model;
        }


        public static void SeedDocumentationTypes()
        {
            var items = _appDbContext.DocumentationTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(DocumentationTypesEnum));
                DocumentationTypesEnum[] values = (DocumentationTypesEnum[])Enum.GetValues(typeof(DocumentationTypesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.DocumentationTypes.Add(new DocumentationType() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();
            }
        }

        public static void SeedWidgets()
        {
            var items = _appDbContext.DocumentationTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(WidgetTagEnum));
                WidgetTagEnum[] values = (WidgetTagEnum[])Enum.GetValues(typeof(WidgetTagEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.Widgets.Add(new Widget() { Title = names[i], WidgetTag = (int)values[i] });
                }
                _appDbContext.SaveChanges();
            }
        }

        #endregion

        #region Equipments
        private static void SeedEquipmentScheduleType()
        {
            var items = _appDbContext.EquipmentScheduleTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(EquipmentScheduleTypeEnum));
                EquipmentScheduleTypeEnum[] values = (EquipmentScheduleTypeEnum[])Enum.GetValues(typeof(EquipmentScheduleTypeEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.EquipmentScheduleTypes.Add(new EquipmentScheduleType() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();

            }
 
        }
        private static void SeedEquipmentScheduleInverval()
        {
            var items = _appDbContext.EquipmentScheduleTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(ScheduleIntervalEnum));
                ScheduleIntervalEnum[] values = (ScheduleIntervalEnum[])Enum.GetValues(typeof(ScheduleIntervalEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.EquipmentScheduleIntervals.Add(new EquipmentScheduleInterval() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();

            }

        }
        private static void SeedMetrices()
        {
            var items = _appDbContext.Metrics.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(MetricesEnum));
                MetricesEnum[] values = (MetricesEnum[])Enum.GetValues(typeof(MetricesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.Metrics.Add(new Metrics() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();

            }

        }
        #endregion

        #region Maintenance
        private static void SeedPriority()
        {
            var items = _appDbContext.Priorities.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(PriorityEnum));
                PriorityEnum[] values = (PriorityEnum[])Enum.GetValues(typeof(PriorityEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.Priorities.Add(new Priority() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();

            }

        }
        private static void SeedWorkOrderStatusType()
        {
            var items = _appDbContext.WorkOrderStatusTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(WorkOrderStatusTypeEnum));
                WorkOrderStatusTypeEnum[] values = (WorkOrderStatusTypeEnum[])Enum.GetValues(typeof(WorkOrderStatusTypeEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.WorkOrderStatusTypes.Add(new WorkOrderStatusType() { Name = names[i], Id = (int)values[i] });
                    _appDbContext.SaveChanges();

                }
            }

        }
        private static void SeedWorkOrderType()
        {
            var items = _appDbContext.WorkOrderTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(WorkOrderTypeEnum));
                WorkOrderTypeEnum[] values = (WorkOrderTypeEnum[])Enum.GetValues(typeof(WorkOrderTypeEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.WorkOrderTypes.Add(new WorkOrderType() { Name = names[i], Id = (int)values[i] });
                }
                _appDbContext.SaveChanges();
            }
        }
        #endregion
    }
}
