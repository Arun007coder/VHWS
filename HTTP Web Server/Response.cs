using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP_Web_Server
{
    

    public class Response
    {
        private Byte[] data = null;
        private string status;
        private string mime;
        public static bool isCMDED;

        



        private Response(string status, string mime, Byte[] data)
        {
            this.data = data;
            this.status = status;
            this.mime = mime;
        }

        public static Response From(Request request)
        {
            if (request == null)
            {
                return MakeNullRequest();
            }

            if (request.Type == "EXT" || request.Type == "GET" || request.Type == "CMD1" || request.Type == "CMD2" || request.Type == "CMD3" || request.Type == "CLR1" || request.Type == "CLR2" || request.Type == "CLR3" || request.Type == "CMDED1" || request.Type == "CMDED2" || request.Type == "CMDED3")
            {
                if (request.Type == "GET")
                {
                    String file = HTTPServer.WEB_DIR + request.URL;
                    Console.WriteLine(file);
                    FileInfo f = new FileInfo(file);
                    if (f.Exists && f.Extension.Contains("."))
                    {
                        return MakeFromFile(f);
                    }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(f + "/");
                        if (!di.Exists)
                            return MakePageNotFound();
                        FileInfo[] files = di.GetFiles();
                        foreach (FileInfo ff in files)
                        {
                            string n = ff.Name;
                            if (n.Contains("default.html") || n.Contains("default.htm") || n.Contains("index.htm") || n.Contains("index.html") || n.Contains("Index.htm") || n.Contains("Index.html") || n.Contains("Default.htm") || n.Contains("Default.html"))
                                return MakeFromFile(ff);
                        }
                    }

                    if (f.Exists == false)
                        return MakePageNotFound();

                }

                if (HTTPServer.CMDBool) {

                    if (isCMDED)
                    {
                        if (request.Type == "CMDED1")
                        {
                            if (HTTPServer.CMDEDIN6.Contains("CMD1"))
                            {
                                string CMDED11 = CharRem(HTTPServer.CMDEDIN6).Replace("CMD1" , " ").Replace("slash", "/").Replace("andd" , "&&").Replace("SPC", " ").Replace("COL" , ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED11);
                                HTTPServer.CMDET(0, CMDED11);
                            }

                            if (HTTPServer.CMDEDIN7.Contains("CMD1"))
                            {
                                string CMDED12 =CharRem(HTTPServer.CMDEDIN7).Replace("CMD1" , " ").Replace("slash", "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine( "Command is " + CMDED12);
                                HTTPServer.CMDET(0, CMDED12);
                            }

                            if (HTTPServer.CMDEDIN5.Contains("CMD1"))
                            {
                                string CMDED13 = CharRem(HTTPServer.CMDEDIN5).Replace("CMD1", " ").Replace("slash" , "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine( "command is " + CMDED13);
                                HTTPServer.CMDET(0, CMDED13);
                            }

                            return MakeCustomres("CMDED1");
                        }

                        if (request.Type == "CMDED2")
                        {
                            if (HTTPServer.CMDEDIN6.Contains("CMD2"))
                            {
                                string CMDED21 = CharRem(HTTPServer.CMDEDIN6).Replace("CMD2", " ").Replace("slash", "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED21);
                                HTTPServer.CMDET(1, CMDED21);
                            }

                            if (HTTPServer.CMDEDIN7.Contains("CMD2"))
                            {
                                string CMDED22 = CharRem(HTTPServer.CMDEDIN7).Replace("CMD2", " ").Replace("slash", "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED22);
                                HTTPServer.CMDET(1, CMDED22);
                            }

                            if (HTTPServer.CMDEDIN5.Contains("CMD2"))
                            {
                                string CMDED23 = CharRem(HTTPServer.CMDEDIN5).Replace("CMD2", " ").Replace("slash", "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED23);
                                HTTPServer.CMDET(1, CMDED23);
                            }

                            return MakeCustomres("CMDED2");
                        }

                        if (request.Type == "CMDED3")
                        {
                            if (HTTPServer.CMDEDIN6.Contains("CMD3"))
                            {
                                string CMDED31 = CharRem(HTTPServer.CMDEDIN6).Replace("CMD3" , string.Empty).Replace("slash", "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED31);
                                HTTPServer.CMDET(2, CMDED31);
                            }

                            if (HTTPServer.CMDEDIN7.Contains("CMD3"))
                            {
                                string CMDED32 = CharRem(HTTPServer.CMDEDIN7).Replace("CMD3", string.Empty).Replace("slash", "/").Replace("andd", "&&").Replace("SPC", " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED32);
                                HTTPServer.CMDET(2, CMDED32);
                            }

                            if (HTTPServer.CMDEDIN5.Contains("CMD3"))
                            {
                                string CMDED33 = CharRem(HTTPServer.CMDEDIN5).Replace("CMD3", string.Empty).Replace("slash", "/").Replace("andd", "&&").Replace("SPC" , " ").Replace("COL", ":").Replace("HYP", "-");
                                Console.WriteLine("Command is " + CMDED33);
                                HTTPServer.CMDET(2, CMDED33);
                            }

                            return MakeCustomres("CMDED3");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The client is trying to change the commands in slots");

                    }

                    if (request.Type == "CMD1")
                    {
                        HTTPServer.CMD(0);
                        return MakeCustomres("CMD1");
                    }

                    if (request.Type == "CMD2")
                    {
                        HTTPServer.CMD(1);
                        return MakeCustomres("CMD2");
                    }

                    if (request.Type == "CMD3")
                    {
                        HTTPServer.CMD(2);
                        return MakeCustomres("CMD3");
                    }

                    if (request.Type == "CLR1")
                    {
                        HTTPServer.CLR(0);
                        return MakeCustomres("CLR1");
                    }

                    if (request.Type == "CLR2")
                    {
                        HTTPServer.CLR(1);
                        return MakeCustomres("CLR2");
                    }

                    if (request.Type == "CLR3")
                    {
                        HTTPServer.CLR(2);
                        return MakeCustomres("CLR3");
                    }

                    if (request.Type == "EXT")
                    {
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(3000);
                            HTTPServer.stop();
                            Environment.Exit(3);
                        });
                        return MakeCustomres("EXT");

                    }
                }
                else
                {
                    Console.WriteLine("Someone is trying to use Commands without permission");
                }
            }
            else
            {
                return MakeMethodNotAllowed();
            }
            return MakePageNotFound();
        }

        private static string CharRem(string _char)
        {
            var str = _char;
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", ":" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }

            return str;
        }

        private static Response MakeFromFile(FileInfo f)
        {
            FileStream FS = f.OpenRead();
            BinaryReader BR = new BinaryReader(FS);
            Byte[] d = new Byte[FS.Length];
            BR.Read(d, 0, d.Length);
            FS.Close();
            return new Response("200 ok", "text/html", d);
        }

        public static Response MakeCustomres(string _reqmsg)
        {
            string file = HTTPServer.MSG_DIR + "404.htm";
            FileInfo FI = new FileInfo(file);
            FileStream FS = FI.OpenRead();
            BinaryReader BR = new BinaryReader(FS);
            Byte[] d = new Byte[FS.Length];
            BR.Read(d, 0, d.Length);
            FS.Close();
            return new Response("1089" + " " + _reqmsg, "text/html", d);
            Console.WriteLine("Custom request is surcessfully working");

        }

        private static Response MakeNullRequest()
        {
            string file = HTTPServer.MSG_DIR + "400.htm";
            FileInfo FI = new FileInfo(file);
            FileStream FS = FI.OpenRead();
            BinaryReader BR = new BinaryReader(FS);
            Byte[] d = new Byte[FS.Length];
            BR.Read(d, 0, d.Length);
            FS.Close();
            return new Response("400 Bad Request", "text/html", d);
        }

        private static Response MakePageNotFound()
        {
            string file = HTTPServer.MSG_DIR + "404.htm";
            Console.WriteLine(file);
            FileInfo FI = new FileInfo(file);
            FileStream FS = FI.OpenRead();
            BinaryReader BR = new BinaryReader(FS);
            Byte[] d = new Byte[FS.Length];
            BR.Read(d, 0, d.Length);
            FS.Close();
            return new Response("404 Page not found", "text/html", d);
        }

        private static Response MakeMethodNotAllowed()
        {
            string file = HTTPServer.MSG_DIR + "405.htm";
            FileInfo FI = new FileInfo(file);
            FileStream FS = FI.OpenRead();
            BinaryReader BR = new BinaryReader(FS);
            Byte[] d = new Byte[FS.Length];
            BR.Read(d, 0, d.Length);
            FS.Close();
            return new Response("405 - HTTP verb used to access this page is not allowed", "text/html", d);
        }

        public void Post(NetworkStream stream)
        {

            try 
            {
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(string.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n",
                    HTTPServer.VERSION, status, HTTPServer.NAME, mime, data.Length));
                using (StreamWriter sw = File.CreateText("CLog.txt"))
                {
                    sw.WriteLine(string.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n",
                    HTTPServer.VERSION, status, HTTPServer.NAME, mime, data.Length));
                    sw.Close();
                }
                writer.Flush();
                stream.Write(data, 0, data.Length);
            }
            catch (IOException e)
            {
                Console.WriteLine("S001 :Connection was interuppted by host");
            }

        }


    } 
}


