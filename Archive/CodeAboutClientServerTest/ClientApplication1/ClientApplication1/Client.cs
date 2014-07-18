/*
 * This is the working code  for the tutorial found here:
 * http://codeabout.wordpress.com/2011/03/06/building-a-simple-server-client-application-using-c/
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ClientApplication1
{
    class Client
    {

        //The server ip address 
        private static IPAddress ipAddress = IPAddress.Parse("10.33.90.52");

        //The server port
        private static Int32 myPort = 8021;

        //The TCP Client object
        private static TcpClient tcpclnt;

        static void Main(string[] args)
        {

            //Start the connection to client
            Thread t = new Thread(startConnection);
            t.Start();

        }

        //Thread fucntion that connects to the server
        static void startConnection()
        {

            //All code in a try catch
            try
            {
                tcpclnt = new TcpClient();
                Console.WriteLine("Connecting...");

                tcpclnt.Connect(ipAddress, myPort);
                Console.WriteLine("Connected.");
                Console.WriteLine("Enter the string to be sent:");

                String str = Console.ReadLine();
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Sending...");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);

                for (int i = 0;i < k; i++)
                {
                    Console.Write(Convert.ToChar(bb[i]));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                tcpclnt.Close();
                Console.ReadKey();
            }

        }
    }
}
