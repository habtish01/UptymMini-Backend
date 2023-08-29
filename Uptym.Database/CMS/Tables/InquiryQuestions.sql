CREATE TABLE [CMS].[InquiryQuestions] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [Email]         NVARCHAR (MAX) NULL,
    [Message]       NVARCHAR (MAX) NULL,
    [ReplyProvided] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsDeleted]     BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]      BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]     INT            NULL,
    [CreatedOn]     DATETIME2 (7)  NOT NULL,
    [UpdatedBy]     INT            NULL,
    [UpdatedOn]     DATETIME2 (7)  NULL,
    CONSTRAINT [PK_InquiryQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InquiryQuestions_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_InquiryQuestions_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

