using System;

namespace StructsSample;

public readonly struct Dimensions(double length, double width)
{
   public double Length { get; } = length;
   public double Width { get; } = width;

   public double Diagonal => Math.Sqrt(Length * Length + Width * Width);

   public override string ToString() => $"Length: {Length}, Width: {Width}";
}