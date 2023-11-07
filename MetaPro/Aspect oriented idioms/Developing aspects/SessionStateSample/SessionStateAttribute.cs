namespace SessionStateSample
{
   using System.Web;

   using PostSharp.Aspects;
   using PostSharp.Reflection;
   using PostSharp.Serialization;

   /// <summary>
   ///    Aspect that, when applied to a field or property, causes this field or property to be persisted in the session
   ///    state.
   /// </summary>
   [PSerializable]
   [LinesOfCodeAvoided(3)]
   public sealed class SessionStateAttribute : LocationInterceptionAspect
   {
      private string _name;

      /// <summary>
      ///    Method invoked at build time.
      /// </summary>
      /// <param _name="targetLocation">Field or property to which the aspect has been applied.</param>
      /// <param _name="aspectInfo">Ignored.</param>
      /// <param name="targetLocation"></param>
      /// <param name="aspectInfo"></param>
      public override void CompileTimeInitialize(LocationInfo targetLocation, AspectInfo aspectInfo)
      {
         // Set the _name of the session item.
         _name = $"{targetLocation.DeclaringType.FullName}.{targetLocation.Name}";
      }

      /// <summary>
      ///    Method invoked when (instead of) the target field or property is retrieved.
      /// </summary>
      /// <param _name="args">Context information.</param>
      /// <param name="args"></param>
      public override void OnGetValue(LocationInterceptionArgs args)
      {
         object value = HttpContext.Current.Session[_name];
         if (value != null)
         {
            args.Value = value;
         }
      }

      /// <summary>
      ///    Method invoked when (instead of) the target field or property is set to a new value.
      /// </summary>
      /// <param _name="args">Context information.</param>
      /// <param name="args"></param>
      public override void OnSetValue(LocationInterceptionArgs args)
      {
         HttpContext.Current.Session[_name] = args.Value;
      }
   }
}