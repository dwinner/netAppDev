using System;

namespace FunctionComposition;

/// <summary>
///    A box can hold 1 thing only
/// </summary>
/// <typeparam name="T"></typeparam>
public class Box<T>
{
   private T _extract;

   public bool IsEmpty = true;

   public Box(T newExtract)
   {
      Extract = newExtract;
      IsEmpty = false;
   }

   public Box()
   {
   }

   public T Extract
   {
      get => _extract;
      set
      {
         _extract = value;
         IsEmpty = false;
      }
   }

   public override string ToString()
      => !IsEmpty ? $"{_extract}" : "The box is empy";
}

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
      var extracted = box.Extract;

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
      var extract = box.Extract;

      // Transform and LiftTo
      var liftedResult = bind(extract);

      if (liftedResult.IsEmpty)
      {
         return new Box<TC>();
      }

      // Project/Combine
      var t2 = project(extract, liftedResult.Extract);
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
      var extract = box.Extract;

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
      var extract = box.Extract;

      // Transform
      var transformed = select(extract);

      // Lift
      return new Box<TB>(transformed);
   }
}