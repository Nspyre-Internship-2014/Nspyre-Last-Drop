using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerApplication1
{
    class Server
    {

        //The ip address 
        private static IPAddress ipAddress = IPAddress.Parse("10.33.90.52");

        //The port
        private static Int32 myPort = 8021;

        //The client listener
        private static TcpListener myList;

        static void Main(string[] args)
        {

            //Start thread that listens to the clients
            Thread t = new Thread(startListening);
            t.Start();

        }

        //Thread fucntion that listens to the clients
        static void startListening()
        {

            //All code in a try catch
            try
            {
                //Begin listening
                myList = new TcpListener(ipAddress, myPort);
                myList.Start(); //Start at that port

                //Write some status info
                Console.WriteLine("Server running on port " + myPort);
                Console.WriteLine("Local end point: " + myList.LocalEndpoint);
                Console.WriteLine("Waiting for clients...");


                //Accept a connection from a client
                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                
                //Close the socket
                s.Close();

            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                myList.Stop();
                Console.ReadKey();
            }
            
        }
    }
}
