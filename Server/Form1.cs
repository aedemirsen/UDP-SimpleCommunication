using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        bool isRunning = false;
        string ip;
        int port;
        byte control = 0;
        Thread thdUDPServer;
        const int index = 5;
        const string openData = "O";
        const string closeData = "C";

        public Form1()
        {
            InitializeComponent();
            button2.BackColor = Color.Red;
        }

        public void serverThread()
        {
            UdpClient udpClient = new UdpClient(port);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse("195.175.1.1"), 0);
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                string handledData = getData(returnData,index);
                if (handledData == openData)
                {
                    button2.BackColor = Color.Green;
                }
                else if (handledData == closeData)
                {
                    button2.BackColor = Color.Red;
                }
            }
        }

        public string getData(string datagram,int index)
        {
            string data;
            data = datagram.ElementAt(index)+"";
            return data;
        }

        private void start(object sender, EventArgs e)
        {
            if (isRunning == false)
            {
                try
                {
                    port = int.Parse(textBox2.Text);
                }
                catch (Exception ex) { MessageBox.Show("Only numbers are allowed!");return; }
                if (control == 0)
                {                    
                    if (textBox2.Text != "")
                    {
                        control = 1;                        
                        thdUDPServer = new Thread(new
                        ThreadStart(serverThread));
                        thdUDPServer.Start();
                        button1.Text = "Stop Server";
                        label2.Text = "Server is running...";
                        isRunning = true;
                    }
                    else
                    {
                        MessageBox.Show("Enter a port number!");
                        return;
                    }              
                }
                else 
                {
                    thdUDPServer.Resume();
                    button1.Text = "Stop Server";
                    label2.Text = "Server is running...";
                    isRunning = true;
                }
            }            
            else
            {
                button1.Text = "Start Server";
                label2.Text = "Server is not running...";
                isRunning = false;
                thdUDPServer.Suspend();                
            }
            
            
            
        }
    }
}
