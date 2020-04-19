
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionTest
{
    public class MojaZaleznosc2 : IMojaZaleznosc
    {
        public MojaZaleznosc2() { }
        public void NapiszWiadomosc(string message)
        {
            Console.WriteLine("Nie wiem o co chodzi, ale wywolano wiadomosc: "+ message);
        }
        //TODO: 3 klasy implementujący metodę WczytajDane
        //1.klasa czytająca z XML
        //2.klasa czytająca z Json
        //3.wstrzyknij klasy do interfejsu i zobacz wynik.

    }
}
