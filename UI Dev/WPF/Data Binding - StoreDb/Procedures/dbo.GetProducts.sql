CREATE PROCEDURE [dbo].[GetProducts]    
AS
    SELECT * FROM Products INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID