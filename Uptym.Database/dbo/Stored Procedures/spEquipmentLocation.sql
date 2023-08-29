
CREATE PROCEDURE [dbo].[spEquipmentLocation]
@userId int=0,
@FacilityId int =0,
@OwnerId int =0
AS
BEGIN

	--SET NOCOUNT ON;

    SELECT 
[Equipment].[Equipments].Name +  ''+ '[SerialNo]:' + Equipment.Equipments.SerialNumber As Equipment,	
Metadata.HealthFacilities.Latitude,Metadata.HealthFacilities.Longitude,EquipmentCategories.Name as Equipment_Category,EquipmentCategories.Id,
 (CASE WHEN [Equipment].[Equipments].EquipmentStatusId  =1 THEN 'Functional' WHEN
      [Equipment].[Equipments].EquipmentStatusId  =3  THEN 'Not Functional' END) as Functionality_Status
FROM    Equipment.Equipments INNER JOIN
	[Metadata].[HealthFacilities] ON
	[Metadata].[HealthFacilities].[Id]=[Equipment].[Equipments].HealthFacilityId inner join 
	Metadata.EquipmentLookups on Equipment.Equipments.EquipmentLookupId=EquipmentLookups.Id inner join
	Metadata.EquipmentCategories on Metadata.EquipmentCategories.Id=EquipmentLookups.EquipmentCategoryId
		WHERE
	--1=CASE WHEN @FacilityId=0 THEN 1 WHEN [Equipment].[Equipments].HealthFacilityId=@FacilityId THEN 1 END AND
	--1=CASE WHEN @OwnerId=0 THEN 1 WHEN Equipment.Equipments.OwnerId=@OwnerId THEN 1 END and
	EquipmentLookups.EquipmentTypeId=4

	Group by
	Metadata.HealthFacilities.Latitude,Metadata.HealthFacilities.Longitude,
	[Equipment].[Equipments].Name , Equipment.Equipments.SerialNumber,EquipmentCategories.Name ,EquipmentCategories.Id,
	[Equipment].[Equipments].EquipmentStatusId
END
