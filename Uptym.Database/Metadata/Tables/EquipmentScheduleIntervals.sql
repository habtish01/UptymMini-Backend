CREATE TABLE [Metadata].[EquipmentScheduleIntervals] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_EquipmentScheduleIntervals] PRIMARY KEY CLUSTERED ([Id] ASC)
);

