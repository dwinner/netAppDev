--create aggregate SampleSum (@value int) returns int external name [SqlSamplesUsingAdventureWorks].[SampleSum];

SELECT
    Purchasing.PurchaseOrderDetail.ProductID AS Id, 
    Production.Product.Name AS Product, 
    dbo.SampleSum(Purchasing.PurchaseOrderDetail.OrderQty) AS Total
FROM Production.Product INNER JOIN Purchasing.PurchaseOrderDetail
    ON Purchasing.PurchaseOrderDetail.ProductID = Production.Product.ProductID 
GROUP BY Purchasing.PurchaseOrderDetail.ProductID, Production.Product.Name
ORDER BY Id;