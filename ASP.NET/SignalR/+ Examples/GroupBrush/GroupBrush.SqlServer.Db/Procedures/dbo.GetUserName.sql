CREATE PROCEDURE [dbo].[GetUserName]
   @UserId Int,
   @UserName NVarChar(100) Output
AS
Begin
   Select @UserName = Name From Users Where UserId = @UserId
End