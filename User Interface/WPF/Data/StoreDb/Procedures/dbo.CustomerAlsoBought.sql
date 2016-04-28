CREATE PROCEDURE [dbo].[CustomerAlsoBought]
    @ProductID int
AS
SELECT  TOP 5 
    OrderDetails.ProductID,
    Products.ModelName,
    SUM (OrderDetails.Quantity) as TotalNum
FROM    
    OrderDetails
  INNER JOIN Products ON OrderDetails.ProductID = Products.ProductID

WHERE   OrderID IN 
(    
    SELECT DISTINCT OrderID 
    FROM OrderDetails
    WHERE ProductID = @ProductID
) AND OrderDetails.ProductID != @ProductID 

GROUP BY OrderDetails.ProductID, Products.ModelName 
ORDER BY TotalNum DESC