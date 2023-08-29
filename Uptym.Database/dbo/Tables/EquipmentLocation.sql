CREATE TABLE [dbo].[EquipmentLocation] (
    [HealthFacilityId] INT            NOT NULL,
    [OwnerId]          INT            NOT NULL,
    [UserId]           INT            NOT NULL,
    [Equipment]        NVARCHAR (100) NOT NULL,
    [Latitude]         NVARCHAR (100) NOT NULL,
    [Longitude]        NVARCHAR (100) NOT NULL
);

