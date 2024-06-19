using System.Text;

namespace GeometricShapes;

public class GraphicObject
{
   private readonly Lazy<List<GraphicObject>> _children = new();

   public string? Color { get; init; }

   public virtual string Name { get; set; } = "Group";
   
   public List<GraphicObject> Children => _children.Value;

   private void Print(StringBuilder sb, int depth)
   {
      sb.Append(new string('*', depth))
         .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
         .AppendLine($"{Name}");
      foreach (var child in Children)
      {
         child.Print(sb, depth + 1);
      }
   }

   public override string ToString()
   {
      var stringBuilder = new StringBuilder();
      Print(stringBuilder, 0);
      return stringBuilder.ToString();
   }
}