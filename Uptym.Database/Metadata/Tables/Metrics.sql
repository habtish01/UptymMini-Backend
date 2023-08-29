CREATE TABLE [Metadata].[Metrics] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Metrics] PRIMARY KEY CLUSTERED ([Id] ASC)
);

