namespace OCP_Sample;

public class Product(string name, Color color, Size size)
{
   public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
   public Color Color { get; } = color;
   public Size Size { get; } = size;
}