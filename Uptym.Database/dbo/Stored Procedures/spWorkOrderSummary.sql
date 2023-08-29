
CREATE PROCEDURE [dbo].[spWorkOrderSummary]
@userId int=0,
@FacilityId int =0,
@OwnerId int =0
--,@isEng int =0
AS
BEGIN

	--SET NOCOUNT ON;

SELECT 

COUNT(CASE WHEN [Maintenance].[WorkOrderHeader].[IsClosed]=0 AND ([Maintenance].[WorkOrders].[WorkOrderHeaderId] IS NULL) THEN 1 END) AS NumberOfNewWorkOrder,
COUNT(CASE WHEN [Maintenance].[WorkOrderHeader].[IsClosed]=0 AND [Maintenance].[WorkOrderStatus].[WorkOrderStatusTypeId]=3 then 1 END) AS NumberOfWorkOrderAwaitingConfirmation, --# of maintained problems waiting for confirmation
COUNT(CASE WHEN [Maintenance].[WorkOrderHeader].[WorkorderTypeId]=6 AND [Maintenance].[WorkOrderHeader].[AutoScheduleStatusId] =3 then 1 END) AS NumberOfUnhandledInspection, --# of unhandled inspection
COUNT(CASE WHEN ([Maintenance].[WorkOrderHeader].[IsClosed]=0 AND ([Maintenance].[WorkOrders].[PlannedEndDate]>SYSDATETIME())) THEN 1 END) AS NumberOfOverdueWorkOrder,--,# of overdue workorders (for follow up)
COUNT(CASE WHEN [Maintenance].[WorkOrderHeader].[IsClosed]=1 AND [Maintenance].[WorkOrderStatus].[WorkOrderStatusTypeId]=7 then 1 END) AS NumberOfClosedWorkOrder,
 CASE  WHEN (  ([Maintenance].[WorkOrderHeader].[IsClosed]=0)
 ) THEN count  (distinct Maintenance.WorkOrderHeader.Id) else 0 END AS NumberOfOpenWorkOrder ----,# of Opened workorders


FROM   Maintenance.WorkOrderHeader LEFT JOIN
           Maintenance.WorkOrders ON Maintenance.WorkOrderHeader.Id = Maintenance.WorkOrders.WorkOrderHeaderId LEFT JOIN
           Maintenance.WorkOrderStatus ON Maintenance.WorkOrders.Id = Maintenance.WorkOrderStatus.WorkOrderId LEFT JOIN
		   Equipment.Equipments ON Equipment.Equipments.Id=Maintenance.WorkOrderHeader.EquipmentId 
WHERE
--1=CASE WHEN @userId=0 THEN 1 WHEN Maintenance.WorkOrderHeader.CreatedBy=@userId  THEN 1 END AND--AND @isEng=1
1=CASE WHEN @FacilityId=0 THEN 1 WHEN [Equipment].[Equipments].HealthFacilityId=@FacilityId THEN 1 END AND
1=CASE WHEN @OwnerId=0 THEN 1 WHEN Equipment.Equipments.OwnerId=@OwnerId THEN 1 END
	group by [Maintenance].[WorkOrderHeader].[IsClosed]
END
