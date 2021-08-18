ALTER TABLE [dbo].[Categories] WITH NOCHECK ADD 
    CONSTRAINT [PK_Categories] PRIMARY KEY  NONCLUSTERED 
    (
        [CategoryID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Customers] WITH NOCHECK ADD 
    CONSTRAINT [PK_Customers] PRIMARY KEY  NONCLUSTERED 
    (
        [CustomerID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[OrderDetails] WITH NOCHECK ADD 
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY  NONCLUSTERED 
    (
        [OrderID],
        [ProductID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Orders] WITH NOCHECK ADD
    CONSTRAINT [DF_Orders_OrderDate] DEFAULT (getdate()) FOR [OrderDate]    
GO

ALTER TABLE [dbo].[Orders] WITH NOCHECK ADD    
    CONSTRAINT [DF_Orders_ShipDate] DEFAULT (getdate()) FOR [ShipDate]    
GO

ALTER TABLE [dbo].[Orders] WITH NOCHECK ADD        
    CONSTRAINT [PK_Orders] PRIMARY KEY  NONCLUSTERED 
    (
        [OrderID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Products] WITH NOCHECK ADD 
    CONSTRAINT [PK_Products] PRIMARY KEY  NONCLUSTERED 
    (
        [ProductID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ShoppingCart] WITH NOCHECK ADD 
    CONSTRAINT [DF_ShoppingCart_Quantity] DEFAULT (1) FOR [Quantity]    
GO

ALTER TABLE [dbo].[ShoppingCart] WITH NOCHECK ADD     
    CONSTRAINT [DF_ShoppingCart_DateCreated] DEFAULT (getdate()) FOR [DateCreated]    
GO

ALTER TABLE [dbo].[ShoppingCart] WITH NOCHECK ADD         
    CONSTRAINT [PK_ShoppingCart] PRIMARY KEY  NONCLUSTERED 
    (
        [RecordID]
    )  ON [PRIMARY] 
GO

CREATE  INDEX [IX_ShoppingCart] ON [dbo].[ShoppingCart]([CartID], [ProductID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderDetails] ADD 
    CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY 
    (
        [OrderID]
    ) REFERENCES [dbo].[Orders] (
        [OrderID]
    ) NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Orders] ADD 
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY 
    (
        [CustomerID]
    ) REFERENCES [dbo].[Customers] (
        [CustomerID]
    ) NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Products] ADD 
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY 
    (
        [CategoryID]
    ) REFERENCES [dbo].[Categories] (
        [CategoryID]
    )
GO

ALTER TABLE [dbo].[Reviews] ADD 
    CONSTRAINT [FK_Reviews_Products] FOREIGN KEY 
    (
        [ProductID]
    ) REFERENCES [dbo].[Products] (
        [ProductID]
    ) NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ShoppingCart] ADD 
    CONSTRAINT [FK_ShoppingCart_Products] FOREIGN KEY 
    (
        [ProductID]
    ) REFERENCES [dbo].[Products] (
        [ProductID]
    )
GO