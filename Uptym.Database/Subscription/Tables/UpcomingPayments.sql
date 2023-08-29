CREATE TABLE [Subscription].[UpcomingPayments] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [CustomerId]          INT             NOT NULL,
    [Amount]              DECIMAL (18, 4) DEFAULT ((0.0)) NOT NULL,
    [TargetDate]          DATETIME2 (7)   NOT NULL,
    [Details]             NVARCHAR (MAX)  NULL,
    [EmailReminderStatus] NVARCHAR (MAX)  NULL,
    [PhoneReminderStatus] NVARCHAR (MAX)  NULL,
    [Status]              NVARCHAR (MAX)  NULL,
    [IsDeleted]           BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]            BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]           INT             NULL,
    [CreatedOn]           DATETIME2 (7)   NOT NULL,
    [UpdatedBy]           INT             NULL,
    [UpdatedOn]           DATETIME2 (7)   NULL,
    CONSTRAINT [PK_UpcomingPayments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UpcomingPayments_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Subscription].[Customers] ([Id]),
    CONSTRAINT [FK_UpcomingPayments_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_UpcomingPayments_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

