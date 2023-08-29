CREATE TABLE [dbo].[PreventiveCompliance] (
    [HealthFacilityId]     INT NOT NULL,
    [OwnerId]              INT NOT NULL,
    [EquipmentId]          INT NOT NULL,
    [NumberOfCompletedPMs] INT NOT NULL,
    [NumberOfScheduledPMs] INT NOT NULL
);

