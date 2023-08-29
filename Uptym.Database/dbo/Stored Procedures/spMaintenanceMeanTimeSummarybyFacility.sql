
CREATE PROCEDURE [dbo].[spMaintenanceMeanTimeSummarybyFacility]


@year int=0,

@OwnerId int=0,
@FacilityId int=0
AS
BEGIN


SELECT [OwnerId],EquipmentId,
(select [dbo].[GetNumberOfEquipmentFailure](EquipmentId,2021)) as NumberOfFailure,
SUM(t.offlineHours) AS OfflineHours,
SUM(t.hoursSinceActive) AS HoursSinceActive,
SUM(t.operationalHours) AS OperationalHours
FROM
(
SELECT [HealthFacilityId],[OwnerId],EquipmentId,
 (datediff(hour,Isnull(EquipmentFailureDate ,Getdate()) ,Isnull([Maintenance].[WorkOrderHeader].UpdatedOn,Getdate())))  AS OfflineHours,

 (datediff(hour, (select [dbo].[GetEquipmentActiveOn](EquipmentId)),Getdate()))  AS HoursSinceActive,
 (datediff(hour, (select [dbo].[GetEquipmentActiveOn](EquipmentId)),Getdate())) - 
 (datediff(hour,Isnull([Maintenance].[WorkOrderHeader].UpdatedOn,Getdate()), Isnull(EquipmentFailureDate ,Getdate())))  AS OperationalHours

FROM
Maintenance.WorkOrderHeader left  JOIN [Equipment].[Equipments] ON
[Equipment].[Equipments].Id=[Maintenance].[WorkOrderHeader].[EquipmentId] INNER JOIN
	[Metadata].[HealthFacilities] ON
	[Metadata].[HealthFacilities].[Id]=[Equipment].[Equipments].HealthFacilityId
WHERE [Maintenance].[WorkOrderHeader].WorkorderTypeId=1
 AND

1=CASE WHEN @FacilityId=0 THEN 1 WHEN [Equipment].[Equipments].HealthFacilityId=@FacilityId THEN 1 END 


) T GROUP BY EquipmentId,[HealthFacilityId],[OwnerId]








END
