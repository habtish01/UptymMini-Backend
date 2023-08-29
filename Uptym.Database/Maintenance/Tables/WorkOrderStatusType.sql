CREATE TABLE [Maintenance].[WorkOrderStatusType] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_WorkOrderStatusType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

