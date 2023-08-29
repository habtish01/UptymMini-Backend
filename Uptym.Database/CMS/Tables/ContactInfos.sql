CREATE TABLE [CMS].[ContactInfos] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Phone]     NVARCHAR (MAX) NULL,
    [Email]     NVARCHAR (MAX) NULL,
    [Address]   NVARCHAR (MAX) NULL,
    [Latitude]  NVARCHAR (MAX) NULL,
    [Longitude] NVARCHAR (MAX) NULL,
    [Facebook]  NVARCHAR (MAX) NULL,
    [Twitter]   NVARCHAR (MAX) NULL,
    [LinkedIn]  NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]  BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy] INT            NULL,
    [CreatedOn] DATETIME2 (7)  NOT NULL,
    [UpdatedBy] INT            NULL,
    [UpdatedOn] DATETIME2 (7)  NULL,
    [AccName]   NVARCHAR (MAX) NULL,
    [AccNo]     NVARCHAR (MAX) NULL,
    [BankName]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ContactInfos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContactInfos_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_ContactInfos_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);

