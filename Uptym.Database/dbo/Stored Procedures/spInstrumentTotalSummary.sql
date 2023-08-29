
CREATE PROCEDURE [dbo].[spInstrumentTotalSummary]
@userId int=0,
@FacilityId int =0,
@OwnerId int =0
AS
BEGIN

    SELECT 
	'36' as AvgInstrumentUptime,
	'29' as AvgInstrumentDowntime,
	'23' as Mtbf,
	'12' as Mrttr

END
