CREATE TABLE [dbo].[MaintenanceMeanTimeSummary] (
    [HealthFacilityId] INT NOT NULL,
    [OwnerId]          INT NOT NULL,
    [EquipmentId]      INT NOT NULL,
    [NumberOfFailure]  INT NOT NULL,
    [OfflineHours]     INT NOT NULL,
    [HoursSinceActive] INT NOT NULL,
    [OperationalHours] INT NOT NULL
);

