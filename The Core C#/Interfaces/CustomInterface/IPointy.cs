using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomInterface
{
   // Этот интерфейс определяет поведение наличия точек
   public interface IPointy
   {
      byte Points { get; } // Неявно открытый доступ
   }

   public interface IDraw3D
   {
      void Draw3D();
   }
}
