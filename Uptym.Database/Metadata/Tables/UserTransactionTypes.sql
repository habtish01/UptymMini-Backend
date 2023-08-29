CREATE TABLE [Metadata].[UserTransactionTypes] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserTransactionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

