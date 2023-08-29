CREATE TABLE [Maintenance].[WorkOrderTaskLists] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [MetricsId]   INT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [TaskId]      INT            NOT NULL,
    [WorkOrderId] INT            NOT NULL,
    [IsDeleted]   BIT            CONSTRAINT [DF_WorkOrderTaskLists_IsDeleted] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]    BIT            CONSTRAINT [DF_WorkOrderTaskLists_IsActive] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]   INT            NULL,
    [CreatedOn]   DATETIME2 (7)  NOT NULL,
    [UpdatedBy]   INT            NULL,
    [UpdatedOn]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_WorkOrderTaskLists] PRIMARY KEY CLUSTERED ([Id] ASC)
);



