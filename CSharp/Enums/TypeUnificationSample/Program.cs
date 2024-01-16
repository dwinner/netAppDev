Display(Nut.Macadamia); // Nut.Macadamia
Display(Size.Large); // Size.Large

return;

void Display(Enum value) // The Enum type unifies all enums
{
   Console.WriteLine($"{value.GetType().Name}.{value}");
}

internal enum Nut
{
   Walnut,
   Hazelnut,
   Macadamia
}

internal enum Size
{
   Small,
   Medium,
   Large
}