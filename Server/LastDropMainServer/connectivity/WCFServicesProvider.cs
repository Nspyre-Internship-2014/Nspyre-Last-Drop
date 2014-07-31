using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Configuration;

namespace LastDropMainServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WCFServicesProvider : IServicesWCF
    {

        public static List<IMyServiceCallback> calls = new List<IMyServiceCallback>();
        private OperationController databaseController = new OperationController();

        public WCFServicesProvider()
        { }

        public WCFServicesProvider(OperationController controller)
        {
            databaseController = controller;
        }

        

        [StatusTraceAspect("Subscribe To Plant")] 
        public void subscribeToPlant(string mail, string plantName)
        {
            databaseController.subscribeToPlant(mail, plantName);
            foreach (IMyServiceCallback callback in calls)
                if (callback != null)
                    try
                    { callback.OnCallback(); }
                    catch (Exception ex)
                    { }
        }

        [StatusTraceAspect("Unsubscribe From Plant")] 
        public void unsubscribeToPlant(string mail, string plantName)
        {
            databaseController.UnsubscribeFromPlant(mail, plantName);
            foreach (IMyServiceCallback callback in calls)
                if (callback != null)
                    callback.OnCallback();
        }

        [StatusTraceAspect("Register User")] 
        public bool registerUser(string mail, string password)
        {
            return databaseController.registerUser(mail, password);
        }

        public UserNotificationOptions getUserOptions(string name)
        {
            UserNotificationOptions userOptions = databaseController.getUserNotificationOptions(name);
            return userOptions;

        }

        [StatusTraceAspect("Set Notification Intervals")] 
        public Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval)
        {
            return databaseController.setNotificationIntervals(username, iFrom, iTo, interval);
        }

        [StatusTraceAspect("Toggle Desktop Notifications")] 
        public void toggleDesktopNotifications(string username, Boolean enabled)
        {
            databaseController.toggleDesktopNotifications(username, enabled);
        }

        [StatusTraceAspect("Toggle Mail Notifications")] 
        public void toggleMailNotifications(string username, Boolean enabled)
        {
            databaseController.toggleMailNotifications(username, enabled);
        }

        [StatusTraceAspect("Log In")] 
        public string logIn(string mail, string password)
        {

            foreach (User us in databaseController.getUserList())
            {
                if (us.Mail == mail && us.Pass == password)
                {
                    string result = serializeToString(getUserOptions(mail));
                    return result;
                }
            }
            return "fail";
        }

        private string serializeToString(Object obj)
        {

            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            StringWriter writer = new StringWriter();

            serializer.Serialize(writer, obj);

            return writer.ToString();
        }

        [StatusTraceAspect("Get Available Plants")] 
        public string getAvailablePlants(string username, string password)
        {

            List<Plant> availablePlants = databaseController.getAvailablePlants(username, password);
            return serializeToString(availablePlants);
        }


        [StatusTraceAspect("Get Dry Plants")] 
        public string getDryPlants(string username, string password)
        {
            List<Plant> dryPlants = databaseController.getDryPlants(username, password);
            return serializeToString(dryPlants);
        }

        [StatusTraceAspect("Get Subscribed Plants")] 
        public string getSubscribedPlants(string username, string password)
        {   
            List<Plant> subscribedPlants = databaseController.getSubscribedPlants(username, password);
            return serializeToString(subscribedPlants);
        }

        public void subscribe()
        {
            LastDropMainServer.IMyServiceCallback Callback;
            Callback = OperationContext.Current.GetCallbackChannel<LastDropMainServer.IMyServiceCallback>();
            calls.Add(Callback);
        }

        public void unsubscribe()
        {
           LastDropMainServer.IMyServiceCallback Callback;
           Callback = OperationContext.Current.GetCallbackChannel<LastDropMainServer.IMyServiceCallback>();
           calls.Remove(Callback);
        }   
    }
}