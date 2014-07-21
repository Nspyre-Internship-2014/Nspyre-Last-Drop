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
    public class WCFServicesProvider : IServicesWCF
    {
        private OperationController databaseController = new OperationController();

        public WCFServicesProvider()
        { }

        public void subscribeToPlant(string mail, string plantName)
        {
            databaseController.subscribeToPlant(mail, plantName);
        }

        public void unsubscribeToPlant(string mail, string plantName)
        {
            databaseController.UnsubscribeFromPlant(mail, plantName);
        }

        public bool registerUser(string mail, string password)
        {
            return databaseController.registerUser(mail, password);
        }

        public string getUserOptions(string name)
        {
            UserNotificationOptions userOptions = databaseController.getUserNotificationOptions(name);
            return serializeToString(userOptions);

        }


        public Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval)
        {
            return databaseController.setNotificationIntervals(username, iFrom, iTo, interval);
        }

        public void toggleDesktopNotifications(string username, Boolean enabled)
        {
            databaseController.toggleDesktopNotifications(username, enabled);
        }

        public void toggleMailNotifications(string username, Boolean enabled)
        {
            databaseController.toggleMailNotifications(username, enabled);
        }

        public string logIn(string mail, string password)
        {
            foreach (User us in databaseController.getUserList())
            {
                if (us.Mail == mail && us.Pass == password)
                    return serializeToString(getUserOptions(mail));
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

        public string getAvailablePlants(string username, string password)
        {
            List<Plant> availablePlants = databaseController.getAvailablePlants(username, password);
            return serializeToString(availablePlants);
        }
    }
}