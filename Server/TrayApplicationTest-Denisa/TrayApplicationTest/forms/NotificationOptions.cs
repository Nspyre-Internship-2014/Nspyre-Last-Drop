using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayApplicationTest
{
    public partial class NotificationOptions : Form
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
       

        public NotificationOptions()
        {
            ServerConnect();
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void NotificationOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.checkBox = checkBox1.Checked;
            Properties.Settings.Default.comboBox1 = comboBox1.Text;
            Properties.Settings.Default.comboBox2 = comboBox2.Text;
            Properties.Settings.Default.comboBox3 = comboBox3.Text;
            Properties.Settings.Default.desktopNotification = checkBox2.Checked;
            Properties.Settings.Default.openWithWindows = checkBox4.Checked;
            Properties.Settings.Default.mailNotification = checkBox3.Checked; 
            Properties.Settings.Default.Save();
            netTcpFactory.Close();
        }

        private void NotificationOptions_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.checkBox;
            comboBox1.Text = Properties.Settings.Default.comboBox1;
            comboBox2.Text = Properties.Settings.Default.comboBox2;
            comboBox3.Text = Properties.Settings.Default.comboBox3;
            checkBox2.Checked = Properties.Settings.Default.desktopNotification;
            checkBox3.Checked = Properties.Settings.Default.mailNotification;
            checkBox4.Checked = Properties.Settings.Default.openWithWindows;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox2.Enabled = false;
                comboBox1.Enabled = false;
            }

            if (checkBox1.Checked == false)
            {
                comboBox2.Enabled = true;
                comboBox1.Enabled = true;
            }

        }
    }
}
