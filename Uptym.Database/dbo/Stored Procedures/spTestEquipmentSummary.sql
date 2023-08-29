
CREATE PROCEDURE [dbo].[spTestEquipmentSummary]
@userId int=0,
@FacilityId int =0,
@OwnerId int =0
AS
BEGIN

	--SET NOCOUNT ON;

    SELECT 
COUNT([Equipment].[Equipments].Id) AS NumberOfEquipment,	
COUNT(CASE WHEN [Equipment].[Equipments].EquipmentStatusId=3  THEN 1 END) AS NumberOfNotFunctionalEquipment,
COUNT(CASE WHEN [Equipment].[EquipmentContracts].[ContractType]=1 AND
[Equipment].[EquipmentContracts].[EndDate] <= SYSDATETIME()  THEN 1 END) AS EquipmentUnderWarranty,--1Warranty
COUNT(CASE WHEN [Equipment].[EquipmentContracts].[ContractType]=2 AND
[Equipment].[EquipmentContracts].[EndDate]>= SYSDATETIME() THEN 1 END) AS EquipmentUnderService, --2Service
COUNT(CASE WHEN [Equipment].[EquipmentContracts].[ContractType]=1 THEN 1 END) AS EquipmentWithWarranty,--1Warranty,
COUNT(CASE WHEN [Equipment].[EquipmentContracts].[ContractType]=1 AND
[Equipment].[EquipmentContracts].[EndDate] >= SYSDATETIME()  THEN 1 END) AS EquipmentwithOutWarranty,
COUNT([Equipment].EquipmentContracts.Id)  AS NumberOfEquipmentWithContract	--1Warranty,
--Equipment.Equipments.*
FROM    Equipment.Equipments INNER JOIN
	[Metadata].[HealthFacilities] ON
	[Metadata].[HealthFacilities].[Id]=[Equipment].[Equipments].HealthFacilityId
	Left JOIN
	[Equipment].[EquipmentContracts] ON
	[Equipment].[Equipments].[Id]=[Equipment].[EquipmentContracts].[EquipmentId]
	WHERE
	1=CASE WHEN @FacilityId=0 THEN 1 WHEN [Equipment].[Equipments].HealthFacilityId=@FacilityId THEN 1 END AND
	1=CASE WHEN @OwnerId=0 THEN 1 WHEN Equipment.Equipments.OwnerId=@OwnerId THEN 1 END

END
