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

namespace FunctionalMandelbrotParallel {
  public class CalcInfo {
    public CalcInfo(double xStart, double xStep, double yStart, double yStep) {
      XStart = xStart;
      XStep = xStep;
      YStart = yStart;
      YStep = yStep;
    }
    public double XStart { get; private set; }
    public double XStep { get; private set; }
    public double YStart { get; private set; }
    public double YStep { get; private set; }
  }
}
