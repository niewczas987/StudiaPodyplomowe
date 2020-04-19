using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace WprowadzenieDoApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program pokazujący wprowadzenie do API");
            Console.WriteLine("Wybierz co chcesz zrobić:");
            Console.WriteLine("[1] - sprawdź jak działa odczyt XML z kursami walut");
            Console.WriteLine("[2] - dokonaj zapisu do pliku XML");
            Console.WriteLine("[3] - deserializuj Json do słownika");
            Console.WriteLine("[4] - deserializuj Json do obiektu");
            Console.WriteLine("[5] - serializacja obiektu");
            Console.WriteLine("[6] - przykład Linq to Json");

            int wybor = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
            switch (wybor)
            {
                case 1:
                    ReadXML();
                    break;
                case 2:
                    List<Person> listaOsob = new List<Person>();
                    Person osoba1 = new Person("Jan", "Kowalski", "12-01-1986", "Murarz");
                    Person osoba2 = new Person("Janusz", "Pawlacz", "14-05-1886", "Tynkarz");
                    listaOsob.Add(osoba1);
                    listaOsob.Add(osoba2);

                    WriteXML(listaOsob);
                    break;
                case 3:
                    DeserialisationToDictionary();
                    break;
                case 4:
                    DeserialisationToClass();
                    break;
                case 5:
                    SerialisationOfObject();
                    break;
                case 6:
                    Linq2Json();
                    break;
                default:
                    Console.WriteLine("Nie ma takiej opcji.");
                    break;
            }
            
            Console.ReadKey();
        }

        static void TestJson()
        {
            var example = @"{""imię"":""Janusz Wielki"",""wiek"":42}";
            Console.ReadKey();
        }
        //deserializacja do słownika
        static void DeserialisationToDictionary()
        {
            var example = @"{""imię"":""Janusz Wielki"",""wiek"":42}";
            var dictPerson = JsonConvert.DeserializeObject<Dictionary<string, string>>(example);
            Console.WriteLine("Osoba ma imię :" + dictPerson["imię"] + " i ma " + dictPerson["wiek"] +" lat.");
        }
        //deserializacja do klasy
        static void DeserialisationToClass()
        {
            var example = @"{""imie"":""Janusz Wielki"",""wiek"":42}";
            var classPerson = JsonConvert.DeserializeObject<Osoba>(example);
            Console.WriteLine("Osoba ma imię :" + classPerson.Imie + " i ma " + classPerson.Wiek +" lat.");
        }
        //serializacja obiektu
        static void SerialisationOfObject()
        {
            var czlowiek = new Czlowiek
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
            var tekst = JsonConvert.SerializeObject(czlowiek);
            var tekst2 = JsonConvert.SerializeObject(czlowiek, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine("Obiekt tekst1:");
            Console.WriteLine(tekst);
            Console.WriteLine("Obiekt tekst2: ");
            Console.WriteLine(tekst2);
        }
        // LINQ to Json
        static void Linq2Json()
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

            foreach(var osoba in inneNizKrk)
            {
                Console.WriteLine(osoba);
            }
        }

        //zapis do pliku XML
        static void WriteXML(List<Person> list)
        {
            Console.WriteLine("--Zapisuję do XML--");
            XmlWriter xmlWriter = XmlWriter.Create("osoby.xml");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Ludzie");
            foreach(Person osoba in list)
            {
                xmlWriter.WriteStartElement("Czlowiek"+(list.IndexOf(osoba)+1));
                xmlWriter.WriteAttributeString("Imie", osoba.Name);
                xmlWriter.WriteAttributeString("Nazwisko", osoba.Surname);
                xmlWriter.WriteAttributeString("DataUrodzenia", osoba.DateOfBirth);
                xmlWriter.WriteAttributeString("Zawod", osoba.Job);
                xmlWriter.WriteString("Osoba wpisana:" + osoba.Name +" "+ osoba.Surname);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            Console.WriteLine("--Zapis zakończony--");

        }
        //odczyt z pliku XML
        static void ReadXML()
        {
            decimal kurs = 0m;
            Console.WriteLine("--Czytam XML--");
            XmlReader xmlReader = XmlReader.Create("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            Console.WriteLine("Podaj walutę:");
            string waluta = Console.ReadLine();
            Console.WriteLine("Podaj kwote:");
            decimal kwota = Convert.ToDecimal(Console.ReadLine(), CultureInfo.InvariantCulture);
            while(xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Cube" && xmlReader.HasAttributes)
                {
                    var checkCurrency = xmlReader.GetAttribute("currency");
                    if (!string.IsNullOrEmpty(checkCurrency) && checkCurrency == waluta)
                    {
                        kurs = Convert.ToDecimal(xmlReader.GetAttribute("rate"), CultureInfo.InvariantCulture);
                        Console.WriteLine("Twoja kwota to:" + decimal.Round(kwota / kurs, 2) + " EUR");
                    }
                }

            }
            Console.WriteLine("--Odczyt zakończony--");
        }

    }
}
