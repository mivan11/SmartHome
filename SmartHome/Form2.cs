using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SmartHome
{
    public partial class Form2 : Form
    {
        string serverAlarmObavijest = "";
        public Form2()
        {
            InitializeComponent();
            IspisiObavijest();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //obavijesti sa servera
            IspisiObavijest();
        }
        private void IspisiObavijest()
        {
            serverAlarmObavijest = "";
            string line;
            StreamReader file = new StreamReader(@"../../../Datoteke/obavijestAlarm.txt");
            while ((line = file.ReadLine()) != null)
            {
                serverAlarmObavijest += line + "\r\n";
            }
            file.Close();
            richTextBox1.Text = serverAlarmObavijest;
        }
    }
}
