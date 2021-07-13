using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System.Configuration;
using Zadnja;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("Server"))
            {
                system.ActorOf(Props.Create(()=> new AktorServer()),"AktorServer");
                Console.ReadLine();
                //system.Terminate().Wait();
            }       
        }
    }
}
