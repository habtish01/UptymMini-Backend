
CREATE PROCEDURE [dbo].[spFrquentlyMaintainedEquipment]
@equipment int=0,
@FacilityId int =0,
@OwnerId int =0
--,@isEng int =0
AS
BEGIN

	--SET NOCOUNT ON;

SELECT 

COUNT([Maintenance].[WorkOrderHeader].EquipmentId) AS NumberOfFailure,
Metadata.ProblemTypes.Name As ProblemType,Equipment.Equipments.Name As Equipment


FROM   Maintenance.WorkOrderHeader LEFT JOIN
           Maintenance.WorkOrders ON Maintenance.WorkOrderHeader.Id = Maintenance.WorkOrders.WorkOrderHeaderId LEFT JOIN
           Maintenance.WorkOrderStatus ON Maintenance.WorkOrders.Id = Maintenance.WorkOrderStatus.WorkOrderId LEFT JOIN
		   Equipment.Equipments ON Equipment.Equipments.Id=Maintenance.WorkOrderHeader.EquipmentId left join
		   Metadata.ProblemTypes on Maintenance.WorkOrderHeader.ProblemTypeId=Metadata.ProblemTypes.Id
WHERE
--1=CASE WHEN @userId=0 THEN 1 WHEN Maintenance.WorkOrderHeader.CreatedBy=@userId  THEN 1 END AND--AND @isEng=1
1=CASE WHEN @FacilityId=0 THEN 1 WHEN [Equipment].[Equipments].HealthFacilityId=@FacilityId THEN 1 END AND
1=CASE WHEN @OwnerId=0 THEN 1 WHEN Equipment.Equipments.OwnerId=@OwnerId THEN 1 END and
1=CASE WHEN @equipment=0 THEN 1 WHEN Equipment.Equipments.Id=@equipment then 1 end
	group by
	Metadata.ProblemTypes.Name,Equipment.Equipments.Name
END
