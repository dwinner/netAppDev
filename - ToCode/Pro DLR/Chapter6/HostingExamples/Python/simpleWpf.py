#
# Copyright 2010 Chaur Wu.
# 
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# 
#      http://www.apache.org/licenses/LICENSE-2.0
# 
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#

import clr
from System.Windows import (Application, Window)
from System.Windows.Controls import (Label, StackPanel)

window = Window()
window.Title = "Simple Python WPF Example"
window.Width = 400
window.Height = 300

stackPanel = StackPanel()
window.Content = stackPanel

helloLabel = Label()
helloLabel.Content = "Hello"
helloLabel.FontSize = 50
stackPanel.Children.Add(helloLabel)

app = Application()
app.Run(window)


