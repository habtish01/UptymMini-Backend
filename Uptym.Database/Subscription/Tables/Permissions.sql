CREATE TABLE [Subscription].[Permissions] (
    [Id]          INT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [IsDeleted]   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

