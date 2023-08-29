CREATE TABLE [Metadata].[TaskList] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Title]             NVARCHAR (MAX) NULL,
    [EquipmentLookupId] INT            NOT NULL,
    [WorkOrderTypeId]   INT            NOT NULL,
    [TaskListTypeId]    INT            NULL,
    [IsDeleted]         BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]         INT            NULL,
    [CreatedOn]         DATETIME2 (7)  NOT NULL,
    [UpdatedBy]         INT            NULL,
    [UpdatedOn]         DATETIME2 (7)  NULL,
    CONSTRAINT [PK_TaskList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaskList_EquipmentLookups_EquipmentLookupId] FOREIGN KEY ([EquipmentLookupId]) REFERENCES [Metadata].[EquipmentLookups] ([Id]),
    CONSTRAINT [FK_TaskList_TaskListType_TaskListTypeId] FOREIGN KEY ([TaskListTypeId]) REFERENCES [Metadata].[TaskListType] ([Id]),
    CONSTRAINT [FK_TaskList_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_TaskList_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_TaskList_WorkOrderType_WorkOrderTypeId] FOREIGN KEY ([WorkOrderTypeId]) REFERENCES [Maintenance].[WorkOrderType] ([Id])
);

