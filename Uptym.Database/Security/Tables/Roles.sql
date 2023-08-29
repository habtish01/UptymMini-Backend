CREATE TABLE [Security].[Roles] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]        BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [RoleType]         INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

