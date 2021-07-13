using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;
using Akka.Cluster;
using Zadnja;
using System.IO;
using Akka.Routing;
using System.Drawing.Text;

namespace SmartHome
{
   
    class actor : ReceiveActor
    {
        string ala = "";
        string zamjenska = "";
        
        public actor(RichTextBox rtb1, RichTextBox rtb2, RichTextBox rtb3, RichTextBox rtb4, RichTextBox rtb5, TextBox txtIspis)
        {
            Receive<Msg>(x => stvoriNovog(x.Content));         
            Receive<Mjerenje>(x => dohvati(x.vlaznost, x.temperatura, x.alarmProvala, x.glavnaVrataZakljucanaOtkljucana, x.hodnikVrata, x.hladnjakVrata));
            Receive<Log>(x => dohvatiLog(x.Logs));
            Receive<Podaci>(x=> 
            {
                zamjenska = x.hodnikVrata.ToString();
                txtIspis.Text+=zamjenska;
            });
            Receive<Poruka>(x =>
            {
                txtIspis.Text = x.Message + x.T;
                ala += txtIspis.Text + "\n";
                using (StreamWriter spremiAlarm = new StreamWriter("../../../Datoteke/obavijestAlarm.txt"))
                {
                    spremiAlarm.WriteLine(ala);
                }
            });

            void stvoriNovog(string content)
            {
                Props props2 = Props.Create(() => new logAktor());
                IActorRef logActor = Context.ActorOf(props2);
                //logActor.Tell(new Log(content));
                Props props3 = Props.Create(() => new dijete());
                IActorRef actor2 = Context.ActorOf(props3);
                actor2.Tell(new Msg(content));
               
            }

            
            void dohvati(int v, int t, int alarmStanje, int glavnaVrata, int hodnikVrata, int hladnjakVrata)
            {

                Program.System.ActorSelection("akka.tcp://Server@localhost:9000/user/AktorServer").Tell(new Podaci(v, t, alarmStanje, glavnaVrata, hodnikVrata, hladnjakVrata));

                //string txt = "";
                string statusVrata = "";
                string statusAlarm = "";
                string hodnikStatus = "";
                string hladnjakStatus = "";

                /*if (glavnaVrata == 1)
                {
                    statusVrata = "Otključana";
                }
                else
                {
                    statusVrata = "Zaključana";
                }*/

                if (alarmStanje == 1)
                {
                    statusAlarm = "ok";
                }
                else
                {
                    statusAlarm = "error";
                }

                statusVrata = provjeriStanje(glavnaVrata);
                hodnikStatus = provjeriStanje(hodnikVrata);
                hladnjakStatus = provjeriStanje(hladnjakVrata);

                rtb1.Text = v + "%";
                rtb2.Text = t + "°C";
                rtb5.Text = statusAlarm;
                rtb3.Text = "Glavna vrata: " + statusVrata + "\nHodnik vrata: " + hodnikStatus + "\nHladnjak vrata: " + hladnjakStatus;
                //rtb4.Text = "\n" + DateTime.Now + "\n" + rtb1.Text + "\n" + rtb2.Text + "\n" + rtb3.Text;                
            }

            void dohvatiLog(string logovi)
            {
                if (rtb1.Text.Length==0 || rtb2.Text.Length == 0 || rtb3.Text.Length == 0 || rtb5.Text.Length == 0)
                {
                    //ne čini ništa
                }
                else
                {
                    rtb4.Text += DateTime.Now + "\n" + "Vlažnost: " + rtb1.Text + "\n" + "Temperatura: " + rtb2.Text + "\n" + rtb3.Text + "\n" + "Status alarma: " + rtb5.Text+"\n";
                }
            }

    

            string provjeriStanje(int status)
            {
                string vrataStr = "";
                if (status == 1)
                {
                    vrataStr = "Zaključana";
                }
                else
                {
                    vrataStr = "Otključana";
                }
                return vrataStr;
            }
        }
    }
    class dijete : ReceiveActor
    {
        
        public string povratna = "";
        public dijete()
        {
            Receive<Msg>(x =>
            {
                Random rnd = new Random();
                int vl = rnd.Next(30, 60);
                int te = rnd.Next(10, 35);
                int alarmStatus = rnd.Next(0, 2);
                int glavnaVrataZakljOtklj = rnd.Next(0, 2);
                int hodnikVrataZakljOtklj = rnd.Next(0, 2);
                int hladnjakVrataZakljOtklj = rnd.Next(0, 2);

                Context.Parent.Tell(new Mjerenje(vl, te, alarmStatus, glavnaVrataZakljOtklj, hodnikVrataZakljOtklj, hladnjakVrataZakljOtklj));

                //Program.System.ActorSelection("akka.tcp://Server@localhost:9000/user/AktorServer").Tell(new Podaci(vl, te, alarmStatus, glavnaVrataZakljOtklj, hodnikVrataZakljOtklj, hladnjakVrataZakljOtklj));
                //Program.System.ActorSelection("akka.tcp://Server@localhost:12345/user/AktorServer").Tell(new Poruka("test uspješan"));
                
            });

        }
    }

    class logAktor : ReceiveActor
    {
        public logAktor()
        {
            Receive<Log>(x =>
            {
                string logovi="";
                logovi = x.Logs;

                Context.Parent.Tell(new Log(logovi));
                //Context.ActorSelection("akka.tcp://SmartHome@localhost:12345/user/AktorServer").Tell(new test(logovi));
            });
        }
    }

    

}
