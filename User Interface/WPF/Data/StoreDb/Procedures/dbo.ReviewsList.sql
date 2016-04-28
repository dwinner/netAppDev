CREATE PROCEDURE [dbo].[ReviewsList]
    @ProductID int
AS
SELECT 
    ReviewID, 
    CustomerName, 
    Rating, 
    Comments    
FROM 
    Reviews    
WHERE 
    ProductID = @ProductID