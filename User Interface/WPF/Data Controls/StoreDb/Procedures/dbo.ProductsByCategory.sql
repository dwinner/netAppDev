CREATE PROCEDURE [dbo].[ProductsByCategory]
    @CategoryID int
AS
SELECT 
    ProductID,
    ModelName,
    UnitCost, 
    ProductImage
FROM 
    Products
WHERE 
    CategoryID = @CategoryID
ORDER BY 
    ModelName, 
    ModelNumber