

CREATE FUNCTION [dbo].[GetNumberOfEquipmentFailure]
(
	@equipmentId int
	,@year int
)
RETURNS int
AS
BEGIN

	DECLARE @numberOfEquipmentFailure int

	SET @numberOfEquipmentFailure =
	(SELECT count(Id) 
	FROM Maintenance.WorkOrderHeader
	WHERE
	WorkorderTypeId=1 AND
	EquipmentId=@equipmentId 
	and 
	YEAR(Maintenance.WorkOrderHeader.EquipmentFailureDate)=@year
	
	)


	RETURN @numberOfEquipmentFailure

END
