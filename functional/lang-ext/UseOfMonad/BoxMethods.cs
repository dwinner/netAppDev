namespace UseOfMonad;

public static class BoxMethods
{
   /// <summary>
   ///    Transforms the contents of a Box, in a user defined way
   /// </summary>
   /// <typeparam name="TA">The type of the thing in the box to start with</typeparam>
   /// <typeparam name="TB">The result type that the transforming function to transform to</typeparam>
   /// <param name="box">The Box that the extension method will work on</param>
   /// <param name="map">User defined way to transform the contents of the box</param>
   /// <returns>The results of the transformation, put back into a box</returns>
   public static Box<TB> Select<TA, TB>(this Box<TA> box, Func<TA, TB> map)
   {
      // Validate/Check if box is valid(not empty) and if so, run the transformation function on it, otherwise don't
      if (box.IsEmpty)
      {
         // No, return the empty box
         return new Box<TB>();
      }

      // Extract the item from the Box and run the provided transform function ie run the map() function
      // ie map is the name of the transformation function the user provided.
      var transformedItem = map(box.Item);

      return new Box<TB>(transformedItem);
   }
}