using System;
using System.IO;
using System.Linq;

namespace HTTP_Web_Server
{
    class Program
    {
        
        public int PT = 30;


        static void Main(string[] args)
        {
            string Set = "Settings.ini";

            HTTPServer.SetTimer();
            int PT = 0;
            string line = File.ReadLines(Set).Skip(4).Take(1).First();
            string dir = File.ReadLines(Set).Skip(7).Take(1).First();
            string msgdir = File.ReadLines(Set).Skip(10).Take(1).First();
            string logdir = File.ReadLines(Set).Skip(13).Take(1).First();
            string Logbool = File.ReadLines(Set).Skip(16).Take(1).First();
            bool logbool = bool.Parse(Logbool);
            HTTPServer.Lbool = logbool;

            if (dir == "")
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

            if (msgdir == "")
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

            if (logdir == "")
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

            if (!logbool)
            {
                Console.WriteLine("Logging is disabled");
            }
            else
            {
                Console.WriteLine("Logging is enabled");
            }

            PT = int.Parse(line);

            Console.WriteLine("press ESC key to exit");
            Console.WriteLine("$ Server starting");
            HTTPServer server = new HTTPServer(PT);
            server.start();
            Console.WriteLine("$ Server started on port :" + PT);

            if(Console.ReadKey().Key == ConsoleKey.Escape)
            {
                server.stop();
            }

        }
    }
}
