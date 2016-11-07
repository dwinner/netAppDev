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

namespace ShowFilteredListUgly {
  public partial class Form1 : Form {
    public Form1( ) {
      InitializeComponent( );

      InitData( );
      UpdateUI( );
    }

    Person[] people;
    const int maxPeople = 10;
    Person[] displayPeople;
    int displayCount;

    private void InitData( ) {
      people = new Person[maxPeople];
      people[0] = new Person { Name = "Anna" };
      people[1] = new Person { Name = "Chris" };
      people[2] = new Person { Name = "Willy" };
      people[3] = new Person { Name = "Hugh" };
      people[4] = new Person { Name = "Steve" };
      people[5] = new Person { Name = "Betty" };
      people[6] = new Person { Name = "Carla" };
      people[7] = new Person { Name = "John" };
      people[8] = new Person { Name = "Pete" };
      people[9] = new Person { Name = "Susan" };
      displayPeople = new Person[maxPeople];
      ResetDisplayPeople( );
    }

    private void ResetDisplayPeople( ) {
      displayCount = people.Length;
      for (int i = 0; i < displayCount; i++)
        displayPeople[i] = people[i];
    }

    private void UpdateUI( ) {
      DisplayListBox.Items.Clear( );
      for (int i = 0; i < displayCount; i++)
        DisplayListBox.Items.Add(displayPeople[i]);
    }

    private void button1_Click(object sender, EventArgs e) {
      string filter = FilterTextBox.Text;
      if (filter != null && filter != "")
        FilterData(filter);
      else
        ResetDisplayPeople( );
      UpdateUI( );
    }

    private void FilterData(string filter) {
      int filteredCount = 0;
      for (int i = 0; i < maxPeople; i++)
        if (people[i].Name.Contains(filter))
          displayPeople[filteredCount++] = people[i];
      displayCount = filteredCount;
    }
  }
}