CREATE TABLE [Equipment].[Timesheets] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [EquipmentId]  INT            NOT NULL,
    [OperatorName] NVARCHAR (MAX) NULL,
    [LocationName] NVARCHAR (MAX) NULL,
    [StartTime]    TIME (7)       NOT NULL,
    [EndTime]      TIME (7)       NOT NULL,
    [Date]         DATETIME2 (7)  NOT NULL,
    [IsDeleted]    BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]     BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]    INT            NULL,
    [CreatedOn]    DATETIME2 (7)  NOT NULL,
    [UpdatedBy]    INT            NULL,
    [UpdatedOn]    DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Timesheets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Timesheets_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment].[Equipments] ([Id]),
    CONSTRAINT [FK_Timesheets_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

