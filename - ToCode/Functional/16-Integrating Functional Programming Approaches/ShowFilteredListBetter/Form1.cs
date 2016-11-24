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
using System.Windows.Forms;

namespace ShowFilteredListBetter {
  public partial class Form1 : Form {
    public Form1( ) {
      InitializeComponent( );

      InitData();
    }

    List<Person> people;

    private void InitData( ) {
      people = new List<Person>{
        new Person { Name = "Anna" },
        new Person { Name = "Chris" },
        new Person { Name = "Willy" },
        new Person { Name = "Hugh" },
        new Person { Name = "Steve" },
        new Person { Name = "Betty" },
        new Person { Name = "Carla" },
        new Person { Name = "John" },
        new Person { Name = "Pete" },
        new Person { Name = "Susan" }
      };

      UpdateUI( );
    }

    private void UpdateUI( ) {
      DisplayListBox.DataSource = 
        GetFilteredList(GetFilterString( ));
    }

    string GetFilterString( ) {
      string value = FilterTextBox.Text;
      return String.IsNullOrEmpty(value) ?
        null : value;
    }

    private List<Person> GetFilteredList(string filter) {
      return filter == null ? people : 
        (from p in people
         where p.Name.Contains(filter)
         select p).ToList( );
    }

    private void FilterButton_Click(object sender, EventArgs e) {
      UpdateUI( );
    }
  }
}