CREATE TABLE [dbo].[ShoppingCart] (
    [RecordID] [int] IDENTITY (1, 1) NOT NULL ,
    [CartID] [nvarchar] (50) ,
    [Quantity] [int] NOT NULL ,
    [ProductID] [int] NOT NULL ,
    [DateCreated] [datetime] NOT NULL 
)