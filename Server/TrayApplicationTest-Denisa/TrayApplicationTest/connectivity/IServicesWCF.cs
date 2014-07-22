using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
 
 
namespace TrayApplicationTest
{
     [ServiceContract(CallbackContract = typeof(IMyServiceCallback))]
    interface IServicesWCF
    {
        [OperationContract]
        void subscribeToPlant(string mail, string plantName);
 
        [OperationContract]
        void unsubscribeToPlant(string mail, string plantName);
 
        [OperationContract]
        bool registerUser(string mail, string password);
 
        //returns fail or the serialized userOptions as string
        [OperationContract]
        string logIn(string mail, string password);
 
        [OperationContract]
         Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval);
 
        [OperationContract]
         void toggleMailNotifications(string username, Boolean enabled);
 
        [OperationContract]
         void toggleDesktopNotifications(string username, Boolean enabled);
 
        //returns a serialized plant list as string
        [OperationContract]
         string getAvailablePlants(string username , string password);
    }
}