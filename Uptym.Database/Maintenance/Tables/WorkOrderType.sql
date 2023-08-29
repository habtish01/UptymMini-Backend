CREATE TABLE [Maintenance].[WorkOrderType] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_WorkOrderType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

