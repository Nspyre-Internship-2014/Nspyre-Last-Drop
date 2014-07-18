using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;


namespace ClientSocketMultiThread
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream; 

        public Form1()
        {
            InitializeComponent();
        }
         private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("10.33.90.52", 8021);
            label1.Text = "Client Socket Program - Server Connected ...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
            byte[] outStream2 = System.Text.Encoding.ASCII.GetBytes("Ionica,password$");
            serverStream.Write(outStream2, 0, outStream2.Length);
            serverStream.Flush();

            byte[] inStream = new byte[1024];
            Int32 bytes=serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream,0,bytes);

            msg("Data from Server : " + returndata);
        }

        public void msg(string mesg)
        {
            textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + mesg;

        } 
    }
}
    
