using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LastDropMainServer
{
    class DatabaseRepository
    {

        SqlConnection con;
        DatabaseMemoryStoring store;

        public DatabaseRepository(String connectionString)
        {
            //Allocate memory
            historyList = new List<History>();
            userList = new List<User>();
            plantList = new List<Plant>();
            subscriberList = new List<Subscriber>();

            //Initialize the connection
            con = new SqlConnection(connectionString);

            //Initialize the memory importer class
            store = new DatabaseMemoryStoring();

            //Refresh from database
            refreshHistoryList();
            refreshPlantList();
            refreshSubscriberList();
            refreshUserList();
            refreshUserNotificationOptionsList();
        }

        public void refreshHistoryList()
        {
            historyList = store.StoreInClassHistory(con);
        }
        public void refreshPlantList()
        {
            plantList = store.StoreInClassPlant(con);
        }
        public void refreshUserList()
        {
            userList = store.StoreInClassUser(con);
        }
        public void refreshSubscriberList()
        {
            subscriberList = store.StoreInClassSubscriber(con);
        }
        public void refreshUserNotificationOptionsList()
        {
            userNotificationOptionsList = store.storeInClassUserNotificationOptions(con);
        }

        private List<History> historyList;

        public List<History> HistoryList
        {
            get
            {
                List<History> histList = new List<History>(historyList);
                return histList;
            }
            set { historyList = value; }
        }

        private List<Plant> plantList;
        public List<Plant> PlantList
        {
            get
            {
                List<Plant> plantlist = new List<Plant>(plantList);
                return plantlist;
            }
            set { plantList = value; }
        }

        private List<Subscriber> subscriberList;
        public List<Subscriber> SubscriberList
        {
            get
            {
                List<Subscriber> subscList = new List<Subscriber>(subscriberList);
                return subscList;
            }
            set { subscriberList = value; }
        }

        private List<User> userList;
        public List<User> UserList
        {
            get
            {
                List<User> usList = new List<User>(userList);
                return usList;
            }
            set { userList = value; }
        }

        private List<UserNotificationOptions> userNotificationOptionsList;
        public List<UserNotificationOptions> UserNotificationOptionsList
        {
            get
            {
                List<UserNotificationOptions> userNotOptList = new List<UserNotificationOptions>(userNotificationOptionsList);
                return userNotOptList;
            }
            set
            {
                userNotificationOptionsList = value;
            }
        }


        public void deletePlant(Plant plant){

            try
            {

                //Delete from database
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from Plants where Name=@Name";
                cmd.Parameters.Add("@Name", plant.Name);
                cmd.Connection = con;

                cmd.ExecuteNonQuery();

                //Delete from memory
                foreach (Plant p in this.plantList)
                    if (p.Equals(plant))
                        this.plantList.Remove(p);
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
                
        }

        public void deleteHistory(History history)
        {
            try
            {
                //Delete from database
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from History where PlantName=@PlantName AND WateredOn=@WateredOn";
                cmd.Parameters.Add("@PlantName", history.PlantName);
                cmd.Parameters.Add("@WateredOn",history.WateredOn);
                cmd.Connection = con;

                cmd.ExecuteNonQuery();

                //Delete from memory
                this.historyList.Remove(history);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void deleteSubscriber(Subscriber subscriber)
        {
            try
            {
                //Delete from database
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from Subscribes where PlantName=@PlantName AND MailSubscriber=@MailSubscriber";
                cmd.Parameters.Add("@PlantName", subscriber.PlantName);
                cmd.Parameters.Add("@MailSubscriber", subscriber.MailSubscriber);
                cmd.Connection = con;

                cmd.ExecuteNonQuery();

                //Delete from memory
                this.subscriberList.Remove(subscriber);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void deleteUser(User user)
        {
            try
            {
                //Delete from database
                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from Users where Mail=@Mail";
                cmd.Parameters.Add("@Mail", user.Mail);
                cmd.Connection = con;

                cmd.ExecuteNonQuery();

                //Delete from memory
                this.userList.Remove(user);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void deleteUserNotificationOptions(UserNotificationOptions userNotOp)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from UserNotificationOptions where Mail=@mail";
                cmd.Parameters.Add("@mail", userNotOp.Mail);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                this.userNotificationOptionsList.Remove(userNotOp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }


        public void addPlant(Plant plant)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " INSERT INTO Plants VALUES (@name , @plantstate , @amount);";
            cmd.Parameters.Add("@name", plant.Name);
            cmd.Parameters.Add("@plantstate", plant.Status);
            cmd.Parameters.Add("@amount", plant.WaterAmount);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            plantList.Add(plant);

        }

        public void addUser(User user)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " INSERT INTO Users VALUES (@mail , @pass);";
            cmd.Parameters.Add("@mail", user.Mail);
            cmd.Parameters.Add("@pass", user.Pass);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            userList.Add(user);
        }

        public void addHistory(History history)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " INSERT INTO History VALUES (@plantname , @wateredon);";
            cmd.Parameters.Add("@plantname", history.PlantName);
            cmd.Parameters.Add("@watteredon", history.WateredOn);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            historyList.Add(history);
        }

        public void addSubscriber(Subscriber subscriber)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " INSERT INTO Subscribes VALUES (@mail , @plantname);";
            cmd.Parameters.Add("@mail", subscriber.MailSubscriber);
            cmd.Parameters.Add("@plantname", subscriber.PlantName);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            subscriberList.Add(subscriber);
        }

        public void addUserNotificationOptions(UserNotificationOptions userNotOp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO UserNotificationOptions VALUES(@mail, @ifrom,@ito, @deskT, @mailT,@interval);";
            cmd.Parameters.Add("@mail", userNotOp.Mail);
            cmd.Parameters.Add("@ifrom", userNotOp.IFrom);
            cmd.Parameters.Add("@ito", userNotOp.ITo);
            cmd.Parameters.Add("@mailT", userNotOp.MailToggle);
            cmd.Parameters.Add("@deskT", userNotOp.DesktopToggle);
            cmd.Parameters.Add("@interval", userNotOp.Interval);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            userNotificationOptionsList.Add(userNotOp);
        }

        public void updatePlant(Plant oldPlant, Plant newPlant)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Plants set Name=@plantName, PlantState=@status, WaterAmount=@waterAmount where Name=@oldPlantName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@oldPlantName", oldPlant.Name);
            cmd.Parameters.Add("@plantName", newPlant.Name);
            cmd.Parameters.Add("@status", newPlant.Status);
            cmd.Parameters.Add("@waterAmount", newPlant.WaterAmount);
            cmd.ExecuteNonQuery();
            con.Close();

            int index = plantList.IndexOf(oldPlant);
            if (index != -1)
            {
                plantList[index] = newPlant;
            }

        }

        public void updateUser(User oldUser, User newUser)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Users set Mail=@mail, Pass=@pass where Mail=@oldUserMail";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@oldUserMail", oldUser.Mail);
            cmd.Parameters.Add("@mail", newUser.Mail);
            cmd.Parameters.Add("@pass", newUser.Pass);
            cmd.ExecuteNonQuery();
            con.Close();

            int index = userList.IndexOf(oldUser);
            if (index != -1)
            {
                userList[index] = newUser;
            }
        }

        public void updateSubscriber(Subscriber oldSubscriber, Subscriber newSubscriber)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Subscribes set MailSubscriber=@mail, PlantName=@name where MailSubscriber=@oldSubscriberMail";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@oldSubscriberMail", oldSubscriber.MailSubscriber);
            cmd.Parameters.Add("@mail", newSubscriber.MailSubscriber);
            cmd.Parameters.Add("@name", newSubscriber.PlantName);
            cmd.ExecuteNonQuery();
            con.Close();

            int index = subscriberList.IndexOf(oldSubscriber);
            if (index != -1)
            {
                subscriberList[index] = newSubscriber;
            }
        }


        public void updateHistory(History oldHistory, History newHistory)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE History set PlantName=@name, WateredOn=@wateredon where PlantName=@oldPlantName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@oldPlantName", oldHistory.PlantName);
            cmd.Parameters.Add("@name", newHistory.PlantName);
            cmd.Parameters.Add("@wateredon", newHistory.WateredOn);
            cmd.ExecuteNonQuery();
            con.Close();

            int index = historyList.IndexOf(oldHistory);
            if (index != -1)
            {
                historyList[index] = newHistory;
            }
        }

        public void updateUserNotificationOptions(UserNotificationOptions oldUserNotOp, UserNotificationOptions newUserNotOp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE UserNotificationOptions set Mail=@mail, IFrom=@ifrom, ITo=@ito, MailToggle=@mailT, DesktopToggle=@deskT, Interval=@interval where Mail=@oldMail";
            cmd.Parameters.Add("@mail", newUserNotOp.Mail);
            cmd.Parameters.Add("@ifrom", newUserNotOp.IFrom);
            cmd.Parameters.Add("@ito", newUserNotOp.ITo);
            cmd.Parameters.Add("@mailT", newUserNotOp.MailToggle);
            cmd.Parameters.Add("@deskT", newUserNotOp.DesktopToggle);
            cmd.Parameters.Add("@interval", newUserNotOp.Interval);
            cmd.Parameters.Add("@oldMail", oldUserNotOp.Mail);
            cmd.ExecuteNonQuery();
            con.Close();
            int index = userNotificationOptionsList.IndexOf(oldUserNotOp);
            if (index != -1)
            {
                userNotificationOptionsList[index] = newUserNotOp;
            }
        }


    }
}