-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTestByParam]
	@ContinentId int=0
AS
  BEGIN
    
   SELECT  Metadata.Continents.Name as one,
   --Metadata.Countries.ShortCode as one, 
   Metadata.Countries.ShortCode as two, 
   Metadata.Countries.ShortName as three
FROM   Metadata.Continents INNER JOIN
           Metadata.Countries ON Metadata.Continents.Id = Metadata.Countries.ContinentId
		   WHERE 
		   1=case when @ContinentId=0 then 1 when Metadata.Continents.Id=@ContinentId then 1 end
		   --Metadata.Continents.Id=@ContinentId
      
     
  END;
