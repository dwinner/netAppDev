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


public class CountryInfo {
	public CountryInfo(string name, int areaKM2, int population) {
		this.Name = name;
		this.AreaKM2 = areaKM2;
		this.Population = population;
	}
  public string Name { get; set; }
  public int AreaKM2 { get; set; }
  public int Population { get; set; }

	public override string ToString( ) {
		return String.Format("Country: {0}, Area km^2: {1}, Population: {2}", 
			Name, AreaKM2, Population);
	}
}
