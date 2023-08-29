CREATE TABLE [Equipment].[EquipmentOperators] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [EquipmentId] INT            NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [PhoneNumber] NVARCHAR (MAX) NULL,
    [IsDeleted]   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]    BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]   INT            NULL,
    [CreatedOn]   DATETIME2 (7)  NOT NULL,
    [UpdatedBy]   INT            NULL,
    [UpdatedOn]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_EquipmentOperators] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentOperators_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment].[Equipments] ([Id]),
    CONSTRAINT [FK_EquipmentOperators_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_EquipmentOperators_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

