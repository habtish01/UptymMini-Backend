CREATE TABLE [Configuration].[UserPreferences] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [PreferenceType]  NVARCHAR (MAX) NULL,
    [PreferenceKey]   NVARCHAR (MAX) NULL,
    [PreferenceValue] NVARCHAR (MAX) NULL,
    [WidgetId]        INT            NOT NULL,
    [UserId]          INT            NOT NULL,
    CONSTRAINT [PK_UserPreferences] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserPreferences_Users] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_UserPreferences_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_UserPreferences_Widgets] FOREIGN KEY ([WidgetId]) REFERENCES [Metadata].[Widgets] ([Id]),
    CONSTRAINT [FK_UserPreferences_Widgets_WidgetId] FOREIGN KEY ([WidgetId]) REFERENCES [Metadata].[Widgets] ([Id])
);

