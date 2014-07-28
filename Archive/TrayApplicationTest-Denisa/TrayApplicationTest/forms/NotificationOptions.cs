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
    public partial class NotificationOptions : Form
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
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(
                  "net.tcp://10.33.92.30:8021/Service1/LastDropService"));

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

            if (userNot.IFrom.ToString() == "00:00:00" && userNot.ITo.ToString() == "00:00:00")
            {
                checkBox1.Checked = true;
                comboBox2.Enabled = false;
                comboBox1.Enabled = false;
                string inter = userNot.Interval.ToString();
                comboBox3.SelectedText = inter;
                bool mailT = userNot.MailToggle;
                checkBox3.Checked = mailT;
                bool desktopT = userNot.DesktopToggle;
                checkBox2.Checked = desktopT;
            }
            else
            {
                string from = userNot.IFrom.Hours + ":" + userNot.IFrom.Minutes + "0";
                comboBox1.SelectedText = from;
                string to = userNot.ITo.Hours + ":" + userNot.ITo.Minutes + "0";
                comboBox2.SelectedText = to;
                string inter = userNot.Interval.ToString();
                comboBox3.SelectedText = inter;
                bool mailT = userNot.MailToggle;
                checkBox3.Checked = mailT;
                bool desktopT = userNot.DesktopToggle;
                checkBox2.Checked = desktopT;
            }
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            serviceProvider.toggleDesktopNotifications(mail, checkBox2.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            serviceProvider.toggleMailNotifications(mail, checkBox3.Checked);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //serviceProvider.functionForOpenWithWindows
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan fromTS = new TimeSpan(0, 0, 0);
            TimeSpan toTS = new TimeSpan(0, 0, 0);
            int interval = 0;

            if (checkBox1.Checked == true)
            {
                TimeSpan fromAll = new TimeSpan(0, 0, 0);
                TimeSpan toAll = new TimeSpan(0, 0, 0);
                interval = Int32.Parse(comboBox3.Items[comboBox3.SelectedIndex].ToString());
                serviceProvider.setNotificationIntervals(mail, fromAll, toAll, interval);

            }
            if (checkBox1.Checked==false)
            {

                if (comboBox1.SelectedIndex != -1)
                {
                    string fromS = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    string[] fromA = fromS.Split(':');
                    int fromI = Int32.Parse(fromA[0]);
                    fromTS = new TimeSpan(fromI, 0, 0);
                }
                else
                {
                    string fromS = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    string[] fromA = fromS.Split(':');
                    int fromI = Int32.Parse(fromA[0]);
                    fromTS = new TimeSpan(fromI, 0, 0);
                }
                if (comboBox2.SelectedIndex != -1)
                {
                    string toS = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                    string[] toA = toS.Split(':');
                    int toI = Int32.Parse(toA[0]);
                    toTS = new TimeSpan(toI, 0, 0);
                }
                else
                {
                    string toS = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                    string[] toA = toS.Split(':');
                    int toI = Int32.Parse(toA[0]);
                    toTS = new TimeSpan(toI, 0, 0);
                }
                if (comboBox3.SelectedIndex != -1)
                {
                    interval = Int32.Parse(comboBox3.Items[comboBox3.SelectedIndex].ToString());

                }
                else
                {
                    interval = Int32.Parse(comboBox3.Items[comboBox3.SelectedIndex].ToString());
                }
                serviceProvider.setNotificationIntervals(mail, fromTS, toTS, interval);
            }
        }


    }
}
