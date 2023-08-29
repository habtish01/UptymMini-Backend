CREATE TABLE [Subscription].[PlanPermissions] (
    [PlanId]       INT NOT NULL,
    [PermissionId] INT NOT NULL,
    [LimitDays]    INT NULL,
    [LimitJobs]    INT NULL,
    CONSTRAINT [PK_PlanPermissions] PRIMARY KEY CLUSTERED ([PlanId] ASC, [PermissionId] ASC),
    CONSTRAINT [FK_PlanPermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Subscription].[Permissions] ([Id]),
    CONSTRAINT [FK_PlanPermissions_Plans_PlanId] FOREIGN KEY ([PlanId]) REFERENCES [Subscription].[Plans] ([Id])
);

