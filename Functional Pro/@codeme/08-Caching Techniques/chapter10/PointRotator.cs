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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace chapter10 {
  public class PointRotator {
    public PointRotator( ) {
      InitLookups( );
    }

    /// <summary>
    /// This method rotates the given point around the origin (0,0) by the
    /// given angle. 
    /// </summary>
    static public PointF RotatePoint(PointF sourcePoint, double angle) {
      double polarLength = Math.Sqrt(Square(sourcePoint.X) + Square(sourcePoint.Y));
      double polarAngle = ToDeg(Math.Atan(sourcePoint.Y / sourcePoint.X));
      double newAngle = polarAngle + angle;
      double newAngleRad = ToRad(newAngle);
      return new PointF(
        (float) (polarLength * Math.Cos(newAngleRad)),
        (float) (polarLength * Math.Sin(newAngleRad)));
    }

    /// <summary>
    ///  This implementation uses lookup tables for the Sin and Cos
    ///  calls, and it rounds the angles to integers before doing so.
    ///  The purpose is of course not to demonstrate perfect lookup
    ///  techniques for complex cases like the ArcTan, or to implement
    ///  a flawless point rotation function ;-)
    ///  This implementation saves about a third in processing time,
    ///  compared to the one above.
    /// </summary>
    public PointF RotatePointWithLookups(PointF sourcePoint, double angle) {
      double polarLength = Math.Sqrt(Square(sourcePoint.X) + Square(sourcePoint.Y));
      double polarAngle = ToDeg(Math.Atan(sourcePoint.Y / sourcePoint.X));
      int newAngleInt = (int) Math.Round(polarAngle + angle);
      float cartesianX = (float) polarLength * Cos(newAngleInt);
      float cartesianY = (float) polarLength * Sin(newAngleInt);
      return new PointF(cartesianX, cartesianY);
    }

    private int NormalizeAngle(int angle) {
      return ((angle % 360) + 360) % 360;
    }

    private float Cos(int angle) {
      return cosines[NormalizeAngle(angle)];
    }

    private float Sin(int angle) {
      return sines[NormalizeAngle(angle)];
    }

    float[] sines, cosines;

    private void InitLookups( ) {
      sines = new float[360];
      cosines = new float[360];
      for (int i = 0; i < 360; i++) {
        sines[i] = (float) Math.Sin(ToRad(i));
        cosines[i] = (float) Math.Cos(ToRad(i));
      }
    }

    static double Square(double x) {
      return x * x;
    }

    static double ToRad(double deg) {
      return deg * Math.PI / 180.0;
    }

    static double ToDeg(double rad) {
      return rad / (Math.PI / 180.0);
    }
  }
}
