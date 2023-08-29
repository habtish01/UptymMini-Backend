CREATE TABLE [Maintenance].[MaintenanceActions] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [MaintenanceActionDate] DATETIME2 (7)  NOT NULL,
    [StartTime]             TIME (7)       NOT NULL,
    [EndTime]               TIME (7)       NOT NULL,
    [Title]                 NVARCHAR (MAX) NULL,
    [Note]                  NVARCHAR (MAX) NULL,
    [EngineerId]            INT            NOT NULL,
    [WorkOrderId]           INT            NOT NULL,
    [IsDeleted]             BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]              BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]             INT            NULL,
    [CreatedOn]             DATETIME2 (7)  NOT NULL,
    [UpdatedBy]             INT            NULL,
    [UpdatedOn]             DATETIME2 (7)  NULL,
    CONSTRAINT [PK_MaintenanceActions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceActions_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_MaintenanceActions_Users_EngineerId] FOREIGN KEY ([EngineerId]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_MaintenanceActions_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_MaintenanceActions_WorkOrders_WorkOrderId] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrders] ([Id])
);

