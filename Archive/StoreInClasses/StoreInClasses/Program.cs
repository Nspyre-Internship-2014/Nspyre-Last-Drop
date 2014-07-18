using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StoreInClasses
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            SqlConnection con = new SqlConnection();
            con.ConnectionString="Data Source=ANDRADA-PC\BD;Initial Catalog=internship;Integrated Security=true";

            Store store=new Store();

            List<Plant> plants=new List<Plant>();
            plants=store.StoreInClassPlant(con);
            Console.WriteLine("Plants:");
            foreach(Plant plant in plants)
                Console.WriteLine(plant.ToString());

            List<User> users=new List<User>();
            users=store.StoreInClassUser(con);
            Console.WriteLine("Users:");
            foreach(User user in users)
                Console.WriteLine(user.ToString());

            List<Subscriber> subscribers=new List<Subscriber>();
            subscribers=store.StoreInClassSubscriber(con);
            Console.WriteLine("Subscribers:");
            foreach(Subscriber subscriber in subscribers)
                Console.WriteLine(subscriber.ToString());

            List<History> histories=new List<History>();
            histories=store.StoreInClassHistory(con);
            Console.WriteLine("History:");
            foreach(History history in histories)
                Console.WriteLine(history.ToString());


        }
    }
}
