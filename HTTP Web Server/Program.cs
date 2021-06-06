using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HTTP_Web_Server
{
    class Program
    {
        


        static void Main(string[] args)
        {
            string Set = "Settings.ini"; // Settings file

            Random rnd = new Random();
            string SPT = File.ReadLines(Environment.CurrentDirectory + "/Docs/Fun.txt").Skip(rnd.Next(0, 18)).Take(1).First();
            HTTPServer.SetTimer();
            int PT = 30;
            string line = File.ReadLines(Set).Skip(5).Take(1).First();
            string dir = File.ReadLines(Set).Skip(8).Take(1).First();
            string msgdir = File.ReadLines(Set).Skip(11).Take(1).First();
            string logdir = File.ReadLines(Set).Skip(14).Take(1).First();
            string Logbool = File.ReadLines(Set).Skip(17).Take(1).First();
            bool logbool = bool.Parse(Logbool);
            bool UPNPF = bool.Parse(Pbool);
            int PT = int.Parse(line);
            HTTPServer.Lbool = logbool;

            

            Console.WriteLine("press ESC key to stop the server");

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            if (dir == "") // To set directory of webpages
            {
                HTTPServer.WEB_DIR = HTTPServer.DEF_WEB_DIR;
                Console.WriteLine("Web root directory is set to " + HTTPServer.DEF_WEB_DIR);
            }
            else
            {
                HTTPServer.SET_WEB_DIR = dir;
                HTTPServer.WEB_DIR = HTTPServer.SET_WEB_DIR;
                Console.WriteLine("Web root directory is set to " + HTTPServer.SET_WEB_DIR);
            }

            if (msgdir == "") // To set directory of error pages
            {
                HTTPServer.MSG_DIR = HTTPServer.DEF_MSG_DIR;
                Console.WriteLine("Msg root directory is set to " + HTTPServer.DEF_MSG_DIR);

            }
            else
            {
                HTTPServer.SET_MSG_DIR = msgdir;
                HTTPServer.MSG_DIR = HTTPServer.SET_MSG_DIR;
                Console.WriteLine("Msg root directory is set to " + HTTPServer.SET_MSG_DIR);
            }

            if (logdir == "") // To set directory of logs
            {
                HTTPServer.LOG_DIR = HTTPServer.DEF_LOG_DIR;
                Console.WriteLine("Log root directory is set to " + HTTPServer.DEF_LOG_DIR);
            }
            else
            {
                HTTPServer.SET_LOG_DIR = logdir;
                HTTPServer.LOG_DIR = HTTPServer.SET_LOG_DIR;
                Console.WriteLine("Log root directory is set to " + HTTPServer.SET_LOG_DIR);
            }

            if (!logbool) // To set logging is disabled or enabled
            {
                Console.WriteLine("Logging is disabled");
            }
            else
            {
                Console.WriteLine("Logging is enabled");
            }

            HTTPServer server = new HTTPServer(PT);

            if (UPNPF)
            {
                if (ELine == "" || ELine == "0")
                {
                    Console.WriteLine("External port is not specified in Settings.ini file or you are trying to port forward to 0");
                    CanPF = false;
                }
                else
                {
                    CanPF = true;
                    EPT = int.Parse(ELine);
                    HTTPServer.EPort = EPT;
                    HTTPServer.CanFWD = CanPF;
                }
            }
            else
            {
                Console.WriteLine("Port forwarding is disabled. You have to manually port forward");
            }

            if (line == "" || line == "0" ) // To stop server from listening to 0
            {
                Console.WriteLine("Port is not specified or you are using reserved port 0. Specify a port in which the server should listen in Settings.ini file");
            }
            else
            {
                Console.WriteLine("$ Server starting");
                server.start();
                Console.WriteLine("$ Server started on port :" + PT);
            }

            if(Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Stopping server");
                HTTPServer.stop();
                Console.WriteLine("Server stopped");
            }

            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                PortForward.REMport(90);
                PortForward.Makeport(90, 90);
                Console.WriteLine("Request sussceded");
            }

        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            HTTPServer.stop();
            Console.WriteLine("Server stopped");
        }
    }
}
