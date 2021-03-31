using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace HTTP_Web_Server
{
    public class HTTPServer
    {
        public const string VERSION = "HTTP/1.1";
        public const string NAME = "C# web server v1.1";
        public const string MSG_DIR = "/Root/msg/";
        public const string WEB_DIR = "/Root/web/";

        private int Port;
        private bool isRunning = false;
        public string Log;
        public string FileName = "Log.txt";
        public string msg;

        private TcpListener TL;

        public HTTPServer(int _port)
        {
            Port = _port;
            TL = new TcpListener(IPAddress.Any, Port);
        }

        public void start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        public void stop()
        {
            Environment.Exit(0);
        }

        private void Run()
        {
            isRunning = true;
            TL.Start();

            while (isRunning == true)
            {
                Console.WriteLine("$ Waiting for Connection...");

                TcpClient client = TL.AcceptTcpClient();

                Console.WriteLine("$ Client connected!");

                HandleClient(client);

                client.Close();
            }

            isRunning = false;

            TL.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            string msg = "";
            while(reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
                log(FileName, msg);
            }

            Debug.WriteLine("$ Request: \n" + msg);
            Console.WriteLine("$ Request: \n" + msg);

            Request req = Request.GetRequest(msg);
            Response resp = Response.From(req);
            resp.Post(client.GetStream());

        }

        public void log(string _file, string _res)
        {
            string Log = "$ Request : \n" + _res;
            using (StreamWriter sw = File.CreateText(_file))
            {
                sw.WriteLine(Log);
                sw.Close();
            }
        }

    }
}
