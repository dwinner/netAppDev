using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Programs_List
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnGetInstalledPrograms(object sender, EventArgs e)
        {
            const string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (var rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (var skName in rk.GetSubKeyNames())
                {
                    using (var sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            var displayName = sk.GetValue("DisplayName");
                            var size = sk.GetValue("EstimatedSize");
                            if (displayName != null)
                            {
                                ListViewItem item;
                                if (size != null)
                                {
                                    item = new ListViewItem(new[]
                                    {
                                        displayName.ToString(),
                                        size.ToString()
                                    });
                                }
                                else
                                {
                                    item = new ListViewItem(new[] { displayName.ToString() });
                                }

                                lstDisplayHardware.Items.Add(item);
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }

                label1.Text += $" ({lstDisplayHardware.Items.Count})";
            }
        }
    }
}