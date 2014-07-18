using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace test1connect
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
           SerialPort port = new SerialPort("COM3", 9600);
            port.Open();
            while (true)
            {
                  
                string s1 = port.ReadLine();
                Console.WriteLine(s1);
              //  MessageBox.Show(s1);
                Thread.Sleep(500);  
                
            }
         */
            ReadArduino ra1 = new ReadArduino();

            while (true)
            {
                Thread.Sleep(1000);
                string s2=ra1.getData();
                Console.WriteLine(s2);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
          
                     
        }
    }
}
