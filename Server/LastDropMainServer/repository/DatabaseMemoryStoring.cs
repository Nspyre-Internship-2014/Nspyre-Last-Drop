using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LastDropMainServer
{
    class DatabaseMemoryStoring
    {
        public List<Plant> StoreInClassPlant(SqlConnection con)
        {
            List<Plant> plants = new List<Plant>();

            con.Open();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from Plants";
            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Plant plant = new Plant(dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), Int32.Parse(dr.GetValue(2).ToString()));
                plants.Add(plant);
            }

            dr.Close();
            con.Close();

            return plants;
        }

        public List<User> StoreInClassUser(SqlConnection con)
        {
            List<User> users = new List<User>();

            con.Open();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from Users";
            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                User user = new User(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());
                users.Add(user);
            }

            dr.Close();
            con.Close();

            return users;
        }

        public List<Subscriber> StoreInClassSubscriber(SqlConnection con)
        {
            List<Subscriber> subscribers = new List<Subscriber>();

            con.Open();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from Subscribes";
            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Subscriber subscriber = new Subscriber(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());
                subscribers.Add(subscriber);
            }

            dr.Close();
            con.Close();

            return subscribers;
        }


        public List<History> StoreInClassHistory(SqlConnection con)
        {
            List<History> histories = new List<History>();

            con.Open();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from History";
            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                History history = new History(dr.GetValue(0).ToString(), Convert.ToDateTime(dr.GetValue(1).ToString()));
                histories.Add(history);
            }

            dr.Close();
            con.Close();

            return histories;
        }

        public List<UserNotificationOptions> storeInClassUserNotificationOptions(SqlConnection con)
        {
            List<UserNotificationOptions> userNotOptList = new List<UserNotificationOptions>();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            cmd.CommandText = "select * from UserNotificationOptions";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserNotificationOptions userNotOp = new UserNotificationOptions(dr.GetValue(0).ToString(), (TimeSpan)dr.GetTimeSpan(1), (TimeSpan)dr.GetTimeSpan(2), dr.GetBoolean(3), dr.GetBoolean(4), Int32.Parse(dr.GetValue(5).ToString()));
                userNotOptList.Add(userNotOp);
            }
            dr.Close();
            con.Close();
            return userNotOptList;

        }
    }
}
