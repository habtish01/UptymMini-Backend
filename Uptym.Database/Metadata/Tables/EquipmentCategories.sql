CREATE TABLE [Metadata].[EquipmentCategories] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [EquipmentTypeId] INT            NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Name]            NVARCHAR (MAX) NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF__Equipment__IsDel__662B2B3B] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF__Equipment__IsAct__671F4F74] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]       INT            NULL,
    [CreatedOn]       DATETIME2 (7)  NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedOn]       DATETIME2 (7)  NULL,
    CONSTRAINT [PK_EquipmentCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypes_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_EquipmentTypes_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

