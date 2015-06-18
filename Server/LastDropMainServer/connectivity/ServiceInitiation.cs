using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Sockets;

namespace LastDropMainServer
{
    class ServiceInitiation
    {

        private ServiceHost host;

        public ServiceInitiation(){}

        private void InitializeService()
        {
            host = new ServiceHost(
                    typeof(WCFServicesProvider),
                    new Uri[]{
                    new Uri("net.tcp://" + this.findLocalIP() + ":8021/Service1"),
                    });

            String uri = this.findLocalIP();

            NetTcpBinding netTcp = new NetTcpBinding();
            netTcp.Security.Mode = SecurityMode.None;
            host.AddServiceEndpoint(typeof(IServicesWCF),
              netTcp,
              "LastDropService");
        }

        public void stopServices()
        {
            host.Close();
        }

        public void startServices()
        {
            InitializeService();

            host.Open();
        }
        private string findLocalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
