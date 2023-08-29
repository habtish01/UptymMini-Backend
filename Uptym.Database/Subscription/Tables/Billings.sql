﻿CREATE TABLE [Subscription].[Billings] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [UniqueId]           NVARCHAR (MAX)  NULL,
    [CustomerId]         INT             NOT NULL,
    [PaymentTypeId]      INT             NOT NULL,
    [CustomerCardNumber] NVARCHAR (MAX)  NULL,
    [PaymentIntentId]    NVARCHAR (MAX)  NULL,
    [Status]             NVARCHAR (MAX)  NULL,
    [Details]            NVARCHAR (MAX)  NULL,
    [BillingDate]        DATETIME2 (7)   NOT NULL,
    [Amount]             DECIMAL (18, 4) DEFAULT ((0.0)) NOT NULL,
    [CardLast]           NVARCHAR (MAX)  NULL,
    [LocationIP]         NVARCHAR (MAX)  NULL,
    [DiscountID]         INT             NULL,
    [IsDeleted]          BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]           BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]          INT             NULL,
    [CreatedOn]          DATETIME2 (7)   NOT NULL,
    [UpdatedBy]          INT             NULL,
    [UpdatedOn]          DATETIME2 (7)   NULL,
    CONSTRAINT [PK_Billings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Billings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Subscription].[Customers] ([Id]),
    CONSTRAINT [FK_Billings_Discounts_DiscountID] FOREIGN KEY ([DiscountID]) REFERENCES [Subscription].[Discounts] ([Id]),
    CONSTRAINT [FK_Billings_PaymentTypes_PaymentTypeId] FOREIGN KEY ([PaymentTypeId]) REFERENCES [Subscription].[PaymentTypes] ([Id]),
    CONSTRAINT [FK_Billings_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Billings_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

