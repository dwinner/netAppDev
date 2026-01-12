namespace UseOfPipelines;

public static class BoxMethods
{
   /// <summary>
   ///    Validate, Extract, Transform and Lift (If Valid)
   /// </summary>
   public static Box<TB> Select<TA, TB>(this Box<TA> box, Func<TA, TB> map)
   {
      // Validate
      if (box.IsEmpty)
      {
         return new Box<TB>();
      }

      // Extract
      var extracted = box.Item;

      // Transform
      var transformedItem = map(extracted);

      // Lift
      return new Box<TB>(transformedItem);
   }

   /// <summary>
   ///    Validate, Extract, Transform and Lift (If Valid)
   ///    Check/Validate then transform to T and lift into BoxT
   /// </summary>
   public static Box<TB> Bind<TA, TB>(this Box<TA> box, Func<TA, Box<TB>> bind /*liftAndTransform*/)
   {
      // Validate
      if (box.IsEmpty)
      {
         return new Box<TB>();
      }

      //Extract 
      var extract = box.Item;

      // Transform and lift
      var transformedAndLifted = bind(extract);

      return transformedAndLifted;
   }

   /// <summary>
   ///    Validate, Extract, Transform and automatic Lift (If Valid)
   /// </summary>
   public static Box<TB> Map<TA, TB>(this Box<TA> box, Func<TA, TB> select /*Transform*/)
   {
      // Validate
      if (box.IsEmpty)
      {
         return new Box<TB>();
      }

      // Extract
      var extract = box.Item;

      // Transform
      var transformed = select(extract);

      // Lift
      return new Box<TB>(transformed);
   }
}