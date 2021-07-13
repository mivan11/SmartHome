using System.Dynamic;

namespace Zadnja
{
        public class Msg
        {
            public string Content { get; private set; }
            public Msg(string messagge)
            {
                Content = messagge;
            }
        }
        public class Mjerenje
        {
            public int vlaznost { get; private set; }
            public int temperatura { get; private set; }

            public int alarmProvala { get; private set; }
            public int glavnaVrataZakljucanaOtkljucana { get; private set; }

            public int hodnikVrata { get; private set; }
            public int hladnjakVrata { get; private set; }

            public Mjerenje(int vlaz, int temp, int alarmStanje, int glavna, int hodnik, int hladnjak)
            {
                vlaznost = vlaz;
                temperatura = temp;
                alarmProvala = alarmStanje;
                glavnaVrataZakljucanaOtkljucana = glavna;
                hodnikVrata = hodnik;
                hladnjakVrata = hladnjak;
            }
        }

        public class Log
        {
            public string Logs { get; private set; }
            public Log(string logMsg)
            {
                Logs = logMsg;
            }
        }

    public class Poruka
    {
        public string Message { get; private set; }
        public string T { get; private set; }
     
        public Poruka(string messagge, string t)
        {
            Message = messagge;
            T = t;
        }
    }

    public class Podaci
    {
        public int vlaznost { get; private set; }
        public int temperatura { get; private set; }

        public int alarmProvala { get; private set; }
        public int glavnaVrataZakljucanaOtkljucana { get; private set; }

        public int hodnikVrata { get; private set; }
        public int hladnjakVrata { get; private set; }

        public Podaci(int vlaz, int temp, int alarmStanje, int glavna, int hodnik, int hladnjak)
        {
            vlaznost = vlaz;
            temperatura = temp;
            alarmProvala = alarmStanje;
            glavnaVrataZakljucanaOtkljucana = glavna;
            hodnikVrata = hodnik;
            hladnjakVrata = hladnjak;
        }
    }

    public class Test
    {
        public int Vlaznost { get; private set; }
       

        public Test(int vlaz)
        {
            Vlaznost = vlaz;
         
        }
    }


}
