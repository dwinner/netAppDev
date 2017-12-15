using System;
using System.Runtime.Serialization;

namespace SerializeVertex
{
   [Serializable]
   public struct Vertex3D : ISerializable
   {
      private int? _id;

      private double _x;
      private double _y;
      private double _z;

      public int? Id
      {
         get => _id;
         set => _id = value;
      }

      public double X
      {
         get => _x;
         set => _x = value;
      }

      public double Y
      {
         get => _y;
         set => _y = value;
      }

      public double Z
      {
         get => _z;
         set => _z = value;
      }

      public Vertex3D(double x, double y, double z)
      {
         _x = x;
         _y = y;
         _z = z;

         _id = null;
      }

      /*
       * Note: Обратите внимание, что конструктор определен с модификатором
       * private. Если бы у нас был класс, он бы имел модификатор protected,
       * чтобы производные классы могли обращаться к нему.
       */
      private Vertex3D(SerializationInfo info, StreamingContext context) // Note: Логика десериализации
      {
         if (info == null)
            throw new ArgumentNullException(nameof(info));

         int tempId;
         _id = (tempId = info.GetInt32("id")) != 0 ? tempId : (int?) null;

         _x = info.GetInt32("x");
         _y = info.GetInt32("y");
         _z = info.GetInt32("z");
      }

      public void GetObjectData(SerializationInfo info, StreamingContext context) // Note: Логика сериализации
      {
         info.AddValue("id", _id ?? 0);
         info.AddValue("x", _x);
         info.AddValue("y", _y);
         info.AddValue("z", _z);
      }
   }
}