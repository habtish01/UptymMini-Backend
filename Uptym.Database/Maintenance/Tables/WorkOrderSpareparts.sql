CREATE TABLE [Maintenance].[WorkOrderSpareparts] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [quantity]    INT             NOT NULL,
    [Cost]        DECIMAL (18, 2) NULL,
    [WorkOrderId] INT             NOT NULL,
    [IsDeleted]   BIT             CONSTRAINT [DF__WorkOrder__IsDel__056ECC6A] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]    BIT             CONSTRAINT [DF__WorkOrder__IsAct__0662F0A3] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]   INT             NULL,
    [CreatedOn]   DATETIME2 (7)   NOT NULL,
    [UpdatedBy]   INT             NULL,
    [UpdatedOn]   DATETIME2 (7)   NULL,
    [SparePartId] INT             NOT NULL,
    CONSTRAINT [PK_WorkOrderSpareparts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

