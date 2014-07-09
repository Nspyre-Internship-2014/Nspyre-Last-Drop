using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;

namespace test1connect
{
    class ReadArduino
    {
        private static string s1;
        public ReadArduino()
        {
            Thread t = new Thread(readData);      
            t.Start();
        }

      private  static void readData()
        {
            SerialPort port = new SerialPort("COM3", 9600);
            port.Open();
            while (true)
            {

                s1 = port.ReadLine();
              //  Console.WriteLine(s1);
                //  MessageBox.Show(s1);
                Thread.Sleep(1000);

            }
        }
        
      public string getData()
      {
          return s1;
      }
    }
}
