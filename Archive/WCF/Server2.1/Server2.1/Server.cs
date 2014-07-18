using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ServiceModel;

namespace WCFServer
{
    [ServiceContract]
    public interface IStringReverser
    {
        [OperationContract]
        string ReverseString(string value);
        [OperationContract]
        string HelloWorld(string str);
     
    }

    public class StringReverser : IStringReverser
    {
        public string ReverseString(string value)
        {
            char[] retVal = value.ToCharArray();
            int idx = 0;
            for (int i = value.Length - 1; i >= 0; i--)
                retVal[idx++] = value[i];

            return new string(retVal);
        }


        public string HelloWorld(string str)
        {
            return "Helloworld from " + str;
        }


       
    
    }

     

    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(
              typeof(StringReverser),
              new Uri[]{
          new Uri("net.tcp://10.33.92.16:8021/Service1"),
        //  new Uri("net.pipe://localhost")
        }))
            {
                NetTcpBinding netTcp = new NetTcpBinding();
                netTcp.Security.Mode = SecurityMode.None;
                host.AddServiceEndpoint(typeof(IStringReverser),
                  netTcp, // BasicHttpBinding() ,
                  "Reverse");


                //   host.AddServiceEndpoint(typeof(IStringReverser),
                //    new NetNamedPipeBinding(),
                //    "PipeReverse");

                host.Open();


                Console.WriteLine("Service is available. " +
                  "Press <ENTER> to exit.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}