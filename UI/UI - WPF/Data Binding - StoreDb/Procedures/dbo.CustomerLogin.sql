CREATE PROCEDURE [dbo].[CustomerLogin]
    @Email      nvarchar(50),
    @Password   nvarchar(50),
    @CustomerID int OUTPUT
AS
SELECT 
    @CustomerID = CustomerID    
FROM 
    Customers    
WHERE 
    EmailAddress = @Email
 AND 
    Password = @Password

IF @@Rowcount < 1 
SELECT 
    @CustomerID = 0