using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace LastDropMainServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Controller with all the required operations
            OperationController controller = new OperationController();

            //Every 10 second, the data collector updates the database
            //SensorDataCollection sensorDataCollection = new SensorDataCollection(controller, new TimeSpan(0, 0, 10));
           
            //Every 5 hours, check if there are any dry plants and send the mails when necessary
            //AutomaticEMailSender autoEmailSender = new AutomaticEMailSender(9999999, controller);


            //Run the main app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainGUI(controller));
            
        }
    }
}
