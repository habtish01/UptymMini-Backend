CREATE TABLE [CMS].[UsefulResources] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Title]              NVARCHAR (MAX) NULL,
    [AttachmentUrl]      NVARCHAR (MAX) NULL,
    [AttachmentSize]     REAL           NULL,
    [AttachmentName]     NVARCHAR (MAX) NULL,
    [ExtensionFormat]    NVARCHAR (MAX) NULL,
    [IsExternalResource] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [DownloadCount]      INT            NOT NULL,
    [IsDeleted]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]           BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]          INT            NULL,
    [CreatedOn]          DATETIME2 (7)  NOT NULL,
    [UpdatedBy]          INT            NULL,
    [UpdatedOn]          DATETIME2 (7)  NULL,
    CONSTRAINT [PK_UsefulResources] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsefulResources_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_UsefulResources_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

