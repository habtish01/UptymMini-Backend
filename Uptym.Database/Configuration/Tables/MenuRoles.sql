CREATE TABLE [Configuration].[MenuRoles] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [MenuId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_MenuRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuRoles_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Configuration].[Menus] ([Id]),
    CONSTRAINT [FK_MenuRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[Roles] ([Id])
);

