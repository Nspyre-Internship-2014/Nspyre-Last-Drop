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
        private static OperationController controller;
        
        public WCFServicesProvider()
        { }

        public static void setController(OperationController opController){
            controller = opController;
        }

        [StatusTraceAspect("Subscribe To Plant")]
        public void subscribeToPlant(string mail, string plantName)
        {
            LastDropMainServer.IMyServiceCallback Callback;
            Callback = OperationContext.Current.GetCallbackChannel<LastDropMainServer.IMyServiceCallback>();
            controller.subscribeToPlant(mail, plantName);
            foreach (IMyServiceCallback callback in calls)
                if (callback != null && callback != Callback)
                    try
                    { callback.OnCallback(); }
                    catch (Exception ex)
                    { }
        }

        [StatusTraceAspect("Unsubscribe From Plant")]
        public void unsubscribeToPlant(string mail, string plantName)
        {
            LastDropMainServer.IMyServiceCallback Callback;
            Callback = OperationContext.Current.GetCallbackChannel<LastDropMainServer.IMyServiceCallback>();
            controller.UnsubscribeFromPlant(mail, plantName);
            foreach (IMyServiceCallback callback in calls)
                if (callback != null && callback != Callback)
                    try
                    { callback.OnCallback(); }
                    catch (Exception ex)
                    { }
        }

        [StatusTraceAspect("Register User")] 
        public bool registerUser(string mail, string password)
        {
            return controller.registerUser(mail, password);
        }

        public UserNotificationOptions getUserOptions(string name)
        {
            UserNotificationOptions userOptions = controller.getUserNotificationOptions(name);
            return userOptions;

        }

        [StatusTraceAspect("Set Notification Intervals")] 
        public Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval)
        {
            return controller.setNotificationIntervals(username, iFrom, iTo, interval);
        }

        [StatusTraceAspect("Toggle Desktop Notifications")] 
        public void toggleDesktopNotifications(string username, Boolean enabled)
        {
            controller.toggleDesktopNotifications(username, enabled);
        }

        [StatusTraceAspect("Toggle Mail Notifications")] 
        public void toggleMailNotifications(string username, Boolean enabled)
        {
            controller.toggleMailNotifications(username, enabled);
        }

        [StatusTraceAspect("Log In")] 
        public string logIn(string mail, string password)
        {

            foreach (User us in controller.getUserList())
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
            if (obj != null)
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());

                StringWriter writer = new StringWriter();

                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
            return "";
        }

        //[StatusTraceAspect("Get Available Plants")] 
        public string getAvailablePlants(string username, string password)
        {

            List<Plant> availablePlants = controller.getAvailablePlants(username, password);
            return serializeToString(availablePlants);
        }

        //[StatusTraceAspect("Get Dry Plants")] 
        public string getDryPlants(string username, string password)
        {
            List<Plant> dryPlants = controller.getDryPlants(username, password);
            return serializeToString(dryPlants);
        }

        //[StatusTraceAspect("Get Subscribed Plants")] 
        public string getSubscribedPlants(string username, string password)
        {   
            List<Plant> subscribedPlants = controller.getSubscribedPlants(username, password);
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
            calls.Add(Callback);
        }

        //public void subscribeCallbackChannel(string mail)
        //{
        //    LastDropMainServer.IMyServiceCallback Callback;
        //    Callback = OperationContext.Current.GetCallbackChannel<LastDropMainServer.IMyServiceCallback>();
        //    calls.Add(Callback);

        //    controller.connectedUsersChannels.Add(mail,Callback);
        //}

        //public void unsubscribeCallbackChannel(string mail)
        //{
        //   LastDropMainServer.IMyServiceCallback Callback;
        //   Callback = OperationContext.Current.GetCallbackChannel<LastDropMainServer.IMyServiceCallback>();
        //   calls.Remove(Callback);

        //   controller.connectedUsersChannels.Remove(mail);
        //}

        //[StatusTraceAspect("Get User Notification Options")]
        public string getUserNotificationOptions(string username)
        {
            foreach (User us in controller.getUserList())
            {
                if (us.Mail == username)
                {
                    string result = serializeToString(getUserOptions(username));
                    return result;
                }
            }
            return "fail";
        }

    }
}