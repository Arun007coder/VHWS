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

            

            
            HTTPServer.SetTimer();
            string CMD_REQ_EN = File.ReadLines(Set).Skip(29).Take(1).First();
            string CMD_REQ_1 = ReadSET(Set, 32);
            string CMD_REQ_2 = ReadSET(Set, 35);
            string CMD_REQ_3 = ReadSET(Set, 38);
            string line = File.ReadLines(Set).Skip(8).Take(1).First();
            string dir = File.ReadLines(Set).Skip(11).Take(1).First();
            string msgdir = File.ReadLines(Set).Skip(14).Take(1).First();
            string logdir = File.ReadLines(Set).Skip(17).Take(1).First();
            string Logbool = File.ReadLines(Set).Skip(20).Take(1).First();
            string Pbool = File.ReadLines(Set).Skip(23).Take(1).First();
            string ELine = File.ReadLines(Set).Skip(26).Take(1).First();
            bool CanPF;
            bool logbool = bool.Parse(Logbool);
            bool UPNPF = bool.Parse(Pbool);
            int PT = int.Parse(line);
            int EPT;
            HTTPServer.CMDBool = bool.Parse(CMD_REQ_EN);
            HTTPServer.Lbool = logbool;
            HTTPServer.CMD1 = CMD_REQ_1;
            HTTPServer.CMD2 = CMD_REQ_2;
            HTTPServer.CMD3 = CMD_REQ_3;


            if (args.Length > 0)
            {
                if (args[1] == "-h")
                {
                    Console.WriteLine("C# HTTP Web Server " + HTTPServer.NAME);
                    Console.WriteLine("  ");
                    Console.WriteLine("-h :To get the arguments of the program ");
                    Console.WriteLine("  ");
                    Console.WriteLine("-sc :To start the server with the configuration of settings file");
                    Console.WriteLine("  ");
                    Console.WriteLine("-spf <EPort> :To start the server with port forwarding. Args <Eport>:External port");
                    Console.WriteLine("  ");
                    Console.WriteLine("-s <Iport> <WEBDir> <ERRDir> <PRTFWD> <Eport> :To start the server with user configuration. Args <Iport>:Internal port <WEBDir>:The Directory where web pages are stored <ERRDir>:The directory where Error pages are stored <PRTFWD>:true or false wheather to port forward or not <Eport>:External port if port forward is enabled ");
                }

                if(args[0] == "-sc")
                {
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

                    HTTPServer serve = new HTTPServer(PT);

                    Console.WriteLine("$ Server starting");
                    serve.start();
                    Console.WriteLine("$ Server started on port :" + PT);

                    if(Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Stopping server");
                        HTTPServer.stop();
                        Console.WriteLine("Server stopped");
                    }

                }


            }

            


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

            if(Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Stopping server");
                HTTPServer.stop();
                Console.WriteLine("Server stopped");
            }

            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                PortForward.REMport(int.Parse(ELine));
                PortForward.Makeport(PT, int.Parse(ELine));
                Console.WriteLine("Request sussceded");
            }

        }

        private static string ReadSET(string _File , int _skip)
        {
            return File.ReadLines(_File).Skip(_skip).First();
        }
    }
}
