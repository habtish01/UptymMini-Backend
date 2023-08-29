CREATE TABLE [Maintenance].[WorkOrderStatus] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Title]                 NVARCHAR (MAX) NULL,
    [StatusDate]            DATETIME2 (7)  NOT NULL,
    [Remark]                NVARCHAR (MAX) NULL,
    [WorkOrderId]           INT            NOT NULL,
    [WorkOrderStatusTypeId] INT            NOT NULL,
    [EquipmentStatusId]     INT            NULL,
    [IsDeleted]             BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]              BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]             INT            NULL,
    [CreatedOn]             DATETIME2 (7)  NOT NULL,
    [UpdatedBy]             INT            NULL,
    [UpdatedOn]             DATETIME2 (7)  NULL,
    CONSTRAINT [PK_WorkOrderStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderStatus_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_WorkOrderStatus_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_WorkOrderStatus_WorkOrders_WorkOrderId] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrders] ([Id]),
    CONSTRAINT [FK_WorkOrderStatus_WorkOrderStatusType_WorkOrderStatusTypeId] FOREIGN KEY ([WorkOrderStatusTypeId]) REFERENCES [Maintenance].[WorkOrderStatusType] ([Id])
);

