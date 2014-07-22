using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.ServiceModel;

namespace TrayApplicationTest
{
    public partial class LoginForm : Form
 {
      DuplexChannelFactory<IServicesWCF> netTcpFactory;
      IServicesWCF serviceProvider;
      ServerConnectivity serverConnectivity = new ServerConnectivity();
       
      public void ServerConnect() 
      { 
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();
            
            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(
                  "net.tcp://10.33.92.62:8021/Service1/LastDropService"));

            serviceProvider = netTcpFactory.CreateChannel();   
        }     
       
        public LoginForm()
        {
            ServerConnect();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string str = serviceProvider.logIn(textBox1.Text, textBox2.Text);
             
           
            var serializer = new XmlSerializer(typeof(UserNotificationOptions));
            UserNotificationOptions userNot;
            if (str != "fail")
            {
                using (TextReader reader = new StringReader(str))
                {
                    userNot = (UserNotificationOptions)serializer.Deserialize(reader);
                }

                notifyIcon2.Visible = true;
                this.Hide();
                this.notifyIcon2.BalloonTipText = "Application has been minized to tray.";
                this.notifyIcon2.ShowBalloonTip(1000);
            }
            if (str == "fail")
            {
                MessageBox.Show("Mail or password are incorrect !");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            
        }
        private void notifyIcon2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon2.Visible = true; 
            
        }


        private void takeCarePlantMenuStrip_click(object sender, EventArgs e)
        {
            this.notifyIcon2.BalloonTipText = "Take care of plant has been clicked.";
            this.notifyIcon2.ShowBalloonTip(1000);
            netTcpFactory.Close();
            TakeCareOfAPlant takeCarePlantForm = new TakeCareOfAPlant(textBox1.Text,textBox2.Text);
            takeCarePlantForm.Visible = true;
            
        }

        private void NotificationOptionsMenuItem_click(object sender, EventArgs e)
        {    
        }

        private void notificationOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon2.BalloonTipText = "Notification options has been clicked.";
            this.notifyIcon2.ShowBalloonTip(1000);
            netTcpFactory.Close();
            NotificationOptions notifOptionForm = new NotificationOptions();
            notifOptionForm.Visible = true;
        }





    }
}
