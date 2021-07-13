using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_mail
{
    class Msg
    {
        public string Msg1 { get; }
        public Msg(string logMsg)
        {
             Msg1=logMsg;
        }
    }

    class Path
    {
        public string Putanj { get; }
        public Path(string putanja)
        {
            Putanj = putanja;
        }
    }

    class TijeloPoruke
    {
        public string Tijelo { get; }
        public TijeloPoruke(string tijelo)
        {
            Tijelo = tijelo;
        }
    }
}
