CREATE TABLE [Metadata].[Continents] (
    [Id]        INT            NOT NULL,
    [ShortCode] NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Continents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

