using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TrayApplicationTest
{
    public partial class RegisterForm : Form
    {
        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;

        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(
                  "net.tcp://10.33.92.62:8021/Service1/LastDropService"));

            serviceProvider = netTcpFactory.CreateChannel();
        }     
       
        public RegisterForm()
        {
            ServerConnect();
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            netTcpFactory.Close();
        }

        public void resetForm()
        {
            textBox2.Text = "";
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           bool result=serviceProvider.registerUser(textBox1.Text, textBox2.Text);

           if (result == true)
           {
               this.Hide();
               LoginForm loginForm = new LoginForm();
               loginForm.Visible = true;
           }
           if (result == false)
           {
               MessageBox.Show("The registration failed !");
               resetForm();
           }
          
        }

        
    }
}
