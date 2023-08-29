CREATE TABLE [Configuration].[ConfigurationAudits] (
    [Id]                            INT            IDENTITY (1, 1) NOT NULL,
    [ConfigurationId]               INT            NOT NULL,
    [NumOfDaysToChangePassword]     INT            NOT NULL,
    [AccountLoginAttempts]          INT            NOT NULL,
    [PasswordExpiryTime]            INT            NOT NULL,
    [UserPhotosize]                 FLOAT (53)     NOT NULL,
    [AttachmentsMaxSize]            FLOAT (53)     NOT NULL,
    [TimesCountBeforePasswordReuse] INT            NOT NULL,
    [TimeToSessionTimeOut]          INT            NOT NULL,
    [TrialPeriodDays]               INT            NOT NULL,
    [ReminderDays]                  INT            NOT NULL,
    [DateOfAction]                  DATETIME2 (7)  NOT NULL,
    [Action]                        NVARCHAR (MAX) NULL,
    [CreatedBy]                     INT            NOT NULL,
    CONSTRAINT [PK_ConfigurationAudits] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConfigurationAudits_Configurations_ConfigurationId] FOREIGN KEY ([ConfigurationId]) REFERENCES [Configuration].[Configurations] ([Id]),
    CONSTRAINT [FK_ConfigurationAudits_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id])
);

