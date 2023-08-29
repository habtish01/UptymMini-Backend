
CREATE PROCEDURE [dbo].[spEquipmentContractSummary]
@equipment int=0,
@FacilityId int =0,
@OwnerId int =0,
@contracttype int=0
AS
BEGIN

	--SET NOCOUNT ON;

    SELECT 
COUNT([Equipment].[Equipments].Id) AS NumberOfEquipment,	
 (CASE WHEN [Equipment].[EquipmentContracts].[ContractType]=2 THEN 'Service' WHEN
			[Equipment].[EquipmentContracts].[ContractType]=1 THEN 'Warranty' END) as ContractType,

Equipment.Equipments.Name as Equipment
FROM    Equipment.Equipments INNER JOIN
	[Metadata].[HealthFacilities] ON
	[Metadata].[HealthFacilities].[Id]=[Equipment].[Equipments].HealthFacilityId
	Left JOIN
	[Equipment].[EquipmentContracts] ON
	[Equipment].[Equipments].[Id]=[Equipment].[EquipmentContracts].[EquipmentId]
	WHERE
	1=CASE WHEN @FacilityId=0 THEN 1 WHEN [Equipment].[Equipments].HealthFacilityId=@FacilityId THEN 1 END AND
	1=CASE WHEN @OwnerId=0 THEN 1 WHEN Equipment.Equipments.OwnerId=@OwnerId THEN 1 END and
	1=CASE WHEN @equipment=0 THEN 1 WHEN [Equipment].[Equipments].Id=@equipment then 1 end and
	1=CASE WHEN @contracttype=0 THEN 1 WHEN [Equipment].[EquipmentContracts].[ContractType]=@contracttype then 1 end
	group by [Equipment].[EquipmentContracts].[ContractType],Equipment.Equipments.Name
END
