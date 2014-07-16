/*
 * This is the working code  for the tutorial found here:
 * http://codeabout.wordpress.com/2011/03/06/building-a-simple-server-client-application-using-c/
*/

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
        private static IPAddress ipAddress = IPAddress.Parse("10.33.92.16");

        //The port
        private static Int32 myPort = 8021;


       private static TcpClient clientSocket=default(TcpClient);

       private static int counter = 0;

       private static TcpListener socketServer = new TcpListener(ipAddress, myPort);

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
              
                socketServer.Start(); //Start at that port

                //Write some status info
                Console.WriteLine("Server running on port " + myPort);
                Console.WriteLine("Local end point: " + socketServer.LocalEndpoint);
                Console.WriteLine("Waiting for clients...");

                while (true)
                {
                    counter=counter+1;
                    //Accept a connection from a client
                    clientSocket = socketServer.AcceptTcpClient();
                    string clientIPAddress = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();
                    Console.WriteLine("Connection accepted from " + clientIPAddress);

                      handleClinet client = new handleClinet();
                 client.startClient(clientSocket, Convert.ToString(counter));
            
                    /*
                    byte[] b = new byte[100];
                    int k = clientSocket.Receive(b);
                    Console.WriteLine("Recieved...");

                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(Convert.ToChar(b[i]));
                    }
                    ASCIIEncoding asen = new ASCIIEncoding();
                    clientSocket.Send(asen.GetBytes("Automatic message: " + "String received by server!"));
                    Console.WriteLine("\n Automatic message sent!");
                    */
                }
                    //Close the socket
              
                clientSocket.Close();
                socketServer.Stop();
                Console.WriteLine(" >> " + "exit");
                Console.ReadLine();

            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                socketServer.Stop();
                Console.ReadKey();
            }
            
        }
    }
}
