CREATE PROCEDURE [dbo].[OrdersList]
    @CustomerID int
AS
SELECT  
    Orders.OrderID,
    Cast(sum(orderdetails.quantity*orderdetails.unitcost) as money) as OrderTotal,
    Orders.OrderDate, 
    Orders.ShipDate
FROM    
    Orders 
  INNER JOIN OrderDetails ON Orders.OrderID = OrderDetails.OrderID
GROUP BY    
    CustomerID, 
    Orders.OrderID, 
    Orders.OrderDate, 
    Orders.ShipDate
HAVING  
    Orders.CustomerID = @CustomerID