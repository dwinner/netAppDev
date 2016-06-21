namespace CustomLoggingSample.Aspects
{
   using System.Text;

   using Aop.Lib;

   using PostSharp.Aspects;
   using PostSharp.Serialization;

   /// <summary>
   ///    Аспект, примененный к методу или свойству, добавляет запись через класс <see cref="Logger" />,
   ///    когда поле или свойство устанавливается в новое значение
   /// </summary>
   [PSerializable]
   public sealed class LogSetValueAttribute : LocationInterceptionAspect
   {
      public override void OnSetValue(LocationInterceptionArgs args)
      {
         var stringBuilder = new StringBuilder();
         stringBuilder.Append("Setting ");
         stringBuilder.AppendTypeName(args.Location.DeclaringType);
         if (args.Index.Count != 0)
         {
            stringBuilder.AppendArguments(args.Index);
         }

         stringBuilder.Append(" = ").Append(args.Value);
         Logger.WriteLine(stringBuilder.ToString());

         base.OnSetValue(args);
      }
   }
}