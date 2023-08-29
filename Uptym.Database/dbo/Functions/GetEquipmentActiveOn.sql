

CREATE FUNCTION [dbo].[GetEquipmentActiveOn]
(
	@equipmentId int
)
RETURNS datetime
AS
BEGIN

	DECLARE @EquipmentActiveOn datetime

	SET @EquipmentActiveOn =
	(SELECT [CreatedOn] 
	FROM [Equipment].[Equipments]
	WHERE
	[Equipment].[Equipments].[Id]=@equipmentId
	)


	RETURN @EquipmentActiveOn

END
