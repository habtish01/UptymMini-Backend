﻿CREATE TABLE [Equipment].[AutoSchedules] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (MAX) NULL,
    [Description]             NVARCHAR (MAX) NULL,
    [Duration]                INT            NOT NULL,
    [EquipmentId]             INT            NOT NULL,
    [AutoSchedulePerformerId] INT            NULL,
    [IntervalId]              INT            NOT NULL,
    [AssignedToId]            INT            NOT NULL,
    [EquipmentScheduleTypeId] INT            NOT NULL,
    [AssignedTo]              INT            NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF__AutoSched__IsDel__4B7734FF] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsActive]                BIT            CONSTRAINT [DF__AutoSched__IsAct__4C6B5938] DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]               INT            NULL,
    [CreatedOn]               DATETIME2 (7)  NOT NULL,
    [UpdatedBy]               INT            NULL,
    [UpdatedOn]               DATETIME2 (7)  NULL,
    [ScheduledDate]           DATE           NULL,
    [ScheduledTime]           TIME (7)       NULL,
    CONSTRAINT [PK_AutoSchedules] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AutoSchedules_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment].[Equipments] ([Id]),
    CONSTRAINT [FK_AutoSchedules_EquipmentScheduleIntervals_IntervalId] FOREIGN KEY ([IntervalId]) REFERENCES [Metadata].[EquipmentScheduleIntervals] ([Id]),
    CONSTRAINT [FK_AutoSchedules_EquipmentScheduleTypes_EquipmentScheduleTypeId] FOREIGN KEY ([EquipmentScheduleTypeId]) REFERENCES [Metadata].[EquipmentScheduleTypes] ([Id]),
    CONSTRAINT [FK_AutoSchedules_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Security].[Users] ([Id]),
    CONSTRAINT [FK_AutoSchedules_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Security].[Users] ([Id])
);
