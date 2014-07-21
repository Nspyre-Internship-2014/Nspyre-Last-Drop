using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace LastDropMainServer
{
    public class OperationController
    {

        DatabaseRepository repository;

        public OperationController()
        {
            repository = new DatabaseRepository("Data Source=LPT0902\\SQLEXPRESS;Initial Catalog=LastDropDB;Integrated Security=true");
        }

        public void subscribeToPlant(String MailS, String plantName)
        {
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
                s = new Subscriber(MailS, plantName);
            repository.addSubscriber(s);

        }

        public void UnsubscribeFromPlant(string MailS, String plantName)
        {
            List<Subscriber> subscribers = repository.SubscriberList;
            foreach (Subscriber sub in subscribers)
                if (sub.PlantName == plantName && sub.MailSubscriber == MailS)
                    repository.deleteSubscriber(sub);

        }

        public bool registerUser(string mail, string password)
        {
            User user1 = new User(mail, password);
            foreach (User us in repository.UserList)
            {
                if (user1.Mail == us.Mail)
                    return false;
            }

            repository.addUser(user1);

            return true;
        }

        public Subscriber getMailSubscriberByPlantName(String name)
        {
            List<Subscriber> subscribers = repository.SubscriberList;
            foreach (Subscriber subscriber in subscribers)
                if (subscriber.PlantName == name)
                    return subscriber;
            return null;
        }


        public void sendMailNotification(Plant p)
        {
            Subscriber subscriber = getMailSubscriberByPlantName(p.Name);
            if (p.Status == "dry")
                sendMail(subscriber.MailSubscriber);
        }

        private void sendMail(String mail)
        {
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "lastdropcontact@yahoo.com";
            string password = "GreenMinds1";
            string subject = "Last Drop";
            string body = "Wet your plant!";

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

        public bool checkPlantState(string nameplant)
        {
            foreach (Plant pl in repository.PlantList)
            {
                if (pl.Name == nameplant)
                    if (pl.Status == "0")
                        return false;
            }
            return true;
        }

        public Boolean setNotificationIntervals(string username, TimeSpan iFrom, TimeSpan iTo, int interval)
        {
            UserNotificationOptions userOptions = getUserNotificationOptions(username);
            UserNotificationOptions oldUserOptions = getUserNotificationOptions(username);

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

            userOptions.MailToggle = enabled;

            repository.updateUserNotificationOptions(oldUserOptions, userOptions);

        }

        public void toggleDesktopNotifications(string username, Boolean enabled)
        {
            UserNotificationOptions userOptions = getUserNotificationOptions(username);
            UserNotificationOptions oldUserOptions = getUserNotificationOptions(username);

            userOptions.DesktopToggle = enabled;

            repository.updateUserNotificationOptions(oldUserOptions, userOptions);
        }

        private User getUserByName(string username)
        {
            foreach (User u in this.repository.UserList)
                if (u.Mail == username)
                    return u;
            return null;
        }

        public UserNotificationOptions getUserNotificationOptions(string username)
        {
            User user = getUserByName(username);

            foreach (UserNotificationOptions opt in repository.UserNotificationOptionsList)
                if (opt.Mail == user.Mail)
                    return opt;
            return null;
        }

        public List<Plant> getAvailablePlants(string name, string password)
        {
            List<Plant> availablePlants = new List<Plant>();
            User us1 = getUserByName(name);
            if (us1.Pass == password)
            {

                foreach (Plant pl in repository.PlantList)
                {
                    bool checkPlant = true;
                    foreach (Subscriber sub in repository.SubscriberList)
                    {
                        if (pl.Name == sub.PlantName && sub.MailSubscriber == name)
                        {
                            availablePlants.Add(pl);
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

        public List<History> getHistoryByPlantName(String name)
        {
            List<History> histories = repository.HistoryList;
            List<History> historyForPlant = new List<History>();

            foreach (History history in histories)
            {
                if (history.PlantName == name)
                    historyForPlant.Add(history);
            }

            return historyForPlant;
        }
    }
}
