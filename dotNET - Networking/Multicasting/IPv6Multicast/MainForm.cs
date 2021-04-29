using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace IPv6Multicast
{
    public partial class MainForm : Form
    {
        private readonly IPAddress _multicastAddress = IPAddress.Parse("ff08::1");
        private const int MulticastPort = 65535;
        private readonly Socket _socket;

        public MainForm()
        {
            InitializeComponent();

            _socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.IPv6Any, MulticastPort));
            _socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership,
                new IPv6MulticastOption(_multicastAddress));
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateColor();
            bwReceive.RunWorkerAsync();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership,
                new IPv6MulticastOption(_multicastAddress));
        }


        private void chb_Click(object sender, EventArgs e)
        {
            _socket.SendTo(new[] {(byte) (chb.Checked ? 'F' : 'T')}, new IPEndPoint(_multicastAddress, MulticastPort));
        }

        private void chb_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            chb.BackColor = chb.Checked ? Color.LightGreen : Color.LightPink;
            chb.Text = chb.Checked ? "ON" : "OFF";
        }

        private void bwReceive_DoWork(object sender, DoWorkEventArgs e)
        {
            EndPoint remoteEp = new IPEndPoint(IPAddress.IPv6Any, 0);
            while (true)
            {
                var buffer = new byte[4096];
                _socket.ReceiveFrom(buffer, ref remoteEp);
                if (buffer[0] == (byte) 'F')
                {
                    bwReceive.ReportProgress(0);
                }
                else if (buffer[0] == (byte) 'T')
                {
                    bwReceive.ReportProgress(1);
                }
            }
        }

        private void bwReceive_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            chb.Checked = e.ProgressPercentage != 0;
        }
    }
}