CREATE TABLE [Maintenance].[AssignedEngineers] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [WorkOrderID]        INT           NOT NULL,
    [AssignedEngineerId] INT           NOT NULL,
    [IsDeleted]          BIT           CONSTRAINT [DF__AssignedE__IsDel__2E70E1FD] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]           BIT           CONSTRAINT [DF__AssignedE__IsAct__2F650636] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]          INT           NULL,
    [CreatedOn]          DATETIME2 (7) NOT NULL,
    [UpdatedBy]          INT           NULL,
    [UpdatedOn]          DATETIME2 (7) NULL,
    CONSTRAINT [PK_AssignedEngineers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssignedEngineers_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_AssignedEngineers_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_AssignedEngineers_WorkOrders_WorkOrderID] FOREIGN KEY ([WorkOrderID]) REFERENCES [Maintenance].[WorkOrders] ([Id])
);

