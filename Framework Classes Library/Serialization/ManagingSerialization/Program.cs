// Управление сериализацией

using System;
using System.Runtime.Serialization;
using static System.Guid;
using static System.Math;

namespace ManagingSerialization
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [Serializable]
   internal class Circle
   {
      private readonly double _radius;
      // NOTE: Если это новое поле, добавленное в класс, модуль форматирования не вбросит исключение SerializationException
      [OptionalField] private readonly Guid _serialVersionGuid = NewGuid();

      [NonSerialized] private double _area;

      public Circle(double radius)
      {
         _radius = radius;
         _area = PI*_radius*_radius;
      }

      #region Методы управления сериализацией

      [OnSerializing]
      private void OnSerializing(StreamingContext context)
      {
         // NOTE: Модификация состояния перед сериализацией
      }

      [OnSerialized]
      private void OnSerialized(StreamingContext context)
      {
         // NOTE: Восстановление любого состояния после сериализации
      }

      [OnDeserializing]
      private void OnDeserializing(StreamingContext context)
      {
         // NOTE: Присвоение полям значений по умолчанию в новой версии типа
      }

      [OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
         // NOTE: Инициализация временного состояния полей
         _area = PI*_radius*_radius;
      }

      #endregion
   }
}