using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System.Configuration;

namespace E_mail
{
    static class Program
    {
        public static ActorSystem System { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System = ActorSystem.Create("Email");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
