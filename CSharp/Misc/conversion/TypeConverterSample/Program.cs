// Type converters are designed to format and parse in design-time environments.

using System.ComponentModel;
using System.Drawing;

var colorConverter = TypeDescriptor.GetConverter(typeof(Color));

var defColor = default(Color);
var beige = (Color)(colorConverter.ConvertFromString("Beige") ?? defColor);
var purple = (Color)(colorConverter.ConvertFromString("#800080") ?? defColor);
var window = (Color)(colorConverter.ConvertFromString("Window") ?? defColor);

Console.WriteLine(beige);
Console.WriteLine(purple);
Console.WriteLine(window);