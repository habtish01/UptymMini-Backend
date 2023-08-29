CREATE TABLE [Equipment].[AutoScheduleAssignedEngineers] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [AutoScheduleId]     INT           NOT NULL,
    [AssignedEngineerId] INT           NOT NULL,
    [IsDeleted]          BIT           CONSTRAINT [DF_AutoScheduleAssignedEngineers_IsDeleted] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]           BIT           CONSTRAINT [DF_AutoScheduleAssignedEngineers_IsActive] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]          INT           NULL,
    [CreatedOn]          DATETIME2 (7) NOT NULL,
    [UpdatedBy]          INT           NULL,
    [UpdatedOn]          DATETIME2 (7) NULL,
    CONSTRAINT [PK_AutoScheduleAssignedEngineers] PRIMARY KEY CLUSTERED ([Id] ASC)
);



