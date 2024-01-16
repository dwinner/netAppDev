// To string:

var bSide = BorderSides.Right.ToString();
var formatted = Enum.Format(typeof(BorderSides), BorderSides.Right, "g");

// From string:
var leftRight = (BorderSides)Enum.Parse(typeof(BorderSides), "Left, Right");
Console.WriteLine(leftRight);

var leftRightCaseInsensitive = (BorderSides)
   Enum.Parse(typeof(BorderSides), "left, right", true);

Console.WriteLine(leftRightCaseInsensitive);

[Flags]
public enum BorderSides
{
   Left = 1,
   Right = 2,
   Top = 4,
   Bottom = 8
}