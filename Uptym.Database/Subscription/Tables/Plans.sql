CREATE TABLE [Subscription].[Plans] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Description]  NVARCHAR (MAX)  NULL,
    [Price]        DECIMAL (18, 4) DEFAULT ((0.0)) NOT NULL,
    [PlanMonths]   INT             NOT NULL,
    [ExtraDays]    INT             NOT NULL,
    [PaypalPlanId] NVARCHAR (MAX)  NULL,
    [Status]       NVARCHAR (MAX)  NULL,
    [Name]         NVARCHAR (MAX)  NULL,
    [IsDeleted]    BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]     BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]    INT             NULL,
    [CreatedOn]    DATETIME2 (7)   NOT NULL,
    [UpdatedBy]    INT             NULL,
    [UpdatedOn]    DATETIME2 (7)   NULL,
    [PlanTypeId]   INT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Plans] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Plans_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Plans_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

