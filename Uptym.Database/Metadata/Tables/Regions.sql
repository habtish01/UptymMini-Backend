CREATE TABLE [Metadata].[Regions] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [CountryId] INT            NOT NULL,
    [ShortName] NVARCHAR (MAX) NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]  BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy] INT            NULL,
    [CreatedOn] DATETIME2 (7)  NOT NULL,
    [UpdatedBy] INT            NULL,
    [UpdatedOn] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Regions_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Metadata].[Countries] ([Id]),
    CONSTRAINT [FK_Regions_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Regions_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

