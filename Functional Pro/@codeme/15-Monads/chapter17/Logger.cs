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
using System.Text;
using FCSColl = FCSlib.Data.Collections;

namespace chapter17 {
  public class Logger<T> {
    private readonly FCSColl::List<string> outputLines;

    private readonly T val;
    public T Value { get { return val; } }

    public Logger(T val, FCSColl::List<string> outputLines) {
      this.val = val;
      this.outputLines = outputLines;
    }

    public Logger(T val, string message) : this(val, new FCSColl::List<string>(message)) { }
    public Logger(T val) : this(val, FCSColl::List<string>.Empty) { }

    public string LogOutput( ) {
      var builder = new StringBuilder( );
      foreach (string outputLine in outputLines)
        builder.AppendLine(outputLine);
      return builder.ToString( );
    }

    public Logger<R> Bind<R>(Func<T, Logger<R>> g) {
      var r = g(val);
      return new Logger<R>(r.Value, outputLines.Append(r.outputLines));
    }
  }

}
