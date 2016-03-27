CREATE PROCEDURE [dbo].[CreateUser]
	@Name nchar(100),
	@Password nchar(255),
	@UserId int output
AS
Begin
	Declare @ReturnValue int = -1
	If Exists (Select 1 From dbo.Users Where Name = @Name)
		Begin
			Set @ReturnValue = 1
		End
	Else
		Begin
			Declare @UserIds Table (userId int)
			Insert Into Users ([Name], [Password])
			Output Inserted.UserId Into @UserIds
			Values (@Name, @Password)
			Select @ReturnValue = 0, @UserId = userId From @UserIds
		End
	Return @ReturnValue
End