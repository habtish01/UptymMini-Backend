CREATE TABLE [Subscription].[Discounts] (
    [Id]             INT             NOT NULL,
    [PlanID]         INT             NOT NULL,
    [StartDate]      DATETIME2 (7)   NOT NULL,
    [EndDate]        DATETIME2 (7)   NOT NULL,
    [EventName]      NVARCHAR (MAX)  NULL,
    [Description]    NVARCHAR (MAX)  NULL,
    [DiscountAmount] DECIMAL (18, 4) DEFAULT ((0.0)) NOT NULL,
    [IsDeleted]      BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]           NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Discounts_Plans_PlanID] FOREIGN KEY ([PlanID]) REFERENCES [Subscription].[Plans] ([Id])
);

