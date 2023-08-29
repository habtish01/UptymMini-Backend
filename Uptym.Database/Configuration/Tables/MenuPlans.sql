CREATE TABLE [Configuration].[MenuPlans] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [MenuId] INT NOT NULL,
    [PlanId] INT NOT NULL,
    CONSTRAINT [PK_MenuPlans] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuPlans_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Configuration].[Menus] ([Id]),
    CONSTRAINT [FK_MenuPlans_Plans_PlanId] FOREIGN KEY ([PlanId]) REFERENCES [Subscription].[Plans] ([Id])
);

