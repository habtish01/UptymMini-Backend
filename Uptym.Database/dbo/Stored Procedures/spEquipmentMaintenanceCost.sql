
CREATE PROCEDURE [dbo].[spEquipmentMaintenanceCost]
@year int=0,
@FacilityId int =0,
@OwnerId int =0
AS
BEGIN

SELECT [HealthFacilityId],[OwnerId], [Equipment].[Equipments].Name +  ''+ '[SerialNo]:' + Equipment.Equipments.SerialNumber As Equipment,
(Sum(Maintenance.WorkOrderMaintenanceCosts.Cost)+ Sum(Maintenance.WorkOrderSpareparts.Cost)) AS TotalCosts

FROM
 [Equipment].[Equipments] left join Maintenance.WorkOrderHeader on Equipment.Equipments.Id=WorkOrderHeader.EquipmentId
 left join Maintenance.WorkOrders on WorkOrderHeader.Id=WorkOrders.WorkOrderHeaderId
 left join Maintenance.WorkOrderMaintenanceCosts on Maintenance.WorkOrders.Id =WorkOrderMaintenanceCosts.WorkOrderId
 left join Maintenance.WorkOrderSpareparts on Maintenance.WorkOrders.Id=WorkOrderSpareparts.WorkOrderId

WHERE 

YEAR([Maintenance].[WorkOrderHeader].[CreatedOn])=@year AND
 1=CASE WHEN @FacilityId=0 THEN 1 WHEN [HealthFacilityId]=@FacilityId THEN 1 END AND
1=CASE WHEN @OwnerId=0 THEN 1 WHEN [OwnerId]=@OwnerId THEN 1 END
GROUP BY 
[HealthFacilityId],[OwnerId],
[Equipment].Equipments.Name, [Equipment].Equipments.SerialNumber

END
