USE Northwind
GO

CREATE PROCEDURE RegionUpdate(@RegionId integer, @RegionDescription NCHAR(50))
AS
	SET NOCOUNT OFF;
	
	UPDATE Region
	SET    RegionDescription     = @RegionDescription
	WHERE  RegionID              = @RegionId;
GO