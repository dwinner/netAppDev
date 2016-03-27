CREATE PROCEDURE [dbo].[CreateCanvas]
   @CanvasName nchar(100),
   @CanvasDescription nchar(255),
   @CanvasId UniqueIdentifier output
AS
Begin
   Declare @ReturnValue int = -1
   If Exists(Select 1 From Canvases Where CanvasName = @CanvasName)
      Begin
         Set @ReturnValue = 1
      End
   Else
      Begin
         Declare @CanvasIds Table (CanvasId UniqueIdentifier)
         Insert Into Canvases (CanvasId, CanvasName, CanvasDescription)
         Output Inserted.CanvasId Into @CanvasIds(CanvasId)
         Values (NewId(), @CanvasName, @CanvasDescription)
         Select @ReturnValue = 0, @CanvasId = CanvasId From @CanvasIds
      End
   Return @ReturnValue
End