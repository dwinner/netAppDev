CREATE PROCEDURE [dbo].[ShoppingCartRemoveItem]
    @CartID nvarchar(50),
    @ProductID int
AS
DELETE FROM ShoppingCart
WHERE 
    CartID = @CartID
  AND
    ProductID = @ProductID