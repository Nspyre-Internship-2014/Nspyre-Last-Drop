using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TrayApplicationTest
{
    class ServerConnectivity
    {   
        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;
        
        public ServerConnectivity()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();
 
            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(
                  "net.tcp://192.168.56.1:8021/Service1/LastDropService"));
            serviceProvider = netTcpFactory.CreateChannel();
        }

        public void endConnection() 
        {
            netTcpFactory.Close();     
        }

        public IServicesWCF getServerConnectivity()
        {
            return serviceProvider;
        }
    }
}
