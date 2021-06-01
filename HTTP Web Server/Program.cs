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

            if (dir == "")
            {
                HTTPServer.WEB_DIR = HTTPServer.DEF_WEB_DIR;
            }
            else
            {
                HTTPServer.SET_WEB_DIR = dir;
                HTTPServer.WEB_DIR = HTTPServer.SET_WEB_DIR;
            }

            if (msgdir == "")
            {
                HTTPServer.MSG_DIR = HTTPServer.DEF_MSG_DIR;

            }
            else
            {
                HTTPServer.SET_MSG_DIR = msgdir;
                HTTPServer.MSG_DIR = HTTPServer.SET_MSG_DIR;
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
