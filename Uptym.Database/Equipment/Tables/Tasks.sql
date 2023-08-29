CREATE TABLE [Equipment].[Tasks] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [MetricsId]      INT            NOT NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [AutoScheduleId] INT            NULL,
    [IsDeleted]      BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]       BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]      INT            NULL,
    [CreatedOn]      DATETIME2 (7)  NOT NULL,
    [UpdatedBy]      INT            NULL,
    [UpdatedOn]      DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Task_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Task_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

