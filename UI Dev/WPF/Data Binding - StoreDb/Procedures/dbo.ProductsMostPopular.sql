CREATE PROCEDURE [dbo].[ProductsMostPopular]    
AS
SELECT TOP 5 
    OrderDetails.ProductID, 
    SUM(OrderDetails.Quantity) as TotalNum, 
    Products.ModelName    
FROM    
    OrderDetails
  INNER JOIN Products ON OrderDetails.ProductID = Products.ProductID  
GROUP BY 
    OrderDetails.ProductID, 
    Products.ModelName    
ORDER BY 
    TotalNum DESC