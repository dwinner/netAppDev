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

namespace chapter17 {
  // This is what an IMonad interface might look like. Problem is, the implementation
  // isn't fixed like this in reality. Bind can be called something else, or be
  // implemented as an extension method. Return will often be replaced by a constructor.
  // So the interface is practically useless in this case.

  //public interface IMonad<A> {
  //  IMonad<A> Return(A val);
  //  IMonad<B> Bind<B>(Func<A, IMonad<B>> g);
  //}
}
