/*
create function CalcHash
(
    @value nvarchar
)
return nvarchar
as external name SqlSamplesUsingAdventureWorks.UserDefinedFunctions.CalcHash
*/
SELECT
    Sales.CreditCard.CardType AS [Card Type], 
    dbo.CalcHash(Sales.CreditCard.CardNumber) AS [Hashed Card] 
FROM Sales.CreditCard INNER JOIN Sales.ContactCreditCard
    ON  Sales.CreditCard.CreditCardID = Sales.ContactCreditCard.CreditCardID 
WHERE Sales.ContactCreditCard.ContactID = 11