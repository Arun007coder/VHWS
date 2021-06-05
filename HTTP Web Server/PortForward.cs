using System;
using NATUPNPLib;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace HTTP_Web_Server
{
    class PortForward
    {
        static UPnPNATClass UPNP = new UPnPNATClass();
        static IStaticPortMappingCollection map = UPNP.StaticPortMappingCollection;
        private static string SNAME = "HTTP Web server";
        private static string HNAme = Dns.GetHostName();
        private static IPAddress[] ips = Dns.GetHostAddresses(HNAme);
        private static  string[] IIP = ips.Select(ip => ip.ToString()).ToArray();
        private static string IIPS = "";
        private static int PRT;
        private static int EPRT;
        static bool lol = true;
        public static string IIPPort = "";


        public static void Makeport(int _Iport, int _Eport)
        {
            PRT = _Iport;
            EPRT = _Eport;

                if (IIPPort.Contains("192.168.1."))
                {
                    IIPS = IIPPort;
                    Console.WriteLine(IIPS);

                }


            if (UPNP == null)
            {
                Console.WriteLine("Initialization failed creating Windows UPnPNAT interface.");
                return;
            }

            if (map == null)
            {
                Console.WriteLine("No mappings found. Do you have a uPnP enabled router as your gateway ? ");
                return;
            }

            if(map.Count == 0)
            {
                map.Add(EPRT, "TCP", PRT, IIPS, true, "HTTP Web server");
                Console.WriteLine("The local port " + _Iport + " is being port forwarded to " + _Eport + " from internal ip " + IIPS + ". Use canyouseeme.org to find the port is forwarded");
            }
            else
            {
                map.Remove(EPRT, "TCP");
            }

            


        }

        public static void REMport(int _Eport)
        {
            map.Remove(_Eport, "TCP");
        }

    }
}
