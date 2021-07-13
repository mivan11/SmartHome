using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using SmartHome;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace E_mail
{
    class Aktor1 : ReceiveActor
    { 
        IActorRef aktorPopuni;
        public Aktor1(TextBox okvir)
        {
            Receive<Msg>(x => HandleMsg(x.Msg1));
            Receive<TijeloPoruke>(x =>
            {
                okvir.Text = x.Tijelo;
            });        
        }
        
        private void HandleMsg(string putanja)
        {
            Props prop = Props.Create<Aktor2>();
            aktorPopuni = Context.ActorOf(prop);
            aktorPopuni.Tell(new Path(putanja));
        }          
    }
    
    class Aktor2 : ReceiveActor
    {
        string logovi;
        public Aktor2()
        {
            Receive<Path>(x => Popuni(x.Putanj));
        }

        private void Popuni(string putanja)
        {
            logovi = "";
            string line;
            StreamReader file = new StreamReader(putanja);
            while ((line = file.ReadLine()) != null)
            {
                logovi += line + "\r\n";
            }
            file.Close();
            //okvir.Text = logovi;
            Sender.Tell(new TijeloPoruke(logovi));
        }
    }
}

