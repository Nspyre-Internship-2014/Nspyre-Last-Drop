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
    public partial class DesktopNotification : Form
    {
        private string mail;
        private string password;

        System.Threading.Timer t;
        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;
        ServerConnectivity serverConnectivity = new ServerConnectivity();

        UserNotificationOptions uno;
        NotifyIcon notifyIcon1;
     
        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(Properties.Settings.Default.serverIP));

            serviceProvider = netTcpFactory.CreateChannel();
        }
        public DesktopNotification(string mail, string password, NotifyIcon notifyIcon2)
        {
            this.mail = mail;
            this.password = password;
            this.notifyIcon1 = notifyIcon2;
            ServerConnect();
            InitializeComponent();
            printDesktopNotifications();
        }

        void TimerCallback(object o)
        {
            string message = null;
            uno = getUserNotificationObject();
            Console.WriteLine("in TimberCallBack, the uno object-> " + uno.ToString());
            List<Plant> dryPlantList;
            dryPlantList = getDryList();

            foreach (Plant p in dryPlantList)
            {
                message += "Please water the plant: " + p.Name + " with the amount: " + p.WaterAmount + " !\n";
            }
            foreach (Plant p in dryPlantList)
            {
                if (uno.DesktopToggle == true)
                {
                    TimeSpan nowTS = DateTime.Now.TimeOfDay;
                    string nowString = nowTS.ToString();
                    string[] nowArray = nowString.Split(':');
                    string nowHour = nowArray[0];
                    int nowHourI = Int32.Parse(nowHour);
                    
                    if (uno.IFrom.Hours < nowHourI && uno.ITo.Hours > nowHourI)
                    {
                        notifyIcon1.BalloonTipText = message;
                        notifyIcon1.ShowBalloonTip(1000);
                    }
                }
            }
        }
       
        private UserNotificationOptions getUserNotificationObject()
        {
            string str = serviceProvider.getUserNotificationOptions(mail);
            var serializer = new XmlSerializer(typeof(UserNotificationOptions));
            if (str != "fail")
            {
                using (TextReader reader = new StringReader(str))
                {
                    uno = (UserNotificationOptions)serializer.Deserialize(reader);
                }
            }
            return uno;
        }

        private List<Plant> getDryList()
        {
            string dryList;
            dryList = serviceProvider.getDryPlants(mail, password);

            var serializer2 = new XmlSerializer(typeof(List<Plant>));
            List<Plant> dryPlantList;
            using (TextReader reader = new StringReader(dryList))
            {
                dryPlantList = (List<Plant>)serializer2.Deserialize(reader);
            }
            return dryPlantList;
        }

        public void printDesktopNotifications()
        {
            //int interval = 60 * 60 * 1000 * uno.Interval;
            System.Threading.TimerCallback callback = new System.Threading.TimerCallback(TimerCallback);
            t = new System.Threading.Timer(callback, null, 0, 3000);         
        }
        public void StopTimer()
        {
            t.Dispose();
        }
    }

}
