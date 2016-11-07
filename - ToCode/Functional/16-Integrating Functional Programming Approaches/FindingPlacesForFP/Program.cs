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

namespace FindingPlacesForFP {
  class Program {
    static void Main(string[] args) {
    }
  }

  public enum Position {
    Worker,
    Suit,
    BigBoss
  }

  public class Customer {
    public string Name { get; set; }
    public Position Position { get; set; }
  }

  public static class CustomerLogic {
    public static void Promote(this Customer customer) {
      switch (customer.Position) {
        case  Position.Worker:
          customer.Position = Position.Suit;
          break;
        case Position.Suit:
          customer.Position = Position.BigBoss;
          break;
      }
    }

    public static Customer PromotePure(this Customer customer) {
      switch (customer.Position) {
        case Position.Worker:
          return new Customer { Name = customer.Name, Position = Position.Suit };
        case Position.Suit:
          return new Customer { Name = customer.Name, Position = Position.BigBoss };
      }
      return customer;
    }

  }
}
