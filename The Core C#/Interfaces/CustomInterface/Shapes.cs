using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomInterface
{
   #region Shape base class
   // Абстрактый базовый класс иерархии.
   abstract class Shape
   {
      public Shape()
      {
         PetName = "NoName";
      }

      public Shape(string name)
      {
         PetName = name;
      }
      
      public abstract void Draw();

      public string PetName { get; set; }
   }
   #endregion

   #region Circle class   
   class Circle : Shape
   {
      public Circle() { }
      
      public Circle(string name) : base(name) { }
      
      public override void Draw()
      {
         Console.WriteLine("Drawing {0} the Circle", PetName);
      }
   }
   #endregion

   #region Hexagon class   
   class Hexagon : Shape, IPointy, IDraw3D
   {
      public Hexagon() { }
      
      public Hexagon(string name) : base(name) { }
      
      public override void Draw()
      {
         Console.WriteLine("Drawing {0} the Hexagon", PetName);
      }

      // IPointy Implementation.
      public byte Points
      {
         get { return 6; }
      }

      #region IDraw3D Members
      public void Draw3D()
      {
         Console.WriteLine("Drawing Hexagon in 3D!");
      }
      #endregion
   }
   #endregion

   #region ThreeDCircle class
   // This class extends Circle and hides the inherited Draw() method.
   class ThreeDCircle : Circle, IDraw3D
   {
      // Hide any Draw() implementation above me.
      public new void Draw()
      {
         Console.WriteLine("Drawing a 3D Circle");
      }

      public new string PetName { get; set; }

      #region IDraw3D Members
      public void Draw3D()
      {
         Console.WriteLine("Drawing Circle in 3D!");
      }
      #endregion
   }
   #endregion

   #region Triangle
   // New Shape derived class named Triangle.
   class Triangle : Shape, IPointy
   {
      public Triangle() { }
      
      public Triangle(string name) : base(name) { }
      
      public override void Draw()
      {
         Console.WriteLine("Drawing {0} the Triangle", PetName);
      }

      // IPointy Implementation.
      public byte Points
      {
         get { return 3; }
      }
   }
   #endregion

}
