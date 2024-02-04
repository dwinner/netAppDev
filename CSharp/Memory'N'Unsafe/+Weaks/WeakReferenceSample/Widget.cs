namespace WeakReferenceSample;

internal class Widget
{
   private static readonly List<WeakReference> AllWidgets = new();

   public readonly string Name;

   public Widget(string name)
   {
      Name = name;
      AllWidgets.Add(new WeakReference(this));
   }

   public static void ListAllWidgets()
   {
      foreach (var weakTarget in
               AllWidgets
                  .Select(weak => (Widget?)weak.Target)
                  .Where(weakTarget => weakTarget != null))
      {
         Console.WriteLine(weakTarget?.Name);
      }
   }
}