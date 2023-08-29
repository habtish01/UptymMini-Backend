CREATE TABLE [Metadata].[EquipmentLookups] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [EquipmentTypeId]     INT            NOT NULL,
    [EquipmentCategoryId] INT            NOT NULL,
    [ManufacturerId]      INT            NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF__Equipment__IsDel__69FBBC1F] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]            BIT            CONSTRAINT [DF__Equipment__IsAct__6AEFE058] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]           INT            NULL,
    [CreatedOn]           DATETIME2 (7)  NOT NULL,
    [UpdatedBy]           INT            NULL,
    [UpdatedOn]           DATETIME2 (7)  NULL,
    CONSTRAINT [PK_EquipmentLookups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentLookups_EquipmentCategories_EquipmentCategoryId] FOREIGN KEY ([EquipmentCategoryId]) REFERENCES [Metadata].[EquipmentCategories] ([Id]),
    CONSTRAINT [FK_EquipmentLookups_Manufacturer_ManufacturerId] FOREIGN KEY ([ManufacturerId]) REFERENCES [Metadata].[Manufacturer] ([Id]),
    CONSTRAINT [FK_EquipmentLookups_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_EquipmentLookups_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

