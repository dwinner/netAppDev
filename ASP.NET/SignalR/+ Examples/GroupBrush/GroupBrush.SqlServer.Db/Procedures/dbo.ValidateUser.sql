CREATE PROCEDURE [dbo].[ValidateUser]
   @Name nchar(100),
   @Password nchar(255),
   @UserId int = NULL output,
   @ValidUser bit output
AS
Begin
   Select @UserId = UserId From Users Where Name = @Name and Password = @Password
   If (@UserId Is Not Null)
      Begin
         Set @ValidUser = 1
      End
   Else
      Begin
         Set @ValidUser = 0
      End
End