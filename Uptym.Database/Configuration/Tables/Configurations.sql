CREATE TABLE [Configuration].[Configurations] (
    [Id]                            INT        IDENTITY (1, 1) NOT NULL,
    [NumOfDaysToChangePassword]     INT        NOT NULL,
    [AccountLoginAttempts]          INT        NOT NULL,
    [PasswordExpiryTime]            INT        NOT NULL,
    [UserPhotosize]                 FLOAT (53) NOT NULL,
    [AttachmentsMaxSize]            FLOAT (53) NOT NULL,
    [TimesCountBeforePasswordReuse] INT        NOT NULL,
    [TimeToSessionTimeOut]          INT        NOT NULL,
    [TrialPeriodDays]               INT        NOT NULL,
    [ReminderDays]                  INT        NOT NULL,
    CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

