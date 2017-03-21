CREATE PROCEDURE [dbo].[OrdersDetail]
    @OrderID    int,
    @OrderDate  datetime OUTPUT,
    @ShipDate   datetime OUTPUT,
    @OrderTotal money OUTPUT
AS
SELECT  
    @OrderTotal = Cast(SUM(OrderDetails.Quantity * OrderDetails.UnitCost) as money)    
FROM    
    OrderDetails    
WHERE   
    OrderID= @OrderID

SELECT 
    @OrderDate = OrderDate,
    @ShipDate = ShipDate    
FROM    
    Orders    
WHERE   
    OrderID = @OrderID

SELECT  
    Products.ProductID, 
    Products.ModelName,
    Products.ModelNumber,
    OrderDetails.UnitCost,
    OrderDetails.Quantity,
    (OrderDetails.Quantity * OrderDetails.UnitCost) as ExtendedAmount
FROM
    OrderDetails
  INNER JOIN Products ON OrderDetails.ProductID = Products.ProductID  
WHERE   
    OrderID = @OrderID