CREATE PROCEDURE [dbo].[ShoppingCartUpdate]
    @CartID    nvarchar(50),
    @ProductID int,
    @Quantity  int
AS
UPDATE ShoppingCart
SET 
    Quantity = @Quantity
WHERE 
    CartID = @CartID 
  AND 
    ProductID = @ProductID