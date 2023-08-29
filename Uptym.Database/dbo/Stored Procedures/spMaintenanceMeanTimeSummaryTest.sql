
CREATE PROCEDURE [dbo].[spMaintenanceMeanTimeSummaryTest]
@year int=0,
@FacilityId int=0,
@OwnerId int=0,
@EquipmentId int=0

AS
BEGIN


SELECT [OwnerId],EquipmentId,[HealthFacilityId],
(select [dbo].[GetNumberOfEquipmentFailure](EquipmentId,@year)) as NumberOfFailure,
SUM(t.offlineHours) AS OfflineHours,
SUM(t.hoursSinceActive) AS HoursSinceActive,
SUM(t.operationalHours) AS OperationalHours
FROM
(
SELECT [HealthFacilityId],[OwnerId],EquipmentId,
 (datediff(hour,Isnull(Cast(EquipmentFailureDate As Date),SYSDATETIME()) ,IsNull(Cast([Maintenance].[WorkOrderHeader].UpdatedOn As Date),SYSDATETIME())))  AS OfflineHours,

 (datediff(hour, (select [dbo].[GetEquipmentActiveOn](EquipmentId)),SYSDATETIME()))  AS HoursSinceActive,
 (datediff(hour, (select [dbo].[GetEquipmentActiveOn](EquipmentId)),SYSDATETIME())) - 
 (datediff(hour,IsNull(Cast([Maintenance].[WorkOrderHeader].UpdatedOn As Date),SYSDATETIME()), Isnull(Cast(EquipmentFailureDate As Date),SYSDATETIME())))  AS OperationalHours

FROM
Maintenance.WorkOrderHeader left  JOIN [Equipment].[Equipments] ON
[Equipment].[Equipments].Id=[Maintenance].[WorkOrderHeader].[EquipmentId] 
  
WHERE [Maintenance].[WorkOrderHeader].WorkorderTypeId=1
 AND YEAR(Isnull(Cast(EquipmentFailureDate As Date),SYSDATETIME()))=@year AND

1=CASE WHEN @OwnerId=0 THEN 1 WHEN [OwnerId]=@OwnerId THEN 1 END and

1=CASE WHEN @FacilityId=0 THEN 1 WHEN [HealthFacilityId]=@FacilityId THEN 1 END and
1=CASE WHEN @EquipmentId=0 THEN 1 WHEN [Equipment].[Equipments].Id=@EquipmentId THEN 1 END

) T GROUP BY EquipmentId,[HealthFacilityId],[OwnerId]








END
