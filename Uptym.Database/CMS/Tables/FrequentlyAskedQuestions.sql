CREATE TABLE [CMS].[FrequentlyAskedQuestions] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Question]  NVARCHAR (MAX) NULL,
    [Answer]    NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]  BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy] INT            NULL,
    [CreatedOn] DATETIME2 (7)  NOT NULL,
    [UpdatedBy] INT            NULL,
    [UpdatedOn] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_FrequentlyAskedQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FrequentlyAskedQuestions_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_FrequentlyAskedQuestions_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

