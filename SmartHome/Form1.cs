using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;
using System.IO;
using Zadnja;

namespace SmartHome
{
    public partial class Form1 : Form
    {
        IActorRef firstActor;
        public string txt = "";
        public string start="Start";
        public Form1()
        {
            InitializeComponent();
            Props props = Props.Create(() => new actor(richTextBox1, richTextBox3, richTextBox4, richTextBox2, richTextBox5, textBox1)).WithDispatcher("akka.actor.synchronized-dispatcher");
            firstActor = Program.System.ActorOf(props);
            firstActor.Tell(new Msg(start));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            firstActor.Tell(new Msg(start));
            firstActor.Tell(new Log(start));

            txt = richTextBox2.Text;
            using (StreamWriter spremiLog = new StreamWriter("../../../Datoteke/logovi.txt"))
            {
                spremiLog.WriteLine(txt);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //izbrisi logove
            richTextBox2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //server logovi 
            Form2 f2 = new Form2();
            f2.Show();
        }
    }    
}

