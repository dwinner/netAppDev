CREATE TABLE [dbo].[Products] (
    [ProductID] [int] IDENTITY (1, 1) NOT NULL ,
    [CategoryID] [int] NOT NULL ,
    [ModelNumber] [nvarchar] (50) ,
    [ModelName] [nvarchar] (50) ,
    [ProductImage] [nvarchar] (50) ,
    [UnitCost] [money] NOT NULL ,
    [Description] [nvarchar] (3800) 
)