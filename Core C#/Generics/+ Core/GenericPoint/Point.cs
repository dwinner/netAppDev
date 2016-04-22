namespace GenericPoint
{
   /// <summary>
   /// Обобщенная структура точки на плоскости
   /// </summary>
   /// <typeparam name="T">Параметр типа точки</typeparam>
   public struct Point<T>
   {      
      private T _xPos;
      private T _yPos;

      // Обобщенный конструктор
      public Point(T xVal, T yVal)
      {
         _xPos = xVal;
         _yPos = yVal;
      }

      #region Обобщенные свойства

      public T X
      {
         get { return _xPos; }
         set { _xPos = value; }
      }

      public T Y
      {
         get { return _yPos; }
         set { _yPos = value; }
      }

      #endregion


      public override string ToString()
      {
         return string.Format("[{0}, {1}]", _xPos, _yPos);
      }
      
      // Сброс полей к значениям по умолчанию для параметра типа
      public void ResetPoint()
      {
         _xPos = default(T);
         _yPos = default(T);
      }
   }
}
