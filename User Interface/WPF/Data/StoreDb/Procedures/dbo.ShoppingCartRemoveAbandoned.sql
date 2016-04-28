CREATE PROCEDURE [dbo].[ShoppingCartRemoveAbandoned]    
AS
DELETE FROM ShoppingCart
WHERE 
    DATEDIFF(dd, DateCreated, GetDate()) > 1