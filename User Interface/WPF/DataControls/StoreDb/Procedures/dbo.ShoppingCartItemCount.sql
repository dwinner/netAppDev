CREATE PROCEDURE [dbo].[ShoppingCartItemCount]
    @CartID    nvarchar(50),
    @ItemCount int OUTPUT
AS
SELECT 
    @ItemCount = COUNT(ProductID)    
FROM 
    ShoppingCart    
WHERE 
    CartID = @CartID