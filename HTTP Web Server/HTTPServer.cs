﻿using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Timers;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HTTP_Web_Server
{
    public class HTTPServer
    {
        public const string VERSION = "HTTP/1.1";
        public const string NAME = "C# web server v2.0.2";
        public static string MSG_DIR;
        public static string WEB_DIR;
        public static string LOG_DIR;
        public static string DEF_WEB_DIR = Environment.CurrentDirectory + "/Root/web/";
        public static string DEF_MSG_DIR = Environment.CurrentDirectory + "/Root/msg/";
        public static string DEF_LOG_DIR = Environment.CurrentDirectory + "/Root/logs/";
        public static string SET_WEB_DIR;
        public static string SET_MSG_DIR;
        public static string SET_LOG_DIR;
        public static bool Lbool;

        private int Port;
        public static System.Timers.Timer timer;
        private bool isRunning = false;
        public static string time;
        public static string FileName;
        public string msg;
        public static string nme = " Log.txt";
        public static bool isHacking = false;

        private TcpListener TL;


        public static void SetTimer()
        {
            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(1000);
            timer.Enabled = true;
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += TimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public static void TimedEvent(object source, ElapsedEventArgs l)
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy hh:mm.ss tt");
            FileName = time + nme;
        }

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



                try
                {

                    if (!isHacking)
                    {
                        Console.WriteLine("Waiting for connection ...");

                        TcpClient client = TL.AcceptTcpClient();

                        Console.WriteLine("Client connected!");

                        HandleClient(client);

                        client.Close();
                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(30000);

                            isHacking = false;

                            Console.WriteLine("Waiting for connection ...");

                            TcpClient client = TL.AcceptTcpClient();

                            Console.WriteLine("Client connected!");

                            HandleClient(client);

                            client.Close();
                        });
                    }

                    
                }
                catch(IndexOutOfRangeException e)
                {
                    isHacking = true;
                    nme = "Hacklog.txt";
                    log(LOG_DIR + FileName, e + "Someone is trying to hack the server or trying to use nikto");
                    Console.WriteLine("Someone is trying to hack the server or trying to use nikto");
                    Task.Factory.StartNew(() =>
                    {
                        log(LOG_DIR + FileName, e + "Someone is trying to hack the server or trying to use nikto");
                        System.Threading.Thread.Sleep(8000);
                        //stop();
                    });
                }
                
            }

            isRunning = false;

            TL.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            string msg = "";
            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
                if(Lbool == true)
                {
                    log(LOG_DIR + FileName, msg);
                }
            }

            Debug.WriteLine("$ Request: \n" + msg);
            Console.WriteLine("$ Request: \n" + msg);

            Request req = Request.GetRequest(msg);
            Response resp = Response.From(req);
            resp.Post(client.GetStream());

        }

        public void log(string _file, string _res)
        {
            string dir = LOG_DIR;
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string Log = "$ Request : \n" + _res;
            using (StreamWriter sw = File.CreateText(_file))
            {
                sw.WriteLine(Log);
                sw.Close();
            }
        }

        
    }
}
