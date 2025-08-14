﻿namespace UseOfMethods_1;

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
   ///    Validate, Extract, Transform, Project(Transform, Extract) and automatic Lift
   /// </summary>
   public static Box<TC> SelectMany<TA, TB, TC>(this Box<TA> box, Func<TA, Box<TB>> bind /*liftTo*/,
      Func<TA, TB, TC> project)
   {
      // Validate
      if (box.IsEmpty)
      {
         return new Box<TC>();
      }

      // Extract
      var extract = box.Item;

      // Transform and LiftTo
      var liftedResult = bind(extract);

      if (liftedResult.IsEmpty)
      {
         return new Box<TC>();
      }

      // Project/Combine
      var t2 = project(extract, liftedResult.Item); // This forms the select xzy in the Linq Expression in the tutorial.
      return new Box<TC>(t2);
   }

   /// <summary>
   ///    Validate, Extract, Transform and Lift (If Valid)
   ///    Check/Validate then transform to T and lift into Box<T>
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