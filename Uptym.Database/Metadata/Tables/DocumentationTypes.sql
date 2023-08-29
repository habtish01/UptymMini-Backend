CREATE TABLE [Metadata].[DocumentationTypes] (
    [Id]        INT            NOT NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_DocumentationTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

