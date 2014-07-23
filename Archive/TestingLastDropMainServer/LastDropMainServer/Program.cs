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
            SensorDataCollection sensorDataCollection = new SensorDataCollection(controller, new TimeSpan(0, 0, 10));
            
            //Run the main app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainGUI(controller));
            
        }
    }
}

/*        
          try
          {
              while (true)
              {
                  SensorDataCollection sensorDataCollection = new SensorDataCollection(controller, new TimeSpan(0, 10, 0));
                  break;
              }
          }
          catch (Exception e)
          {
              MessageBox.Show("The arduino board is not connected to this computer. Please reconnect it and resume the program.");
          }
           */