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
using ComponentFactory.Krypton.Toolkit;


namespace TrayApplicationTest
{
    public partial class LoginForm : KryptonForm
    {
        private string mail = null;
        private string password = null;
        private string p = null;
        private string str = null;
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
                kryptonCheckBox2.Checked = true;
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

        public void setCredentials(string password)
        {           
            p = password;
            try
            {
                str = serviceProvider.logIn(textBox1.Text, p);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Server unavailable. Try again later.");
                this.Dispose();
                System.Windows.Forms.Application.Exit();
                Application.Exit();
                System.Environment.Exit(1);
            }
            this.mail = textBox1.Text;
            this.password = p;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {     
            if (Properties.Settings.Default.username != "")
            {
                if (Properties.Settings.Default.username != textBox1.Text)
                {
                    setCredentials(SHA1Util.SHA1HashStringForUTF8String(textBox2.Text));                   
                }
                else
                {
                    setCredentials(Properties.Settings.Default.password);                   
                }
            }
            else
            {
                setCredentials(SHA1Util.SHA1HashStringForUTF8String(textBox2.Text));         
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

                if (kryptonCheckBox2.Checked && textBox1.Text != "" && textBox2.Text != "$$$$")
                {
                    Properties.Settings.Default.username = textBox1.Text;
                    Properties.Settings.Default.password = p;
                    Properties.Settings.Default.Save();
                }

                deskNot = new DesktopNotification(mail, password, notifyIcon2);
            }
            if (str == "fail")
            {
                KryptonMessageBox.Show("Mail or password are incorrect !");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

    }
}
