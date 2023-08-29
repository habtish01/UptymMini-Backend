CREATE TABLE [CMS].[Articles] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (MAX) NULL,
    [Content]      NVARCHAR (MAX) NULL,
    [ProvidedBy]   NVARCHAR (MAX) NULL,
    [ProvidedDate] DATETIME2 (7)  NOT NULL,
    [IsDeleted]    BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]     BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]    INT            NULL,
    [CreatedOn]    DATETIME2 (7)  NOT NULL,
    [UpdatedBy]    INT            NULL,
    [UpdatedOn]    DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Articles_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_Articles_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

