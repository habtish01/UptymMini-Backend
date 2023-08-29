﻿CREATE TABLE [Subscription].[Customers] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [Email]                  NVARCHAR (450) NULL,
    [FirstName]              NVARCHAR (MAX) NULL,
    [LastName]               NVARCHAR (MAX) NULL,
    [Address]                NVARCHAR (MAX) NULL,
    [Location_IP]            NVARCHAR (MAX) NULL,
    [Location_City]          NVARCHAR (MAX) NULL,
    [Location_Region]        NVARCHAR (MAX) NULL,
    [Location_RegionCode]    NVARCHAR (MAX) NULL,
    [Location_CountryName]   NVARCHAR (MAX) NULL,
    [Location_CountryCode]   NVARCHAR (MAX) NULL,
    [Location_ContinentName] NVARCHAR (MAX) NULL,
    [Location_ContinentCode] NVARCHAR (MAX) NULL,
    [Location_Latitude]      REAL           NULL,
    [Location_Longitude]     REAL           NULL,
    [Location_ASN]           NVARCHAR (MAX) NULL,
    [Location_Flag]          NVARCHAR (MAX) NULL,
    [Location_Postal]        NVARCHAR (MAX) NULL,
    [Location_CallingCode]   NVARCHAR (MAX) NULL,
    [Location_RTL]           BIT            CONSTRAINT [DF__Customers__Locat__2704CA5F] DEFAULT (CONVERT([bit],(0))) NULL,
    [PhoneNumber]            NVARCHAR (MAX) NULL,
    [PersonalImagePath]      NVARCHAR (MAX) NULL,
    [WorkHistory]            NVARCHAR (MAX) NULL,
    [Organization]           NVARCHAR (MAX) NULL,
    [Status]                 NVARCHAR (MAX) NULL,
    [IsTrial]                BIT            CONSTRAINT [DF__Customers__IsTri__27F8EE98] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [ReminderDays]           INT            NOT NULL,
    [PlanId]                 INT            NOT NULL,
    [CustomerTypeId]         INT            NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF__Customers__IsDel__28ED12D1] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]               BIT            CONSTRAINT [DF__Customers__IsAct__29E1370A] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]              INT            NULL,
    [CreatedOn]              DATETIME2 (7)  NOT NULL,
    [UpdatedBy]              INT            NULL,
    [UpdatedOn]              DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customers_CustomerTypes_CustomerTypeId] FOREIGN KEY ([CustomerTypeId]) REFERENCES [Subscription].[CustomerTypes] ([Id]),
    CONSTRAINT [FK_Customers_Plans_PlanId] FOREIGN KEY ([PlanId]) REFERENCES [Subscription].[Plans] ([Id]),
    CONSTRAINT [FK_Customers_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Customers_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

