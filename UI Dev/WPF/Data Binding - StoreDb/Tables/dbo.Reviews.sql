CREATE TABLE [dbo].[Reviews] (
    [ReviewID] [int] IDENTITY (1, 1) NOT NULL ,
    [ProductID] [int] NOT NULL ,
    [CustomerName] [nvarchar] (50) ,
    [CustomerEmail] [nvarchar] (50) ,
    [Rating] [int] NOT NULL ,
    [Comments] [nvarchar] (3850) ,
)