CREATE TABLE [dbo].[EquipmentSummary] (
    [NumberOfNotFunctionalEquipment] INT NOT NULL,
    [EquipmentUnderService]          INT DEFAULT ((0)) NOT NULL,
    [EquipmentUnderWarranty]         INT DEFAULT ((0)) NOT NULL,
    [EquipmentWithWarranty]          INT DEFAULT ((0)) NOT NULL,
    [NumberOfEquipment]              INT DEFAULT ((0)) NOT NULL
);

