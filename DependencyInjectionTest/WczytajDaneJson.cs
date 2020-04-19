using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionTest
{
    public class WczytajDaneJson : IWczytajDane
    {
        public void LoadData()
        {
            var czlowiek1 = new Czlowiek
            {
                Imie = "Kacper",
                Nazwisko = "Niewczas",
                AdresDomowy = new Adres
                {
                    Ulica = "Pilsudskiego",
                    NumerDomu = 112,
                    KodPocztowy = "43-2001",
                    Miasto = "Rybnik"
                }
            };
            var czlowiek2 = new Czlowiek
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                AdresDomowy = new Adres
                {
                    Ulica = "Komorowskiego",
                    NumerDomu = 112,
                    KodPocztowy = "43553001",
                    Miasto = "Krakow"
                }
            };
            var czlowiek3 = new Czlowiek
            {
                Imie = "Jan",
                Nazwisko = "Kowalewski",
                AdresDomowy = new Adres
                {
                    Ulica = "Komorowskiego-123",
                    NumerDomu = 1112,
                    KodPocztowy = "001",
                    Miasto = "Krakow"
                }
            };
            List<Czlowiek> listaLudzi = new List<Czlowiek>();
            listaLudzi.Add(czlowiek1);
            listaLudzi.Add(czlowiek2);
            listaLudzi.Add(czlowiek3);

            string jsonLista = JsonConvert.SerializeObject(listaLudzi, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(jsonLista);

            var lista = JArray.Parse(jsonLista);
            // pokaż imię, nazwisk i adres ludzi którzy nie mieszkają w Krakowie
            Console.WriteLine("Pokaż imię, nazwisk i adres ludzi którzy nie mieszkają w Krakowie");
            var inneNizKrk = from Czlowiek in lista
                             where Convert.ToString(Czlowiek["AdresDomowy"]["Miasto"]) != "Krakow"
                             select new { imie = Czlowiek["Imie"], nazwisko = Czlowiek["Nazwisko"], ulica = Czlowiek["AdresDomowy"]["Ulica"] };

            foreach (var osoba in inneNizKrk)
            {
                Console.WriteLine(osoba);
            }
        }
    }
}
