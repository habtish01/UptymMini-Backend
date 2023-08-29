
CREATE PROCEDURE [dbo].[spOperatorFailures]
@year int=0,
@FacilityId int =0,
@OwnerId int =0
AS
BEGIN

SELECT [HealthFacilityId],[OwnerId], [EquipmentOperators].Name As OperatorName,
(COUNT(CASE WHEN [Maintenance].[WorkOrderHeader].WorkorderTypeId =1  THEN 1 END)) AS NumberOfFailure

FROM
Maintenance.WorkOrderHeader LEFT JOIN [Equipment].[Equipments] ON
[Equipment].[Equipments].Id=[Maintenance].[WorkOrderHeader].[EquipmentId] 
left Join 
[Equipment].[EquipmentOperators] on [Equipments].Id =[EquipmentOperators].EquipmentId
WHERE 

YEAR([Maintenance].[WorkOrderHeader].[CreatedOn])=@year AND
 1=CASE WHEN @FacilityId=0 THEN 1 WHEN [HealthFacilityId]=@FacilityId THEN 1 END AND
1=CASE WHEN @OwnerId=0 THEN 1 WHEN [OwnerId]=@OwnerId THEN 1 END
GROUP BY 
[EquipmentOperators].Id,[HealthFacilityId],[OwnerId],[EquipmentOperators].Name

END
