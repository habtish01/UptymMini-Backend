CREATE TABLE [dbo].[WorkOrderSummary] (
    [NumberOfNewWorkOrder]                  INT NOT NULL,
    [NumberOfWorkOrderAwaitingConfirmation] INT NOT NULL,
    [NumberOfUnhandledInspection]           INT NOT NULL,
    [NumberOfOverdueWorkOrder]              INT NOT NULL,
    [NumberOfOpenWorkOrder]                 INT NOT NULL,
    [NumberOfClosedWorkOrder]               INT NULL
);

