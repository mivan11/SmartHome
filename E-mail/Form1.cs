using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using Akka.Actor;
using System.IO;

namespace E_mail
{
    public partial class Form1 : Form
    {
        IActorRef formAktor;
        public string porukaAktora="";
        public string autoPorukaAktora = "";
        public void posaljiEmail()
        {
            string posiljatelj, primatelj, lozinka;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            posiljatelj = "pametna.kuca1920@gmail.com";
            mail.From = new MailAddress(posiljatelj);         
            primatelj = "pametna.kuca1920@gmail.com";
            mail.To.Add(primatelj);
            mail.Subject = textBox2.Text;
            if (mail.Subject == "")
                mail.Subject = "Test";
            porukaAktora = textBox3.Text;
            mail.Body = porukaAktora;
            /*if (mail.Body == "" || mail.Body==null)
            {
                formAktor.Tell(new Msg("../../../Datoteke/logovi.txt"));
                autoPorukaAktora = textBox3.Text;
                mail.Body= autoPorukaAktora;
            }*/
            lozinka = "###############";
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(posiljatelj, lozinka);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer2.Start();
            Props props = Props.Create(() => new Aktor1(textBox3)).WithDispatcher("akka.actor.synchronized-dispatcher"); 
            formAktor = Program.System.ActorOf(props);
            //aktor1.Tell(new Msg(textBox3.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pošalji
            posaljiEmail(); //ručno
            //textBox2.Text = "";
            //textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //popuni
            string path = "../../../Datoteke/logovi.txt";
            formAktor.Tell(new Msg(path));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //ako postoji želja korisnika da se popuni automatski iako se šalje svakih 5 minuta pa nema potrebe
            //aktor1.Tell(new Msg(textBox3.Text));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string path = "../../../Datoteke/logovi.txt";
            formAktor.Tell(new Msg(path));
            //automatski pošalji email
            posaljiEmail(); //svakih 5 minuta
            //textBox3.Text = "";
        }
    }
}
