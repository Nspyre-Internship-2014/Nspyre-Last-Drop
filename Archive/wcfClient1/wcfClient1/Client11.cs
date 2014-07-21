using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml.Serialization;
using System.IO;

namespace LastDropMainServer
{
    [ServiceContract]
    public interface IStringReverser
    {
       // [OperationContract]
      //  string ReverseString(string value);

         [OperationContract]
         List<Plant22> getPlants(string username);
         [OperationContract]
         string SerializeToString();

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
            string str = "merge ";
            str = str + useroption;
            foreach (Plant22 pl in listPlantAv)
                str = str + pl.toString();
            foreach (Plant22 pl in listPlantSub)
                str = str + pl.toString();
            return str;
        }


    }
   
    public class PlantList
    {

        PlantList()
        {

        }
       
        public List<Plant22> getPlants(string username)
        {
            List<Plant22> listPlant = new List<Plant22>();
            Plant22 plant1 = new Plant22("alfa", 3);
            Plant22 plant2 = new Plant22("beta", 7);
            listPlant.Add(plant1);
            listPlant.Add(plant2);
            return listPlant;
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
       /*     NetTcpBinding netTcp = new NetTcpBinding();
            netTcp.Security.Mode = SecurityMode.None;
            ChannelFactory<IServicesWCF> httpFactory =
              new ChannelFactory<IServicesWCF>(
              netTcp,
                new EndpointAddress(
                  "net.tcp://10.33.90.52:8021/Service1/LastDropService"));
         //  "net.tcp://169.254.2.121:8021/Service1/Reverse"));

            IServicesWCF serviceProvider =
              httpFactory.CreateChannel();
        */

            NetTcpBinding netTcp = new NetTcpBinding();
            netTcp.Security.Mode = SecurityMode.None;
            ChannelFactory<IStringReverser> httpFactory =
              new ChannelFactory<IStringReverser>(
              netTcp,
                new EndpointAddress(
                  "net.tcp://10.33.90.52:8021/Service1/LastDropService"));
            //  "net.tcp://169.254.2.121:8021/Service1/Reverse"));

            IStringReverser serviceProvider =
              httpFactory.CreateChannel();
           
       

            while (true)
            {
                string str = Console.ReadLine();
                string str2 = serviceProvider.SerializeToString();
            //    foreach(Plant22 plant2 in list2)
                var serializer = new XmlSerializer(typeof(Plant22));
                Plant22 plant2=new Plant22("beta",4);

                using (TextReader reader = new StringReader(str2))
                {
                    plant2 = (Plant22)serializer.Deserialize(reader);
                }
                Console.WriteLine("http: " +  plant2.toString());
           
            } 

         //   serviceProvider.subscribeToPlant("maxim_ale@yahoo.com", "Jon");        
         //   serviceProvider.unsubscribeToPlant( "marcIon@yahoo.com","Marcel");
     /*     string str2=  serviceProvider.logIn("mari@yahoo.com","maricica");
         
          var serializer = new XmlSerializer(typeof(UserNotificationOptions));
          UserNotificationOptions usop;
          if (str2 != "fail")
          {
              using (TextReader reader = new StringReader(str2))
              {
                  usop = (UserNotificationOptions)serializer.Deserialize(reader);
              }
              Console.WriteLine("http: " + usop.ToString());

          }
      */

            /* 
              string str3 = serviceProvider.getAvailablePlants("mari@yahoo.com","maricica");
              var serializer = new XmlSerializer(typeof(List<Plant>));
              List<Plant> usop;
              if (str3 != "fail")
              {
                  using (TextReader reader = new StringReader(str3))
                  {
                      usop = (List<Plant>)serializer.Deserialize(reader);
                  }
                  Console.WriteLine("http: "+usop.Count);

              }
             */


            Console.ReadKey();

        }
    }
}