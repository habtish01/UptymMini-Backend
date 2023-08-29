CREATE TABLE [Equipment].[EquipmentContracts] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NULL,
    [StartDate]          DATETIME2 (7)  NOT NULL,
    [EndDate]            DATETIME2 (7)  NOT NULL,
    [ContractProviderId] INT            NULL,
    [EquipmentId]        INT            NOT NULL,
    [DocumentPath]       NVARCHAR (MAX) NULL,
    [ContractType]       INT            NOT NULL,
    [IsDeleted]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]           BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]          INT            NULL,
    [CreatedOn]          DATETIME2 (7)  NOT NULL,
    [UpdatedBy]          INT            NULL,
    [UpdatedOn]          DATETIME2 (7)  NULL,
    [OfflineContractor]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_EquipmentContracts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentContracts_Customers_ContractProviderId] FOREIGN KEY ([ContractProviderId]) REFERENCES [Subscription].[Customers] ([Id]),
    CONSTRAINT [FK_EquipmentContracts_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment].[Equipments] ([Id]),
    CONSTRAINT [FK_EquipmentContracts_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_EquipmentContracts_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

