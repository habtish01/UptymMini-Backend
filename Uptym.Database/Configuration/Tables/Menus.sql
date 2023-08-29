CREATE TABLE [Configuration].[Menus] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Path]         NVARCHAR (MAX) NULL,
    [Title]        NVARCHAR (MAX) NULL,
    [IconType]     NVARCHAR (MAX) NULL,
    [Icon]         NVARCHAR (MAX) NULL,
    [Class]        NVARCHAR (MAX) NULL,
    [GroupTitle]   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Badge]        NVARCHAR (MAX) NULL,
    [BadgeClass]   NVARCHAR (MAX) NULL,
    [ParentMenuId] INT            NOT NULL,
    [Order]        INT            NOT NULL,
    [IsDeleted]    BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

