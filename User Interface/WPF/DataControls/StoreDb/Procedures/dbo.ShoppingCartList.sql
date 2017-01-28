CREATE PROCEDURE [dbo].[ShoppingCartList]
    @CartID nvarchar(50)
AS
SELECT 
    Products.ProductID, 
    Products.ModelName,
    Products.ModelNumber,
    ShoppingCart.Quantity,
    Products.UnitCost,
    Cast((Products.UnitCost * ShoppingCart.Quantity) as money) as ExtendedAmount
FROM 
    Products,
    ShoppingCart
WHERE 
    Products.ProductID = ShoppingCart.ProductID
  AND 
    ShoppingCart.CartID = @CartID
ORDER BY 
    Products.ModelName, 
    Products.ModelNumber