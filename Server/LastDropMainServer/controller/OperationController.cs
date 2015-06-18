using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Configuration;
 
namespace LastDropMainServer
{
    public class OperationController
    {
 
        DatabaseRepository repository;
        public Dictionary<String, IMyServiceCallback> connectedUsersChannels = new Dictionary<string, IMyServiceCallback>();

        public OperationController()
        {
            //Requires this: http://stackoverflow.com/questions/851783/system-configuration-configurationmanager-not-available
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LastDropDB"].ConnectionString;
            repository = new DatabaseRepository("Data Source=LOOP-PC;Initial Catalog=LastDropDB;Integrated Security=True");//connectionString);
            foreach (MemberInfo info in GetType().GetMembers())
            {
                Console.Out.WriteLine("Member name: " + info.Name);
                Console.Out.WriteLine("Member type: " + info.MemberType);
                Console.Out.WriteLine("Declared by: " + info.DeclaringType);
                Console.Out.WriteLine();
            }
            

        }
 
        public void subscribeToPlant(String MailS, String plantName)
        {
            repository.refreshUserList();
            repository.refreshPlantList();
            repository.refreshSubscriberList();
            List<User> users = repository.UserList;
            List<Plant> plants = repository.PlantList;
            List<Subscriber> subscribers = repository.SubscriberList;
            Subscriber s = null;
            bool okp = false;
            bool oku = false;
            bool available = true;
            foreach (User u in users)
                if (u.Mail == MailS)
                    oku = true;
            foreach (Plant p in plants)
                if (p.Name == plantName)
                    okp = true;
            foreach (Subscriber su in subscribers)
            {
                if (su.PlantName == plantName)
                    available = false;
            }
            if (okp && oku && available)
            {
                s = new Subscriber(MailS, plantName);
                repository.addSubscriber(s);
            }
        }
 
        public void UnsubscribeFromPlant(string MailS, String plantName)
        {
            repository.refreshUserList();
            repository.refreshPlantList();
            repository.refreshSubscriberList();
            List<Subscriber> subscribers = repository.SubscriberList;
            foreach (Subscriber sub in subscribers)
                if (sub.PlantName == plantName && sub.MailSubscriber == MailS)
                    repository.deleteSubscriber(sub);
 
        }
 
        public bool registerUser(string mail, string password)
        {
            repository.refreshUserList();
            User user1 = new User(mail, password);
            foreach (User us in repository.UserList)
            {
                if (user1.Mail == us.Mail)
                    return false;
            }
 
            TimeSpan t1 = new TimeSpan(9, 0, 0);
            TimeSpan t2 = new TimeSpan(17,0,0);
            UserNotificationOptions usop = new UserNotificationOptions(mail, t1,t2,false,false,1);
 
            repository.addUser(user1);
            repository.addUserNotificationOptions(usop);
 
            return true;
        }
 
        public Subscriber getMailSubscriberByPlantName(String name)
        {
            repository.refreshSubscriberList();
            List<Subscriber> subscribers = repository.SubscriberList;
            foreach (Subscriber subscriber in subscribers)
                if (subscriber.PlantName == name)
                    return subscriber;
            return null;
        }
 
        public int checkPlantState(string nameplant)
        {
            repository.refreshPlantList();
            foreach (Plant pl in repository.PlantList)
            {
                if (pl.Name == nameplant)
                {
                    if (pl.Status == "2")
                        return 2;
                    if (pl.Status == "1")
                        return 1;
                    if (pl.Status == "0")
                        return 0;
                }
            }
            return -1;
        }
 
        public Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval)
        {
           
            UserNotificationOptions userOptions = getUserNotificationOptions(username);
            UserNotificationOptions oldUserOptions = getUserNotificationOptions(username);
 
            if (userOptions == null)
                return false;
 
            userOptions.IFrom = iFrom;
            userOptions.ITo = iTo;
            userOptions.Interval = interval;
 
            repository.updateUserNotificationOptions(oldUserOptions, userOptions);
 
            return true;
        }
 
        public void toggleMailNotifications(string username, Boolean enabled)
        {
           
            UserNotificationOptions userOptions = getUserNotificationOptions(username);
            UserNotificationOptions oldUserOptions = getUserNotificationOptions(username);
 
            if (userOptions != null)
            {
                userOptions.MailToggle = enabled;
                repository.updateUserNotificationOptions(oldUserOptions, userOptions);
            }
           
        }
 
        public void toggleDesktopNotifications(string username, Boolean enabled)
        {
 
            UserNotificationOptions userOptions = getUserNotificationOptions(username);
            UserNotificationOptions oldUserOptions = getUserNotificationOptions(username);
 
            if (userOptions != null)
            {
                userOptions.DesktopToggle = enabled;
                repository.updateUserNotificationOptions(oldUserOptions, userOptions);
            }
        }
 
        private User getUserByName(string username)
        {
            repository.refreshUserList();
            foreach (User u in this.repository.UserList)
                if (u.Mail == username)
                    return u;
            return null;
        }
 
        public UserNotificationOptions getUserNotificationOptions(string username)
        {
            UserNotificationOptions obj = null;
            repository.refreshUserNotificationOptionsList();
            User user = getUserByName(username);
            if (user == null)
                return null;
 
            foreach (UserNotificationOptions opt in repository.UserNotificationOptionsList)
                if (opt.Mail == user.Mail)
                {
                    obj = opt;
                }
            return obj;
        }
 
        public List<Plant> getAvailablePlants(string name, string password)
        {
            List<Plant> availablePlants = new List<Plant>();
            repository.refreshSubscriberList();
            repository.refreshUserList();
            repository.refreshPlantList();
            User us1 = getUserByName(name);
 
            if (us1 != null && us1.Pass == password)
            {
 
                foreach (Plant pl in repository.PlantList)
                {
                    bool checkPlant = true;
                    foreach (Subscriber sub in repository.SubscriberList)
                    {
                        if (pl.Name == sub.PlantName && sub.MailSubscriber == name)
                        {
                            checkPlant = false;
                            break;
                        }
                        if (pl.Name == sub.PlantName)
                        {
                            checkPlant = false;
                            break;
                        }
                    }
                    if (checkPlant)
                        availablePlants.Add(pl);
                }
            }
            return availablePlants;
        }
 
        public List<User> getUserList()
        {
            return repository.UserList;
        }
 
        public List<Plant> getPlantList()
        {
            return repository.PlantList;
        }
 
        public void updateDatabasePlantState(Plant plant, Int32 sensorData)
        {
            if(sensorData>plant.DryValue)
            {
                Plant newPlant = new Plant(plant.Name,"2",plant.WaterAmount,plant.CoolDown,plant.DryValue,plant.HumidValue,plant.PlantID);
                repository.updatePlant(plant, newPlant);
            }
            else if (sensorData >= plant.HumidValue)
            {
                Plant newPlant = new Plant(plant.Name, "1", plant.WaterAmount, plant.CoolDown, plant.DryValue, plant.HumidValue, plant.PlantID);
                repository.updatePlant(plant, newPlant);
                if (plant.Status == "2")
                {
                    History historyPoint = new History(newPlant.Name, DateTime.Now);
                    repository.addHistory(historyPoint);
                }
            }
            else
            {
                Plant newPlant = new Plant(plant.Name, "0", plant.WaterAmount, plant.CoolDown, plant.DryValue, plant.HumidValue, plant.PlantID);
                repository.updatePlant(plant, newPlant);
                if (plant.Status == "1")
                {
                    History historyPoint = new History(newPlant.Name, DateTime.Now);
                    repository.addHistory(historyPoint);
                }
            }
        }
 
        public List<Plant> getDryPlants(string username, string password)
        {
 
            List<Plant> plantList = new List<Plant>();
            repository.refreshSubscriberList();
            repository.refreshUserList();
            repository.refreshPlantList();
            User us1 = getUserByName(username);
            if (us1 != null && us1.Pass == password)
            {
                foreach (Subscriber sub in repository.SubscriberList)
                {
                    if (sub.MailSubscriber == username)
                    {
                        string namePl = sub.PlantName;
                        foreach (Plant pl in repository.PlantList)
                        {
                            if (pl.Name == namePl && pl.Status == "2")
                                plantList.Add(pl);
                        }
                    }
                }
            }
            return plantList;
        }
 
        public Plant getPlantByName(String name)
        {
            List<Plant> plants = repository.PlantList;
            foreach (Plant plant in plants)
                if (plant.Name == name)
                    return plant;
            return null;
        }
        public List<Plant> getSubscribedPlants(string mail, string password)
        {
            List<Plant> plantList = new List<Plant>();
            User user = getUserByName(mail);
            repository.refreshSubscriberList();
            repository.refreshUserList();
            repository.refreshPlantList();
            if (user.Pass == password)
                foreach (Subscriber subscriber in repository.SubscriberList)
                    if (subscriber.MailSubscriber == mail)
                        plantList.Add(getPlantByName(subscriber.PlantName));
 
            return plantList;
        }



        public void sendMailNotification(User user)
        {
            List<Plant> dryPlants = this.getDryPlants(user.Mail, user.Pass);

            if (dryPlants.Count != 0)
            {
                string subject = "Last Drop Digest";
                var sb = new StringBuilder();
                string s = "<img src=\"http://i.imgur.com/R8f9mjF.png\"><br/>";
                sb.AppendLine(s);
                s = "There are plants that require your attention:<br/>";
                sb.AppendLine(s);
                foreach (Plant p in dryPlants)
                {
                    s = "- Plant " + p.Name + " requires " + p.WaterAmount + "ml of water.<br/>";
                    sb.AppendLine(s);
                }

                sendMail(user.Mail, subject, sb.ToString());
            }
        }

        public void sendMailNotification(Plant p)
        {
            Subscriber subscriber = getMailSubscriberByPlantName(p.Name);

            if (p.Status == "2")
            {
                string subject = "Last Drop Notice";
                string body = "You need to wet " + p.Name + " with " + p.WaterAmount + "ml of life-giving water.\n";

                sendMail(subscriber.MailSubscriber, subject, body);
            }
        }

        private void sendMail(string mail, string subject, string body)
        {
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "lastdropcontact@yahoo.com";
            string password = "GreenMinds1";
            string mailSubject = subject;
            string mailBody = body;


            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(emailFrom);
                message.To.Add(mail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(message);
                }
            }
        }

        public void updateDatabase()
        {
            repository.refreshHistoryList();
            repository.refreshPlantList();
            repository.refreshSubscriberList();
            repository.refreshUserList();
            repository.refreshUserNotificationOptionsList();
        }

        public Boolean isLoggedIn(string mail)
        {
            return this.connectedUsersChannels.ContainsKey(mail);
        }

        public bool isOnCooldown(string plantName)
        {
            History lastWatered = this.getLastWateredOn(plantName);
            if (lastWatered == null)
                return false;

            Plant plant = this.getPlantByName(plantName);

            Double hoursPassed = (DateTime.Now - lastWatered.WateredOn).TotalHours;

            return hoursPassed > plant.CoolDown.TotalHours;
        }

        public History getLastWateredOn(string plantName)
        {
            Plant plant = this.getPlantByName(plantName);

            History lastWatered = null;

            foreach(History history in this.repository.HistoryList){
                if(history.PlantName == plant.Name)
                    lastWatered = history;
            }

            return lastWatered;
        }

    }
}


