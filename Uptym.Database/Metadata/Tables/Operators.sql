CREATE TABLE [Metadata].[Operators] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [EquipmentLookupId] INT            NOT NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [PhoneNumber]       NVARCHAR (MAX) NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF__Operators__IsDel__116A8EFB] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]          BIT            CONSTRAINT [DF__Operators__IsAct__125EB334] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]         INT            NULL,
    [CreatedOn]         DATETIME2 (7)  NOT NULL,
    [UpdatedBy]         INT            NULL,
    [UpdatedOn]         DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Operators_EquipmentLookups_EquipmentLookupId] FOREIGN KEY ([EquipmentLookupId]) REFERENCES [Metadata].[EquipmentLookups] ([Id]),
    CONSTRAINT [FK_Operators_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Operators_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

