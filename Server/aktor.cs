using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Zadnja;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class AktorServer : ReceiveActor
    {
        public AktorServer()
        {
            Receive<Podaci>(x => HandleContent(x.temperatura, x.vlaznost, x.hodnikVrata, x.glavnaVrataZakljucanaOtkljucana, x.hladnjakVrata, x.alarmProvala));         
        }

        private void HandleContent(int t, int v, int hodnik, int glavna, int hladnjak, int alarm)
        {
            string zaTemp = "";
            string zaAlarm = "";
            Console.WriteLine("*******Logovi sa upravljačke ploče*******\n");
            Console.WriteLine("Temperatura: {0}\n Vlažnost u kući: {1}\n Hodnik vrata: {2}\n Glavna vrata: {3}\n Hladnjak: {4}\n Status alarma: {5}\n", t, v, hodnik, glavna, hladnjak, alarm);
            if (alarm == 0)
            {
                Console.WriteLine("Alarm nije ok");
                zaAlarm = DateTime.Now + " - Greška - status alarma\n";
                //Sender.Tell(new Poruka(zaAlarm));
            }
            else
            {
                //Sender.Tell(new Poruka("U redu"));
            }

            if(t<=13 || t>=28)
            {
                Console.WriteLine("Pali se klima...");
                zaTemp= DateTime.Now + " - Klima upaljena.";
            }
            else
            {
                //Sender.Tell(new Poruka("Temperatura u redu"));
            }
            Sender.Tell(new Poruka(zaAlarm, zaTemp));
        }
    }
    
}