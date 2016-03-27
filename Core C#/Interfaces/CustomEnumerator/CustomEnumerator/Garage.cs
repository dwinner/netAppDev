/*
 * Сделано в SharpDevelop.
 * Пользователь: Denis
 * Дата: 03.01.2013
 * Время: 8:17
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections;

namespace CustomEnumerator
{   
   /// <summary>
   /// Гараж содержит множество объектов Car
   /// </summary>
   public class Garage : IEnumerable
   {
      private Car[] carArray = new Car[4]
      {
         new Car("Rusty", 30),
         new Car("Clunker", 55),
         new Car("Zippy", 30),
         new Car("Fred", 30)
      };
      
      public Garage()
      {         
      }        
      
      public IEnumerator GetEnumerator()
      {
         // Вернем существующий метод массива
         return carArray.GetEnumerator();
      }
   }
}
