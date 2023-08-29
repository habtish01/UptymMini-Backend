CREATE TABLE [CMS].[InquiryQuestionReplies] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [InquiryQuestionId] INT            NOT NULL,
    [Message]           NVARCHAR (MAX) NULL,
    [IsDeleted]         BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]         INT            NULL,
    [CreatedOn]         DATETIME2 (7)  NOT NULL,
    [UpdatedBy]         INT            NULL,
    [UpdatedOn]         DATETIME2 (7)  NULL,
    CONSTRAINT [PK_InquiryQuestionReplies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InquiryQuestionReplies_InquiryQuestions_InquiryQuestionId] FOREIGN KEY ([InquiryQuestionId]) REFERENCES [CMS].[InquiryQuestions] ([Id]),
    CONSTRAINT [FK_InquiryQuestionReplies_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_InquiryQuestionReplies_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

