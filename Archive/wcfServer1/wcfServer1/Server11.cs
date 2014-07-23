using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Timers;

namespace WCFServer
{
    [ServiceContract(CallbackContract = typeof(IMyServiceCallback))]
    public interface IMyService
    {
        [OperationContract]
        string OpenSession();
        // [OperationContract]
        //  string ReverseString(string value);
       // [OperationContract]
      //  List<Plant22> getPlants(string username);
     //   [OperationContract]
     //   string SerializeToString();
    }

   
    public interface IMyServiceCallback
    {
       [OperationContract]
        void OnCallback();
    }

    [Serializable]
    public class PlantList2
    {
        List<Plant22> listPlantAv;
        List<Plant22> listPlantSub;
        string useroption = "123";

        public PlantList2(int a)
        {
            
        }

        private PlantList2()
        { }

        public string toString()
        {
            listPlantAv = new List<Plant22>();
            listPlantSub = new List<Plant22>();
            listPlantSub.Add(new Plant22("gg", 7));
            listPlantAv.Add(new Plant22("mm", 9));
            string str="merge ";
            str = str + useroption;
            foreach (Plant22 pl in listPlantAv)
                str = str + pl.toString();
            foreach (Plant22 pl in listPlantSub)
                str = str + pl.toString();
            return str;
        }


    }
	

    [Serializable]
    public class Plant22
    {
        public string name;
        public int amount;
        public Plant22(string name, int amount)
        {
            this.name = name;
            this.amount = amount;
        }

        private Plant22()
        { }

        public string toString()
        {
            return name + " , amount=" + amount;
        }

    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyService : IMyService
    {
       

        public static Timer Timer;


        
        public string OpenSession()
        {
            IMyServiceCallback Callback;
            Callback = OperationContext.Current.GetCallbackChannel<IMyServiceCallback>();
            Console.WriteLine("> Session opened at {0}", DateTime.Now);

          
           // while (true)
            {
                System.Threading.Thread.Sleep(1000);
                Callback.OnCallback();

            }
            return "ss";
        }

       
    }


        class Program
        {
       //     public static IMyServiceCallback Callback;
            static void Main(string[] args)
            {

                using (ServiceHost host = new ServiceHost(
                  typeof(MyService),
                  new Uri[]{
          new Uri("net.tcp://10.33.90.52:8021/Service1"),
      //  new Uri("net.tcp://169.254.2.121:8021/Service1"),
        }))
                {
                    NetTcpBinding netTcp = new NetTcpBinding();
                    netTcp.Security.Mode = SecurityMode.None;
                    host.AddServiceEndpoint(typeof(IMyService),
                      netTcp,
                      "LastDropService");

                    host.Open();

                   
                  

                    Console.WriteLine("Service is available. " +
                      "Press <ENTER> to exit.");
                    Console.ReadLine();

                    host.Close();
                }
            }
        }
    }



