/*
 * Copyright 2010 Chaur Wu.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Scripting.Hosting;

namespace HelloConsole
{
public partial class MainPage : UserControl
{
    private ScriptEngine scriptEngine;

    public MainPage()
    {
        InitializeComponent();
            
        LanguageSetup langSetup = new LanguageSetup(
            typeName: "HelloLanguage.HelloContext,HelloLanguage, Version=1.0.0.0, Culture=neutral",
            displayName: "Hello",
            names: new String[] { "Hello" },
            fileExtensions: new String[] { ".hello" });

        ScriptRuntimeSetup setup = new ScriptRuntimeSetup();
        setup.LanguageSetups.Add(langSetup);
        ScriptRuntime scriptRuntime = new ScriptRuntime(setup);
        scriptEngine = scriptRuntime.GetEngine("Hello");
            
    }

    private void TextBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            int index = helloConsole.Text.LastIndexOf(">>>");
            String code = helloConsole.Text.Substring(index + 3);
            String result = scriptEngine.Execute(code);
            helloConsole.Text += result + "\n>>>";
            helloConsole.SelectionStart = helloConsole.Text.Length;
        }
    }
}
}
