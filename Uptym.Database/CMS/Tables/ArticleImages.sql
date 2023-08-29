CREATE TABLE [CMS].[ArticleImages] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ArticleId]          INT            NOT NULL,
    [AttachmentUrl]      NVARCHAR (MAX) NULL,
    [AttachmentSize]     REAL           NULL,
    [AttachmentName]     NVARCHAR (MAX) NULL,
    [ExtensionFormat]    NVARCHAR (MAX) NULL,
    [IsDefault]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsExternalResource] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsDeleted]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]           BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]          INT            NULL,
    [CreatedOn]          DATETIME2 (7)  NOT NULL,
    [UpdatedBy]          INT            NULL,
    [UpdatedOn]          DATETIME2 (7)  NULL,
    CONSTRAINT [PK_ArticleImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleImages_Articles_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [CMS].[Articles] ([Id]),
    CONSTRAINT [FK_ArticleImages_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_ArticleImages_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

