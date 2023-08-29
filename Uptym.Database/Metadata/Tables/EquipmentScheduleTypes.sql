CREATE TABLE [Metadata].[EquipmentScheduleTypes] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_EquipmentScheduleTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

