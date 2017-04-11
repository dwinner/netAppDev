CREATE PROCEDURE [dbo].[OrdersAdd]
    @CustomerID int,
    @CartID     nvarchar(50),
    @OrderDate  datetime,        
    @ShipDate   datetime,
    @OrderID    int OUTPUT
AS
BEGIN TRAN AddOrder

/* Create the Order header */
INSERT INTO Orders
(
    CustomerID, 
    OrderDate, 
    ShipDate
)
VALUES
(   
    @CustomerID, 
    @OrderDate, 
    @ShipDate
)

SELECT
    @OrderID = @@Identity    

/* Copy items from given shopping cart to OrdersDetail table for given OrderID*/
INSERT INTO OrderDetails
(
    OrderID, 
    ProductID, 
    Quantity, 
    UnitCost
)
SELECT 
    @OrderID, 
    ShoppingCart.ProductID, 
    Quantity, 
    Products.UnitCost
FROM 
    ShoppingCart 
  INNER JOIN Products ON ShoppingCart.ProductID = Products.ProductID  
WHERE 
    CartID = @CartID

/* Removal of  items from user's shopping cart will happen on the business layer*/
EXEC ShoppingCartEmpty @CartId

COMMIT TRAN AddOrder