using System;
using System.IO;
using System.Linq;

namespace HTTP_Web_Server
{
    class Program
    {
        


        static void Main(string[] args)
        {
            string Set = "Settings.ini"; // Settings file

            HTTPServer.SetTimer();
            int EPT;
            bool CanPF;
            string line = File.ReadLines(Set).Skip(7).Take(1).First();
            string dir = File.ReadLines(Set).Skip(10).Take(1).First();
            string msgdir = File.ReadLines(Set).Skip(13).Take(1).First();
            string logdir = File.ReadLines(Set).Skip(16).Take(1).First();
            string Logbool = File.ReadLines(Set).Skip(19).Take(1).First();
            string ELine = File.ReadLines(Set).Skip(25).Take(1).First();
            string Pbool = File.ReadLines(Set).Skip(22).Take(1).First();
            string ipa = File.ReadLines(Set).Skip(28).Take(1).First();

            bool logbool = bool.Parse(Logbool);
            bool UPNPF = bool.Parse(Pbool);
            int PT = int.Parse(line);
            HTTPServer.Lbool = logbool;

            

            Console.WriteLine("press ESC key to stop the server");

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

            if(ipa == "" || ipa == "192.168.1.1" || ipa == "0.0.0.0")
            {
                Console.WriteLine("IP address specified is invalid");
                CanPF = false;
            }
            else
            {
                HTTPServer.IPA = ipa;
                CanPF = true;
                PortForward.IIPPort = ipa;
            }

            

            if(Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Stopping server");
                server.stop();
                Console.WriteLine("Server stopped");
            }

        }
    }
}
