using System;
using System.Collections.Generic;
using System.Text;

namespace WprowadzenieDoApi
{
    class Osoba
    {
        public string Imie { get; set; }
        public int Wiek { get; set; }
    }

    class Czlowiek
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Adres AdresDomowy { get; set; }
    }
    class Adres
    {
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public int NumerDomu { get; set; }
        public string KodPocztowy { get; set; }
    }
}
