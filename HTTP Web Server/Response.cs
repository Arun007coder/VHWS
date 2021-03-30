using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HTTP_Web_Server
{
    

    public class Response
    {
        private Byte[] data = null;
        private string status;
        private string mime;



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

            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + HTTPServer.WEB_DIR + request.URL;
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
            else
            {
                return MakeMethodNotAllowed();
            }
            return MakePageNotFound();

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

        private static Response MakeNullRequest()
        {
            string file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.htm";
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
            string file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "404.htm";
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
            string file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "405.htm";
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


            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(string.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n",
                HTTPServer.VERSION, status, HTTPServer.NAME, mime, data.Length));
            writer.Flush();
            stream.Write(data, 0, data.Length);

        }


    } 
}


