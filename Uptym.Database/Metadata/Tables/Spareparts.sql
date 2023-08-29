CREATE TABLE [Metadata].[Spareparts] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [EquipmentLookupId] INT            NOT NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [IsDeleted]         BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]         INT            NULL,
    [CreatedOn]         DATETIME2 (7)  NOT NULL,
    [UpdatedBy]         INT            NULL,
    [UpdatedOn]         DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Spareparts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Spareparts_EquipmentLookups_EquipmentLookupId] FOREIGN KEY ([EquipmentLookupId]) REFERENCES [Metadata].[EquipmentLookups] ([Id]),
    CONSTRAINT [FK_Spareparts_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Spareparts_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

