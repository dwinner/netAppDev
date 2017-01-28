CREATE TABLE [dbo].[Customers] (
    [CustomerID] [int] IDENTITY (1, 1) NOT NULL ,
    [FullName] [nvarchar] (50) ,
    [EmailAddress] [nvarchar] (50) ,
    [Password] [nvarchar] (50) 
)