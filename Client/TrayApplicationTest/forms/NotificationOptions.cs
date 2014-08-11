using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;
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
    public partial class NotificationOptions : KryptonForm
    {
        private string mail;
        private string password;

        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;

        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(Properties.Settings.Default.serverIP));

            serviceProvider = netTcpFactory.CreateChannel();
        }
        public NotificationOptions(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
            ServerConnect();
            InitializeComponent();
        }

        private void NotificationOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            setNotificationOptions();
            netTcpFactory.Close();
        }

        private void NotificationOptions_Load(object sender, EventArgs e)
        {         
            string str = serviceProvider.logIn(mail, password);           
            var serializer = new XmlSerializer(typeof(UserNotificationOptions));
            UserNotificationOptions userNot=null;
            if (str != "fail")
            {
                using (TextReader reader = new StringReader(str))
                {
                    userNot = (UserNotificationOptions)serializer.Deserialize(reader);
                }
            }
            if (userNot.IFrom.ToString() == "00:00:01" && userNot.ITo.ToString() == "23:59:59")
            {
                kryptonCheckBox1.Checked = true;
                kryptonComboBox2.Enabled = false;
                kryptonComboBox1.Enabled = false;
                string inter = userNot.Interval.ToString();
                kryptonComboBox3.SelectedText = inter;
                bool mailT = userNot.MailToggle;
                kryptonCheckBox3.Checked = mailT;
                bool desktopT = userNot.DesktopToggle;
                kryptonCheckBox2.Checked = desktopT;
            }
            else
            {
                string from = userNot.IFrom.Hours + ":" + userNot.IFrom.Minutes + "0";
                kryptonComboBox1.SelectedText = from;
                string to = userNot.ITo.Hours + ":" + userNot.ITo.Minutes + "0";
                kryptonComboBox2.SelectedText = to;
                string inter = userNot.Interval.ToString();
                kryptonComboBox3.SelectedText = inter;
                bool mailT = userNot.MailToggle;
                kryptonCheckBox3.Checked = mailT;
                bool desktopT = userNot.DesktopToggle;
                kryptonCheckBox2.Checked = desktopT;
            }
            kryptonCheckBox4.Checked = Properties.Settings.Default.openWithWindows;
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonCheckBox1.Checked == true)
            {
                kryptonComboBox2.Enabled = false;
                kryptonComboBox1.Enabled = false;
            }
            if (kryptonCheckBox1.Checked == false)
            {
                kryptonComboBox2.Enabled = true;      
                kryptonComboBox1.Enabled = true;
            }          
        }

        private void kryptonCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            serviceProvider.toggleDesktopNotifications(mail, kryptonCheckBox2.Checked);
        }

        private void kryptonCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            serviceProvider.toggleMailNotifications(mail, kryptonCheckBox3.Checked);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //serviceProvider.functionForOpenWithWindows
        }

        public void enableOpenWithWindows()
        {
            Microsoft.Win32.RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rkApp.SetValue("LastDropClient", Application.ExecutablePath.ToString());
        }

        public void disableOpenWithWindows()
        {
            Microsoft.Win32.RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rkApp.SetValue("LastDropClient", Application.ExecutablePath.ToString());
            rkApp.DeleteValue("LastDropClient");
        }

        public void setNotificationOptions()
        {
            TimeSpan fromTS = new TimeSpan(0, 0, 1);
            TimeSpan toTS = new TimeSpan(23, 59, 59);
            int interval = 0;
            if (kryptonCheckBox1.Checked == true)
            {
                TimeSpan fromAll = new TimeSpan(0, 0, 1);
                TimeSpan toAll = new TimeSpan(23, 59, 59);

                string intervalS = kryptonComboBox3.Text;
                interval = Int32.Parse(intervalS);

                serviceProvider.setNotificationIntervals(mail, fromAll, toAll, interval);
            }
            if (kryptonCheckBox1.Checked == false)
            {
                if (kryptonComboBox1.Text == "")
                {
                    kryptonComboBox1.Text = "9:00";
                }
                if (kryptonComboBox2.Text == "")
                {
                    kryptonComboBox2.Text = "23:00";
                }
                string fromS = kryptonComboBox1.Text;
                string[] fromA = fromS.Split(':');
                int fromI = Int32.Parse(fromA[0]);
                fromTS = new TimeSpan(fromI, 0, 0);

                string toS = kryptonComboBox2.Text;
                string[] toA = toS.Split(':');
                int toI = Int32.Parse(toA[0]);
                toTS = new TimeSpan(toI, 0, 0);

                if (toI == 24)
                    toTS = new TimeSpan(23, 59, 59);

                string intervalS = kryptonComboBox3.Text;
                interval = Int32.Parse(intervalS);
            }
            serviceProvider.setNotificationIntervals(mail, fromTS, toTS, interval);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            setNotificationOptions();
            Properties.Settings.Default.openWithWindows = kryptonCheckBox4.Checked;
            Properties.Settings.Default.Save();

            if (Properties.Settings.Default.openWithWindows)
                enableOpenWithWindows();
            else disableOpenWithWindows();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show("        By selecting 'Desktop Notification' option, messages will appear above the taskbar saying which plants need to be watered. \n"+
                "       By selecting 'Mail Notification' option, a mail will be sent to the address used for registration saying which plants need to be watered. \n"+
                "       By selecting 'All day Notification' option, Desktop or Mail notifications will arrive when a plant needs to be watered throughout the day. \n"+
                "       There is the option of selecting custom hours for when the notifications will arrive, and it also allows inputing a time interval on which the notifications will reappear. \n"+
                "       The 'Open with Windows' option will start the application when the computer powers on if it is selected.");
           
        }
    }
}

