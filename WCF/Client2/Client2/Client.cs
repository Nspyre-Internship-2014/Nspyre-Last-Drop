using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WCFClient
{
    [ServiceContract]
    public interface IStringReverser
    {
        [OperationContract]
        string ReverseString(string value);
        [OperationContract]
        string HelloWorld(string str);
     
        
    }


    


    class Program
    {
        static void Main(string[] args)
        {

            NetTcpBinding netTcp = new NetTcpBinding();
            netTcp.Security.Mode = SecurityMode.None;
            ChannelFactory<IStringReverser> httpFactory =
              new ChannelFactory<IStringReverser>(
                netTcp,
                new EndpointAddress(
                  "net.tcp://10.33.92.16:8021/Service1/Reverse"));

            /*  ChannelFactory<IStringReverser> pipeFactory =
               new ChannelFactory<IStringReverser>(
                 new NetNamedPipeBinding(),
                 new EndpointAddress(
                   "net.pipe://10.33.90.52/PipeReverse")); */

              IStringReverser httpProxy =
             httpFactory.CreateChannel();

           // IStringReverser pipeProxy =
           //   pipeFactory.CreateChannel();

            while (true)
            {
                string str = Console.ReadLine();
                Console.WriteLine("http: " +
                 httpProxy.HelloWorld(str));
                Console.WriteLine("http: " +
                 httpProxy.ReverseString(str));

                // Console.WriteLine("pipe: " +
                   // pipeProxy.ReverseString(str));
            }
        }
    }
}