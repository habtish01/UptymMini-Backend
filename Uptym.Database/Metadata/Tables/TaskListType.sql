CREATE TABLE [Metadata].[TaskListType] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TaskListType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

