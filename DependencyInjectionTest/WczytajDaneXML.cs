using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace DependencyInjectionTest
{
    public class WczytajDaneXML : IWczytajDane
    {
        public void LoadData()
        {
            decimal kurs = 0m;
            Console.WriteLine("--Czytam XML--");
            XmlReader xmlReader = XmlReader.Create("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            string waluta = "PLN";
            decimal kwota = 100;
            while (xmlReader.Read())
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
            Console.WriteLine("--Odczyt zakonczony--");
        }
    }
}
