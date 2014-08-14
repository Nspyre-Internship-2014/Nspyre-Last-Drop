using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;


namespace LastDropMainServer
{
    [ServiceContract(CallbackContract = typeof(IMyServiceCallback), SessionMode = SessionMode.Required)]
    public interface IServicesWCF
    {
        [OperationContract]
        void subscribeToPlant(string mail, string plantName);

        [OperationContract]
        void unsubscribeToPlant(string mail, string plantName);

        [OperationContract]
        bool registerUser(string mail, string password);

        [OperationContract]
        string logIn(string mail, string password);

        [OperationContract]
        Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval);

        [OperationContract]
        void toggleMailNotifications(string username, Boolean enabled);

        [OperationContract]
        void toggleDesktopNotifications(string username, Boolean enabled);

        [OperationContract]
        string getAvailablePlants(string username, string password);

        [OperationContract]
        string getDryPlants(string username, string password);

        [OperationContract]
        string getSubscribedPlants(string username, string password);

        //[OperationContract]
        //void subscribeCallbackChannel(string mail);

        //[OperationContract]
        //void unsubscribeCallbackChannel(string mail);

        [OperationContract]
        void subscribe();

        [OperationContract]
        void unsubscribe();

        [OperationContract]
        string getUserNotificationOptions(string username);

    }
}

