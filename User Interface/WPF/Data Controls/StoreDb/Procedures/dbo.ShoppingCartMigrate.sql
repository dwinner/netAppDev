CREATE PROCEDURE [dbo].[ShoppingCartMigrate]
    @OriginalCartId nvarchar(50),
    @NewCartId      nvarchar(50)
AS
UPDATE 
    ShoppingCart    
SET 
    CartId = @NewCartId    
WHERE 
    CartId = @OriginalCartId