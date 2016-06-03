CREATE TABLE [dbo].[Rental]
(
	[RentalId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NULL, 
    [CarId] NVARCHAR(50) NULL, 
    [PickUpLocation] INT NULL, 
    [DropOffLocation] INT NULL, 
    [PickUpDateTime] DATETIME NULL, 
    [DropOffDateTime] DATETIME NULL, 
    [PaymentStatus] CHAR(3) NULL, 
    [Comments] NVARCHAR(1000) NULL
)
