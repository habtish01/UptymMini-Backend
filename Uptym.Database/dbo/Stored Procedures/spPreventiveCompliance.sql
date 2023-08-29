
CREATE PROCEDURE [dbo].[spPreventiveCompliance]
@year int=0,
@FacilityId int =0,
@OwnerId int =0
AS
BEGIN

SELECT [HealthFacilityId],[OwnerId],EquipmentId,
(COUNT(CASE WHEN [Maintenance].[WorkOrderHeader].[IsClosed]=1 THEN 1 END)) AS NumberOfCompletedPMs,
(COUNT(Maintenance.WorkOrderHeader.Id)) AS NumberOfScheduledPMs
FROM
Maintenance.WorkOrderHeader LEFT JOIN [Equipment].[Equipments] ON
[Equipment].[Equipments].Id=[Maintenance].[WorkOrderHeader].[EquipmentId]
WHERE 
[Maintenance].[WorkOrderHeader].WorkorderTypeId=3 
AND YEAR([Maintenance].[WorkOrderHeader].[CreatedOn])=@year AND
 1=CASE WHEN @FacilityId=0 THEN 1 WHEN [HealthFacilityId]=@FacilityId THEN 1 END AND
1=CASE WHEN @OwnerId=0 THEN 1 WHEN [OwnerId]=@OwnerId THEN 1 END
GROUP BY 
EquipmentId,[HealthFacilityId],[OwnerId]

END
