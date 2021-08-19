CREATE PROCEDURE [dbo].[ShoppingCartTotal]
    @CartID    nvarchar(50),
    @TotalCost money OUTPUT
AS
SELECT 
    @TotalCost = SUM(Products.UnitCost * ShoppingCart.Quantity)
FROM 
    ShoppingCart,
    Products
WHERE
    ShoppingCart.CartID = @CartID
  AND
    Products.ProductID = ShoppingCart.ProductID