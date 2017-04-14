using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        UdpClient udpClient;
        
        private string openKey = "qRNhgOv56SDty";
        private string closeKey = "qRNhgCv56SDty";
        bool connected = false;
        public Form1()
        {
            InitializeComponent();
            label4.Text = "Not Connected";
            textBox1.Text = "localhost";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                udpClient = new UdpClient();
                udpClient.Connect(textBox1.Text, int.Parse(textBox3.Text));
                connected = true;
                label4.Text = "Connected";
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            udpClient.Close();
            connected = false;
            label4.Text = "Not Connected";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                Byte[] senddata = Encoding.ASCII.GetBytes(openKey);
                udpClient.Send(senddata, senddata.Length);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                Byte[] senddata = Encoding.ASCII.GetBytes(closeKey);
                udpClient.Send(senddata, senddata.Length);
            }
        }
    }
}
