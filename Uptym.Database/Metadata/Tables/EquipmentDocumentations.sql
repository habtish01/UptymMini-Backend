CREATE TABLE [Metadata].[EquipmentDocumentations] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [DocumentPath]        NVARCHAR (MAX) NULL,
    [DocumentationTypeId] INT            NOT NULL,
    [EquipmentLookupId]   INT            NOT NULL,
    [IsDeleted]           BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]            BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]           INT            NULL,
    [CreatedOn]           DATETIME2 (7)  NOT NULL,
    [UpdatedBy]           INT            NULL,
    [UpdatedOn]           DATETIME2 (7)  NULL,
    CONSTRAINT [PK_EquipmentDocumentations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentDocumentations_DocumentationTypes_DocumentationTypeId] FOREIGN KEY ([DocumentationTypeId]) REFERENCES [Metadata].[DocumentationTypes] ([Id]),
    CONSTRAINT [FK_EquipmentDocumentations_EquipmentLookups_EquipmentLookupId] FOREIGN KEY ([EquipmentLookupId]) REFERENCES [Metadata].[EquipmentLookups] ([Id]),
    CONSTRAINT [FK_EquipmentDocumentations_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_EquipmentDocumentations_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

