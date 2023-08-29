CREATE TABLE [Metadata].[TaskListDetails] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Task]       NVARCHAR (MAX) NULL,
    [TaskListId] INT            NOT NULL,
    [IsDeleted]  BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]  INT            NULL,
    [CreatedOn]  DATETIME2 (7)  NOT NULL,
    [UpdatedBy]  INT            NULL,
    [UpdatedOn]  DATETIME2 (7)  NULL,
    CONSTRAINT [PK_TaskListDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaskListDetails_TaskList_TaskListId] FOREIGN KEY ([TaskListId]) REFERENCES [Metadata].[TaskList] ([Id]),
    CONSTRAINT [FK_TaskListDetails_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_TaskListDetails_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

