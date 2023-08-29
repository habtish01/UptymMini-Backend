CREATE TABLE [Metadata].[Widgets] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (MAX) NULL,
    [Path]        NVARCHAR (MAX) NULL,
    [ImagePath]   NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [WidgetTag]   INT            NOT NULL,
    CONSTRAINT [PK_Widgets] PRIMARY KEY CLUSTERED ([Id] ASC)
);

