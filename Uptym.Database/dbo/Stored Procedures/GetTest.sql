-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTest]
	
AS
  BEGIN
    
--   SELECT  Metadata.Continents.Name as one,
--   --Metadata.Countries.ShortCode as one, 
--   Metadata.Countries.ShortCode as two, 
--   Metadata.Countries.ShortName as three
--FROM   Metadata.Continents INNER JOIN
--           Metadata.Countries ON Metadata.Continents.Id = Metadata.Countries.ContinentId

      SELECT '136' as one,'29' as two,'23' as three
	  ,'12' as four, '34' as five
     
  END;
