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
using System.Threading;


namespace TrayApplicationTest
{
    public partial class LoginForm : Form
    {
        private string mail = null;
        private string password = null;
       
        NotificationOptions notifOptionForm;
        TakeCareOfAPlant takeCarePlantForm;
        DesktopNotification deskNot;

        UserNotificationOptions userNO;

        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;
        ServerConnectivity serverConnectivity = new ServerConnectivity();

        public string getMail() { return this.mail; }
        public string getPassword() { return this.password; }

        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(Properties.Settings.Default.serverIP));

            serviceProvider = netTcpFactory.CreateChannel();
        }

        public LoginForm()
        {
            ServerConnect();
            InitializeComponent();
            if (deskNot != null)
                deskNot.printDesktopNotifications();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p = null;
            string str = null;
            if (Properties.Settings.Default.username != "")
            {

                p = Properties.Settings.Default.password;
                str = serviceProvider.logIn(Properties.Settings.Default.username, p);
                this.mail = Properties.Settings.Default.username;
                this.password = p;
            }
            else
            {
                p = SHA1Util.SHA1HashStringForUTF8String(textBox2.Text);
                str = serviceProvider.logIn(textBox1.Text, p);
                this.mail = textBox1.Text;
                this.password = p;
            }
           
            var serializer = new XmlSerializer(typeof(UserNotificationOptions));
            if (str != "fail")
            {
                using (TextReader reader = new StringReader(str))
                {
                    userNO = (UserNotificationOptions)serializer.Deserialize(reader);
                }             
                notifyIcon2.Visible = true;
                this.notifyIcon2.BalloonTipText = "Application has been minized to tray.";
                this.notifyIcon2.ShowBalloonTip(1000);
                netTcpFactory.Close();
                
                this.Hide();

                if (checkBox1.Checked && textBox1.Text !="" && textBox2.Text !="$$$$")
                {
                    Properties.Settings.Default.username = textBox1.Text;
                    Properties.Settings.Default.password = p;
                    Properties.Settings.Default.Save();
                }

                deskNot = new DesktopNotification(mail, password, notifyIcon2);           
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
            netTcpFactory.Close();
            takeCarePlantForm = new TakeCareOfAPlant(mail, password);
            takeCarePlantForm.Visible = true;
            if (deskNot != null)
                deskNot.printDesktopNotifications();
        }

        private void notificationOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netTcpFactory.Close();
            notifOptionForm = new NotificationOptions(mail, password);
            notifOptionForm.Visible = true;
            if (deskNot != null)
                deskNot.printDesktopNotifications();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.username != "" && Properties.Settings.Default.password != "")
            {
                textBox1.Text = Properties.Settings.Default.username;
                textBox2.Text = "$$$$";
                checkBox1.Checked = true;
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon2.Visible = false;
            Properties.Settings.Default.username = "";
            Properties.Settings.Default.password = "";
            Properties.Settings.Default.Save();

            this.Close();
            if (takeCarePlantForm != null && takeCarePlantForm.Visible == true)
                takeCarePlantForm.Dispose();
            if (notifOptionForm != null && notifOptionForm.Visible == true)
                notifOptionForm.Dispose();           
           deskNot.StopTimer();
           deskNot.Dispose();
           netTcpFactory.Close();
           this.Dispose();
        }

        private void LoginForm_FormClosed(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Visible = true;
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
        }
    }
}
