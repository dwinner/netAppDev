CREATE PROCEDURE [dbo].[ShoppingCartAddItem]
    @CartID nvarchar(50),
    @ProductID int,
    @Quantity int
AS

DECLARE @CountItems int

SELECT
    @CountItems = Count(ProductID)
FROM
    ShoppingCart
WHERE
    ProductID = @ProductID
  AND
    CartID = @CartID
    
IF @CountItems > 0  /* There are items - update the current quantity */
    UPDATE
        ShoppingCart
    SET
        Quantity = (@Quantity + ShoppingCart.Quantity)
    WHERE
        ProductID = @ProductID
      AND
        CartID = @CartID

ELSE  /* New entry for this Cart.  Add a new record */
    INSERT INTO ShoppingCart
    (
        CartID,
        Quantity,
        ProductID
    )
    VALUES
    (
        @CartID,
        @Quantity,
        @ProductID
    )