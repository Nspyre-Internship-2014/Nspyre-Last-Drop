using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Forms;

namespace LastDropMainServer
{
    class ReadArduino
    {
        private static string data = "-1";
        private static int refreshInterval;
        private static string serialPort;

        public ReadArduino(int refreshIntervalOption, string serialPortOption)
        {
            refreshInterval = refreshIntervalOption;
            serialPort = serialPortOption;


            Task task = new Task(readData);
            task.ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
            task.Start();
            
        }

        private static void readData()
        {
            SerialPort port = new SerialPort(serialPort, 9600);
            try
            {
                 port.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Arduino board needs to be connected.");
                Environment.Exit(1);
            }
            while (true)
            {
                data = port.ReadLine();
                Thread.Sleep(refreshInterval);
            }
        }

        static void ExceptionHandler(Task task)
        {
            var exception = task.Exception;
            Debug.WriteLine(exception);
        }

        public string getData()
        {
            return data;
        }
    }
}
