using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionTest
{
    public class MojaZaleznosc: IMojaZaleznosc
    {
        public MojaZaleznosc(){}

        public void NapiszWiadomosc(string message)
        {
            Console.WriteLine("Wywolano MojaZaleznosc.NapiszWiadomosc. Wiadomosc :" + message);
        }
    }
}
