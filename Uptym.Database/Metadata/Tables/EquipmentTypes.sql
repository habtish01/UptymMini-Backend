CREATE TABLE [Metadata].[EquipmentTypes] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NULL,
    [Description] NVARCHAR (MAX) NULL,
    [IsDeleted]   BIT            NULL,
    [IsActive]    BIT            NULL,
    [CreatedBy]   INT            NULL,
    [CreatedOn]   DATETIME2 (7)  NULL,
    [UpdatedBy]   INT            NULL,
    [UpdatedOn]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED ([Id] ASC)
);

