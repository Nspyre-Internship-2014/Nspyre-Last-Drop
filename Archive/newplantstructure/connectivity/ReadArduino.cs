using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;

namespace LastDropMainServer
{
    class ReadArduino
    {
        private static string data;
        private static int refreshInterval;
        private static string serialPort;

        public ReadArduino(int refreshIntervalOption, string serialPortOption)
        {
            refreshInterval = refreshIntervalOption;
            serialPort = serialPortOption;

            Thread t = new Thread(readData);
            t.Start();
        }

        private static void readData()
        {
            SerialPort port = new SerialPort(serialPort, 9600);
            port.Open();
            while (true)
            {
                data = port.ReadLine();
                Thread.Sleep(refreshInterval);
            }
        }

        public string getData()
        {
            return data;
        }
    }
}
