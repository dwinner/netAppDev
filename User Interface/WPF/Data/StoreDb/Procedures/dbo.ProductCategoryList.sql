CREATE PROCEDURE [dbo].[ProductCategoryList]    
AS
SELECT 
    CategoryID,
    CategoryName
FROM 
    Categories
ORDER BY 
    CategoryName ASC