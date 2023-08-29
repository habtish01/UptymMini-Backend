CREATE TABLE [dbo].[InspectionCompliance] (
    [HealthFacilityId]     INT NOT NULL,
    [OwnerId]              INT NOT NULL,
    [EquipmentId]          INT NOT NULL,
    [NumberOfCompletedIMs] INT NOT NULL,
    [NumberOfScheduledIMs] INT NOT NULL
);

