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
using NUnit.Framework;

namespace chapter16 {
  public class MutableProduct {
    public string Name { get; set; }
    public decimal Price { get; set; }
  }

  public class MutableOrderLine {
    public MutableProduct Product { get; set; }
    public int Count { get; set; }

    public decimal GetValue( ) {
      return Product.Price * Count;
    }
  }

  [TestFixture]
  public class EphemeralTests {
    [Test]
    public void TestOrderLineValue( ) {
      var product = new MutableProduct { Name = "Rubber boat", Price = 16.99m };
      var line = new MutableOrderLine { Product = product, Count = 3 };
      Assert.AreEqual(50.97, line.GetValue( ));
    }
  }
}
