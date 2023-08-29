CREATE TABLE [Security].[UserRoles] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [RoleId1] INT NULL,
    [UserId]  INT NOT NULL,
    [RoleId]  INT NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[Roles] ([Id]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId1] FOREIGN KEY ([RoleId1]) REFERENCES [Security].[Roles] ([Id]),
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id])
);

