
create PROCEDURE [dbo].[spMaintenanceMeanTimeSummarybyFacility1]


@Facility int =0

AS
BEGIN

Select * FROM
Maintenance.WorkOrderHeader
left  JOIN [Equipment].[Equipments] ON
[Equipment].[Equipments].Id=[Maintenance].[WorkOrderHeader].[EquipmentId] 

WHERE [Maintenance].[WorkOrderHeader].WorkorderTypeId=1 and  [Equipment].[Equipments].HealthFacilityId =@Facility

end
