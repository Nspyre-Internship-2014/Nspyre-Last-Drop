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
        private static int baudRate = 57600;

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
            SerialPort port = new SerialPort(serialPort, baudRate);
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

        public int getSensorValue()
        {
            int result = 0;
            if (data != "-1")
            {
                string s2 = data.Substring(0, 8);
                if (s2.Substring(s2.Length - 2, 2) == "00")
                    s2 = data.Substring(0, 6);
                if (s2.Substring(s2.Length - 2, 2) == "00")
                    s2 = data.Substring(0, 4);
                if (s2.Substring(s2.Length - 2, 2) == "00")
                    s2 = data.Substring(0, 2);

                int len = s2.Length;
                int i;
                for (i = 0; i <= len - 2; i += 2)
                {
                    string s3 = s2.Substring(i, 2);
                    int conv1 = Convert.ToInt32(s3);
                    int conv2 = conv1 - 48;
                    result = result * 10 + conv2;

                }
            }
            
            return result;
        }

        public int getNodeId()
        {
            if (data != "-1")
            {
                const int ASCIItoINT = 64;
                int len = data.Length;
                string s3 = data.Substring(len - 3);
                int a1 = Convert.ToInt32(s3);
                int result = a1 - ASCIItoINT;
                return result;
            }
            return -1;
        }
    }
}
