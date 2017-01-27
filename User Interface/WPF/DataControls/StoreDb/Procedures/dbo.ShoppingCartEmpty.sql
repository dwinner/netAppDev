CREATE PROCEDURE [dbo].[ShoppingCartEmpty]
    @CartID nvarchar(50)
AS
DELETE FROM ShoppingCart
WHERE CartID = @CartID