using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LastDropDBOperations
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Start the connection
            DatabaseController databaseController = new DatabaseController("Data Source=LPT0902\\SQLEXPRESS;Initial Catalog=LastDropDB;Integrated Security=true");

            //Get a plant and print its status
            Plant testPlant = databaseController.getPlantByName("Jon");
            MessageBox.Show("The plant's status is " + testPlant.Status);

        }
    }
}
