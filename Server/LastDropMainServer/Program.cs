using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;


namespace LastDropDBOperations
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (ServiceHost host = new ServiceHost(
                typeof(WCFServicesProvider),
                new Uri[]{
          new Uri("net.tcp://10.33.92.62:8021/Service1"),
        }))
            {
                NetTcpBinding netTcp = new NetTcpBinding();
                netTcp.Security.Mode = SecurityMode.None;
                host.AddServiceEndpoint(typeof(IServicesWCF),
                  netTcp,
                  "Serv1");

                host.Open();

                MessageBox.Show("Service is available. " +
                  "Press <Ok> to exit.");

                host.Close();

            }



        }
    }
}
