using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Collections;
using System.ComponentModel;


namespace GenericsAndXamlTypeResolver
{
    /// Interaction logic for Window1.xaml

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();

        }

        public void OnLoaded(object sender, EventArgs arts)
        {
        }
    }


    // Test class

    public class MyGenericClass<T1,T2>
    {
        private T1 _prop1;
        public T1 Prop1
        {
            get { return _prop1; }
            set { _prop1 = value; }
        }

        private T2 _prop2;
        public T2 Prop2
        {
            get { return _prop2; }
            set { _prop2 = value; }
        }
    }

}
