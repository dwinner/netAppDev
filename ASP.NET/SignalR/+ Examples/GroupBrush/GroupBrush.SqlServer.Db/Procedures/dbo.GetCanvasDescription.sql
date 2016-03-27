CREATE PROCEDURE [dbo].[GetCanvasDescription]
   @CanvasId UniqueIdentifier,
   @CanvasName NVarChar(100) output,
   @CanvasDescription NVarChar(100) output
AS
Begin
   Select @CanvasName = CanvasName, @CanvasDescription = CanvasDescription
   From Canvases
   Where CanvasId = @CanvasId
End