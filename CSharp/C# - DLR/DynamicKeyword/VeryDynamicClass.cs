namespace DynamicKeyword
{
   class VeryDynamicClass
   {
      // Динамическое поле.
      private static dynamic _myDynamicField;

      // Динамическое свойство.
      public dynamic DynamicProperty { get; set; }

      // Динамическое возвращаемое значение и тип параметра.
      public dynamic DynamicMethod(dynamic dynamicParam)
      {
         // Динамическая локальная переменная.
         dynamic dynamicLocalVar = "Local variable";

         int myInt = 10;

         if (dynamicParam is int)
         {
            return dynamicLocalVar;
         }
         return myInt;
      }
   }
}
