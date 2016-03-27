CREATE PROCEDURE [dbo].[LookUpCanvas]
   @CanvasName nvarchar(100),
   @CanvasId UniqueIdentifier = NULL output
AS
Begin
   Declare @ReturnValue int = -1
   Select @CanvasId = CanvasId From Canvases Where CanvasName = @CanvasName
   If @CanvasId Is Not Null
      Begin
         Set @ReturnValue = 0
      End
   Return @ReturnValue
End