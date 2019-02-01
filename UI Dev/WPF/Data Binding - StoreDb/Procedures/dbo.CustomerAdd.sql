CREATE PROCEDURE [dbo].[CustomerAdd]
    @FullName   nvarchar(50),
    @Email      nvarchar(50),
    @Password   nvarchar(50),
    @CustomerID int OUTPUT
AS
INSERT INTO Customers
(
    FullName,
    EMailAddress,
    Password
)

VALUES
(
    @FullName,
    @Email,
    @Password
)

SELECT
    @CustomerID = @@Identity