using RefStructInterfaces;

Shape myShape = new();
myShape.Draw();
return;

// Draw_Shape(myShape);        // Error
// var shape2 = (IShape)shape; // Error

void DrawShape(IShape shape)
{
   shape.Draw();
}