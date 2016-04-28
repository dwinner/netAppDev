CREATE PROCEDURE [dbo].[GetProductByID]
    @ProductID int
AS
SELECT * FROM Products WHERE ProductID=@ProductID