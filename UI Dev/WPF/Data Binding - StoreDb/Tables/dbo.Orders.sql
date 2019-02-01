CREATE TABLE [dbo].[Orders] (
    [OrderID] [int] IDENTITY (1, 1) NOT NULL ,
    [CustomerID] [int] NOT NULL ,
    [OrderDate] [datetime] NOT NULL ,
    [ShipDate] [datetime] NOT NULL 
)