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

namespace PreferAnonymousMethods {
  class Program {
    static void Main(string[] args) {
      var calc = new PrimeCalculatorImperative( );
      var primes = calc.GetPrimes(11);
      Output(primes);

      var primesF = PrimeCalculatorFunctional.GetPrimes( ).TakeWhile(
        v => v <= 11);
      Output(primesF);
    }

    private static void Output(IEnumerable<int> primes) {
      foreach (int prime in primes) {
        Console.WriteLine(prime);
      }
    }
  }

  public class PrimeCalculatorImperative {
    public bool IsPrime(int v) {
      for (int d = 2; d <= v / 2; d++)
        if (v % d == 0)
          return false;
      return true;
    }

    public List<int> GetPrimes(int max) {
      var result = new List<int>( );
      for (int v = 3; v <= max; v++) {
        if (IsPrime(v))
          result.Add(v);
      }
      return result;
    }
  }

  public static class PrimeCalculatorFunctional {
    public static IEnumerable<int> GetPrimes( ) {
      Func<int, bool> isPrime = v => {
        for (int d = 2; d <= v / 2; d++)
          if (v % d == 0)
            return false;
        return true;
      };

      for (int v = 3; true; v++)
        if (isPrime(v))
          yield return v;
    }
  }
}
