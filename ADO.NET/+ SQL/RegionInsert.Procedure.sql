USE Northwind
GO

CREATE PROCEDURE RegionInsert(@RegionDescription NCHAR(50), @RegionId integer OUTPUT)
AS
	SET NOCOUNT OFF;
	SELECT @RegionId = MAX(RegionId) + 1
	FROM   Region;
	
	INSERT INTO Region
	  (
	    RegionID,
	    RegionDescription
	  )
	VALUES
	  (
	    @RegionId,
	    @RegionDescription
	  );
GO 	