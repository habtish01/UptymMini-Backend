CREATE TABLE [Metadata].[ProblemTypes] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [EquipmentLookupId] INT            NOT NULL,
    [TaskListTypeId]    INT            NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [IsDeleted]         BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]         INT            NULL,
    [CreatedOn]         DATETIME2 (7)  NOT NULL,
    [UpdatedBy]         INT            NULL,
    [UpdatedOn]         DATETIME2 (7)  NULL,
    CONSTRAINT [PK_ProblemTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProblemTypes_EquipmentLookups_EquipmentLookupId] FOREIGN KEY ([EquipmentLookupId]) REFERENCES [Metadata].[EquipmentLookups] ([Id]),
    CONSTRAINT [FK_ProblemTypes_TaskListType_TaskListTypeId] FOREIGN KEY ([TaskListTypeId]) REFERENCES [Metadata].[TaskListType] ([Id]),
    CONSTRAINT [FK_ProblemTypes_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_ProblemTypes_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

