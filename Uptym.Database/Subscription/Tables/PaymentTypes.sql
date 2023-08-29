CREATE TABLE [Subscription].[PaymentTypes] (
    [Id]        INT            NOT NULL,
    [Address]   NVARCHAR (MAX) NULL,
    [SK]        NVARCHAR (MAX) NULL,
    [PK]        NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

