USE Northwind
GO

CREATE PROCEDURE RegionDelete(@RegionId integer)
AS
	SET NOCOUNT OFF;
	
	DELETE 
	FROM   Region
	WHERE  RegionID = @RegionId;
GO