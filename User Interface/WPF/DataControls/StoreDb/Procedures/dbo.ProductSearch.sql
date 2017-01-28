CREATE PROCEDURE [dbo].[ProductSearch]
    @Search nvarchar(255)
AS
SELECT 
    ProductID,
    ModelName,
    ModelNumber,
    UnitCost, 
    ProductImage
FROM 
    Products
WHERE 
    ModelNumber LIKE '%' + @Search + '%' 
   OR
    ModelName LIKE '%' + @Search + '%'
   OR
    Description LIKE '%' + @Search + '%'