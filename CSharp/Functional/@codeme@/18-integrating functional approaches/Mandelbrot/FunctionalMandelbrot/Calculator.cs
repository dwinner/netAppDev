// Copyright (C) 2011 Oliver Sturm <oliver@oliversturm.com>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, see <http://www.gnu.org/licenses/>.
  


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using FCSlib;
using FCSlib.Data;

namespace FunctionalMandelbrot {
  public static class Calculator {
    private static int MAX_ITERATION = 1000;
    public static int MaxIteration {
      get {
        return MAX_ITERATION;
      }
      set
      {
      	MAX_ITERATION = value;
      }
    }
    private const int COLOR_COUNT = 10;

    static Calculator( ) {
      InitColors( );
    }

    private static void InitColors( ) {
      colors = new Color[COLOR_COUNT];
      colors[0] = Color.White;
      colors[1] = Color.FromArgb(0, 0, 25);
      colors[2] = Color.FromArgb(0, 0, 50);
      colors[3] = Color.FromArgb(0, 0, 75);
      colors[4] = Color.FromArgb(0, 0, 100);
      colors[5] = Color.FromArgb(0, 0, 125);
      colors[6] = Color.FromArgb(0, 0, 150);
      colors[7] = Color.FromArgb(0, 0, 175);
      colors[8] = Color.FromArgb(0, 0, 200);
      colors[9] = Color.FromArgb(0, 0, 255);
    }
    static Color[] colors;

    private static int Iterator(double x, double y) {
      double tx = 0, ty = 0;
      int iteration = 0;
      while ((tx * tx + ty * ty) < (2 * 2) &&
        iteration < MAX_ITERATION) {
        double ttx = tx * tx - ty * ty + x;
        ty = 2 * tx * ty + y;
        tx = ttx;
        iteration++;
      }
      return iteration;
    }

    private static Color ColorFromIterations(int iterations) {
      return iterations == MAX_ITERATION ?
        Color.Black : colors[iterations % COLOR_COUNT];
    }

    private static PointResult CalcPoint(Point point, CalcInfo calcInfo) {
      var iterations = Iterator(calcInfo.XStart + point.X * calcInfo.XStep,
        calcInfo.YStart + point.Y * calcInfo.YStep);
      return new PointResult(point, ColorFromIterations(iterations));
    }

    public static IEnumerable<Point> PointSequence(int width, int height) {
      for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
          yield return new Point(x, y);
    }

    public static IEnumerable<Point> PointSequenceUsingSequenceFunction(int width, int height) {
      return Functional.Sequence<Point>(
        p => 
          p.X < width - 1 ? 
            new Point(p.X + 1, p.Y) : 
            p.Y < height - 1 ? 
              new Point(0, p.Y + 1) : 
              Point.Empty,
        new Point(0, 0), 
        p => p.X == width - 1 && p.Y == height - 1);
    }

    //public static IEnumerable<Point> TheoreticalPointSequenceWithRecursion(int width, int height, int x, int y) { 
    //  if (x < width && y < height)
    //    yield return new Point(x, y);
    //  if (x < width - 1)
    //    return TheoreticalPointSequenceWithRecursion(width, height, x + 1, y);
    //  else if (x < height - 1)
    //    return TheoreticalPointSequenceWithRecursion(width, height, 0, y + 1);
    //}

    public static IEnumerable<PointResult> CalcArea(int width, int height, CalcInfo calcInfo) {
      var points = PointSequence(width, height);
      return Functional.Map(p => CalcPoint(p, calcInfo), points);
    }

    public static Image CalcImage(IEnumerable<PointResult> results, Point start,  int width, int height) {
      return Functional.FoldL<PointResult, Bitmap>(
        (r, v) => {
          r.SetPixel(v.Point.X, v.Point.Y, v.Color);
          return r;
        },
        new Bitmap(width, height),
        results);
    }
  }
}
