CREATE TABLE [Metadata].[CountryPeriods] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CountryPeriods] PRIMARY KEY CLUSTERED ([Id] ASC)
);

