CREATE PROCEDURE [dbo].[CustomerDetail]
    @CustomerID int,
    @FullName   nvarchar(50) OUTPUT,
    @Email      nvarchar(50) OUTPUT,
    @Password   nvarchar(50) OUTPUT
AS
SELECT 
    @FullName = FullName, 
    @Email    = EmailAddress, 
    @Password = Password
FROM 
    Customers
WHERE 
    CustomerID = @CustomerID