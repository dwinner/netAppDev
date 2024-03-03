using System.Runtime.InteropServices;

MessageBox(IntPtr.Zero,
   "Please do not press this again.", "Attention", 0);
return;

[DllImport("user32.dll")]
static extern int MessageBox(IntPtr hWnd, string text, string caption, int type);