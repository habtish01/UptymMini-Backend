CREATE TABLE [Maintenance].[WorkOrderMaintenanceCosts] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Cost]            DECIMAL (18, 2) NULL,
    [MaintenanceType] NVARCHAR (MAX)  NULL,
    [WorkOrderId]     INT             NOT NULL,
    [Description]     NVARCHAR (MAX)  NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF__WorkOrder__IsDel__056PCC6A] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]        BIT             CONSTRAINT [DF__WorkOrder__IsAct__0662FPA3] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]       INT             NULL,
    [CreatedOn]       DATETIME2 (7)   NOT NULL,
    [UpdatedBy]       INT             NULL,
    [UpdatedOn]       DATETIME2 (7)   NULL,
    CONSTRAINT [PK_WorkOrderMaintenanceCosts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderMaintenanceCosts_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_WorkOrderMaintenanceCosts_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_WorkOrderMaintenanceCosts_WorkOrders_WorkOrderId] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrders] ([Id])
);

